namespace ArenaV2
{
    partial class frmSelecionar
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
            this.dtgPersonagens = new System.Windows.Forms.DataGridView();
            this.Foto = new System.Windows.Forms.DataGridViewImageColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPersonagem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpInformacoes = new System.Windows.Forms.GroupBox();
            this.picEnergia5 = new System.Windows.Forms.PictureBox();
            this.picEnergia4 = new System.Windows.Forms.PictureBox();
            this.lblRecarga = new System.Windows.Forms.Label();
            this.picEnergia3 = new System.Windows.Forms.PictureBox();
            this.picEnergia2 = new System.Windows.Forms.PictureBox();
            this.picEnergia1 = new System.Windows.Forms.PictureBox();
            this.picHabInfoSelecionada = new System.Windows.Forms.PictureBox();
            this.lblInfoNome = new System.Windows.Forms.Label();
            this.txtInfoPersonagem = new System.Windows.Forms.RichTextBox();
            this.picHab4Info = new System.Windows.Forms.PictureBox();
            this.picHab3Info = new System.Windows.Forms.PictureBox();
            this.picHab2Info = new System.Windows.Forms.PictureBox();
            this.picHab1Info = new System.Windows.Forms.PictureBox();
            this.picPersonagemInfo = new System.Windows.Forms.PictureBox();
            this.lblEnergia = new System.Windows.Forms.Label();
            this.btnEscolher = new System.Windows.Forms.Button();
            this.picPersonagemEscolhido1 = new System.Windows.Forms.PictureBox();
            this.picPersonagemEscolhido2 = new System.Windows.Forms.PictureBox();
            this.picPersonagemEscolhido3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPersonagens)).BeginInit();
            this.grpInformacoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHabInfoSelecionada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab4Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab3Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab2Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab1Info)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemEscolhido1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemEscolhido2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemEscolhido3)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgPersonagens
            // 
            this.dtgPersonagens.AllowUserToAddRows = false;
            this.dtgPersonagens.AllowUserToDeleteRows = false;
            this.dtgPersonagens.AllowUserToResizeColumns = false;
            this.dtgPersonagens.AllowUserToResizeRows = false;
            this.dtgPersonagens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPersonagens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Foto,
            this.Nome,
            this.Descricao,
            this.Id});
            this.dtgPersonagens.Location = new System.Drawing.Point(12, 321);
            this.dtgPersonagens.Name = "dtgPersonagens";
            this.dtgPersonagens.ReadOnly = true;
            this.dtgPersonagens.RowHeadersVisible = false;
            this.dtgPersonagens.RowHeadersWidth = 75;
            this.dtgPersonagens.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dtgPersonagens.RowTemplate.Height = 75;
            this.dtgPersonagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dtgPersonagens.Size = new System.Drawing.Size(647, 228);
            this.dtgPersonagens.TabIndex = 11;
            this.dtgPersonagens.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPersonagens_CellClick);
            this.dtgPersonagens.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgPersonagens_CellDoubleClick);
            // 
            // Foto
            // 
            this.Foto.DataPropertyName = "Foto";
            this.Foto.Frozen = true;
            this.Foto.HeaderText = "Foto";
            this.Foto.Name = "Foto";
            this.Foto.ReadOnly = true;
            this.Foto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Foto.Width = 75;
            // 
            // Nome
            // 
            this.Nome.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Visible = false;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // txtPersonagem
            // 
            this.txtPersonagem.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPersonagem.Location = new System.Drawing.Point(248, 282);
            this.txtPersonagem.MaxLength = 30;
            this.txtPersonagem.Name = "txtPersonagem";
            this.txtPersonagem.Size = new System.Drawing.Size(394, 32);
            this.txtPersonagem.TabIndex = 12;
            this.txtPersonagem.TextChanged += new System.EventHandler(this.txtPersonagem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 27);
            this.label1.TabIndex = 13;
            this.label1.Text = "Pesquisar Personagens:";
            // 
            // grpInformacoes
            // 
            this.grpInformacoes.Controls.Add(this.picEnergia5);
            this.grpInformacoes.Controls.Add(this.picEnergia4);
            this.grpInformacoes.Controls.Add(this.lblRecarga);
            this.grpInformacoes.Controls.Add(this.picEnergia3);
            this.grpInformacoes.Controls.Add(this.picEnergia2);
            this.grpInformacoes.Controls.Add(this.picEnergia1);
            this.grpInformacoes.Controls.Add(this.picHabInfoSelecionada);
            this.grpInformacoes.Controls.Add(this.lblInfoNome);
            this.grpInformacoes.Controls.Add(this.txtInfoPersonagem);
            this.grpInformacoes.Controls.Add(this.picHab4Info);
            this.grpInformacoes.Controls.Add(this.picHab3Info);
            this.grpInformacoes.Controls.Add(this.picHab2Info);
            this.grpInformacoes.Controls.Add(this.picHab1Info);
            this.grpInformacoes.Controls.Add(this.picPersonagemInfo);
            this.grpInformacoes.Controls.Add(this.lblEnergia);
            this.grpInformacoes.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInformacoes.ForeColor = System.Drawing.Color.Red;
            this.grpInformacoes.Location = new System.Drawing.Point(212, 12);
            this.grpInformacoes.Name = "grpInformacoes";
            this.grpInformacoes.Size = new System.Drawing.Size(585, 235);
            this.grpInformacoes.TabIndex = 28;
            this.grpInformacoes.TabStop = false;
            this.grpInformacoes.Text = "Informações";
            this.grpInformacoes.Visible = false;
            // 
            // picEnergia5
            // 
            this.picEnergia5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEnergia5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEnergia5.Location = new System.Drawing.Point(453, 130);
            this.picEnergia5.Name = "picEnergia5";
            this.picEnergia5.Size = new System.Drawing.Size(12, 12);
            this.picEnergia5.TabIndex = 42;
            this.picEnergia5.TabStop = false;
            this.picEnergia5.Visible = false;
            // 
            // picEnergia4
            // 
            this.picEnergia4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEnergia4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEnergia4.Location = new System.Drawing.Point(439, 130);
            this.picEnergia4.Name = "picEnergia4";
            this.picEnergia4.Size = new System.Drawing.Size(12, 12);
            this.picEnergia4.TabIndex = 41;
            this.picEnergia4.TabStop = false;
            this.picEnergia4.Visible = false;
            // 
            // lblRecarga
            // 
            this.lblRecarga.AutoSize = true;
            this.lblRecarga.Font = new System.Drawing.Font("Trebuchet MS", 8.25F);
            this.lblRecarga.ForeColor = System.Drawing.Color.DimGray;
            this.lblRecarga.Location = new System.Drawing.Point(348, 215);
            this.lblRecarga.Name = "lblRecarga";
            this.lblRecarga.Size = new System.Drawing.Size(109, 16);
            this.lblRecarga.TabIndex = 40;
            this.lblRecarga.Text = "TEMPO DE RECARGA:";
            this.lblRecarga.Visible = false;
            // 
            // picEnergia3
            // 
            this.picEnergia3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEnergia3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEnergia3.Location = new System.Drawing.Point(425, 130);
            this.picEnergia3.Name = "picEnergia3";
            this.picEnergia3.Size = new System.Drawing.Size(12, 12);
            this.picEnergia3.TabIndex = 39;
            this.picEnergia3.TabStop = false;
            this.picEnergia3.Visible = false;
            // 
            // picEnergia2
            // 
            this.picEnergia2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEnergia2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEnergia2.Location = new System.Drawing.Point(411, 130);
            this.picEnergia2.Name = "picEnergia2";
            this.picEnergia2.Size = new System.Drawing.Size(12, 12);
            this.picEnergia2.TabIndex = 38;
            this.picEnergia2.TabStop = false;
            this.picEnergia2.Visible = false;
            // 
            // picEnergia1
            // 
            this.picEnergia1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picEnergia1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEnergia1.Location = new System.Drawing.Point(397, 130);
            this.picEnergia1.Name = "picEnergia1";
            this.picEnergia1.Size = new System.Drawing.Size(12, 12);
            this.picEnergia1.TabIndex = 31;
            this.picEnergia1.TabStop = false;
            this.picEnergia1.Visible = false;
            // 
            // picHabInfoSelecionada
            // 
            this.picHabInfoSelecionada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHabInfoSelecionada.Location = new System.Drawing.Point(500, 130);
            this.picHabInfoSelecionada.Name = "picHabInfoSelecionada";
            this.picHabInfoSelecionada.Size = new System.Drawing.Size(75, 75);
            this.picHabInfoSelecionada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHabInfoSelecionada.TabIndex = 36;
            this.picHabInfoSelecionada.TabStop = false;
            // 
            // lblInfoNome
            // 
            this.lblInfoNome.AutoSize = true;
            this.lblInfoNome.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfoNome.ForeColor = System.Drawing.Color.Red;
            this.lblInfoNome.Location = new System.Drawing.Point(7, 126);
            this.lblInfoNome.Name = "lblInfoNome";
            this.lblInfoNome.Size = new System.Drawing.Size(19, 22);
            this.lblInfoNome.TabIndex = 35;
            this.lblInfoNome.Text = "0";
            // 
            // txtInfoPersonagem
            // 
            this.txtInfoPersonagem.BackColor = System.Drawing.SystemColors.Control;
            this.txtInfoPersonagem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfoPersonagem.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInfoPersonagem.Location = new System.Drawing.Point(10, 146);
            this.txtInfoPersonagem.Name = "txtInfoPersonagem";
            this.txtInfoPersonagem.ReadOnly = true;
            this.txtInfoPersonagem.Size = new System.Drawing.Size(465, 66);
            this.txtInfoPersonagem.TabIndex = 34;
            this.txtInfoPersonagem.Text = "0";
            // 
            // picHab4Info
            // 
            this.picHab4Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHab4Info.Location = new System.Drawing.Point(500, 30);
            this.picHab4Info.Name = "picHab4Info";
            this.picHab4Info.Size = new System.Drawing.Size(75, 75);
            this.picHab4Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHab4Info.TabIndex = 32;
            this.picHab4Info.TabStop = false;
            this.picHab4Info.Click += new System.EventHandler(this.Info_PicsClick);
            // 
            // picHab3Info
            // 
            this.picHab3Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHab3Info.Location = new System.Drawing.Point(400, 30);
            this.picHab3Info.Name = "picHab3Info";
            this.picHab3Info.Size = new System.Drawing.Size(75, 75);
            this.picHab3Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHab3Info.TabIndex = 31;
            this.picHab3Info.TabStop = false;
            this.picHab3Info.Click += new System.EventHandler(this.Info_PicsClick);
            // 
            // picHab2Info
            // 
            this.picHab2Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHab2Info.Location = new System.Drawing.Point(300, 30);
            this.picHab2Info.Name = "picHab2Info";
            this.picHab2Info.Size = new System.Drawing.Size(75, 75);
            this.picHab2Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHab2Info.TabIndex = 30;
            this.picHab2Info.TabStop = false;
            this.picHab2Info.Click += new System.EventHandler(this.Info_PicsClick);
            // 
            // picHab1Info
            // 
            this.picHab1Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picHab1Info.Location = new System.Drawing.Point(200, 30);
            this.picHab1Info.Name = "picHab1Info";
            this.picHab1Info.Size = new System.Drawing.Size(75, 75);
            this.picHab1Info.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHab1Info.TabIndex = 29;
            this.picHab1Info.TabStop = false;
            this.picHab1Info.Click += new System.EventHandler(this.Info_PicsClick);
            // 
            // picPersonagemInfo
            // 
            this.picPersonagemInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPersonagemInfo.Location = new System.Drawing.Point(10, 30);
            this.picPersonagemInfo.Name = "picPersonagemInfo";
            this.picPersonagemInfo.Size = new System.Drawing.Size(75, 75);
            this.picPersonagemInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPersonagemInfo.TabIndex = 28;
            this.picPersonagemInfo.TabStop = false;
            this.picPersonagemInfo.Click += new System.EventHandler(this.Info_PicsClick);
            // 
            // lblEnergia
            // 
            this.lblEnergia.AutoSize = true;
            this.lblEnergia.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnergia.ForeColor = System.Drawing.Color.DimGray;
            this.lblEnergia.Location = new System.Drawing.Point(346, 129);
            this.lblEnergia.Name = "lblEnergia";
            this.lblEnergia.Size = new System.Drawing.Size(54, 16);
            this.lblEnergia.TabIndex = 37;
            this.lblEnergia.Text = "ENERGIA:";
            this.lblEnergia.Visible = false;
            // 
            // btnEscolher
            // 
            this.btnEscolher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEscolher.Enabled = false;
            this.btnEscolher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEscolher.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEscolher.Location = new System.Drawing.Point(834, 474);
            this.btnEscolher.Name = "btnEscolher";
            this.btnEscolher.Size = new System.Drawing.Size(162, 75);
            this.btnEscolher.TabIndex = 30;
            this.btnEscolher.Text = "Escolher Personagens";
            this.btnEscolher.UseVisualStyleBackColor = false;
            this.btnEscolher.Click += new System.EventHandler(this.btnEscolher_Click);
            // 
            // picPersonagemEscolhido1
            // 
            this.picPersonagemEscolhido1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPersonagemEscolhido1.Image = global::ArenaV2.Properties.Resources.Ponto_de_interrogacao;
            this.picPersonagemEscolhido1.Location = new System.Drawing.Point(834, 418);
            this.picPersonagemEscolhido1.Name = "picPersonagemEscolhido1";
            this.picPersonagemEscolhido1.Size = new System.Drawing.Size(50, 50);
            this.picPersonagemEscolhido1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPersonagemEscolhido1.TabIndex = 29;
            this.picPersonagemEscolhido1.TabStop = false;
            this.picPersonagemEscolhido1.DoubleClick += new System.EventHandler(this.picPersonagemEscolhido1_DoubleClick);
            // 
            // picPersonagemEscolhido2
            // 
            this.picPersonagemEscolhido2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPersonagemEscolhido2.Image = global::ArenaV2.Properties.Resources.Ponto_de_interrogacao;
            this.picPersonagemEscolhido2.Location = new System.Drawing.Point(890, 418);
            this.picPersonagemEscolhido2.Name = "picPersonagemEscolhido2";
            this.picPersonagemEscolhido2.Size = new System.Drawing.Size(50, 50);
            this.picPersonagemEscolhido2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPersonagemEscolhido2.TabIndex = 31;
            this.picPersonagemEscolhido2.TabStop = false;
            this.picPersonagemEscolhido2.DoubleClick += new System.EventHandler(this.picPersonagemEscolhido2_DoubleClick);
            // 
            // picPersonagemEscolhido3
            // 
            this.picPersonagemEscolhido3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPersonagemEscolhido3.Image = global::ArenaV2.Properties.Resources.Ponto_de_interrogacao;
            this.picPersonagemEscolhido3.Location = new System.Drawing.Point(946, 418);
            this.picPersonagemEscolhido3.Name = "picPersonagemEscolhido3";
            this.picPersonagemEscolhido3.Size = new System.Drawing.Size(50, 50);
            this.picPersonagemEscolhido3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPersonagemEscolhido3.TabIndex = 32;
            this.picPersonagemEscolhido3.TabStop = false;
            this.picPersonagemEscolhido3.DoubleClick += new System.EventHandler(this.picPersonagemEscolhido3_DoubleClick);
            // 
            // frmSelecionar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.picPersonagemEscolhido3);
            this.Controls.Add(this.picPersonagemEscolhido2);
            this.Controls.Add(this.btnEscolher);
            this.Controls.Add(this.picPersonagemEscolhido1);
            this.Controls.Add(this.grpInformacoes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPersonagem);
            this.Controls.Add(this.dtgPersonagens);
            this.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSelecionar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arena - Escolher Personagem";
            this.Load += new System.EventHandler(this.frmSelecionar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPersonagens)).EndInit();
            this.grpInformacoes.ResumeLayout(false);
            this.grpInformacoes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEnergia1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHabInfoSelecionada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab4Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab3Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab2Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHab1Info)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemEscolhido1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemEscolhido2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPersonagemEscolhido3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgPersonagens;
        private System.Windows.Forms.TextBox txtPersonagem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpInformacoes;
        private System.Windows.Forms.Label lblRecarga;
        private System.Windows.Forms.PictureBox picEnergia3;
        private System.Windows.Forms.PictureBox picEnergia2;
        private System.Windows.Forms.PictureBox picEnergia1;
        private System.Windows.Forms.PictureBox picHabInfoSelecionada;
        private System.Windows.Forms.Label lblInfoNome;
        private System.Windows.Forms.RichTextBox txtInfoPersonagem;
        private System.Windows.Forms.PictureBox picHab4Info;
        private System.Windows.Forms.PictureBox picHab3Info;
        private System.Windows.Forms.PictureBox picHab2Info;
        private System.Windows.Forms.PictureBox picHab1Info;
        private System.Windows.Forms.PictureBox picPersonagemInfo;
        private System.Windows.Forms.Label lblEnergia;
        private System.Windows.Forms.PictureBox picPersonagemEscolhido1;
        private System.Windows.Forms.Button btnEscolher;
        private System.Windows.Forms.PictureBox picEnergia5;
        private System.Windows.Forms.PictureBox picEnergia4;
        private System.Windows.Forms.DataGridViewImageColumn Foto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.PictureBox picPersonagemEscolhido2;
        private System.Windows.Forms.PictureBox picPersonagemEscolhido3;
    }
}