using IdentityModel.Client;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NGP.Framework.Core;
using NGP.Framework.Core.Filters;
using NGP.Identity.Api.Entities;
using NGP.Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace NGP.Identity.Api.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [NGPValidator]
    [Route("connect")]
    public class NGPOAuthController : ControllerBase
    {
        private readonly IUnitRepository _repository;
        private readonly IWebHelper _webHelper;
        /// <summary>
        /// 文件提供者
        /// </summary>
        private readonly INGPFileProvider _fileProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="webHelper"></param>
        /// <param name="workContext"></param>
        /// <param name="configuration"></param>
        public NGPOAuthController(IUnitRepository repository, IWebHelper webHelper, INGPFileProvider fileProvider)
        {
            _repository = repository;
            _webHelper = webHelper;
            _fileProvider = fileProvider;
        }

        #region 获取验证码
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost("sendverificationcode")]
        [AllowAnonymous]
        public ActionResult<NGPResponse> SendVerificationCode([FromBody]NGPSingleRequest userName)
        {
            // 数据库查询
            var employee = _repository.FirstOrDefault<SysOrg_Employee>(s => s.LoginName == userName.RequestData);
            // 用户不存在
            if (employee == null)
            {
                return NGPResponse.Create(Framework.Core.StatusCode.NotExistErrorOne, userName.RequestData);
            }
            // 用户被删除
            if (employee.IsDelete)
            {
                return NGPResponse.Create(Framework.Core.StatusCode.HasBeenDeletedError, userName.RequestData);
            }
            //判断当前用户是否填写手机号
            if (string.IsNullOrWhiteSpace(employee.PhoneNumber))
            {
                return NGPResponse.Create(Framework.Core.StatusCode.UserPhoneEmpty);
            }
            var randomItem = new Random();
            var verificationCode = Convert.ToString(randomItem.Next(100000, 999999));
            // TODO 短信发送单列
            var messageSend = Singleton<IEngine>.Instance.Resolve<IMobileMessageSend>();
            messageSend.SendMessage(new MobileSendContext { Mobiles = new List<string> { employee.PhoneNumber }, Content = string.Format("您本次登录的验证码为【{0}】。", verificationCode) });
            var insertItem = new SysOrg_EmpVerification
            {
                Id = CommonHelper.NewGuid(),
                EmpId = employee.Id,
                SendTime = DateTime.Now,
                VerificationCode = verificationCode,
            };
            insertItem.CreatedBy = "System";
            insertItem.CreatedTime = DateTime.Now;
            insertItem.CreatedArea = "32";
            insertItem.CreatedCompany = "System";
            insertItem.OrderIndex = 0;
            insertItem.UpdatedBy = "System";
            insertItem.UpdatedTime = DateTime.Now;
            insertItem.UpdatedArea = "System";
            insertItem.UpdatedCompany = "System";
            insertItem.IsDelete = false;
            _repository.Insert(insertItem);
            _repository.SaveChanges();

            return Ok(NGPResponse.Create());
        }
        #endregion

        #region 重置密码
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        public ActionResult<NGPResponse> ResetPassword([FromBody]ResetPasswordRequest request)
        {
            //获取人员Id
            var employee = _repository.FirstOrDefault<SysOrg_Employee>(s => s.LoginName == request.LoginName);
            // 用户不存在
            if (employee == null)
            {
                return NGPResponse.Create(Framework.Core.StatusCode.NotExistErrorOne, request.LoginName);
            }
            var empId = employee.Id;
            // 验证码校验
            var verItem = _repository.All<SysOrg_EmpVerification>(s => s.IsDelete == false && s.EmpId == empId).OrderByDescending(s => s.SendTime).FirstOrDefault();
            // 验证码错误
            if (verItem == null || !verItem.VerificationCode.Equals(request.VerificationCode))
            {
                return NGPResponse.Create(Framework.Core.StatusCode.VerificationCodeError);
            }

            employee.Password = CommonHelper.Encrypt(request.Password);
            employee.InitUpdateDefaultFields();
            _repository.Update(employee);
            _repository.SaveChanges();
            return Ok(NGPResponse.Create());
        }
        #endregion



        #region refresh_token
        /// <summary>
        /// refresh_token
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpPost("companytoken")]
        public ActionResult<JObject> CompanyToken([FromBody] CompanyTokenRequest tokenRequest)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _webHelper.GetStoreHost(false),
                Policy = { RequireHttps = false }
            }).Result;

            if (disco.IsError)
            {
                throw new NGPException(disco.Error);
            }

            //获取人员Id
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var employee = _repository.FindById<SysOrg_Employee>(workContext.Current.EmplId);

            // request token
            var request = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "debt_api_client",
                ClientSecret = "debt_secret",
                UserName = employee.LoginName,
                Password = CommonHelper.Decrypt(employee.Password),
                GrantType = GrantType.ResourceOwnerPassword
            };
            request.Parameters["company_id"] = tokenRequest.CompanyId;

            var tokenResponse = client.RequestPasswordTokenAsync(request).Result;
            return Ok(tokenResponse.Json);
        }
        #endregion

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileRequest"></param>
        /// <returns></returns>        
        [HttpPost("UploadAuthFile")]
        [DisableRequestSizeLimit]
        [AllowAnonymous]
        public async Task<ActionResult<NGPResponse>> UploadAuthFile(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                // 创建划分的文件夹
                var folderPath = _fileProvider.MapPath("authfile");

                if (_fileProvider.DirectoryExists(folderPath))
                {
                    _fileProvider.DeleteDirectory(folderPath);
                }
                _fileProvider.CreateDirectory(folderPath);

                // 完整路径
                var fullPath = Path.Combine(folderPath, formFile.FileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return Ok(NGPResponse.Create());
        }

        /// <summary>
        /// 系统认证
        /// </summary>
        /// <returns></returns>
        [HttpPost("AppAuth")]
        [AllowAnonymous]
        public ActionResult<NGPDataResponse<AppAuthResponse>> AppAuth()
        {
            var folderPath = _fileProvider.MapPath("authfile");
            // 完整路径
            var fullPath = Path.Combine(folderPath, "LGDAuth.pfx");
            var keyFilePassword = "lgd9o0p-[=]";

            if (!System.IO.File.Exists(fullPath))
            {
                throw new NGPException(Framework.Core.StatusCode.AuthFileException);
            }
            var cer = new X509Certificate2(fullPath, keyFilePassword);

            var content = cer.Subject.Split('=').LastOrDefault().Split('&');
            var date = DateTime.Parse(DateTime.Now.ToShortDateString());
            var expireDate = DateTime.Now;
            DateTime.TryParse(content[1], out expireDate);
            var result = new AppAuthResponse
            {
                AreaTag = content[0],
                IsExpired = DateTime.Compare(expireDate, date) >= 0
            };
            return Ok(NGPDataResponse<AppAuthResponse>.Create(result));
        }
    }
}