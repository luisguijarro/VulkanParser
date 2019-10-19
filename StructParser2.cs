using System;
using System.Xml;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
    public static class StructParser2
    {
        //private static float f_Work;
        public static Dictionary<string, Estructura> estructuras;
        public static void Parse(XmlDocument xdoc)
        {
            //f_Work = 0f;
            estructuras = new Dictionary<string, Estructura>();
            XmlNodeList xml_structs = xdoc.SelectNodes("/registry/types/type[@category='struct']");
            if (xml_structs.Count > 0)
            {
                for (int i = 0; i < xml_structs.Count; i++) // RECORRER ESTRUCTURAS.
                {
                    Estructura str = new Estructura(); //Declaracion de estructura
                    XmlNode xmln = xml_structs[i];
                    string s_name_struct = xmln.Attributes["name"].Value; //NOMBRE DE ESTRUCTURA
                    str.Nombre = s_name_struct;

                    XmlNodeList xml_miembros = xmln.SelectNodes("member"); //LISTA DE VALORES DE STRUCTURA
                    if (xml_miembros.Count > 0)
                    {
                        for (int m = 0; m < xml_miembros.Count; m++) //REPASO A LOS VALORES DE ESTRUCTURA
                        {
                            Valor vl = new Valor(); //Declaración de Valor
                            string s_m_name = xml_miembros[m].SelectSingleNode("name").InnerText; //obtener nombre
                            vl.nombre = s_m_name; //Indicar nombre de valor.
                            string s_m_type = xml_miembros[m].SelectSingleNode("type").InnerText; //obtener tipo
                            vl.puntero = xml_miembros[m].InnerText.Contains("*"); //¿Es puntero?
                            vl.constante = xml_miembros[m].InnerText.Contains("const"); //¿Es Constante?

                            XmlNode copianodo = xml_miembros[m].Clone();
                            if (copianodo.SelectSingleNode("comment") != null)
                            {
                                /*copianodo = */copianodo.RemoveChild(copianodo.SelectSingleNode("comment"));
                            }
                            vl.esArray = copianodo.InnerText.Contains("["); //¿Es Array?

                            // SI EL VALOR ES UN ARRAY HAY QUE OBTENER LA DIMENSIÓN DEL MISMO:
                            if (vl.esArray) // ¿El valor es un array?
                            {
                                string s_value = copianodo.InnerText.Split('[')[1].Split(']')[0];
                                //DEFINIR VALOR A UINT
                                uint maxval = 0;
                                if (!uint.TryParse(s_value, out maxval))
                                {
                                    //buscar valor entre los enum.
                                    if (copianodo.SelectSingleNode("enum")!= null)
                                    {
                                        s_value = copianodo.SelectSingleNode("enum").InnerText;
                                        if (EnumParser.d_TotalEnumValues.ContainsKey(s_value))
                                        {
                                            vl.maxAray = uint.Parse(EnumParser.d_TotalEnumValues[s_value]);
                                        }
                                        else
                                        {
                                            if (APIConstantsParser.ConstValues.ContainsKey(s_value))
                                            {
                                                vl.maxAray = uint.Parse(APIConstantsParser.ConstValues[s_value]);
                                            }
                                            else
                                            {
                                                throw new Exception("No existe valor para enumerador con nombre: " + s_value);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("El Valor del Array no es un enumerador ni uint");
                                    }
                                    //Arrays con: fixed tipovalor nombrevalor[dimension]
                                }
                                else
                                {
                                    vl.maxAray = maxval;
                                }
                            }

                            // OBTENER EL TIPO DE VALOR:
                            if (EnumParser.d_Enums.ContainsKey(s_m_type)) 
                            {
                                if (xml_miembros[m].Attributes["values"] != null)
                                {
                                    string svalor = xml_miembros[m].Attributes["values"].Value;
                                    if (EnumParser.d_TotalEnumValues.ContainsKey(svalor))
                                    {
                                        vl.typo = s_m_type;
                                        vl.svalor = s_m_type + "." + svalor;
                                        vl.esStruct = false;
                                        vl.tieneValor = true;
                                    }
                                    else
                                    {
                                        throw new Exception("OSTIAS!!! No existe el valor en el enumerador");
                                    }
                                    // En caso contrario algo no va bien.
                                }
                                else
                                {
                                    vl.typo = s_m_type;
                                    //throw new Exception("OSTIAS!!! No existe el enumerador");
                                }
                                // En caso contrario algo no va bien.
                            }
                            else if (estructuras.ContainsKey(s_m_type))
                            {
                                vl.typo = s_m_type;
                                vl.esStruct = true;
                            }
                            else
                            {
                                vl.typo = s_m_type;
                            }


                            // OBTENER COMENTARIO EN CASO DE QUE LO TENGA:
                            if (xml_miembros[m].SelectSingleNode("comment") != null) //¿Tiene comentario?
                            {
                                vl.comentado = true;
                                vl.comentario = xml_miembros[m].SelectSingleNode("comment").InnerText; //Añadir Comentario.
                            }
                            if (!vl.tieneValor)
                            {
                                vl.constante = false;
                            }

                            // AÑADIR VALORES A LA ESTRUCTURA.
                            str.valores.Add(s_m_name, vl);
                        }
                    }
                    estructuras.Add(s_name_struct, str); //Añade Estructura a la lista
                }
            }
        }
    }
}
