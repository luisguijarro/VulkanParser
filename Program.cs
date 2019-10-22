/*
 * Creado por SharpDevelop.
 * Usuario: luisgp82
 * Fecha: 04/07/2018
 * Hora: 10:15
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Windows.Forms;

namespace VulkanParser
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
            if (args.Length <= 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                for (int i=0;i<args.Length;i++)
                {
                    switch(args[i])
                    {
                        case "-n":
                            string nspace = args[i + 1];
                            i++;
                            Console.WriteLine("Namespace: " + nspace);
                            break;
                        case "-c":
                            string dllname = args[i + 1];
                            i++;
                            Console.WriteLine("Dll name: " + dllname);
                            break;
                        case "-b":
                            //string dllname = args[i + 1];
                            break;
                        case "-h":
                            Console.WriteLine("Vulkan Parser for C# developed by The BlackSword Brotherhood.");
                            Console.WriteLine("Help info.");
                            Console.WriteLine();
                            Console.WriteLine("-n <namespace>  : Definir espacio de nombres del proyecto.");
                            Console.WriteLine("-c <nombre.dll> : Compilar .cs en librería con nombre determinado.");
                            Console.WriteLine("-b              : Generar clases extra (VKDevice) y sus dependencias.");
                            Console.WriteLine("-h              : Muestra esta ayuda.");
                            break;
                    }
                }
            }
        }
		
	}
}
