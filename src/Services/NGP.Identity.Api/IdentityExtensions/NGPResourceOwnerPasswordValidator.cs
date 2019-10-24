// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using NGP.Framework.Core;
using NGP.Identity.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NGP.Identity.Api.IdentityExtensions
{
    /// <summary>
    /// Resource owner password validator for test users
    /// </summary>
    /// <seealso cref="IdentityServer4.Validation.IResourceOwnerPasswordValidator" />
    public class NGPResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ISystemClock _clock;
        private readonly IWebHelper _webHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NGPResourceOwnerPasswordValidator"/> class.
        /// </summary>
        /// <param name="unitRepository">unitRepository.</param>
        /// <param name="clock">The clock.</param>
        /// <param name="webHelper">webHelper.</param>
        public NGPResourceOwnerPasswordValidator(IUnitRepository unitRepository, ISystemClock clock, IWebHelper webHelper)
        {
            _unitRepository = unitRepository;
            _clock = clock;
            _webHelper = webHelper;
        }

        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var dtNow = DateTime.Now;
            // 人员验证
            var employee = _unitRepository.FirstOrDefault<SysOrg_Employee>(s => s.LoginName == context.UserName && s.IsDelete == false);
            if (employee == null)
            {
                InsertLoginRecord(null, context.UserName);

                // "用户不存在!"

                // TODO
                context.Result.CustomResponse = NGPResponse.Create(StatusCode.UserNotExistOrDeleted).ToDictionary();
                return Task.CompletedTask;
            }
            if (employee.IsDelete)
            {
                InsertLoginRecord(null, context.UserName);
                context.Result.CustomResponse = NGPResponse.Create(StatusCode.HasDeleted).ToDictionary();
                return Task.CompletedTask;
            }
            //获取当前用户登录验证信息
            var verificationInfo = _unitRepository.All<SysOrg_EmpVerification>(s => s.IsDelete == false && s.EmpId == employee.Id).OrderByDescending(s => s.SendTime).FirstOrDefault();


            var verificationCode = string.Empty;
            if (context.Request.Raw.AllKeys.Contains("verification_code"))
            {
                verificationCode = context.Request.Raw["verification_code"];
            }

            //用户已锁定
            if (employee.IsLocking.HasValue && employee.IsLocking.Value)
            {
                //判断传参是否有验证码
                if (!string.IsNullOrWhiteSpace(verificationCode))
                {
                    //判断验证码是否正确
                    if (verificationInfo != null && verificationCode == verificationInfo.VerificationCode)
                    {
                        employee.IsLocking = false;
                        employee.FailureTimes = 0;
                    }
                    else
                    {
                        InsertLoginRecord(employee, context.UserName, false);
                        context.Result.CustomResponse = NGPResponse.Create(StatusCode.WrongCode).ToDictionary();
                        return Task.CompletedTask;
                    }
                    //获取时间差
                    var mins = dtNow.Subtract(verificationInfo.SendTime.Value).Minutes;
                    //判断验证码是否失效
                    if (mins > 5)
                    {
                        InsertLoginRecord(employee, context.UserName, false);
                        context.Result.CustomResponse = NGPResponse.Create(StatusCode.OverdueCode).ToDictionary();
                        return Task.CompletedTask;
                    }
                }
                else
                {
                    //判断该账户是否绑定手机
                    if (string.IsNullOrEmpty(employee.PhoneNumber))
                    {
                        InsertLoginRecord(employee, context.UserName, false);
                        context.Result.CustomResponse = NGPResponse.Create(StatusCode.BeLocked).ToDictionary();
                        return Task.CompletedTask;
                    }
                    else
                    {
                        InsertLoginRecord(employee, context.UserName, false);
                        context.Result.CustomResponse = NGPResponse.Create(StatusCode.WarnRelieve).ToDictionary();
                        return Task.CompletedTask;
                    }
                }
            }

            //用户被禁用
            if (employee.AccountStatus.HasValue && employee.AccountStatus == false)
            {
                InsertLoginRecord(employee, context.UserName, false);
                context.Result.CustomResponse = NGPResponse.Create(StatusCode.BeBaned).ToDictionary();
                return Task.CompletedTask;
            }
            // 密码不正确
            if (!string.Equals(employee.Password, CommonHelper.Encrypt(context.Password), StringComparison.CurrentCulture))
            {

                //判断登录失败次数是否已超过5次
                if (employee.FailureTimes.HasValue && employee.FailureTimes.Value >= 4)
                {
                    employee.IsLocking = true;
                }
                else
                {
                    employee.FailureTimes = (employee.FailureTimes ?? 0) + 1;
                }
                _unitRepository.Update(employee);
                InsertLoginRecord(employee, context.UserName, false);
                context.Result.CustomResponse = NGPResponse.Create(StatusCode.WrongPassword).ToDictionary();
                return Task.CompletedTask;
            }
            //判断当前用户是否为白名单
            if (!(employee.IsWhiteList.HasValue && employee.IsWhiteList.Value))
            {
                //判断当前用户是否填写手机号
                if (string.IsNullOrWhiteSpace(employee.PhoneNumber))
                {
                    InsertLoginRecord(employee, context.UserName, false);
                    context.Result.CustomResponse = NGPResponse.Create(StatusCode.UnBindPhone).ToDictionary();
                    return Task.CompletedTask;
                }
                //判断验证码是否正确
                if (string.IsNullOrWhiteSpace(verificationCode))
                {
                    InsertLoginRecord(employee, context.UserName, false);
                    context.Result.CustomResponse = NGPResponse.Create(StatusCode.PleaseSendVerification).ToDictionary();
                    return Task.CompletedTask;
                }
                if (verificationInfo == null || !verificationCode.Equals(verificationInfo.VerificationCode))
                {
                    InsertLoginRecord(employee, context.UserName, false);
                    InsertLoginRecord(employee, context.UserName, false);
                    context.Result.CustomResponse = NGPResponse.Create(StatusCode.WrongCode).ToDictionary();
                    return Task.CompletedTask;
                }
                else
                {
                    //获取时间差
                    var minutes = dtNow.Subtract(verificationInfo.SendTime.Value).Minutes;
                    //判断验证码是否失效
                    if (minutes > 5)
                    {
                        InsertLoginRecord(employee, context.UserName, false);
                        context.Result.CustomResponse = NGPResponse.Create(StatusCode.OverdueCode).ToDictionary();
                        return Task.CompletedTask;
                    }
                }
            }

            // 添加claim
            ICollection<Claim> claims = new HashSet<Claim>(new ClaimComparer())
                {
                    // 用户id
                    new Claim(JwtClaimTypes.Id,employee.Id),
                    // 用户名（登录后显示用）
                    new Claim(JwtClaimTypes.Name,employee.EmployeeName),
                    // AreaId
                    new Claim("ngp_area_id",employee.AreaId),
                    // LoginName
                    new Claim("ngp_login_name",employee.LoginName),
                };

            // 添加公司id
            if (context.Request.Raw.AllKeys.Contains("company_id"))
            {
                claims.Add(new Claim("company_id", context.Request.Raw["company_id"]));
            }

            // 部门id
            if (!string.IsNullOrWhiteSpace(employee.DeptId))
            {
                claims.Add(new Claim("ngp_dept_id", employee.DeptId));
            }

            // 人员类别
            if (!string.IsNullOrWhiteSpace(employee.EmployeeTypes))
            {
                claims.Add(new Claim("ngp_employee_types", employee.EmployeeTypes));
            }

            // 是否系统管理员
            if (employee.IsSystemAdmin.HasValue)
            {
                claims.Add(new Claim("is_system_admin", employee.IsSystemAdmin.ToString()));
            }
            //身份类别
            if (!string.IsNullOrWhiteSpace(employee.IdentityType))
            {
                claims.Add(new Claim("ngp_identity_type", employee.IdentityType));
            }
            // 用户角色
            var roleEmpl = _unitRepository.FirstOrDefault<SysOrg_RoleEmpl>(s => s.EmplId == employee.Id && s.IsDelete == false);
            // 角色信息
            if (roleEmpl != null)
            {
                claims.Add(new Claim("role_id", roleEmpl.RoleId));
                var roleinfo = _unitRepository.FirstOrDefault<SysOrg_Role>(s => s.Id == roleEmpl.RoleId && s.IsDelete == false);
                if (roleinfo != null)
                {
                    claims.Add(new Claim("role_type", roleinfo.RoleType));
                }
            }

            employee.FailureTimes = 0;
            employee.SuccessTimes = (employee.SuccessTimes ?? 0) + 1;
            employee.LoginTime = DateTime.Now;
            _unitRepository.Update(employee);
            InsertLoginRecord(employee, context.UserName, true);

            context.Result = new GrantValidationResult(
                employee.LoginName,
                OidcConstants.AuthenticationMethods.Password,
                _clock.UtcNow.UtcDateTime,
                claims);
            context.Result.CustomResponse = NGPResponse.Create(StatusCode.Success).ToDictionary();
            return Task.CompletedTask;
        }

        #region 添加登录履历
        /// <summary>
        /// 添加登录履历
        /// </summary>
        /// <param name="emplInfo"></param>
        /// <param name="loginName"></param>
        /// <param name="isLoginSucess"></param>
        private void InsertLoginRecord(SysOrg_Employee emplInfo, string loginName, bool isLoginSucess = false)
        {
            //实例新增对象
            var insertItem = new SysOrg_EmplLoginRecord();
            insertItem.Id = CommonHelper.NewGuid();
            insertItem.LoginName = loginName;
            insertItem.IsLoginSucess = isLoginSucess;
            //获取当前登录IP
            insertItem.LoginIP = _webHelper.GetCurrentIpAddress();
            if (emplInfo != null)
            {
                var roleEmpl = _unitRepository.FirstOrDefault<SysOrg_RoleEmpl>(s => s.EmplId == emplInfo.Id && s.IsDelete == false);
                if (roleEmpl != null)
                {
                    insertItem.EmpId = emplInfo.Id;
                    insertItem.EmployeeType = emplInfo.EmployeeTypes;
                    insertItem.AreaId = emplInfo.AreaId;
                    insertItem.RoleId = roleEmpl == null ? string.Empty : roleEmpl.RoleId;
                }
            }
            insertItem.InitAddDefaultFields();
            _unitRepository.Insert(insertItem);
            _unitRepository.SaveChanges();
        }
        #endregion
    }
}