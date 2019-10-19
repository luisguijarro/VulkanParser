using System;
using System.Xml;
using System.Collections.Generic;

namespace VulkanParser.objetos
{
	/// <summary>
	/// Description of versiones.
	/// </summary>
	public class version
	{
		public Dictionary<string,List<string>> grupocomandos;
		public string Version;
		public version(string version,  Dictionary<string,List<string>> lgrupocomandos)
		{
			this.grupocomandos = lgrupocomandos;
			this.Version = version;
		}
	}
}
