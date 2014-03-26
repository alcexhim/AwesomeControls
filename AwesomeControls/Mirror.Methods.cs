using System;
using System.Reflection;
using System.Collections.Generic;

namespace Mirror
{
	internal static class Methods
	{
		private static Assembly[] mvarAvailableAssemblies = null;
		public static Assembly[] GetAvailableAssemblies()
		{
			if (mvarAvailableAssemblies == null)
			{
				List<Assembly> assemblies = new List<Assembly>();

				System.Reflection.Assembly asm0 = System.Reflection.Assembly.GetEntryAssembly();
				if (asm0 == null) return assemblies.ToArray();
	
				string[] files = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(asm0.Location), "*.dll", System.IO.SearchOption.AllDirectories);
				foreach (string file in files)
				{
					try
					{
						Assembly asm = Assembly.LoadFile(file);
						assemblies.Add(asm);
					}
					catch
					{
					}
				}
				files = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "*.exe", System.IO.SearchOption.AllDirectories);
				foreach (string file in files)
				{
					try
					{
						Assembly asm = Assembly.LoadFile(file);
						assemblies.Add(asm);
					}
					catch
					{
					}
				}
	
				mvarAvailableAssemblies = assemblies.ToArray();
			}
			return mvarAvailableAssemblies;
		}
	}
}

