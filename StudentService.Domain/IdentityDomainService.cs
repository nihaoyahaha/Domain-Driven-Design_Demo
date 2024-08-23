using Commons;
using Commons.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

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



}
