using Intermediario;
using Model.Monstro;
using Model.Personagem;
using Model.Shared.Utilitarios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ArenaV2
{
    public partial class frmSelecionar : Form
    {
        #region Atributos

        private List<Personagem> lstPersonagens;
        private int personagensEscolhidos = 0;
        private Personagem personagemSelecionado;
        private Monstro monstroSelecionado;
        private readonly PictureBox[] infoPics = new PictureBox[5];
        private readonly List<byte[]> slotsOcupados = new List<byte[]>() { null, null, null };

        #endregion

        #region Event Methods

        public frmSelecionar()
        {
            InitializeComponent();
        }

        public void frmSelecionar_Load(object sender, EventArgs e)
        {
            try
            {
                dtgPersonagens.DataSource = lstPersonagens = new PersonagemBLL().Select();

                infoPics[0] = picEnergia1;
                infoPics[1] = picEnergia2;
                infoPics[2] = picEnergia3;
                infoPics[3] = picEnergia4;
                infoPics[4] = picEnergia5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPersonagem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPersonagem.Text))
            {
                dtgPersonagens.DataSource = lstPersonagens.Where(x => x.Nome.ToLower().StartsWith(txtPersonagem.Text.Trim().ToLower())).ToList();
            }
            else
            {
                dtgPersonagens.DataSource = lstPersonagens;
            }
        }

        private void dtgPersonagens_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int id = (int)dtgPersonagens[dtgPersonagens.Columns["Id"].Index, e.RowIndex].Value;
                    personagemSelecionado = lstPersonagens.Where(x => x.Id == id).First();

                    CarregaInformacoesPersonagem(personagemSelecionado);

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
                    SelecionaPersonagem(personagemSelecionado);
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
                    lblEnergia.Visible = true;
                    switch (((Control)sender).Name)
                    {
                        case "picHab1Info":
                            CarregaInformacoesHab1(personagemSelecionado);
                            break;
                        case "picHab2Info":
                            CarregaInformacoesHab2(personagemSelecionado);
                            break;
                        case "picHab3Info":
                            CarregaInformacoesHab3(personagemSelecionado);
                            break;
                        case "picHab4Info":
                            CarregaInformacoesHab4(personagemSelecionado);
                            break;
                        default:
                            CarregaInformacoesPersonagem(personagemSelecionado);
                            break;
                    }
                }
                else
                {
                    CarregaInformacoesPersonagem(personagemSelecionado);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEscolher_Click(object sender, EventArgs e)
        {
            try
            {
                if (personagensEscolhidos == 3)
                {
                    Hide();
                    monstroSelecionado = new MonstroBLL().GerarMonstroAleatorio();

                    //frmArena formArena = new frmArena(personagemSelecionado, monstroSelecionado);
                    //formArena.ShowDialog();

                    personagensEscolhidos = 0;
                    picPersonagemEscolhido1.Image = picPersonagemEscolhido2.Image = picPersonagemEscolhido3.Image = Properties.Resources.Ponto_de_interrogacao;
                    slotsOcupados[0] = slotsOcupados[1] = slotsOcupados[2] = null;
                    btnEscolher.Enabled = false;
                    grpInformacoes.Visible = false;

                    Show();
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

        #region DoubleClick nos Selecionados
        private void picPersonagemEscolhido1_DoubleClick(object sender, EventArgs e)
        {
            picPersonagemEscolhido1.Image = Properties.Resources.Ponto_de_interrogacao;
            slotsOcupados[0] = null;
            personagensEscolhidos = slotsOcupados.Where(x => x != null).Count();
            btnEscolher.Enabled = personagensEscolhidos == 3;
        }

        private void picPersonagemEscolhido2_DoubleClick(object sender, EventArgs e)
        {
            picPersonagemEscolhido2.Image = Properties.Resources.Ponto_de_interrogacao;
            slotsOcupados[1] = null;
            personagensEscolhidos = slotsOcupados.Where(x => x != null).Count();
            btnEscolher.Enabled = personagensEscolhidos == 3;
        }

        private void picPersonagemEscolhido3_DoubleClick(object sender, EventArgs e)
        {
            picPersonagemEscolhido3.Image = Properties.Resources.Ponto_de_interrogacao;
            slotsOcupados[2] = null;
            personagensEscolhidos = slotsOcupados.Where(x => x != null).Count();
            btnEscolher.Enabled = personagensEscolhidos == 3;
        }
        #endregion

        #endregion

        #region Private Methods

        private void SelecionaPersonagem(Personagem personagem)
        {
            if (slotsOcupados[0] == null && slotsOcupados[1] != personagem.Foto && slotsOcupados[2] != personagem.Foto)
            {
                slotsOcupados[0] = personagem.Foto;
                picPersonagemEscolhido1.Image = Utils.ConverteFoto(personagem.Foto);
                personagensEscolhidos++;
            }
            else if (slotsOcupados[1] == null && slotsOcupados[0] != personagem.Foto && slotsOcupados[2] != personagem.Foto)
            {
                slotsOcupados[1] = personagem.Foto;
                picPersonagemEscolhido2.Image = Utils.ConverteFoto(personagem.Foto);
                personagensEscolhidos++;
            }
            else if (slotsOcupados[2] == null && slotsOcupados[0] != personagem.Foto && slotsOcupados[1] != personagem.Foto)
            {
                slotsOcupados[2] = personagem.Foto;
                picPersonagemEscolhido3.Image = Utils.ConverteFoto(personagem.Foto);
                personagensEscolhidos++;
            }

            btnEscolher.Enabled = personagensEscolhidos == 3;
        }

        private void CarregaInformacoesPersonagem(Personagem personagem)
        {
            lblEnergia.Visible =
            lblRecarga.Visible =
            picEnergia1.Visible =
            picEnergia2.Visible =
            picEnergia3.Visible =
            picEnergia4.Visible =
            picEnergia5.Visible = false;

            int i = 0;
            picPersonagemInfo.Image = Image.FromStream(new MemoryStream(personagem.Foto));
            picHab1Info.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i++].Foto));
            picHab2Info.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i++].Foto));
            picHab3Info.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i++].Foto));
            picHab4Info.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i++].Foto));

            picHabInfoSelecionada.Image = Image.FromStream(new MemoryStream(personagem.Foto));

            lblInfoNome.Text = personagem.Nome;
            txtInfoPersonagem.Text = personagem.Descricao;
        }

        private void CarregaInformacoesHab1(Personagem personagem)
        {
            int i = 0;
            lblRecarga.Text = "TEMPO DE RECARGA: ";

            lblInfoNome.Text = personagem.Habilidades[i].Nome;
            txtInfoPersonagem.Text = personagem.Habilidades[i].Descricao;
            picHabInfoSelecionada.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i].Foto));
            lblRecarga.Text += personagem.Habilidades[i].Recarga == 1 ? $"{personagem.Habilidades[i].Recarga} TURNO" : $"{personagem.Habilidades[i].Recarga} TURNOS";
            CarregaPicEnergias(personagem.Habilidades[i]);
        }

        private void CarregaInformacoesHab2(Personagem personagem)
        {
            int i = 1;
            lblRecarga.Text = "TEMPO DE RECARGA: ";

            lblInfoNome.Text = personagem.Habilidades[i].Nome;
            txtInfoPersonagem.Text = personagem.Habilidades[i].Descricao;
            picHabInfoSelecionada.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i].Foto));
            lblRecarga.Text += personagem.Habilidades[i].Recarga == 1 ? $"{personagem.Habilidades[i].Recarga} TURNO" : $"{personagem.Habilidades[i].Recarga} TURNOS";
            CarregaPicEnergias(personagem.Habilidades[i]);
        }

        private void CarregaInformacoesHab3(Personagem personagem)
        {
            int i = 2;
            lblRecarga.Text = "TEMPO DE RECARGA: ";

            lblInfoNome.Text = personagem.Habilidades[i].Nome;
            txtInfoPersonagem.Text = personagem.Habilidades[i].Descricao;
            picHabInfoSelecionada.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i].Foto));
            lblRecarga.Text += personagem.Habilidades[i].Recarga == 1 ? $"{personagem.Habilidades[i].Recarga} TURNO" : $"{personagem.Habilidades[i].Recarga} TURNOS";
            CarregaPicEnergias(personagem.Habilidades[i]);
        }

        private void CarregaInformacoesHab4(Personagem personagem)
        {
            int i = 3;
            lblRecarga.Text = "TEMPO DE RECARGA: ";

            lblInfoNome.Text = personagem.Habilidades[i].Nome;
            txtInfoPersonagem.Text = personagem.Habilidades[i].Descricao;
            picHabInfoSelecionada.Image = Image.FromStream(new MemoryStream(personagem.Habilidades[i].Foto));
            lblRecarga.Text += personagem.Habilidades[i].Recarga == 1 ? $"{personagem.Habilidades[i].Recarga} TURNO" : $"{personagem.Habilidades[i].Recarga} TURNOS";
            CarregaPicEnergias(personagem.Habilidades[i]);
        }

        private void CarregaPicEnergias(HabilidadePersonagem hab)
        {
            int verdes = hab.EnergiaVerde.Quantidade;
            int azuis = hab.EnergiaAzul.Quantidade;
            int vermelhos = hab.EnergiaVermelho.Quantidade;
            int pretos = hab.EnergiaPreto.Quantidade;

            int totalEnergias = verdes + azuis + vermelhos + pretos;

            lblEnergia.Text = "ENERGIA:";

            for (int i = 0; i < infoPics.Length; i++)
            {
                infoPics[i].Visible = false;
            }

            if (totalEnergias > 0)
            {
                for (int i = 0; i < verdes; i++)
                {
                    infoPics[i].Image = Properties.Resources.Verde;
                }
                for (int i = 0; i < azuis; i++)
                {
                    infoPics[(verdes + i)].Image = Properties.Resources.Azul;
                }
                for (int i = 0; i < vermelhos; i++)
                {
                    infoPics[(verdes + azuis + i)].Image = Properties.Resources.Vermelho;
                }
                for (int i = 0; i < pretos; i++)
                {
                    infoPics[(verdes + azuis + vermelhos + i)].Image = Properties.Resources.Preto;
                }

                #region Deixar os Pic de Energia visiveis ou não

                switch (totalEnergias)
                {
                    case 1:
                        picEnergia1.Visible = true;
                        break;
                    case 2:
                        picEnergia1.Visible =
                        picEnergia2.Visible = true;
                        break;
                    case 3:
                        picEnergia1.Visible =
                        picEnergia2.Visible =
                        picEnergia3.Visible = true;
                        break;
                    case 4:
                        picEnergia1.Visible =
                        picEnergia2.Visible =
                        picEnergia3.Visible =
                        picEnergia4.Visible = true;
                        break;
                    case 5:
                        picEnergia1.Visible =
                        picEnergia2.Visible =
                        picEnergia3.Visible =
                        picEnergia4.Visible =
                        picEnergia5.Visible = true;
                        break;
                    default:
                        throw new Exception("Hab1 com erro!");
                }
                #endregion
            }
            else
            {
                lblEnergia.Text += " NENHUMA";
            }
        }

        #endregion                
    }
}
