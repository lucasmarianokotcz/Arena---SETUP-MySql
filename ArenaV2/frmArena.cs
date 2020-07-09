using Model.Arena.Regras.Classes;
using Model.Monstro;
using Model.Personagem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ArenaV2
{
    public partial class frmArena : Form
    {
        #region Atributos

        Intermediario.Arena Arena;
        private List<Personagem> lstPersonagens;
        private List<Monstro> lstMonstros;
        private bool habFoiCompletada = false;
        #region Bitmaps (Personagem/Monstro)
        private Bitmap bmpFotoPersonagem1Disponivel, bmpFotoPersonagem1Indisponivel, bmpFotoPersonagem1Hab1Indisponivel, bmpFotoPersonagem1Hab2Indisponivel, bmpFotoPersonagem1Hab3Indisponivel, bmpFotoPersonagem1Hab4Indisponivel;
        private Bitmap bmpFotoPersonagem2Disponivel, bmpFotoPersonagem2Indisponivel, bmpFotoPersonagem2Hab1Indisponivel, bmpFotoPersonagem2Hab2Indisponivel, bmpFotoPersonagem2Hab3Indisponivel, bmpFotoPersonagem2Hab4Indisponivel;
        private Bitmap bmpFotoPersonagem3Disponivel, bmpFotoPersonagem3Indisponivel, bmpFotoPersonagem3Hab1Indisponivel, bmpFotoPersonagem3Hab2Indisponivel, bmpFotoPersonagem3Hab3Indisponivel, bmpFotoPersonagem3Hab4Indisponivel;
        private Bitmap bmpFotoMonstro1Disponivel, bmpFotoMonstro1Indisponivel, bmpFotoMonstro1Hab1Indisponivel, bmpFotoMonstro1Hab2Indisponivel, bmpFotoMonstro1Hab3Indisponivel, bmpFotoMonstro1Hab4Indisponivel;
        private Bitmap bmpFotoMonstro2Disponivel, bmpFotoMonstro2Indisponivel, bmpFotoMonstro2Hab1Indisponivel, bmpFotoMonstro2Hab2Indisponivel, bmpFotoMonstro2Hab3Indisponivel, bmpFotoMonstro2Hab4Indisponivel;
        private Bitmap bmpFotoMonstro3Disponivel, bmpFotoMonstro3Indisponivel, bmpFotoMonstro3Hab1Indisponivel, bmpFotoMonstro3Hab2Indisponivel, bmpFotoMonstro3Hab3Indisponivel, bmpFotoMonstro3Hab4Indisponivel;
        Image imgPersonagemHabUsada, imgMonstroHabUsada;
        #endregion
        #region Informações da Partida
        PictureBox[] groupBoxInformacoesPics = new PictureBox[5];
        PictureBox[] personagemInformacoesPics = new PictureBox[8];
        PictureBox[] monstroInformacoesPics = new PictureBox[8];
        #endregion
        #region ToolTip
        string[,] personagemInfoHabs = new string[8, 3];
        string[,] monstroInfoHabs = new string[8, 3];
        #endregion
        Thread t;
        int EnergiaVerde = 0, EnergiaAzul = 0, EnergiaVermelha = 0, EnergiaPreta = 0;
        bool Hab1PodeUsar = false, Hab2PodeUsar = false, Hab3PodeUsar = false, Hab4PodeUsar = false;
        HabilidadePersonagem habilidadePersonagemSelecionada;
        Personagem personagemSelecionado;
        string alvo;
        HabilidadeMonstro monstroHabUsada;
        string monstroAlvo;
        bool MonstroInvulneravel;
        bool PassandoTempo = false;
        Monstro monstroSelecionado;
        private int Tempo;

        #endregion

        #region Event Methods

        public frmArena(List<Personagem> lstPersonagens, List<Monstro> lstMonstros)
        {
            InitializeComponent();

            this.lstPersonagens = lstPersonagens;
            this.lstMonstros = lstMonstros;
            Arena = new Intermediario.Arena();

            prgBarTempo.Maximum = ArenaRegras.TempoMaxPassando;
        }

        private void frmArena_Load(object sender, EventArgs e)
        {
            SetarPicturesBoxesInformacoes();
            SetarImagensPersonagens();
            SetarImagensMonstros();
            DefineQuemIniciaAPartida();
        }

        //A cada 500ms do timer, diminui 1 value do prgbar. Quando chega no tempo desejado, Para o timer, ativa botões de pronto e Chama a função FazerMonstro.
        private void Timer_Tick(object sender, EventArgs e)
        {
            prgBarTempo.Value -= 1;

            if (prgBarTempo.Value == (prgBarTempo.Maximum - Tempo))
            {
                Timer.Stop();
                PassandoTempo = false;
                btnPronto.Enabled = true;
                btnTrocarEnergia.Enabled = true;
                prgBarTempo.Value = prgBarTempo.Maximum;
                FazerMonstro();
            }
        }

        #region Double Click
        private void picHabEscolhida_DoubleClick(object sender, EventArgs e) //Evento DoubleClick no picHabEscolhida. Essa função cancela a seleção de habilidade. 
        {
            int i = 0;
            SetarFotosPersonagem1(i++);
            SetarFotosPersonagem2(i++);
            SetarFotosPersonagem3(i++);
            CarregarInformacoesDefault();
        }
        #endregion

        #region Click
        private void picVerHabs_Click(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "btnVerHabilidadesInimigo")
            {
                picInfoHabSelecionada.Image = ConverteFoto(monstroSelecionado.Foto);

                btnVerHabilidadesInimigo.Visible = false;
                txtInfoDescricao.Visible = false;

                int i = 0;
                picVerHab1.Image = ConverteFoto(monstroSelecionado.Habilidades[i++].Foto);
                picVerHab2.Image = ConverteFoto(monstroSelecionado.Habilidades[i++].Foto);
                picVerHab3.Image = ConverteFoto(monstroSelecionado.Habilidades[i++].Foto);
                picVerHab4.Image = ConverteFoto(monstroSelecionado.Habilidades[i++].Foto);

                picVerHab1.Visible = true;
                picVerHab2.Visible = true;
                picVerHab3.Visible = true;
                picVerHab4.Visible = true;

                lblRecarga.Visible = false;
                lblInfoNome.Text = monstroSelecionado.Nome;
            }
            else
            {
                btnVerHabilidadesInimigo.Visible = true;
                btnVerHabilidadesInimigo.Text = "Voltar";
                txtInfoDescricao.Visible = true;

                picVerHab1.Visible = false;
                picVerHab2.Visible = false;
                picVerHab3.Visible = false;
                picVerHab4.Visible = false;

                CarregarInformacoes();

                if (((Control)sender).Name == "picVerHab1")
                {
                    CarregarInformacoesHabilidadeMonstro(monstroSelecionado.Habilidades[0]);
                }
                else if (((Control)sender).Name == "picVerHab2")
                {
                    CarregarInformacoesHabilidadeMonstro(monstroSelecionado.Habilidades[1]);
                }
                else if (((Control)sender).Name == "picVerHab3")
                {
                    CarregarInformacoesHabilidadeMonstro(monstroSelecionado.Habilidades[2]);
                }
                else if (((Control)sender).Name == "picVerHab4")
                {
                    CarregarInformacoesHabilidadeMonstro(monstroSelecionado.Habilidades[3]);
                }
            }
        }
        private void btnPronto_Click(object sender, EventArgs e)	//Evento click do btnPronto. Carrega localmente o número de energias da habilidade usada. Instancia o frmTurno. Se foram usados pretos, diminui as energias escolhidas. Diminui o número de turnos invulneráveis do monstro, diminui o cds do Personagem, chama a função UsarHabilidade, SetarEnergia, VerificaJogoAcabou e PassarTempo. 
        {
            int AleatoriosHab = 0;
            int VerdesHab = 0;
            int AzuisHab = 0;
            int VermelhosHab = 0;
            if (habFoiCompletada)
            {
                AleatoriosHab = habilidadePersonagemSelecionada.EnergiaPreto.Quantidade;
                if (habilidadePersonagemSelecionada != null)
                {
                    VerdesHab = habilidadePersonagemSelecionada.EnergiaVerde.Quantidade;
                    AzuisHab = habilidadePersonagemSelecionada.EnergiaAzul.Quantidade;
                    VermelhosHab = habilidadePersonagemSelecionada.EnergiaVermelho.Quantidade;
                }
            }

            //frmTurno Turno = new frmTurno(imgPersonagemHabUsada, ConverteFoto(personagem.Foto), ConverteFoto(monstro.Foto), alvo, "Personagem", AleatoriosHab, (Arena.RetornaVerdes() - VerdesHab), (Arena.RetornaAzuls() - AzuisHab), (Arena.RetornaVermelhos() - VermelhosHab));

            //if (Turno.ShowDialog() == DialogResult.Yes)
            //{
            //    if (Turno.HabAleatorios >= 0)
            //    {
            //        Arena.TirarEnergiaVerde(Turno.RetornaVerdesGastos());
            //        Arena.TirarEnergiaAzul(Turno.RetornaAzuisGastos());
            //        Arena.TirarEnergiaVermelha(Turno.RetornaVermelhosGastos());
            //    }

            //    Arena.DiminuirHabilidadesPorTurno();
            //    Arena.DiminuirCDSPersonagem();
            //    UsarHabilidade(habilidadaUsadaPersonagem);
            //    UsarHabilidadesPorTurno();
            //    SetarEnergia();
            //    VerificaJogoAcabou();
            //    PassarTempo();


            //    lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0).ToString();
            //}
        }
        private void btnTrocarEnergia_Click(object sender, EventArgs e) //Evento click do btnTrocarEnergia. Carrega localmente o número de energias que o personagem tem, e se tem o número mínimo para a troca, inicia o frmTurno. Tira as energias gastas e coloca as energias ganhas. No fim, chama a função SetarEnegia. 
        {
            int Verdes = Arena.RetornaVerdes();
            int Azuis = Arena.RetornaAzuls();
            int Vermelhos = Arena.RetornaVermelhos();

            if (Verdes >= ArenaRegras.EnergiasIguaisMinimasParaTroca || Azuis >= ArenaRegras.EnergiasIguaisMinimasParaTroca || Vermelhos >= ArenaRegras.EnergiasIguaisMinimasParaTroca)
            {
                //frmTurno Turno = new frmTurno("TrocarEnergia", "Personagem", Verdes, Azuis, Vermelhos);
                //if (Turno.ShowDialog() == DialogResult.Yes)
                //{
                //    Arena.TirarEnergiaVerde(Turno.RetornaVerdesGastos());
                //    Arena.TirarEnergiaAzul(Turno.RetornaAzuisGastos());
                //    Arena.TirarEnergiaVermelha(Turno.RetornaVermelhosGastos());

                //    Arena.PorEnergiaVerde(Turno.RetornaVerdesGanhos());
                //    Arena.PorEnergiaAzul(Turno.RetornaAzuisGanhos());
                //    Arena.PorEnergiaVermelha(Turno.RetornaVermelhosGanhos());

                //    SetarEnergia();
                //}
            }
            else
            {
                MessageBox.Show("Você precisa ter no mínimo " + ArenaRegras.EnergiasIguaisMinimasParaTroca.ToString() + " Energias iguais para trocar!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Personagem 1
        private void picPersonagem1_Click(object sender, EventArgs e)
        {
            personagemSelecionado = lstPersonagens[0];
            ShowInfoPersonagem(personagemSelecionado);
        }
        private void picHab1Personagem1_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[0].Habilidades[0];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab2Personagem1_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[0].Habilidades[1];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab3Personagem1_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[0].Habilidades[2];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab4Personagem1_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[0].Habilidades[3];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        #endregion
        #region Personagem 2
        private void picPersonagem2_Click(object sender, EventArgs e)
        {
            personagemSelecionado = lstPersonagens[1];
            ShowInfoPersonagem(personagemSelecionado);
        }
        private void picHab1Personagem2_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[1].Habilidades[0];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab2Personagem2_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[1].Habilidades[1];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab3Personagem2_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[1].Habilidades[2];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab4Personagem2_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[1].Habilidades[3];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        #endregion
        #region Personagem 3
        private void picPersonagem3_Click(object sender, EventArgs e)
        {
            personagemSelecionado = lstPersonagens[2];
            ShowInfoPersonagem(personagemSelecionado);
        }
        private void picHab1Personagem3_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[2].Habilidades[0];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab2Personagem3_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[2].Habilidades[1];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab3Personagem3_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[2].Habilidades[2];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        private void picHab4Personagem3_Click(object sender, EventArgs e)
        {
            habilidadePersonagemSelecionada = lstPersonagens[2].Habilidades[3];
            ShowInfoHabilidadePersonagem(habilidadePersonagemSelecionada);
        }
        #endregion

        #region Monstro 1
        private void picMonstro1_Click(object sender, EventArgs e)
        {
            monstroSelecionado = lstMonstros[0];
            ShowInfoMonstro(monstroSelecionado);
        }
        #endregion
        #region Monstro 2
        private void picMonstro2_Click(object sender, EventArgs e)
        {
            monstroSelecionado = lstMonstros[1];
            ShowInfoMonstro(monstroSelecionado);
        }
        #endregion
        #region Monstro 3
        private void picMonstro3_Click(object sender, EventArgs e)
        {
            monstroSelecionado = lstMonstros[2];
            ShowInfoMonstro(monstroSelecionado);
        }
        #endregion
        #endregion

        #region FormClosing
        private void frmArena_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer.Stop();
        }
        private void frmArena_FormClosed(object sender, FormClosedEventArgs e)
        {
            Timer.Stop();
        }
        #endregion

        #region MouseHover
        private void picPersonagemInfos_MouseHover(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "picPersonagemInfo1":
                    toolTip1.ToolTipTitle = personagemInfoHabs[0, 1];
                    break;
                case "picPersonagemInfo2":
                    toolTip1.ToolTipTitle = personagemInfoHabs[1, 1];
                    break;
                case "picPersonagemInfo3":
                    toolTip1.ToolTipTitle = personagemInfoHabs[2, 1];
                    break;
                case "picPersonagemInfo4":
                    toolTip1.ToolTipTitle = personagemInfoHabs[3, 1];
                    break;
                case "picPersonagemInfo5":
                    toolTip1.ToolTipTitle = personagemInfoHabs[4, 1];
                    break;
                case "picPersonagemInfo6":
                    toolTip1.ToolTipTitle = personagemInfoHabs[5, 1];
                    break;
                case "picPersonagemInfo7":
                    toolTip1.ToolTipTitle = personagemInfoHabs[6, 1];
                    break;
                case "picPersonagemInfo8":
                    toolTip1.ToolTipTitle = personagemInfoHabs[7, 1];
                    break;
                case "lblPersonagemInfoInfinitas":
                    toolTip1.ToolTipTitle = "Muitas informações...";
                    break;
            }
        }
        private void picMonstroInfos_MouseHover(object sender, EventArgs e)
        {
            switch (((Control)sender).Name)
            {
                case "picMonstroInfo1":
                    toolTip1.ToolTipTitle = monstroInfoHabs[0, 0];
                    break;
                case "picMonstroInfo2":
                    toolTip1.ToolTipTitle = monstroInfoHabs[1, 0];
                    break;
                case "picMonstroInfo3":
                    toolTip1.ToolTipTitle = monstroInfoHabs[2, 0];
                    break;
                case "picMonstroInfo4":
                    toolTip1.ToolTipTitle = monstroInfoHabs[3, 0];
                    break;
                case "picMonstroInfo5":
                    toolTip1.ToolTipTitle = monstroInfoHabs[4, 0];
                    break;
                case "picMonstroInfo6":
                    toolTip1.ToolTipTitle = monstroInfoHabs[5, 0];
                    break;
                case "picMonstroInfo7":
                    toolTip1.ToolTipTitle = monstroInfoHabs[6, 0];
                    break;
                case "picMonstroInfo8":
                    toolTip1.ToolTipTitle = monstroInfoHabs[7, 0];
                    break;
                case "lblMonstroInfoInfinitas":
                    toolTip1.ToolTipTitle = "Muitas informações...";
                    break;
            }
        }
        #endregion

        #endregion

        #region Private Methods

        private void ShowInfoPersonagem(Personagem personagem)
        {
            btnVerHabilidadesInimigo.Visible = false;
            btnVerHabilidadesInimigo.Text = "Ver Habilidades";
            picVerHab1.Visible = picVerHab2.Visible = picVerHab3.Visible = picVerHab4.Visible = false;
            txtInfoDescricao.Visible = true;

            // Não sei pq serve ainda, vou comentar por enquanto
            //if (!personagem.IsInvulneravel)
            //{
            //    picPersonagem1.Image = ConverteFoto(personagem.Foto);
            //}

            CarregarInformacoes();
            CarregarInformacoesPersonagem(personagem);
        }

        private void ShowInfoHabilidadePersonagem(HabilidadePersonagem hab)
        {
            btnVerHabilidadesInimigo.Visible = false;
            btnVerHabilidadesInimigo.Text = "Ver Habilidades";
            picVerHab1.Visible = picVerHab2.Visible = picVerHab3.Visible = picVerHab4.Visible = false;
            lblInfoNome.Text = hab.Nome;
            txtInfoDescricao.Text = hab.Descricao;
            txtInfoDescricao.Visible = true;
            lblRecarga.Text = "TEMPO DE RECARGA: ";
            lblRecarga.Text += hab.Recarga == 1 ? $"{hab.Recarga} TURNO" : $"{hab.Recarga} TURNOS";
            lblRecarga.Visible = hab.Recarga > 0;
            picInfoHabSelecionada.Image = ConverteFoto(hab.Foto);
            CarregaPicsEnergiaPersonagem(hab);
        }

        private void ShowInfoMonstro(Monstro monstro)
        {
            btnVerHabilidadesInimigo.Visible = true;
            btnVerHabilidadesInimigo.Text = "Ver Habilidades";
            picVerHab1.Visible = picVerHab2.Visible = picVerHab3.Visible = picVerHab4.Visible = false;
            txtInfoDescricao.Visible = true;

            // Não sei pq serve ainda, vou comentar por enquanto
            //if (!personagem.IsInvulneravel)
            //{
            //    picPersonagem1.Image = ConverteFoto(personagem.Foto);
            //}

            CarregarInformacoes();
            CarregarInformacoesMonstro(monstro);
        }

        private void ShowInfoHabilidadeMonstro(HabilidadeMonstro hab)
        {
            btnVerHabilidadesInimigo.Visible = false;
            btnVerHabilidadesInimigo.Text = "Ver Habilidades";
            picVerHab1.Visible = picVerHab2.Visible = picVerHab3.Visible = picVerHab4.Visible = false;
            lblInfoNome.Text = hab.Nome;
            txtInfoDescricao.Text = hab.Descricao;
            lblRecarga.Text = "TEMPO DE RECARGA: ";
            lblRecarga.Text += hab.Recarga == 1 ? $"{hab.Recarga} TURNO" : $"{hab.Recarga} TURNOS";
            lblRecarga.Visible = hab.Recarga > 0;
            picInfoHabSelecionada.Image = ConverteFoto(hab.Foto);
        }

        private void DefineQuemIniciaAPartida()
        {
            int chanceDeComecarAPartida = new Random().Next(0, ArenaRegras.ChanceDeComecar);
            if (chanceDeComecarAPartida != 0)
            {
                // Personagem começa.
                GerarEnergiaAleatoria();
            }
            else
            {
                // Monstro começa.
                PassarTempo();
            }
        }

        #region Setar Imagens
        private void SetarImagensPersonagens()
        {
            int i = 0;
            picPersonagem1.Image = ConverteFoto(lstPersonagens[i++].Foto);
            picPersonagem2.Image = ConverteFoto(lstPersonagens[i++].Foto);
            picPersonagem3.Image = ConverteFoto(lstPersonagens[i++].Foto);
            i = 0;
            SetarBitmapsPersonagem1(i++);
            SetarBitmapsPersonagem2(i++);
            SetarBitmapsPersonagem3(i++);
            i = 0;
            SetarFotosPersonagem1(i++);
            SetarFotosPersonagem2(i++);
            SetarFotosPersonagem3(i++);
            SetarPicturesBoxesInformacoesPersonagem();
        }
        private void SetarImagensMonstros()
        {
            int i = 0;
            picMonstro1.Image = ConverteFoto(lstMonstros[i++].Foto);
            picMonstro2.Image = ConverteFoto(lstMonstros[i++].Foto);
            picMonstro3.Image = ConverteFoto(lstMonstros[i++].Foto);
            SetarPicturesBoxesInformacoesMonstro();
            i = 0;
            SetarBitmapsMonstro1(i++);
            SetarBitmapsMonstro2(i++);
            SetarBitmapsMonstro3(i++);
            i = 0;
            SetarFotosMonstro1(i++);
            SetarFotosMonstro2(i++);
            SetarFotosMonstro3(i++);
        }
        #endregion

        #region Setar PictureBoxes
        private void SetarPicturesBoxesInformacoes()
        {
            groupBoxInformacoesPics[0] = picInfoEnergia1;
            groupBoxInformacoesPics[1] = picInfoEnergia2;
            groupBoxInformacoesPics[2] = picInfoEnergia3;
            groupBoxInformacoesPics[3] = picInfoEnergia4;
            groupBoxInformacoesPics[4] = picInfoEnergia5;
        }
        private void SetarPicturesBoxesInformacoesPersonagem()
        {
            personagemInformacoesPics[0] = picPersonagem1Info;
            personagemInformacoesPics[1] = picPersonagem2Info;
            personagemInformacoesPics[2] = picPersonagem3Info;
        }
        private void SetarPicturesBoxesInformacoesMonstro()
        {
            monstroInformacoesPics[0] = picMonstro1Info;
            monstroInformacoesPics[1] = picMonstro2Info;
            monstroInformacoesPics[2] = picMonstro3Info;
        }
        #endregion

        #region Setar Bitmaps
        private void SetarBitmapsPersonagem1(int j)
        {
            bmpFotoPersonagem1Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Foto));
            bmpFotoPersonagem1Disponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Foto));
            int i = 0;
            bmpFotoPersonagem1Hab1Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem1Hab2Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem1Hab3Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem1Hab4Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Personagem_Indisponivel
            for (int Width = 0; Width < bmpFotoPersonagem1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem1Indisponivel.GetPixel(Width, Height);

                    //int r, g, b;
                    #region R
                    //if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    //{
                    //    r = CorAnterior.R + 15;
                    //}
                    //else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    //{
                    //    r = CorAnterior.R + 25;
                    //}
                    //else if (CorAnterior.R + 40 <= 255)
                    //{
                    //    r = CorAnterior.R + 40;
                    //}
                    //else
                    //{
                    //    r = 255;
                    //}
                    #endregion
                    #region G
                    //if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    //{
                    //    g = CorAnterior.G + 15;
                    //}
                    //else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    //{
                    //    g = CorAnterior.G + 25;
                    //}
                    //else if (CorAnterior.G + 40 <= 255)
                    //{
                    //    g = CorAnterior.G + 40;
                    //}
                    //else
                    //{
                    //    g = 255;
                    //}
                    #endregion
                    #region B
                    //if (CorAnterior.B < 25)
                    //{
                    //    b = 0;
                    //}
                    //else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    //{
                    //    b = CorAnterior.B - 25;
                    //}
                    //else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    //{
                    //    b = CorAnterior.B - 60;
                    //}
                    //else
                    //{
                    //    b = CorAnterior.B - 110;
                    //}
                    #endregion

                    //CorNova = Color.FromArgb(150, r, g, b);

                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Personagem_Disponivel
            for (int Width = 0; Width < bmpFotoPersonagem1Disponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem1Disponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem1Disponivel.GetPixel(Width, Height);

                    int r, g, b;
                    #region R
                    if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    {
                        r = CorAnterior.R + 15;
                    }
                    else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    {
                        r = CorAnterior.R + 25;
                    }
                    else if (CorAnterior.R + 40 <= 255)
                    {
                        r = CorAnterior.R + 40;
                    }
                    else
                    {
                        r = 255;
                    }
                    #endregion
                    #region G
                    if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    {
                        g = CorAnterior.G + 15;
                    }
                    else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    {
                        g = CorAnterior.G + 25;
                    }
                    else if (CorAnterior.G + 40 <= 255)
                    {
                        g = CorAnterior.G + 40;
                    }
                    else
                    {
                        g = 255;
                    }
                    #endregion
                    #region B
                    if (CorAnterior.B < 25)
                    {
                        b = 0;
                    }
                    else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    {
                        b = CorAnterior.B - 25;
                    }
                    else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    {
                        b = CorAnterior.B - 60;
                    }
                    else
                    {
                        b = CorAnterior.B - 110;
                    }
                    #endregion

                    CorNova = Color.FromArgb(CorAnterior.A, r, g, b);

                    bmpFotoPersonagem1Disponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Hab1
            for (int Width = 0; Width < bmpFotoPersonagem1Hab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem1Hab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem1Hab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem1Hab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab2
            for (int Width = 0; Width < bmpFotoPersonagem1Hab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem1Hab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem1Hab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem1Hab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab3
            for (int Width = 0; Width < bmpFotoPersonagem1Hab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem1Hab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem1Hab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem1Hab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab4
            for (int Width = 0; Width < bmpFotoPersonagem1Hab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem1Hab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem1Hab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem1Hab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        private void SetarBitmapsPersonagem2(int j)
        {
            bmpFotoPersonagem2Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Foto));
            bmpFotoPersonagem2Disponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Foto));
            int i = 0;
            bmpFotoPersonagem2Hab1Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem2Hab2Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem2Hab3Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem2Hab4Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Personagem_Indisponivel
            for (int Width = 0; Width < bmpFotoPersonagem2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem2Indisponivel.GetPixel(Width, Height);

                    //int r, g, b;
                    #region R
                    //if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    //{
                    //    r = CorAnterior.R + 15;
                    //}
                    //else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    //{
                    //    r = CorAnterior.R + 25;
                    //}
                    //else if (CorAnterior.R + 40 <= 255)
                    //{
                    //    r = CorAnterior.R + 40;
                    //}
                    //else
                    //{
                    //    r = 255;
                    //}
                    #endregion
                    #region G
                    //if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    //{
                    //    g = CorAnterior.G + 15;
                    //}
                    //else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    //{
                    //    g = CorAnterior.G + 25;
                    //}
                    //else if (CorAnterior.G + 40 <= 255)
                    //{
                    //    g = CorAnterior.G + 40;
                    //}
                    //else
                    //{
                    //    g = 255;
                    //}
                    #endregion
                    #region B
                    //if (CorAnterior.B < 25)
                    //{
                    //    b = 0;
                    //}
                    //else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    //{
                    //    b = CorAnterior.B - 25;
                    //}
                    //else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    //{
                    //    b = CorAnterior.B - 60;
                    //}
                    //else
                    //{
                    //    b = CorAnterior.B - 110;
                    //}
                    #endregion

                    //CorNova = Color.FromArgb(150, r, g, b);

                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Personagem_Disponivel
            for (int Width = 0; Width < bmpFotoPersonagem2Disponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem2Disponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem2Disponivel.GetPixel(Width, Height);

                    int r, g, b;
                    #region R
                    if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    {
                        r = CorAnterior.R + 15;
                    }
                    else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    {
                        r = CorAnterior.R + 25;
                    }
                    else if (CorAnterior.R + 40 <= 255)
                    {
                        r = CorAnterior.R + 40;
                    }
                    else
                    {
                        r = 255;
                    }
                    #endregion
                    #region G
                    if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    {
                        g = CorAnterior.G + 15;
                    }
                    else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    {
                        g = CorAnterior.G + 25;
                    }
                    else if (CorAnterior.G + 40 <= 255)
                    {
                        g = CorAnterior.G + 40;
                    }
                    else
                    {
                        g = 255;
                    }
                    #endregion
                    #region B
                    if (CorAnterior.B < 25)
                    {
                        b = 0;
                    }
                    else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    {
                        b = CorAnterior.B - 25;
                    }
                    else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    {
                        b = CorAnterior.B - 60;
                    }
                    else
                    {
                        b = CorAnterior.B - 110;
                    }
                    #endregion

                    CorNova = Color.FromArgb(CorAnterior.A, r, g, b);

                    bmpFotoPersonagem2Disponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Hab1
            for (int Width = 0; Width < bmpFotoPersonagem2Hab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem2Hab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem2Hab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem2Hab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab2
            for (int Width = 0; Width < bmpFotoPersonagem2Hab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem2Hab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem2Hab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem2Hab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab3
            for (int Width = 0; Width < bmpFotoPersonagem2Hab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem2Hab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem2Hab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem2Hab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab4
            for (int Width = 0; Width < bmpFotoPersonagem2Hab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem2Hab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem2Hab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem2Hab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        private void SetarBitmapsPersonagem3(int j)
        {
            bmpFotoPersonagem3Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Foto));
            bmpFotoPersonagem3Disponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Foto));
            int i = 0;
            bmpFotoPersonagem3Hab1Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem3Hab2Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem3Hab3Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));
            bmpFotoPersonagem3Hab4Indisponivel = new Bitmap(ConverteFoto(lstPersonagens[j].Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Personagem_Indisponivel
            for (int Width = 0; Width < bmpFotoPersonagem3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem3Indisponivel.GetPixel(Width, Height);

                    //int r, g, b;
                    #region R
                    //if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    //{
                    //    r = CorAnterior.R + 15;
                    //}
                    //else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    //{
                    //    r = CorAnterior.R + 25;
                    //}
                    //else if (CorAnterior.R + 40 <= 255)
                    //{
                    //    r = CorAnterior.R + 40;
                    //}
                    //else
                    //{
                    //    r = 255;
                    //}
                    #endregion
                    #region G
                    //if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    //{
                    //    g = CorAnterior.G + 15;
                    //}
                    //else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    //{
                    //    g = CorAnterior.G + 25;
                    //}
                    //else if (CorAnterior.G + 40 <= 255)
                    //{
                    //    g = CorAnterior.G + 40;
                    //}
                    //else
                    //{
                    //    g = 255;
                    //}
                    #endregion
                    #region B
                    //if (CorAnterior.B < 25)
                    //{
                    //    b = 0;
                    //}
                    //else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    //{
                    //    b = CorAnterior.B - 25;
                    //}
                    //else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    //{
                    //    b = CorAnterior.B - 60;
                    //}
                    //else
                    //{
                    //    b = CorAnterior.B - 110;
                    //}
                    #endregion

                    //CorNova = Color.FromArgb(150, r, g, b);

                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Personagem_Disponivel
            for (int Width = 0; Width < bmpFotoPersonagem3Disponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem3Disponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem3Disponivel.GetPixel(Width, Height);

                    int r, g, b;
                    #region R
                    if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    {
                        r = CorAnterior.R + 15;
                    }
                    else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    {
                        r = CorAnterior.R + 25;
                    }
                    else if (CorAnterior.R + 40 <= 255)
                    {
                        r = CorAnterior.R + 40;
                    }
                    else
                    {
                        r = 255;
                    }
                    #endregion
                    #region G
                    if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    {
                        g = CorAnterior.G + 15;
                    }
                    else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    {
                        g = CorAnterior.G + 25;
                    }
                    else if (CorAnterior.G + 40 <= 255)
                    {
                        g = CorAnterior.G + 40;
                    }
                    else
                    {
                        g = 255;
                    }
                    #endregion
                    #region B
                    if (CorAnterior.B < 25)
                    {
                        b = 0;
                    }
                    else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    {
                        b = CorAnterior.B - 25;
                    }
                    else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    {
                        b = CorAnterior.B - 60;
                    }
                    else
                    {
                        b = CorAnterior.B - 110;
                    }
                    #endregion

                    CorNova = Color.FromArgb(CorAnterior.A, r, g, b);

                    bmpFotoPersonagem3Disponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Hab1
            for (int Width = 0; Width < bmpFotoPersonagem3Hab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem3Hab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem3Hab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem3Hab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab2
            for (int Width = 0; Width < bmpFotoPersonagem3Hab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem3Hab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem3Hab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem3Hab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab3
            for (int Width = 0; Width < bmpFotoPersonagem3Hab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem3Hab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem3Hab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem3Hab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab4
            for (int Width = 0; Width < bmpFotoPersonagem3Hab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagem3Hab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagem3Hab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagem3Hab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        private void SetarBitmapsMonstro1(int j)
        {
            bmpFotoMonstro1Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Foto));
            bmpFotoMonstro1Disponivel = new Bitmap(ConverteFoto(lstMonstros[j].Foto));
            int i = 0;
            bmpFotoMonstro1Hab1Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro1Hab2Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro1Hab3Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro1Hab4Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Monstro_Indisponivel
            for (int Width = 0; Width < bmpFotoMonstro1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Disponivel
            for (int Width = 0; Width < bmpFotoMonstro1Disponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro1Disponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro1Disponivel.GetPixel(Width, Height);

                    int r, g, b;
                    #region R
                    if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    {
                        r = CorAnterior.R + 15;
                    }
                    else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    {
                        r = CorAnterior.R + 25;
                    }
                    else if (CorAnterior.R + 40 <= 255)
                    {
                        r = CorAnterior.R + 40;
                    }
                    else
                    {
                        r = 255;
                    }
                    #endregion
                    #region G
                    if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    {
                        g = CorAnterior.G + 15;
                    }
                    else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    {
                        g = CorAnterior.G + 25;
                    }
                    else if (CorAnterior.G + 40 <= 255)
                    {
                        g = CorAnterior.G + 40;
                    }
                    else
                    {
                        g = 255;
                    }
                    #endregion
                    #region B
                    if (CorAnterior.B < 25)
                    {
                        b = 0;
                    }
                    else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    {
                        b = CorAnterior.B - 25;
                    }
                    else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    {
                        b = CorAnterior.B - 60;
                    }
                    else
                    {
                        b = CorAnterior.B - 110;
                    }
                    #endregion

                    CorNova = Color.FromArgb(CorAnterior.A, r, g, b);

                    bmpFotoMonstro1Disponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Monstro_Hab1
            for (int Width = 0; Width < bmpFotoMonstro1Hab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro1Hab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro1Hab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro1Hab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab2
            for (int Width = 0; Width < bmpFotoMonstro1Hab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro1Hab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro1Hab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro1Hab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab3
            for (int Width = 0; Width < bmpFotoMonstro1Hab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro1Hab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro1Hab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro1Hab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab4
            for (int Width = 0; Width < bmpFotoMonstro1Hab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro1Hab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro1Hab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro1Hab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        private void SetarBitmapsMonstro2(int j)
        {
            bmpFotoMonstro2Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Foto));
            bmpFotoMonstro2Disponivel = new Bitmap(ConverteFoto(lstMonstros[j].Foto));
            int i = 0;
            bmpFotoMonstro2Hab1Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro2Hab2Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro2Hab3Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro2Hab4Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Monstro_Indisponivel
            for (int Width = 0; Width < bmpFotoMonstro2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Disponivel
            for (int Width = 0; Width < bmpFotoMonstro2Disponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro2Disponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro2Disponivel.GetPixel(Width, Height);

                    int r, g, b;
                    #region R
                    if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    {
                        r = CorAnterior.R + 15;
                    }
                    else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    {
                        r = CorAnterior.R + 25;
                    }
                    else if (CorAnterior.R + 40 <= 255)
                    {
                        r = CorAnterior.R + 40;
                    }
                    else
                    {
                        r = 255;
                    }
                    #endregion
                    #region G
                    if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    {
                        g = CorAnterior.G + 15;
                    }
                    else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    {
                        g = CorAnterior.G + 25;
                    }
                    else if (CorAnterior.G + 40 <= 255)
                    {
                        g = CorAnterior.G + 40;
                    }
                    else
                    {
                        g = 255;
                    }
                    #endregion
                    #region B
                    if (CorAnterior.B < 25)
                    {
                        b = 0;
                    }
                    else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    {
                        b = CorAnterior.B - 25;
                    }
                    else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    {
                        b = CorAnterior.B - 60;
                    }
                    else
                    {
                        b = CorAnterior.B - 110;
                    }
                    #endregion

                    CorNova = Color.FromArgb(CorAnterior.A, r, g, b);

                    bmpFotoMonstro2Disponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Monstro_Hab1
            for (int Width = 0; Width < bmpFotoMonstro2Hab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro2Hab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro2Hab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro2Hab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab2
            for (int Width = 0; Width < bmpFotoMonstro2Hab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro2Hab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro2Hab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro2Hab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab3
            for (int Width = 0; Width < bmpFotoMonstro2Hab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro2Hab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro2Hab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro2Hab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab4
            for (int Width = 0; Width < bmpFotoMonstro2Hab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro2Hab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro2Hab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro2Hab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        private void SetarBitmapsMonstro3(int j)
        {
            bmpFotoMonstro3Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Foto));
            bmpFotoMonstro3Disponivel = new Bitmap(ConverteFoto(lstMonstros[j].Foto));
            int i = 0;
            bmpFotoMonstro3Hab1Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro3Hab2Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro3Hab3Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));
            bmpFotoMonstro3Hab4Indisponivel = new Bitmap(ConverteFoto(lstMonstros[j].Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Monstro_Indisponivel
            for (int Width = 0; Width < bmpFotoMonstro3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Disponivel
            for (int Width = 0; Width < bmpFotoMonstro3Disponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro3Disponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro3Disponivel.GetPixel(Width, Height);

                    int r, g, b;
                    #region R
                    if (CorAnterior.R >= 0 && CorAnterior.R <= 15)
                    {
                        r = CorAnterior.R + 15;
                    }
                    else if (CorAnterior.R > 15 && CorAnterior.R <= 100)
                    {
                        r = CorAnterior.R + 25;
                    }
                    else if (CorAnterior.R + 40 <= 255)
                    {
                        r = CorAnterior.R + 40;
                    }
                    else
                    {
                        r = 255;
                    }
                    #endregion
                    #region G
                    if (CorAnterior.G >= 0 && CorAnterior.G <= 15)
                    {
                        g = CorAnterior.G + 15;
                    }
                    else if (CorAnterior.G > 15 && CorAnterior.G <= 100)
                    {
                        g = CorAnterior.G + 25;
                    }
                    else if (CorAnterior.G + 40 <= 255)
                    {
                        g = CorAnterior.G + 40;
                    }
                    else
                    {
                        g = 255;
                    }
                    #endregion
                    #region B
                    if (CorAnterior.B < 25)
                    {
                        b = 0;
                    }
                    else if (CorAnterior.B >= 25 && CorAnterior.B <= 100)
                    {
                        b = CorAnterior.B - 25;
                    }
                    else if (CorAnterior.B > 100 && CorAnterior.B <= 250)
                    {
                        b = CorAnterior.B - 60;
                    }
                    else
                    {
                        b = CorAnterior.B - 110;
                    }
                    #endregion

                    CorNova = Color.FromArgb(CorAnterior.A, r, g, b);

                    bmpFotoMonstro3Disponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Monstro_Hab1
            for (int Width = 0; Width < bmpFotoMonstro3Hab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro3Hab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro3Hab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro3Hab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab2
            for (int Width = 0; Width < bmpFotoMonstro3Hab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro3Hab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro3Hab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro3Hab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab3
            for (int Width = 0; Width < bmpFotoMonstro3Hab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro3Hab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro3Hab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro3Hab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab4
            for (int Width = 0; Width < bmpFotoMonstro3Hab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstro3Hab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstro3Hab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstro3Hab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        #endregion

        private void SetarEnergia()     //Função que seta as variáveis locais de energia, além de setar os labels de energia. No fim, chama a função SetarFotos. 
        {
            EnergiaVerde = Arena.RetornaVerdes();
            EnergiaAzul = Arena.RetornaAzuls();
            EnergiaVermelha = Arena.RetornaVermelhos();
            EnergiaPreta = Arena.RetornaPretos();

            lblVerdes.Text = EnergiaVerde.ToString();
            lblAzuls.Text = EnergiaAzul.ToString();
            lblVermelhos.Text = EnergiaVermelha.ToString();
            lblPretos.Text = EnergiaPreta.ToString();

            int i = 0;
            SetarFotosPersonagem1(i++);
            SetarFotosPersonagem2(i++);
            SetarFotosPersonagem3(i++);
            i = 0;
            SetarFotosMonstro1(i++);
            SetarFotosMonstro2(i++);
            SetarFotosMonstro3(i++);
        }

        private void SetarFotosPersonagem1(int j)
        {
            alvo = "";
            //habilidadaUsadaPersonagem = "";
            imgPersonagemHabUsada = null;
            habFoiCompletada = false;

            imgMonstroHabUsada = null;
            //monstroHabUsada = "";

            picPersonagem1.Image = ConverteFoto(lstPersonagens[j].Foto);

            picHab1Personagem1.Visible = true;
            picHab2Personagem1.Visible = true;
            picHab3Personagem1.Visible = true;
            picHab4Personagem1.Visible = true;

            picHabEscolhidaPersonagem1.Image = Properties.Resources.Ponto_de_interrogacao;
            int Verdes, Azuls, Vermelhos, Pretos;

            bool PersonagemRecebendo = false;
            bool MonstroRecebendo = false;

            #region Personagem

            #region PersonagemInvulneravel
            if (Arena.RetornaPersonagemTurnosInvulneravel() > 0)
            {
                //PersonagemInvulneravel = true;
                picPersonagem1.Image = bmpFotoPersonagem1Indisponivel;
            }
            else
            {

            }
            #endregion

            #region PersonagemDanoTurno
            if (Arena.RetornaPersonagemDanoTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemDanoPerfuranteTurno
            if (Arena.RetornaPersonagemDanoPerfuranteTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemDanoVerdadeiroTurno
            if (Arena.RetornaPersonagemDanoVerdadeiroTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemCuraTurno
            if (Arena.RetornaPersonagemCuraTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemArmaduraTurno
            if (Arena.RetornaPersonagemArmaduraTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #endregion

            #region Monstro

            #region MonstroInvulnerável
            if (Arena.RetornaMonstroTurnosInvulneravel() > 0)
            {
                MonstroInvulneravel = true;
                picMonstro1.Image = bmpFotoMonstro1Indisponivel;
            }
            else
            {
                MonstroInvulneravel = false;
            }
            #endregion

            #region MonstroDanoTurno
            if (Arena.RetornaMonstroDanoTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroDanoPerfuranteTurno
            if (Arena.RetornaMonstroDanoPerfuranteTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroDanoVerdadeiroTurno
            if (Arena.RetornaMonstroDanoVerdadeiroTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroCuraTurno
            if (Arena.RetornaMonstroCuraTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroArmaduraTurno
            if (Arena.RetornaMonstroArmaduraTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #endregion;

            //if (MonstroRecebendo == true)
            //{
            //    lblMonstroRecebendo.Visible = true;
            //}
            //else { lblMonstroRecebendo.Visible = false; }
            //if (PersonagemRecebendo == true)
            //{
            //    lblPersonagemRecebendo.Visible = true;
            //}
            //else { lblPersonagemRecebendo.Visible = false; }

            #region Hab1

            Verdes = Arena.RetornaHabVerdes("Hab1");
            Vermelhos = Arena.RetornaHabVermelhos("Hab1");
            Azuls = Arena.RetornaHabAzuls("Hab1");
            Pretos = (Arena.RetornaHabPretos("Hab1") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab1") == 0)
            {
                lblPersonagem1CDHab1.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab1PodeUsar = true;
                    picHab1Personagem1.Image = ConverteFoto(lstPersonagens[j].Habilidades[0].Foto);
                }
                else
                {
                    Hab1PodeUsar = false;
                    picHab1Personagem1.Image = bmpFotoMonstro1Hab1Indisponivel;
                }
            }
            else
            {
                lblPersonagem1CDHab1.Visible = true;
                lblPersonagem1CDHab1.Text = Arena.RetornaPersonagemCD("Hab1").ToString();

                Hab1PodeUsar = false;
                picHab1Personagem1.Image = bmpFotoMonstro1Hab1Indisponivel;
            }
            #endregion
            #region Hab2

            Verdes = Arena.RetornaHabVerdes("Hab2");
            Vermelhos = Arena.RetornaHabVermelhos("Hab2");
            Azuls = Arena.RetornaHabAzuls("Hab2");
            Pretos = (Arena.RetornaHabPretos("Hab2") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab2") == 0)
            {
                lblPersonagem1CDHab2.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab2PodeUsar = true;
                    picHab2Personagem1.Image = ConverteFoto(lstPersonagens[j].Habilidades[1].Foto);
                }
                else
                {
                    Hab2PodeUsar = false;
                    picHab2Personagem1.Image = bmpFotoPersonagem1Hab2Indisponivel;
                }
            }
            else
            {
                lblPersonagem1CDHab2.Visible = true;
                lblPersonagem1CDHab2.Text = Arena.RetornaPersonagemCD("Hab2").ToString();

                Hab2PodeUsar = false;
                picHab2Personagem1.Image = bmpFotoPersonagem1Hab2Indisponivel;
            }
            #endregion
            #region Hab3

            Verdes = Arena.RetornaHabVerdes("Hab3");
            Vermelhos = Arena.RetornaHabVermelhos("Hab3");
            Azuls = Arena.RetornaHabAzuls("Hab3");
            Pretos = (Arena.RetornaHabPretos("Hab3") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab3") == 0)
            {
                lblPersonagem1CDHab3.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab3PodeUsar = true;
                    picHab3Personagem1.Image = ConverteFoto(lstPersonagens[j].Habilidades[2].Foto);
                }
                else
                {
                    Hab3PodeUsar = false;
                    picHab3Personagem1.Image = bmpFotoMonstro1Hab3Indisponivel;
                }
            }
            else
            {
                lblPersonagem1CDHab3.Visible = true;
                lblPersonagem1CDHab3.Text = Arena.RetornaPersonagemCD("Hab3").ToString();

                Hab3PodeUsar = false;
                picHab3Personagem1.Image = bmpFotoMonstro1Hab3Indisponivel;
            }
            #endregion
            #region Hab4

            Verdes = Arena.RetornaHabVerdes("Hab4");
            Vermelhos = Arena.RetornaHabVermelhos("Hab4");
            Azuls = Arena.RetornaHabAzuls("Hab4");
            Pretos = (Arena.RetornaHabPretos("Hab4") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab4") == 0)
            {
                lblPersonagem1CDHab4.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab4PodeUsar = true;
                    picHab4Personagem1.Image = ConverteFoto(lstPersonagens[j].Habilidades[3].Foto);
                }
                else
                {
                    Hab4PodeUsar = false;
                    picHab4Personagem1.Image = bmpFotoMonstro1Hab4Indisponivel;
                }
            }
            else
            {
                lblPersonagem1CDHab4.Visible = true;
                lblPersonagem1CDHab4.Text = Arena.RetornaPersonagemCD("Hab4").ToString();

                Hab4PodeUsar = false;
                picHab4Personagem1.Image = bmpFotoMonstro1Hab4Indisponivel;
            }
            #endregion
        }
        private void SetarFotosPersonagem2(int j)
        {
            alvo = "";
            //habilidadaUsadaPersonagem = "";
            imgPersonagemHabUsada = null;
            habFoiCompletada = false;

            imgMonstroHabUsada = null;
            //monstroHabUsada = "";

            picPersonagem2.Image = ConverteFoto(lstPersonagens[j].Foto);

            picHab1Personagem2.Visible = true;
            picHab2Personagem2.Visible = true;
            picHab3Personagem2.Visible = true;
            picHab4Personagem2.Visible = true;

            picHabEscolhidaPersonagem2.Image = Properties.Resources.Ponto_de_interrogacao;
            int Verdes, Azuls, Vermelhos, Pretos;

            bool PersonagemRecebendo = false;
            bool MonstroRecebendo = false;

            #region Personagem

            #region PersonagemInvulneravel
            if (Arena.RetornaPersonagemTurnosInvulneravel() > 0)
            {
                //PersonagemInvulneravel = true;
                picPersonagem2.Image = bmpFotoPersonagem2Indisponivel;
            }
            else
            {

            }
            #endregion

            #region PersonagemDanoTurno
            if (Arena.RetornaPersonagemDanoTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemDanoPerfuranteTurno
            if (Arena.RetornaPersonagemDanoPerfuranteTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemDanoVerdadeiroTurno
            if (Arena.RetornaPersonagemDanoVerdadeiroTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemCuraTurno
            if (Arena.RetornaPersonagemCuraTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemArmaduraTurno
            if (Arena.RetornaPersonagemArmaduraTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #endregion

            #region Monstro

            #region MonstroInvulnerável
            if (Arena.RetornaMonstroTurnosInvulneravel() > 0)
            {
                MonstroInvulneravel = true;
                picMonstro1.Image = bmpFotoMonstro1Indisponivel;
            }
            else
            {
                MonstroInvulneravel = false;
            }
            #endregion

            #region MonstroDanoTurno
            if (Arena.RetornaMonstroDanoTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroDanoPerfuranteTurno
            if (Arena.RetornaMonstroDanoPerfuranteTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroDanoVerdadeiroTurno
            if (Arena.RetornaMonstroDanoVerdadeiroTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroCuraTurno
            if (Arena.RetornaMonstroCuraTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroArmaduraTurno
            if (Arena.RetornaMonstroArmaduraTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #endregion;

            //if (MonstroRecebendo == true)
            //{
            //    lblMonstroRecebendo.Visible = true;
            //}
            //else { lblMonstroRecebendo.Visible = false; }
            //if (PersonagemRecebendo == true)
            //{
            //    lblPersonagemRecebendo.Visible = true;
            //}
            //else { lblPersonagemRecebendo.Visible = false; }

            #region Hab1

            Verdes = Arena.RetornaHabVerdes("Hab1");
            Vermelhos = Arena.RetornaHabVermelhos("Hab1");
            Azuls = Arena.RetornaHabAzuls("Hab1");
            Pretos = (Arena.RetornaHabPretos("Hab1") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab1") == 0)
            {
                lblPersonagem2CDHab1.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab1PodeUsar = true;
                    picHab1Personagem2.Image = ConverteFoto(lstPersonagens[j].Habilidades[0].Foto);
                }
                else
                {
                    Hab1PodeUsar = false;
                    picHab1Personagem2.Image = bmpFotoMonstro1Hab1Indisponivel;
                }
            }
            else
            {
                lblPersonagem2CDHab1.Visible = true;
                lblPersonagem2CDHab1.Text = Arena.RetornaPersonagemCD("Hab1").ToString();

                Hab1PodeUsar = false;
                picHab1Personagem2.Image = bmpFotoMonstro1Hab1Indisponivel;
            }
            #endregion
            #region Hab2

            Verdes = Arena.RetornaHabVerdes("Hab2");
            Vermelhos = Arena.RetornaHabVermelhos("Hab2");
            Azuls = Arena.RetornaHabAzuls("Hab2");
            Pretos = (Arena.RetornaHabPretos("Hab2") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab2") == 0)
            {
                lblPersonagem2CDHab2.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab2PodeUsar = true;
                    picHab2Personagem2.Image = ConverteFoto(lstPersonagens[j].Habilidades[1].Foto);
                }
                else
                {
                    Hab2PodeUsar = false;
                    picHab2Personagem2.Image = bmpFotoPersonagem2Hab2Indisponivel;
                }
            }
            else
            {
                lblPersonagem2CDHab2.Visible = true;
                lblPersonagem2CDHab2.Text = Arena.RetornaPersonagemCD("Hab2").ToString();

                Hab2PodeUsar = false;
                picHab2Personagem2.Image = bmpFotoPersonagem2Hab2Indisponivel;
            }
            #endregion
            #region Hab3

            Verdes = Arena.RetornaHabVerdes("Hab3");
            Vermelhos = Arena.RetornaHabVermelhos("Hab3");
            Azuls = Arena.RetornaHabAzuls("Hab3");
            Pretos = (Arena.RetornaHabPretos("Hab3") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab3") == 0)
            {
                lblPersonagem2CDHab3.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab3PodeUsar = true;
                    picHab3Personagem2.Image = ConverteFoto(lstPersonagens[j].Habilidades[2].Foto);
                }
                else
                {
                    Hab3PodeUsar = false;
                    picHab3Personagem2.Image = bmpFotoMonstro1Hab3Indisponivel;
                }
            }
            else
            {
                lblPersonagem2CDHab3.Visible = true;
                lblPersonagem2CDHab3.Text = Arena.RetornaPersonagemCD("Hab3").ToString();

                Hab3PodeUsar = false;
                picHab3Personagem2.Image = bmpFotoMonstro1Hab3Indisponivel;
            }
            #endregion
            #region Hab4

            Verdes = Arena.RetornaHabVerdes("Hab4");
            Vermelhos = Arena.RetornaHabVermelhos("Hab4");
            Azuls = Arena.RetornaHabAzuls("Hab4");
            Pretos = (Arena.RetornaHabPretos("Hab4") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab4") == 0)
            {
                lblPersonagem2CDHab4.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab4PodeUsar = true;
                    picHab4Personagem2.Image = ConverteFoto(lstPersonagens[j].Habilidades[3].Foto);
                }
                else
                {
                    Hab4PodeUsar = false;
                    picHab4Personagem2.Image = bmpFotoMonstro1Hab4Indisponivel;
                }
            }
            else
            {
                lblPersonagem2CDHab4.Visible = true;
                lblPersonagem2CDHab4.Text = Arena.RetornaPersonagemCD("Hab4").ToString();

                Hab4PodeUsar = false;
                picHab4Personagem2.Image = bmpFotoMonstro1Hab4Indisponivel;
            }
            #endregion
        }
        private void SetarFotosPersonagem3(int j)
        {
            alvo = "";
            //habilidadaUsadaPersonagem = "";
            imgPersonagemHabUsada = null;
            habFoiCompletada = false;

            imgMonstroHabUsada = null;
            //monstroHabUsada = "";

            picPersonagem3.Image = ConverteFoto(lstPersonagens[j].Foto);

            picHab1Personagem3.Visible = true;
            picHab2Personagem3.Visible = true;
            picHab3Personagem3.Visible = true;
            picHab4Personagem3.Visible = true;

            picHabEscolhidaPersonagem3.Image = Properties.Resources.Ponto_de_interrogacao;
            int Verdes, Azuls, Vermelhos, Pretos;

            bool PersonagemRecebendo = false;
            bool MonstroRecebendo = false;

            #region Personagem

            #region PersonagemInvulneravel
            if (Arena.RetornaPersonagemTurnosInvulneravel() > 0)
            {
                //PersonagemInvulneravel = true;
                picPersonagem3.Image = bmpFotoPersonagem3Indisponivel;
            }
            else
            {

            }
            #endregion

            #region PersonagemDanoTurno
            if (Arena.RetornaPersonagemDanoTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemDanoPerfuranteTurno
            if (Arena.RetornaPersonagemDanoPerfuranteTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemDanoVerdadeiroTurno
            if (Arena.RetornaPersonagemDanoVerdadeiroTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemCuraTurno
            if (Arena.RetornaPersonagemCuraTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region PersonagemArmaduraTurno
            if (Arena.RetornaPersonagemArmaduraTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #endregion

            #region Monstro

            #region MonstroInvulnerável
            if (Arena.RetornaMonstroTurnosInvulneravel() > 0)
            {
                MonstroInvulneravel = true;
                picMonstro1.Image = bmpFotoMonstro1Indisponivel;
            }
            else
            {
                MonstroInvulneravel = false;
            }
            #endregion

            #region MonstroDanoTurno
            if (Arena.RetornaMonstroDanoTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroDanoPerfuranteTurno
            if (Arena.RetornaMonstroDanoPerfuranteTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroDanoVerdadeiroTurno
            if (Arena.RetornaMonstroDanoVerdadeiroTurno() > 0)
            {
                PersonagemRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroCuraTurno
            if (Arena.RetornaMonstroCuraTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #region MonstroArmaduraTurno
            if (Arena.RetornaMonstroArmaduraTurno() > 0)
            {
                MonstroRecebendo = true;

            }
            else
            {
            }
            #endregion

            #endregion;

            //if (MonstroRecebendo == true)
            //{
            //    lblMonstroRecebendo.Visible = true;
            //}
            //else { lblMonstroRecebendo.Visible = false; }
            //if (PersonagemRecebendo == true)
            //{
            //    lblPersonagemRecebendo.Visible = true;
            //}
            //else { lblPersonagemRecebendo.Visible = false; }

            #region Hab1

            Verdes = Arena.RetornaHabVerdes("Hab1");
            Vermelhos = Arena.RetornaHabVermelhos("Hab1");
            Azuls = Arena.RetornaHabAzuls("Hab1");
            Pretos = (Arena.RetornaHabPretos("Hab1") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab1") == 0)
            {
                lblPersonagem3CDHab1.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab1PodeUsar = true;
                    picHab1Personagem3.Image = ConverteFoto(lstPersonagens[j].Habilidades[0].Foto);
                }
                else
                {
                    Hab1PodeUsar = false;
                    picHab1Personagem3.Image = bmpFotoMonstro1Hab1Indisponivel;
                }
            }
            else
            {
                lblPersonagem3CDHab1.Visible = true;
                lblPersonagem3CDHab1.Text = Arena.RetornaPersonagemCD("Hab1").ToString();

                Hab1PodeUsar = false;
                picHab1Personagem3.Image = bmpFotoMonstro1Hab1Indisponivel;
            }
            #endregion
            #region Hab2

            Verdes = Arena.RetornaHabVerdes("Hab2");
            Vermelhos = Arena.RetornaHabVermelhos("Hab2");
            Azuls = Arena.RetornaHabAzuls("Hab2");
            Pretos = (Arena.RetornaHabPretos("Hab2") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab2") == 0)
            {
                lblPersonagem3CDHab2.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab2PodeUsar = true;
                    picHab2Personagem3.Image = ConverteFoto(lstPersonagens[j].Habilidades[1].Foto);
                }
                else
                {
                    Hab2PodeUsar = false;
                    picHab2Personagem3.Image = bmpFotoPersonagem3Hab2Indisponivel;
                }
            }
            else
            {
                lblPersonagem3CDHab2.Visible = true;
                lblPersonagem3CDHab2.Text = Arena.RetornaPersonagemCD("Hab2").ToString();

                Hab2PodeUsar = false;
                picHab2Personagem3.Image = bmpFotoPersonagem3Hab2Indisponivel;
            }
            #endregion
            #region Hab3

            Verdes = Arena.RetornaHabVerdes("Hab3");
            Vermelhos = Arena.RetornaHabVermelhos("Hab3");
            Azuls = Arena.RetornaHabAzuls("Hab3");
            Pretos = (Arena.RetornaHabPretos("Hab3") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab3") == 0)
            {
                lblPersonagem3CDHab3.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab3PodeUsar = true;
                    picHab3Personagem3.Image = ConverteFoto(lstPersonagens[j].Habilidades[2].Foto);
                }
                else
                {
                    Hab3PodeUsar = false;
                    picHab3Personagem3.Image = bmpFotoMonstro1Hab3Indisponivel;
                }
            }
            else
            {
                lblPersonagem3CDHab3.Visible = true;
                lblPersonagem3CDHab3.Text = Arena.RetornaPersonagemCD("Hab3").ToString();

                Hab3PodeUsar = false;
                picHab3Personagem3.Image = bmpFotoMonstro1Hab3Indisponivel;
            }
            #endregion
            #region Hab4

            Verdes = Arena.RetornaHabVerdes("Hab4");
            Vermelhos = Arena.RetornaHabVermelhos("Hab4");
            Azuls = Arena.RetornaHabAzuls("Hab4");
            Pretos = (Arena.RetornaHabPretos("Hab4") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab4") == 0)
            {
                lblPersonagem3CDHab4.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab4PodeUsar = true;
                    picHab4Personagem3.Image = ConverteFoto(lstPersonagens[j].Habilidades[3].Foto);
                }
                else
                {
                    Hab4PodeUsar = false;
                    picHab4Personagem3.Image = bmpFotoMonstro1Hab4Indisponivel;
                }
            }
            else
            {
                lblPersonagem3CDHab4.Visible = true;
                lblPersonagem3CDHab4.Text = Arena.RetornaPersonagemCD("Hab4").ToString();

                Hab4PodeUsar = false;
                picHab4Personagem3.Image = bmpFotoMonstro1Hab4Indisponivel;
            }
            #endregion
        }

        private void SetarFotosMonstro1(int j)
        {
            picMonstro1.Image = ConverteFoto(lstMonstros[j].Foto);
        }
        private void SetarFotosMonstro2(int j)
        {
            picMonstro2.Image = ConverteFoto(lstMonstros[j].Foto);
        }
        private void SetarFotosMonstro3(int j)
        {
            picMonstro3.Image = ConverteFoto(lstMonstros[j].Foto);
        }

        private void GerarEnergiaAleatoria()    //Função que gera uma nova energia na Classe Arena. No fim, chama a função SetarEnergia. 
        {
            Arena.GerarEnergia(ArenaRegras.EnergiasPorRound);
            SetarEnergia();
        }

        private void UsarHabilidade(HabilidadePersonagem hab)	//Função que faz a habilidade do personagem acontecer. Faz o Dano, Cura, Armadura, Energia Gasta, Energia Ganha, CDs, Invulnerabilidade. Tudo do personagem. 
        {
            if (habFoiCompletada)
            {
                //Dano
                if (hab.Dano.DanoHabilidade > 0 || hab.DanoPerfurante.DanoHabilidade > 0 || hab.DanoVerdadeiro.DanoHabilidade > 0)
                {
                    prgBarMonstro1Vida.Value = Arena.AtacarMonstroVida(hab.Dano.DanoHabilidade, hab.DanoPerfurante.DanoHabilidade, hab.DanoVerdadeiro.DanoHabilidade, prgBarMonstro1Vida.Minimum);
                    lblMonstro1Vida.Text = prgBarMonstro1Vida.Value.ToString() + "/" + prgBarMonstro1Vida.Maximum.ToString();

                }
                //Cura
                if (hab.Cura.CuraHabilidade > 0)
                {
                    prgBarPersonagem1Vida.Value = Arena.CurarPersonagemVida(hab.Cura.CuraHabilidade, prgBarPersonagem1Vida.Maximum);
                    lblPersonagem1Vida.Text = prgBarPersonagem1Vida.Value.ToString() + "/" + prgBarPersonagem1Vida.Maximum.ToString();
                }
                //Armadura
                if (hab.Armadura.ArmaduraHabilidade > 0)
                {
                }

                //Arena.SetarHabilidadePorTurno(hab);

                Arena.TirarEnergiaVerde(hab.EnergiaVerde.Quantidade);
                Arena.TirarEnergiaAzul(hab.EnergiaAzul.Quantidade);
                Arena.TirarEnergiaVermelha(hab.EnergiaVermelho.Quantidade);

                Arena.PorEnergiaVerde(hab.EnergiaVerde.Ganho);
                Arena.PorEnergiaAzul(hab.EnergiaAzul.Ganho);
                Arena.PorEnergiaVermelha(hab.EnergiaVermelho.Ganho);
                Arena.PorEnergiaAleatoria(hab.EnergiaPreto.Ganho);

                #region Código Obsoleto
                /*
				switch (Habilidade)
                {
                    #region Hab1

                    case "Hab1":

                        #region Dano
                        if (Arena.RetornaHab1Dano() > 0 || Arena.RetornaHab1DanoVerdadeiro() > 0)
                        {
                            prgBarMonstroVida.Value = Arena.AtacarMonstroVida(Arena.RetornaHab1Dano(), Arena.RetornaHab1DanoPerfurante() ,Arena.RetornaHab1DanoVerdadeiro(), prgBarMonstroVida.Minimum);
                            lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                            lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0);
                        }
                        #endregion

                        #region Cura
                        if (Arena.RetornaHab1Cura() > 0)
                        {
                            prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(Arena.RetornaHab1Cura(), prgBarPersonagemVida.Maximum);
                            lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                        }
                        #endregion

                        #region Armadura
                        if (Arena.RetornaHab1Armadura() > 0)
                        {
                            lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(Arena.RetornaHab1Armadura());
                        }
                        #endregion

                        #region Energia Gasta

                        Arena.TirarEnergiaVerde(Arena.RetornaHab1Verdes());
                        Arena.TirarEnergiaAzul(Arena.RetornaHab1Azuls());
                        Arena.TirarEnergiaVermelha(Arena.RetornaHab1Vermelhos());

                        #endregion

                        #region Energia Ganha

                        Arena.PorEnergiaVerde(Arena.RetornaHab1VerdesGanhos());
                        Arena.PorEnergiaAzul(Arena.RetornaHab1AzulsGanhos());
                        Arena.PorEnergiaVermelha(Arena.RetornaHab1VermelhosGanhos());
                        Arena.PorEnergiaAleatoria(Arena.RetornaHab1PretosGanhos());

                        #endregion

                        break;

                    #endregion
                    #region Hab2

                    case "Hab2":

                        #region Dano
                        if (Arena.RetornaHab2Dano() > 0 || Arena.RetornaHab2DanoVerdadeiro() > 0)
                        {
                            prgBarMonstroVida.Value = Arena.AtacarMonstroVida(Arena.RetornaHab2Dano(), Arena.RetornaHab2DanoPerfurante(), Arena.RetornaHab2DanoVerdadeiro(), prgBarMonstroVida.Minimum);
                            lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                            lblMonstroArmadura.Text = "Armadura : " + Arena.SetarMonstroArmadura(0);
                        }
                        #endregion

                        #region Cura
                        if (Arena.RetornaHab2Cura() > 0)
                        {
                            prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(Arena.RetornaHab2Cura(), prgBarPersonagemVida.Maximum);
                            lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                        }
                        #endregion

                        #region Armadura
                        if (Arena.RetornaHab2Armadura() > 0)
                        {
                            lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(Arena.RetornaHab2Armadura());
                        }
                        #endregion

                        #region Energia Gasta

                        Arena.TirarEnergiaVerde(Arena.RetornaHab2Verdes());
                        Arena.TirarEnergiaAzul(Arena.RetornaHab2Azuls());
                        Arena.TirarEnergiaVermelha(Arena.RetornaHab2Vermelhos());

                        #endregion

                        #region Energia Ganha

                        Arena.PorEnergiaVerde(Arena.RetornaHab2VerdesGanhos());
                        Arena.PorEnergiaAzul(Arena.RetornaHab2AzulsGanhos());
                        Arena.PorEnergiaVermelha(Arena.RetornaHab2VermelhosGanhos());
                        Arena.PorEnergiaAleatoria(Arena.RetornaHab2PretosGanhos());

                        #endregion

                        break;

                    #endregion
                    #region Hab3

                    case "Hab3":

                        #region Dano
                        if (Arena.RetornaHab3Dano() > 0 || Arena.RetornaHab3DanoVerdadeiro() > 0)
                        {
                            prgBarMonstroVida.Value = Arena.AtacarMonstroVida(Arena.RetornaHab3Dano(), Arena.RetornaHab3DanoPerfurante(), Arena.RetornaHab3DanoVerdadeiro(), prgBarMonstroVida.Minimum);
                            lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                            lblMonstroArmadura.Text = "Armadura : " + Arena.SetarMonstroArmadura(0);
                        }
                        #endregion

                        #region Cura
                        if (Arena.RetornaHab3Cura() > 0)
                        {
                            prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(Arena.RetornaHab3Cura(), prgBarPersonagemVida.Maximum);
                            lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                        }
                        #endregion

                        #region Armadura
                        if (Arena.RetornaHab3Armadura() > 0)
                        {
                            lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(Arena.RetornaHab3Armadura());
                        }
                        #endregion

                        #region Energia Gasta

                        Arena.TirarEnergiaVerde(Arena.RetornaHab3Verdes());
                        Arena.TirarEnergiaAzul(Arena.RetornaHab3Azuls());
                        Arena.TirarEnergiaVermelha(Arena.RetornaHab3Vermelhos());

                        #endregion

                        #region Energia Ganha

                        Arena.PorEnergiaVerde(Arena.RetornaHab3VerdesGanhos());
                        Arena.PorEnergiaAzul(Arena.RetornaHab3AzulsGanhos());
                        Arena.PorEnergiaVermelha(Arena.RetornaHab3VermelhosGanhos());
                        Arena.PorEnergiaAleatoria(Arena.RetornaHab3PretosGanhos());

                        #endregion

                        break;

                    #endregion
                    #region  Hab4

                    case "Hab4":

                        #region Dano
                        if (Arena.RetornaHab4Dano() > 0 || Arena.RetornaHab4DanoVerdadeiro() > 0)
                        {
							prgBarMonstroVida.Value = Arena.AtacarMonstroVida(Arena.RetornaHab4Dano(), Arena.RetornaHab4DanoPerfurante(), Arena.RetornaHab4DanoVerdadeiro(), prgBarMonstroVida.Minimum);
                            lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                            lblMonstroArmadura.Text = "Armadura : " + Arena.SetarMonstroArmadura(0);
                        }
                        #endregion

                        #region Cura
                        if (Arena.RetornaHab4Cura() > 0)
                        {
                            prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(Arena.RetornaHab4Cura(), prgBarPersonagemVida.Maximum);
                            lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                        }
                        #endregion

                        #region Armadura
                        if (Arena.RetornaHab4Armadura() > 0)
                        {
                            lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(Arena.RetornaHab4Armadura());
                        }
                        #endregion

                        #region Energia Gasta

                        Arena.TirarEnergiaVerde(Arena.RetornaHab4Verdes());
                        Arena.TirarEnergiaAzul(Arena.RetornaHab4Azuls());
                        Arena.TirarEnergiaVermelha(Arena.RetornaHab4Vermelhos());

                        #endregion

                        #region Energia Ganha

                        Arena.PorEnergiaVerde(Arena.RetornaHab4VerdesGanhos());
                        Arena.PorEnergiaAzul(Arena.RetornaHab4AzulsGanhos());
                        Arena.PorEnergiaVermelha(Arena.RetornaHab4VermelhosGanhos());
                        Arena.PorEnergiaAleatoria(Arena.RetornaHab4PretosGanhos());

                        #endregion

                        break;

                        #endregion
                }*/
                #endregion
                //Arena.SetarCDSPersonagem(hab);
            }
        }

        private void UsarHabilidadesPorTurno()
        {
            if (Arena.RetornaMonstroTurnosInvulneravel() == 0)
            {
                int dano = Arena.RetornaPersonagemDanoTurno();
                int danoP = Arena.RetornaPersonagemDanoPerfuranteTurno();
                int danoV = Arena.RetornaPersonagemDanoVerdadeiroTurno();

                if (dano != 0 || danoP != 0 || danoV != 0)
                {
                    prgBarMonstro1Vida.Value = Arena.AtacarMonstroVida(dano, danoP, danoV, prgBarMonstro1Vida.Minimum);
                    lblMonstro1Vida.Text = prgBarMonstro1Vida.Value.ToString() + "/" + prgBarMonstro1Vida.Maximum.ToString();

                }
            }

            int cura = Arena.RetornaPersonagemCuraTurno();
            int armadura = Arena.RetornaPersonagemArmaduraTurno();

            if (cura != 0)
            {
                prgBarPersonagem1Vida.Value = Arena.CurarPersonagemVida(Arena.RetornaPersonagemCuraTurno(), prgBarPersonagem1Vida.Maximum);
                lblPersonagem1Vida.Text = prgBarPersonagem1Vida.Value.ToString() + "/" + prgBarPersonagem1Vida.Maximum.ToString();
            }
            if (armadura != 0)
            {
            }

        }

        private void EscolherHabilidadeMosntro()	//Função que carrega a HabAleatóriaDoArena. Carrega a foto da habilidade do monstro e o Alvo da habilidade. 
        {
            string Hab = Arena.RetornaHabAleatoria();

            switch (Hab)
            {
                case "Hab1":
                    imgMonstroHabUsada = ConverteFoto(lstMonstros.FirstOrDefault().Habilidades[0].Foto);
                    break;
                case "Hab2":
                    imgMonstroHabUsada = ConverteFoto(lstMonstros.FirstOrDefault().Habilidades[1].Foto);
                    break;
                case "Hab3":
                    imgMonstroHabUsada = ConverteFoto(lstMonstros.FirstOrDefault().Habilidades[2].Foto);
                    break;
                case "Hab4":
                    imgMonstroHabUsada = ConverteFoto(lstMonstros.FirstOrDefault().Habilidades[3].Foto);
                    break;
                case "Pass":
                    break;
                default:
                    throw new Exception("Erro no método EscolherHabilidadeMosntro!\nO Switch retornou o valor default.");
            }

            if (Hab != "Pass")
            {
                if (Arena.RetornaMonstro_HabDano(Hab) > 0 || Arena.RetornaMonstro_HabDanoPerfurante(Hab) > 0 || Arena.RetornaMonstro_HabDanoVerdadeiro(Hab) > 0) { monstroAlvo = "Personagem"; }
                else { monstroAlvo = "Monstro"; }
            }

            //monstroHabUsada = Hab;
        }

        private void UsarMonstroHabilidade()	//Função que  faz a habilidade do monstro acontecer. Faz o Dano, Cura, Armadura, CDs, Invulnerabilidade. Tudo do Monstro. 
        {
            if (monstroHabUsada != null)
            {
                if (monstroHabUsada.Dano.DanoHabilidade > 0 || monstroHabUsada.DanoPerfurante.DanoHabilidade > 0 || monstroHabUsada.DanoVerdadeiro.DanoHabilidade > 0)
                {
                    prgBarPersonagem1Vida.Value = Arena.AtacarPersonagemVida(monstroHabUsada.Dano.DanoHabilidade, monstroHabUsada.DanoPerfurante.DanoHabilidade, monstroHabUsada.DanoVerdadeiro.DanoHabilidade, prgBarPersonagem1Vida.Minimum);
                    lblPersonagem1Vida.Text = prgBarPersonagem1Vida.Value.ToString() + "/" + prgBarPersonagem1Vida.Maximum.ToString();

                }
                if (monstroHabUsada.Cura.CuraHabilidade > 0)
                {
                    prgBarMonstro1Vida.Value = Arena.CurarMonstroVida(monstroHabUsada.Cura.CuraHabilidade, prgBarMonstro1Vida.Maximum);
                    lblMonstro1Vida.Text = prgBarMonstro1Vida.Value.ToString() + "/" + prgBarMonstro1Vida.Maximum.ToString();
                }
                if (monstroHabUsada.Armadura.ArmaduraHabilidade > 0)
                {
                }

                //Arena.SetarMonstroHabilidadePorTurno(monstroHabUsada);
                //Arena.SetarCDSMonstro(monstroHabUsada);
            }

            #region Código Obsoleto
            /*
			switch (MonstroHabUsada)
			{
				#region Hab1

				case "Hab1":

					#region Dano
					if (MonstroHab1Dano > 0)
					{
						MonstroAlvo = "Personagem";

						prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(Arena.RetornaMonstro_Hab1Dano(), Arena.RetornaMonstro_Hab1DanoPerfurante(), Arena.RetornaMonstro_Hab1DanoVerdadeiro(), prgBarPersonagemVida.Minimum);
						lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
						lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(0);
					}
					else
					{
						MonstroAlvo = "Monstro";
					}
					#endregion

					#region Cura
					if (Arena.RetornaMonstro_Hab1Cura() > 0)
					{
						prgBarMonstroVida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstro_Hab1Cura(), prgBarMonstroVida.Maximum);
						lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
					}
					#endregion

					#region Armadura
					if (Arena.RetornaMonstro_Hab1Armadura() > 0)
					{
						lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(Arena.RetornaMonstro_Hab1Armadura());
					}
					#endregion

					break;

				#endregion
				#region Hab2

				case "Hab2":

					#region Dano
					if (MonstroHab2Dano > 0)
					{
						MonstroAlvo = "Personagem";

						prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(Arena.RetornaMonstro_Hab2Dano(), Arena.RetornaMonstro_Hab2DanoPerfurante(), Arena.RetornaMonstro_Hab2DanoVerdadeiro(), prgBarPersonagemVida.Minimum);
						lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
						lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(0);
					}
					else
					{
						MonstroAlvo = "Monstro";
					}
					#endregion

					#region Cura
					if (Arena.RetornaMonstro_Hab2Cura() > 0)
					{
						prgBarMonstroVida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstro_Hab2Cura(), prgBarMonstroVida.Maximum);
						lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
					}
					#endregion

					#region Armadura
					if (Arena.RetornaMonstro_Hab2Armadura() > 0)
					{
						lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(Arena.RetornaMonstro_Hab2Armadura());
					}
					#endregion

					break;

				#endregion
				#region Hab3

				case "Hab3":

					#region Dano
					if (MonstroHab3Dano > 0)
					{
						MonstroAlvo = "Personagem";

						prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(Arena.RetornaMonstro_Hab3Dano(), Arena.RetornaMonstro_Hab3DanoPerfurante(), Arena.RetornaMonstro_Hab3DanoVerdadeiro(), prgBarPersonagemVida.Minimum);
						lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
						lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(0);
					}
					else
					{
						MonstroAlvo = "Monstro";
					}
					#endregion

					#region Cura
					if (Arena.RetornaMonstro_Hab3Cura() > 0)
					{
						prgBarMonstroVida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstro_Hab3Cura(), prgBarMonstroVida.Maximum);
						lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
					}
					#endregion

					#region Armadura
					if (Arena.RetornaMonstro_Hab3Armadura() > 0)
					{
						lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(Arena.RetornaMonstro_Hab3Armadura());
					}
					#endregion

					break;

				#endregion
				#region Hab4

				case "Hab4":

					#region Dano
					if (MonstroHab4Dano > 0)
					{
						MonstroAlvo = "Personagem";

						prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(Arena.RetornaMonstro_Hab4Dano(), Arena.RetornaMonstro_Hab4DanoPerfurante(), Arena.RetornaMonstro_Hab4DanoVerdadeiro(), prgBarPersonagemVida.Minimum);
						lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
						lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(0);
					}
					else
					{
						MonstroAlvo = "Monstro";
					}
					#endregion

					#region Cura
					if (Arena.RetornaMonstro_Hab4Cura() > 0)
					{
						prgBarMonstroVida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstro_Hab4Cura(), prgBarMonstroVida.Maximum);
						lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
					}
					#endregion

					#region Armadura
					if (Arena.RetornaMonstro_Hab4Armadura() > 0)
					{
						lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(Arena.RetornaMonstro_Hab4Armadura());
					}
					#endregion

					break;

					#endregion
			} 
	*/
            #endregion
        }

        private void UsarMonstroHabilidadesPorTurno()
        {
            if (Arena.RetornaPersonagemTurnosInvulneravel() == 0)
            {
                int dano = Arena.RetornaMonstroDanoTurno();
                int danoP = Arena.RetornaMonstroDanoPerfuranteTurno();
                int danoV = Arena.RetornaMonstroDanoVerdadeiroTurno();

                if (dano != 0 || danoP != 0 || danoV != 0)
                {
                    prgBarPersonagem1Vida.Value = Arena.AtacarPersonagemVida(dano, danoP, danoV, prgBarPersonagem1Vida.Minimum);
                    lblPersonagem1Vida.Text = prgBarPersonagem1Vida.Value.ToString() + "/" + prgBarPersonagem1Vida.Maximum.ToString();

                }
            }

            int cura = Arena.RetornaMonstroCuraTurno();
            int armadura = Arena.RetornaMonstroArmaduraTurno();

            if (cura != 0)
            {
                prgBarMonstro1Vida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstroCuraTurno(), prgBarMonstro1Vida.Maximum);
                lblMonstro1Vida.Text = prgBarMonstro1Vida.Value.ToString() + "/" + prgBarMonstro1Vida.Maximum.ToString();
            }
            if (armadura != 0)
            {
            }
        }

        private void VerificaJogoAcabou()	//Função que verifica se alguém morreu. Se sim, avisa o usuario do resultado e fecha o form. 
        {
            if (prgBarMonstro1Vida.Value == prgBarMonstro1Vida.Minimum)
            {
                MessageBox.Show("Você ganhou!", "Parabéns!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Timer.Stop();
                Close();
            }
            else if (prgBarPersonagem1Vida.Value == prgBarPersonagem1Vida.Minimum)
            {
                MessageBox.Show("Você Perdeu!", "Derrota!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Timer.Stop();
                Close();
            }
        }

        private void PassarTempo()	//Função que bloqueia as ações do Personagem e seta as fotos de habilidade como indisponíveis. Depois, starta o Timer e faz um random de quanto tempo será o turno. 
        {
            PassandoTempo = true;
            btnPronto.Enabled = false;
            btnTrocarEnergia.Enabled = false;

            picHab1Personagem1.Image = bmpFotoMonstro1Hab1Indisponivel;
            picHab2Personagem1.Image = bmpFotoMonstro1Hab2Indisponivel;
            picHab3Personagem1.Image = bmpFotoMonstro1Hab3Indisponivel;
            picHab4Personagem1.Image = bmpFotoMonstro1Hab4Indisponivel;

            lblPersonagem1CDHab1.Visible = false;
            lblPersonagem1CDHab2.Visible = false;
            lblPersonagem1CDHab3.Visible = false;
            lblPersonagem1CDHab4.Visible = false;

            Timer.Start();

            Random rnd = new Random();

            Tempo = rnd.Next(8, ArenaRegras.TempoMaxPassando);

            t = new Thread(EscolherHabilidadeMosntro);
            t.Start();

        }

        private void CarregarInformacoesPersonagem(Personagem personagem)
        {
            picInfoHabSelecionada.Image = ConverteFoto(personagem.Foto);
            lblInfoNome.Text = personagem.Nome;
            txtInfoDescricao.Text = personagem.Descricao;

            lblEnergia.Visible = false;

            lblRecarga.Visible = false;
        }

        private void CarregarInformacoesMonstro(Monstro monstro)
        {
            picInfoHabSelecionada.Image = ConverteFoto(monstro.Foto);
            lblInfoNome.Text = monstro.Nome;
            txtInfoDescricao.Text = monstro.Descricao;

            lblEnergia.Visible = false;

            lblRecarga.Visible = false;
        }

        private void CarregarInformacoesMonstroPass()
        {
            picInfoHabSelecionada.Image = Properties.Resources.Ponto_de_interrogacao;
            lblInfoNome.Text = "O Monstro passou o turno!";
            txtInfoDescricao.Text = "O Monstro passou o turno!\nTome cuidado!";

            lblEnergia.Visible = false;
            lblRecarga.Visible = false;
        }

        private void CarregarInformacoesVazio()
        {
            picInfoHabSelecionada.Image = Properties.Resources.Ponto_de_interrogacao;
            lblInfoNome.Text = "Você está em combate!";
            txtInfoDescricao.Text = "Você está no campo de batalha!";

            lblEnergia.Visible = false;
            lblRecarga.Visible = false;
        }

        private void CarregarInformacoesDefault()
        {
            lblEnergia.Visible = true;
            lblRecarga.Visible = true;
        }

        private void CarregarInformacoesHabilidadePersonagem(HabilidadePersonagem hab)
        {
            lblInfoNome.Text = hab.Nome;
            txtInfoDescricao.Text = hab.Descricao;
            lblRecarga.Text = "TEMPO DE RECARGA: ";
            lblRecarga.Text += hab.Recarga == 1 ? $"{hab.Recarga} TURNO" : $"{hab.Recarga} TURNOS";
            picInfoHabSelecionada.Image = ConverteFoto(hab.Foto);
            CarregaPicsEnergiaPersonagem(hab);
        }

        private void CarregarInformacoesHabilidadeMonstro(HabilidadeMonstro hab)
        {
            lblInfoNome.Text = hab.Nome;
            txtInfoDescricao.Text = hab.Descricao;
            lblEnergia.Visible = false;
            lblRecarga.Text = "TEMPO DE RECARGA: ";
            lblRecarga.Text += hab.Recarga == 1 ? $"{hab.Recarga} TURNO" : $"{hab.Recarga} TURNOS";
            picInfoHabSelecionada.Image = ConverteFoto(hab.Foto);
        }

        private void CarregarInformacoes()
        {
            for (int i = 0; i < groupBoxInformacoesPics.Length; i++)
            {
                groupBoxInformacoesPics[i].Visible = false;
            }
        }

        private void CarregaPicsEnergiaPersonagem(HabilidadePersonagem hab)
        {
            int totalEnergias = hab.EnergiaVerde.Quantidade + hab.EnergiaAzul.Quantidade + hab.EnergiaVermelho.Quantidade + hab.EnergiaPreto.Quantidade;
            lblEnergia.Visible = true;

            for (int i = 0; i < groupBoxInformacoesPics.Length; i++)
            {
                groupBoxInformacoesPics[i].Visible = false;
            }

            if (totalEnergias > 0)
            {
                lblEnergia.Text = "ENERGIA:";

                for (int i = 0; i < hab.EnergiaVerde.Quantidade; i++)
                {
                    groupBoxInformacoesPics[i].Image = Properties.Resources.Verde;
                }
                for (int i = 0; i < hab.EnergiaAzul.Quantidade; i++)
                {
                    groupBoxInformacoesPics[hab.EnergiaVerde.Quantidade + i].Image = Properties.Resources.Azul;
                }
                for (int i = 0; i < hab.EnergiaVermelho.Quantidade; i++)
                {
                    groupBoxInformacoesPics[hab.EnergiaVerde.Quantidade + hab.EnergiaAzul.Quantidade + i].Image = Properties.Resources.Vermelho;
                }
                for (int i = 0; i < hab.EnergiaPreto.Quantidade; i++)
                {
                    groupBoxInformacoesPics[hab.EnergiaVerde.Quantidade + hab.EnergiaAzul.Quantidade + hab.EnergiaVermelho.Quantidade + i].Image = Properties.Resources.Preto;
                }

                #region Deixar os Pic de Energia visiveis ou não
                for (int i = 0; i < totalEnergias; i++)
                {
                    groupBoxInformacoesPics[i].Visible = true;
                }
                #endregion
            }
            else
            {
                lblEnergia.Text = "ENERGIA: NENHUMA";
            }
        }

        private void FazerMonstro() //Chama a função EscolherHabilidadeMosntro. Chama a função CarregaInformacoes com as info da habilidade que o monstro usou. Inicia o frmTurno. Ao sair do frmTurno, diminui o numero de turnos invulneraveis do personagem, diminui o cds do monstro, chama a função UsarMonstroHabilidade e depois VerificaSeJogoAcabou. Por fim, chama a função GerarEnergiaAleatoria. 
        {
            while (t.IsAlive)
            {
                MessageBox.Show("a");
            }

            CarregarInformacoesHabilidadeMonstro(monstroHabUsada);
            //frmTurno Turno = new frmTurno(imgMonstroHabUsada, ConverteFoto(personagem.Foto), ConverteFoto(monstro.Foto), monstroAlvo, "Monstro");
            //if (Turno.ShowDialog() == DialogResult.OK)
            //{
            //    Arena.DiminuirMonstroHabilidadesPorTurno();
            //    Arena.DiminuirCDSMonstro();
            //    UsarMonstroHabilidade();
            //    UsarMonstroHabilidadesPorTurno();
            //    VerificaJogoAcabou();
            //}

            GerarEnergiaAleatoria();
        }

        private Image ConverteFoto(byte[] imagem)
        {
            return Image.FromStream(new MemoryStream(imagem));
        }

        #endregion
    }
}
