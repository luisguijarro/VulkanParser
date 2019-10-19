using System;
using System.Collections.Generic;

namespace VulkanParser.objetos
{
	/// <summary>
	/// Description of Metodo.
	/// </summary>
	public class Metodo
	{
		public string Nombre;
		public string ValueReturned;
		public Dictionary<string, Parametro> Parametros;
		public bool Alias;
		public string AliasName;
		public Metodo(string nombre)
		{
			this.Nombre = nombre;
			this.Parametros = new Dictionary<string, Parametro>();
		}
		public Metodo(string nombre, string aliasName)
		{
			this.Nombre = nombre;
			this.Alias = true;
			this.AliasName = aliasName;
			this.Parametros = new Dictionary<string, Parametro>();
		}
	}
}
