using System;
using System.Windows.Forms;

namespace VulkanParser
{
	/// <summary>
	/// Description of FormUpdater.
	/// </summary>
	public static class FormUpdater
	{
		public static void UpdateText(string text)
		{
			((MainForm)Application.OpenForms[0]).Invoke(jamon, new object[]{text+"\r\n"}); //UpdateFormtext, new string[]{""});
		}
		public static void UpdateProgress(int cant)
		{
			((MainForm)Application.OpenForms[0]).Invoke(jamon2, new object[]{(int)cant}); //UpdateFormtext, new string[]{""});
		}
		public static void UpdateCompilerText(string text)
		{
			((MainForm)Application.OpenForms[0]).Invoke(jamon3, new object[]{text+"\r\n"}); //UpdateFormtext, new string[]{""});
		}				
		
		private static UpdateFormtext jamon = UpdateFtext;
		private static UpdateFormProgress jamon2 = UpdateFProgress;
		private static UpdateFormCompilerText jamon3 = UpdateFCompilerText;
		
		private static void UpdateFtext(string text)
		{
			((MainForm)Application.OpenForms[0]).richTextBox1.AppendText(text);
			((MainForm)Application.OpenForms[0]).richTextBox1.ScrollToCaret();
		}
		
		private static void UpdateFProgress(int cant)
		{
			((MainForm)Application.OpenForms[0]).SetValue((int)cant);
		}
		
		private static void UpdateFCompilerText(string text)
		{
			((MainForm)Application.OpenForms[0]).textBoxExit.Text = text;
		}
	}
	public delegate void UpdateFormtext(string text);	
	public delegate void UpdateFormProgress(int cant);
	public delegate void UpdateFormCompilerText(string text);	
}
