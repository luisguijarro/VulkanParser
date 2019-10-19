using System;
using System.IO;
using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateStructsCS.
	/// </summary>
	public static class CreaterStructsCS
	{
		public static void CreateStructsCS()
		{
			StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"VKStructs.cs");
			sw.WriteLine("// Document Created with VulkanParser.");
			sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
			sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
			sw.WriteLine();
			sw.WriteLine("using System;");
            sw.WriteLine("using System.Runtime.InteropServices;");
            sw.WriteLine();
			sw.WriteLine("namespace "+VKxmlParser.GetNamespace());
			sw.WriteLine("{");
			
			//MEOLLO
			foreach (string key in StructParser2.estructuras.Keys) //ESTRUCTURAS
            {
                sw.WriteLine("\t" + "[StructLayout(LayoutKind.Sequential)]");
				sw.WriteLine("\t"+"public unsafe struct "+key);
				sw.WriteLine("\t"+"{");
				//bool valorado = false;
				foreach (string key2 in StructParser2.estructuras[key].valores.Keys) //VALORES DE LA ESTRUCTURA
				{
					Valor ValorTemp = StructParser2.estructuras[key].valores[key2]; //VALOR
					if (ValorTemp.comentado)//.comentario != "") //¿TIENE COMENTARIO?
					{
						sw.WriteLine("\t"+"\t"+"/// <summary>"+ValorTemp.comentario+"</summary>");
					}
					//Tipo de valor ¿puntero? ¿estructura? ¿valor?
					string linea  = "\t"+"\t"+"public ";
					if (ValorTemp.constante | ValorTemp.tieneValor)
					{
						linea += "const ";
					}

                    string tipovalor = Tools.VariableType(ValorTemp.typo);
                    if (ValorTemp.typo.Contains("PFN_"))
                    {
                        tipovalor = "IntPtr";
                    }
                    if (ValorTemp.typo == "char")
                    {
                        if (ValorTemp.puntero)
                        {
                            tipovalor = "char*";
                        }
                    }

                    if (ValorTemp.esArray) //Arrays con: fixed tipovalor nombrevalor[dimension];
                    {
                        if ((StructParser2.estructuras.ContainsKey(ValorTemp.typo)) || (tipovalor == "IntPtr")) //Si es estructura o IntPtr se define la dimensión antes.
                        {
                            sw.WriteLine("\t" + "\t" + "[MarshalAs(UnmanagedType.ByValArray, SizeConst = " + ValorTemp.maxAray + ")]");
                        }
                        else
                        {
                            linea += "fixed ";
                        }
                    }
                    /*if (ValorTemp.esStruct)
					{
						string tipovalor = ValorTemp.typo;
						linea += tipovalor;
					}
					else
					{						
						string tipovalor = Tools.VariableType(ValorTemp.typo);
						linea += tipovalor;						
					}*/
                    /*if (HandleParser.handleTypes.Contains(ValorTemp.typo))
                    {
                        string jamon = "";
                    }*/

                    linea += tipovalor;
                    if (ValorTemp.esArray) //Si nes Array y estructura o IntPtr se marca como array tras el tipo []
                    {
                        if ((StructParser2.estructuras.ContainsKey(ValorTemp.typo)) || (tipovalor == "IntPtr"))
                        {
                            linea += "*";
                        }
                    }
                    if ((ValorTemp.puntero) && (!ValorTemp.typo.Contains("PFN_")))
                    {
						linea +="*";
					}
					if (ValorTemp.nombre == "object")
					{
						ValorTemp.nombre = "@object";
					}
					linea += " "+ValorTemp.nombre;
					if (ValorTemp.tieneValor)
					{
						//valorado = true;
						linea += " = "+ValorTemp.svalor;
					}

                    if (ValorTemp.esArray) //Arrays con: fixed tipovalor nombrevalor[dimension]
                    {
                        if (!StructParser2.estructuras.ContainsKey(ValorTemp.typo) && (tipovalor != "IntPtr"))
                        {
                            linea += "[" + ValorTemp.maxAray + "]";
                        }
                    }
                    /*
                    if (ValorTemp.esArray) //Arrays con: fixed tipovalor nombrevalor[dimension]
                    {
                        if (StructParser2.estructuras.ContainsKey(ValorTemp.typo) || tipovalor == "IntPtr")
                        {
                            for (int n=1;n<ValorTemp.maxAray;n++)
                            {
                                linea = "\t" + "\t" + "public " + tipovalor + " " + ValorTemp.nombre+n.ToString()+";";
                                sw.WriteLine(linea); //ESCRIBIR LINEA DE VALOR
                            }
                            linea = "\t" + "\t" + "public " + tipovalor + " " + ValorTemp.nombre + ValorTemp.maxAray;
                        }
                    }*/
                    linea += ";";
					sw.WriteLine(linea); //ESCRIBIR LINEA DE VALOR
				}
				
				/*if (valorado)
				{
					sw.WriteLine();
					sw.WriteLine("\t"+"\t"+"public "+key+"()");
					sw.WriteLine("\t"+"\t"+"{");
					foreach (string key3 in StructParser2.estructuras[key].valores.Keys)
					{
						Valor ValorTemp = StructParser2.estructuras[key].valores[key3]; //VALOR
						if (ValorTemp.tieneValor)
						{
							string s_lineavalora = "\t"+"\t"+"\t";	
							s_lineavalora += (ValorTemp.nombre+" = "+ValorTemp.svalor+";");
							sw.WriteLine(s_lineavalora);
						}
					}
					sw.WriteLine("\t"+"\t"+"}");
				}*/
				
				sw.WriteLine("\t"+"}");
                sw.WriteLine();
            }
			
			sw.WriteLine("}");
			sw.WriteLine();
			sw.Close();
		}
	}
}
