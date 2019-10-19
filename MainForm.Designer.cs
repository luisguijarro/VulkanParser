/*
 * Creado por SharpDevelop.
 * Usuario: luisgp82
 * Fecha: 04/07/2018
 * Hora: 10:15
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
namespace VulkanParser
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.SplitContainer splitContainer1;
		public System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.CheckBox checkBox2;
		public System.Windows.Forms.CheckBox checkBox1;
		public System.Windows.Forms.TextBox textBoxExit;
		private System.Windows.Forms.GroupBox groupBox2;
		public System.Windows.Forms.CheckBox checkvkdevices;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkvkdevices = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxExit = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Black;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(357, 106);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(363, 7);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Select Xml File";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(732, 7);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(61, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "From Net";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(363, 36);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(130, 23);
			this.button3.TabIndex = 4;
			this.button3.Text = "Select Destination Path";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(364, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 19);
			this.label1.TabIndex = 6;
			this.label1.Text = "namespace";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(441, 64);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(107, 20);
			this.textBox3.TabIndex = 7;
			this.textBox3.Text = "ENTK";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(548, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = ".Vulkan";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(363, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 19);
			this.label3.TabIndex = 9;
			this.label3.Text = "{";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripProgressBar1,
			this.toolStripStatusLabel1,
			this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 466);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(804, 22);
			this.statusStrip1.TabIndex = 10;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(350, 16);
			this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.toolStripProgressBar1.ToolTipText = "PARCIAL:";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.AutoSize = false;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
			this.toolStripStatusLabel1.Text = "0%";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.AutoSize = false;
			this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
			| System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
			| System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
			this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
			this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(380, 17);
			this.toolStripStatusLabel2.Text = "Estado";
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.button4.Location = new System.Drawing.Point(702, 64);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(91, 42);
			this.button4.TabIndex = 11;
			this.button4.Text = "INIT PARSING";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button5.ForeColor = System.Drawing.Color.Red;
			this.button5.Location = new System.Drawing.Point(605, 64);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(91, 42);
			this.button5.TabIndex = 12;
			this.button5.Text = "ABORT PARSING";
			this.button5.UseVisualStyleBackColor = true;
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.folderBrowserDialog1.SelectedPath = "./";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "vk.xml";
			this.openFileDialog1.Filter = "XML|*.xml";
			this.openFileDialog1.InitialDirectory = "./";
			this.openFileDialog1.Title = "Selecciona archivo vk.xml";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.BackColor = System.Drawing.Color.White;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Location = new System.Drawing.Point(457, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(269, 20);
			this.label4.TabIndex = 13;
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label5.BackColor = System.Drawing.Color.White;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label5.Location = new System.Drawing.Point(499, 37);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(294, 20);
			this.label5.TabIndex = 14;
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Location = new System.Drawing.Point(0, 112);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.richTextBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(800, 351);
			this.splitContainer1.SplitterDistance = 408;
			this.splitContainer1.TabIndex = 15;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.richTextBox1.Size = new System.Drawing.Size(408, 351);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.checkvkdevices);
			this.groupBox2.Location = new System.Drawing.Point(3, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(382, 160);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Crear Clases Extra:";
			// 
			// checkvkdevices
			// 
			this.checkvkdevices.Location = new System.Drawing.Point(6, 19);
			this.checkvkdevices.Name = "checkvkdevices";
			this.checkvkdevices.Size = new System.Drawing.Size(104, 24);
			this.checkvkdevices.TabIndex = 0;
			this.checkvkdevices.Text = "VKDevice.cs";
			this.checkvkdevices.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxExit);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.checkBox2);
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Location = new System.Drawing.Point(3, 169);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(382, 179);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Crear Biblioteca .dll independiente:";
			// 
			// textBoxExit
			// 
			this.textBoxExit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxExit.BackColor = System.Drawing.Color.Black;
			this.textBoxExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBoxExit.ForeColor = System.Drawing.Color.White;
			this.textBoxExit.Location = new System.Drawing.Point(6, 69);
			this.textBoxExit.Multiline = true;
			this.textBoxExit.Name = "textBoxExit";
			this.textBoxExit.ReadOnly = true;
			this.textBoxExit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxExit.Size = new System.Drawing.Size(370, 104);
			this.textBoxExit.TabIndex = 5;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(162, 46);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = ".dll";
			// 
			// textBox1
			// 
			this.textBox1.Enabled = false;
			this.textBox1.Location = new System.Drawing.Point(60, 43);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 46);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 17);
			this.label6.TabIndex = 2;
			this.label6.Text = "Nombre:";
			// 
			// checkBox2
			// 
			this.checkBox2.Enabled = false;
			this.checkBox2.Location = new System.Drawing.Point(253, 41);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(123, 24);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Eliminar archivos .cs";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(6, 19);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(104, 24);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Crear Biblioteca";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(26, 46);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 23);
			this.label8.TabIndex = 1;
			this.label8.Text = "VKDelegates.cs";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(26, 69);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(115, 23);
			this.label9.TabIndex = 2;
			this.label9.Text = "VKDelegatesTools.cs";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 488);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(820, 526);
			this.Name = "MainForm";
			this.Text = "C# Vulkan XML Parser";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
