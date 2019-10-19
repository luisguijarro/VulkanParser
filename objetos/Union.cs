using System;
using System.Collections.Generic;

namespace VulkanParser.objetos
{
	/// <summary>
	/// Description of Union.
	/// </summary>
	public class Union
	{
		public string Name;
		public Dictionary<string,unionmembers> members;
		public Union()
		{
			this.members = new Dictionary<string, unionmembers>();
		}
	}
	
	public class unionmembers
	{
		public string name;
		public string Type;
		public string cant;
		public unionmembers()
		{
			this.name = "";
			this.Type = "";
			this.cant = "";
		}
	}
}
