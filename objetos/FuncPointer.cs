using System;
using System.Collections.Generic;

namespace VulkanParser.objetos
{
	/// <summary>
	/// Description of FuncPointer.
	/// </summary>
	public class FuncPointer
	{
		public string Nombre;
		public string ValueReturned;
		public Dictionary<string, Parametro> Parametros;
		public FuncPointer()
		{
			
		}
	}
}
