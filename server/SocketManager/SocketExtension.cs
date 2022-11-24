using System;
using System.Diagnostics;
using System.Reflection;
using server.Handlers;

namespace server.SocketManager
{
	public static class SocketExtension
	{
		public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
		{
			services.AddTransient<ConnectionManager>();
			foreach(Type type in Assembly.GetEntryAssembly()?.ExportedTypes)
			{
				Debug.WriteLine(type.Name+ ": " + type.BaseType);
				if (type.GetTypeInfo().BaseType == typeof(SocketMessageHandler))
					services.AddSingleton(type);
			}
			return services;
		}

		public static IApplicationBuilder MapSockets(this IApplicationBuilder app, PathString path, SocketHandler handler)
		{
			return app.Map(path, x => x.UseMiddleware<SocketMiddleware>(handler));
		}
	}
}

