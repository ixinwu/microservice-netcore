/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * StatusCode Description:
 * 状态码定义
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-15   hulei@ixinwu.com
 * Updated                          1.0         2019-8-29   hulei@ixinwu.com 
 *
 * ------------------------------------------------------------------------------*/

using System.ComponentModel;
using System.Linq;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 状态码定义
    /// 4000后为业务状态码
    /// </summary>
    public enum StatusCode
    {
        #region 登录验证状态码 100开始
        /// <summary>
        /// 用户不存在！
        /// </summary>
        [Description("用户不存在！")]
        NotExist = 100,
        /// <summary>
        /// 用户被删除！
        /// </summary>
        [Description("用户被删除！")]
        HasDeleted = 101,
        /// <summary>
        /// 验证码错误，请重试！
        /// </summary>
        [Description("验证码错误，请重试！")]
        WrongCode = 102,
        /// <summary>
        /// 验证码已失效，请重试！
        /// </summary>
        [Description("验证码已失效，请重试！")]
        OverdueCode = 103,
        /// <summary>
        /// 用户被锁定！请联系管理员！
        /// </summary>
        [Description("用户被锁定！请联系管理员！")]
        BeLocked = 104,
        /// <summary>
        /// 此账号已被锁定，是否通过手机短信验证解锁账号！
        /// </summary>
        [Description("此账号已被锁定，是否通过手机短信验证解锁账号！")]
        WarnRelieve = 105,
        /// <summary>
        /// 用户被禁用！请联系管理员！
        /// </summary>
        [Description("用户被禁用！请联系管理员！")]
        BeBaned = 106,
        /// <summary>
        /// 用户被禁用！请联系管理员！
        /// </summary>
        [Description("密码错误,请重试！")]
        WrongPassword = 107,
        /// <summary>
        /// 此账号用户未绑定手机号码，请联系账号对应管理员或联系客服。
        /// </summary>
        [Description("此账号用户未绑定手机号码，请联系账号对应管理员或联系客服。")]
        UnBindPhone = 108,
        /// <summary>
        /// 用户不存在或已被删除！
        /// </summary>
        [Description("用户不存在或已被删除！")]
        UserNotExistOrDeleted = 109,
        /// <summary>
        /// 请获取验证码后登录。
        /// </summary>
        [Description("请获取验证码后登录。")]
        PleaseSendVerification = 110,
        
        #endregion

        /// <summary>
        /// 操作成功！
        /// </summary>
        [Description("操作成功！")]
        Success = 200,

        /// <summary>
        /// 系统异常！请联系管理员！
        /// </summary>
        [Description("系统异常！请联系管理员！")]
        SystemException = 500,

        /// <summary>
        /// 未知的依赖！请联系管理员！
        /// </summary>
        [Description("未知的依赖！请联系管理员！")]
        DependencyError = 501,

        /// <summary>
        /// 未知构造函数！请联系管理员！
        /// </summary>
        [Description("未知构造函数！请联系管理员！")]
        NoConstructorError = 502,

        /// <summary>
        /// 在类型{1}的实例上找不到属性{0}！
        /// </summary>
        [Description("在类型{1}的实例上找不到属性{0}！")]
        TypeNotPropertyError = 503,

        /// <summary>
        /// 类型属性错误
        /// </summary>
        [Description("类型{1}的实例上的属性{0}没有setter。！")]
        TypePropertyNotSetError = 504,

        /// <summary>
        /// 数据库系统异常！请联系管理员！
        /// </summary>
        [Description("数据库系统异常！请联系管理员！")]
        DBException = 600,

        /// <summary>
        /// 数据库操作失败！请刷新重试！
        /// </summary>
        [Description("数据库操作失败！请刷新重试！")]
        DBOperatorError = 601,

        /// <summary>
        /// 操作文件系统异常！请联系管理员！
        /// </summary>
        [Description("操作文件系统异常！请联系管理员！")]
        FileException = 700,

        /// <summary>
        /// 授权文件系统异常！请联系管理员！
        /// </summary>
        [Description("授权文件错误！请联系管理员！")]
        AuthFileException = 700,

        /// <summary>
        /// 网络异常，请检查网络！
        /// </summary>
        [Description("网络异常，请检查网络！")]
        InternetException = 800,

        /// <summary>
        /// 没有操作权限，请联系管理员！
        /// </summary>
        [Description("没有操作权限，请联系管理员！")]
        AuthorizationError = 1000,


        /// <summary>
        /// {0}不能为空！
        /// </summary>
        [Description("{0}不能为空！")]
        EmptyError = 2000,

        /// <summary>
        /// {0}：{1}已经存在，不能操作!
        /// </summary>
        [Description("{0}：{1}已经存在，不能操作!")]
        AlreadyExistError = 2001,

        /// <summary>
        /// {0}：{1}已经被使用,不能操作！
        /// </summary>
        [Description("{0}：{1}已经被使用,不能操作！")]
        AlreadyUsedError = 2002,

        /// <summary>
        /// {0}已经存在，不能操作!
        /// </summary>
        [Description("{0}已经存在，不能操作!")]
        AlreadyExistErrorOne = 2003,

        /// <summary>
        /// {0}：{1}不存在，不能操作
        /// </summary>
        [Description("{0}：{1}不存在，不能操作")]
        NotExistError = 2010,

        /// <summary>
        /// {0}不存在，不能操作
        /// </summary>
        [Description("{0}不存在，不能操作")]
        NotExistErrorOne = 2011,

        /// <summary>
        /// {0}长度必须小于{1}！
        /// </summary>
        [Description("{0}长度必须小于{1}！")]
        MaxLengthError = 2020,

        /// <summary>
        /// {0}长度必须大于{1}！
        /// </summary>
        [Description("{0}长度必须大于{1}！")]
        MinLengthError = 2021,

        /// <summary>
        /// {0}长度必须在{1}-{2}之间！
        /// </summary>
        [Description("{0}长度必须在{1}-{2}之间！")]
        BetweenLengthError = 2022,


        /// <summary>
        /// 操作的数据在数据库不存在,不能操作！
        /// </summary>
        [Description("操作的数据在数据库不存在,不能操作！")]
        DBNotExistError = 2030,

        /// <summary>
        /// {0}必须是{1}类型！
        /// </summary>
        [Description("{0}必须是{1}类型！")]
        TypeError = 2100,

        /// <summary>
        /// {0}格式错误，正确的格式是{1}！
        /// </summary>
        [Description("{0}格式错误，正确的格式是{1}！")]
        FormatError = 2200,

        /// <summary>
        /// {0}已经被删除，不能操作！
        /// </summary>
        [Description("{0}已经被删除，不能操作！")]
        HasBeenDeletedError = 2300,

        /// <summary>
        /// {0}必须小于{1}！
        /// </summary>
        [Description("{0}必须小于{1}！")]
        MaxValueError = 2400,

        /// <summary>
        /// {0}必须大于{1}！
        /// </summary>
        [Description("{0}必须大于{1}！")]
        MinValueError = 2401,

        /// <summary>
        /// {0}必须在{1}-{2}之间！
        /// </summary>
        [Description("{0}必须在{1}-{2}之间！")]
        BetweenValueError = 2402,

        /// <summary>
        /// 不允许上传空白附件!
        /// </summary>
        [Description("不允许上传空白附件!")]
        UploadEmptyFileError = 2500,

        /// <summary>
        /// 未知DSL，请检查DSL是否正确!
        /// </summary>
        [Description("未知DSL，请检查DSL是否正确!")]
        UnknowDSLError = 3000,

        /// <summary>
        /// 统计总个数{0}小于错误个数{1}!
        /// </summary>
        [Description("统计总个数{0}小于错误个数{1}!")]
        NoTotalError = 4000,

        #region 业务错误码以4000开始

        /// <summary>
        /// 删除的信息中存在债务类型为存量债务或者审核状态为已通过的债务信息！
        /// </summary>
        [Description("删除的信息中存在债务类型为存量债务或者审核状态为已通过的债务信息！")]
        DeleteDebtCheck = 4001,

        /// <summary>
        /// 存在报警信息，暂时无法删除。
        /// </summary>
        [Description("存在报警信息，暂时无法删除。")]
        DeleteDebtCheckMessageAlarm = 4002,

        /// <summary>
        /// 存在预警信息，暂时无法删除。
        /// </summary>
        [Description("存在预警信息，暂时无法删除。")]
        DeleteDebtCheckMessageWarn = 4003,

        /// <summary>
        /// 该债务余额为0
        /// </summary>
        [Description("该债务余额为0")]
        DebtBalanceCheck = 4004,

        /// <summary>
        /// 该债务非隐性债务或者审核不通过。
        /// </summary>
        [Description("该债务非隐性债务或者审核不通过。")]
        CheckDebtByContractNumber = 4005,

        /// <summary>
        /// 该债务存在未完结的还款记录！
        /// </summary>
        [Description("该债务存在未完结的还款记录！")]
        CheckRepayPlanByContractNumber = 4006,

        /// <summary>
        /// 置换总额和债务余额不相等！
        /// </summary>
        [Description("置换总额和债务余额不相等！")]
        MoneyNoEqual = 4007,

        /// <summary>
        /// 当前债务尚有待审核或已驳回的还款，审核通过后才能继续下次还款。
        /// </summary>
        [Description("当前债务尚有待审核或已驳回的还款，审核通过后才能继续下次还款。")]
        RepaymentDataCheck = 4008,

        /// <summary>
        /// 存在相同债务合同编号,请重新填写！
        /// </summary>
        [Description("存在相同债务合同编号,请重新填写！")]
        RecordCheckInfoHavingSameContractNumber = 4009,

        /// <summary>
        /// 存在相同债务编号，请重新填写！
        /// </summary>
        [Description("存在相同债务编号，请重新填写！")]
        RecordCheckInfoHavingSameDebtCodeOrName = 4010,

        /// <summary>
        /// 融资总额不能小于债务余额，请重新输入!
        /// </summary>
        [Description("融资总额不能小于债务余额，请重新输入!")]
        RecordCheckInfoCheckDebtBalanceAndFinanceMoney = 4011,

        /// <summary>
        /// 计划还款总额不等于债务余额！
        /// </summary>
        [Description("计划还款总额不等于债务余额！")]
        RepayPlanMoneyCheck = 4012,

        /// <summary>
        /// 还款计划剩余金额不等于债务余额！
        /// </summary>
        [Description("还款计划剩余金额不等于债务余额！")]
        RecordCheckInfoDebtBalanceNoEqualRepayMoney = 4013,

        /// <summary>
        /// 初次提款时间不能在最早一笔提款记录之后
        /// </summary>
        [Description("初次提款时间不能在最早一笔提款记录之后！")]
        FirstWithdrawalTimeCannotAfterTheEarliest = 4014,

        /// <summary>
        /// 初次提款时间不能在其余提款记录之后
        /// </summary>
        [Description("初次提款时间不能在其余提款记录之后！")]
        FirstWithdrawalTimeCannotAfterOther = 4014,

        /// <summary>
        /// 存在未还完的置换金额，请先处理。
        /// </summary>
        [Description("存在未还完的置换金额，请先处理。")]
        RecordCheckInfoHavingNotReplacement = 4015,

        /// <summary>
        /// 存在待审核的还款记录，请先处理。
        /// </summary>
        [Description("存在待审核的还款记录，请先处理。")]
        RecordCheckInfoHavingPendingReview = 4016,

        /// <summary>
        /// 债务状态不是待审核状态，请刷新后重试！
        /// </summary>
        [Description("债务状态不是待审核状态，请刷新后重试！")]
        IsNotNeedAudit = 4017,

        /// <summary>
        /// 存在待审核或者审核不通过的还款记录，请先处理。
        /// </summary>
        [Description("存在待审核或者审核不通过的还款记录，请先处理。")]
        HavingNoDealPlan = 4018,

        /// <summary>
        /// 隐性债务置换验证
        /// {0}债务编号
        /// {1}已关联置换金额
        /// {2}置换总金额
        /// </summary>
        [Description("债务编号【{0}】已关联置换金额【{1}】万元，与置换总金额【{2}】万元不等，请重新分配!")]
        ReplacementCheckMessage = 4019,

        /// <summary>
        /// 当前债务不存在待还款的还款计划,请确认！
        /// </summary>
        [Description("当前债务不存在待还款的还款计划,请确认！")]
        HasNoRepaymentPlan = 4020,

        /// <summary>
        /// 当前还款总金额【{0}】万元，其中置换总金额为【{1}】万元，还款计划中关联的置换金额为【{2}】万元，置换金额不平，请重新输入资金来源组成
        /// </summary>
        [Description("当前还款总金额【{0}】万元，其中置换总金额为【{1}】万元，还款计划中关联的置换金额为【{2}】万元，置换金额不平，请重新输入资金来源组成。")]
        DataUpdateMoneyCheck = 4021,

        /// <summary>
        /// 当前还款总金额【{0}】万元，其中非置换总金额为【{1}】万元，还款计划中非关联置换的总金额【{2}】万元，请重新输入资金来源组成
        /// </summary>
        [Description("当前还款总金额【{0}】万元，其中非置换总金额为【{1}】万元，还款计划中非关联置换的总金额【{2}】万元，请重新输入资金来源组成。")]
        RepaymentPlanMoneyCheck = 4022,

        /// <summary>
        /// 您输入的生效日期已存在，请核实！
        /// </summary>
        [Description("您输入的生效日期已存在，请核实！")]
        HasSameEffectiveDate = 4023,

        /// <summary>
        /// 所选债务为新增债务且审核未通过，请重新输入合同编号！
        /// </summary>
        [Description("所选债务为新增债务且审核未通过，请重新输入合同编号！")]
        NotNewPassedDebt = 4024,

        /// <summary>
        /// 所选债务不是存量债务，请重新输入合同编号！
        /// </summary>
        [Description("所选债务不是存量债务，请重新输入合同编号！")]
        NotStockDebt = 4025,

        /// <summary>
        /// 债务已还清，不需要还款！
        /// </summary>
        [Description("债务已还清，不需要还款！")]
        DebtPayOff = 4026,

        /// <summary>
        /// 存在未置换的债务，请先确认！
        /// </summary>
        [Description("存在未置换的债务，请先确认！")]
        RecordCheckInfoHasDebtReplacement = 4027,

        /// <summary>
        /// 存在未处理的审核不通过的还款记录，请先处理。
        /// </summary>
        [Description("存在未处理的审核不通过的还款记录，请先处理。")]
        RecordCheckInfoHavingNotProcessed = 4028,

        /// <summary>
        /// 当前债务存在未处理完结的提款记录，请确认提款后再次提款！
        /// </summary>
        [Description("当前债务存在未处理完结的提款记录，请确认提款后再次提款！")]
        RecordCheckInfoHasUnCompletedWithdrawRecord = 4029,

        /// <summary>
        /// 该笔债务有未分配的置换债务金额，请先关联对应的隐性债务！
        /// </summary>
        [Description("该笔债务有未分配的置换债务金额，请先关联对应的隐性债务！")]
        WithdrawRecordMessageHasUnAllocationMoney = 4030,

        /// <summary>
        /// 当前债务总额与置换金额不平，请重新分配金额！
        /// </summary>
        [Description("当前债务总额与置换金额不平，请重新分配金额！")]
        RecordCheckInfoMoneyIsNotFair = 4031,

        /// <summary>
        /// 还款记录不是待审核状态，请刷新后重试！
        /// </summary>
        [Description("还款记录不是待审核状态，请刷新后重试！")]
        RepaymentRecordNeedNotAudit = 4032,

        /// <summary>
        /// 当前还款记录不存在，请确认！
        /// </summary>
        [Description("当前还款记录不存在，请确认！")]
        RecordCheckInfoHasNoRepaymentRecord = 4033,

        /// <summary>
        /// 当前还款记录对应的债务不存在，请确认！
        /// </summary>
        [Description("当前还款记录对应的债务不存在，请确认！")]
        RecordCheckInfoHasNoDebt = 4034,

        /// <summary>
        /// 债务已操作展期续借,请先取消展期续借后再回滚款记录！
        /// </summary>
        [Description("债务已操作展期续借,请先取消展期续借后再回滚款记录！")]
        WithdrawRecordMessageHasDoExtended = 4035,

        /// <summary>
        /// 债务已操作计划调整,请确认！
        /// </summary>
        [Description("债务已操作计划调整,请确认！")]
        RecordCheckInfoHasAdjustPlan = 4036,

        /// <summary>
        /// 当前提款记录为展期续借前的提款记录，无法删除！
        /// </summary>
        [Description("当前提款记录为展期续借前的提款记录，无法删除！")]
        WithdrawRecordMessageBeforeExtended = 4037,

        /// <summary>
        /// 当前还款记录不是最新还款记录，无法回滚！
        /// </summary>
        [Description("当前还款记录不是最新还款记录，无法回滚！")]
        IsNotNewRecord = 4038,

        /// <summary>
        /// {0}已还款{1}剩余{2}待还
        /// </summary>
        [Description("{0}已还款{1}剩余{2}待还")]
        RepaymentPlanMoneyCheckMessage = 4039,

        /// <summary>
        /// 当前债务存在未审核的还款记录，请先审核还款记录后再取消置换关联关系！
        /// </summary>
        [Description("当前债务存在未审核的还款记录，请先审核还款记录后再取消置换关联关系！")]
        HasNeedAuditRecord = 4040,

        /// <summary>
        /// 当前债务未确认关联任何债务，无需取消置换关联关系！
        /// </summary>
        [Description("当前债务未确认关联任何债务，无需取消置换关联关系！")]
        NeedNotCancel = 4041,

        /// <summary>
        /// 需取消关联金额过大，请重新选择！
        /// </summary>
        [Description("需取消关联金额过大，请重新选择！")]
        NeedAdjustCancelMoney = 4042,

        /// <summary>
        /// 当前债务审核未通过，无法提款！
        /// </summary>
        [Description("当前债务审核未通过，无法提款！")]
        IsNotPassAudit = 4043,

        /// <summary>
        /// 当前债务为应付工程款，无法提款！
        /// </summary>
        [Description("当前债务为应付工程款，无法提款！")]
        IsEngineeringDebt = 4044,

        /// <summary>
        /// 当前债务已全部提款，无法再次提款！
        /// </summary>
        [Description("当前债务已全部提款，无法再次提款！")]
        HasWithdrawAll = 4045,

        /// <summary>
        /// 当前债务存在未处理完结的还款记录，请先确认！
        /// </summary>
        [Description("当前债务存在未处理完结的还款记录，请先确认！")]
        WithdrawRecordMessageHasUnCompletedRecord = 4046,

        /// <summary>
        /// 当前债务存在未处理完结的提款记录，请确认！
        /// </summary>
        [Description("当前债务存在未处理完结的提款记录，请确认！")]
        WithdrawRecordMessageHasUnCompletedWithdrawRecord = 4047,

        /// <summary>
        /// 新增提款的还款计划时间不能早于提款时间或已还款的还款时间！
        /// </summary>
        [Description("新增提款的还款计划时间不能早于提款时间或已还款的还款时间！")]
        BeforeRepaidDate = 4048,

        /// <summary>
        /// 债务【{0}】不存在，请确认后重新关联！
        /// </summary>
        [Description("债务【{0}】不存在，请确认后重新关联！")]
        DebtIsNotExist = 4049,

        /// <summary>
        /// 债务【{0}】与债务【{1}】已存在置换关联关系，且关联状态为未关联状态，请先确认关联关系后重新再次关联！
        /// </summary>
        [Description("债务【{0}】与债务【{1}】已存在置换关联关系，且关联状态为未关联状态，请先确认关联关系后重新再次关联！")]
        WithdrawRecordMessageHasRelation = 4050,

        /// <summary>
        /// 债务【{0}】最大可置换金额为{1}，请减少置换金额！
        /// </summary>
        [Description("债务【{0}】最大可置换金额为{1}，请减少置换金额！")]
        WithdrawRecordMessageBigThanMaxMoney = 4051,

        /// <summary>
        /// 提款记录不是待审核状态，请刷新后重试！
        /// </summary>
        [Description("提款记录不是待审核状态，请刷新后重试！")]
        RecordNeedNotAudit = 4052,

        /// <summary>
        /// 当前提款记录对应的债务不存在，请确认！
        /// </summary>
        [Description("当前提款记录对应的债务不存在，请确认！")]
        WithdrawRecordMessageHasNoWithdrawRecord = 4053,

        /// <summary>
        /// 当前提款记录已进行还款，请先删除还款记录！
        /// </summary>
        [Description("当前提款记录已进行还款，请先删除还款记录！")]
        WithdrawRecordMessageHasRepaid = 4054,

        /// <summary>
        /// 当前提款记录已进行置换，请取消置换关联关系！
        /// </summary>
        [Description("当前提款记录已进行置换，请取消置换关联关系！")]
        WithdrawRecordMessageHasReplacePlan = 4055,

        /// <summary>
        /// 当前展期续借债务存在报警或预警，请确认！
        /// </summary>
        [Description("当前展期续借债务存在报警或预警，请确认！")]
        WithdrawRecordMessageHasAlarmOrWarn = 4056,

        /// <summary>
        /// 当前债务为应付工程款，不能进行展期续借！
        /// </summary>
        [Description("当前债务为应付工程款，不能进行展期续借！")]
        RecordCheckInfoIsEngineeringDebt = 4057,

        /// <summary>
        /// 债务余额加上已置换金额不等于置换总额，不能进行展期续借！
        /// </summary>
        [Description("债务余额加上已置换金额不等于置换总额，不能进行展期续借！")]
        RecordCheckInfoReplacementMoneyCheck = 4058,

        /// <summary>
        /// 存在不是审核已通过的还款计划。
        /// </summary>
        [Description("存在不是审核已通过的还款计划。")]
        CheckPlanRecordData = 4059,

        /// <summary>
        /// 已存在相同债权人名称
        /// </summary>
        [Description("已存在相同债权人名称。")]
        HavingName = 4060,

        /// <summary>
        /// 还款时间不能小于初次提款时间
        /// </summary>
        [Description("还款时间不能小于初次提款时间！")]
        RepaidDateNotThanFirstWithdrawalTime = 4061,

        /// <summary>
        /// 还款时间不能大于当前时间！
        /// </summary>
        [Description("还款时间不能大于当前时间！")]
        RepaymentRecordRepaidDateNotThanNow = 4062,

        /// <summary>
        /// 还息时间不能大于当前时间
        /// </summary>
        [Description("还息时间不能大于当前时间！")]
        InterestRecordRepaidDateNotThanNow = 4063,

        /// <summary>
        /// 此账号用户未绑定手机号码，请联系账号对应管理员或联系客服
        /// </summary>
        [Description("此账号用户未绑定手机号码，请联系账号对应管理员或联系客服！")]
        UserPhoneEmpty = 4064,

        /// <summary>
        /// 验证码错误，请重试！
        /// </summary>
        [Description("验证码错误，请重试！")]
        VerificationCodeError = 4065,

        /// <summary>
        /// 当前债务不是隐性债务，不可转换债务性质。
        /// </summary>
        [Description("当前债务不是隐性债务，不可转换债务性质。")]
        RecordCheckInfoIsNotImplicitDebt = 4066,

        /// <summary>
        /// 当前债务存在未分配的置换金额，不可转换债务性质。
        /// </summary>
        [Description("当前债务存在未分配的置换金额，不可转换债务性质。")]
        RecordCheckInfoHasAllotDebt = 4067,

        /// <summary>
        /// 当前债务存在未还款的置换计划，不可转换债务性质。
        /// </summary>
        [Description("当前债务存在未还款的置换计划，不可转换债务性质。")]
        RecordCheckInfoHasRelationPlan = 4068,

        /// <summary>
        /// 当前债务审核未通过，无法回滚展期续借！
        /// </summary>
        [Description("当前债务审核未通过，无法回滚展期续借！")]
        DebtExtensionIsNotPassAudit = 4069,

        /// <summary>
        /// 当前债务审核未通过，请确认！
        /// </summary>
        [Description("当前债务审核未通过，请确认！")]
        CancelDebtAuditMessageIsNotPassAudit = 4070,

        /// <summary>
        /// 当前债务不存在或已被删除，请确认！
        /// </summary>
        [Description("当前债务不存在或已被删除，请确认！")]
        CancelDebtAuditMessageHasNoDebt = 4071,

        /// <summary>
        /// 当前债务未做过展期续借，无法回滚展期续借！
        /// </summary>
        [Description("当前债务未做过展期续借，无法回滚展期续借！")]
        DebtExtensionIsNotExtendAudit = 4072,

        /// <summary>
        /// 当前债务存在未处理完结的还款记录，请先确认！
        /// </summary>
        [Description("当前债务存在未处理完结的还款记录，请先确认！")]
        DebtExtensionHasUnCompletedRecord = 4073,

        /// <summary>
        /// 当前展期续借债务已存在还款记录，请先回滚还款记录！
        /// </summary>
        [Description("当前展期续借债务已存在还款记录，请先回滚还款记录！")]
        DebtExtensionHasRepaidMoney = 4074,

        /// <summary>
        /// 当前债务存在未处理完结的提款记录，请确认！
        /// </summary>
        [Description("当前债务存在未处理完结的提款记录，请确认！")]
        DebtExtensionHasUnCompletedWithdrawRecord = 4075,

        /// <summary>
        /// 当前展期续借债务存在提款记录，请先回滚提款记录！
        /// </summary>
        [Description("当前展期续借债务存在提款记录，请先回滚提款记录！")]
        DebtExtensionHasWithdrawRecord = 4076,

        /// <summary>
        /// 当前债务存在置换关联关系，请先在置换债务中取消置换关联关系！
        /// </summary>
        [Description("当前债务存在置换关联关系，请先在置换债务中取消置换关联关系！")]
        DebtExtensionHasRelatedMasterDebt = 4077,

        /// <summary>
        /// 当前债务存在置换来源且已分配计划，请先取消分配计划！
        /// </summary>
        [Description("当前债务存在置换来源且已分配计划，请先取消分配计划！")]
        DebtExtensionHasRelatedSlaveDebtPlan = 4078,

        /// <summary>
        /// 当前债务存在置换来源，请先变更来源债务的置换关系！
        /// </summary>
        [Description("当前债务存在置换来源，请先变更来源债务的置换关系！")]
        DebtExtensionHasRelatedSlaveDebt = 4079,

        /// <summary>
        /// 当前展期续借债务存在报警或预警，请确认！
        /// </summary>
        [Description("当前展期续借债务存在报警或预警，请确认！")]
        DebtExtensionHasAlarmOrWarn = 4080,

        /// <summary>
        /// 债务已操作展期续借,请先取消展期续借后再回滚款记录！
        /// </summary>
        [Description("债务已操作展期续借,请先取消展期续借后再回滚款记录！")]
        RecordCheckInfoHasDoExtended = 4081,

        /// <summary>
        /// 当前提款记录为展期续借前的提款记录，无法回滚！
        /// </summary>
        [Description("当前提款记录为展期续借前的提款记录，无法回滚！")]
        RecordCheckInfoBeforeExtended = 4082,

        /// <summary>
        /// 该笔债务有未分配的置换债务金额，请先关联对应的隐性债务！
        /// </summary>
        [Description("该笔债务有未分配的置换债务金额，请先关联对应的隐性债务！")]
        RecordCheckInfoHasUnAllocationMoney = 4083,

        /// <summary>
        /// 当前债务总额与置换金额不平，请重新分配金额！
        /// </summary>
        [Description("当前债务总额与置换金额不平，请重新分配金额！")]
        WithdrawRecordMessageMoneyIsNotFair = 4084,

        /// <summary>
        /// 债务已操作展期续借,请先取消展期续借！
        /// </summary>
        [Description("债务已操作展期续借,请先取消展期续借！")]
        CancelDebtAuditMessageHasDoExtended = 4085,

        /// <summary>
        /// 该笔债务存在还款记录，无法取消审核，请先删除还款记录！
        /// </summary>
        [Description("该笔债务存在还款记录，无法取消审核，请先删除还款记录！")]
        CancelDebtAuditMessageHasRepaidRecord = 4086,

        /// <summary>
        /// 该笔债务已操作过还款计划的调整，请确认！
        /// </summary>
        [Description("该笔债务已操作过还款计划的调整，请确认！")]
        CancelDebtAuditMessageHasAdjustedPlan = 4087,

        /// <summary>
        /// 该笔债务已经提款，无法取消审核，请先删除提款记录！
        /// </summary>
        [Description("该笔债务已经提款，无法取消审核，请先删除提款记录！")]
        CancelDebtAuditMessageHasWithdrawRecord = 4088,

        /// <summary>
        /// 当前债务存在置换关联关系，请先在置换债务中取消置换关联关系！
        /// </summary>
        [Description("当前债务存在置换关联关系，请先在置换债务中取消置换关联关系！")]
        CancelDebtAuditMessageHasRelatedMasterDebt = 4089,

        /// <summary>
        /// 当前债务存在置换来源且已分配计划，请先取消分配计划！
        /// </summary>
        [Description("当前债务存在置换来源且已分配计划，请先取消分配计划！")]
        CancelDebtAuditMessageHasRelatedSlaveDebtPlan = 4090,

        /// <summary>
        /// 当前债务存在置换来源，请先变更来源债务的置换关系！
        /// </summary>
        [Description("当前债务存在置换来源，请先变更来源债务的置换关系！")]
        CancelDebtAuditMessageHasRelatedSlaveDebt = 4091,

        /// <summary>
        /// 当前债务存在报警或预警，请确认！
        /// </summary>
        [Description("当前债务存在报警或预警，请确认！")]
        CancelDebtAuditMessageHasAlarmOrWarn = 4092,

        /// <summary>
        /// 当前债务已被{0}债务用作存量债务资金偿还，请确认！
        /// </summary>
        [Description("当前债务已被{0}债务用作存量债务资金偿还，请确认！")]
        CancelDebtAuditMessageHasRepaidDebt = 4093,

        /// <summary>
        /// 当前债务不是应付工程款，请确认！
        /// </summary>
        [Description("当前债务不是应付工程款，请确认！")]
        AdjustPlanMessageIsNotEngineerDebt = 4094,

        /// <summary>
        /// 当前债务审核未通过，请确认！
        /// </summary>
        [Description("当前债务审核未通过，请确认！")]
        AdjustPlanMessageIsNotPassAudit = 4095,

        /// <summary>
        /// 还款计划剩余金额不等于债务余额
        /// </summary>
        [Description("还款计划剩余金额不等于债务余额！")]
        AdjustPlanMessageDebtBalanceNoEqualRepayMoney = 4096,

        /// <summary>
        /// 存在未还完的置换金额，请先处理。
        /// </summary>
        [Description("存在未还完的置换金额，请先处理。")]
        AdjustPlanMessageHavingNotReplacement = 4097,

        /// <summary>
        /// 当前债务存在未处理完结的还款记录，请先确认！
        /// </summary>
        [Description("当前债务存在未处理完结的还款记录，请先确认！")]
        AdjustPlanMessageHasUnCompletedRecord = 4098,

        /// <summary>
        /// 级别阀值已存在
        /// </summary>
        [Description("级别阀值已存在！")]
        DebtRateCheckDebtRateCheckError = 4099,

        /// <summary>
        /// 该线索已被占用，不能停用
        /// </summary>
        [Description("该线索已被占用，不能停用！")]
        AccountabilityMessageNotStopClue = 4100,

        /// <summary>
        /// 已存在相同等级线索，请重新选择
        /// </summary>
        [Description("已存在相同等级线索，请重新选择！")]
        AccountabilityMessageHasClue = 4101,

        /// <summary>
        /// 已存在相同问责事由，请重新选择
        /// </summary>
        [Description("已存在相同问责事由，请重新选择！")]
        AccountabilityMessageHasReason = 4102,

        /// <summary>
        /// 权限不足，无法更改管理员账号信息
        /// </summary>
        [Description("权限不足，无法更改管理员账号信息！")]
        EmpOperationInsufficientAuthority = 4103,

        /// <summary>
        /// 权限不足，无法获取管理员账号信息
        /// </summary>
        [Description("权限不足，无法获取管理员账号信息！")]
        EmpOperationObtainInsufficientAuthority = 4104,

        /// <summary>
        /// 债务开始日期必须小于还款计划中最小计划还款日期！
        /// </summary>
        [Description("债务开始日期必须小于还款计划中最小计划还款日期！")]
        DebtStratDateMustlessThanPlanDate = 4105,

        /// <summary>
        /// 债务结束日期必须大于还款计划中最大计划还款日期！
        /// </summary>
        [Description("债务结束日期必须大于还款计划中最大计划还款日期！")]
        DebtEndDateMustGreaterThanPlanDate = 4106,

        /// <summary>
        /// 债务开始日期必须小于提款记录中最小提款日期！
        /// </summary>
        [Description("债务开始日期必须小于提款记录中最小提款日期！")]
        DebtStratDateMustlessThanWithdrawDate = 4107,

        /// <summary>
        /// 债务结束日期必须大于提款记录中最大提款日期！
        /// </summary>
        [Description("债务结束日期必须大于提款记录中最大提款日期！")]
        DebtStratDateMustGreaterThanWithdrawDate = 4108,

        /// <summary>
        /// 已有用户关联该平台，请先取消关联。
        /// </summary>
        [Description("已有用户关联该平台，请先取消关联。")]
        PlatFromHasContactCompany = 4109,

        /// <summary>
        /// 新地区Id和原地区Id不能相同。
        /// </summary>
        [Description("新地区Id和原地区Id不能相同。")]
        NewAreaIdAndOldAreaIdConNotTheSame = 4110,

        /// <summary>
        /// 上传文档格式不匹配。
        /// </summary>
        [Description("上传文档格式不匹配。")]
        TitleNotMatch = 4111,

        /// <summary>
        /// 置换金额已用还款，请先删除相关还款记录再进行关联取消！
        /// </summary>
        [Description("置换金额已用还款，请先删除相关还款记录再进行关联取消！")]
        NeedRollBackRecord = 4112,

        /// <summary>
        /// 已存在当前年份资产信息！
        /// </summary>
        [Description("已存在当前年份资产信息！")]
        HavingCompanyAsset = 4113,
        #endregion
    }

    /// <summary>
    /// 状态吗扩展
    /// </summary>
    public static class StatusCodeExtension
    {
        /// <summary>
        /// 获取StatusCode对应的消息
        /// </summary>
        /// <param name="statusCode">状态码</param>
        /// <param name="formatValues">消息格式化参数</param>
        /// <returns>返回给使用者的消息</returns>
        public static string Message(this StatusCode statusCode, params object[] formatValues)
        {
            var type = typeof(StatusCode);

            var memInfo = type.GetMember(type.GetEnumName(statusCode));

            var descriptionAttribute = memInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

            var message = descriptionAttribute.Description;

            if (!formatValues.IsNullOrEmpty())
            {
                return string.Format(message, formatValues);
            }
            return message;
        }
    }
}
