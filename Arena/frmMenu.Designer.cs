namespace Arena
{
	partial class frmMenu
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
			this.btnJogar = new System.Windows.Forms.Button();
			this.btnSobre = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnJogar
			// 
			this.btnJogar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.btnJogar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnJogar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnJogar.Location = new System.Drawing.Point(12, 12);
			this.btnJogar.Name = "btnJogar";
			this.btnJogar.Size = new System.Drawing.Size(760, 40);
			this.btnJogar.TabIndex = 0;
			this.btnJogar.Text = "Jogar";
			this.btnJogar.UseVisualStyleBackColor = false;
			this.btnJogar.Click += new System.EventHandler(this.btnJogar_Click);
			// 
			// btnSobre
			// 
			this.btnSobre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.btnSobre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSobre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSobre.Location = new System.Drawing.Point(668, 509);
			this.btnSobre.Name = "btnSobre";
			this.btnSobre.Size = new System.Drawing.Size(104, 40);
			this.btnSobre.TabIndex = 1;
			this.btnSobre.Text = "Sobre";
			this.btnSobre.UseVisualStyleBackColor = false;
			this.btnSobre.Click += new System.EventHandler(this.btnSobre_Click);
			// 
			// frmMenu
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.btnSobre);
			this.Controls.Add(this.btnJogar);
			this.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "frmMenu";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Menu";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnJogar;
		private System.Windows.Forms.Button btnSobre;
	}
}