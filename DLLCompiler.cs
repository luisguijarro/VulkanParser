using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace VulkanParser
{
	/// <summary>
	/// Description of DLLCompiler.
	/// </summary>
	public static class DLLCompiler
	{
		public static string CompileDLL(string[] SourceCodeFiles, string OutPutName)
		{
			string ret = "";
			//System.CodeDom.Compiler.ICodeCompiler cc = new ;
			//System.CodeDom.Compiler.CodeDomProvider cp;
			CSharpCodeProvider CCprovider = new CSharpCodeProvider();
			CompilerParameters CParams = new CompilerParameters();
			
			//CParams.ReferencedAssemblies.AddRange(SourceCodeFiles);
			CParams.GenerateInMemory = false;
			CParams.GenerateExecutable = false;
			CParams.OutputAssembly = OutPutName+".dll";
			CParams.CompilerOptions += "/unsafe";
			//System.CodeDom.Compiler.CodeGenerator CG;
			//CG.GenerateCodeFromMember
			if (System.Environment.OSVersion.Platform == System.PlatformID.Win32NT)
			{
				for (int i=0;i<SourceCodeFiles.Length;i++)
				{
					SourceCodeFiles[i] = SourceCodeFiles[i].Replace('/','\\');
				}
			}
			CompilerResults resultado = CCprovider.CompileAssemblyFromFile(CParams, SourceCodeFiles);
			if ((resultado.Errors.HasErrors) || (resultado.Errors.HasWarnings))
			{
				int i_errores = 0;
				int i_warnings = 0;
				foreach (CompilerError error in resultado.Errors)
				{
					ret += error.ToString()+Environment.NewLine;
					if (error.IsWarning)
					{
						i_warnings++;
					}
					else
					{
						i_errores++;
					}
				}
				ret +=Environment.NewLine;
				ret += "Errores: "+ i_errores + Environment.NewLine;
				ret += "Warnings: "+ i_warnings + Environment.NewLine;
			}
			else
			{
				ret = "Compile SUCESSFULL!!!!";
			}
			
			return ret;
		}
	}
}
