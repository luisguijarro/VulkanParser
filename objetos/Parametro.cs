using System;

namespace VulkanParser.objetos
{
	/// <summary>
	/// Description of Parametro.
	/// </summary>
	public class Parametro
	{
		public string Nombre;
		public bool Opcional;
		public bool puntero;
		public string TipoValor;
		public bool constante;
        public bool esarray;
		public Parametro(string nombre)
		{
			this.Nombre = nombre;
		}
	}
}
