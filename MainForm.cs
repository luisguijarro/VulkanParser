using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace VulkanParser
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Thread hiloParse;
		int valor;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.hiloParse = new Thread(new ThreadStart(InitParse));
			if (System.IO.File.Exists("./vk.xml"))
			{
				this.label4.Text = "./vk.xml";
				this.openFileDialog1.FileName = "./vk.xml";
			}
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		#region OVERRIDE METHODS:
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if (this.hiloParse.IsAlive)
			{
				this.hiloParse.Suspend();
				if (MessageBox.Show("Si cierra la aplicación el proceso de parseado se interrumpirá. \n ¿Realmente desea abortar el proceso?", "Solicitud de Cerrado de Aplicación", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					this.hiloParse.Abort();
					base.OnClosing(e);
				}
				else
				{
					this.hiloParse.Resume();
				}
			}
			else
			{
				base.OnClosing(e);
			}
		}
		#endregion
		
		#region PRIVATE METHODS:
		public void Habilitar(bool habilitar, bool todo)
		{
			lock(this)
			{
				if (todo)
				{
					for (int i=0;i<this.Controls.Count;i++)
					{
						this.Controls[i].Enabled = habilitar;
					}
				}
				else
				{
					for (int i=0;i<this.Controls.Count;i++)
					{
						this.Controls[i].Enabled = habilitar;
					}
					this.richTextBox1.Enabled = true;
					this.button5.Enabled = true;
				}
			}
		}
		private void InitParse()
		{
			VKxmlParser.Parse(this.openFileDialog1.FileName, this.textBox3.Text+".Vulkan", this.folderBrowserDialog1.SelectedPath+"/");
		}
		#endregion
			
		#region Event Methods		
		void Button1Click(object sender, EventArgs e)
		{
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				this.label4.Text = this.openFileDialog1.FileName;
			}
		}
		
		void Button2Click(object sender, EventArgs e) //DESCARGA XML
		{
			if (MessageBox.Show("Se va ha proceder a descargar el archivo vk.xml desde la rama  \"Master\" del repositorio oficial de Khronos Group. \n Este documento pesa entre 700 KB y 1 MB. \n ¿Está seguro de querer realizar esta descarga)", "Descarga de vk.xml", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				Habilitar(false, true);
				this.button4.Enabled = false;
				using (System.Net.WebClient wc = new System.Net.WebClient())
				{
					wc.DownloadProgressChanged += delegate(object o_sender, System.Net.DownloadProgressChangedEventArgs f) 
					{
						this.toolStripProgressBar1.Value =  f.ProgressPercentage;
						this.toolStripStatusLabel1.Text = f.ProgressPercentage.ToString()+"%";
					};
					wc.DownloadFileCompleted += delegate(object d_sender, System.ComponentModel.AsyncCompletedEventArgs f2) 
					{
						if (f2.Cancelled)
						{
							MessageBox.Show("Descarga de vk.xml Cancelada");
						}
						if (f2.Error != null)
						{
							MessageBox.Show(f2.Error.Message, "Error en descarga de vk.xml");
						}
						if ((f2.Error == null) && (!f2.Cancelled))
						{
							MessageBox.Show("Descarga de vk.xml Realizada con Exito");
							this.label4.Text = "./vk.xml";
							this.openFileDialog1.FileName = "./vk.xml";
						}
						Habilitar(true, true);
					};
					this.toolStripStatusLabel2.Text = "Descargando vk.xml desde repositorio oficial.";
					//wc.UseDefaultCredentials = false;
					wc.Proxy = System.Net.WebRequest.GetSystemWebProxy();
					wc.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
					//MessageBox.Show(wc.Proxy.ToString());
					Uri uri = new Uri("https://raw.githubusercontent.com/KhronosGroup/Vulkan-Docs/master/xml/vk.xml");
					wc.DownloadFileAsync(uri, "./vk.xml");					
					this.toolStripStatusLabel2.Text = "Archivo vk.xml descargado.";
				}
			}
		}
		
		void Button3Click(object sender, EventArgs e) //DESTINO
		{
			if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				this.label5.Text = this.folderBrowserDialog1.SelectedPath+"/";
			}
		}
		void Button4Click(object sender, EventArgs e) //Init Parsing
		{
			if (label4.Text.Length > 0)
			{
				if (label5.Text.Length > 0) 
				{
					if (this.textBox3.Text.Length>0)
					{
						//this.InitParse();
						this.valor = 0;
						this.toolStripProgressBar1.Value = this.valor;
						this.toolStripStatusLabel1.Text = this.valor.ToString()+"%";
						Habilitar(false, false);
						//this.hiloParse = new Thread(new ThreadStart(InitParse));
						this.hiloParse.Start();
					}
					else
					{
						MessageBox.Show("NameSpace no especificado");
					}
				}
				else
				{
					MessageBox.Show("Ruta de Destino no especificada.");
				}
			}
			else
			{
				MessageBox.Show("Archivo XML no especificado.");
			}
		}
		#endregion
		
		public void SetValue(int i_valor)
		{
			lock(this)
			{
				this.valor = i_valor;
				this.toolStripStatusLabel1.Text = ((int)(this.valor/4f)).ToString()+"%";
				this.toolStripProgressBar1.Value = (int)(this.valor/4f);
			}
		}
		
		public void SetFinish()
		{			
			this.Habilitar(true, true);
		}
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			this.checkBox2.Enabled = this.checkBox1.Checked;
			this.textBox1.Enabled = this.checkBox1.Checked;
		}
	}
	
	public delegate void FinishDel();	
}
