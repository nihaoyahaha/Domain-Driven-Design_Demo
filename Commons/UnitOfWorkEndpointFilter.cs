
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Commons;

public class UnitOfWorkEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var UnitOfWorkAttribute = context.HttpContext.GetEndpoint()?.Metadata.GetMetadata<UnitOfWorkAttribute>();
        if( UnitOfWorkAttribute is null)
        {
            return await next(context);
        }
        using TransactionScope txScope = new(TransactionScopeAsyncFlowOption.Enabled);
        List<DbContext> dbCtxs = new List<DbContext>();
        foreach (var dbCtxType in UnitOfWorkAttribute.DbContextTypes)
        {
            //用HttpContext的RequestServices
            //确保获取的是和请求相关的Scope实例
            var sp = context.HttpContext.RequestServices;
            DbContext dbCtx = (DbContext)sp.GetRequiredService(dbCtxType);
            dbCtxs.Add(dbCtx);
        }
        var result = await next(context);
        var statusCode = context.HttpContext.Response.StatusCode ;
        if (statusCode >=200 && statusCode < 300)
        {
            foreach (var dbCtx in dbCtxs)
            {
                await dbCtx.SaveChangesAsync();
            }
            txScope.Complete();
        }
        return result;
    }

}
