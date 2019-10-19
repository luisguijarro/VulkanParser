using System;

namespace VulkanParser
{
	/// <summary>
	/// Description of TypeRemplacer.
	/// </summary>
	public static class TypeReplacer
	{
		public static string Replace(string source)
		{
			string ret = source;
			ret = ret.Replace("(void)", "()");
			ret = ret.Replace("void*", "IntPtr");
			ret = ret.Replace("char*", "string");
			ret = ret.Replace("uint8_t", "Byte");
			ret = ret.Replace("uint32_t", "UInt32");
			ret = ret.Replace("uint64_t", "UInt64");
			ret = ret.Replace("int32_t", "Int32");
			ret = ret.Replace("size_t", "UInt32");
			ret = ret.Replace("ssize_t", "Int32");
			return ret;
		}
	}
}
