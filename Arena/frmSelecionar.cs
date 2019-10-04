using System;
using System.Collections.Generic;
using System.ComponentModel;
using Intermediario;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Arena
{
    public partial class frmSelecionar : Form
    {
        public frmSelecionar()
        {
            InitializeComponent();
        }


        frmArena FormArena;
		Intermediario.Arena Arena;

        Personagens Personagem = new Personagens();
        Monstros Monstros = new Monstros();

        MemoryStream Personagem_Foto, Foto_Hab1, Foto_Hab2, Foto_Hab3, Foto_Hab4;
        int rowIndex, doubleRowIndex;

        DataTable Dados = new DataTable();
        DataTable DadosMonstro = new DataTable();

        bool FoiEscolhido = false;

        PictureBox[] Info_Pics = new PictureBox[5];
		
        public void frmSelecionar_Load(object sender, EventArgs e)
        {
            try
            {
                Dados = Personagem.Listar();
                dtgPersonagens.DataSource = Dados;

                Info_Pics[0] = picEnergia1;
                Info_Pics[1] = picEnergia2;
                Info_Pics[2] = picEnergia3;
                Info_Pics[3] = picEnergia4;
                Info_Pics[4] = picEnergia5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void txtPersonagem_TextChanged(object sender, EventArgs e)
        {
            try
            {
				timer1.Start();				
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtPersonagem.Text))
			{
				dtgPersonagens.DataSource = Personagem.Listar();
				timer1.Stop();
			}
			else
			{
				dtgPersonagens.DataSource = Personagem.ListarSearch(txtPersonagem.Text);
				timer1.Stop(); 
			}
		}

		private void txtPersonagem_Leave(object sender, EventArgs e)
		{
			timer1.Stop();
		}

		private void dtgPersonagens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
					rowIndex = e.RowIndex;

                    byte[] ByteFoto = (byte[])(dtgPersonagens[dtgPersonagens.Columns["Foto_Personagem"].Index, e.RowIndex].Value);
                    Personagem_Foto = new MemoryStream(ByteFoto);

                    ByteFoto = (byte[])(dtgPersonagens[dtgPersonagens.Columns["Hab1_Foto"].Index, e.RowIndex].Value);
                    Foto_Hab1 = new MemoryStream(ByteFoto);
                    ByteFoto = (byte[])(dtgPersonagens[dtgPersonagens.Columns["Hab2_Foto"].Index, e.RowIndex].Value);
                    Foto_Hab2 = new MemoryStream(ByteFoto);
                    ByteFoto = (byte[])(dtgPersonagens[dtgPersonagens.Columns["Hab3_Foto"].Index, e.RowIndex].Value);
                    Foto_Hab3 = new MemoryStream(ByteFoto);
                    ByteFoto = (byte[])(dtgPersonagens[dtgPersonagens.Columns["Hab4_Foto"].Index, e.RowIndex].Value);
                    Foto_Hab4 = new MemoryStream(ByteFoto);

                    Info_CarregaInformacoesPersonagem();

                    grpInformacoes.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtgPersonagens_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    doubleRowIndex = e.RowIndex;
                    FoiEscolhido = true;
                    picPersonagemEscolhido.Image = Image.FromStream(Personagem_Foto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Info_PicsClick(object sender, EventArgs e)
        {
            try
            {
                if (((Control)sender).Name != "picPersonagemInfo")
                {
                    lblRecarga.Visible = true;
                    if (((Control)sender).Name == "picHab1Info")
                    {
                        Info_CarregaInformacoesHab1();
                    }
                    if (((Control)sender).Name == "picHab2Info")
                    {
                        Info_CarregaInformacoesHab2();
                    }
                    if (((Control)sender).Name == "picHab3Info")
                    {
                        Info_CarregaInformacoesHab3();
                    }
                    if (((Control)sender).Name == "picHab4Info")
                    {
                        Info_CarregaInformacoesHab4();
                    }
                }
                else
                {
                    Info_CarregaInformacoesPersonagem();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Info_CarregaInformacoesPersonagem()
        {
            lblEnergia.Visible = false;
            lblRecarga.Visible = false;
            picEnergia1.Visible = false;
            picEnergia2.Visible = false;
            picEnergia3.Visible = false;
            picEnergia4.Visible = false;
            picEnergia5.Visible = false;

            picPersonagemInfo.Image = Image.FromStream(Personagem_Foto);
            picHab1Info.Image = Image.FromStream(Foto_Hab1);
            picHab2Info.Image = Image.FromStream(Foto_Hab2);
            picHab3Info.Image = Image.FromStream(Foto_Hab3);
            picHab4Info.Image = Image.FromStream(Foto_Hab4);

            picHabInfoSelecionada.Image = Image.FromStream(Personagem_Foto);

            lblInfoNome.Text = dtgPersonagens[dtgPersonagens.Columns["Nome_Personagem"].Index, rowIndex].Value.ToString();
            txtInfoPersonagem.Text = dtgPersonagens[dtgPersonagens.Columns["Descricao_Personagem"].Index, rowIndex].Value.ToString();

        }
        private void Info_CarregaInformacoesHab1()
        {
            int Hab1Recarga = Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Recarga"].Index, rowIndex].Value);

            lblInfoNome.Text = dtgPersonagens[dtgPersonagens.Columns["Hab1_Nome"].Index, rowIndex].Value.ToString();
            txtInfoPersonagem.Text = dtgPersonagens[dtgPersonagens.Columns["Hab1_Descricao"].Index, rowIndex].Value.ToString();
            picHabInfoSelecionada.Image = picHab1Info.Image;
            CarregaPicEnergias("Hab1");
            if (Hab1Recarga == 1)
            {
                lblRecarga.Text = "TEMPO DE RECARGA: 1 TURNO";
            }
            else
            {
                lblRecarga.Text = "TEMPO DE RECARGA: " + Hab1Recarga.ToString() + " TURNOS";
            }
        }
        private void Info_CarregaInformacoesHab2()
        {
            int Hab2Recarga = Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Recarga"].Index, rowIndex].Value);

            lblInfoNome.Text = dtgPersonagens[dtgPersonagens.Columns["Hab2_Nome"].Index, rowIndex].Value.ToString();
            txtInfoPersonagem.Text = dtgPersonagens[dtgPersonagens.Columns["Hab2_Descricao"].Index, rowIndex].Value.ToString();
            picHabInfoSelecionada.Image = picHab2Info.Image;
            CarregaPicEnergias("Hab2");
            if (Hab2Recarga == 1)
            {
                lblRecarga.Text = "TEMPO DE RECARGA: 1 TURNO";
            }
            else
            {
                lblRecarga.Text = "TEMPO DE RECARGA: " + Hab2Recarga.ToString() + " TURNOS";
            }
        }
        private void Info_CarregaInformacoesHab3()
        {
            int Hab3Recarga = Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Recarga"].Index, rowIndex].Value);

            lblInfoNome.Text = dtgPersonagens[dtgPersonagens.Columns["Hab3_Nome"].Index, rowIndex].Value.ToString();
            txtInfoPersonagem.Text = dtgPersonagens[dtgPersonagens.Columns["Hab3_Descricao"].Index, rowIndex].Value.ToString();
            picHabInfoSelecionada.Image = picHab3Info.Image;
            CarregaPicEnergias("Hab3");
            if (Hab3Recarga == 1)
            {
                lblRecarga.Text = "TEMPO DE RECARGA: 1 TURNO";
            }
            else
            {
                lblRecarga.Text = "TEMPO DE RECARGA: " + Hab3Recarga.ToString() + " TURNOS";
            }
        }

		private void Info_CarregaInformacoesHab4()
        {
            int Hab4Recarga = Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Recarga"].Index, rowIndex].Value);

            lblInfoNome.Text = dtgPersonagens[dtgPersonagens.Columns["Hab4_Nome"].Index, rowIndex].Value.ToString();
            txtInfoPersonagem.Text = dtgPersonagens[dtgPersonagens.Columns["Hab4_Descricao"].Index, rowIndex].Value.ToString();
            picHabInfoSelecionada.Image = picHab4Info.Image;
            CarregaPicEnergias("Hab4");
            if (Hab4Recarga == 1)
            {
                lblRecarga.Text = "TEMPO DE RECARGA: 1 TURNO";
            }
            else
            {
                lblRecarga.Text = "TEMPO DE RECARGA: " + Hab4Recarga.ToString() + " TURNOS";
            }
        }
        private void CarregaPicEnergias(string NomeHabilidade)
        {
            int Verdes, Azuls, Vermelhos, Pretos;
            Verdes = Convert.ToInt32((dtgPersonagens[dtgPersonagens.Columns[NomeHabilidade + "_Verde"].Index, rowIndex].Value));
            Azuls = Convert.ToInt32((dtgPersonagens[dtgPersonagens.Columns[NomeHabilidade + "_Azul"].Index, rowIndex].Value));
            Vermelhos = Convert.ToInt32((dtgPersonagens[dtgPersonagens.Columns[NomeHabilidade + "_Vermelho"].Index, rowIndex].Value));
            Pretos = Convert.ToInt32((dtgPersonagens[dtgPersonagens.Columns[NomeHabilidade + "_Preto"].Index, rowIndex].Value));
            int totalEnergias = Verdes + Azuls + Vermelhos + Pretos;

            lblEnergia.Text = "ENERGIA:";

            for (int i = 0; i < Info_Pics.Length; i++)
            {
                Info_Pics[i].Visible = false;
            }

            if (totalEnergias > 0)
            {
                for (int i = 0; i < Verdes; i++)
                {
                    Info_Pics[i].Image = Properties.Resources.Verde;
                }
                for (int i = 0; i < Azuls; i++)
                {
                    Info_Pics[(Verdes + i)].Image = Properties.Resources.Azul;
                }
                for (int i = 0; i < Vermelhos; i++)
                {
                    Info_Pics[(Verdes + Azuls + i)].Image = Properties.Resources.Vermelho;
                }
                for (int i = 0; i < Pretos; i++)
                {
                    Info_Pics[(Verdes + Azuls + Vermelhos + i)].Image = Properties.Resources.Preto;
                }

                #region Deixar os Pic de Energia visiveis ou não

                switch (totalEnergias)
                {
                    case 1:
                        picEnergia1.Visible = true;
                        break;
                    case 2:
                        picEnergia1.Visible = true;
                        picEnergia2.Visible = true;
                        break;
                    case 3:
                        picEnergia1.Visible = true;
                        picEnergia2.Visible = true;
                        picEnergia3.Visible = true;
                        break;
                    case 4:
                        picEnergia1.Visible = true;
                        picEnergia2.Visible = true;
                        picEnergia3.Visible = true;
                        picEnergia4.Visible = true;
                        break;
                    case 5:
                        picEnergia1.Visible = true;
                        picEnergia2.Visible = true;
                        picEnergia3.Visible = true;
                        picEnergia4.Visible = true;
                        picEnergia5.Visible = true;
                        break;
                    default:
                        throw new Exception("Hab1 com erro!");
                }
                #endregion
            }
            else
            {
                lblEnergia.Text = lblEnergia.Text + " NENHUMA";
            }
        }

        private void btnEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                if (FoiEscolhido)
                {
                    this.Hide();
                    DadosMonstro = Monstros.ListarSearchID();
					dtgMonstros.DataSource = DadosMonstro;

					Arena = new Intermediario.Arena();

					SetarPersonagem();
					SetarMonstro();

                    FormArena = new frmArena(Arena);
                    FormArena.ShowDialog();

					doubleRowIndex = -1;
					FoiEscolhido = false;
					picPersonagemEscolhido.Image = Properties.Resources.Ponto_de_interrogacao;

					frmSelecionar_Load(null, null);

					grpInformacoes.Visible = false;
                    this.Show();
                }
                else
                {
                    throw new Exception("Selecione um personagem dando dois cliques na lista.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		protected void SetarPersonagem()
		{
			#region Personagem
			Arena.SetarPersonagem(Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["ID_Personagem"].Index, doubleRowIndex].Value)
				, dtgPersonagens[dtgPersonagens.Columns["Nome_Personagem"].Index, doubleRowIndex].Value.ToString()
				, dtgPersonagens[dtgPersonagens.Columns["Descricao_Personagem"].Index, doubleRowIndex].Value.ToString()
				, (byte[])dtgPersonagens[dtgPersonagens.Columns["Foto_Personagem"].Index, doubleRowIndex].Value);
			#endregion
			#region Hab1
			Arena.SetarHab1(dtgPersonagens[dtgPersonagens.Columns["Hab1_Nome"].Index, doubleRowIndex].Value.ToString()
				, dtgPersonagens[dtgPersonagens.Columns["Hab1_Descricao"].Index, doubleRowIndex].Value.ToString()
				, (byte[])dtgPersonagens[dtgPersonagens.Columns["Hab1_Foto"].Index, doubleRowIndex].Value
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Dano"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoPerfurante"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoPerfurantePorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoPerfurantePorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoVerdadeiro"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoVerdadeiroPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_DanoVerdadeiroPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Cura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_CuraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_CuraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Armadura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_ArmaduraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_ArmaduraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Recarga"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Verde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Azul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Vermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Preto"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_Invulnerabilidade"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_GanhoVerde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_GanhoAzul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_GanhoVermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab1_GanhoPreto"].Index, doubleRowIndex].Value));

			#endregion
			#region Hab2
			Arena.SetarHab2(dtgPersonagens[dtgPersonagens.Columns["Hab2_Nome"].Index, doubleRowIndex].Value.ToString()
				, dtgPersonagens[dtgPersonagens.Columns["Hab2_Descricao"].Index, doubleRowIndex].Value.ToString()
				, (byte[])dtgPersonagens[dtgPersonagens.Columns["Hab2_Foto"].Index, doubleRowIndex].Value
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Dano"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoPerfurante"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoPerfurantePorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoPerfurantePorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoVerdadeiro"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoVerdadeiroPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_DanoVerdadeiroPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Cura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_CuraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_CuraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Armadura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_ArmaduraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_ArmaduraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Recarga"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Verde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Azul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Vermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Preto"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_Invulnerabilidade"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_GanhoVerde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_GanhoAzul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_GanhoVermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab2_GanhoPreto"].Index, doubleRowIndex].Value));

			#endregion
			#region Hab3
			Arena.SetarHab3(dtgPersonagens[dtgPersonagens.Columns["Hab3_Nome"].Index, doubleRowIndex].Value.ToString()
				, dtgPersonagens[dtgPersonagens.Columns["Hab3_Descricao"].Index, doubleRowIndex].Value.ToString()
				, (byte[])dtgPersonagens[dtgPersonagens.Columns["Hab3_Foto"].Index, doubleRowIndex].Value
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Dano"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoPerfurante"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoPerfurantePorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoPerfurantePorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoVerdadeiro"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoVerdadeiroPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_DanoVerdadeiroPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Cura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_CuraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_CuraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Armadura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_ArmaduraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_ArmaduraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Recarga"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Verde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Azul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Vermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Preto"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_Invulnerabilidade"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_GanhoVerde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_GanhoAzul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_GanhoVermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab3_GanhoPreto"].Index, doubleRowIndex].Value));

			#endregion
			#region Hab4
			Arena.SetarHab4(dtgPersonagens[dtgPersonagens.Columns["Hab4_Nome"].Index, doubleRowIndex].Value.ToString()
				, dtgPersonagens[dtgPersonagens.Columns["Hab4_Descricao"].Index, doubleRowIndex].Value.ToString()
				, (byte[])dtgPersonagens[dtgPersonagens.Columns["Hab4_Foto"].Index, doubleRowIndex].Value
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Dano"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoPerfurante"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoPerfurantePorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoPerfurantePorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoVerdadeiro"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoVerdadeiroPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_DanoVerdadeiroPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Cura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_CuraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_CuraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Armadura"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_ArmaduraPorTurno"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_ArmaduraPorTurno_Turnos"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Recarga"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Verde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Azul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Vermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Preto"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_Invulnerabilidade"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_GanhoVerde"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_GanhoAzul"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_GanhoVermelho"].Index, doubleRowIndex].Value)
				, Convert.ToInt32(dtgPersonagens[dtgPersonagens.Columns["Hab4_GanhoPreto"].Index, doubleRowIndex].Value));

			#endregion
		}
		protected void SetarMonstro()
		{
			#region Monstro
			Arena.SetarMonstro(Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["ID_Monstro"].Index, 0].Value)
				, dtgMonstros[dtgMonstros.Columns["Nome_Monstro"].Index, 0].Value.ToString()
				, dtgMonstros[dtgMonstros.Columns["Descricao_Monstro"].Index, 0].Value.ToString()
				, (byte[])dtgMonstros[dtgMonstros.Columns["Foto_Monstro"].Index, 0].Value);
			#endregion
			#region Hab1
			Arena.SetarMonstro_Hab1(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Nome"].Index, 0].Value.ToString()
				, dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Descricao"].Index, 0].Value.ToString()
				, (byte[])dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Foto"].Index, 0].Value
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Dano"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_DanoPerfurante"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_DanoVerdadeiro"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Cura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Armadura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Recarga"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Invulnerabilidade"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab1_Disposicao"].Index, 0].Value));
			#endregion
			#region Hab2
			Arena.SetarMonstro_Hab2(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Nome"].Index, 0].Value.ToString()
				, dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Descricao"].Index, 0].Value.ToString()
				, (byte[])dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Foto"].Index, 0].Value
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Dano"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_DanoPerfurante"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_DanoVerdadeiro"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Cura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Armadura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Recarga"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Invulnerabilidade"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab2_Disposicao"].Index, 0].Value));
			#endregion
			#region Hab3
			Arena.SetarMonstro_Hab3(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Nome"].Index, 0].Value.ToString()
				, dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Descricao"].Index, 0].Value.ToString()
				, (byte[])dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Foto"].Index, 0].Value
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Dano"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_DanoPerfurante"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_DanoVerdadeiro"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Cura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Armadura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Recarga"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Invulnerabilidade"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab3_Disposicao"].Index, 0].Value));
			#endregion
			#region Hab4
			Arena.SetarMonstro_Hab4(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Nome"].Index, 0].Value.ToString()
				, dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Descricao"].Index, 0].Value.ToString()
				, (byte[])dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Foto"].Index, 0].Value
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Dano"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_DanoPerfurante"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_DanoVerdadeiro"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Cura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Armadura"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Recarga"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Invulnerabilidade"].Index, 0].Value)
				, Convert.ToInt32(dtgMonstros[dtgMonstros.Columns["Monstro_Hab4_Disposicao"].Index, 0].Value));
			#endregion
		}
	}    
}