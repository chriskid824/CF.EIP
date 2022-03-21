using BPMAPI;
using Convience.JwtAuthentication;
using Convience.ManagentApi.Infrastructure.Logs.LoginLog;
using Convience.Model.Constants;
using Convience.Model.Models.Account;
using Convience.Model.Models.SystemManage;
using Convience.Service.Account;
using Convience.Service.SystemManage;
using Convience.Util.Extension;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Convience.ManagentApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public class returnData
        {
            public object data { get; set; }
            public string cookies { get; set; }
            public string logonid { get; set; }
            public string password { get; set; }
        }
        public class UserData
        {
            public string USERNAME { get; set; }
            public string CUST_4 { get; set; }
            public string EMAIL { get; set; }
        }
        private readonly IAccountService _loginService;

        private readonly IMenuService _menuService;

        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public AccountController(IAccountService loginService,
            IMenuService menuService,
            IRoleService roleService,
            IUserService userService)
        {
            _loginService = loginService;
            _menuService = menuService;
            _roleService = roleService;
            _userService = userService;
        }

        [HttpGet("captcha")]
        public IActionResult GetCaptcha()
        {
            var result = _loginService.GetCaptcha();
            return Ok(result);
        }

        [HttpPost("login")]
        [LoginLogFilter]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // 验证验证码
            var validResult = _loginService.ValidateCaptcha(model.CaptchaKey, model.CaptchaValue);
            if (!string.IsNullOrEmpty(validResult))
            {
                return this.BadRequestResult(validResult);
            }

            BasicHttpBinding binding = new BasicHttpBinding();

            EndpointAddress address = new EndpointAddress("http://service3.chenfull.com.tw/CF.BPM.Service/BPMAPI.asmx");

            BPMAPISoapClient client = new BPMAPISoapClient(binding, address);

            CallMethodParams callMethodParm = new CallMethodParams();//GetBorgEmpDataByLogonID
            JObject param = new JObject();
            param.Add("logonid", model.UserName);
            param.Add("password", model.Password == null ? "" : model.Password);
            callMethodParm.Method = "LoginValidate";
            callMethodParm.Options = JsonConvert.SerializeObject(param);
            Task<CallMethodResponse> response = client.CallMethodAsync(callMethodParm);
            CallMethodResult result = response.Result.Body.CallMethodResult;
            if (!result.Success)
            {
                //return this.BadRequestResult(result.Message);
                //throw new Exception(result.Message);
                return this.BadRequestResult(AccountConstants.ACCOUNT_WRONG_INPUT);
            }
            returnData returnData = JsonConvert.DeserializeObject<returnData>(result.Options.ToString());
            UserData UserData = JsonConvert.DeserializeObject<UserData>(returnData.data.ToString());
            //如果BPM通過本地不存在就新增
            bool isExsit = _loginService.IsExist(model.UserName);
            if (!isExsit)
            {
                UserViewModel newuser = new UserViewModel()
                {
                    Name = UserData.USERNAME,
                    UserName = model.UserName,
                    Password = model.Password,
                    RoleIds = "1",
                    PositionIds = "",
                    IsActive = true
                };
                //if (model.UserName == "137680"|| model.UserName == "139560" || model.UserName == "139330" || model.UserName == "137680")
                //{
                //    newuser.RoleIds = "1";
                //}
                await _userService.AddUserAsync(newuser);
            }

            // 验证用户是否可以使用
            //var isActive = _loginService.IsStopUsing(model.UserName);
            //if (!isActive)
            //{
            //    return this.BadRequestResult(AccountConstants.ACCOUNT_NOT_ACTIVE);
            //}

            // 取得用户信息
            var validateResult = await _loginService.ValidateCredentialsAsync(model.UserName, model.Password);
            if (validateResult is null)
            {
                return this.BadRequestResult(AccountConstants.ACCOUNT_WRONG_INPUT);
            }

            // 取得权限信息
            var menuIds = _roleService.GetRoleClaimValue(validateResult.RoleIds.Split(',',
                StringSplitOptions.RemoveEmptyEntries), CustomClaimTypes.RoleMenus);

            // 获取菜单权限对应的前端标识
            var irs = _menuService.GetIdentificationRoutes(menuIds.ToArray());

            return Ok(new LoginResultModel(
                validateResult.Name,
                validateResult.Avatar,
                validateResult.Token,
                irs.Item1,
                irs.Item2,
                validateResult.CostNo,
                validateResult.Werks));
        }

        [HttpPost("password")]
        [Authorize]
        public async Task<IActionResult> ChangePwdByOldPwd(ChangePwdViewModel viewmodel)
        {
            var result = await _loginService.ChangePasswordAsync(User.GetUserName(), viewmodel.OldPassword, viewmodel.NewPassword);
            if (!result)
            {
                return this.BadRequestResult(AccountConstants.ACCOUNT_MODIFY_PASSWORD_FAIL);
            }
            return Ok();
        }
    }
}