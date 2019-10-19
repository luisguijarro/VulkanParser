using System;
using System.Xml;
using System.Windows.Forms;

namespace VulkanParser
{
	/// <summary>
	/// Description of VKxmlParser.
	/// </summary>
	public static class VKxmlParser
	{
		private static float f_value;
		private static string s_namespace;
		private static string s_destination;
		private static XmlDocument xdoc;
		public static void Parse(string rutaxml, string @namespace, string destination)
		{
			f_value = 0;
			
			s_namespace = @namespace;
			s_destination = destination;
			
			xdoc = new XmlDocument();
			xdoc.Load(rutaxml);
			
			((MainForm)Application.OpenForms[0]).toolStripStatusLabel2.Text = "Parseando Archivo xml de Vulkan.";
			
			//EL MEOLLO YA AQUÍ
			
			#region Lectura
			Tools.DefineTypes(xdoc);
			HandleParser.Parse(xdoc);
			APIConstantsParser.Parse(xdoc);
			IntPtrsParser.Parse(xdoc);
			EnumParser.Parse(xdoc);
			EnumParser.ExtParse(xdoc);
			ExtensionsParser.ParseExtendedEnumValues(xdoc); //EXTENSIONES
			FuncPointerParser.Parse(xdoc);
			StructParser2.Parse(xdoc);
			UnionParser.Parse(xdoc);
			CommandParser.Parse(xdoc);
			VersionParser.Parse(xdoc);			
			#endregion
			
			#region Escritura
			CreaterConstCS.CreateConstCS();
			//CreaterIntPtrsCS.CreateIntPtrsCS(); //IntPtrs & Handles
			CreaterEnumCS.CreateEnumCS();
			CreaterStructsCS.CreateStructsCS();
			CreaterUnionCS.CreateUnionCS();
			
			//CreaterMethodsCS.CreateMethodsCS();
            //CreaterMethodsCS2.CreateMethodsCS();
            CreaterMethodsCS3.CreateMethodsCS();
            //CreaterMethodsICS.CreateMethodsICS();
            //CreaterMethodsDCS.CreateMethodsDCS();

            if (((MainForm)Application.OpenForms[0]).checkvkdevices.Checked)
            {
                VKDeviceMaker.VKDeviceMake();
                VKDelegatesMaker.VKDelegatesMake();
                VKDelegatorMaker.VKDelegatorMake();
            }


            #endregion

            #region Compile DLL
            if (((MainForm)Application.OpenForms[0]).checkBox1.Checked)
            {
            	string nombrearchivo = ((MainForm)Application.OpenForms[0]).textBox1.Text;
            	string[] files = System.IO.Directory.GetFiles(s_destination, "*.cs");
            	//delegate delmethod = new dele
            	string retornado = DLLCompiler.CompileDLL(files, s_destination+nombrearchivo);
            	            	
            	FormUpdater.UpdateCompilerText(retornado);
            	
            }
            #endregion
            
            //EL MEOLLO YA AQUÍ

            
			if (((MainForm)Application.OpenForms[0]).checkBox2.Checked)
            {
				string[] archivos = System.IO.Directory.GetFiles(s_destination, "*.cs");
				for (int i=0;i<archivos.Length;i++)
				{
					System.IO.File.Delete(archivos[i]);
				}
            }
            //((MainForm)Application.OpenForms[0]).SetValue((int)100); //Casca sin un invoke por ejecutarse desde otro hilo.

            ((MainForm)Application.OpenForms[0]).toolStripStatusLabel2.Text = "Archivo xml de Vulkan Parseado.";
            
			if (System.Windows.Forms.MessageBox.Show("El Archivo XML de Vulkan ha sido Parseado correctamente", "Trabajo Terminado!") == DialogResult.OK)
			{
				
			}
			
			((MainForm)Application.OpenForms[0]).Invoke(finish);
			//((MainForm)Application.OpenForms[0]).Habilitar(true, true);
			System.Threading.Thread.CurrentThread.Abort();
		}
		
		static FinishDel finish = ((MainForm)Application.OpenForms[0]).SetFinish;
		
		public static void Increment(float valor)
		{
			f_value += valor;
			((MainForm)Application.OpenForms[0]).SetValue((int)f_value);
		}
		
		public static string GetNamespace()
		{
			return s_namespace;
		}
		
		public static string GetDestination()
		{
			return s_destination;
		}
	}
}
