using System;
using System.Collections.Generic;

namespace VulkanParser.objetos
{
	/// <summary>
	/// Description of Estructuras.
	/// </summary>
	public class Estructura
	{
		public string Nombre;
		public Dictionary<string, Valor> valores;
		public Estructura()
		{
			this.valores = new Dictionary<string, Valor>();
		}
	}
	
	public class Valor
	{
		public string nombre;
		public string typo;
		public bool puntero;
		public bool esStruct;
		public string comentario;
		public bool constante;
		public string svalor;
		public bool tieneValor;
		public bool comentado;
        public bool esArray;
        public uint maxAray;
		public Valor()
		{
			
		}
	}
}
