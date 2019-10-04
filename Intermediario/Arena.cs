using System;

namespace Intermediario
{
	public class Arena
	{

		Random rnd = new Random();
		protected const int numIndex = 5;

		#region Atributos do Personagem

		protected int PersonagemVida = 100, PersonagemArmadura = 0;

		protected int PersonagemCDHab1, PersonagemCDHab2, PersonagemCDHab3, PersonagemCDHab4;

		protected int[] PersonagemTurnos_Hab1Dano = new int[numIndex], PersonagemTurnos_Hab2Dano = new int[numIndex], PersonagemTurnos_Hab3Dano = new int[numIndex], PersonagemTurnos_Hab4Dano = new int[numIndex];
		protected int[] PersonagemTurnos_Hab1DanoPerfurante = new int[numIndex], PersonagemTurnos_Hab2DanoPerfurante = new int[numIndex], PersonagemTurnos_Hab3DanoPerfurante = new int[numIndex], PersonagemTurnos_Hab4DanoPerfurante = new int[numIndex];
		protected int[] PersonagemTurnos_Hab1DanoVerdadeiro = new int[numIndex], PersonagemTurnos_Hab2DanoVerdadeiro = new int[numIndex], PersonagemTurnos_Hab3DanoVerdadeiro = new int[numIndex], PersonagemTurnos_Hab4DanoVerdadeiro = new int[numIndex];
		protected int[] PersonagemTurnos_Hab1Cura = new int[numIndex], PersonagemTurnos_Hab2Cura = new int[numIndex], PersonagemTurnos_Hab3Cura = new int[numIndex], PersonagemTurnos_Hab4Cura = new int[numIndex];
		protected int[] PersonagemTurnos_Hab1Armadura = new int[numIndex], PersonagemTurnos_Hab2Armadura = new int[numIndex], PersonagemTurnos_Hab3Armadura = new int[numIndex], PersonagemTurnos_Hab4Armadura = new int[numIndex];
		protected int[] PersonagemTurnos_Hab1Invulneravel = new int[numIndex], PersonagemTurnos_Hab2Invulneravel = new int[numIndex], PersonagemTurnos_Hab3Invulneravel = new int[numIndex], PersonagemTurnos_Hab4Invulneravel = new int[numIndex];

		#region Atributos Personagem
		protected int PersonagemID;
		protected string PersonagemNome, PersonagemDescricao;
		protected byte[] PersonagemFoto;
		#endregion
		#region Atributos Hab1
		protected string Hab1Nome, Hab1Descricao;
		protected byte[] Hab1Foto;
		protected int Hab1Dano, Hab1DanoPorTurno, Hab1DanoPorTurno_Turnos,
		Hab1DanoPerfurante, Hab1DanoPerfurantePorTurno, Hab1DanoPerfurantePorTurno_Turnos,
		Hab1DanoVerdadeiro, Hab1DanoVerdadeiroPorTurno, Hab1DanoVerdadeiroPorTurno_Turnos,
		Hab1Cura, Hab1CuraPorTurno, Hab1CuraPorTurno_Turnos,
		Hab1Armadura, Hab1ArmaduraPorTurno, Hab1ArmaduraPorTurno_Turnos,
		Hab1Recarga,
		Hab1Verdes, Hab1Azuls, Hab1Vermelhos, Hab1Pretos,
		Hab1Invulnerabilidade,
		Hab1VerdesGanhos, Hab1AzulsGanhos, Hab1VermelhosGanhos, Hab1PretosGanhos;
		#endregion
		#region Atributos Hab2
		protected string Hab2Nome, Hab2Descricao;
		protected byte[] Hab2Foto;
		protected int Hab2Dano, Hab2DanoPorTurno, Hab2DanoPorTurno_Turnos,
		Hab2DanoPerfurante, Hab2DanoPerfurantePorTurno, Hab2DanoPerfurantePorTurno_Turnos,
		Hab2DanoVerdadeiro, Hab2DanoVerdadeiroPorTurno, Hab2DanoVerdadeiroPorTurno_Turnos,
		Hab2Cura, Hab2CuraPorTurno, Hab2CuraPorTurno_Turnos,
		Hab2Armadura, Hab2ArmaduraPorTurno, Hab2ArmaduraPorTurno_Turnos,
		Hab2Recarga,
		Hab2Verdes, Hab2Azuls, Hab2Vermelhos, Hab2Pretos,
		Hab2Invulnerabilidade,
		Hab2VerdesGanhos, Hab2AzulsGanhos, Hab2VermelhosGanhos, Hab2PretosGanhos;
		#endregion
		#region Atributos Hab3
		protected string Hab3Nome, Hab3Descricao;
		protected byte[] Hab3Foto;
		protected int Hab3Dano, Hab3DanoPorTurno, Hab3DanoPorTurno_Turnos,
		Hab3DanoPerfurante, Hab3DanoPerfurantePorTurno, Hab3DanoPerfurantePorTurno_Turnos,
		Hab3DanoVerdadeiro, Hab3DanoVerdadeiroPorTurno, Hab3DanoVerdadeiroPorTurno_Turnos,
		Hab3Cura, Hab3CuraPorTurno, Hab3CuraPorTurno_Turnos,
		Hab3Armadura, Hab3ArmaduraPorTurno, Hab3ArmaduraPorTurno_Turnos,
		Hab3Recarga,
		Hab3Verdes, Hab3Azuls, Hab3Vermelhos, Hab3Pretos,
		Hab3Invulnerabilidade,
		Hab3VerdesGanhos, Hab3AzulsGanhos, Hab3VermelhosGanhos, Hab3PretosGanhos;
		#endregion
		#region Atributos Hab4
		protected string Hab4Nome, Hab4Descricao;
		protected byte[] Hab4Foto;
		protected int Hab4Dano, Hab4DanoPorTurno, Hab4DanoPorTurno_Turnos,
		Hab4DanoPerfurante, Hab4DanoPerfurantePorTurno, Hab4DanoPerfurantePorTurno_Turnos,
		Hab4DanoVerdadeiro, Hab4DanoVerdadeiroPorTurno, Hab4DanoVerdadeiroPorTurno_Turnos,
		Hab4Cura, Hab4CuraPorTurno, Hab4CuraPorTurno_Turnos,
		Hab4Armadura, Hab4ArmaduraPorTurno, Hab4ArmaduraPorTurno_Turnos,
		Hab4Recarga,
		Hab4Verdes, Hab4Azuls, Hab4Vermelhos, Hab4Pretos,
		Hab4Invulnerabilidade,
		Hab4VerdesGanhos, Hab4AzulsGanhos, Hab4VermelhosGanhos, Hab4PretosGanhos;
		#endregion

		#endregion

		#region Métodos do Personagem

		public int AtacarPersonagemVida(int Dano, int DanoPerfurante, int DanoVerdadeiro, int Minimum)
		{
			if (PersonagemVida - DanoVerdadeiro < Minimum)
			{
				PersonagemVida = Minimum;
			}
			else
			{
				PersonagemVida -= DanoVerdadeiro;

				if (PersonagemVida - DanoPerfurante < Minimum)
				{
					PersonagemVida = Minimum;
				}
				else if (DanoPerfurante > 0)
				{
					PersonagemVida -= DanoPerfurante;
					PersonagemArmadura = 0;
				}

				if ((PersonagemVida + PersonagemArmadura) - Dano < Minimum)
				{
					PersonagemVida = Minimum;
				}
				else
				{
					if (PersonagemArmadura - Dano > 0)
					{
						PersonagemArmadura -= Dano;
					}
					else
					{
						PersonagemVida -= (Dano - PersonagemArmadura);
						PersonagemArmadura = 0;
					}
				}
			}
			return PersonagemVida;
		}
		public int CurarPersonagemVida(int Cura, int Maximum)
		{
			if (PersonagemVida + Cura > Maximum)
			{
				PersonagemVida = Maximum;
			}
			else
			{
				PersonagemVida += Cura;
			}
			return PersonagemVida;
		}
		public int SetarPersonagemArmadura(int Armadura)
		{
			PersonagemArmadura += Armadura;

			return PersonagemArmadura;
		}

		public bool PersonagemHabAtaca(string QualHab)
		{
			if (QualHab == "Hab1")
			{
				if (Hab1Dano > 0 || Hab1DanoVerdadeiro > 0 || Hab1DanoPerfurante > 0 || Hab1DanoPorTurno > 0 || Hab1DanoPerfurantePorTurno > 0 || Hab1DanoVerdadeiroPorTurno > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (QualHab == "Hab2")
			{
				if (Hab2Dano > 0 || Hab2DanoVerdadeiro > 0 || Hab2DanoPerfurante > 0 || Hab2DanoPorTurno > 0 || Hab2DanoPerfurantePorTurno > 0 || Hab2DanoVerdadeiroPorTurno > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (QualHab == "Hab3")
			{
				if (Hab3Dano > 0 || Hab3DanoVerdadeiro > 0 || Hab3DanoPerfurante > 0 || Hab3DanoPorTurno > 0 || Hab3DanoPerfurantePorTurno > 0 || Hab3DanoVerdadeiroPorTurno > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else if (QualHab == "Hab4")
			{
				if (Hab4Dano > 0 || Hab4DanoVerdadeiro > 0 || Hab4DanoPerfurante > 0 || Hab4DanoPorTurno > 0 || Hab4DanoPerfurantePorTurno > 0 || Hab4DanoVerdadeiroPorTurno > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				throw new Exception("Erro no método PersonagemHabAtaca");
			}
		}
		public bool PersonagemHabDefende(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					if (Hab1Cura > 0 || Hab1Armadura > 0 || Hab1CuraPorTurno > 0 || Hab1ArmaduraPorTurno > 0)
					{
						return true;
					}
					else
					{
						return false;
					}

				case "Hab2":
					if (Hab2Cura > 0 || Hab2Armadura > 0 || Hab2CuraPorTurno > 0 || Hab2ArmaduraPorTurno > 0)
					{
						return true;
					}
					else
					{
						return false;
					}

				case "Hab3":
					if (Hab3Cura > 0 || Hab3Armadura > 0 || Hab3CuraPorTurno > 0 || Hab3ArmaduraPorTurno > 0)
					{
						return true;
					}
					else
					{
						return false;
					}

				case "Hab4":
					if (Hab4Cura > 0 || Hab4Armadura > 0 || Hab4CuraPorTurno > 0 || Hab4ArmaduraPorTurno > 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				default:
					throw new Exception("Erro no método PersonagemHabDefende");
			}
		}

		public int RetornaPersonagemCD(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return PersonagemCDHab1;

				case "Hab2":
					return PersonagemCDHab2;

				case "Hab3":
					return PersonagemCDHab3;

				case "Hab4":
					return PersonagemCDHab4;

				default:
					throw new Exception("Erro no método RetornaPersonagemCD. O valor default foi retornado.");
			}
		}
		public void DiminuirCDSPersonagem()
		{
			if (PersonagemCDHab1 > 0)
			{
				PersonagemCDHab1 -= 1;
			}
			if (PersonagemCDHab2 > 0)
			{
				PersonagemCDHab2 -= 1;
			}
			if (PersonagemCDHab3 > 0)
			{
				PersonagemCDHab3 -= 1;
			}
			if (PersonagemCDHab4 > 0)
			{
				PersonagemCDHab4 -= 1;
			}
		}
		public void SetarCDSPersonagem(string Habilidade)
		{
			switch (Habilidade)
			{
				case "Hab1":
					PersonagemCDHab1 = RetornaHabRecarga(Habilidade);
					break;
				case "Hab2":
					PersonagemCDHab2 = RetornaHabRecarga(Habilidade);
					break;
				case "Hab3":
                    PersonagemCDHab3 = RetornaHabRecarga(Habilidade);
					break;
				case "Hab4":
					PersonagemCDHab4 = RetornaHabRecarga(Habilidade);
					break;
			}
		}

		public void SetarHabilidadePorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					#region Hab1

					if (Hab1DanoPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab1Dano[i] == 0)
							{
								PersonagemTurnos_Hab1Dano[i] += Hab1DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab1DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab1DanoPerfurante[i] == 0)
							{
								PersonagemTurnos_Hab1DanoPerfurante[i] += Hab1DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab1DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab1DanoVerdadeiro[i] == 0)
							{
								PersonagemTurnos_Hab1DanoVerdadeiro[i] += Hab1DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab1CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab1Cura[i] == 0)
							{
								PersonagemTurnos_Hab1Cura[i] += Hab1CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab1ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab1Armadura[i] == 0)
							{
								PersonagemTurnos_Hab1Armadura[i] += Hab1ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab1Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab1Invulneravel[i] == 0)
							{
								PersonagemTurnos_Hab1Invulneravel[i] += Hab1Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				case "Hab2":
					#region Hab2

					if (Hab2DanoPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab2Dano[i] == 0)
							{
								PersonagemTurnos_Hab2Dano[i] += Hab2DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab2DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab2DanoPerfurante[i] == 0)
							{
								PersonagemTurnos_Hab2DanoPerfurante[i] += Hab2DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab2DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab2DanoVerdadeiro[i] == 0)
							{
								PersonagemTurnos_Hab2DanoVerdadeiro[i] += Hab2DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab2CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab2Cura[i] == 0)
							{
								PersonagemTurnos_Hab2Cura[i] += Hab2CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab2ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab2Armadura[i] == 0)
							{
								PersonagemTurnos_Hab2Armadura[i] += Hab2ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab2Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab2Invulneravel[i] == 0)
							{
								PersonagemTurnos_Hab2Invulneravel[i] += Hab2Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				case "Hab3":
					#region Hab3

					if (Hab3DanoPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab3Dano[i] == 0)
							{
								PersonagemTurnos_Hab3Dano[i] += Hab3DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab3DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab3DanoPerfurante[i] == 0)
							{
								PersonagemTurnos_Hab3DanoPerfurante[i] += Hab3DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab3DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab3DanoVerdadeiro[i] == 0)
							{
								PersonagemTurnos_Hab3DanoVerdadeiro[i] += Hab3DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab3CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab3Cura[i] == 0)
							{
								PersonagemTurnos_Hab3Cura[i] += Hab3CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab3ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab3Armadura[i] == 0)
							{
								PersonagemTurnos_Hab3Armadura[i] += Hab3ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab3Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab3Invulneravel[i] == 0)
							{
								PersonagemTurnos_Hab3Invulneravel[i] += Hab3Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				case "Hab4":
					#region Hab4

					if (Hab4DanoPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab4Dano[i] == 0)
							{
								PersonagemTurnos_Hab4Dano[i] += Hab4DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab4DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab4DanoPerfurante[i] == 0)
							{
								PersonagemTurnos_Hab4DanoPerfurante[i] += Hab4DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab4DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab4DanoVerdadeiro[i] == 0)
							{
								PersonagemTurnos_Hab4DanoVerdadeiro[i] += Hab4DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab4CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab4Cura[i] == 0)
							{
								PersonagemTurnos_Hab4Cura[i] += Hab4CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab4ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab4Armadura[i] == 0)
							{
								PersonagemTurnos_Hab4Armadura[i] += Hab4ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Hab4Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (PersonagemTurnos_Hab4Invulneravel[i] == 0)
							{
								PersonagemTurnos_Hab4Invulneravel[i] += Hab4Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				default:
					throw new Exception("Erro no método SetarHabilidadePorTurno");
			}
		}
		public void DiminuirHabilidadesPorTurno()
		{

			/*				
			 *			Lógica do Diminuir Habilidades por Turno (do Personagem)
			 *			O for deve percorrer o array inteiro, pois pode acontecer de:
			 *			'O dano da hab1 dura 3 turnos, mas a cura dura 5.'
			 *			Um break aqui faria com o que os turnos da cura não diminuissem
			 *			Possível solução-desempenho: Um for para cada propriedade;
			 *			e.g.: Um For para a Hab1Dano, um For para Hab1Cura, um For para Hab2Dano, etc 
			 *			
			 *			Em 07/09/2018 foi decidido que a lógica atual está funcionando,
			 *			e que seria melhor do que um For para cada atributo.
			 *			-BeaKKeRR
			 */





			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1Dano[i] > 0) { PersonagemTurnos_Hab1Dano[i] -= 1; }
				if (PersonagemTurnos_Hab1DanoPerfurante[i] > 0) { PersonagemTurnos_Hab1DanoPerfurante[i] -= 1; }
				if (PersonagemTurnos_Hab1DanoVerdadeiro[i] > 0) { PersonagemTurnos_Hab1DanoVerdadeiro[i] -= 1; }
				if (PersonagemTurnos_Hab1Cura[i] > 0) { PersonagemTurnos_Hab1Cura[i] -= 1; }
				if (PersonagemTurnos_Hab1Armadura[i] > 0) { PersonagemTurnos_Hab1Armadura[i] -= 1; }
				if (MonstroTurnos_Hab1Invulneravel[i] > 0) { MonstroTurnos_Hab1Invulneravel[i] -= 1; }

				if (PersonagemTurnos_Hab2Dano[i] > 0) { PersonagemTurnos_Hab2Dano[i] -= 1; }
				if (PersonagemTurnos_Hab2DanoPerfurante[i] > 0) { PersonagemTurnos_Hab2DanoPerfurante[i] -= 1; }
				if (PersonagemTurnos_Hab2DanoVerdadeiro[i] > 0) { PersonagemTurnos_Hab2DanoVerdadeiro[i] -= 1; }
				if (PersonagemTurnos_Hab2Cura[i] > 0) { PersonagemTurnos_Hab2Cura[i] -= 1; }
				if (PersonagemTurnos_Hab2Armadura[i] > 0) { PersonagemTurnos_Hab2Armadura[i] -= 1; }
				if (MonstroTurnos_Hab2Invulneravel[i] > 0) { MonstroTurnos_Hab2Invulneravel[i] -= 1; }

				if (PersonagemTurnos_Hab3Dano[i] > 0) { PersonagemTurnos_Hab3Dano[i] -= 1; }
				if (PersonagemTurnos_Hab3DanoPerfurante[i] > 0) { PersonagemTurnos_Hab3DanoPerfurante[i] -= 1; }
				if (PersonagemTurnos_Hab3DanoVerdadeiro[i] > 0) { PersonagemTurnos_Hab3DanoVerdadeiro[i] -= 1; }
				if (PersonagemTurnos_Hab3Cura[i] > 0) { PersonagemTurnos_Hab3Cura[i] -= 1; }
				if (PersonagemTurnos_Hab3Armadura[i] > 0) { PersonagemTurnos_Hab3Armadura[i] -= 1; }
				if (MonstroTurnos_Hab3Invulneravel[i] > 0) { MonstroTurnos_Hab3Invulneravel[i] -= 1; }

				if (PersonagemTurnos_Hab4Dano[i] > 0) { PersonagemTurnos_Hab4Dano[i] -= 1; }
				if (PersonagemTurnos_Hab4DanoPerfurante[i] > 0) { PersonagemTurnos_Hab4DanoPerfurante[i] -= 1; }
				if (PersonagemTurnos_Hab4DanoVerdadeiro[i] > 0) { PersonagemTurnos_Hab4DanoVerdadeiro[i] -= 1; }
				if (PersonagemTurnos_Hab4Cura[i] > 0) { PersonagemTurnos_Hab4Cura[i] -= 1; }
				if (PersonagemTurnos_Hab4Armadura[i] > 0) { PersonagemTurnos_Hab4Armadura[i] -= 1; }
				if (MonstroTurnos_Hab4Invulneravel[i] > 0) { MonstroTurnos_Hab4Invulneravel[i] -= 1; }
			}

			OrganizarArrayPorTurno();
		}
		private void OrganizarArrayPorTurno()
		{
			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1Dano[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab1Dano[0] = PersonagemTurnos_Hab1Dano[1];
						PersonagemTurnos_Hab1Dano[1] = PersonagemTurnos_Hab1Dano[2];
						PersonagemTurnos_Hab1Dano[2] = PersonagemTurnos_Hab1Dano[3];
						PersonagemTurnos_Hab1Dano[3] = PersonagemTurnos_Hab1Dano[4];
						PersonagemTurnos_Hab1Dano[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab1Dano[1] = PersonagemTurnos_Hab1Dano[2];
						PersonagemTurnos_Hab1Dano[2] = PersonagemTurnos_Hab1Dano[3];
						PersonagemTurnos_Hab1Dano[3] = PersonagemTurnos_Hab1Dano[4];
						PersonagemTurnos_Hab1Dano[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab1Dano[2] = PersonagemTurnos_Hab1Dano[3];
						PersonagemTurnos_Hab1Dano[3] = PersonagemTurnos_Hab1Dano[4];
						PersonagemTurnos_Hab1Dano[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab1Dano[3] = PersonagemTurnos_Hab1Dano[4];
						PersonagemTurnos_Hab1Dano[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab1DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab1DanoPerfurante[0] = PersonagemTurnos_Hab1DanoPerfurante[1];
						PersonagemTurnos_Hab1DanoPerfurante[1] = PersonagemTurnos_Hab1DanoPerfurante[2];
						PersonagemTurnos_Hab1DanoPerfurante[2] = PersonagemTurnos_Hab1DanoPerfurante[3];
						PersonagemTurnos_Hab1DanoPerfurante[3] = PersonagemTurnos_Hab1DanoPerfurante[4];
						PersonagemTurnos_Hab1DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab1DanoPerfurante[1] = PersonagemTurnos_Hab1DanoPerfurante[2];
						PersonagemTurnos_Hab1DanoPerfurante[2] = PersonagemTurnos_Hab1DanoPerfurante[3];
						PersonagemTurnos_Hab1DanoPerfurante[3] = PersonagemTurnos_Hab1DanoPerfurante[4];
						PersonagemTurnos_Hab1DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab1DanoPerfurante[2] = PersonagemTurnos_Hab1DanoPerfurante[3];
						PersonagemTurnos_Hab1DanoPerfurante[3] = PersonagemTurnos_Hab1DanoPerfurante[4];
						PersonagemTurnos_Hab1DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab1DanoPerfurante[3] = PersonagemTurnos_Hab1DanoPerfurante[4];
						PersonagemTurnos_Hab1DanoPerfurante[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab1DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab1DanoVerdadeiro[0] = PersonagemTurnos_Hab1DanoVerdadeiro[1];
						PersonagemTurnos_Hab1DanoVerdadeiro[1] = PersonagemTurnos_Hab1DanoVerdadeiro[2];
						PersonagemTurnos_Hab1DanoVerdadeiro[2] = PersonagemTurnos_Hab1DanoVerdadeiro[3];
						PersonagemTurnos_Hab1DanoVerdadeiro[3] = PersonagemTurnos_Hab1DanoVerdadeiro[4];
						PersonagemTurnos_Hab1DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab1DanoVerdadeiro[1] = PersonagemTurnos_Hab1DanoVerdadeiro[2];
						PersonagemTurnos_Hab1DanoVerdadeiro[2] = PersonagemTurnos_Hab1DanoVerdadeiro[3];
						PersonagemTurnos_Hab1DanoVerdadeiro[3] = PersonagemTurnos_Hab1DanoVerdadeiro[4];
						PersonagemTurnos_Hab1DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab1DanoVerdadeiro[2] = PersonagemTurnos_Hab1DanoVerdadeiro[3];
						PersonagemTurnos_Hab1DanoVerdadeiro[3] = PersonagemTurnos_Hab1DanoVerdadeiro[4];
						PersonagemTurnos_Hab1DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab1DanoVerdadeiro[3] = PersonagemTurnos_Hab1DanoVerdadeiro[4];
						PersonagemTurnos_Hab1DanoVerdadeiro[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab1Cura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab1Cura[0] = PersonagemTurnos_Hab1Cura[1];
						PersonagemTurnos_Hab1Cura[1] = PersonagemTurnos_Hab1Cura[2];
						PersonagemTurnos_Hab1Cura[2] = PersonagemTurnos_Hab1Cura[3];
						PersonagemTurnos_Hab1Cura[3] = PersonagemTurnos_Hab1Cura[4];
						PersonagemTurnos_Hab1Cura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab1Cura[1] = PersonagemTurnos_Hab1Cura[2];
						PersonagemTurnos_Hab1Cura[2] = PersonagemTurnos_Hab1Cura[3];
						PersonagemTurnos_Hab1Cura[3] = PersonagemTurnos_Hab1Cura[4];
						PersonagemTurnos_Hab1Cura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab1Cura[2] = PersonagemTurnos_Hab1Cura[3];
						PersonagemTurnos_Hab1Cura[3] = PersonagemTurnos_Hab1Cura[4];
						PersonagemTurnos_Hab1Cura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab1Cura[3] = PersonagemTurnos_Hab1Cura[4];
						PersonagemTurnos_Hab1Cura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab1Armadura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab1Armadura[0] = PersonagemTurnos_Hab1Armadura[1];
						PersonagemTurnos_Hab1Armadura[1] = PersonagemTurnos_Hab1Armadura[2];
						PersonagemTurnos_Hab1Armadura[2] = PersonagemTurnos_Hab1Armadura[3];
						PersonagemTurnos_Hab1Armadura[3] = PersonagemTurnos_Hab1Armadura[4];
						PersonagemTurnos_Hab1Armadura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab1Armadura[1] = PersonagemTurnos_Hab1Armadura[2];
						PersonagemTurnos_Hab1Armadura[2] = PersonagemTurnos_Hab1Armadura[3];
						PersonagemTurnos_Hab1Armadura[3] = PersonagemTurnos_Hab1Armadura[4];
						PersonagemTurnos_Hab1Armadura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab1Armadura[2] = PersonagemTurnos_Hab1Armadura[3];
						PersonagemTurnos_Hab1Armadura[3] = PersonagemTurnos_Hab1Armadura[4];
						PersonagemTurnos_Hab1Armadura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab1Armadura[3] = PersonagemTurnos_Hab1Armadura[4];
						PersonagemTurnos_Hab1Armadura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab1Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab1Invulneravel[0] = PersonagemTurnos_Hab1Invulneravel[1];
						PersonagemTurnos_Hab1Invulneravel[1] = PersonagemTurnos_Hab1Invulneravel[2];
						PersonagemTurnos_Hab1Invulneravel[2] = PersonagemTurnos_Hab1Invulneravel[3];
						PersonagemTurnos_Hab1Invulneravel[3] = PersonagemTurnos_Hab1Invulneravel[4];
						PersonagemTurnos_Hab1Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab1Invulneravel[1] = PersonagemTurnos_Hab1Invulneravel[2];
						PersonagemTurnos_Hab1Invulneravel[2] = PersonagemTurnos_Hab1Invulneravel[3];
						PersonagemTurnos_Hab1Invulneravel[3] = PersonagemTurnos_Hab1Invulneravel[4];
						PersonagemTurnos_Hab1Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab1Invulneravel[2] = PersonagemTurnos_Hab1Invulneravel[3];
						PersonagemTurnos_Hab1Invulneravel[3] = PersonagemTurnos_Hab1Invulneravel[4];
						PersonagemTurnos_Hab1Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab1Invulneravel[3] = PersonagemTurnos_Hab1Invulneravel[4];
						PersonagemTurnos_Hab1Invulneravel[4] = 0;
					}
				}

				if (PersonagemTurnos_Hab2Dano[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab2Dano[0] = PersonagemTurnos_Hab2Dano[1];
						PersonagemTurnos_Hab2Dano[1] = PersonagemTurnos_Hab2Dano[2];
						PersonagemTurnos_Hab2Dano[2] = PersonagemTurnos_Hab2Dano[3];
						PersonagemTurnos_Hab2Dano[3] = PersonagemTurnos_Hab2Dano[4];
						PersonagemTurnos_Hab2Dano[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab2Dano[1] = PersonagemTurnos_Hab2Dano[2];
						PersonagemTurnos_Hab2Dano[2] = PersonagemTurnos_Hab2Dano[3];
						PersonagemTurnos_Hab2Dano[3] = PersonagemTurnos_Hab2Dano[4];
						PersonagemTurnos_Hab2Dano[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab2Dano[2] = PersonagemTurnos_Hab2Dano[3];
						PersonagemTurnos_Hab2Dano[3] = PersonagemTurnos_Hab2Dano[4];
						PersonagemTurnos_Hab2Dano[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab2Dano[3] = PersonagemTurnos_Hab2Dano[4];
						PersonagemTurnos_Hab2Dano[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab2DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab2DanoPerfurante[0] = PersonagemTurnos_Hab2DanoPerfurante[1];
						PersonagemTurnos_Hab2DanoPerfurante[1] = PersonagemTurnos_Hab2DanoPerfurante[2];
						PersonagemTurnos_Hab2DanoPerfurante[2] = PersonagemTurnos_Hab2DanoPerfurante[3];
						PersonagemTurnos_Hab2DanoPerfurante[3] = PersonagemTurnos_Hab2DanoPerfurante[4];
						PersonagemTurnos_Hab2DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab2DanoPerfurante[1] = PersonagemTurnos_Hab2DanoPerfurante[2];
						PersonagemTurnos_Hab2DanoPerfurante[2] = PersonagemTurnos_Hab2DanoPerfurante[3];
						PersonagemTurnos_Hab2DanoPerfurante[3] = PersonagemTurnos_Hab2DanoPerfurante[4];
						PersonagemTurnos_Hab2DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab2DanoPerfurante[2] = PersonagemTurnos_Hab2DanoPerfurante[3];
						PersonagemTurnos_Hab2DanoPerfurante[3] = PersonagemTurnos_Hab2DanoPerfurante[4];
						PersonagemTurnos_Hab2DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab2DanoPerfurante[3] = PersonagemTurnos_Hab2DanoPerfurante[4];
						PersonagemTurnos_Hab2DanoPerfurante[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab2DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab2DanoVerdadeiro[0] = PersonagemTurnos_Hab2DanoVerdadeiro[1];
						PersonagemTurnos_Hab2DanoVerdadeiro[1] = PersonagemTurnos_Hab2DanoVerdadeiro[2];
						PersonagemTurnos_Hab2DanoVerdadeiro[2] = PersonagemTurnos_Hab2DanoVerdadeiro[3];
						PersonagemTurnos_Hab2DanoVerdadeiro[3] = PersonagemTurnos_Hab2DanoVerdadeiro[4];
						PersonagemTurnos_Hab2DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab2DanoVerdadeiro[1] = PersonagemTurnos_Hab2DanoVerdadeiro[2];
						PersonagemTurnos_Hab2DanoVerdadeiro[2] = PersonagemTurnos_Hab2DanoVerdadeiro[3];
						PersonagemTurnos_Hab2DanoVerdadeiro[3] = PersonagemTurnos_Hab2DanoVerdadeiro[4];
						PersonagemTurnos_Hab2DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab2DanoVerdadeiro[2] = PersonagemTurnos_Hab2DanoVerdadeiro[3];
						PersonagemTurnos_Hab2DanoVerdadeiro[3] = PersonagemTurnos_Hab2DanoVerdadeiro[4];
						PersonagemTurnos_Hab2DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab2DanoVerdadeiro[3] = PersonagemTurnos_Hab2DanoVerdadeiro[4];
						PersonagemTurnos_Hab2DanoVerdadeiro[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab2Cura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab2Cura[0] = PersonagemTurnos_Hab2Cura[1];
						PersonagemTurnos_Hab2Cura[1] = PersonagemTurnos_Hab2Cura[2];
						PersonagemTurnos_Hab2Cura[2] = PersonagemTurnos_Hab2Cura[3];
						PersonagemTurnos_Hab2Cura[3] = PersonagemTurnos_Hab2Cura[4];
						PersonagemTurnos_Hab2Cura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab2Cura[1] = PersonagemTurnos_Hab2Cura[2];
						PersonagemTurnos_Hab2Cura[2] = PersonagemTurnos_Hab2Cura[3];
						PersonagemTurnos_Hab2Cura[3] = PersonagemTurnos_Hab2Cura[4];
						PersonagemTurnos_Hab2Cura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab2Cura[2] = PersonagemTurnos_Hab2Cura[3];
						PersonagemTurnos_Hab2Cura[3] = PersonagemTurnos_Hab2Cura[4];
						PersonagemTurnos_Hab2Cura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab2Cura[3] = PersonagemTurnos_Hab2Cura[4];
						PersonagemTurnos_Hab2Cura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab2Armadura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab2Armadura[0] = PersonagemTurnos_Hab2Armadura[1];
						PersonagemTurnos_Hab2Armadura[1] = PersonagemTurnos_Hab2Armadura[2];
						PersonagemTurnos_Hab2Armadura[2] = PersonagemTurnos_Hab2Armadura[3];
						PersonagemTurnos_Hab2Armadura[3] = PersonagemTurnos_Hab2Armadura[4];
						PersonagemTurnos_Hab2Armadura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab2Armadura[1] = PersonagemTurnos_Hab2Armadura[2];
						PersonagemTurnos_Hab2Armadura[2] = PersonagemTurnos_Hab2Armadura[3];
						PersonagemTurnos_Hab2Armadura[3] = PersonagemTurnos_Hab2Armadura[4];
						PersonagemTurnos_Hab2Armadura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab2Armadura[2] = PersonagemTurnos_Hab2Armadura[3];
						PersonagemTurnos_Hab2Armadura[3] = PersonagemTurnos_Hab2Armadura[4];
						PersonagemTurnos_Hab2Armadura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab2Armadura[3] = PersonagemTurnos_Hab2Armadura[4];
						PersonagemTurnos_Hab2Armadura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab2Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab2Invulneravel[0] = PersonagemTurnos_Hab2Invulneravel[1];
						PersonagemTurnos_Hab2Invulneravel[1] = PersonagemTurnos_Hab2Invulneravel[2];
						PersonagemTurnos_Hab2Invulneravel[2] = PersonagemTurnos_Hab2Invulneravel[3];
						PersonagemTurnos_Hab2Invulneravel[3] = PersonagemTurnos_Hab2Invulneravel[4];
						PersonagemTurnos_Hab2Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab2Invulneravel[1] = PersonagemTurnos_Hab2Invulneravel[2];
						PersonagemTurnos_Hab2Invulneravel[2] = PersonagemTurnos_Hab2Invulneravel[3];
						PersonagemTurnos_Hab2Invulneravel[3] = PersonagemTurnos_Hab2Invulneravel[4];
						PersonagemTurnos_Hab2Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab2Invulneravel[2] = PersonagemTurnos_Hab2Invulneravel[3];
						PersonagemTurnos_Hab2Invulneravel[3] = PersonagemTurnos_Hab2Invulneravel[4];
						PersonagemTurnos_Hab2Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab2Invulneravel[3] = PersonagemTurnos_Hab2Invulneravel[4];
						PersonagemTurnos_Hab2Invulneravel[4] = 0;
					}
				}

				if (PersonagemTurnos_Hab3Dano[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab3Dano[0] = PersonagemTurnos_Hab3Dano[1];
						PersonagemTurnos_Hab3Dano[1] = PersonagemTurnos_Hab3Dano[2];
						PersonagemTurnos_Hab3Dano[2] = PersonagemTurnos_Hab3Dano[3];
						PersonagemTurnos_Hab3Dano[3] = PersonagemTurnos_Hab3Dano[4];
						PersonagemTurnos_Hab3Dano[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab3Dano[1] = PersonagemTurnos_Hab3Dano[2];
						PersonagemTurnos_Hab3Dano[2] = PersonagemTurnos_Hab3Dano[3];
						PersonagemTurnos_Hab3Dano[3] = PersonagemTurnos_Hab3Dano[4];
						PersonagemTurnos_Hab3Dano[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab3Dano[2] = PersonagemTurnos_Hab3Dano[3];
						PersonagemTurnos_Hab3Dano[3] = PersonagemTurnos_Hab3Dano[4];
						PersonagemTurnos_Hab3Dano[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab3Dano[3] = PersonagemTurnos_Hab3Dano[4];
						PersonagemTurnos_Hab3Dano[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab3DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab3DanoPerfurante[0] = PersonagemTurnos_Hab3DanoPerfurante[1];
						PersonagemTurnos_Hab3DanoPerfurante[1] = PersonagemTurnos_Hab3DanoPerfurante[2];
						PersonagemTurnos_Hab3DanoPerfurante[2] = PersonagemTurnos_Hab3DanoPerfurante[3];
						PersonagemTurnos_Hab3DanoPerfurante[3] = PersonagemTurnos_Hab3DanoPerfurante[4];
						PersonagemTurnos_Hab3DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab3DanoPerfurante[1] = PersonagemTurnos_Hab3DanoPerfurante[2];
						PersonagemTurnos_Hab3DanoPerfurante[2] = PersonagemTurnos_Hab3DanoPerfurante[3];
						PersonagemTurnos_Hab3DanoPerfurante[3] = PersonagemTurnos_Hab3DanoPerfurante[4];
						PersonagemTurnos_Hab3DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab3DanoPerfurante[2] = PersonagemTurnos_Hab3DanoPerfurante[3];
						PersonagemTurnos_Hab3DanoPerfurante[3] = PersonagemTurnos_Hab3DanoPerfurante[4];
						PersonagemTurnos_Hab3DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab3DanoPerfurante[3] = PersonagemTurnos_Hab3DanoPerfurante[4];
						PersonagemTurnos_Hab3DanoPerfurante[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab3DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab3DanoVerdadeiro[0] = PersonagemTurnos_Hab3DanoVerdadeiro[1];
						PersonagemTurnos_Hab3DanoVerdadeiro[1] = PersonagemTurnos_Hab3DanoVerdadeiro[2];
						PersonagemTurnos_Hab3DanoVerdadeiro[2] = PersonagemTurnos_Hab3DanoVerdadeiro[3];
						PersonagemTurnos_Hab3DanoVerdadeiro[3] = PersonagemTurnos_Hab3DanoVerdadeiro[4];
						PersonagemTurnos_Hab3DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab3DanoVerdadeiro[1] = PersonagemTurnos_Hab3DanoVerdadeiro[2];
						PersonagemTurnos_Hab3DanoVerdadeiro[2] = PersonagemTurnos_Hab3DanoVerdadeiro[3];
						PersonagemTurnos_Hab3DanoVerdadeiro[3] = PersonagemTurnos_Hab3DanoVerdadeiro[4];
						PersonagemTurnos_Hab3DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab3DanoVerdadeiro[2] = PersonagemTurnos_Hab3DanoVerdadeiro[3];
						PersonagemTurnos_Hab3DanoVerdadeiro[3] = PersonagemTurnos_Hab3DanoVerdadeiro[4];
						PersonagemTurnos_Hab3DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab3DanoVerdadeiro[3] = PersonagemTurnos_Hab3DanoVerdadeiro[4];
						PersonagemTurnos_Hab3DanoVerdadeiro[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab3Cura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab3Cura[0] = PersonagemTurnos_Hab3Cura[1];
						PersonagemTurnos_Hab3Cura[1] = PersonagemTurnos_Hab3Cura[2];
						PersonagemTurnos_Hab3Cura[2] = PersonagemTurnos_Hab3Cura[3];
						PersonagemTurnos_Hab3Cura[3] = PersonagemTurnos_Hab3Cura[4];
						PersonagemTurnos_Hab3Cura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab3Cura[1] = PersonagemTurnos_Hab3Cura[2];
						PersonagemTurnos_Hab3Cura[2] = PersonagemTurnos_Hab3Cura[3];
						PersonagemTurnos_Hab3Cura[3] = PersonagemTurnos_Hab3Cura[4];
						PersonagemTurnos_Hab3Cura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab3Cura[2] = PersonagemTurnos_Hab3Cura[3];
						PersonagemTurnos_Hab3Cura[3] = PersonagemTurnos_Hab3Cura[4];
						PersonagemTurnos_Hab3Cura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab3Cura[3] = PersonagemTurnos_Hab3Cura[4];
						PersonagemTurnos_Hab3Cura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab3Armadura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab3Armadura[0] = PersonagemTurnos_Hab3Armadura[1];
						PersonagemTurnos_Hab3Armadura[1] = PersonagemTurnos_Hab3Armadura[2];
						PersonagemTurnos_Hab3Armadura[2] = PersonagemTurnos_Hab3Armadura[3];
						PersonagemTurnos_Hab3Armadura[3] = PersonagemTurnos_Hab3Armadura[4];
						PersonagemTurnos_Hab3Armadura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab3Armadura[1] = PersonagemTurnos_Hab3Armadura[2];
						PersonagemTurnos_Hab3Armadura[2] = PersonagemTurnos_Hab3Armadura[3];
						PersonagemTurnos_Hab3Armadura[3] = PersonagemTurnos_Hab3Armadura[4];
						PersonagemTurnos_Hab3Armadura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab3Armadura[2] = PersonagemTurnos_Hab3Armadura[3];
						PersonagemTurnos_Hab3Armadura[3] = PersonagemTurnos_Hab3Armadura[4];
						PersonagemTurnos_Hab3Armadura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab3Armadura[3] = PersonagemTurnos_Hab3Armadura[4];
						PersonagemTurnos_Hab3Armadura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab3Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab3Invulneravel[0] = PersonagemTurnos_Hab3Invulneravel[1];
						PersonagemTurnos_Hab3Invulneravel[1] = PersonagemTurnos_Hab3Invulneravel[2];
						PersonagemTurnos_Hab3Invulneravel[2] = PersonagemTurnos_Hab3Invulneravel[3];
						PersonagemTurnos_Hab3Invulneravel[3] = PersonagemTurnos_Hab3Invulneravel[4];
						PersonagemTurnos_Hab3Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab3Invulneravel[1] = PersonagemTurnos_Hab3Invulneravel[2];
						PersonagemTurnos_Hab3Invulneravel[2] = PersonagemTurnos_Hab3Invulneravel[3];
						PersonagemTurnos_Hab3Invulneravel[3] = PersonagemTurnos_Hab3Invulneravel[4];
						PersonagemTurnos_Hab3Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab3Invulneravel[2] = PersonagemTurnos_Hab3Invulneravel[3];
						PersonagemTurnos_Hab3Invulneravel[3] = PersonagemTurnos_Hab3Invulneravel[4];
						PersonagemTurnos_Hab3Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab3Invulneravel[3] = PersonagemTurnos_Hab3Invulneravel[4];
						PersonagemTurnos_Hab3Invulneravel[4] = 0;
					}
				}

				if (PersonagemTurnos_Hab4Dano[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab4Dano[0] = PersonagemTurnos_Hab4Dano[1];
						PersonagemTurnos_Hab4Dano[1] = PersonagemTurnos_Hab4Dano[2];
						PersonagemTurnos_Hab4Dano[2] = PersonagemTurnos_Hab4Dano[3];
						PersonagemTurnos_Hab4Dano[3] = PersonagemTurnos_Hab4Dano[4];
						PersonagemTurnos_Hab4Dano[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab4Dano[1] = PersonagemTurnos_Hab4Dano[2];
						PersonagemTurnos_Hab4Dano[2] = PersonagemTurnos_Hab4Dano[3];
						PersonagemTurnos_Hab4Dano[3] = PersonagemTurnos_Hab4Dano[4];
						PersonagemTurnos_Hab4Dano[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab4Dano[2] = PersonagemTurnos_Hab4Dano[3];
						PersonagemTurnos_Hab4Dano[3] = PersonagemTurnos_Hab4Dano[4];
						PersonagemTurnos_Hab4Dano[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab4Dano[3] = PersonagemTurnos_Hab4Dano[4];
						PersonagemTurnos_Hab4Dano[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab4DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab4DanoPerfurante[0] = PersonagemTurnos_Hab4DanoPerfurante[1];
						PersonagemTurnos_Hab4DanoPerfurante[1] = PersonagemTurnos_Hab4DanoPerfurante[2];
						PersonagemTurnos_Hab4DanoPerfurante[2] = PersonagemTurnos_Hab4DanoPerfurante[3];
						PersonagemTurnos_Hab4DanoPerfurante[3] = PersonagemTurnos_Hab4DanoPerfurante[4];
						PersonagemTurnos_Hab4DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab4DanoPerfurante[1] = PersonagemTurnos_Hab4DanoPerfurante[2];
						PersonagemTurnos_Hab4DanoPerfurante[2] = PersonagemTurnos_Hab4DanoPerfurante[3];
						PersonagemTurnos_Hab4DanoPerfurante[3] = PersonagemTurnos_Hab4DanoPerfurante[4];
						PersonagemTurnos_Hab4DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab4DanoPerfurante[2] = PersonagemTurnos_Hab4DanoPerfurante[3];
						PersonagemTurnos_Hab4DanoPerfurante[3] = PersonagemTurnos_Hab4DanoPerfurante[4];
						PersonagemTurnos_Hab4DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab4DanoPerfurante[3] = PersonagemTurnos_Hab4DanoPerfurante[4];
						PersonagemTurnos_Hab4DanoPerfurante[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab4DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab4DanoVerdadeiro[0] = PersonagemTurnos_Hab4DanoVerdadeiro[1];
						PersonagemTurnos_Hab4DanoVerdadeiro[1] = PersonagemTurnos_Hab4DanoVerdadeiro[2];
						PersonagemTurnos_Hab4DanoVerdadeiro[2] = PersonagemTurnos_Hab4DanoVerdadeiro[3];
						PersonagemTurnos_Hab4DanoVerdadeiro[3] = PersonagemTurnos_Hab4DanoVerdadeiro[4];
						PersonagemTurnos_Hab4DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab4DanoVerdadeiro[1] = PersonagemTurnos_Hab4DanoVerdadeiro[2];
						PersonagemTurnos_Hab4DanoVerdadeiro[2] = PersonagemTurnos_Hab4DanoVerdadeiro[3];
						PersonagemTurnos_Hab4DanoVerdadeiro[3] = PersonagemTurnos_Hab4DanoVerdadeiro[4];
						PersonagemTurnos_Hab4DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab4DanoVerdadeiro[2] = PersonagemTurnos_Hab4DanoVerdadeiro[3];
						PersonagemTurnos_Hab4DanoVerdadeiro[3] = PersonagemTurnos_Hab4DanoVerdadeiro[4];
						PersonagemTurnos_Hab4DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab4DanoVerdadeiro[3] = PersonagemTurnos_Hab4DanoVerdadeiro[4];
						PersonagemTurnos_Hab4DanoVerdadeiro[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab4Cura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab4Cura[0] = PersonagemTurnos_Hab4Cura[1];
						PersonagemTurnos_Hab4Cura[1] = PersonagemTurnos_Hab4Cura[2];
						PersonagemTurnos_Hab4Cura[2] = PersonagemTurnos_Hab4Cura[3];
						PersonagemTurnos_Hab4Cura[3] = PersonagemTurnos_Hab4Cura[4];
						PersonagemTurnos_Hab4Cura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab4Cura[1] = PersonagemTurnos_Hab4Cura[2];
						PersonagemTurnos_Hab4Cura[2] = PersonagemTurnos_Hab4Cura[3];
						PersonagemTurnos_Hab4Cura[3] = PersonagemTurnos_Hab4Cura[4];
						PersonagemTurnos_Hab4Cura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab4Cura[2] = PersonagemTurnos_Hab4Cura[3];
						PersonagemTurnos_Hab4Cura[3] = PersonagemTurnos_Hab4Cura[4];
						PersonagemTurnos_Hab4Cura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab4Cura[3] = PersonagemTurnos_Hab4Cura[4];
						PersonagemTurnos_Hab4Cura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab4Armadura[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab4Armadura[0] = PersonagemTurnos_Hab4Armadura[1];
						PersonagemTurnos_Hab4Armadura[1] = PersonagemTurnos_Hab4Armadura[2];
						PersonagemTurnos_Hab4Armadura[2] = PersonagemTurnos_Hab4Armadura[3];
						PersonagemTurnos_Hab4Armadura[3] = PersonagemTurnos_Hab4Armadura[4];
						PersonagemTurnos_Hab4Armadura[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab4Armadura[1] = PersonagemTurnos_Hab4Armadura[2];
						PersonagemTurnos_Hab4Armadura[2] = PersonagemTurnos_Hab4Armadura[3];
						PersonagemTurnos_Hab4Armadura[3] = PersonagemTurnos_Hab4Armadura[4];
						PersonagemTurnos_Hab4Armadura[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab4Armadura[2] = PersonagemTurnos_Hab4Armadura[3];
						PersonagemTurnos_Hab4Armadura[3] = PersonagemTurnos_Hab4Armadura[4];
						PersonagemTurnos_Hab4Armadura[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab4Armadura[3] = PersonagemTurnos_Hab4Armadura[4];
						PersonagemTurnos_Hab4Armadura[4] = 0;
					}
				}
				if (PersonagemTurnos_Hab4Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						PersonagemTurnos_Hab4Invulneravel[0] = PersonagemTurnos_Hab4Invulneravel[1];
						PersonagemTurnos_Hab4Invulneravel[1] = PersonagemTurnos_Hab4Invulneravel[2];
						PersonagemTurnos_Hab4Invulneravel[2] = PersonagemTurnos_Hab4Invulneravel[3];
						PersonagemTurnos_Hab4Invulneravel[3] = PersonagemTurnos_Hab4Invulneravel[4];
						PersonagemTurnos_Hab4Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						PersonagemTurnos_Hab4Invulneravel[1] = PersonagemTurnos_Hab4Invulneravel[2];
						PersonagemTurnos_Hab4Invulneravel[2] = PersonagemTurnos_Hab4Invulneravel[3];
						PersonagemTurnos_Hab4Invulneravel[3] = PersonagemTurnos_Hab4Invulneravel[4];
						PersonagemTurnos_Hab4Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						PersonagemTurnos_Hab4Invulneravel[2] = PersonagemTurnos_Hab4Invulneravel[3];
						PersonagemTurnos_Hab4Invulneravel[3] = PersonagemTurnos_Hab4Invulneravel[4];
						PersonagemTurnos_Hab4Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						PersonagemTurnos_Hab4Invulneravel[3] = PersonagemTurnos_Hab4Invulneravel[4];
						PersonagemTurnos_Hab4Invulneravel[4] = 0;
					}
				}
			}
		}

		public int RetornaPersonagemDanoTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1Dano[i] > 0)
				{
					soma += Hab1DanoPorTurno;
				}
				if (PersonagemTurnos_Hab2Dano[i] > 0)
				{
					soma += Hab2DanoPorTurno;
				}
				if (PersonagemTurnos_Hab3Dano[i] > 0)
				{
					soma += Hab3DanoPorTurno;
				}
				if (PersonagemTurnos_Hab4Dano[i] > 0)
				{
					soma += Hab4DanoPorTurno;
				}
			}

			return soma;
		}
		public int RetornaPersonagemTurnosDano()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += PersonagemTurnos_Hab1Dano[i];
				soma += PersonagemTurnos_Hab2Dano[i];
				soma += PersonagemTurnos_Hab3Dano[i];
				soma += PersonagemTurnos_Hab4Dano[i];
			}

			return soma;
		}
		public int RetornaPersonagemDanoPerfuranteTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1DanoPerfurante[i] > 0)
				{
					soma += Hab1DanoPerfurantePorTurno;
				}
				if (PersonagemTurnos_Hab2DanoPerfurante[i] > 0)
				{
					soma += Hab2DanoPerfurantePorTurno;
				}
				if (PersonagemTurnos_Hab3DanoPerfurante[i] > 0)
				{
					soma += Hab3DanoPerfurantePorTurno;
				}
				if (PersonagemTurnos_Hab4DanoPerfurante[i] > 0)
				{
					soma += Hab4DanoPerfurantePorTurno;
				}
			}

			return soma;
		}
		public int RetornaPersonagemTurnosDanoPerfurante()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += PersonagemTurnos_Hab1DanoPerfurante[i];
				soma += PersonagemTurnos_Hab2DanoPerfurante[i];
				soma += PersonagemTurnos_Hab3DanoPerfurante[i];
				soma += PersonagemTurnos_Hab4DanoPerfurante[i];
			}

			return soma;
		}
		public int RetornaPersonagemDanoVerdadeiroTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1DanoVerdadeiro[i] > 0)
				{
					soma += Hab1DanoVerdadeiroPorTurno;
				}
				if (PersonagemTurnos_Hab2DanoVerdadeiro[i] > 0)
				{
					soma += Hab2DanoVerdadeiroPorTurno;
				}
				if (PersonagemTurnos_Hab3DanoVerdadeiro[i] > 0)
				{
					soma += Hab3DanoVerdadeiroPorTurno;
				}
				if (PersonagemTurnos_Hab4DanoVerdadeiro[i] > 0)
				{
					soma += Hab4DanoVerdadeiroPorTurno;
				}
			}

			return soma;
		}
		public int RetornaPersonagemTurnosDanoVerdadeiro()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += PersonagemTurnos_Hab1DanoVerdadeiro[i];
				soma += PersonagemTurnos_Hab2DanoVerdadeiro[i];
				soma += PersonagemTurnos_Hab3DanoVerdadeiro[i];
				soma += PersonagemTurnos_Hab4DanoVerdadeiro[i];
			}

			return soma;
		}
		public int RetornaPersonagemCuraTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1Cura[i] > 0)
				{
					soma += Hab1CuraPorTurno;
				}
				if (PersonagemTurnos_Hab2Cura[i] > 0)
				{
					soma += Hab2CuraPorTurno;
				}
				if (PersonagemTurnos_Hab3Cura[i] > 0)
				{
					soma += Hab3CuraPorTurno;
				}
				if (PersonagemTurnos_Hab4Cura[i] > 0)
				{
					soma += Hab4CuraPorTurno;
				}
			}

			return soma;
		}
		public int RetornaPersonagemTurnosCura()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += PersonagemTurnos_Hab1Cura[i];
				soma += PersonagemTurnos_Hab2Cura[i];
				soma += PersonagemTurnos_Hab3Cura[i];
				soma += PersonagemTurnos_Hab4Cura[i];
			}

			return soma;
		}
		public int RetornaPersonagemArmaduraTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (PersonagemTurnos_Hab1Armadura[i] > 0)
				{
					soma += Hab1ArmaduraPorTurno;
				}
				if (PersonagemTurnos_Hab2Armadura[i] > 0)
				{
					soma += Hab2ArmaduraPorTurno;
				}
				if (PersonagemTurnos_Hab3Armadura[i] > 0)
				{
					soma += Hab3ArmaduraPorTurno;
				}
				if (PersonagemTurnos_Hab4Armadura[i] > 0)
				{
					soma += Hab4ArmaduraPorTurno;
				}
			}

			return soma;
		}
		public int RetornaPersonagemTurnosArmadura()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += PersonagemTurnos_Hab1Armadura[i];
				soma += PersonagemTurnos_Hab2Armadura[i];
				soma += PersonagemTurnos_Hab3Armadura[i];
				soma += PersonagemTurnos_Hab4Armadura[i];
			}

			return soma;
		}
		public int RetornaPersonagemTurnosInvulneravel()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += PersonagemTurnos_Hab1Invulneravel[i];
				soma += PersonagemTurnos_Hab2Invulneravel[i];
				soma += PersonagemTurnos_Hab3Invulneravel[i];
				soma += PersonagemTurnos_Hab4Invulneravel[i];
			}

			return soma;
		}


		public int RetornaPretosHab(string QualHab)
		{
			if (QualHab == "Hab1")
			{
				return Hab1Pretos;
			}
			else if (QualHab == "Hab2")
			{
				return Hab2Pretos;
			}
			else if (QualHab == "Hab3")
			{
				return Hab3Pretos;
			}
			else if (QualHab == "Hab4")
			{
				return Hab4Pretos;
			}
			else
			{
				return 0;
			}
		}

		#region Métodos Get

		#region Personagem
		public int RetornaPersonagemID()
		{
			return PersonagemID;
		}
		public string RetornaPersonagemNome()
		{
			return PersonagemNome;
		}
		public string RetornaPersonagemDescricao()
		{
			return PersonagemDescricao;
		}
		public byte[] RetornaPersonagemFoto()
		{
			return PersonagemFoto;
		}
		#endregion

		public string RetornaHabNome(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Nome;
				case "Hab2":
					return Hab2Nome;
				case "Hab3":
					return Hab3Nome;
				case "Hab4":
					return Hab4Nome;
				default:
					throw new Exception("Erro no método RetornaHabNome.");
			}
		}
		public string RetornaHabDescricao(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Descricao;
				case "Hab2":
					return Hab2Descricao;
				case "Hab3":
					return Hab3Descricao;
				case "Hab4":
					return Hab4Descricao;
				default:
					throw new Exception("Erro no método RetornaHabDescricao.");
			}
		}
		public byte[] RetornaHabFoto(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Foto;
				case "Hab2":
					return Hab2Foto;
				case "Hab3":
					return Hab3Foto;
				case "Hab4":
					return Hab4Foto;
				default:
					throw new Exception("Erro no método RetornaHabFoto.");
			}
		}
		public int RetornaHabDano(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Dano;
				case "Hab2":
					return Hab2Dano;
				case "Hab3":
					return Hab3Dano;
				case "Hab4":
					return Hab4Dano;
				default:
					throw new Exception("Erro no método RetornaHabDano.");
			}
		}
		public int RetornaHabDanoPorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1DanoPorTurno;
				case "Hab2":
					return Hab2DanoPorTurno;
				case "Hab3":
					return Hab3DanoPorTurno;
				case "Hab4":
					return Hab4DanoPorTurno;
				default:
					throw new Exception("Erro no método RetornaHabDanoPorTurno.");
			}
		}
		public int RetornaHabDanoPerfurante(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1DanoPerfurante;
				case "Hab2":
					return Hab2DanoPerfurante;
				case "Hab3":
					return Hab3DanoPerfurante;
				case "Hab4":
					return Hab4DanoPerfurante;
				default:
					throw new Exception("Erro no método RetornaDanoPerfurante.");
			}
		}
		public int RetornaHabDanoPerfurantePorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1DanoPerfurantePorTurno;
				case "Hab2":
					return Hab2DanoPerfurantePorTurno;
				case "Hab3":
					return Hab3DanoPerfurantePorTurno;
				case "Hab4":
					return Hab4DanoPerfurantePorTurno;
				default:
					throw new Exception("Erro no método RetornaHabDanoPerfurantePorTurno.");
			}
		}
		public int RetornaHabDanoVerdadeiro(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1DanoVerdadeiro;
				case "Hab2":
					return Hab2DanoVerdadeiro;
				case "Hab3":
					return Hab3DanoVerdadeiro;
				case "Hab4":
					return Hab4DanoVerdadeiro;
				default:
					throw new Exception("Erro no método RetornaHabDanoVerdadeiro.");
			}
		}
		public int RetornaHabDanoVerdadeiroPorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1DanoVerdadeiroPorTurno;
				case "Hab2":
					return Hab2DanoVerdadeiroPorTurno;
				case "Hab3":
					return Hab3DanoVerdadeiroPorTurno;
				case "Hab4":
					return Hab4DanoVerdadeiroPorTurno;
				default:
					throw new Exception("Erro no método RetornaHabDanoVerdadeiroPorTurno.");
			}
		}
		public int RetornaHabCura(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Cura;
				case "Hab2":
					return Hab2Cura;
				case "Hab3":
					return Hab3Cura;
				case "Hab4":
					return Hab4Cura;
				default:
					throw new Exception("Erro no método RetornaHabCura.");
			}
		}
		public int RetornaHabCuraPorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1CuraPorTurno;
				case "Hab2":
					return Hab2CuraPorTurno;
				case "Hab3":
					return Hab3CuraPorTurno;
				case "Hab4":
					return Hab4CuraPorTurno;
				default:
					throw new Exception("Erro no método RetornaHabCuraPorTurno.");
			}
		}
		public int RetornaHabArmadura(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Armadura;
				case "Hab2":
					return Hab2Armadura;
				case "Hab3":
					return Hab3Armadura;
				case "Hab4":
					return Hab4Armadura;
				default:
					throw new Exception("Erro no método RetornaHabArmadura.");
			}
		}
		public int RetornaHabArmaduraPorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1ArmaduraPorTurno;
				case "Hab2":
					return Hab2ArmaduraPorTurno;
				case "Hab3":
					return Hab3ArmaduraPorTurno;
				case "Hab4":
					return Hab4ArmaduraPorTurno;
				default:
					throw new Exception("Erro no método RetornaHabArmaduraPorTurno.");
			}
		}
		public int RetornaHabRecarga(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Recarga;
				case "Hab2":
					return Hab2Recarga;
				case "Hab3":
					return Hab3Recarga;
				case "Hab4":
					return Hab4Recarga;
				default:
					throw new Exception("Erro no método RetornaHabRecarga.");
			}
		}
		public int RetornaHabVerdes(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Verdes;
				case "Hab2":
					return Hab2Verdes;
				case "Hab3":
					return Hab3Verdes;
				case "Hab4":
					return Hab4Verdes;
				default:
					throw new Exception("Erro no método RetornaHabVerdes.");
			}
		}
		public int RetornaHabAzuls(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Azuls;
				case "Hab2":
					return Hab2Azuls;
				case "Hab3":
					return Hab3Azuls;
				case "Hab4":
					return Hab4Azuls;
				default:
					throw new Exception("Erro no método RetornaHabAzuls.");
			}
		}
		public int RetornaHabVermelhos(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Vermelhos;
				case "Hab2":
					return Hab2Vermelhos;
				case "Hab3":
					return Hab3Vermelhos;
				case "Hab4":
					return Hab4Vermelhos;
				default:
					throw new Exception("Erro no método RetornaHabVermelhos.");
			}
		}
		public int RetornaHabPretos(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Pretos;
				case "Hab2":
					return Hab2Pretos;
				case "Hab3":
					return Hab3Pretos;
				case "Hab4":
					return Hab4Pretos;
				default:
					throw new Exception("Erro no método RetornaHabPretos.");
			}
		}
		public int RetornaHabInvulnerabilidade(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1Invulnerabilidade;
				case "Hab2":
					return Hab2Invulnerabilidade;
				case "Hab3":
					return Hab3Invulnerabilidade;
				case "Hab4":
					return Hab4Invulnerabilidade;
				default:
					throw new Exception("Erro no método RetornaHabInvulnerabilidade.");
			}
		}
		public int RetornaHabVerdesGanhos(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1VerdesGanhos;
				case "Hab2":
					return Hab2VerdesGanhos;
				case "Hab3":
					return Hab3VerdesGanhos;
				case "Hab4":
					return Hab4VerdesGanhos;
				default:
					throw new Exception("Erro no método RetornaHabVerdesGanhos.");
			}
		}
		public int RetornaHabAzulsGanhos(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1AzulsGanhos;
				case "Hab2":
					return Hab2AzulsGanhos;
				case "Hab3":
					return Hab3AzulsGanhos;
				case "Hab4":
					return Hab4AzulsGanhos;
				default:
					throw new Exception("Erro no método RetornaHabAzulsGanhos.");
			}
		}
		public int RetornaHabVermelhosGanhos(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1VermelhosGanhos;
				case "Hab2":
					return Hab2VermelhosGanhos;
				case "Hab3":
					return Hab3VermelhosGanhos;
				case "Hab4":
					return Hab4VermelhosGanhos;
				default:
					throw new Exception("Erro no método RetornaHabVermelhosGanhos.");
			}
		}
		public int RetornaHabPretosGanhos(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
					return Hab1PretosGanhos;
				case "Hab2":
					return Hab2PretosGanhos;
				case "Hab3":
					return Hab3PretosGanhos;
				case "Hab4":
					return Hab4PretosGanhos;
				default:
					throw new Exception("Erro no método RetornaHabPretosGanhos.");
			}
		}

		#endregion

		#region Métodos Set
		public void SetarPersonagem(int ID, string Nome, string Descricao, byte[] Foto)
		{
			PersonagemID = ID;
			PersonagemNome = Nome;
			PersonagemDescricao = Descricao;
			PersonagemFoto = Foto;
		}
		public void SetarHab1(string Habilidade1Nome, string Habilidade1Descricao, byte[] Habilidade1Foto, int Habilidade1Dano, int Habilidade1DanoPorTurno, int Habilidade1DanoPorTurno_Turnos, int Habilidade1DanoPerfurante, int Habilidade1DanoPerfurantePorTurno, int Habilidade1DanoPerfurantePorTurno_Turnos, int Habilidade1DanoVerdadeiro, int Habilidade1DanoVerdadeiroPorTurno, int Habilidade1DanoVerdadeiroPorTurno_Turnos, int Habilidade1Cura, int Habilidade1CuraPorTurno, int Habilidade1CuraPorTurno_Turnos, int Habilidade1Armadura, int Habilidade1ArmaduraPorTurno, int Habilidade1ArmaduraPorTurno_Turnos, int Habilidade1Recarga, int Habilidade1Verdes, int Habilidade1Azuls, int Habilidade1Vermelhos, int Habilidade1Pretos, int Habilidade1Invulnerabilidade, int Habilidade1VerdesGanhos, int Habilidade1AzulsGanhos, int Habilidade1VermelhosGanhos, int Habilidade1PretosGanhos)
		{
			Hab1Nome = Habilidade1Nome;
			Hab1Foto = Habilidade1Foto;
			Hab1Descricao = Habilidade1Descricao;

			Hab1Dano = Habilidade1Dano;
			Hab1DanoPorTurno = Habilidade1DanoPorTurno;
			Hab1DanoPorTurno_Turnos = Habilidade1DanoPorTurno_Turnos;

			Hab1DanoPerfurante = Habilidade1DanoPerfurante;
			Hab1DanoPerfurantePorTurno = Habilidade1DanoPerfurantePorTurno;
			Hab1DanoPerfurantePorTurno_Turnos = Habilidade1DanoPerfurantePorTurno_Turnos;

			Hab1DanoVerdadeiro = Habilidade1DanoVerdadeiro;
			Hab1DanoVerdadeiroPorTurno = Habilidade1DanoVerdadeiroPorTurno;
			Hab1DanoVerdadeiroPorTurno_Turnos = Habilidade1DanoVerdadeiroPorTurno_Turnos;

			Hab1Cura = Habilidade1Cura;
			Hab1CuraPorTurno = Habilidade1CuraPorTurno;
			Hab1CuraPorTurno_Turnos = Habilidade1CuraPorTurno_Turnos;

			Hab1Armadura = Habilidade1Armadura;
			Hab1ArmaduraPorTurno = Habilidade1ArmaduraPorTurno;
			Hab1ArmaduraPorTurno_Turnos = Habilidade1CuraPorTurno_Turnos;

			Hab1Recarga = Habilidade1Recarga;

			Hab1Verdes = Habilidade1Verdes;
			Hab1Azuls = Habilidade1Azuls;
			Hab1Vermelhos = Habilidade1Vermelhos;
			Hab1Pretos = Habilidade1Pretos;

			Hab1Invulnerabilidade = Habilidade1Invulnerabilidade;

			Hab1VerdesGanhos = Habilidade1VerdesGanhos;
			Hab1AzulsGanhos = Habilidade1AzulsGanhos;
			Hab1VermelhosGanhos = Habilidade1VermelhosGanhos;
			Hab1PretosGanhos = Habilidade1PretosGanhos;
		}
		public void SetarHab2(string Habilidade2Nome, string Habilidade2Descricao, byte[] Habilidade2Foto, int Habilidade2Dano, int Habilidade2DanoPorTurno, int Habilidade2DanoPorTurno_Turnos, int Habilidade2DanoPerfurante, int Habilidade2DanoPerfurantePorTurno, int Habilidade2DanoPerfurantePorTurno_Turnos, int Habilidade2DanoVerdadeiro, int Habilidade2DanoVerdadeiroPorTurno, int Habilidade2DanoVerdadeiroPorTurno_Turnos, int Habilidade2Cura, int Habilidade2CuraPorTurno, int Habilidade2CuraPorTurno_Turnos, int Habilidade2Armadura, int Habilidade2ArmaduraPorTurno, int Habilidade2ArmaduraPorTurno_Turnos, int Habilidade2Recarga, int Habilidade2Verdes, int Habilidade2Azuls, int Habilidade2Vermelhos, int Habilidade2Pretos, int Habilidade2Invulnerabilidade, int Habilidade2VerdesGanhos, int Habilidade2AzulsGanhos, int Habilidade2VermelhosGanhos, int Habilidade2PretosGanhos)
		{
			Hab2Nome = Habilidade2Nome;
			Hab2Foto = Habilidade2Foto;
			Hab2Descricao = Habilidade2Descricao;

			Hab2Dano = Habilidade2Dano;
			Hab2DanoPorTurno = Habilidade2DanoPorTurno;
			Hab2DanoPorTurno_Turnos = Habilidade2DanoPorTurno_Turnos;

			Hab2DanoPerfurante = Habilidade2DanoPerfurante;
			Hab2DanoPerfurantePorTurno = Habilidade2DanoPerfurantePorTurno;
			Hab2DanoPerfurantePorTurno_Turnos = Habilidade2DanoPerfurantePorTurno_Turnos;

			Hab2DanoVerdadeiro = Habilidade2DanoVerdadeiro;
			Hab2DanoVerdadeiroPorTurno = Habilidade2DanoVerdadeiroPorTurno;
			Hab2DanoVerdadeiroPorTurno_Turnos = Habilidade2DanoVerdadeiroPorTurno_Turnos;

			Hab2Cura = Habilidade2Cura;
			Hab2CuraPorTurno = Habilidade2CuraPorTurno;
			Hab2CuraPorTurno_Turnos = Habilidade2CuraPorTurno_Turnos;

			Hab2Armadura = Habilidade2Armadura;
			Hab2ArmaduraPorTurno = Habilidade2ArmaduraPorTurno;
			Hab2ArmaduraPorTurno_Turnos = Habilidade2CuraPorTurno_Turnos;

			Hab2Recarga = Habilidade2Recarga;

			Hab2Verdes = Habilidade2Verdes;
			Hab2Azuls = Habilidade2Azuls;
			Hab2Vermelhos = Habilidade2Vermelhos;
			Hab2Pretos = Habilidade2Pretos;

			Hab2Invulnerabilidade = Habilidade2Invulnerabilidade;

			Hab2VerdesGanhos = Habilidade2VerdesGanhos;
			Hab2AzulsGanhos = Habilidade2AzulsGanhos;
			Hab2VermelhosGanhos = Habilidade2VermelhosGanhos;
			Hab2PretosGanhos = Habilidade2PretosGanhos;
		}
		public void SetarHab3(string Habilidade3Nome, string Habilidade3Descricao, byte[] Habilidade3Foto, int Habilidade3Dano, int Habilidade3DanoPorTurno, int Habilidade3DanoPorTurno_Turnos, int Habilidade3DanoPerfurante, int Habilidade3DanoPerfurantePorTurno, int Habilidade3DanoPerfurantePorTurno_Turnos, int Habilidade3DanoVerdadeiro, int Habilidade3DanoVerdadeiroPorTurno, int Habilidade3DanoVerdadeiroPorTurno_Turnos, int Habilidade3Cura, int Habilidade3CuraPorTurno, int Habilidade3CuraPorTurno_Turnos, int Habilidade3Armadura, int Habilidade3ArmaduraPorTurno, int Habilidade3ArmaduraPorTurno_Turnos, int Habilidade3Recarga, int Habilidade3Verdes, int Habilidade3Azuls, int Habilidade3Vermelhos, int Habilidade3Pretos, int Habilidade3Invulnerabilidade, int Habilidade3VerdesGanhos, int Habilidade3AzulsGanhos, int Habilidade3VermelhosGanhos, int Habilidade3PretosGanhos)
		{
			Hab3Nome = Habilidade3Nome;
			Hab3Foto = Habilidade3Foto;
			Hab3Descricao = Habilidade3Descricao;

			Hab3Dano = Habilidade3Dano;
			Hab3DanoPorTurno = Habilidade3DanoPorTurno;
			Hab3DanoPorTurno_Turnos = Habilidade3DanoPorTurno_Turnos;

			Hab3DanoPerfurante = Habilidade3DanoPerfurante;
			Hab3DanoPerfurantePorTurno = Habilidade3DanoPerfurantePorTurno;
			Hab3DanoPerfurantePorTurno_Turnos = Habilidade3DanoPerfurantePorTurno_Turnos;

			Hab3DanoVerdadeiro = Habilidade3DanoVerdadeiro;
			Hab3DanoVerdadeiroPorTurno = Habilidade3DanoVerdadeiroPorTurno;
			Hab3DanoVerdadeiroPorTurno_Turnos = Habilidade3DanoVerdadeiroPorTurno_Turnos;

			Hab3Cura = Habilidade3Cura;
			Hab3CuraPorTurno = Habilidade3CuraPorTurno;
			Hab3CuraPorTurno_Turnos = Habilidade3CuraPorTurno_Turnos;

			Hab3Armadura = Habilidade3Armadura;
			Hab3ArmaduraPorTurno = Habilidade3ArmaduraPorTurno;
			Hab3ArmaduraPorTurno_Turnos = Habilidade3CuraPorTurno_Turnos;

			Hab3Recarga = Habilidade3Recarga;

			Hab3Verdes = Habilidade3Verdes;
			Hab3Azuls = Habilidade3Azuls;
			Hab3Vermelhos = Habilidade3Vermelhos;
			Hab3Pretos = Habilidade3Pretos;

			Hab3Invulnerabilidade = Habilidade3Invulnerabilidade;

			Hab3VerdesGanhos = Habilidade3VerdesGanhos;
			Hab3AzulsGanhos = Habilidade3AzulsGanhos;
			Hab3VermelhosGanhos = Habilidade3VermelhosGanhos;
			Hab3PretosGanhos = Habilidade3PretosGanhos;
		}
		public void SetarHab4(string Habilidade4Nome, string Habilidade4Descricao, byte[] Habilidade4Foto, int Habilidade4Dano, int Habilidade4DanoPorTurno, int Habilidade4DanoPorTurno_Turnos, int Habilidade4DanoPerfurante, int Habilidade4DanoPerfurantePorTurno, int Habilidade4DanoPerfurantePorTurno_Turnos, int Habilidade4DanoVerdadeiro, int Habilidade4DanoVerdadeiroPorTurno, int Habilidade4DanoVerdadeiroPorTurno_Turnos, int Habilidade4Cura, int Habilidade4CuraPorTurno, int Habilidade4CuraPorTurno_Turnos, int Habilidade4Armadura, int Habilidade4ArmaduraPorTurno, int Habilidade4ArmaduraPorTurno_Turnos, int Habilidade4Recarga, int Habilidade4Verdes, int Habilidade4Azuls, int Habilidade4Vermelhos, int Habilidade4Pretos, int Habilidade4Invulnerabilidade, int Habilidade4VerdesGanhos, int Habilidade4AzulsGanhos, int Habilidade4VermelhosGanhos, int Habilidade4PretosGanhos)
		{
			Hab4Nome = Habilidade4Nome;
			Hab4Foto = Habilidade4Foto;
			Hab4Descricao = Habilidade4Descricao;

			Hab4Dano = Habilidade4Dano;
			Hab4DanoPorTurno = Habilidade4DanoPorTurno;
			Hab4DanoPorTurno_Turnos = Habilidade4DanoPorTurno_Turnos;

			Hab4DanoPerfurante = Habilidade4DanoPerfurante;
			Hab4DanoPerfurantePorTurno = Habilidade4DanoPerfurantePorTurno;
			Hab4DanoPerfurantePorTurno_Turnos = Habilidade4DanoPerfurantePorTurno_Turnos;

			Hab4DanoVerdadeiro = Habilidade4DanoVerdadeiro;
			Hab4DanoVerdadeiroPorTurno = Habilidade4DanoVerdadeiroPorTurno;
			Hab4DanoVerdadeiroPorTurno_Turnos = Habilidade4DanoVerdadeiroPorTurno_Turnos;

			Hab4Cura = Habilidade4Cura;
			Hab4CuraPorTurno = Habilidade4CuraPorTurno;
			Hab4CuraPorTurno_Turnos = Habilidade4CuraPorTurno_Turnos;

			Hab4Armadura = Habilidade4Armadura;
			Hab4ArmaduraPorTurno = Habilidade4ArmaduraPorTurno;
			Hab4ArmaduraPorTurno_Turnos = Habilidade4CuraPorTurno_Turnos;

			Hab4Recarga = Habilidade4Recarga;

			Hab4Verdes = Habilidade4Verdes;
			Hab4Azuls = Habilidade4Azuls;
			Hab4Vermelhos = Habilidade4Vermelhos;
			Hab4Pretos = Habilidade4Pretos;

			Hab4Invulnerabilidade = Habilidade4Invulnerabilidade;

			Hab4VerdesGanhos = Habilidade4VerdesGanhos;
			Hab4AzulsGanhos = Habilidade4AzulsGanhos;
			Hab4VermelhosGanhos = Habilidade4VermelhosGanhos;
			Hab4PretosGanhos = Habilidade4PretosGanhos;
		}
		#endregion

		#endregion




		#region Atributos do Monstro

		protected int MonstroVida = 100, MonstroArmadura = 0;

		protected int MonstroCDHab1, MonstroCDHab2, MonstroCDHab3, MonstroCDHab4;

		protected int[] MonstroTurnos_Hab1Dano = new int[numIndex], MonstroTurnos_Hab2Dano = new int[numIndex], MonstroTurnos_Hab3Dano = new int[numIndex], MonstroTurnos_Hab4Dano = new int[numIndex];
		protected int[] MonstroTurnos_Hab1DanoPerfurante = new int[numIndex], MonstroTurnos_Hab2DanoPerfurante = new int[numIndex], MonstroTurnos_Hab3DanoPerfurante = new int[numIndex], MonstroTurnos_Hab4DanoPerfurante = new int[numIndex];
		protected int[] MonstroTurnos_Hab1DanoVerdadeiro = new int[numIndex], MonstroTurnos_Hab2DanoVerdadeiro = new int[numIndex], MonstroTurnos_Hab3DanoVerdadeiro = new int[numIndex], MonstroTurnos_Hab4DanoVerdadeiro = new int[numIndex];
		protected int[] MonstroTurnos_Hab1Cura = new int[numIndex], MonstroTurnos_Hab2Cura = new int[numIndex], MonstroTurnos_Hab3Cura = new int[numIndex], MonstroTurnos_Hab4Cura = new int[numIndex];
		protected int[] MonstroTurnos_Hab1Armadura = new int[numIndex], MonstroTurnos_Hab2Armadura = new int[numIndex], MonstroTurnos_Hab3Armadura = new int[numIndex], MonstroTurnos_Hab4Armadura = new int[numIndex];
		protected int[] MonstroTurnos_Hab1Invulneravel = new int[numIndex], MonstroTurnos_Hab2Invulneravel = new int[numIndex], MonstroTurnos_Hab3Invulneravel = new int[numIndex], MonstroTurnos_Hab4Invulneravel = new int[numIndex];

		#region Atributos Do Monstro
		protected int Monstro_ID;
		protected string Monstro_Nome, Monstro_Descricao;
		protected byte[] Monstro_Foto;
		#endregion
		#region Atributos Hab1 Monstro
		protected string Monstro_Hab1Nome, Monstro_Hab1Descricao;
		protected byte[] Monstro_Hab1Foto;
		protected int Monstro_Hab1Dano, Monstro_Hab1DanoPorTurno, Monstro_Hab1DanoPorTurno_Turnos,
			Monstro_Hab1DanoPerfurante, Monstro_Hab1DanoPerfurantePorTurno, Monstro_Hab1DanoPerfurantePorTurno_Turnos,
			Monstro_Hab1DanoVerdadeiro, Monstro_Hab1DanoVerdadeiroPorTurno, Monstro_Hab1DanoVerdadeiroPorTurno_Turnos,
			Monstro_Hab1Cura, Monstro_Hab1CuraPorTurno, Monstro_Hab1CuraPorTurno_Turnos,
			Monstro_Hab1Armadura, Monstro_Hab1ArmaduraPorTurno, Monstro_Hab1ArmaduraPorTurno_Turnos,
			Monstro_Hab1Recarga,
			Monstro_Hab1Invulnerabilidade,
			Monstro_Hab1Disposicao;
		#endregion
		#region Atributos Hab2 Monstro
		protected string Monstro_Hab2Nome, Monstro_Hab2Descricao;
		protected byte[] Monstro_Hab2Foto;
		protected int Monstro_Hab2Dano, Monstro_Hab2DanoPorTurno, Monstro_Hab2DanoPorTurno_Turnos,
			Monstro_Hab2DanoPerfurante, Monstro_Hab2DanoPerfurantePorTurno, Monstro_Hab2DanoPerfurantePorTurno_Turnos,
			Monstro_Hab2DanoVerdadeiro, Monstro_Hab2DanoVerdadeiroPorTurno, Monstro_Hab2DanoVerdadeiroPorTurno_Turnos,
			Monstro_Hab2Cura, Monstro_Hab2CuraPorTurno, Monstro_Hab2CuraPorTurno_Turnos,
			Monstro_Hab2Armadura, Monstro_Hab2ArmaduraPorTurno, Monstro_Hab2ArmaduraPorTurno_Turnos,
			Monstro_Hab2Recarga,
			Monstro_Hab2Invulnerabilidade,
			Monstro_Hab2Disposicao;
		#endregion
		#region Atributos Hab3 Monstro
		protected string Monstro_Hab3Nome, Monstro_Hab3Descricao;
		protected byte[] Monstro_Hab3Foto;
		protected int Monstro_Hab3Dano, Monstro_Hab3DanoPorTurno, Monstro_Hab3DanoPorTurno_Turnos,
			Monstro_Hab3DanoPerfurante, Monstro_Hab3DanoPerfurantePorTurno, Monstro_Hab3DanoPerfurantePorTurno_Turnos,
			Monstro_Hab3DanoVerdadeiro, Monstro_Hab3DanoVerdadeiroPorTurno, Monstro_Hab3DanoVerdadeiroPorTurno_Turnos,
			Monstro_Hab3Cura, Monstro_Hab3CuraPorTurno, Monstro_Hab3CuraPorTurno_Turnos,
			Monstro_Hab3Armadura, Monstro_Hab3ArmaduraPorTurno, Monstro_Hab3ArmaduraPorTurno_Turnos,
			Monstro_Hab3Recarga,
			Monstro_Hab3Invulnerabilidade,
			Monstro_Hab3Disposicao;
		#endregion
		#region Atributos Hab4 Monstro
		protected string Monstro_Hab4Nome, Monstro_Hab4Descricao;
		protected byte[] Monstro_Hab4Foto;
		protected int Monstro_Hab4Dano, Monstro_Hab4DanoPorTurno, Monstro_Hab4DanoPorTurno_Turnos,
			Monstro_Hab4DanoPerfurante, Monstro_Hab4DanoPerfurantePorTurno, Monstro_Hab4DanoPerfurantePorTurno_Turnos,
			Monstro_Hab4DanoVerdadeiro, Monstro_Hab4DanoVerdadeiroPorTurno, Monstro_Hab4DanoVerdadeiroPorTurno_Turnos,
			Monstro_Hab4Cura, Monstro_Hab4CuraPorTurno, Monstro_Hab4CuraPorTurno_Turnos,
			Monstro_Hab4Armadura, Monstro_Hab4ArmaduraPorTurno, Monstro_Hab4ArmaduraPorTurno_Turnos,
			Monstro_Hab4Recarga,
			Monstro_Hab4Invulnerabilidade,
			Monstro_Hab4Disposicao;
		#endregion

		#endregion

		#region Métodos do Monstro

		public int AtacarMonstroVida(int Dano, int DanoPerfurante, int DanoVerdadeiro, int Minimum)
		{
			if (MonstroVida - DanoVerdadeiro < Minimum)
			{
				MonstroVida = Minimum;
			}
			else
			{
				MonstroVida -= DanoVerdadeiro;

				if (MonstroVida - DanoPerfurante < Minimum)
				{
					MonstroVida = Minimum;
				}
				else if (DanoPerfurante > 0)
				{
					MonstroVida -= DanoPerfurante;
					MonstroArmadura = 0;
				}

				if ((MonstroVida + MonstroArmadura) - Dano < Minimum)
				{
					MonstroVida = Minimum;
				}
				else
				{
					if (MonstroArmadura - Dano > 0)
					{
						MonstroArmadura -= Dano;
					}
					else
					{
						MonstroVida -= (Dano - MonstroArmadura);
						MonstroArmadura = 0;
					}
				}
			}
			//}

			return MonstroVida;
		}
		public int CurarMonstroVida(int Cura, int Maximum)
		{
			if (MonstroVida + Cura > Maximum)
			{
				MonstroVida = Maximum;
			}
			else
			{
				MonstroVida += Cura;
			}
			return MonstroVida;
		}
		public int SetarMonstroArmadura(int Armadura)
		{
			MonstroArmadura += Armadura;

			return MonstroArmadura;
		}

		public string RetornaHabAleatoria()
		{
			int hab1disposicao = Monstro_Hab1Disposicao;
			int hab2disposicao = Monstro_Hab2Disposicao + hab1disposicao;
			int hab3disposicao = Monstro_Hab3Disposicao + hab2disposicao;
			int hab4disposicao = Monstro_Hab4Disposicao + hab3disposicao;
			int somadisposicao = hab4disposicao;

			bool IgnorarHab1 = false;
			bool IgnorarHab2 = false;
			bool IgnorarHab3 = false;
			bool IgnorarHab4 = false;

			/*
			 *							Lógica de escolha de uso das habilidades do Monstro:
			 *	
			 *	1. As habilidades só poderão ser usadas quando o monstro não estiver atordoado. (não existe atordoamento ainda)
			 *	2. A habilidade não pode estar em recarga.
			 *	3. Se a habilidade der dano, só pode ser usada quando o personagem não estiver invulnerável.
			 *	4. O monstro só usará habilidade unicamente de cura se puder curar (vida != máximo).
			 *	
			 *										05/09/2018	-	20h22
			*/


			int MonstroStunado = 0;             //Cabe implementação
			if (MonstroStunado == 0)            //Se o Monstro não está estunado
			{
				if (
					(MonstroCDHab1 > 0)     //Se a Hab1 está em recarga									
					|| (                    //Ou a Hab1 dá dano E o personagem está invulnerável
					(Monstro_Hab1Dano > 0 || Monstro_Hab1DanoPorTurno > 0 || Monstro_Hab1DanoPerfurante > 0 || Monstro_Hab1DanoPerfurantePorTurno > 0 || Monstro_Hab1DanoVerdadeiro > 0 || Monstro_Hab1DanoVerdadeiroPorTurno > 0)
					&& RetornaPersonagemTurnosInvulneravel() > 0)
					|| (                    //Ou se a Hab1 SOMENTE cura, mas a vida está no máximo
					(Monstro_Hab1Cura > 0 || Monstro_Hab1CuraPorTurno > 0) && (Monstro_Hab1Armadura == 0 && Monstro_Hab1ArmaduraPorTurno == 0 && Monstro_Hab1Invulnerabilidade == 0)
					&& (MonstroVida == 100))
					)
				{
					IgnorarHab1 = true;
				}
				if (
					(MonstroCDHab2 > 0)     //Se a Hab2 está em recarga									
					|| (                    //Ou a Hab2 dá dano E o personagem está invulnerável
					(Monstro_Hab2Dano > 0 || Monstro_Hab2DanoPorTurno > 0 || Monstro_Hab2DanoPerfurante > 0 || Monstro_Hab2DanoPerfurantePorTurno > 0 || Monstro_Hab2DanoVerdadeiro > 0 || Monstro_Hab2DanoVerdadeiroPorTurno > 0)
					&& RetornaPersonagemTurnosInvulneravel() > 0)
					|| (                    //Ou se a Hab2 SOMENTE cura, mas a vida está no máximo
					(Monstro_Hab2Cura > 0 || Monstro_Hab2CuraPorTurno > 0) && (Monstro_Hab2Armadura == 0 && Monstro_Hab2ArmaduraPorTurno == 0 && Monstro_Hab2Invulnerabilidade == 0)
					&& (MonstroVida == 100))
					)
				{
					IgnorarHab2 = true;
				}
				if (
					(MonstroCDHab3 > 0)     //Se a Hab3 está em recarga									
					|| (                    //Ou a Hab3 dá dano E o personagem está invulnerável
					(Monstro_Hab3Dano > 0 || Monstro_Hab3DanoPorTurno > 0 || Monstro_Hab3DanoPerfurante > 0 || Monstro_Hab3DanoPerfurantePorTurno > 0 || Monstro_Hab3DanoVerdadeiro > 0 || Monstro_Hab3DanoVerdadeiroPorTurno > 0)
					&& RetornaPersonagemTurnosInvulneravel() > 0)
					|| (                    //Ou se a Hab3 SOMENTE cura, mas a vida está no máximo
					(Monstro_Hab3Cura > 0 || Monstro_Hab3CuraPorTurno > 0) && (Monstro_Hab3Armadura == 0 && Monstro_Hab3ArmaduraPorTurno == 0 && Monstro_Hab3Invulnerabilidade == 0)
					&& (MonstroVida == 100))
					)
				{
					IgnorarHab3 = true;
				}
				if (
					(MonstroCDHab4 > 0)     //Se a Hab4 está em recarga									
					|| (                    //Ou a Hab4 dá dano E o personagem está invulnerável
					(Monstro_Hab4Dano > 0 || Monstro_Hab4DanoPorTurno > 0 || Monstro_Hab4DanoPerfurante > 0 || Monstro_Hab4DanoPerfurantePorTurno > 0 || Monstro_Hab4DanoVerdadeiro > 0 || Monstro_Hab4DanoVerdadeiroPorTurno > 0)
					&& RetornaPersonagemTurnosInvulneravel() > 0)
					|| (                    //Ou se a Hab4 SOMENTE cura, mas a vida está no máximo
					(Monstro_Hab4Cura > 0 || Monstro_Hab4CuraPorTurno > 0) && (Monstro_Hab4Armadura == 0 && Monstro_Hab4ArmaduraPorTurno == 0 && Monstro_Hab4Invulnerabilidade == 0)
					&& (MonstroVida == 100))
					)
				{
					IgnorarHab4 = true;
				}
			}

			#region Código Obsoleto
			/*

			if (((Monstro_Hab1Cura > 0 && MonstroVida == 100) && Monstro_Hab1Dano == 0 && Monstro_Hab1Armadura == 0)
				|| MonstroCDHab1 > 0
				|| (Monstro_Hab1Dano > 0 || Monstro_Hab1DanoPerfurante > 0 || Monstro_Hab1DanoVerdadeiro > 0) && RetornaPersonagemTurnosInvulneravel() > 0)
			{
				IgnorarHab1 = true;
			}
			if (((Monstro_Hab2Cura > 0 && MonstroVida == 100) && Monstro_Hab2Dano == 0 && Monstro_Hab2Armadura == 0)
				|| MonstroCDHab2 > 0
				|| (Monstro_Hab2Dano > 0 || Monstro_Hab2DanoPerfurante > 0 || Monstro_Hab2DanoVerdadeiro > 0) && RetornaPersonagemTurnosInvulneravel() > 0)
			{
				IgnorarHab2 = true;
			}
			if (((Monstro_Hab3Cura > 0 && MonstroVida == 100) && Monstro_Hab3Dano == 0 && Monstro_Hab3Armadura == 0)
				|| MonstroCDHab3 > 0
				|| (Monstro_Hab3Dano > 0 || Monstro_Hab3DanoPerfurante > 0 || Monstro_Hab3DanoVerdadeiro > 0) && RetornaPersonagemTurnosInvulneravel() > 0)
			{
				IgnorarHab3 = true;
			}
			if (((Monstro_Hab4Cura > 0 && MonstroVida == 100) && Monstro_Hab4Dano == 0 && Monstro_Hab4Armadura == 0)
				|| MonstroCDHab4 > 0
				|| (Monstro_Hab4Dano > 0 || Monstro_Hab4DanoPerfurante > 0 || Monstro_Hab4DanoVerdadeiro > 0) && RetornaPersonagemTurnosInvulneravel() > 0)
			{
				IgnorarHab4 = true;
			}
	*/
			#endregion

			for (int i = 0; i < 50000; i++)
			{
				int hab = rnd.Next(1, somadisposicao + 2);

				if (hab <= hab1disposicao)
				{
					if (IgnorarHab1 == false)
					{
						return "Hab1";
					}
				}
				else if (hab <= hab2disposicao)
				{
					if (IgnorarHab2 == false)
					{
						return "Hab2";
					}
				}
				else if (hab <= hab3disposicao)
				{
					if (IgnorarHab3 == false)
					{
						return "Hab3";
					}
				}
				else if (hab <= hab4disposicao)
				{
					if (IgnorarHab4 == false)
					{
						return "Hab4";
					}
				}
				else
				{
					return "Pass";
				}
			}

			throw new Exception("Erro!");
		}

		public void DiminuirCDSMonstro()
		{
			if (MonstroCDHab1 > 0)
			{
				MonstroCDHab1 -= 1;
			}
			if (MonstroCDHab2 > 0)
			{
				MonstroCDHab2 -= 1;
			}
			if (MonstroCDHab3 > 0)
			{
				MonstroCDHab3 -= 1;
			}
			if (MonstroCDHab4 > 0)
			{
				MonstroCDHab4 -= 1;
			}
		}
		public void SetarCDSMonstro(string Habilidade)
		{
			switch (Habilidade)
			{
				case "Hab1":
					MonstroCDHab1 = RetornaMonstro_HabRecarga(Habilidade);
					break;
				case "Hab2":
					MonstroCDHab2 = RetornaMonstro_HabRecarga(Habilidade);
					break;
				case "Hab3":
					MonstroCDHab3 = RetornaMonstro_HabRecarga(Habilidade);
					break;
				case "Hab4":
					MonstroCDHab4 = RetornaMonstro_HabRecarga(Habilidade);
					break;
			}
		}

		public void SetarMonstroHabilidadePorTurno(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "Monstro_Hab1":
					#region Monstro_Hab1

					if (Monstro_Hab1DanoPorTurno > 0)
					{
						//canbreak3Dano = false;

						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab1Dano[i] == 0)
							{
								MonstroTurnos_Hab1Dano[i] += Monstro_Hab1DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab1DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab1DanoPerfurante[i] == 0)
							{
								MonstroTurnos_Hab1DanoPerfurante[i] += Monstro_Hab1DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab1DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab1DanoVerdadeiro[i] == 0)
							{
								MonstroTurnos_Hab1DanoVerdadeiro[i] += Monstro_Hab1DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab1CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab1Cura[i] == 0)
							{
								MonstroTurnos_Hab1Cura[i] += Monstro_Hab1CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab1ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab1Armadura[i] == 0)
							{
								MonstroTurnos_Hab1Armadura[i] += Monstro_Hab1ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab1Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab1Invulneravel[i] == 0)
							{
								MonstroTurnos_Hab1Invulneravel[i] += Monstro_Hab1Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				case "Hab2":
				case "MonstroHab2":
					#region Monstro_Hab2

					if (Monstro_Hab2DanoPorTurno > 0)
					{
						//canbreak3Dano = false;

						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab2Dano[i] == 0)
							{
								MonstroTurnos_Hab2Dano[i] += Monstro_Hab2DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab2DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab2DanoPerfurante[i] == 0)
							{
								MonstroTurnos_Hab2DanoPerfurante[i] += Monstro_Hab2DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab2DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab2DanoVerdadeiro[i] == 0)
							{
								MonstroTurnos_Hab2DanoVerdadeiro[i] += Monstro_Hab2DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab2CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab2Cura[i] == 0)
							{
								MonstroTurnos_Hab2Cura[i] += Monstro_Hab2CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab2ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab2Armadura[i] == 0)
							{
								MonstroTurnos_Hab2Armadura[i] += Monstro_Hab2ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab2Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab2Invulneravel[i] == 0)
							{
								MonstroTurnos_Hab2Invulneravel[i] += Monstro_Hab2Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				case "Hab3":
				case "MonstroHab3":
					#region Monstro_Hab3

					if (Monstro_Hab3DanoPorTurno > 0)
					{
						//canbreak3Dano = false;

						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab3Dano[i] == 0)
							{
								MonstroTurnos_Hab3Dano[i] += Monstro_Hab3DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab3DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab3DanoPerfurante[i] == 0)
							{
								MonstroTurnos_Hab3DanoPerfurante[i] += Monstro_Hab3DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab3DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab3DanoVerdadeiro[i] == 0)
							{
								MonstroTurnos_Hab3DanoVerdadeiro[i] += Monstro_Hab3DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab3CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab3Cura[i] == 0)
							{
								MonstroTurnos_Hab3Cura[i] += Monstro_Hab3CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab3ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab3Armadura[i] == 0)
							{
								MonstroTurnos_Hab3Armadura[i] += Monstro_Hab3ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab3Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab3Invulneravel[i] == 0)
							{
								MonstroTurnos_Hab3Invulneravel[i] += Monstro_Hab3Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;

				case "Hab4":
				case "MonstroHab4":
					#region Monstro_Hab4

					if (Monstro_Hab4DanoPorTurno > 0)
					{
						//canbreak3Dano = false;

						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab4Dano[i] == 0)
							{
								MonstroTurnos_Hab4Dano[i] += Monstro_Hab4DanoPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab4DanoPerfurantePorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab4DanoPerfurante[i] == 0)
							{
								MonstroTurnos_Hab4DanoPerfurante[i] += Monstro_Hab4DanoPerfurantePorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab4DanoVerdadeiroPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab4DanoVerdadeiro[i] == 0)
							{
								MonstroTurnos_Hab4DanoVerdadeiro[i] += Monstro_Hab4DanoVerdadeiroPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab4CuraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab4Cura[i] == 0)
							{
								MonstroTurnos_Hab4Cura[i] += Monstro_Hab4CuraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab4ArmaduraPorTurno > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab4Armadura[i] == 0)
							{
								MonstroTurnos_Hab4Armadura[i] += Monstro_Hab4ArmaduraPorTurno_Turnos;
								break;
							}
						}
					}
					if (Monstro_Hab4Invulnerabilidade > 0)
					{
						for (int i = 0; i < 5; i++)
						{
							if (MonstroTurnos_Hab4Invulneravel[i] == 0)
							{
								MonstroTurnos_Hab4Invulneravel[i] += Monstro_Hab4Invulnerabilidade;
								break;
							}
						}
					}

					#endregion
					break;


				default:
					throw new Exception("Erro no método SetarMonstroHabilidadePorTurno");
			}
		}
		public void DiminuirMonstroHabilidadesPorTurno()
		{
			for (int i = 0; i < 5; i++)
			{
				if (MonstroTurnos_Hab1Dano[i] > 0) { MonstroTurnos_Hab1Dano[i] -= 1; }
				if (MonstroTurnos_Hab1DanoPerfurante[i] > 0) { MonstroTurnos_Hab1DanoPerfurante[i] -= 1; }
				if (MonstroTurnos_Hab1DanoVerdadeiro[i] > 0) { MonstroTurnos_Hab1DanoVerdadeiro[i] -= 1; }
				if (MonstroTurnos_Hab1Cura[i] > 0) { MonstroTurnos_Hab1Cura[i] -= 1; }
				if (MonstroTurnos_Hab1Armadura[i] > 0) { MonstroTurnos_Hab1Armadura[i] -= 1; }
				if (PersonagemTurnos_Hab1Invulneravel[i] > 0) { PersonagemTurnos_Hab1Invulneravel[i] -= 1; }

				if (MonstroTurnos_Hab1Dano[i] > 0) { MonstroTurnos_Hab1Dano[i] -= 1; }
				if (MonstroTurnos_Hab1DanoPerfurante[i] > 0) { MonstroTurnos_Hab1DanoPerfurante[i] -= 1; }
				if (MonstroTurnos_Hab1DanoVerdadeiro[i] > 0) { MonstroTurnos_Hab1DanoVerdadeiro[i] -= 1; }
				if (MonstroTurnos_Hab1Cura[i] > 0) { MonstroTurnos_Hab1Cura[i] -= 1; }
				if (MonstroTurnos_Hab1Armadura[i] > 0) { MonstroTurnos_Hab1Armadura[i] -= 1; }
				if (PersonagemTurnos_Hab2Invulneravel[i] > 0) { PersonagemTurnos_Hab2Invulneravel[i] -= 1; }

				if (MonstroTurnos_Hab1Dano[i] > 0) { MonstroTurnos_Hab1Dano[i] -= 1; }
				if (MonstroTurnos_Hab1DanoPerfurante[i] > 0) { MonstroTurnos_Hab1DanoPerfurante[i] -= 1; }
				if (MonstroTurnos_Hab1DanoVerdadeiro[i] > 0) { MonstroTurnos_Hab1DanoVerdadeiro[i] -= 1; }
				if (MonstroTurnos_Hab1Cura[i] > 0) { MonstroTurnos_Hab1Cura[i] -= 1; }
				if (MonstroTurnos_Hab1Armadura[i] > 0) { MonstroTurnos_Hab1Armadura[i] -= 1; }
				if (PersonagemTurnos_Hab3Invulneravel[i] > 0) { PersonagemTurnos_Hab3Invulneravel[i] -= 1; }

				if (MonstroTurnos_Hab1Dano[i] > 0) { MonstroTurnos_Hab1Dano[i] -= 1; }
				if (MonstroTurnos_Hab1DanoPerfurante[i] > 0) { MonstroTurnos_Hab1DanoPerfurante[i] -= 1; }
				if (MonstroTurnos_Hab1DanoVerdadeiro[i] > 0) { MonstroTurnos_Hab1DanoVerdadeiro[i] -= 1; }
				if (MonstroTurnos_Hab1Cura[i] > 0) { MonstroTurnos_Hab1Cura[i] -= 1; }
				if (MonstroTurnos_Hab1Armadura[i] > 0) { MonstroTurnos_Hab1Armadura[i] -= 1; }
				if (PersonagemTurnos_Hab4Invulneravel[i] > 0) { PersonagemTurnos_Hab4Invulneravel[i] -= 1; }
			}
		}
		private void OrganizarMonstroArrayPorTurno()
		{
			for (int i = 0; i < numIndex; i++)
			{
				if (MonstroTurnos_Hab1Dano[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab1Dano[0] = MonstroTurnos_Hab1Dano[1];
						MonstroTurnos_Hab1Dano[1] = MonstroTurnos_Hab1Dano[2];
						MonstroTurnos_Hab1Dano[2] = MonstroTurnos_Hab1Dano[3];
						MonstroTurnos_Hab1Dano[3] = MonstroTurnos_Hab1Dano[4];
						MonstroTurnos_Hab1Dano[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab1Dano[1] = MonstroTurnos_Hab1Dano[2];
						MonstroTurnos_Hab1Dano[2] = MonstroTurnos_Hab1Dano[3];
						MonstroTurnos_Hab1Dano[3] = MonstroTurnos_Hab1Dano[4];
						MonstroTurnos_Hab1Dano[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab1Dano[2] = MonstroTurnos_Hab1Dano[3];
						MonstroTurnos_Hab1Dano[3] = MonstroTurnos_Hab1Dano[4];
						MonstroTurnos_Hab1Dano[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab1Dano[3] = MonstroTurnos_Hab1Dano[4];
						MonstroTurnos_Hab1Dano[4] = 0;
					}
				}
				if (MonstroTurnos_Hab1DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab1DanoPerfurante[0] = MonstroTurnos_Hab1DanoPerfurante[1];
						MonstroTurnos_Hab1DanoPerfurante[1] = MonstroTurnos_Hab1DanoPerfurante[2];
						MonstroTurnos_Hab1DanoPerfurante[2] = MonstroTurnos_Hab1DanoPerfurante[3];
						MonstroTurnos_Hab1DanoPerfurante[3] = MonstroTurnos_Hab1DanoPerfurante[4];
						MonstroTurnos_Hab1DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab1DanoPerfurante[1] = MonstroTurnos_Hab1DanoPerfurante[2];
						MonstroTurnos_Hab1DanoPerfurante[2] = MonstroTurnos_Hab1DanoPerfurante[3];
						MonstroTurnos_Hab1DanoPerfurante[3] = MonstroTurnos_Hab1DanoPerfurante[4];
						MonstroTurnos_Hab1DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab1DanoPerfurante[2] = MonstroTurnos_Hab1DanoPerfurante[3];
						MonstroTurnos_Hab1DanoPerfurante[3] = MonstroTurnos_Hab1DanoPerfurante[4];
						MonstroTurnos_Hab1DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab1DanoPerfurante[3] = MonstroTurnos_Hab1DanoPerfurante[4];
						MonstroTurnos_Hab1DanoPerfurante[4] = 0;
					}
				}
				if (MonstroTurnos_Hab1DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab1DanoVerdadeiro[0] = MonstroTurnos_Hab1DanoVerdadeiro[1];
						MonstroTurnos_Hab1DanoVerdadeiro[1] = MonstroTurnos_Hab1DanoVerdadeiro[2];
						MonstroTurnos_Hab1DanoVerdadeiro[2] = MonstroTurnos_Hab1DanoVerdadeiro[3];
						MonstroTurnos_Hab1DanoVerdadeiro[3] = MonstroTurnos_Hab1DanoVerdadeiro[4];
						MonstroTurnos_Hab1DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab1DanoVerdadeiro[1] = MonstroTurnos_Hab1DanoVerdadeiro[2];
						MonstroTurnos_Hab1DanoVerdadeiro[2] = MonstroTurnos_Hab1DanoVerdadeiro[3];
						MonstroTurnos_Hab1DanoVerdadeiro[3] = MonstroTurnos_Hab1DanoVerdadeiro[4];
						MonstroTurnos_Hab1DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab1DanoVerdadeiro[2] = MonstroTurnos_Hab1DanoVerdadeiro[3];
						MonstroTurnos_Hab1DanoVerdadeiro[3] = MonstroTurnos_Hab1DanoVerdadeiro[4];
						MonstroTurnos_Hab1DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab1DanoVerdadeiro[3] = MonstroTurnos_Hab1DanoVerdadeiro[4];
						MonstroTurnos_Hab1DanoVerdadeiro[4] = 0;
					}
				}
				if (MonstroTurnos_Hab1Cura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab1Cura[0] = MonstroTurnos_Hab1Cura[1];
						MonstroTurnos_Hab1Cura[1] = MonstroTurnos_Hab1Cura[2];
						MonstroTurnos_Hab1Cura[2] = MonstroTurnos_Hab1Cura[3];
						MonstroTurnos_Hab1Cura[3] = MonstroTurnos_Hab1Cura[4];
						MonstroTurnos_Hab1Cura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab1Cura[1] = MonstroTurnos_Hab1Cura[2];
						MonstroTurnos_Hab1Cura[2] = MonstroTurnos_Hab1Cura[3];
						MonstroTurnos_Hab1Cura[3] = MonstroTurnos_Hab1Cura[4];
						MonstroTurnos_Hab1Cura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab1Cura[2] = MonstroTurnos_Hab1Cura[3];
						MonstroTurnos_Hab1Cura[3] = MonstroTurnos_Hab1Cura[4];
						MonstroTurnos_Hab1Cura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab1Cura[3] = MonstroTurnos_Hab1Cura[4];
						MonstroTurnos_Hab1Cura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab1Armadura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab1Armadura[0] = MonstroTurnos_Hab1Armadura[1];
						MonstroTurnos_Hab1Armadura[1] = MonstroTurnos_Hab1Armadura[2];
						MonstroTurnos_Hab1Armadura[2] = MonstroTurnos_Hab1Armadura[3];
						MonstroTurnos_Hab1Armadura[3] = MonstroTurnos_Hab1Armadura[4];
						MonstroTurnos_Hab1Armadura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab1Armadura[1] = MonstroTurnos_Hab1Armadura[2];
						MonstroTurnos_Hab1Armadura[2] = MonstroTurnos_Hab1Armadura[3];
						MonstroTurnos_Hab1Armadura[3] = MonstroTurnos_Hab1Armadura[4];
						MonstroTurnos_Hab1Armadura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab1Armadura[2] = MonstroTurnos_Hab1Armadura[3];
						MonstroTurnos_Hab1Armadura[3] = MonstroTurnos_Hab1Armadura[4];
						MonstroTurnos_Hab1Armadura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab1Armadura[3] = MonstroTurnos_Hab1Armadura[4];
						MonstroTurnos_Hab1Armadura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab1Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab1Invulneravel[0] = MonstroTurnos_Hab1Invulneravel[1];
						MonstroTurnos_Hab1Invulneravel[1] = MonstroTurnos_Hab1Invulneravel[2];
						MonstroTurnos_Hab1Invulneravel[2] = MonstroTurnos_Hab1Invulneravel[3];
						MonstroTurnos_Hab1Invulneravel[3] = MonstroTurnos_Hab1Invulneravel[4];
						MonstroTurnos_Hab1Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab1Invulneravel[1] = MonstroTurnos_Hab1Invulneravel[2];
						MonstroTurnos_Hab1Invulneravel[2] = MonstroTurnos_Hab1Invulneravel[3];
						MonstroTurnos_Hab1Invulneravel[3] = MonstroTurnos_Hab1Invulneravel[4];
						MonstroTurnos_Hab1Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab1Invulneravel[2] = MonstroTurnos_Hab1Invulneravel[3];
						MonstroTurnos_Hab1Invulneravel[3] = MonstroTurnos_Hab1Invulneravel[4];
						MonstroTurnos_Hab1Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab1Invulneravel[3] = MonstroTurnos_Hab1Invulneravel[4];
						MonstroTurnos_Hab1Invulneravel[4] = 0;
					}
				}

				if (MonstroTurnos_Hab2Dano[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab2Dano[0] = MonstroTurnos_Hab2Dano[1];
						MonstroTurnos_Hab2Dano[1] = MonstroTurnos_Hab2Dano[2];
						MonstroTurnos_Hab2Dano[2] = MonstroTurnos_Hab2Dano[3];
						MonstroTurnos_Hab2Dano[3] = MonstroTurnos_Hab2Dano[4];
						MonstroTurnos_Hab2Dano[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab2Dano[1] = MonstroTurnos_Hab2Dano[2];
						MonstroTurnos_Hab2Dano[2] = MonstroTurnos_Hab2Dano[3];
						MonstroTurnos_Hab2Dano[3] = MonstroTurnos_Hab2Dano[4];
						MonstroTurnos_Hab2Dano[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab2Dano[2] = MonstroTurnos_Hab2Dano[3];
						MonstroTurnos_Hab2Dano[3] = MonstroTurnos_Hab2Dano[4];
						MonstroTurnos_Hab2Dano[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab2Dano[3] = MonstroTurnos_Hab2Dano[4];
						MonstroTurnos_Hab2Dano[4] = 0;
					}
				}
				if (MonstroTurnos_Hab2DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab2DanoPerfurante[0] = MonstroTurnos_Hab2DanoPerfurante[1];
						MonstroTurnos_Hab2DanoPerfurante[1] = MonstroTurnos_Hab2DanoPerfurante[2];
						MonstroTurnos_Hab2DanoPerfurante[2] = MonstroTurnos_Hab2DanoPerfurante[3];
						MonstroTurnos_Hab2DanoPerfurante[3] = MonstroTurnos_Hab2DanoPerfurante[4];
						MonstroTurnos_Hab2DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab2DanoPerfurante[1] = MonstroTurnos_Hab2DanoPerfurante[2];
						MonstroTurnos_Hab2DanoPerfurante[2] = MonstroTurnos_Hab2DanoPerfurante[3];
						MonstroTurnos_Hab2DanoPerfurante[3] = MonstroTurnos_Hab2DanoPerfurante[4];
						MonstroTurnos_Hab2DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab2DanoPerfurante[2] = MonstroTurnos_Hab2DanoPerfurante[3];
						MonstroTurnos_Hab2DanoPerfurante[3] = MonstroTurnos_Hab2DanoPerfurante[4];
						MonstroTurnos_Hab2DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab2DanoPerfurante[3] = MonstroTurnos_Hab2DanoPerfurante[4];
						MonstroTurnos_Hab2DanoPerfurante[4] = 0;
					}
				}
				if (MonstroTurnos_Hab2DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab2DanoVerdadeiro[0] = MonstroTurnos_Hab2DanoVerdadeiro[1];
						MonstroTurnos_Hab2DanoVerdadeiro[1] = MonstroTurnos_Hab2DanoVerdadeiro[2];
						MonstroTurnos_Hab2DanoVerdadeiro[2] = MonstroTurnos_Hab2DanoVerdadeiro[3];
						MonstroTurnos_Hab2DanoVerdadeiro[3] = MonstroTurnos_Hab2DanoVerdadeiro[4];
						MonstroTurnos_Hab2DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab2DanoVerdadeiro[1] = MonstroTurnos_Hab2DanoVerdadeiro[2];
						MonstroTurnos_Hab2DanoVerdadeiro[2] = MonstroTurnos_Hab2DanoVerdadeiro[3];
						MonstroTurnos_Hab2DanoVerdadeiro[3] = MonstroTurnos_Hab2DanoVerdadeiro[4];
						MonstroTurnos_Hab2DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab2DanoVerdadeiro[2] = MonstroTurnos_Hab2DanoVerdadeiro[3];
						MonstroTurnos_Hab2DanoVerdadeiro[3] = MonstroTurnos_Hab2DanoVerdadeiro[4];
						MonstroTurnos_Hab2DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab2DanoVerdadeiro[3] = MonstroTurnos_Hab2DanoVerdadeiro[4];
						MonstroTurnos_Hab2DanoVerdadeiro[4] = 0;
					}
				}
				if (MonstroTurnos_Hab2Cura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab2Cura[0] = MonstroTurnos_Hab2Cura[1];
						MonstroTurnos_Hab2Cura[1] = MonstroTurnos_Hab2Cura[2];
						MonstroTurnos_Hab2Cura[2] = MonstroTurnos_Hab2Cura[3];
						MonstroTurnos_Hab2Cura[3] = MonstroTurnos_Hab2Cura[4];
						MonstroTurnos_Hab2Cura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab2Cura[1] = MonstroTurnos_Hab2Cura[2];
						MonstroTurnos_Hab2Cura[2] = MonstroTurnos_Hab2Cura[3];
						MonstroTurnos_Hab2Cura[3] = MonstroTurnos_Hab2Cura[4];
						MonstroTurnos_Hab2Cura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab2Cura[2] = MonstroTurnos_Hab2Cura[3];
						MonstroTurnos_Hab2Cura[3] = MonstroTurnos_Hab2Cura[4];
						MonstroTurnos_Hab2Cura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab2Cura[3] = MonstroTurnos_Hab2Cura[4];
						MonstroTurnos_Hab2Cura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab2Armadura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab2Armadura[0] = MonstroTurnos_Hab2Armadura[1];
						MonstroTurnos_Hab2Armadura[1] = MonstroTurnos_Hab2Armadura[2];
						MonstroTurnos_Hab2Armadura[2] = MonstroTurnos_Hab2Armadura[3];
						MonstroTurnos_Hab2Armadura[3] = MonstroTurnos_Hab2Armadura[4];
						MonstroTurnos_Hab2Armadura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab2Armadura[1] = MonstroTurnos_Hab2Armadura[2];
						MonstroTurnos_Hab2Armadura[2] = MonstroTurnos_Hab2Armadura[3];
						MonstroTurnos_Hab2Armadura[3] = MonstroTurnos_Hab2Armadura[4];
						MonstroTurnos_Hab2Armadura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab2Armadura[2] = MonstroTurnos_Hab2Armadura[3];
						MonstroTurnos_Hab2Armadura[3] = MonstroTurnos_Hab2Armadura[4];
						MonstroTurnos_Hab2Armadura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab2Armadura[3] = MonstroTurnos_Hab2Armadura[4];
						MonstroTurnos_Hab2Armadura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab2Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab2Invulneravel[0] = MonstroTurnos_Hab2Invulneravel[1];
						MonstroTurnos_Hab2Invulneravel[1] = MonstroTurnos_Hab2Invulneravel[2];
						MonstroTurnos_Hab2Invulneravel[2] = MonstroTurnos_Hab2Invulneravel[3];
						MonstroTurnos_Hab2Invulneravel[3] = MonstroTurnos_Hab2Invulneravel[4];
						MonstroTurnos_Hab2Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab2Invulneravel[1] = MonstroTurnos_Hab2Invulneravel[2];
						MonstroTurnos_Hab2Invulneravel[2] = MonstroTurnos_Hab2Invulneravel[3];
						MonstroTurnos_Hab2Invulneravel[3] = MonstroTurnos_Hab2Invulneravel[4];
						MonstroTurnos_Hab2Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab2Invulneravel[2] = MonstroTurnos_Hab2Invulneravel[3];
						MonstroTurnos_Hab2Invulneravel[3] = MonstroTurnos_Hab2Invulneravel[4];
						MonstroTurnos_Hab2Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab2Invulneravel[3] = MonstroTurnos_Hab2Invulneravel[4];
						MonstroTurnos_Hab2Invulneravel[4] = 0;
					}
				}

				if (MonstroTurnos_Hab3Dano[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab3Dano[0] = MonstroTurnos_Hab3Dano[1];
						MonstroTurnos_Hab3Dano[1] = MonstroTurnos_Hab3Dano[2];
						MonstroTurnos_Hab3Dano[2] = MonstroTurnos_Hab3Dano[3];
						MonstroTurnos_Hab3Dano[3] = MonstroTurnos_Hab3Dano[4];
						MonstroTurnos_Hab3Dano[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab3Dano[1] = MonstroTurnos_Hab3Dano[2];
						MonstroTurnos_Hab3Dano[2] = MonstroTurnos_Hab3Dano[3];
						MonstroTurnos_Hab3Dano[3] = MonstroTurnos_Hab3Dano[4];
						MonstroTurnos_Hab3Dano[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab3Dano[2] = MonstroTurnos_Hab3Dano[3];
						MonstroTurnos_Hab3Dano[3] = MonstroTurnos_Hab3Dano[4];
						MonstroTurnos_Hab3Dano[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab3Dano[3] = MonstroTurnos_Hab3Dano[4];
						MonstroTurnos_Hab3Dano[4] = 0;
					}
				}
				if (MonstroTurnos_Hab3DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab3DanoPerfurante[0] = MonstroTurnos_Hab3DanoPerfurante[1];
						MonstroTurnos_Hab3DanoPerfurante[1] = MonstroTurnos_Hab3DanoPerfurante[2];
						MonstroTurnos_Hab3DanoPerfurante[2] = MonstroTurnos_Hab3DanoPerfurante[3];
						MonstroTurnos_Hab3DanoPerfurante[3] = MonstroTurnos_Hab3DanoPerfurante[4];
						MonstroTurnos_Hab3DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab3DanoPerfurante[1] = MonstroTurnos_Hab3DanoPerfurante[2];
						MonstroTurnos_Hab3DanoPerfurante[2] = MonstroTurnos_Hab3DanoPerfurante[3];
						MonstroTurnos_Hab3DanoPerfurante[3] = MonstroTurnos_Hab3DanoPerfurante[4];
						MonstroTurnos_Hab3DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab3DanoPerfurante[2] = MonstroTurnos_Hab3DanoPerfurante[3];
						MonstroTurnos_Hab3DanoPerfurante[3] = MonstroTurnos_Hab3DanoPerfurante[4];
						MonstroTurnos_Hab3DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab3DanoPerfurante[3] = MonstroTurnos_Hab3DanoPerfurante[4];
						MonstroTurnos_Hab3DanoPerfurante[4] = 0;
					}
				}
				if (MonstroTurnos_Hab3DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab3DanoVerdadeiro[0] = MonstroTurnos_Hab3DanoVerdadeiro[1];
						MonstroTurnos_Hab3DanoVerdadeiro[1] = MonstroTurnos_Hab3DanoVerdadeiro[2];
						MonstroTurnos_Hab3DanoVerdadeiro[2] = MonstroTurnos_Hab3DanoVerdadeiro[3];
						MonstroTurnos_Hab3DanoVerdadeiro[3] = MonstroTurnos_Hab3DanoVerdadeiro[4];
						MonstroTurnos_Hab3DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab3DanoVerdadeiro[1] = MonstroTurnos_Hab3DanoVerdadeiro[2];
						MonstroTurnos_Hab3DanoVerdadeiro[2] = MonstroTurnos_Hab3DanoVerdadeiro[3];
						MonstroTurnos_Hab3DanoVerdadeiro[3] = MonstroTurnos_Hab3DanoVerdadeiro[4];
						MonstroTurnos_Hab3DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab3DanoVerdadeiro[2] = MonstroTurnos_Hab3DanoVerdadeiro[3];
						MonstroTurnos_Hab3DanoVerdadeiro[3] = MonstroTurnos_Hab3DanoVerdadeiro[4];
						MonstroTurnos_Hab3DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab3DanoVerdadeiro[3] = MonstroTurnos_Hab3DanoVerdadeiro[4];
						MonstroTurnos_Hab3DanoVerdadeiro[4] = 0;
					}
				}
				if (MonstroTurnos_Hab3Cura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab3Cura[0] = MonstroTurnos_Hab3Cura[1];
						MonstroTurnos_Hab3Cura[1] = MonstroTurnos_Hab3Cura[2];
						MonstroTurnos_Hab3Cura[2] = MonstroTurnos_Hab3Cura[3];
						MonstroTurnos_Hab3Cura[3] = MonstroTurnos_Hab3Cura[4];
						MonstroTurnos_Hab3Cura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab3Cura[1] = MonstroTurnos_Hab3Cura[2];
						MonstroTurnos_Hab3Cura[2] = MonstroTurnos_Hab3Cura[3];
						MonstroTurnos_Hab3Cura[3] = MonstroTurnos_Hab3Cura[4];
						MonstroTurnos_Hab3Cura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab3Cura[2] = MonstroTurnos_Hab3Cura[3];
						MonstroTurnos_Hab3Cura[3] = MonstroTurnos_Hab3Cura[4];
						MonstroTurnos_Hab3Cura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab3Cura[3] = MonstroTurnos_Hab3Cura[4];
						MonstroTurnos_Hab3Cura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab3Armadura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab3Armadura[0] = MonstroTurnos_Hab3Armadura[1];
						MonstroTurnos_Hab3Armadura[1] = MonstroTurnos_Hab3Armadura[2];
						MonstroTurnos_Hab3Armadura[2] = MonstroTurnos_Hab3Armadura[3];
						MonstroTurnos_Hab3Armadura[3] = MonstroTurnos_Hab3Armadura[4];
						MonstroTurnos_Hab3Armadura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab3Armadura[1] = MonstroTurnos_Hab3Armadura[2];
						MonstroTurnos_Hab3Armadura[2] = MonstroTurnos_Hab3Armadura[3];
						MonstroTurnos_Hab3Armadura[3] = MonstroTurnos_Hab3Armadura[4];
						MonstroTurnos_Hab3Armadura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab3Armadura[2] = MonstroTurnos_Hab3Armadura[3];
						MonstroTurnos_Hab3Armadura[3] = MonstroTurnos_Hab3Armadura[4];
						MonstroTurnos_Hab3Armadura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab3Armadura[3] = MonstroTurnos_Hab3Armadura[4];
						MonstroTurnos_Hab3Armadura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab3Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab3Invulneravel[0] = MonstroTurnos_Hab3Invulneravel[1];
						MonstroTurnos_Hab3Invulneravel[1] = MonstroTurnos_Hab3Invulneravel[2];
						MonstroTurnos_Hab3Invulneravel[2] = MonstroTurnos_Hab3Invulneravel[3];
						MonstroTurnos_Hab3Invulneravel[3] = MonstroTurnos_Hab3Invulneravel[4];
						MonstroTurnos_Hab3Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab3Invulneravel[1] = MonstroTurnos_Hab3Invulneravel[2];
						MonstroTurnos_Hab3Invulneravel[2] = MonstroTurnos_Hab3Invulneravel[3];
						MonstroTurnos_Hab3Invulneravel[3] = MonstroTurnos_Hab3Invulneravel[4];
						MonstroTurnos_Hab3Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab3Invulneravel[2] = MonstroTurnos_Hab3Invulneravel[3];
						MonstroTurnos_Hab3Invulneravel[3] = MonstroTurnos_Hab3Invulneravel[4];
						MonstroTurnos_Hab3Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab3Invulneravel[3] = MonstroTurnos_Hab3Invulneravel[4];
						MonstroTurnos_Hab3Invulneravel[4] = 0;
					}
				}

				if (MonstroTurnos_Hab4Dano[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab4Dano[0] = MonstroTurnos_Hab4Dano[1];
						MonstroTurnos_Hab4Dano[1] = MonstroTurnos_Hab4Dano[2];
						MonstroTurnos_Hab4Dano[2] = MonstroTurnos_Hab4Dano[3];
						MonstroTurnos_Hab4Dano[3] = MonstroTurnos_Hab4Dano[4];
						MonstroTurnos_Hab4Dano[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab4Dano[1] = MonstroTurnos_Hab4Dano[2];
						MonstroTurnos_Hab4Dano[2] = MonstroTurnos_Hab4Dano[3];
						MonstroTurnos_Hab4Dano[3] = MonstroTurnos_Hab4Dano[4];
						MonstroTurnos_Hab4Dano[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab4Dano[2] = MonstroTurnos_Hab4Dano[3];
						MonstroTurnos_Hab4Dano[3] = MonstroTurnos_Hab4Dano[4];
						MonstroTurnos_Hab4Dano[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab4Dano[3] = MonstroTurnos_Hab4Dano[4];
						MonstroTurnos_Hab4Dano[4] = 0;
					}
				}
				if (MonstroTurnos_Hab4DanoPerfurante[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab4DanoPerfurante[0] = MonstroTurnos_Hab4DanoPerfurante[1];
						MonstroTurnos_Hab4DanoPerfurante[1] = MonstroTurnos_Hab4DanoPerfurante[2];
						MonstroTurnos_Hab4DanoPerfurante[2] = MonstroTurnos_Hab4DanoPerfurante[3];
						MonstroTurnos_Hab4DanoPerfurante[3] = MonstroTurnos_Hab4DanoPerfurante[4];
						MonstroTurnos_Hab4DanoPerfurante[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab4DanoPerfurante[1] = MonstroTurnos_Hab4DanoPerfurante[2];
						MonstroTurnos_Hab4DanoPerfurante[2] = MonstroTurnos_Hab4DanoPerfurante[3];
						MonstroTurnos_Hab4DanoPerfurante[3] = MonstroTurnos_Hab4DanoPerfurante[4];
						MonstroTurnos_Hab4DanoPerfurante[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab4DanoPerfurante[2] = MonstroTurnos_Hab4DanoPerfurante[3];
						MonstroTurnos_Hab4DanoPerfurante[3] = MonstroTurnos_Hab4DanoPerfurante[4];
						MonstroTurnos_Hab4DanoPerfurante[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab4DanoPerfurante[3] = MonstroTurnos_Hab4DanoPerfurante[4];
						MonstroTurnos_Hab4DanoPerfurante[4] = 0;
					}
				}
				if (MonstroTurnos_Hab4DanoVerdadeiro[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab4DanoVerdadeiro[0] = MonstroTurnos_Hab4DanoVerdadeiro[1];
						MonstroTurnos_Hab4DanoVerdadeiro[1] = MonstroTurnos_Hab4DanoVerdadeiro[2];
						MonstroTurnos_Hab4DanoVerdadeiro[2] = MonstroTurnos_Hab4DanoVerdadeiro[3];
						MonstroTurnos_Hab4DanoVerdadeiro[3] = MonstroTurnos_Hab4DanoVerdadeiro[4];
						MonstroTurnos_Hab4DanoVerdadeiro[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab4DanoVerdadeiro[1] = MonstroTurnos_Hab4DanoVerdadeiro[2];
						MonstroTurnos_Hab4DanoVerdadeiro[2] = MonstroTurnos_Hab4DanoVerdadeiro[3];
						MonstroTurnos_Hab4DanoVerdadeiro[3] = MonstroTurnos_Hab4DanoVerdadeiro[4];
						MonstroTurnos_Hab4DanoVerdadeiro[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab4DanoVerdadeiro[2] = MonstroTurnos_Hab4DanoVerdadeiro[3];
						MonstroTurnos_Hab4DanoVerdadeiro[3] = MonstroTurnos_Hab4DanoVerdadeiro[4];
						MonstroTurnos_Hab4DanoVerdadeiro[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab4DanoVerdadeiro[3] = MonstroTurnos_Hab4DanoVerdadeiro[4];
						MonstroTurnos_Hab4DanoVerdadeiro[4] = 0;
					}
				}
				if (MonstroTurnos_Hab4Cura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab4Cura[0] = MonstroTurnos_Hab4Cura[1];
						MonstroTurnos_Hab4Cura[1] = MonstroTurnos_Hab4Cura[2];
						MonstroTurnos_Hab4Cura[2] = MonstroTurnos_Hab4Cura[3];
						MonstroTurnos_Hab4Cura[3] = MonstroTurnos_Hab4Cura[4];
						MonstroTurnos_Hab4Cura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab4Cura[1] = MonstroTurnos_Hab4Cura[2];
						MonstroTurnos_Hab4Cura[2] = MonstroTurnos_Hab4Cura[3];
						MonstroTurnos_Hab4Cura[3] = MonstroTurnos_Hab4Cura[4];
						MonstroTurnos_Hab4Cura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab4Cura[2] = MonstroTurnos_Hab4Cura[3];
						MonstroTurnos_Hab4Cura[3] = MonstroTurnos_Hab4Cura[4];
						MonstroTurnos_Hab4Cura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab4Cura[3] = MonstroTurnos_Hab4Cura[4];
						MonstroTurnos_Hab4Cura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab4Armadura[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab4Armadura[0] = MonstroTurnos_Hab4Armadura[1];
						MonstroTurnos_Hab4Armadura[1] = MonstroTurnos_Hab4Armadura[2];
						MonstroTurnos_Hab4Armadura[2] = MonstroTurnos_Hab4Armadura[3];
						MonstroTurnos_Hab4Armadura[3] = MonstroTurnos_Hab4Armadura[4];
						MonstroTurnos_Hab4Armadura[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab4Armadura[1] = MonstroTurnos_Hab4Armadura[2];
						MonstroTurnos_Hab4Armadura[2] = MonstroTurnos_Hab4Armadura[3];
						MonstroTurnos_Hab4Armadura[3] = MonstroTurnos_Hab4Armadura[4];
						MonstroTurnos_Hab4Armadura[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab4Armadura[2] = MonstroTurnos_Hab4Armadura[3];
						MonstroTurnos_Hab4Armadura[3] = MonstroTurnos_Hab4Armadura[4];
						MonstroTurnos_Hab4Armadura[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab4Armadura[3] = MonstroTurnos_Hab4Armadura[4];
						MonstroTurnos_Hab4Armadura[4] = 0;
					}
				}
				if (MonstroTurnos_Hab4Invulneravel[i] == 0)
				{
					if (i == 0)
					{
						MonstroTurnos_Hab4Invulneravel[0] = MonstroTurnos_Hab4Invulneravel[1];
						MonstroTurnos_Hab4Invulneravel[1] = MonstroTurnos_Hab4Invulneravel[2];
						MonstroTurnos_Hab4Invulneravel[2] = MonstroTurnos_Hab4Invulneravel[3];
						MonstroTurnos_Hab4Invulneravel[3] = MonstroTurnos_Hab4Invulneravel[4];
						MonstroTurnos_Hab4Invulneravel[4] = 0;
					}
					else if (i == 1)
					{
						MonstroTurnos_Hab4Invulneravel[1] = MonstroTurnos_Hab4Invulneravel[2];
						MonstroTurnos_Hab4Invulneravel[2] = MonstroTurnos_Hab4Invulneravel[3];
						MonstroTurnos_Hab4Invulneravel[3] = MonstroTurnos_Hab4Invulneravel[4];
						MonstroTurnos_Hab4Invulneravel[4] = 0;
					}
					else if (i == 2)
					{
						MonstroTurnos_Hab4Invulneravel[2] = MonstroTurnos_Hab4Invulneravel[3];
						MonstroTurnos_Hab4Invulneravel[3] = MonstroTurnos_Hab4Invulneravel[4];
						MonstroTurnos_Hab4Invulneravel[4] = 0;

					}
					else if (i == 3)
					{
						MonstroTurnos_Hab4Invulneravel[3] = MonstroTurnos_Hab4Invulneravel[4];
						MonstroTurnos_Hab4Invulneravel[4] = 0;
					}
				}
			}
		}

		public int RetornaMonstroDanoTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (MonstroTurnos_Hab1Dano[i] > 0)
				{
					soma += Hab1DanoPorTurno;
				}
				if (MonstroTurnos_Hab2Dano[i] > 0)
				{
					soma += Hab2DanoPorTurno;
				}
				if (MonstroTurnos_Hab3Dano[i] > 0)
				{
					soma += Hab3DanoPorTurno;
				}
				if (MonstroTurnos_Hab4Dano[i] > 0)
				{
					soma += Hab4DanoPorTurno;
				}
			}

			return soma;
		}
		public int RetornaMonstroTurnosDano()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += MonstroTurnos_Hab1Dano[i];
				soma += MonstroTurnos_Hab2Dano[i];
				soma += MonstroTurnos_Hab3Dano[i];
				soma += MonstroTurnos_Hab4Dano[i];
			}

			return soma;
		}
		public int RetornaMonstroDanoPerfuranteTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (MonstroTurnos_Hab1DanoPerfurante[i] > 0)
				{
					soma += Hab1DanoPerfurantePorTurno;
				}
				if (MonstroTurnos_Hab2DanoPerfurante[i] > 0)
				{
					soma += Hab2DanoPerfurantePorTurno;
				}
				if (MonstroTurnos_Hab3DanoPerfurante[i] > 0)
				{
					soma += Hab3DanoPerfurantePorTurno;
				}
				if (MonstroTurnos_Hab4DanoPerfurante[i] > 0)
				{
					soma += Hab4DanoPerfurantePorTurno;
				}
			}

			return soma;
		}
		public int RetornaMonstroTurnosDanoPerfurante()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += MonstroTurnos_Hab1DanoPerfurante[i];
				soma += MonstroTurnos_Hab2DanoPerfurante[i];
				soma += MonstroTurnos_Hab3DanoPerfurante[i];
				soma += MonstroTurnos_Hab4DanoPerfurante[i];
			}

			return soma;
		}
		public int RetornaMonstroDanoVerdadeiroTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (MonstroTurnos_Hab1DanoVerdadeiro[i] > 0)
				{
					soma += Hab1DanoVerdadeiroPorTurno;
				}
				if (MonstroTurnos_Hab2DanoVerdadeiro[i] > 0)
				{
					soma += Hab2DanoVerdadeiroPorTurno;
				}
				if (MonstroTurnos_Hab3DanoVerdadeiro[i] > 0)
				{
					soma += Hab3DanoVerdadeiroPorTurno;
				}
				if (MonstroTurnos_Hab4DanoVerdadeiro[i] > 0)
				{
					soma += Hab4DanoVerdadeiroPorTurno;
				}
			}

			return soma;
		}
		public int RetornaMonstroTurnosDanoVerdadeiro()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += MonstroTurnos_Hab1DanoVerdadeiro[i];
				soma += MonstroTurnos_Hab2DanoVerdadeiro[i];
				soma += MonstroTurnos_Hab3DanoVerdadeiro[i];
				soma += MonstroTurnos_Hab4DanoVerdadeiro[i];
			}

			return soma;
		}
		public int RetornaMonstroCuraTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (MonstroTurnos_Hab1Cura[i] > 0)
				{
					soma += Hab1CuraPorTurno;
				}
				if (MonstroTurnos_Hab2Cura[i] > 0)
				{
					soma += Hab2CuraPorTurno;
				}
				if (MonstroTurnos_Hab3Cura[i] > 0)
				{
					soma += Hab3CuraPorTurno;
				}
				if (MonstroTurnos_Hab4Cura[i] > 0)
				{
					soma += Hab4CuraPorTurno;
				}
			}

			return soma;
		}
		public int RetornaMonstroTurnosCura()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += MonstroTurnos_Hab1Cura[i];
				soma += MonstroTurnos_Hab2Cura[i];
				soma += MonstroTurnos_Hab3Cura[i];
				soma += MonstroTurnos_Hab4Cura[i];
			}

			return soma;
		}
		public int RetornaMonstroArmaduraTurno()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				if (MonstroTurnos_Hab1Armadura[i] > 0)
				{
					soma += Hab1ArmaduraPorTurno;
				}
				if (MonstroTurnos_Hab2Armadura[i] > 0)
				{
					soma += Hab2ArmaduraPorTurno;
				}
				if (MonstroTurnos_Hab3Armadura[i] > 0)
				{
					soma += Hab3ArmaduraPorTurno;
				}
				if (MonstroTurnos_Hab4Armadura[i] > 0)
				{
					soma += Hab4ArmaduraPorTurno;
				}
			}

			return soma;
		}
		public int RetornaMonstroTurnosArmadura()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += MonstroTurnos_Hab1Armadura[i];
				soma += MonstroTurnos_Hab2Armadura[i];
				soma += MonstroTurnos_Hab3Armadura[i];
				soma += MonstroTurnos_Hab4Armadura[i];
			}

			return soma;
		}
		public int RetornaMonstroTurnosInvulneravel()
		{
			int soma = 0;

			for (int i = 0; i < numIndex; i++)
			{
				soma += MonstroTurnos_Hab1Invulneravel[i];
				soma += MonstroTurnos_Hab2Invulneravel[i];
				soma += MonstroTurnos_Hab3Invulneravel[i];
				soma += MonstroTurnos_Hab4Invulneravel[i];
			}

			return soma;
		}


		#region Métodos Get
		#region RetornaMonstro
		public int RetornaMonstro_ID()
		{
			return Monstro_ID;
		}
		public string RetornaMonstro_Nome()
		{
			return Monstro_Nome;
		}
		public string RetornaMonstro_Descricao()
		{
			return Monstro_Descricao;
		}
		public byte[] RetornaMonstro_Foto()
		{
			return Monstro_Foto;
		}
		#endregion
		public string RetornaMonstro_HabNome(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Nome;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Nome;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Nome;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Nome;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabNome.");
			}
		}
		public string RetornaMonstro_HabDescricao(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Descricao;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Descricao;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Descricao;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Descricao;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabDescricao.");
			}
		}
		public byte[] RetornaMonstro_HabFoto(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Foto;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Foto;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Foto;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Foto;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabFoto.");
			}
		}
		public int RetornaMonstro_HabDano(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Dano;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Dano;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Dano;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Dano;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabDano.");
			}
		}
		public int RetornaMonstro_HabDanoPerfurante(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1DanoPerfurante;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2DanoPerfurante;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3DanoPerfurante;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4DanoPerfurante;
				default:
					throw new Exception("Erro no método RetornaMonstro_DanoPerfurante.");
			}
		}
		public int RetornaMonstro_HabDanoVerdadeiro(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1DanoVerdadeiro;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2DanoVerdadeiro;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3DanoVerdadeiro;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4DanoVerdadeiro;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabDanoVerdadeiro.");
			}
		}
		public int RetornaMonstro_HabCura(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Cura;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Cura;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Cura;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Cura;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabCura.");
			}
		}
		public int RetornaMonstro_HabArmadura(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Armadura;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Armadura;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Armadura;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Armadura;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabArmadura.");
			}
		}
		public int RetornaMonstro_HabRecarga(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Recarga;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Recarga;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Recarga;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Recarga;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabRecarga.");
			}
		}
		public int RetornaMonstro_HabInvulnerabilidade(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Invulnerabilidade;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Invulnerabilidade;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Invulnerabilidade;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Invulnerabilidade;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabInvulnerabilidade.");
			}
		}
		public int RetornaMonstro_HabDisposicao(string QualHab)
		{
			switch (QualHab)
			{
				case "Hab1":
				case "MonstroHab1":
					return Monstro_Hab1Disposicao;
				case "Hab2":
				case "MonstroHab2":
					return Monstro_Hab2Disposicao;
				case "Hab3":
				case "MonstroHab3":
					return Monstro_Hab3Disposicao;
				case "Hab4":
				case "MonstroHab4":
					return Monstro_Hab4Disposicao;
				default:
					throw new Exception("Erro no método RetornaMonstro_HabDisposicao.");
			}
		}
		#endregion

		#region Métodos Set
		public void SetarMonstro(int ID, string Nome, string Descricao, byte[] Foto)
		{
			Monstro_ID = ID;
			Monstro_Nome = Nome;
			Monstro_Descricao = Descricao;
			Monstro_Foto = Foto;
		}
		public void SetarMonstro_Hab1(string Monstro_Habilidade1Nome, string Monstro_Habilidade1Descricao, byte[] Monstro_Habilidade1Foto, int Monstro_Habilidade1Dano, int Monstro_Habilidade1DanoPerfurante, int Monstro_Habilidade1DanoVerdadeiro, int Monstro_Habilidade1Cura, int Monstro_Habilidade1Armadura, int Monstro_Habilidade1Recarga, int Monstro_Habilidade1Invulnerabilidade, int Monstro_Habilidade1Disposicao)
		{
			Monstro_Hab1Nome = Monstro_Habilidade1Nome;
			Monstro_Hab1Descricao = Monstro_Habilidade1Descricao;
			Monstro_Hab1Foto = Monstro_Habilidade1Foto;

			Monstro_Hab1Dano = Monstro_Habilidade1Dano;
			Monstro_Hab1DanoPerfurante = Monstro_Habilidade1DanoPerfurante;
			Monstro_Hab1DanoVerdadeiro = Monstro_Habilidade1DanoVerdadeiro;
			Monstro_Hab1Cura = Monstro_Habilidade1Cura;
			Monstro_Hab1Armadura = Monstro_Habilidade1Armadura;

			Monstro_Hab1Recarga = Monstro_Habilidade1Recarga;
			Monstro_Hab1Invulnerabilidade = Monstro_Habilidade1Invulnerabilidade;
			Monstro_Hab1Disposicao = Monstro_Habilidade1Disposicao;
		}
		public void SetarMonstro_Hab2(string Monstro_Habilidade2Nome, string Monstro_Habilidade2Descricao, byte[] Monstro_Habilidade2Foto, int Monstro_Habilidade2Dano, int Monstro_Habilidade2DanoPerfurante, int Monstro_Habilidade2DanoVerdadeiro, int Monstro_Habilidade2Cura, int Monstro_Habilidade2Armadura, int Monstro_Habilidade2Recarga, int Monstro_Habilidade2Invulnerabilidade, int Monstro_Habilidade2Disposicao)
		{
			Monstro_Hab2Nome = Monstro_Habilidade2Nome;
			Monstro_Hab2Descricao = Monstro_Habilidade2Descricao;
			Monstro_Hab2Foto = Monstro_Habilidade2Foto;

			Monstro_Hab2Dano = Monstro_Habilidade2Dano;
			Monstro_Hab2DanoPerfurante = Monstro_Habilidade2DanoPerfurante;
			Monstro_Hab2DanoVerdadeiro = Monstro_Habilidade2DanoVerdadeiro;
			Monstro_Hab2Cura = Monstro_Habilidade2Cura;
			Monstro_Hab2Armadura = Monstro_Habilidade2Armadura;

			Monstro_Hab2Recarga = Monstro_Habilidade2Recarga;
			Monstro_Hab2Invulnerabilidade = Monstro_Habilidade2Invulnerabilidade;
			Monstro_Hab2Disposicao = Monstro_Habilidade2Disposicao;
		}
		public void SetarMonstro_Hab3(string Monstro_Habilidade3Nome, string Monstro_Habilidade3Descricao, byte[] Monstro_Habilidade3Foto, int Monstro_Habilidade3Dano, int Monstro_Habilidade3DanoPerfurante, int Monstro_Habilidade3DanoVerdadeiro, int Monstro_Habilidade3Cura, int Monstro_Habilidade3Armadura, int Monstro_Habilidade3Recarga, int Monstro_Habilidade3Invulnerabilidade, int Monstro_Habilidade3Disposicao)
		{
			Monstro_Hab3Nome = Monstro_Habilidade3Nome;
			Monstro_Hab3Descricao = Monstro_Habilidade3Descricao;
			Monstro_Hab3Foto = Monstro_Habilidade3Foto;

			Monstro_Hab3Dano = Monstro_Habilidade3Dano;
			Monstro_Hab3DanoPerfurante = Monstro_Habilidade3DanoPerfurante;
			Monstro_Hab3DanoVerdadeiro = Monstro_Habilidade3DanoVerdadeiro;
			Monstro_Hab3Cura = Monstro_Habilidade3Cura;
			Monstro_Hab3Armadura = Monstro_Habilidade3Armadura;

			Monstro_Hab3Recarga = Monstro_Habilidade3Recarga;
			Monstro_Hab3Invulnerabilidade = Monstro_Habilidade3Invulnerabilidade;
			Monstro_Hab3Disposicao = Monstro_Habilidade3Disposicao;
		}
		public void SetarMonstro_Hab4(string Monstro_Habilidade4Nome, string Monstro_Habilidade4Descricao, byte[] Monstro_Habilidade4Foto, int Monstro_Habilidade4Dano, int Monstro_Habilidade4DanoPerfurante, int Monstro_Habilidade4DanoVerdadeiro, int Monstro_Habilidade4Cura, int Monstro_Habilidade4Armadura, int Monstro_Habilidade4Recarga, int Monstro_Habilidade4Invulnerabilidade, int Monstro_Habilidade4Disposicao)
		{
			Monstro_Hab4Nome = Monstro_Habilidade4Nome;
			Monstro_Hab4Descricao = Monstro_Habilidade4Descricao;
			Monstro_Hab4Foto = Monstro_Habilidade4Foto;

			Monstro_Hab4Dano = Monstro_Habilidade4Dano;
			Monstro_Hab4DanoPerfurante = Monstro_Habilidade4DanoPerfurante;
			Monstro_Hab4DanoVerdadeiro = Monstro_Habilidade4DanoVerdadeiro;
			Monstro_Hab4Cura = Monstro_Habilidade4Cura;
			Monstro_Hab4Armadura = Monstro_Habilidade4Armadura;

			Monstro_Hab4Recarga = Monstro_Habilidade4Recarga;
			Monstro_Hab4Invulnerabilidade = Monstro_Habilidade4Invulnerabilidade;
			Monstro_Hab4Disposicao = Monstro_Habilidade4Disposicao;
		}
		#endregion

		#endregion

		#region Energia
		protected int Verdes = 0, Azuls = 0, Vermelhos = 0; // Pretos = 0;

		public void TirarEnergiaVerde(int Quantidade)
		{
			Verdes -= Quantidade;
		}
		public void TirarEnergiaAzul(int Quantidade)
		{
			Azuls -= Quantidade;
		}
		public void TirarEnergiaVermelha(int Quantidade)
		{
			Vermelhos -= Quantidade;
		}
		public void TirarEnergiaAleatoria(int Quantidade)
		{
			for (int e = 0; e < Quantidade; e++)
			{
				int Aleatorio = 0;

				bool SairDoLoop = false;

				for (int i = 0; i < 500; i++)
				{
					Aleatorio = rnd.Next(1, 4);
					switch (Aleatorio)
					{
						case 1:
							if (Verdes > 0)
							{
								Verdes = Verdes - 1;
								SairDoLoop = true;
							}
							break;
						case 2:
							if (Azuls > 0)
							{
								Azuls = Azuls - 1;
								SairDoLoop = true;
							}
							break;
						case 3:
							if (Vermelhos > 0)
							{
								Vermelhos = Vermelhos - 1;
								SairDoLoop = true;
							}
							break;
					}

					if (SairDoLoop == true)
					{
						break;
					}
				}
			}
		}

		public void PorEnergiaVerde(int Quantidade)
		{
			Verdes += Quantidade;
		}
		public void PorEnergiaAzul(int Quantidade)
		{
			Azuls += Quantidade;
		}
		public void PorEnergiaVermelha(int Quantidade)
		{
			Vermelhos += Quantidade;
		}
		public void PorEnergiaAleatoria(int _Quantidade)
		{
			GerarEnergia(_Quantidade);
		}

		public void GerarEnergia(int Quantidade)
		{
			int EnergiaGerada;
			EnergiaGerada = rnd.Next(1, 31);

			for (int i = 0; i < Quantidade; i++)
			{
				if (EnergiaGerada >= 1 && EnergiaGerada <= 10)
				{
					Verdes += 1;
					EnergiaGerada = rnd.Next(1, 31);
				}
				else if (EnergiaGerada >= 11 && EnergiaGerada <= 20)
				{
					Azuls += 1;
					EnergiaGerada = rnd.Next(1, 31);
				}
				else if (EnergiaGerada >= 21 && EnergiaGerada <= 30)
				{
					Vermelhos += 1;
					EnergiaGerada = rnd.Next(1, 31);
				}
			}
		}

		public int RetornaVerdes()
		{
			return Verdes;
		}
		public int RetornaAzuls()
		{
			return Azuls;
		}
		public int RetornaVermelhos()
		{
			return Vermelhos;
		}
		public int RetornaPretos()
		{
			return (Verdes + Azuls + Vermelhos);
		}
		#endregion
	}
}

#region ToolTip
/*
protected int[] Hab1ToolTipsTurnos = new int[3],
Hab2ToolTipsTurnos = new int[3],
Hab3ToolTipsTurnos = new int[3],
Hab4ToolTipsTurnos = new int[3];
public void SetarPersonagemToolTip(string QualHab)
{
	if (!(string.IsNullOrEmpty(QualHab)))
	{
		switch (QualHab)
		{
			case "Hab1":
				for (int i = 0; i < Hab1ToolTipsTurnos.Length; i++)
				{
					if (Hab1ToolTipsTurnos[i] == 0)
					{
						Hab1ToolTipsTurnos[i] = Hab1Invulnerabilidade;
						break;					
					}						
				}
				break;

			case "Hab2":
				for (int i = 0; i < Hab2ToolTipsTurnos.Length; i++)
				{
					if (Hab2ToolTipsTurnos[i] == 0)
					{
						Hab2ToolTipsTurnos[i] = Hab2Invulnerabilidade;
						break;
					}
				}
				break;

			case "Hab3":
				for (int i = 0; i < Hab3ToolTipsTurnos.Length; i++)
				{
					if (Hab3ToolTipsTurnos[i] == 0)
					{
						Hab3ToolTipsTurnos[i] = Hab3Invulnerabilidade;
						break;
					}
				}
				break;

			case "Hab4":
				for (int i = 0; i < Hab4ToolTipsTurnos.Length; i++)
				{
					if (Hab4ToolTipsTurnos[i] == 0)
					{
						Hab4ToolTipsTurnos[i] = Hab4Invulnerabilidade;
						break;
					}
				}
				break;

			default:
				throw new Exception("Erro no método SetarPersonagemToolTip");
		}
	}
}
public bool PersonagemHabTemToolTip(string QualHab)
{
	switch (QualHab)
	{
		case "Hab1":
			if (Hab1ToolTipsTurnos[0] > 0) { return true; }
			else { return false; }

		case "Hab2":
			if (Hab2ToolTipsTurnos[0] > 0) { return true; }
			else { return false; }

		case "Hab3":
			if (Hab3ToolTipsTurnos[0] > 0) { return true; }
			else { return false; }

		case "Hab4":
			if (Hab4ToolTipsTurnos[0] > 0) { return true; }
			else { return false; }

		default: throw new Exception("Erro no método PersonagemTemToolTip");
	}
}

public string[] PersonagemRetornaToolTip(string QualHab)
{
	string[] Texto = new string[3];
	switch (QualHab)
	{
		case "Hab1":
			for (int i = 0; i < RetornaNumeroDeToolTipsMesmaHabilidade(QualHab); i++)
			{
				Texto[i] = "Este personagem está invulnerável.\n" + Hab1ToolTipsTurnos[i] + " Turno(s) restante(s).";
			}
			return Texto;

		case "Hab2":
			for (int i = 0; i < RetornaNumeroDeToolTipsMesmaHabilidade(QualHab); i++)
			{
				Texto[i] = "Este personagem está invulnerável.\n" + Hab2ToolTipsTurnos[i] + " Turno(s) restante(s).";
			}
			return Texto;

		case "Hab3":
			for (int i = 0; i < RetornaNumeroDeToolTipsMesmaHabilidade(QualHab); i++)
			{
				Texto[i] = "Este personagem está invulnerável.\n" + Hab3ToolTipsTurnos[i] + " Turno(s) restante(s).";
			}
			return Texto;

		case "Hab4":
			for (int i = 0; i < RetornaNumeroDeToolTipsMesmaHabilidade(QualHab); i++)
			{
				Texto[i] = "Este personagem está invulnerável.\n" + Hab4ToolTipsTurnos[i] + " Turno(s) restante(s).";
			}
			return Texto;

		default: throw new Exception("Erro no método PersonagemRetornaToolTip");
	}
}
public void PersonagemDiminuiTurnosToolTip()
{
	for (int i = 0; i < Hab1ToolTipsTurnos.Length; i++)
	{
		if (Hab1ToolTipsTurnos[i] > 0)
		{
			if (i != Hab1ToolTipsTurnos.Length - 1)
			{
				if (--Hab1ToolTipsTurnos[i] == 0)
				{
					Hab1ToolTipsTurnos[i] = Hab1ToolTipsTurnos[i - 1];
					i = 0;
				}
			}
		}
		if (Hab2ToolTipsTurnos[i] > 0)
		{
			if (i != Hab2ToolTipsTurnos.Length - 1)
			{
				if (--Hab2ToolTipsTurnos[i] == 0)
				{
					Hab2ToolTipsTurnos[i] = Hab2ToolTipsTurnos[i - 1];
					i = 0;
				}
			}
		}
		if (Hab3ToolTipsTurnos[i] > 0)
		{
			if (i != Hab2ToolTipsTurnos.Length - 1)
			{
				if (--Hab2ToolTipsTurnos[i] == 0)
				{
					Hab2ToolTipsTurnos[i] = Hab2ToolTipsTurnos[i - 1];
					//i = 0;
				}
			}
		}
		if (Hab4ToolTipsTurnos[i] > 0)
		{
			if (--Hab4ToolTipsTurnos[i] == 0)
			{
				if (i != Hab4ToolTipsTurnos.Length - 1)
				{
					Hab4ToolTipsTurnos[i] = Hab4ToolTipsTurnos[i + 1];
					Hab4ToolTipsTurnos[i + 1] = 0;
					//i = 0;
				}
			}
		}
	}
}

public int RetornaNumeroDeToolTipsMesmaHabilidade(string QualHab)
{
	int a = 0;
	switch (QualHab)
	{
		case "Hab1":
			for (int i = 0; i < Hab1ToolTipsTurnos.Length; i++)
			{
				if (Hab1ToolTipsTurnos[i] != 0)
				{
					a++;
				}
			} break;

		case "Hab2":
			for (int i = 0; i < Hab1ToolTipsTurnos.Length; i++)
			{
				if (Hab2ToolTipsTurnos[i] != 0)
				{
					a++;
				}
			}
			break;

		case "Hab3":
			for (int i = 0; i < Hab1ToolTipsTurnos.Length; i++)
			{
				if (Hab3ToolTipsTurnos[i] != 0)
				{
					a++;
				}
			}
			break;

		case "Hab4":
			for (int i = 0; i < Hab1ToolTipsTurnos.Length; i++)
			{
				if (Hab4ToolTipsTurnos[i] != 0)
				{
					a++;
				}
			}
			break;

		default: throw new Exception("Erro no método PersonagemRetornaToolTip");
	}

	return a;
}
*/
#endregion