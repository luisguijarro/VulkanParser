using System;
using System.Xml;
using System.Collections.Generic;

namespace VulkanParser
{
	/// <summary>
	/// Description of Tools.
	/// </summary>
	public static class Tools
	{
		private static Dictionary<string,string> variabletypes; //OriginalVKType - C# Type
		private static Dictionary<string,string> aliases; //Name - Alias
		
		public static void DefineTypes(XmlDocument xdoc)
		{
			variabletypes = new Dictionary<string, string>();
			aliases = new Dictionary<string, string>();
			
			XmlNodeList xnlBaseType = xdoc.SelectNodes("/registry/types/type[@category='basetype']");
			for (int i=0;i<xnlBaseType.Count;i++)
			{
				XmlNode xmln = xnlBaseType[i];
				string name = xmln.SelectSingleNode("name").InnerText;
				string csname = VariableBaseType(xmln.SelectSingleNode("type").InnerText);
				variabletypes.Add(name,csname);
			}
			
			XmlNodeList xnlbitmask = xdoc.SelectNodes("/registry/types/type[@category='bitmask']");
			for (int i=0;i<xnlbitmask.Count;i++)
			{				
				XmlNode xmln2 = xnlbitmask[i];
				if (xmln2.ChildNodes.Count > 0)
				{					
					string name = xmln2.SelectSingleNode("name").InnerText;
					string tipo = xmln2.SelectSingleNode("type").InnerText;
					string csname = "";
					if (variabletypes.ContainsKey(tipo))
					{
						csname = variabletypes[tipo];
					}
					else					
					{
						csname = tipo;
					}
					variabletypes.Add(name,csname);
				}
				else
				{
					//Alliases.
					aliases.Add(xmln2.Attributes["name"].Value, xmln2.Attributes["alias"].Value);
				}
			}
			
		}
		
		public static string VariableType(string originaltype)
		{
			string asterisco = "";
			string sotipe = originaltype;
			switch(originaltype)
			{
				case "void":
					return "void";
				case "void*":
					return "IntPtr";
                case "IntPtr":
                    return "IntPtr";
                case "IntPtr*":
                    return "IntPtr*";
                case "char":
					return "char";
				case "char*":
					return "string";
				case "float":
					return "float";
				case "float*":
					return "float*";
				case "uint8_t":
					return "Byte";
				case "uint8_t*":
					return "Byte*";
                case "uint16_t":
                    return "UInt16";
                case "uint16_t*":
                    return "UInt16*";
                case "uint32_t":
					return "UInt32";
				case "uint32_t*":
					return "UInt32*";
				case "uint64_t":
					return "UInt64";
				case "uint64_t*":
					return "UInt64*";
				case "int":
					return "Int32";
				case "int*":
					return "Int32*";
                case "int16_t":
                    return "Int16";
                case "int16_t*":
                    return "Int16*";
                case "int32_t":
					return "Int32";
				case "int32_t*":
					return "Int32*";
                case "int64_t":
                    return "long";
                case "int64_t*":
                    return "long*";
                case "size_t*":
					return "UInt32*";
				case "size_t":
					return "UInt32";	
				case "ssize_t*":
					return "Int32";	
				case "ssize_t":
					return "Int32*";
                case "double":
                    return "double";
                case "double*":
                    return "double*";
                default:
					if (originaltype.Contains("*"))
					{
						sotipe = originaltype.Replace("*","");
						asterisco = "*";
					}
					if (variabletypes.ContainsKey(sotipe))
				    {
						return variabletypes[sotipe]+asterisco;
				    }
					else
					{
						if (aliases.ContainsKey(sotipe))
						{
							return aliases[sotipe]+asterisco;
						}
						else
						{
							if (EnumParser.d_Enums.ContainsKey(sotipe))
							{
								return sotipe+asterisco;
							}
							else
							{
								if (StructParser2.estructuras.ContainsKey(sotipe))
								{
									return sotipe+asterisco;
								}
								else
								{
									if (FuncPointerParser.funPointers.ContainsKey(sotipe))
									{
										return sotipe+asterisco;
									}
									else
									{										
										if (HandleParser.handleTypes.Contains(sotipe))
										{
											return "IntPtr"+asterisco;
										}
										else
										{
											if (UnionParser.unions.ContainsKey(sotipe))
											{
												return sotipe+asterisco;
											}
											else
											{
												if (IntPtrsParser.IntPtrs.Contains(sotipe))
												{
													return sotipe+asterisco;
												}
												else
												{
													return "FAIL DEFINE VARIABLE";
												}
											}
										}
									}
								}
							}
						}
					}
			}
		}
		
		private static string VariableBaseType(string originaltype)
		{
			switch(originaltype)
			{
				case "void":
					return "void";
				case "void*":
					return "IntPtr";
				case "char":
					return "char";
				case "float":
					return "float";
				case "uint8_t":
					return "Byte";
				case "uint32_t":
					return "UInt32";
				case "uint64_t":
					return "UInt64";
				case "int32_t":
					return "Int32";
				case "size_t":
					return "UIntPtr";	
				default:
					return originaltype;
			}
		}
	}
}
