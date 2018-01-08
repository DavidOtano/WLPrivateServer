using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace WLPrivateServer.Bootstrapper
{
	public class WLPrivateServerBootstrapper
	{
		public static void Initialize()
		{
			LoadAllReferencedAssemblies();

			InitializeAutoMapper();
		}

		private static void InitializeAutoMapper()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfiles(AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.FullName.Contains("AutoMapper")).ToArray());
			});
		}

		/// <summary>
		/// Code from StackOverflow
		/// https://stackoverflow.com/questions/2384592/is-there-a-way-to-force-all-referenced-assemblies-to-be-loaded-into-the-app-doma
		/// </summary>
		private static void LoadAllReferencedAssemblies()
		{
			var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
			var loadedPaths = loadedAssemblies.Select(x => x.Location).ToArray();

			var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
			var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
			toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
		}
	}
}