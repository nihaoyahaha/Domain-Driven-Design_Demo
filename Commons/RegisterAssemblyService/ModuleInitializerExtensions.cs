using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commons;

public static class ModuleInitializerExtensions
{
	public static IServiceCollection RunModuleInitializers(this IServiceCollection services,
		IEnumerable<Assembly> assemblies) 
	{
		foreach (var assembly in assemblies) 
		{
			Type[] types = assembly.GetTypes();
			//获取非抽象类且继承自IModuleInitializer接口的类型
			var moduleInitializerTypes = types
				.Where(t => !t.IsAbstract 
				&& typeof(IModuleInitializer).IsAssignableFrom(t));
			foreach (var implType in moduleInitializerTypes) 
			{
				var initializer = (IModuleInitializer?)Activator.CreateInstance(implType);
				if (initializer == null)
				{
					throw new ApplicationException($"Cannot create ${implType}");
				}
				initializer.Initialize(services);
			}
		}
		return services;
	}
}
