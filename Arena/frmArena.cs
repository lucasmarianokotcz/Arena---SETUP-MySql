using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Arena
{
	public partial class frmArena : Form
	{
		Intermediario.Arena Arena;
		
		protected int EnergiasPorRound = 5; //Energias que o personagem ganha por round.
		protected int ChanceDeComeçar = 5000;// 5000;   //Chance de começar o jogo. Valor Exclusive. Quando vale 2 -> 1/2 chances de começar. Quando vale 3 -> 2/3 chances de começar.
		protected int TempoMaxPassando = 5 * 2;    //Controla o tempo (em segundos) máximo que demora pro Timer de tempo acabar. O produto da multiplicação deve ser maior que 8.
		protected int EnergiasIguaisMinimasParaTroca = 2; //Controla a quantidade de energias iguais mínimas requeridas para realizar a troca de energia. 

		public frmArena(Intermediario.Arena arena)
		{
			InitializeComponent();

			Arena = arena;

			prgBarTempo.Maximum = TempoMaxPassando;
		}

		//Declara as váriaveis Image (Foto das Hab e dos Personagens)
		#region  Declaração de Imagens
		Image Personagem_Foto, Hab1_Foto, Hab2_Foto, Hab3_Foto, Hab4_Foto;
		Image Personagem_Foto_Disponivel;
		Image Personagem_Foto_Indisponivel, Hab1_Foto_Indisponivel, Hab2_Foto_Indisponivel, Hab3_Foto_Indisponivel, Hab4_Foto_Indisponivel;
		Image MonstroFoto, MonstroHab1_Foto, MonstroHab2_Foto, MonstroHab3_Foto, MonstroHab4_Foto;
		Image MonstroFoto_Disponivel;
		Image MonstroFoto_Indisponivel, MonstroHab1_Foto_Indisponivel, MonstroHab2_Foto_Indisponivel, MonstroHab3_Foto_Indisponivel, MonstroHab4_Foto_Indisponivel;

		Image Foto_HabUsada, Foto_MonstroHabUsada;
		#endregion
		//Declara os arrays de pictureboxes
		PictureBox[] Grp_Info_Pics = new PictureBox[5];
		PictureBox[] Personagem_Info_Pics = new PictureBox[8];
		PictureBox[] Monstro_Info_Pics = new PictureBox[8];
		string[,] Personagem_Info_Habs = new string[8, 3];
		string[,] Monstro_Info_Habs = new string[8, 3];
		
		Thread t;

		int EnergiaVerde = 0, EnergiaAzul = 0, EnergiaVermelha = 0, EnergiaPreta = 0;

		bool Hab1PodeUsar = false, Hab2PodeUsar = false, Hab3PodeUsar = false, Hab4PodeUsar = false;
		string Alvo, HabUsada;
		bool HabFoiCompletada = false;
		bool PersonagemInvulneravel;
		
		private void picPersonagemInfos_MouseHover(object sender, EventArgs e)
		{
			switch (((Control)sender).Name)
			{
				case "picPersonagemInfo1":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[0, 1];
					break;
				case "picPersonagemInfo2":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[1, 1];
					break;
				case "picPersonagemInfo3":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[2, 1];
					break;
				case "picPersonagemInfo4":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[3, 1];
					break;
				case "picPersonagemInfo5":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[4, 1];
					break;
				case "picPersonagemInfo6":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[5, 1];
					break;
				case "picPersonagemInfo7":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[6, 1];
					break;
				case "picPersonagemInfo8":
					toolTip1.ToolTipTitle = Personagem_Info_Habs[7, 1];
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
					toolTip1.ToolTipTitle = Monstro_Info_Habs[0, 0];
					break;
				case "picMonstroInfo2":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[1, 0];
					break;
				case "picMonstroInfo3":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[2, 0];
					break;
				case "picMonstroInfo4":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[3, 0];
					break;
				case "picMonstroInfo5":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[4, 0];
					break;
				case "picMonstroInfo6":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[5, 0];
					break;
				case "picMonstroInfo7":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[6, 0];
					break;
				case "picMonstroInfo8":
					toolTip1.ToolTipTitle = Monstro_Info_Habs[7, 0];
					break;
				case "lblMonstroInfoInfinitas":
					toolTip1.ToolTipTitle = "Muitas informações...";
					break;
			}
		}

		string MonstroAlvo, MonstroHabUsada;
		bool MonstroInvulneravel;

		bool PassandoTempo = false;
		int Tempo;

		private void frmArena_Load(object sender, EventArgs e)  //Evento Load do Form. Popula os DataGridView, Chama a função SetarPersonagem, SetarMonstros e SetarImagens. Faz o random de quem começa jogando. 
		{
			SetarImagens();

			Random rnd = new Random();
			int d = rnd.Next(0, ChanceDeComeçar);
			if (d == 0)
			{
				PassarTempo();
			}
			else
			{
				GerarEnergiaAleatoria();
			}
		}

		private void SetarImagens()     //Função que popula os arrays de pictureboxes, popula as variáveis Image e cria as alterações nas imagens (de disponível ou não). No fim, chama a função SetarFotos. 
		{
			Grp_Info_Pics[0] = picInfoEnergia1;
			Grp_Info_Pics[1] = picInfoEnergia2;
			Grp_Info_Pics[2] = picInfoEnergia3;
			Grp_Info_Pics[3] = picInfoEnergia4;
			Grp_Info_Pics[4] = picInfoEnergia5;

			Personagem_Info_Pics[0] = picPersonagemInfo1;
			Personagem_Info_Pics[1] = picPersonagemInfo2;
			Personagem_Info_Pics[2] = picPersonagemInfo3;
			Personagem_Info_Pics[3] = picPersonagemInfo4;
			Personagem_Info_Pics[4] = picPersonagemInfo5;
			Personagem_Info_Pics[5] = picPersonagemInfo6;
			Personagem_Info_Pics[6] = picPersonagemInfo7;
			Personagem_Info_Pics[7] = picPersonagemInfo8;

			Monstro_Info_Pics[0] = picMonstroInfo1;
			Monstro_Info_Pics[1] = picMonstroInfo2;
			Monstro_Info_Pics[2] = picMonstroInfo3;
			Monstro_Info_Pics[3] = picMonstroInfo4;
			Monstro_Info_Pics[4] = picMonstroInfo5;
			Monstro_Info_Pics[5] = picMonstroInfo6;
			Monstro_Info_Pics[6] = picMonstroInfo7;
			Monstro_Info_Pics[7] = picMonstroInfo8;

			byte[] Byte_Foto_Personagem = Arena.RetornaPersonagemFoto();
			byte[] Byte_Foto_Hab1 = Arena.RetornaHabFoto("Hab1");
			byte[] Byte_Foto_Hab2 = Arena.RetornaHabFoto("Hab2");
			byte[] Byte_Foto_Hab3 = Arena.RetornaHabFoto("Hab3");
			byte[] Byte_Foto_Hab4 = Arena.RetornaHabFoto("Hab4");
			byte[] Byte_Foto_Monstro = Arena.RetornaMonstro_Foto();
			byte[] Byte_Foto_Monstro_Hab1 = Arena.RetornaMonstro_HabFoto("Hab1");
			byte[] Byte_Foto_Monstro_Hab2 = Arena.RetornaMonstro_HabFoto("Hab2");
			byte[] Byte_Foto_Monstro_Hab3 = Arena.RetornaMonstro_HabFoto("Hab3");
			byte[] Byte_Foto_Monstro_Hab4 = Arena.RetornaMonstro_HabFoto("Hab4");

			MemoryStream MS_Foto_Personagem = new MemoryStream(Byte_Foto_Personagem);
			MemoryStream MS_Foto_Hab1 = new MemoryStream(Byte_Foto_Hab1);
			MemoryStream MS_Foto_Hab2 = new MemoryStream(Byte_Foto_Hab2);
			MemoryStream MS_Foto_Hab3 = new MemoryStream(Byte_Foto_Hab3);
			MemoryStream MS_Foto_Hab4 = new MemoryStream(Byte_Foto_Hab4);
			MemoryStream MS_Foto_Monstro = new MemoryStream(Byte_Foto_Monstro);
			MemoryStream MS_Foto_Monstro_Hab1 = new MemoryStream(Byte_Foto_Monstro_Hab1);
			MemoryStream MS_Foto_Monstro_Hab2 = new MemoryStream(Byte_Foto_Monstro_Hab2);
			MemoryStream MS_Foto_Monstro_Hab3 = new MemoryStream(Byte_Foto_Monstro_Hab3);
			MemoryStream MS_Foto_Monstro_Hab4 = new MemoryStream(Byte_Foto_Monstro_Hab4);

			Personagem_Foto = Image.FromStream(MS_Foto_Personagem);
			Hab1_Foto = Image.FromStream(MS_Foto_Hab1);
			Hab2_Foto = Image.FromStream(MS_Foto_Hab2);
			Hab3_Foto = Image.FromStream(MS_Foto_Hab3);
			Hab4_Foto = Image.FromStream(MS_Foto_Hab4);
			MonstroFoto = Image.FromStream(MS_Foto_Monstro);
			MonstroHab1_Foto = Image.FromStream(MS_Foto_Monstro_Hab1);
			MonstroHab2_Foto = Image.FromStream(MS_Foto_Monstro_Hab2);
			MonstroHab3_Foto = Image.FromStream(MS_Foto_Monstro_Hab3);
			MonstroHab4_Foto = Image.FromStream(MS_Foto_Monstro_Hab4);

			Bitmap bmp_Foto_Personagem_Indisponivel = new Bitmap(Personagem_Foto);
			Bitmap bmp_Foto_Personagem_Disponivel = new Bitmap(Personagem_Foto);
			Bitmap bmp_Foto_Hab1_Indisponivel = new Bitmap(Hab1_Foto);
			Bitmap bmp_Foto_Hab2_Indisponivel = new Bitmap(Hab2_Foto);
			Bitmap bmp_Foto_Hab3_Indisponivel = new Bitmap(Hab3_Foto);
			Bitmap bmp_Foto_Hab4_Indisponivel = new Bitmap(Hab4_Foto);
			Bitmap bmp_Foto_Monstro_Indisponivel = new Bitmap(MonstroFoto);
			Bitmap bmp_Foto_Monstro_Disponivel = new Bitmap(MonstroFoto);
			Bitmap bmp_Foto_Monstro_Hab1_Indisponivel = new Bitmap(MonstroHab1_Foto);
			Bitmap bmp_Foto_Monstro_Hab2_Indisponivel = new Bitmap(MonstroHab2_Foto);
			Bitmap bmp_Foto_Monstro_Hab3_Indisponivel = new Bitmap(MonstroHab3_Foto);
			Bitmap bmp_Foto_Monstro_Hab4_Indisponivel = new Bitmap(MonstroHab4_Foto);

			Color CorAnterior;
			Color CorNova;

			#region Foto_Personagem_Indisponivel
			for (int Width = 0; Width < bmp_Foto_Personagem_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Personagem_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Personagem_Indisponivel.GetPixel(Width, Height);

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
					bmp_Foto_Personagem_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Personagem_Disponivel
			for (int Width = 0; Width < bmp_Foto_Personagem_Disponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Personagem_Disponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Personagem_Disponivel.GetPixel(Width, Height);

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

					bmp_Foto_Personagem_Disponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion

			#region Foto_Hab1
			for (int Width = 0; Width < bmp_Foto_Hab1_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Hab1_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Hab1_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Hab1_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Hab2
			for (int Width = 0; Width < bmp_Foto_Hab2_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Hab2_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Hab2_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Hab2_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Hab3
			for (int Width = 0; Width < bmp_Foto_Hab3_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Hab3_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Hab3_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Hab3_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Hab4
			for (int Width = 0; Width < bmp_Foto_Hab4_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Hab4_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Hab4_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Hab4_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion

			#region Foto_Monstro_Indisponivel
			for (int Width = 0; Width < bmp_Foto_Monstro_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Monstro_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Monstro_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Monstro_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Monstro_Disponivel
			for (int Width = 0; Width < bmp_Foto_Monstro_Disponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Monstro_Disponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Monstro_Disponivel.GetPixel(Width, Height);

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

					bmp_Foto_Monstro_Disponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion

			#region Foto_Monstro_Hab1
			for (int Width = 0; Width < bmp_Foto_Monstro_Hab1_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Monstro_Hab1_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Monstro_Hab1_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Monstro_Hab1_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Monstro_Hab2
			for (int Width = 0; Width < bmp_Foto_Monstro_Hab2_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Monstro_Hab2_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Monstro_Hab2_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Monstro_Hab2_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Monstro_Hab3
			for (int Width = 0; Width < bmp_Foto_Monstro_Hab3_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Monstro_Hab3_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Monstro_Hab3_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Monstro_Hab3_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion
			#region Foto_Monstro_Hab4
			for (int Width = 0; Width < bmp_Foto_Monstro_Hab4_Indisponivel.Width; Width++)
			{
				for (int Height = 0; Height < bmp_Foto_Monstro_Hab4_Indisponivel.Height; Height++)
				{
					CorAnterior = bmp_Foto_Monstro_Hab4_Indisponivel.GetPixel(Width, Height);
					CorNova = Color.FromArgb(150, CorAnterior);
					bmp_Foto_Monstro_Hab4_Indisponivel.SetPixel(Width, Height, CorNova);
				}
			}
			#endregion

			Personagem_Foto_Indisponivel = bmp_Foto_Personagem_Indisponivel;
			Personagem_Foto_Disponivel = bmp_Foto_Personagem_Disponivel;
			Hab1_Foto_Indisponivel = bmp_Foto_Hab1_Indisponivel;
			Hab2_Foto_Indisponivel = bmp_Foto_Hab2_Indisponivel;
			Hab3_Foto_Indisponivel = bmp_Foto_Hab3_Indisponivel;
			Hab4_Foto_Indisponivel = bmp_Foto_Hab4_Indisponivel;
			MonstroFoto_Indisponivel = bmp_Foto_Monstro_Indisponivel;
			MonstroFoto_Disponivel = bmp_Foto_Monstro_Disponivel;
			MonstroHab1_Foto_Indisponivel = bmp_Foto_Monstro_Hab1_Indisponivel;
			MonstroHab2_Foto_Indisponivel = bmp_Foto_Monstro_Hab2_Indisponivel;
			MonstroHab3_Foto_Indisponivel = bmp_Foto_Monstro_Hab3_Indisponivel;
			MonstroHab4_Foto_Indisponivel = bmp_Foto_Monstro_Hab4_Indisponivel;

			picPersonagem.Image = Personagem_Foto;
			picMonstro.Image = MonstroFoto;

			SetarFotos();
		}

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

			SetarFotos();
		}
		private void SetarFotos()       //Função que carrega as imagens nos pictureboxes, de acordo com a disponibilidade de uso (energias, invulnerabilidade). 
		{
			Alvo = "";
			HabUsada = "";
			Foto_HabUsada = null;
			HabFoiCompletada = false;

			Foto_MonstroHabUsada = null;
			MonstroHabUsada = "";

			picPersonagem.Image = Personagem_Foto;
			picMonstro.Image = MonstroFoto;

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
				picPersonagem.Image = Personagem_Foto_Indisponivel;
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

				lblMonstroDanoTurno.Text = Arena.RetornaPersonagemDanoTurno().ToString() + " de Dano por " + Arena.RetornaPersonagemTurnosDano().ToString() +" turnos";
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
				picMonstro.Image = MonstroFoto_Indisponivel;
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
					picHab1.Image = Hab1_Foto;
				}
				else
				{
					Hab1PodeUsar = false;
					picHab1.Image = Hab1_Foto_Indisponivel;
				}
			}
			else
			{
				lblPersonagemCDHab1.Visible = true;
				lblPersonagemCDHab1.Text = Arena.RetornaPersonagemCD("Hab1").ToString();

				Hab1PodeUsar = false;
				picHab1.Image = Hab1_Foto_Indisponivel;
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
					picHab2.Image = Hab2_Foto;
				}
				else
				{
					Hab2PodeUsar = false;
					picHab2.Image = Hab2_Foto_Indisponivel;
				}
			}
			else
			{
				lblPersonagemCDHab2.Visible = true;
				lblPersonagemCDHab2.Text = Arena.RetornaPersonagemCD("Hab2").ToString();

				Hab2PodeUsar = false;
				picHab2.Image = Hab2_Foto_Indisponivel;
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
					picHab3.Image = Hab3_Foto;
				}
				else
				{
					Hab3PodeUsar = false;
					picHab3.Image = Hab3_Foto_Indisponivel;
				}
			}
			else
			{
				lblPersonagemCDHab3.Visible = true;
				lblPersonagemCDHab3.Text = Arena.RetornaPersonagemCD("Hab3").ToString();

				Hab3PodeUsar = false;
				picHab3.Image = Hab3_Foto_Indisponivel;
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
					picHab4.Image = Hab4_Foto;
				}
				else
				{
					Hab4PodeUsar = false;
					picHab4.Image = Hab4_Foto_Indisponivel;
				}
			}
			else
			{
				lblPersonagemCDHab4.Visible = true;
				lblPersonagemCDHab4.Text = Arena.RetornaPersonagemCD("Hab4").ToString();

				Hab4PodeUsar = false;
				picHab4.Image = Hab4_Foto_Indisponivel;
			}
			#endregion
		}
		private void GerarEnergiaAleatoria()    //Função que gera uma nova energia na Classe Arena. No fim, chama a função SetarEnergia. 
		{
			Arena.GerarEnergia(EnergiasPorRound);
			SetarEnergia();
		}
        private void UsarHabilidade(string Habilidade)	//Função que faz a habilidade do personagem acontecer. Faz o Dano, Cura, Armadura, Energia Gasta, Energia Ganha, CDs, Invulnerabilidade. Tudo do personagem. 
        {
            if (HabFoiCompletada)
            {
				//Dano
				if (Arena.RetornaHabDano(Habilidade) > 0 || Arena.RetornaHabDanoPerfurante(Habilidade) > 0 || Arena.RetornaHabDanoVerdadeiro(Habilidade) > 0)
				{
					prgBarMonstroVida.Value = Arena.AtacarMonstroVida(Arena.RetornaHabDano(Habilidade), Arena.RetornaHabDanoPerfurante(Habilidade), Arena.RetornaHabDanoVerdadeiro(Habilidade), prgBarMonstroVida.Minimum);
					lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
					lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0);
				}
				//Cura
				if (Arena.RetornaHabCura(Habilidade) > 0)
				{
					prgBarPersonagemVida.Value = Arena.CurarPersonagemVida(Arena.RetornaHabCura(Habilidade), prgBarPersonagemVida.Maximum);
					lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
				}
				//Armadura
				if (Arena.RetornaHabArmadura(Habilidade) > 0)
				{
					lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(Arena.RetornaHabArmadura(Habilidade));
				}

				Arena.SetarHabilidadePorTurno(Habilidade);

				Arena.TirarEnergiaVerde(Arena.RetornaHabVerdes(Habilidade));
				Arena.TirarEnergiaAzul(Arena.RetornaHabAzuls(Habilidade));
				Arena.TirarEnergiaVermelha(Arena.RetornaHabVermelhos(Habilidade));

				Arena.PorEnergiaVerde(Arena.RetornaHabVerdesGanhos(Habilidade));
				Arena.PorEnergiaAzul(Arena.RetornaHabAzulsGanhos(Habilidade));
				Arena.PorEnergiaVermelha(Arena.RetornaHabVermelhosGanhos(Habilidade));
				Arena.PorEnergiaAleatoria(Arena.RetornaHabPretosGanhos(Habilidade));

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
				Arena.SetarCDSPersonagem(Habilidade);
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
                    Foto_MonstroHabUsada = MonstroHab1_Foto;
                    break;
                case "Hab2":
                    Foto_MonstroHabUsada = MonstroHab2_Foto;
                    break;
                case "Hab3":
                    Foto_MonstroHabUsada = MonstroHab3_Foto;
                    break;
                case "Hab4":
                    Foto_MonstroHabUsada = MonstroHab4_Foto;
                    break;
                case "Pass":
					break;
                default:
                    throw new Exception("Erro no método EscolherHabilidadeMosntro!\nO Switch retornou o valor default.");
            }

			if (Hab != "Pass")
			{
				if (Arena.RetornaMonstro_HabDano(Hab) > 0 || Arena.RetornaMonstro_HabDanoPerfurante(Hab) > 0 || Arena.RetornaMonstro_HabDanoVerdadeiro(Hab) > 0) { MonstroAlvo = "Personagem"; }
				else { MonstroAlvo = "Monstro"; } 
			}

            MonstroHabUsada = Hab;
        }
        private void UsarMonstroHabilidade()	//Função que  faz a habilidade do monstro acontecer. Faz o Dano, Cura, Armadura, CDs, Invulnerabilidade. Tudo do Monstro. 
        {
			if (MonstroHabUsada != "Pass")
			{
				if (Arena.RetornaMonstro_HabDano(MonstroHabUsada) > 0 || Arena.RetornaMonstro_HabDanoPerfurante(MonstroHabUsada) > 0 || Arena.RetornaMonstro_HabDanoVerdadeiro(MonstroHabUsada) > 0)
				{
					prgBarPersonagemVida.Value = Arena.AtacarPersonagemVida(Arena.RetornaMonstro_HabDano(MonstroHabUsada), Arena.RetornaMonstro_HabDanoPerfurante(MonstroHabUsada), Arena.RetornaMonstro_HabDanoVerdadeiro(MonstroHabUsada), prgBarPersonagemVida.Minimum);
					lblPersonagemVida.Text = prgBarPersonagemVida.Value.ToString() + "/" + prgBarPersonagemVida.Maximum.ToString();
					lblPersonagemArmadura.Text = "Armadura: " + Arena.SetarPersonagemArmadura(0);
				}
				if (Arena.RetornaMonstro_HabCura(MonstroHabUsada) > 0)
				{
					prgBarMonstroVida.Value = Arena.CurarMonstroVida(Arena.RetornaMonstro_HabCura(MonstroHabUsada), prgBarMonstroVida.Maximum);
					lblMonstroVida.Text = prgBarMonstroVida.Value.ToString() + "/" + prgBarMonstroVida.Maximum.ToString();
				}
				if (Arena.RetornaMonstro_HabArmadura(MonstroHabUsada) > 0)
				{
					lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(Arena.RetornaMonstro_HabArmadura(MonstroHabUsada));
				}

				Arena.SetarMonstroHabilidadePorTurno(MonstroHabUsada);

				Arena.SetarCDSMonstro(MonstroHabUsada);
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

            picHab1.Image = Hab1_Foto_Indisponivel;
            picHab2.Image = Hab2_Foto_Indisponivel;
            picHab3.Image = Hab3_Foto_Indisponivel;
            picHab4.Image = Hab4_Foto_Indisponivel;

            lblPersonagemCDHab1.Visible = false;
            lblPersonagemCDHab2.Visible = false;
            lblPersonagemCDHab3.Visible = false;
            lblPersonagemCDHab4.Visible = false;

            Timer.Start();

            Random rnd = new Random();

            Tempo = rnd.Next(8, TempoMaxPassando);

			t = new Thread(EscolherHabilidadeMosntro);
			t.Start();
		
        }

        private void Timer_Tick(object sender, EventArgs e)	//A cada 500ms do timer, diminui 1 value do prgbar. Quando chega no tempo desejado, Para o timer, ativa botões de pronto e Chama a função FazerMonstro. 
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

        private void FazerMonstro()	//Chama a função EscolherHabilidadeMosntro. Chama a função CarregaInformacoes com as info da habilidade que o monstro usou. Inicia o frmTurno. Ao sair do frmTurno, diminui o numero de turnos invulneraveis do personagem, diminui o cds do monstro, chama a função UsarMonstroHabilidade e depois VerificaSeJogoAcabou. Por fim, chama a função GerarEnergiaAleatoria. 
		{
			while (t.IsAlive)
			{
				MessageBox.Show("a");
			}

            CarregaInformacoes("Monstro" + MonstroHabUsada);
            frmTurno Turno = new frmTurno(Foto_MonstroHabUsada, Personagem_Foto, MonstroFoto, MonstroAlvo, "Monstro");
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
        private void btnPronto_Click(object sender, EventArgs e)	//Evento click do btnPronto. Carrega localmente o número de energias da habilidade usada. Instancia o frmTurno. Se foram usados pretos, diminui as energias escolhidas. Diminui o número de turnos invulneráveis do monstro, diminui o cds do Personagem, chama a função UsarHabilidade, SetarEnergia, VerificaJogoAcabou e PassarTempo. 
        {
            int AleatoriosHab = 0;
            int VerdesHab = 0;
            int AzuisHab = 0;
            int VermelhosHab = 0;
            if (HabFoiCompletada)
            {
                AleatoriosHab = Arena.RetornaPretosHab(HabUsada);
				if (HabUsada != "Pass")
				{
					VerdesHab = Arena.RetornaHabVerdes(HabUsada);
					AzuisHab = Arena.RetornaHabAzuls(HabUsada);
					VermelhosHab = Arena.RetornaHabVermelhos(HabUsada);
				}
            }

            frmTurno Turno = new frmTurno(Foto_HabUsada, Personagem_Foto, MonstroFoto, Alvo, "Personagem", AleatoriosHab, (Arena.RetornaVerdes() - VerdesHab), (Arena.RetornaAzuls() - AzuisHab), (Arena.RetornaVermelhos() - VermelhosHab));



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
                UsarHabilidade(HabUsada);
				UsarHabilidadesPorTurno();
				SetarEnergia();
                VerificaJogoAcabou();
                PassarTempo();


				lblMonstroArmadura.Text = "Armadura: " + Arena.SetarMonstroArmadura(0).ToString();
			}
        }
		private void btnTrocarEnergia_Click(object sender, EventArgs e)	//Evento click do btnTrocarEnergia. Carrega localmente o número de energias que o personagem tem, e se tem o número mínimo para a troca, inicia o frmTurno. Tira as energias gastas e coloca as energias ganhas. No fim, chama a função SetarEnegia. 
		{
			int Verdes = Arena.RetornaVerdes();
			int Azuis = Arena.RetornaAzuls();
			int Vermelhos = Arena.RetornaVermelhos();

			if (Verdes >= EnergiasIguaisMinimasParaTroca || Azuis >= EnergiasIguaisMinimasParaTroca || Vermelhos >= EnergiasIguaisMinimasParaTroca)
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
				MessageBox.Show("Você precisa ter no mínimo " + EnergiasIguaisMinimasParaTroca.ToString() + " Energias iguais para trocar!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                picPersonagem.Image = Personagem_Foto;
            }

            if (MonstroInvulneravel == false)
            {
                picMonstro.Image = MonstroFoto;
            }

            #region Personagem
            if (((Control)sender).Name == "picPersonagem")
            {
                CarregaInformacoes("Personagem");

                if (Alvo == "Personagem" && PassandoTempo == false)
                {
                    HabFoiCompletada = true;

                    Hab1PodeUsar = false;
                    Hab2PodeUsar = false;
                    Hab3PodeUsar = false;
                    Hab4PodeUsar = false;

                    switch (HabUsada)
                    {
                        case "Hab1":
                            Foto_HabUsada = Hab1_Foto;
                            picHab1.Visible = false;

                            picHab2.Image = Hab2_Foto_Indisponivel;
                            picHab3.Image = Hab3_Foto_Indisponivel;
                            picHab4.Image = Hab4_Foto_Indisponivel;
                            break;
                        case "Hab2":
                            Foto_HabUsada = Hab2_Foto;
                            picHab2.Visible = false;

                            picHab1.Image = Hab1_Foto_Indisponivel;
                            picHab3.Image = Hab3_Foto_Indisponivel;
                            picHab4.Image = Hab4_Foto_Indisponivel;
                            break;
                        case "Hab3":
                            Foto_HabUsada = Hab3_Foto;
                            picHab3.Visible = false;

                            picHab1.Image = Hab1_Foto_Indisponivel;
                            picHab2.Image = Hab2_Foto_Indisponivel;
                            picHab4.Image = Hab4_Foto_Indisponivel;
                            break;
                        case "Hab4":
                            Foto_HabUsada = Hab4_Foto;
                            picHab4.Visible = false;

                            picHab1.Image = Hab1_Foto_Indisponivel;
                            picHab2.Image = Hab2_Foto_Indisponivel;
                            picHab3.Image = Hab3_Foto_Indisponivel;
                            break;
                    }

                    picHabEscolhida.Image = Foto_HabUsada;
                }
                else
                {
                    Alvo = "";
                    HabUsada = "";
                    HabFoiCompletada = false;
                }
            }
            #endregion
            #region Hab1
            else if (((Control)sender).Name == "picHab1")
            {
                CarregaInformacoes("Hab1");
                if (Hab1PodeUsar == true && PassandoTempo == false)
                {
                    HabUsada = "Hab1";

                    picHab2.Visible = true;
                    picHab3.Visible = true;
                    picHab4.Visible = true;

					if (Arena.PersonagemHabAtaca("Hab1") == false)				 //Se a habilidade não causa dano, o alvo é o personagem.
					{
						Alvo = "Personagem";
						picPersonagem.Image = Personagem_Foto_Disponivel;
						picMonstro.Image = MonstroFoto_Indisponivel;
					}
					else
					{															//Se a habilidade causa dano:
						if (MonstroInvulneravel == false)						//Se o monstro não está invulnerável, o alvo é o monstro.
						{
							Alvo = "Monstro";
							picPersonagem.Image = Personagem_Foto_Indisponivel;
							picMonstro.Image = MonstroFoto_Disponivel;
						}
						else
						{														//Se o monstro está invulnerável, configura a habilidade para não fazer nada.
							Alvo = "";
							HabUsada = "";

							#region Código Obsoleto
							/*
							if (Arena.PersonagemHabDefende("Hab1"))
							{
								Alvo = "Personagem";
								picPersonagem.Image = Personagem_Foto_Disponivel;
								picMonstro.Image = MonstroFoto_Indisponivel;
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
                CarregaInformacoes("Hab2");
                if (Hab2PodeUsar == true && PassandoTempo == false)
                {
                    picHab1.Visible = true;
                    picHab3.Visible = true;
                    picHab4.Visible = true;

                    HabUsada = "Hab2";
					
                    if (Arena.PersonagemHabAtaca("Hab2") == false)
                    {
                        Alvo = "Personagem";
                        picPersonagem.Image = Personagem_Foto_Disponivel;
                        picMonstro.Image = MonstroFoto_Indisponivel;
                    }
                    else
                    {
                        if (MonstroInvulneravel == false)
                        {
                            Alvo = "Monstro";
                            picPersonagem.Image = Personagem_Foto_Indisponivel;
                            picMonstro.Image = MonstroFoto_Disponivel;
                        }
                        else
                        {
							Alvo = "";
							HabUsada = "";
                        }
                    }
                }
            }
            #endregion
            #region Hab3
            else if (((Control)sender).Name == "picHab3")
            {
                CarregaInformacoes("Hab3");
                if (Hab3PodeUsar == true && PassandoTempo == false)
                {
                    picHab1.Visible = true;
                    picHab2.Visible = true;
                    picHab4.Visible = true;

                    HabUsada = "Hab3";
					if (Arena.PersonagemHabAtaca("Hab3") == false)
					{
						Alvo = "Personagem";
						picPersonagem.Image = Personagem_Foto_Disponivel;
						picMonstro.Image = MonstroFoto_Indisponivel;
					}
					else
					{
						if (MonstroInvulneravel == false)
						{
							Alvo = "Monstro";
							picPersonagem.Image = Personagem_Foto_Indisponivel;
							picMonstro.Image = MonstroFoto_Disponivel;
						}
						else
						{
							Alvo = "";
							HabUsada = "";
						}
					}
				}
            }
            #endregion
            #region Hab4
            else if (((Control)sender).Name == "picHab4")
            {
                CarregaInformacoes("Hab4");
				if (Hab4PodeUsar == true && PassandoTempo == false)
				{
					picHab1.Visible = true;
					picHab2.Visible = true;
					picHab3.Visible = true;

					HabUsada = "Hab4";
					if (Arena.PersonagemHabAtaca("Hab4") == false)
					{
						Alvo = "Personagem";
						picPersonagem.Image = Personagem_Foto_Disponivel;
						picMonstro.Image = MonstroFoto_Indisponivel;
					}
					else
					{
						if (MonstroInvulneravel == false)
						{
							Alvo = "Monstro";
							picPersonagem.Image = Personagem_Foto_Indisponivel;
							picMonstro.Image = MonstroFoto_Disponivel;
						}
						else
						{
							Alvo = "";
							HabUsada = "";
						}
					}
				}				
            }
            #endregion
            #region Monstro
            else if (((Control)sender).Name == "picMonstro")
            {
                CarregaInformacoes("Monstro");
                btnVerHabilidadesInimigo.Visible = true;

                if (Alvo == "Monstro" && PassandoTempo == false)
                {
                    HabFoiCompletada = true;

                    Hab1PodeUsar = false;
                    Hab2PodeUsar = false;
                    Hab3PodeUsar = false;
                    Hab4PodeUsar = false;

                    switch (HabUsada)
                    {
                        case "Hab1":
                            Foto_HabUsada = Hab1_Foto;
                            picHab1.Visible = false;

                            picHab2.Image = Hab2_Foto_Indisponivel;
                            picHab3.Image = Hab3_Foto_Indisponivel;
                            picHab4.Image = Hab4_Foto_Indisponivel;
                            break;
                        case "Hab2":
                            Foto_HabUsada = Hab2_Foto;
                            picHab2.Visible = false;

                            picHab1.Image = Hab1_Foto_Indisponivel;
                            picHab3.Image = Hab3_Foto_Indisponivel;
                            picHab4.Image = Hab4_Foto_Indisponivel;
                            break;
                        case "Hab3":
                            Foto_HabUsada = Hab3_Foto;
                            picHab3.Visible = false;

                            picHab1.Image = Hab1_Foto_Indisponivel;
                            picHab2.Image = Hab2_Foto_Indisponivel;
                            picHab4.Image = Hab4_Foto_Indisponivel;
                            break;
                        case "Hab4":
                            Foto_HabUsada = Hab4_Foto;
                            picHab4.Visible = false;

                            picHab1.Image = Hab1_Foto_Indisponivel;
                            picHab2.Image = Hab2_Foto_Indisponivel;
                            picHab3.Image = Hab3_Foto_Indisponivel;
                            break;
                    }

                    picHabEscolhida.Image = Foto_HabUsada;
                }
                else
                {
                    Alvo = "";
                    HabUsada = "";
                    HabFoiCompletada = false;
                }
            }
            #endregion
            #region picHabEscolhida
            else if (((Control)sender).Name == "picHabEscolhida")
            {
                Alvo = "";
                if (HabFoiCompletada == true)
                {
                    CarregaInformacoes(HabUsada);
                }
                else
                {
                    CarregaInformacoes("");
                }
            }
            #endregion
            #region picMonstroHabEscolhida
            else if (((Control)sender).Name == "picMonstroHabEscolhida")
            {
                Alvo = "";
                CarregaInformacoes("");
            }
            #endregion
        }

        private void picHabEscolhida_DoubleClick(object sender, EventArgs e) //Evento DoubleClick no picHabEscolhida. Essa função cancela a seleção de habilidade. 
        {
            SetarFotos();
            CarregaInformacoes("");
        }

		#region grpInformações
		private void picVerHabs_Click(object sender, EventArgs e)
		{
			if (((Control)sender).Name == "btnVerHabilidadesInimigo")
			{
				picInfoHabSelecionada.Image = MonstroFoto;

				btnVerHabilidadesInimigo.Visible = false;
				txtInfoDescricao.Visible = false;

				picVerHab1.Image = MonstroHab1_Foto;
				picVerHab2.Image = MonstroHab2_Foto;
				picVerHab3.Image = MonstroHab3_Foto;
				picVerHab4.Image = MonstroHab4_Foto;

				picVerHab1.Visible = true;
				picVerHab2.Visible = true;
				picVerHab3.Visible = true;
				picVerHab4.Visible = true;

				lblRecarga.Visible = false;
				lblInfoNome.Text = Arena.RetornaMonstro_Nome();

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

				if (((Control)sender).Name == "picVerHab1")
				{
					CarregaInformacoes("MonstroHab1");
				}
				else if (((Control)sender).Name == "picVerHab2")
				{
					CarregaInformacoes("MonstroHab2");
				}
				else if (((Control)sender).Name == "picVerHab3")
				{
					CarregaInformacoes("MonstroHab3");
				}
				else if (((Control)sender).Name == "picVerHab4")
				{
					CarregaInformacoes("MonstroHab4");
				}
			}
		}
		private void CarregaInformacoes(string nomeInformacao)
		{
			for (int i = 0; i < Grp_Info_Pics.Length; i++)
			{
				Grp_Info_Pics[i].Visible = false;
			}

			if (nomeInformacao == "Personagem")
			{
				picInfoHabSelecionada.Image = Personagem_Foto;
				lblInfoNome.Text = Arena.RetornaPersonagemNome();
				txtInfoDescricao.Text = Arena.RetornaPersonagemDescricao();

				lblEnergia.Visible = false;

				lblRecarga.Visible = false;
			}
			else if (nomeInformacao == "Monstro")
			{
				picInfoHabSelecionada.Image = MonstroFoto;
				lblInfoNome.Text = Arena.RetornaMonstro_Nome();
				txtInfoDescricao.Text = Arena.RetornaMonstro_Descricao();

				lblEnergia.Visible = false;

				lblRecarga.Visible = false;
			}
			else if (nomeInformacao == "")
			{
				picInfoHabSelecionada.Image = Properties.Resources.Ponto_de_interrogacao;
				lblInfoNome.Text = "Você está em combate!";
				txtInfoDescricao.Text = "Você está no campo de batalha!";

				lblEnergia.Visible = false;
				lblRecarga.Visible = false;
			}
			else if (nomeInformacao == "MonstroPass")
			{
				picInfoHabSelecionada.Image = Properties.Resources.Ponto_de_interrogacao;
				lblInfoNome.Text = "O Monstro passou o turno!";
				txtInfoDescricao.Text = "O Monstro passou o turno!\nTome cuidado!";

				lblEnergia.Visible = false;
				lblRecarga.Visible = false;
			}
			else
			{
				lblEnergia.Visible = true;
				lblRecarga.Visible = true;

				if (!(nomeInformacao.Contains("Monstro")))
				{
					lblInfoNome.Text = Arena.RetornaHabNome(nomeInformacao);
					txtInfoDescricao.Text = Arena.RetornaHabDescricao(nomeInformacao);

					if (Arena.RetornaHabRecarga(nomeInformacao) == 1)
					{
						lblRecarga.Text = "TEMPO DE RECARGA: 1 TURNO";
					}
					else
					{
						lblRecarga.Text = "TEMPO DE RECARGA: " + Arena.RetornaHabRecarga(nomeInformacao).ToString() + " TURNOS";
					}
					if (nomeInformacao == "Hab1")
					{
						picInfoHabSelecionada.Image = Hab1_Foto;
					}
					else if (nomeInformacao == "Hab2")
					{
						picInfoHabSelecionada.Image = Hab2_Foto;
					}
					else if (nomeInformacao == "Hab3")
					{
						picInfoHabSelecionada.Image = Hab3_Foto;
					}
					else if (nomeInformacao == "Hab4")
					{
						picInfoHabSelecionada.Image = Hab4_Foto;
					}

					CarregaPicsEnergia(nomeInformacao);
				}
				else
				{
					lblInfoNome.Text = Arena.RetornaMonstro_HabNome(nomeInformacao);
					txtInfoDescricao.Text = Arena.RetornaMonstro_HabDescricao(nomeInformacao);
					lblEnergia.Visible = false;

					if (Arena.RetornaMonstro_HabRecarga(nomeInformacao) == 1) {	lblRecarga.Text = "TEMPO DE RECARGA: 1 TURNO";	}
					else { lblRecarga.Text = "TEMPO DE RECARGA: " + Arena.RetornaMonstro_HabRecarga(nomeInformacao).ToString() + " TURNOS"; }

					if (nomeInformacao == "MonstroHab1")
					{
						picInfoHabSelecionada.Image = MonstroHab1_Foto;
					}
					else if (nomeInformacao == "MonstroHab2")
					{
						picInfoHabSelecionada.Image = MonstroHab2_Foto;
					}
					else if (nomeInformacao == "MonstroHab3")
					{
						picInfoHabSelecionada.Image = MonstroHab3_Foto;
					}
					else if (nomeInformacao == "MonstroHab4")
					{
						picInfoHabSelecionada.Image = MonstroHab4_Foto;
					}
				}
			}
		}
        private void CarregaPicsEnergia(string nomeHabilidade)
        {
            int VerdesNecessarios = 0, AzulsNecessarios = 0, VermelhosNecessarios = 0, PretosNecessarios = 0;

			VerdesNecessarios = Arena.RetornaHabVerdes(nomeHabilidade);
			AzulsNecessarios = Arena.RetornaHabAzuls(nomeHabilidade);
			VermelhosNecessarios = Arena.RetornaHabVermelhos(nomeHabilidade);
			PretosNecessarios = Arena.RetornaHabPretos(nomeHabilidade);			

            int TotalEnergias = (VerdesNecessarios + AzulsNecessarios + VermelhosNecessarios + PretosNecessarios);

            if (TotalEnergias > 0)
            {
                lblEnergia.Text = "ENERGIA:";
                for (int i = 0; i < VerdesNecessarios; i++)
                {
                    Grp_Info_Pics[i].Image = Properties.Resources.Verde;
                }
                for (int i = 0; i < AzulsNecessarios; i++)
                {
                    Grp_Info_Pics[(VerdesNecessarios + i)].Image = Properties.Resources.Azul;
                }
                for (int i = 0; i < VermelhosNecessarios; i++)
                {
                    Grp_Info_Pics[(VerdesNecessarios + AzulsNecessarios + i)].Image = Properties.Resources.Vermelho;
                }
                for (int i = 0; i < PretosNecessarios; i++)
                {
                    Grp_Info_Pics[(VerdesNecessarios + AzulsNecessarios + VermelhosNecessarios + i)].Image = Properties.Resources.Preto;
                }

				#region Deixar os Pic de Energia visiveis ou não

				for (int i = 0; i < TotalEnergias; i++)
				{
					Grp_Info_Pics[i].Visible = true;
				}				
				#endregion
			}
            else
            {
                lblEnergia.Text = "ENERGIA: NENHUMA";
            }
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
	}
}