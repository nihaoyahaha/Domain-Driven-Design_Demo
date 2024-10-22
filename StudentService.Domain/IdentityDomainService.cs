using System.Security.Claims;
using Commons;
using Commons.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using StudentService.Domain.Entities;

namespace StudentService.Domain;

public class IdentityDomainService
{
    readonly IIdentityRepository _repository;
    readonly ITokenService _tokenService;
    readonly IOptions<JWTOptions> _jwtOpt;

    public IdentityDomainService(IIdentityRepository repository, ITokenService tokenService, IOptions<JWTOptions> jwtOpt)
    {
        _repository = repository;
        _tokenService = tokenService;
        _jwtOpt = jwtOpt;
    }

    /// <summary>
    /// 检查用户名和密码
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task<SignInResult> CheckUserNameAndPwdAsync(string userName, string password)
    {
        var user = await _repository.FindByNameAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }
        //CheckPasswordSignInAsync会对于多次重复失败进行账号禁用
        var result = await _repository.CheckForSignInAsync(user, password, true);
        return result;
    }

      private async Task<SignInResult> CheckPhoneNumAndPwdAsync(string phoneNum, string password)
        {
            var user = await _repository.FindByPhoneNumberAsync(phoneNum);
            if (user == null)
            {
                return SignInResult.Failed;
            }
            var result = await _repository.CheckForSignInAsync(user, password, true);
            return result;
        }

        //<(SignInResult Result, string? Token)>  元组的语法
        public async Task<(SignInResult Result, string? Token)> LoginByPhoneAndPwdAsync(string phoneNum, string password)
        {
            var checkResult = await CheckPhoneNumAndPwdAsync(phoneNum, password);
            if (checkResult.Succeeded)
            {
                var user = await _repository.FindByPhoneNumberAsync(phoneNum);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }
            else
            {
                return (checkResult, null);
            }
        }

        public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
        {
            var checkResult = await CheckUserNameAndPwdAsync(userName, password);
            if (checkResult.Succeeded)
            {
                var user = await _repository.FindByNameAsync(userName);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }
            else
            {
                return (checkResult, null);
            }
        }

        private async Task<string> BuildTokenAsync(User user)
        {
            var roles = await _repository.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return _tokenService.BuildToken(claims, _jwtOpt.Value);
        }



}
