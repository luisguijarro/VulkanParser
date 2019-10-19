/*
 * Creado por SharpDevelop.
 * Usuario: luisgp82
 * Fecha: 05/07/2018
 * Hora: 9:56
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace VulkanParser
{
	/// <summary>
	/// Description of WorkingEventArgs.
	/// </summary>
	public class WorkingEventArgs : EventArgs
	{
		float f_value;
		string s_accion;
		public WorkingEventArgs(float value, string accion)
		{
			this.f_value = value;
			this.s_accion = accion;
		}
		public float WorkProgress
		{
			get { return this.f_value; }
		}
		public string Accion
		{
			get { return this.s_accion;}
		}
	}
}
