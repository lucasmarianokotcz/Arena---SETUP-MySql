using Model.Arena.Regras.Classes;
using Model.Monstro;
using Model.Personagem;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Arena
{
    public partial class frmArena : Form
    {
        #region Atributos

        Intermediario.Arena Arena;
        private Personagem personagem;
        private Monstro monstro;
        private bool habFoiCompletada = false;
        #region Bitmaps (Personagem/Monstro)
        private Bitmap bmpFotoPersonagemDisponivel, bmpFotoPersonagemIndisponivel, bmpFotoPersonagemHab1Indisponivel, bmpFotoPersonagemHab2Indisponivel, bmpFotoPersonagemHab3Indisponivel, bmpFotoPersonagemHab4Indisponivel;
        private Bitmap bmpFotoMonstroDisponivel, bmpFotoMonstroIndisponivel, bmpFotoMonstroHab1Indisponivel, bmpFotoMonstroHab2Indisponivel, bmpFotoMonstroHab3Indisponivel, bmpFotoMonstroHab4Indisponivel;
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
        HabilidadePersonagem habilidadaUsadaPersonagem;
        string alvo;
        bool PersonagemInvulneravel;
        HabilidadeMonstro monstroHabUsada;
        string monstroAlvo;
        bool MonstroInvulneravel;
        bool PassandoTempo = false;
        private int Tempo;

        #endregion

        #region Event Methods

        public frmArena(Personagem personagem, Monstro monstro)
        {
            InitializeComponent();

            this.personagem = personagem;
            this.monstro = monstro;
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
            SetarFotosPersonagem();
            CarregarInformacoesDefault();
        }
        #endregion

        #region Click
        private void picVerHabs_Click(object sender, EventArgs e)
        {
            if (((Control)sender).Name == "btnVerHabilidadesInimigo")
            {
                picInfoHabSelecionada.Image = ConverteFoto(monstro.Foto);

                btnVerHabilidadesInimigo.Visible = false;
                txtInfoDescricao.Visible = false;

                int i = 0;
                picVerHab1.Image = ConverteFoto(monstro.Habilidades[i++].Foto);
                picVerHab2.Image = ConverteFoto(monstro.Habilidades[i++].Foto);
                picVerHab3.Image = ConverteFoto(monstro.Habilidades[i++].Foto);
                picVerHab4.Image = ConverteFoto(monstro.Habilidades[i++].Foto);

                picVerHab1.Visible = true;
                picVerHab2.Visible = true;
                picVerHab3.Visible = true;
                picVerHab4.Visible = true;

                lblRecarga.Visible = false;
                lblInfoNome.Text = monstro.Nome;
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
                    CarregarInformacoesHabilidadeMonstro(monstro.Habilidades[0]);
                }
                else if (((Control)sender).Name == "picVerHab2")
                {
                    CarregarInformacoesHabilidadeMonstro(monstro.Habilidades[1]);
                }
                else if (((Control)sender).Name == "picVerHab3")
                {
                    CarregarInformacoesHabilidadeMonstro(monstro.Habilidades[2]);
                }
                else if (((Control)sender).Name == "picVerHab4")
                {
                    CarregarInformacoesHabilidadeMonstro(monstro.Habilidades[3]);
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
                AleatoriosHab = habilidadaUsadaPersonagem.EnergiaPreto.Quantidade;
                if (habilidadaUsadaPersonagem != null)
                {
                    VerdesHab = habilidadaUsadaPersonagem.EnergiaVerde.Quantidade;
                    AzuisHab = habilidadaUsadaPersonagem.EnergiaAzul.Quantidade;
                    VermelhosHab = habilidadaUsadaPersonagem.EnergiaVermelho.Quantidade;
                }
            }

            frmTurno Turno = new frmTurno(imgPersonagemHabUsada, ConverteFoto(personagem.Foto), ConverteFoto(monstro.Foto), alvo, "Personagem", AleatoriosHab, (Arena.RetornaVerdes() - VerdesHab), (Arena.RetornaAzuls() - AzuisHab), (Arena.RetornaVermelhos() - VermelhosHab));

            if (Turno.ShowDialog() == DialogResult.Yes)
            {
                if (Turno.HabAleatorios >= 0)
                {
                    Arena.TirarEnergiaVerde(Turno.RetornaVerdesGastos());
                    Arena.TirarEnergiaAzul(Turno.RetornaAzuisGastos());
                    Arena.TirarEnergiaVermelha(Turno.RetornaVermelhosGastos());
                }

                Arena.DiminuirHabilidadesPorTurno();
                Arena.DiminuirCDSPersonagem();
                UsarHabilidade(habilidadaUsadaPersonagem);
                UsarHabilidadesPorTurno();
                SetarEnergia();
                VerificaJogoAcabou();
                PassarTempo();


                lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0).ToString();
            }
        }
        private void btnTrocarEnergia_Click(object sender, EventArgs e) //Evento click do btnTrocarEnergia. Carrega localmente o número de energias que o personagem tem, e se tem o número mínimo para a troca, inicia o frmTurno. Tira as energias gastas e coloca as energias ganhas. No fim, chama a função SetarEnegia. 
        {
            int Verdes = Arena.RetornaVerdes();
            int Azuis = Arena.RetornaAzuls();
            int Vermelhos = Arena.RetornaVermelhos();

            if (Verdes >= ArenaRegras.EnergiasIguaisMinimasParaTroca || Azuis >= ArenaRegras.EnergiasIguaisMinimasParaTroca || Vermelhos >= ArenaRegras.EnergiasIguaisMinimasParaTroca)
            {
                frmTurno Turno = new frmTurno("TrocarEnergia", "Personagem", Verdes, Azuis, Vermelhos);
                if (Turno.ShowDialog() == DialogResult.Yes)
                {
                    Arena.TirarEnergiaVerde(Turno.RetornaVerdesGastos());
                    Arena.TirarEnergiaAzul(Turno.RetornaAzuisGastos());
                    Arena.TirarEnergiaVermelha(Turno.RetornaVermelhosGastos());

                    Arena.PorEnergiaVerde(Turno.RetornaVerdesGanhos());
                    Arena.PorEnergiaAzul(Turno.RetornaAzuisGanhos());
                    Arena.PorEnergiaVermelha(Turno.RetornaVermelhosGanhos());

                    SetarEnergia();
                }
            }
            else
            {
                MessageBox.Show("Você precisa ter no mínimo " + ArenaRegras.EnergiasIguaisMinimasParaTroca.ToString() + " Energias iguais para trocar!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Personagem_Habs_Click(object sender, EventArgs e)	//Evento click das habilidades do Personagem, do Monstro. Essa função verifica se você pode usar essa habilidade, atribui o alvo, as imagens, e a habilidade. Quando é click no Monstro ou no Personagem, atribui o HabFoiCompletada. 
        {
            btnVerHabilidadesInimigo.Visible = false;
            btnVerHabilidadesInimigo.Text = "Ver Habilidades";
            picVerHab1.Visible = false;
            picVerHab2.Visible = false;
            picVerHab3.Visible = false;
            picVerHab4.Visible = false;

            txtInfoDescricao.Visible = true;

            if (PersonagemInvulneravel == false)
            {
                picPersonagem.Image = ConverteFoto(personagem.Foto);
            }

            if (MonstroInvulneravel == false)
            {
                picMonstro.Image = ConverteFoto(monstro.Foto);
            }

            CarregarInformacoes();

            #region Personagem
            if (((Control)sender).Name == "picPersonagem")
            {
                CarregarInformacoesPersonagem();

                if (alvo == "Personagem" && PassandoTempo == false)
                {
                    habFoiCompletada = true;

                    Hab1PodeUsar = false;
                    Hab2PodeUsar = false;
                    Hab3PodeUsar = false;
                    Hab4PodeUsar = false;

                    switch (habilidadaUsadaPersonagem)
                    {
                        case "Hab1":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[0].Foto);
                            picHab1.Visible = false;

                            picHab2.Image = bmpFotoPersonagemHab2Indisponivel;
                            picHab3.Image = bmpFotoPersonagemHab3Indisponivel;
                            picHab4.Image = bmpFotoPersonagemHab4Indisponivel;
                            break;
                        case "Hab2":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[1].Foto);
                            picHab2.Visible = false;

                            picHab1.Image = bmpFotoPersonagemHab1Indisponivel;
                            picHab3.Image = bmpFotoPersonagemHab3Indisponivel;
                            picHab4.Image = bmpFotoPersonagemHab4Indisponivel;
                            break;
                        case "Hab3":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[2].Foto);
                            picHab3.Visible = false;

                            picHab1.Image = bmpFotoPersonagemHab1Indisponivel;
                            picHab2.Image = bmpFotoPersonagemHab2Indisponivel;
                            picHab4.Image = bmpFotoPersonagemHab4Indisponivel;
                            break;
                        case "Hab4":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[3].Foto);
                            picHab4.Visible = false;

                            picHab1.Image = bmpFotoPersonagemHab1Indisponivel;
                            picHab2.Image = bmpFotoPersonagemHab2Indisponivel;
                            picHab3.Image = bmpFotoPersonagemHab3Indisponivel;
                            break;
                    }

                    picHabEscolhida.Image = imgPersonagemHabUsada;
                }
                else
                {
                    alvo = "";
                    habilidadaUsadaPersonagem = null;
                    habFoiCompletada = false;
                }
            }
            #endregion
            #region Hab1            
            else if (((Control)sender).Name == "picHab1")
            {
                CarregarInformacoesHabilidadePersonagem(personagem.Habilidades[0]);
                if (Hab1PodeUsar == true && PassandoTempo == false)
                {
                    habilidadaUsadaPersonagem = "Hab1";

                    picHab2.Visible = true;
                    picHab3.Visible = true;
                    picHab4.Visible = true;

                    if (Arena.PersonagemHabAtaca("Hab1") == false)               //Se a habilidade não causa dano, o alvo é o personagem.
                    {
                        alvo = "Personagem";
                        picPersonagem.Image = bmpFotoPersonagemDisponivel;
                        picMonstro.Image = bmpFotoMonstroIndisponivel;
                    }
                    else
                    {                                                           //Se a habilidade causa dano:
                        if (MonstroInvulneravel == false)                       //Se o monstro não está invulnerável, o alvo é o monstro.
                        {
                            alvo = "Monstro";
                            picPersonagem.Image = bmpFotoPersonagemIndisponivel;
                            picMonstro.Image = bmpFotoMonstroDisponivel;
                        }
                        else
                        {                                                       //Se o monstro está invulnerável, configura a habilidade para não fazer nada.
                            alvo = "";
                            habilidadaUsadaPersonagem = null;

                            #region Código Obsoleto
                            /*
							if (Arena.PersonagemHabDefende("Hab1"))
							{
								Alvo = "Personagem";
								picPersonagem.Image = bmp_Foto_Personagem_Disponivel;
								picMonstro.Image = bmp_Foto_Monstro_Indisponivel;
							}
							else
							{
								Alvo = "";
								HabUsada = "";
							}
							*/
                            #endregion
                        }
                    }
                }
            }
            #endregion
            #region Hab2
            else if (((Control)sender).Name == "picHab2")
            {
                CarregarInformacoesHabilidadePersonagem(personagem.Habilidades[1]);
                if (Hab2PodeUsar == true && PassandoTempo == false)
                {
                    picHab1.Visible = true;
                    picHab3.Visible = true;
                    picHab4.Visible = true;

                    habilidadaUsadaPersonagem = "Hab2";

                    if (Arena.PersonagemHabAtaca("Hab2") == false)
                    {
                        alvo = "Personagem";
                        picPersonagem.Image = bmpFotoPersonagemDisponivel;
                        picMonstro.Image = bmpFotoMonstroIndisponivel;
                    }
                    else
                    {
                        if (MonstroInvulneravel == false)
                        {
                            alvo = "Monstro";
                            picPersonagem.Image = bmpFotoPersonagemIndisponivel;
                            picMonstro.Image = bmpFotoMonstroDisponivel;
                        }
                        else
                        {
                            alvo = "";
                            habilidadaUsadaPersonagem = "";
                        }
                    }
                }
            }
            #endregion
            #region Hab3
            else if (((Control)sender).Name == "picHab3")
            {
                CarregarInformacoesHabilidadePersonagem(personagem.Habilidades[2]);
                if (Hab3PodeUsar == true && PassandoTempo == false)
                {
                    picHab1.Visible = true;
                    picHab2.Visible = true;
                    picHab4.Visible = true;

                    habilidadaUsadaPersonagem = "Hab3";
                    if (Arena.PersonagemHabAtaca("Hab3") == false)
                    {
                        alvo = "Personagem";
                        picPersonagem.Image = bmpFotoPersonagemDisponivel;
                        picMonstro.Image = bmpFotoMonstroIndisponivel;
                    }
                    else
                    {
                        if (MonstroInvulneravel == false)
                        {
                            alvo = "Monstro";
                            picPersonagem.Image = bmpFotoPersonagemIndisponivel;
                            picMonstro.Image = bmpFotoMonstroDisponivel;
                        }
                        else
                        {
                            alvo = "";
                            habilidadaUsadaPersonagem = "";
                        }
                    }
                }
            }
            #endregion
            #region Hab4
            else if (((Control)sender).Name == "picHab4")
            {
                CarregarInformacoesHabilidadePersonagem(personagem.Habilidades[3]);
                if (Hab4PodeUsar == true && PassandoTempo == false)
                {
                    picHab1.Visible = true;
                    picHab2.Visible = true;
                    picHab3.Visible = true;

                    habilidadaUsadaPersonagem = "Hab4";
                    if (Arena.PersonagemHabAtaca("Hab4") == false)
                    {
                        alvo = "Personagem";
                        picPersonagem.Image = bmpFotoPersonagemDisponivel;
                        picMonstro.Image = bmpFotoMonstroIndisponivel;
                    }
                    else
                    {
                        if (MonstroInvulneravel == false)
                        {
                            alvo = "Monstro";
                            picPersonagem.Image = bmpFotoPersonagemIndisponivel;
                            picMonstro.Image = bmpFotoMonstroDisponivel;
                        }
                        else
                        {
                            alvo = "";
                            habilidadaUsadaPersonagem = "";
                        }
                    }
                }
            }
            #endregion
            #region Monstro
            else if (((Control)sender).Name == "picMonstro")
            {
                CarregarInformacoesMonstro();
                btnVerHabilidadesInimigo.Visible = true;

                if (alvo == "Monstro" && PassandoTempo == false)
                {
                    habFoiCompletada = true;

                    Hab1PodeUsar = false;
                    Hab2PodeUsar = false;
                    Hab3PodeUsar = false;
                    Hab4PodeUsar = false;

                    switch (habilidadaUsadaPersonagem)
                    {
                        case "Hab1":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[0].Foto);
                            picHab1.Visible = false;

                            picHab2.Image = bmpFotoMonstroHab2Indisponivel;
                            picHab3.Image = bmpFotoMonstroHab3Indisponivel;
                            picHab4.Image = bmpFotoMonstroHab4Indisponivel;
                            break;
                        case "Hab2":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[1].Foto);
                            picHab2.Visible = false;

                            picHab1.Image = bmpFotoMonstroHab1Indisponivel;
                            picHab3.Image = bmpFotoMonstroHab3Indisponivel;
                            picHab4.Image = bmpFotoMonstroHab4Indisponivel;
                            break;
                        case "Hab3":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[2].Foto);
                            picHab3.Visible = false;

                            picHab1.Image = bmpFotoMonstroHab1Indisponivel;
                            picHab2.Image = bmpFotoMonstroHab2Indisponivel;
                            picHab4.Image = bmpFotoMonstroHab4Indisponivel;
                            break;
                        case "Hab4":
                            imgPersonagemHabUsada = ConverteFoto(personagem.Habilidades[3].Foto);
                            picHab4.Visible = false;

                            picHab1.Image = bmpFotoMonstroHab1Indisponivel;
                            picHab2.Image = bmpFotoMonstroHab2Indisponivel;
                            picHab3.Image = bmpFotoMonstroHab3Indisponivel;
                            break;
                    }

                    picHabEscolhida.Image = imgPersonagemHabUsada;
                }
                else
                {
                    alvo = "";
                    habilidadaUsadaPersonagem = "";
                    habFoiCompletada = false;
                }
            }
            #endregion
            #region picHabEscolhida
            else if (((Control)sender).Name == "picHabEscolhida")
            {
                alvo = "";
                if (habFoiCompletada == true)
                {
                    CarregarInformacoes();
                }
                else
                {
                    CarregarInformacoesDefault();
                }
            }
            #endregion
            #region picMonstroHabEscolhida
            else if (((Control)sender).Name == "picMonstroHabEscolhida")
            {
                alvo = "";
                CarregarInformacoesDefault();
            }
            #endregion
        }
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
            picPersonagem.Image = ConverteFoto(personagem.Foto);
            SetarPicturesBoxesInformacoesPersonagem();
            SetarBitmapsPersonagens();
            SetarFotosPersonagem();
        }
        private void SetarImagensMonstros()
        {
            picMonstro.Image = ConverteFoto(monstro.Foto);
            SetarPicturesBoxesInformacoesMonstro();
            SetarBitmapsMonstros();
            SetarFotosMonstro();
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
            personagemInformacoesPics[0] = picPersonagemInfo1;
            personagemInformacoesPics[1] = picPersonagemInfo2;
            personagemInformacoesPics[2] = picPersonagemInfo3;
            personagemInformacoesPics[3] = picPersonagemInfo4;
            personagemInformacoesPics[4] = picPersonagemInfo5;
            personagemInformacoesPics[5] = picPersonagemInfo6;
            personagemInformacoesPics[6] = picPersonagemInfo7;
            personagemInformacoesPics[7] = picPersonagemInfo8;
        }
        private void SetarPicturesBoxesInformacoesMonstro()
        {
            monstroInformacoesPics[0] = picMonstroInfo1;
            monstroInformacoesPics[1] = picMonstroInfo2;
            monstroInformacoesPics[2] = picMonstroInfo3;
            monstroInformacoesPics[3] = picMonstroInfo4;
            monstroInformacoesPics[4] = picMonstroInfo5;
            monstroInformacoesPics[5] = picMonstroInfo6;
            monstroInformacoesPics[6] = picMonstroInfo7;
            monstroInformacoesPics[7] = picMonstroInfo8;
        }
        #endregion

        #region Setar Bitmaps
        private void SetarBitmapsPersonagens()
        {
            bmpFotoPersonagemIndisponivel = new Bitmap(ConverteFoto(personagem.Foto));
            bmpFotoPersonagemDisponivel = new Bitmap(ConverteFoto(personagem.Foto));
            int i = 0;
            bmpFotoPersonagemHab1Indisponivel = new Bitmap(ConverteFoto(personagem.Habilidades[i++].Foto));
            bmpFotoPersonagemHab2Indisponivel = new Bitmap(ConverteFoto(personagem.Habilidades[i++].Foto));
            bmpFotoPersonagemHab3Indisponivel = new Bitmap(ConverteFoto(personagem.Habilidades[i++].Foto));
            bmpFotoPersonagemHab4Indisponivel = new Bitmap(ConverteFoto(personagem.Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Personagem_Indisponivel
            for (int Width = 0; Width < bmpFotoPersonagemIndisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagemIndisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagemIndisponivel.GetPixel(Width, Height);

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
                    bmpFotoPersonagemIndisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Personagem_Disponivel
            for (int Width = 0; Width < bmpFotoPersonagemDisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagemDisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagemDisponivel.GetPixel(Width, Height);

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

                    bmpFotoPersonagemDisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Hab1
            for (int Width = 0; Width < bmpFotoPersonagemHab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagemHab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagemHab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagemHab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab2
            for (int Width = 0; Width < bmpFotoPersonagemHab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagemHab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagemHab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagemHab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab3
            for (int Width = 0; Width < bmpFotoPersonagemHab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagemHab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagemHab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagemHab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Hab4
            for (int Width = 0; Width < bmpFotoPersonagemHab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoPersonagemHab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoPersonagemHab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoPersonagemHab4Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
        }
        private void SetarBitmapsMonstros()
        {
            bmpFotoMonstroIndisponivel = new Bitmap(ConverteFoto(monstro.Foto));
            bmpFotoMonstroDisponivel = new Bitmap(ConverteFoto(monstro.Foto));
            int i = 0;
            bmpFotoMonstroHab1Indisponivel = new Bitmap(ConverteFoto(monstro.Habilidades[i++].Foto));
            bmpFotoMonstroHab2Indisponivel = new Bitmap(ConverteFoto(monstro.Habilidades[i++].Foto));
            bmpFotoMonstroHab3Indisponivel = new Bitmap(ConverteFoto(monstro.Habilidades[i++].Foto));
            bmpFotoMonstroHab4Indisponivel = new Bitmap(ConverteFoto(monstro.Habilidades[i++].Foto));

            Color CorAnterior;
            Color CorNova;

            #region Foto_Monstro_Indisponivel
            for (int Width = 0; Width < bmpFotoMonstroIndisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstroIndisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstroIndisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstroIndisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Disponivel
            for (int Width = 0; Width < bmpFotoMonstroDisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstroDisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstroDisponivel.GetPixel(Width, Height);

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

                    bmpFotoMonstroDisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion

            #region Foto_Monstro_Hab1
            for (int Width = 0; Width < bmpFotoMonstroHab1Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstroHab1Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstroHab1Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstroHab1Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab2
            for (int Width = 0; Width < bmpFotoMonstroHab2Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstroHab2Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstroHab2Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstroHab2Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab3
            for (int Width = 0; Width < bmpFotoMonstroHab3Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstroHab3Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstroHab3Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstroHab3Indisponivel.SetPixel(Width, Height, CorNova);
                }
            }
            #endregion
            #region Foto_Monstro_Hab4
            for (int Width = 0; Width < bmpFotoMonstroHab4Indisponivel.Width; Width++)
            {
                for (int Height = 0; Height < bmpFotoMonstroHab4Indisponivel.Height; Height++)
                {
                    CorAnterior = bmpFotoMonstroHab4Indisponivel.GetPixel(Width, Height);
                    CorNova = Color.FromArgb(150, CorAnterior);
                    bmpFotoMonstroHab4Indisponivel.SetPixel(Width, Height, CorNova);
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

            SetarFotosPersonagem();
            SetarFotosMonstro();
        }

        private void SetarFotosPersonagem()
        {
            alvo = "";
            habilidadaUsadaPersonagem = "";
            imgPersonagemHabUsada = null;
            habFoiCompletada = false;

            imgMonstroHabUsada = null;
            monstroHabUsada = "";

            picPersonagem.Image = ConverteFoto(personagem.Foto);

            picHab1.Visible = true;
            picHab2.Visible = true;
            picHab3.Visible = true;
            picHab4.Visible = true;

            picHabEscolhida.Image = Properties.Resources.Ponto_de_interrogacao;
            int Verdes, Azuls, Vermelhos, Pretos;

            bool PersonagemRecebendo = false;
            bool MonstroRecebendo = false;

            #region Personagem

            #region PersonagemInvulneravel
            if (Arena.RetornaPersonagemTurnosInvulneravel() > 0)
            {
                PersonagemInvulneravel = true;
                picPersonagem.Image = bmpFotoPersonagemIndisponivel;
                lblPersonagemInvulneravel.Text = "Invulnerável: " + Arena.RetornaPersonagemTurnosInvulneravel().ToString() + " turnos";
                lblPersonagemInvulneravel.Visible = true;
            }
            else
            {
                PersonagemInvulneravel = false;
                lblPersonagemInvulneravel.Visible = false;
            }
            #endregion

            #region PersonagemDanoTurno
            if (Arena.RetornaPersonagemDanoTurno() > 0)
            {
                MonstroRecebendo = true;

                lblMonstroDanoTurno.Text = Arena.RetornaPersonagemDanoTurno().ToString() + " de Dano por " + Arena.RetornaPersonagemTurnosDano().ToString() + " turnos";
                lblMonstroDanoTurno.Visible = true;
            }
            else
            {
                lblMonstroDanoTurno.Visible = false;
            }
            #endregion

            #region PersonagemDanoPerfuranteTurno
            if (Arena.RetornaPersonagemDanoPerfuranteTurno() > 0)
            {
                MonstroRecebendo = true;

                lblMonstroDanoPerfuranteTurno.Text = Arena.RetornaPersonagemDanoPerfuranteTurno().ToString() + " de Dano Perfurante por " + Arena.RetornaPersonagemTurnosDanoPerfurante().ToString() + " turnos";
                lblMonstroDanoPerfuranteTurno.Visible = true;
            }
            else
            {
                lblMonstroDanoPerfuranteTurno.Visible = false;
            }
            #endregion

            #region PersonagemDanoVerdadeiroTurno
            if (Arena.RetornaPersonagemDanoVerdadeiroTurno() > 0)
            {
                MonstroRecebendo = true;

                lblMonstroDanoVerdadeiroTurno.Text = Arena.RetornaPersonagemDanoVerdadeiroTurno().ToString() + " de Dano Verdadeiro por " + Arena.RetornaPersonagemTurnosDanoVerdadeiro().ToString() + " turnos";
                lblMonstroDanoVerdadeiroTurno.Visible = true;
            }
            else
            {
                lblMonstroDanoVerdadeiroTurno.Visible = false;
            }
            #endregion

            #region PersonagemCuraTurno
            if (Arena.RetornaPersonagemCuraTurno() > 0)
            {
                PersonagemRecebendo = true;

                lblPersonagemCuraTurno.Text = Arena.RetornaPersonagemCuraTurno().ToString() + " de Cura por " + Arena.RetornaPersonagemTurnosCura().ToString() + " turnos";
                lblPersonagemCuraTurno.Visible = true;
            }
            else
            {
                lblPersonagemCuraTurno.Visible = false;
            }
            #endregion

            #region PersonagemArmaduraTurno
            if (Arena.RetornaPersonagemArmaduraTurno() > 0)
            {
                PersonagemRecebendo = true;

                lblPersonagemArmaduraTurno.Text = Arena.RetornaPersonagemArmaduraTurno().ToString() + " de Armadura por " + Arena.RetornaPersonagemTurnosArmadura().ToString() + " turnos";
                lblPersonagemArmaduraTurno.Visible = true;
            }
            else
            {
                lblPersonagemArmaduraTurno.Visible = false;
            }
            #endregion

            #endregion

            #region Monstro

            #region MonstroInvulnerável
            if (Arena.RetornaMonstroTurnosInvulneravel() > 0)
            {
                MonstroInvulneravel = true;
                picMonstro.Image = bmpFotoMonstroIndisponivel;
                lblMonstroInvulneravel.Text = "Invulnerável: " + Arena.RetornaMonstroTurnosInvulneravel().ToString() + " turnos";
                lblMonstroInvulneravel.Visible = true;
            }
            else
            {
                MonstroInvulneravel = false;
                lblMonstroInvulneravel.Visible = false;
            }
            #endregion

            #region MonstroDanoTurno
            if (Arena.RetornaMonstroDanoTurno() > 0)
            {
                PersonagemRecebendo = true;

                lblPersonagemDanoTurno.Text = Arena.RetornaMonstroDanoTurno().ToString() + " de Dano por " + Arena.RetornaMonstroTurnosDano().ToString() + " turnos";
                lblPersonagemDanoTurno.Visible = true;
            }
            else
            {
                lblPersonagemDanoTurno.Visible = false;
            }
            #endregion

            #region MonstroDanoPerfuranteTurno
            if (Arena.RetornaMonstroDanoPerfuranteTurno() > 0)
            {
                PersonagemRecebendo = true;

                lblPersonagemDanoPerfuranteTurno.Text = Arena.RetornaMonstroDanoPerfuranteTurno().ToString() + " de Dano Perfurante por " + Arena.RetornaMonstroTurnosDanoPerfurante().ToString() + " turnos";
                lblPersonagemDanoPerfuranteTurno.Visible = true;
            }
            else
            {
                lblPersonagemDanoPerfuranteTurno.Visible = false;
            }
            #endregion

            #region MonstroDanoVerdadeiroTurno
            if (Arena.RetornaMonstroDanoVerdadeiroTurno() > 0)
            {
                PersonagemRecebendo = true;

                lblPersonagemDanoVerdadeiroTurno.Text = Arena.RetornaMonstroDanoVerdadeiroTurno().ToString() + " de Dano Verdadeiro por " + Arena.RetornaMonstroTurnosDanoVerdadeiro().ToString() + " turnos";
                lblPersonagemDanoVerdadeiroTurno.Visible = true;
            }
            else
            {
                lblPersonagemDanoVerdadeiroTurno.Visible = false;
            }
            #endregion

            #region MonstroCuraTurno
            if (Arena.RetornaMonstroCuraTurno() > 0)
            {
                MonstroRecebendo = true;

                lblMonstroCuraTurno.Text = Arena.RetornaMonstroCuraTurno().ToString() + " de Cura por " + Arena.RetornaMonstroTurnosCura().ToString() + " turnos";
                lblMonstroCuraTurno.Visible = true;
            }
            else
            {
                lblMonstroCuraTurno.Visible = false;
            }
            #endregion

            #region MonstroArmaduraTurno
            if (Arena.RetornaMonstroArmaduraTurno() > 0)
            {
                MonstroRecebendo = true;

                lblMonstroArmaduraTurno.Text = Arena.RetornaMonstroArmaduraTurno().ToString() + " de Armadura por " + Arena.RetornaMonstroTurnosArmadura().ToString() + " turnos";
                lblMonstroArmaduraTurno.Visible = true;
            }
            else
            {
                lblMonstroArmaduraTurno.Visible = false;
            }
            #endregion

            #endregion;

            if (MonstroRecebendo == true)
            {
                lblMonstroRecebendo.Visible = true;
            }
            else { lblMonstroRecebendo.Visible = false; }
            if (PersonagemRecebendo == true)
            {
                lblPersonagemRecebendo.Visible = true;
            }
            else { lblPersonagemRecebendo.Visible = false; }

            #region Hab1

            Verdes = Arena.RetornaHabVerdes("Hab1");
            Vermelhos = Arena.RetornaHabVermelhos("Hab1");
            Azuls = Arena.RetornaHabAzuls("Hab1");
            Pretos = (Arena.RetornaHabPretos("Hab1") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab1") == 0)
            {
                lblPersonagemCDHab1.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab1PodeUsar = true;
                    picHab1.Image = ConverteFoto(personagem.Habilidades[0].Foto);
                }
                else
                {
                    Hab1PodeUsar = false;
                    picHab1.Image = bmpFotoMonstroHab1Indisponivel;
                }
            }
            else
            {
                lblPersonagemCDHab1.Visible = true;
                lblPersonagemCDHab1.Text = Arena.RetornaPersonagemCD("Hab1").ToString();

                Hab1PodeUsar = false;
                picHab1.Image = bmpFotoMonstroHab1Indisponivel;
            }
            #endregion
            #region Hab2

            Verdes = Arena.RetornaHabVerdes("Hab2");
            Vermelhos = Arena.RetornaHabVermelhos("Hab2");
            Azuls = Arena.RetornaHabAzuls("Hab2");
            Pretos = (Arena.RetornaHabPretos("Hab2") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab2") == 0)
            {
                lblPersonagemCDHab2.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab2PodeUsar = true;
                    picHab2.Image = ConverteFoto(personagem.Habilidades[1].Foto);
                }
                else
                {
                    Hab2PodeUsar = false;
                    picHab2.Image = bmpFotoPersonagemHab2Indisponivel;
                }
            }
            else
            {
                lblPersonagemCDHab2.Visible = true;
                lblPersonagemCDHab2.Text = Arena.RetornaPersonagemCD("Hab2").ToString();

                Hab2PodeUsar = false;
                picHab2.Image = bmpFotoPersonagemHab2Indisponivel;
            }
            #endregion
            #region Hab3

            Verdes = Arena.RetornaHabVerdes("Hab3");
            Vermelhos = Arena.RetornaHabVermelhos("Hab3");
            Azuls = Arena.RetornaHabAzuls("Hab3");
            Pretos = (Arena.RetornaHabPretos("Hab3") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab3") == 0)
            {
                lblPersonagemCDHab3.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab3PodeUsar = true;
                    picHab3.Image = ConverteFoto(personagem.Habilidades[2].Foto);
                }
                else
                {
                    Hab3PodeUsar = false;
                    picHab3.Image = bmpFotoMonstroHab3Indisponivel;
                }
            }
            else
            {
                lblPersonagemCDHab3.Visible = true;
                lblPersonagemCDHab3.Text = Arena.RetornaPersonagemCD("Hab3").ToString();

                Hab3PodeUsar = false;
                picHab3.Image = bmpFotoMonstroHab3Indisponivel;
            }
            #endregion
            #region Hab4

            Verdes = Arena.RetornaHabVerdes("Hab4");
            Vermelhos = Arena.RetornaHabVermelhos("Hab4");
            Azuls = Arena.RetornaHabAzuls("Hab4");
            Pretos = (Arena.RetornaHabPretos("Hab4") + Verdes + Azuls + Vermelhos);

            if (Arena.RetornaPersonagemCD("Hab4") == 0)
            {
                lblPersonagemCDHab4.Visible = false;

                if (EnergiaVerde >= Verdes && EnergiaAzul >= Azuls && EnergiaVermelha >= Vermelhos && EnergiaPreta >= Pretos)
                {
                    Hab4PodeUsar = true;
                    picHab4.Image = ConverteFoto(personagem.Habilidades[3].Foto);
                }
                else
                {
                    Hab4PodeUsar = false;
                    picHab4.Image = bmpFotoMonstroHab4Indisponivel;
                }
            }
            else
            {
                lblPersonagemCDHab4.Visible = true;
                lblPersonagemCDHab4.Text = Arena.RetornaPersonagemCD("Hab4").ToString();

                Hab4PodeUsar = false;
                picHab4.Image = bmpFotoMonstroHab4Indisponivel;
            }
            #endregion
        }

        private void SetarFotosMonstro()
        {
            picMonstro.Image = ConverteFoto(monstro.Foto);
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
                    prgBarMonstroVida.Value = Arena.AtacarMonstroVida(hab.Dano.DanoHabilidade, hab.DanoPerfurante.DanoHabilidade, hab.DanoVerdadeiro.DanoHabilidade, prgBarMonstroVida.Minimum);
                    lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                    lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0);
                }
                //Cura
                if (hab.Cura.CuraHabilidade > 0)
                {
                    prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(hab.Cura.CuraHabilidade, prgBarPersonagemVida.Maximum);
                    lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                }
                //Armadura
                if (hab.Armadura.ArmaduraHabilidade > 0)
                {
                    lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(hab.Armadura.ArmaduraHabilidade);
                }

                Arena.SetarHabilidadePorTurno(hab);

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
                Arena.SetarCDSPersonagem(hab);
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
                    prgBarMonstroVida.Value = Arena.AtacarMonstroVida(dano, danoP, danoV, prgBarMonstroVida.Minimum);
                    lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                    lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0);
                }
            }

            int cura = Arena.RetornaPersonagemCuraTurno();
            int armadura = Arena.RetornaPersonagemArmaduraTurno();

            if (cura != 0)
            {
                prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(Arena.RetornaPersonagemCuraTurno(), prgBarPersonagemVida.Maximum);
                lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
            }
            if (armadura != 0)
            {
                lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(Arena.RetornaPersonagemArmaduraTurno());
            }

        }

        private void EscolherHabilidadeMosntro()	//Função que carrega a HabAleatóriaDoArena. Carrega a foto da habilidade do monstro e o Alvo da habilidade. 
        {
            string Hab = Arena.RetornaHabAleatoria();

            switch (Hab)
            {
                case "Hab1":
                    imgMonstroHabUsada = ConverteFoto(monstro.Habilidades[0].Foto);
                    break;
                case "Hab2":
                    imgMonstroHabUsada = ConverteFoto(monstro.Habilidades[1].Foto);
                    break;
                case "Hab3":
                    imgMonstroHabUsada = ConverteFoto(monstro.Habilidades[2].Foto);
                    break;
                case "Hab4":
                    imgMonstroHabUsada = ConverteFoto(monstro.Habilidades[3].Foto);
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

            monstroHabUsada = Hab;
        }

        private void UsarMonstroHabilidade()	//Função que  faz a habilidade do monstro acontecer. Faz o Dano, Cura, Armadura, CDs, Invulnerabilidade. Tudo do Monstro. 
        {
            if (monstroHabUsada != null)
            {
                if (monstroHabUsada.Dano.DanoHabilidade > 0 || monstroHabUsada.DanoPerfurante.DanoHabilidade > 0 || monstroHabUsada.DanoVerdadeiro.DanoHabilidade > 0)
                {
                    prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(monstroHabUsada.Dano.DanoHabilidade, monstroHabUsada.DanoPerfurante.DanoHabilidade, monstroHabUsada.DanoVerdadeiro.DanoHabilidade, prgBarPersonagemVida.Minimum);
                    lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                    lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(0);
                }
                if (monstroHabUsada.Cura.CuraHabilidade > 0)
                {
                    prgBarMonstroVida.Value = Arena.CurarMonstroVida(monstroHabUsada.Cura.CuraHabilidade, prgBarMonstroVida.Maximum);
                    lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
                }
                if (monstroHabUsada.Armadura.ArmaduraHabilidade > 0)
                {
                    lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(monstroHabUsada.Armadura.ArmaduraHabilidade);
                }

                Arena.SetarMonstroHabilidadePorTurno(monstroHabUsada);
                Arena.SetarCDSMonstro(monstroHabUsada);
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
                    prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(dano, danoP, danoV, prgBarPersonagemVida.Minimum);
                    lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
                    lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0);
                }
            }

            int cura = Arena.RetornaMonstroCuraTurno();
            int armadura = Arena.RetornaMonstroArmaduraTurno();

            if (cura != 0)
            {
                prgBarMonstroVida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstroCuraTurno(), prgBarMonstroVida.Maximum);
                lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
            }
            if (armadura != 0)
            {
                lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(Arena.RetornaMonstroArmaduraTurno());
            }
        }

        private void VerificaJogoAcabou()	//Função que verifica se alguém morreu. Se sim, avisa o usuario do resultado e fecha o form. 
        {
            if (prgBarMonstroVida.Value == prgBarMonstroVida.Minimum)
            {
                MessageBox.Show("Você ganhou!", "Parabéns!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Timer.Stop();
                Close();
            }
            else if (prgBarPersonagemVida.Value == prgBarPersonagemVida.Minimum)
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

            picHab1.Image = bmpFotoMonstroHab1Indisponivel;
            picHab2.Image = bmpFotoMonstroHab2Indisponivel;
            picHab3.Image = bmpFotoMonstroHab3Indisponivel;
            picHab4.Image = bmpFotoMonstroHab4Indisponivel;

            lblPersonagemCDHab1.Visible = false;
            lblPersonagemCDHab2.Visible = false;
            lblPersonagemCDHab3.Visible = false;
            lblPersonagemCDHab4.Visible = false;

            Timer.Start();

            Random rnd = new Random();

            Tempo = rnd.Next(8, ArenaRegras.TempoMaxPassando);

            t = new Thread(EscolherHabilidadeMosntro);
            t.Start();

        }

        private void CarregarInformacoesPersonagem()
        {
            picInfoHabSelecionada.Image = ConverteFoto(personagem.Foto);
            lblInfoNome.Text = Arena.RetornaPersonagemNome();
            txtInfoDescricao.Text = Arena.RetornaPersonagemDescricao();

            lblEnergia.Visible = false;

            lblRecarga.Visible = false;
        }

        private void CarregarInformacoesMonstro()
        {
            picInfoHabSelecionada.Image = ConverteFoto(monstro.Foto);
            lblInfoNome.Text = Arena.RetornaMonstro_Nome();
            txtInfoDescricao.Text = Arena.RetornaMonstro_Descricao();

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
            frmTurno Turno = new frmTurno(imgMonstroHabUsada, ConverteFoto(personagem.Foto), ConverteFoto(monstro.Foto), monstroAlvo, "Monstro");
            if (Turno.ShowDialog() == DialogResult.OK)
            {
                Arena.DiminuirMonstroHabilidadesPorTurno();
                Arena.DiminuirCDSMonstro();
                UsarMonstroHabilidade();
                UsarMonstroHabilidadesPorTurno();
                VerificaJogoAcabou();
            }

            GerarEnergiaAleatoria();
        }

        private Image ConverteFoto(byte[] imagem)
        {
            return Image.FromStream(new MemoryStream(imagem));
        }

        #endregion
    }
}
