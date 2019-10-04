using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arena
{
    public partial class frmTurno : Form
    {
        Image Foto_Habilidade, Foto_Personagem, Foto_Monstro;
        string Alvo, Turno;
        public int HabAleatorios;

        protected int Verdes, Azuis, Vermelhos;
        protected int VerdesGastos, AzuisGastos, VermelhosGastos;
        protected int VerdesGanhos, AzuisGanhos, VermelhosGanhos;

        public int RetornaVerdesGastos()
        {
            return VerdesGastos;
        }
        public int RetornaAzuisGastos()
        {
            return AzuisGastos;
        }
        public int RetornaVermelhosGastos()
        {
            return VermelhosGastos;
        }
        public int RetornaVerdesGanhos()
        {
            return VerdesGanhos;
        }
        public int RetornaAzuisGanhos()
        {
            return AzuisGanhos;
        }
        public int RetornaVermelhosGanhos()
        {
            return VermelhosGanhos;
        }

		//Quando o Personagem clica no botão
        public frmTurno(Image Hab, Image Personagem, Image Monstro, string Alvo, string Turno, int HabAleatorios, int Verdes, int Azuis, int Vermelhos)
        {
            InitializeComponent();

            Foto_Habilidade = Hab;
            Foto_Personagem = Personagem;
            Foto_Monstro = Monstro;

            this.Alvo = Alvo;
            this.Turno = Turno;
            this.HabAleatorios = HabAleatorios;

            this.Verdes = Verdes;
            this.Azuis = Azuis;
            this.Vermelhos = Vermelhos;
        }

		//Quando o Monstro faz seu turno
		public frmTurno(Image Hab, Image Personagem, Image Monstro, string Alvo, string Turno)
		{
			InitializeComponent();

			Foto_Habilidade = Hab;
			Foto_Personagem = Personagem;
			Foto_Monstro = Monstro;

			this.Alvo = Alvo;
			this.Turno = Turno;
		}

		//Quando é clicado para trocar a energia;
		public frmTurno(string Alvo, string Turno, int Verdes, int Azuis, int Vermelhos)
		{
			InitializeComponent();

			this.Alvo = Alvo;

			this.Verdes = Verdes;
			this.Azuis = Azuis;
			this.Vermelhos = Vermelhos;
		}

        private void frmTurno_Load(object sender, EventArgs e)
        {
            picHabilidade.Image = Foto_Habilidade;
            picPersonagem.Image = Foto_Personagem;
            picMonstro.Image = Foto_Monstro;

            ResetaValoresAleatorios();

            if (Turno == "Monstro")
            {
                Height = 242;

                lblTurno.Text = "Turno do Monstro:";            

                btnOK.Visible = true;
                btnConfirmar.Visible = false;
                btnCancelar.Visible = false;

                picSeta1.Image = Properties.Resources.SetaEsq;
                picSeta2.Image = Properties.Resources.SetaEsq;

                if (Alvo == "Monstro")
                {
                    picPersonagem.Image = Foto_Monstro;
                }
                if (Foto_Habilidade == null)
                {
                    picHabilidade.Image = Properties.Resources.Ponto_de_interrogacao;
                    picPersonagem.Image = Properties.Resources.Ponto_de_interrogacao;
                }
            }
            else
            {
                grpEscolherAleatorio.Visible = true;

                if (HabAleatorios == 0)
                {
                    grpEscolherAleatorio.Enabled = false;
                    grpEscolherAleatorio.Text = "Não há nenhum Aleatório nessa habilidade:";
                }
                else
                {
                    grpEscolherAleatorio.Text = "Escolha " + HabAleatorios.ToString() + " Aleatórios: ";
                    btnConfirmar.Enabled = false;
                }

                if (Foto_Habilidade == null)
                {
                    picHabilidade.Image = Properties.Resources.Ponto_de_interrogacao;
                    picMonstro.Image = Properties.Resources.Ponto_de_interrogacao;
                }


                if (Alvo == "Personagem" && Foto_Habilidade != null) //Se for uma habilidade de usar em si mesmo
                {
                    picMonstro.Image = Foto_Personagem;
                }

                if (Alvo == "TrocarEnergia")
                {
                    Height = 300;
                    btnConfirmar.Location = new Point(btnConfirmar.Location.X, 201);
                    btnCancelar.Location = new Point(btnCancelar.Location.X, 201);
                    grpEscolherAleatorio.Location = new Point(grpEscolherAleatorio.Location.X, 55);

                    picPersonagem.Visible = false;
                    picSeta1.Visible = false;
                    picHabilidade.Visible = false;
                    picSeta2.Visible = false;
                    picMonstro.Visible = false;

                    picTrocarVerde.Visible = true;                    
                    picTrocarAzul.Visible = true;
                    picTrocarVermelho.Visible = true;

                    btnVerdes_Mais.Visible = false;
                    btnVerdes_Menos.Visible = false;
                    btnAzuis_Mais.Visible = false;
                    btnAzuis_Menos.Visible = false;
                    btnVermelhos_Mais.Visible = false;
                    btnVermelhos_Menos.Visible = false;

                    btnEscolherTrocarVerde.Visible = true;
                    btnEscolherTrocarAzul.Visible = true;
                    btnEscolherTrocarVermelho.Visible = true;
                    btnEscolherPorVerde.Visible = true;
                    btnEscolherPorAzul.Visible = true;
                    btnEscolherPorVermelho.Visible = true;

                    lblRestVerdes_num.Visible = false;
                    lblRestAzuis_num.Visible = false;
                    lblRestVermelhos_num.Visible = false;
                    lblAleaVerdes_num.Visible = false;
                    lblAleaAzuis_num.Visible = false;
                    lblAleaVermelhos_num.Visible = false;


                    btnConfirmar.Enabled = false;

                    lblEscolha.Visible = true;
                    lblTurno.Text = "Trocar Energias:";
                    grpEscolherAleatorio.Text = "Escolha 2 energias pra serem trocadas:";
                    grpEscolherAleatorio.Enabled = true;
                    lblEnergiasRestantes.Text = "Trocar:";
                    lblEnergiaAleatoria.Text = "Por:";

                    /*
                    lblRestVerdes.Text = "Verdes";
                    lblRestAzuis.Text = "Azuis";
                    lblRestVermelhos.Text = "Vermelhos";
                    lblAleaVerdes.Text = "Verde";
                    lblAleaAzuis.Text = "Azul";
                    lblAleaVermelhos.Text = "Vermelho"; */

                    lblRestVerdes.Enabled = false;
                    lblRestAzuis.Enabled = false;
                    lblRestVermelhos.Enabled = false;
                    lblAleaVerdes.Enabled = false;
                    lblAleaAzuis.Enabled = false;
                    lblAleaVermelhos.Enabled = false;

                    if (Verdes < 2)
                    {
                        btnEscolherTrocarVerde.Enabled = false;
                    }
                    if (Azuis < 2)
                    {
                        btnEscolherTrocarAzul.Enabled = false;
                    }
                    if (Vermelhos < 2)
                    {
                        btnEscolherTrocarVermelho.Enabled = false;
                    }
                }
            }
        }

        private void btnEscolher_CheckedChanged(object sender, EventArgs e)
        {
            lblRestVerdes.Enabled = btnEscolherTrocarVerde.Checked;
            lblRestAzuis.Enabled = btnEscolherTrocarAzul.Checked;
            lblRestVermelhos.Enabled = btnEscolherTrocarVermelho.Checked;
            lblAleaVerdes.Enabled = btnEscolherPorVerde.Checked;
            lblAleaAzuis.Enabled = btnEscolherPorAzul.Checked;
            lblAleaVermelhos.Enabled = btnEscolherPorVermelho.Checked;

            VerdesGastos = 0;
            AzuisGastos = 0;
            VermelhosGastos = 0;
            VerdesGanhos = 0;
            AzuisGanhos = 0;
            VermelhosGanhos = 0;

            bool Prosseguir = true;

            if (btnEscolherTrocarVerde.Checked == true) { VerdesGastos = 2; }
            else if (btnEscolherTrocarAzul.Checked == true) { AzuisGastos = 2; }
            else if (btnEscolherTrocarVermelho.Checked == true) { VermelhosGastos = 2; }
            else { Prosseguir = false; }

            if (btnEscolherPorVerde.Checked == true) { VerdesGanhos = 1; }
            else if (btnEscolherPorAzul.Checked == true) { AzuisGanhos = 1; }
            else if (btnEscolherPorVermelho.Checked == true) { VermelhosGanhos = 1; }
            else { Prosseguir = false; }

            if (Prosseguir)
            {
                btnConfirmar.Enabled = true;               
            }
            else
            {
                btnConfirmar.Enabled = false;
            }

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            podeSair = true;
            Close();
        }

        private void ResetaValoresAleatorios()
        {
            lblRestVerdes_num.Text = Verdes.ToString();
            lblRestAzuis_num.Text = Azuis.ToString();
            lblRestVermelhos_num.Text = Vermelhos.ToString();

            lblAleaVerdes_num.Text = VerdesGastos.ToString();
            lblAleaAzuis_num.Text = AzuisGastos.ToString();
            lblAleaVermelhos_num.Text = VermelhosGastos.ToString();

            grpEscolherAleatorio.Text = grpEscolherAleatorio.Text = "Escolha " + (HabAleatorios - (VerdesGastos + AzuisGastos + VermelhosGastos)).ToString() + " Aleatórios: ";

            if (HabAleatorios - (VerdesGastos + AzuisGastos + VermelhosGastos) == 0)
            {
                btnConfirmar.Enabled = true;
            }
            else
            {
                btnConfirmar.Enabled = false;
            }
        }

        #region btnsVerdesAleatorios
        private void btnVerdes_Menos_Click(object sender, EventArgs e)
        {
            if (VerdesGastos > 0)
            {
                Verdes += 1;
                VerdesGastos -= 1;

                ResetaValoresAleatorios();
            }
        }
        private void btnVerdes_Mais_Click(object sender, EventArgs e)
        {
            if (Verdes > 0 && HabAleatorios - (VerdesGastos + AzuisGastos + VermelhosGastos) > 0)
            {
                Verdes -= 1;
                VerdesGastos += 1;

                ResetaValoresAleatorios();
            }
        }
        #endregion
        #region btnsAzuisAleatorios
        private void btnAzuis_Menos_Click(object sender, EventArgs e)
        {
            if (AzuisGastos > 0)
            {
                Azuis += 1;
                AzuisGastos -= 1;

                ResetaValoresAleatorios();
            }
        }
        private void btnAzuis_Mais_Click(object sender, EventArgs e)
        {
            if (Azuis > 0 && HabAleatorios - (VerdesGastos + AzuisGastos + VermelhosGastos) > 0)
            {
                Azuis -= 1;
                AzuisGastos += 1;

                ResetaValoresAleatorios();
            }
        }
        #endregion
        #region btnsVermelhosAleatorios
        private void btnVermelhos_Menos_Click(object sender, EventArgs e)
        {
            if (VermelhosGastos > 0)
            {
                Vermelhos += 1;
                VermelhosGastos -= 1;

                ResetaValoresAleatorios();
            }
        }
        private void btnVermelhos_Mais_Click(object sender, EventArgs e)
        {
            if (Vermelhos > 0 && HabAleatorios - (VerdesGastos + AzuisGastos + VermelhosGastos) > 0)
            {
                Vermelhos -= 1;
                VermelhosGastos += 1;

                ResetaValoresAleatorios();
            }
        }
        #endregion

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            podeSair = true;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            podeSair = true;
            Close();
        }

        bool podeSair = false;

        private void frmTurno_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (podeSair == false)
            {
                Application.Exit();
            }
        }
    }
}
