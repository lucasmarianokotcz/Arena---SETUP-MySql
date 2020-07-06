using AcessoDados;
using Intermediario.Interfaces;
using Model.Personagem;
using Model.Personagem.Energias;
using Model.Personagem.Regras.Classes;
using Model.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Intermediario
{
    public class PersonagemBLL : IRepository<Personagem>
    {
        #region Atributes

        private readonly Acesso acesso = new Acesso();
        private readonly StringBuilder sql = new StringBuilder();
        private readonly string personagem = PersonagemRegras.NomeTabela;
        private readonly string p = PersonagemRegras.AliasTabela;
        private readonly int numeroHabilidades = PersonagemRegras.NumeroHabilidades;

        #endregion

        #region Methods

        public List<Personagem> Select()
        {
            var lst = new List<Personagem>();

            sql.Append($" SELECT * FROM {personagem} {p} ");
            for (int i = 1; i <= numeroHabilidades; i++)
            {
                sql.Append($" INNER JOIN {personagem}_hab{i} h{i} ON h{i}.hab{i}_id_{personagem} = {p}.id_{personagem} ");
            }

            DataTable dt = acesso.Select(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    var obj = new Personagem()
                    {
                        Id = Convert.ToInt32(r[$"id_{personagem}"]),
                        Nome = Convert.ToString(r[$"nome_{personagem}"]),
                        Descricao = r[$"descricao_{personagem}"] is DBNull ? string.Empty : Convert.ToString(r[$"descricao_{personagem}"]),
                        Foto = (byte[])r[$"foto_{personagem}"]
                    };
                    for (int i = 1; i <= numeroHabilidades; i++)
                    {
                        obj.Habilidades.Add(new HabilidadePersonagem()
                        {
                            Id = Convert.ToInt32(r[$"id_hab{i}"]),
                            Nome = Convert.ToString(r[$"hab{i}_nome"]),
                            Foto = (byte[])r[$"hab{i}_foto"],
                            Dano = new Dano() { DanoHabilidade = Convert.ToInt32(r[$"hab{i}_dano"]) },
                            DanoPorTurno = new DanoPorTurno() { DanoHabilidade = Convert.ToInt32(r[$"hab{i}_dano_por_turno"]), Turnos = Convert.ToInt32(r[$"hab{i}_dano_por_turno_turnos"]) },
                            DanoPerfurante = new DanoPerfurante() { DanoHabilidade = Convert.ToInt32(r[$"hab{i}_dano_perfurante"]) },
                            DanoPerfurantePorTurno = new DanoPerfurantePorTurno() { DanoHabilidade = Convert.ToInt32(r[$"hab{i}_dano_perfurante_por_turno"]), Turnos = Convert.ToInt32(r[$"hab{i}_dano_perfurante_por_turno_turnos"]) },
                            DanoVerdadeiro = new DanoVerdadeiro() { DanoHabilidade = Convert.ToInt32(r[$"hab{i}_dano_verdadeiro"]) },
                            DanoVerdadeiroPorTurno = new DanoVerdadeiroPorTurno() { DanoHabilidade = Convert.ToInt32(r[$"hab{i}_dano_verdadeiro_por_turno"]), Turnos = Convert.ToInt32(r[$"hab{i}_dano_verdadeiro_por_turno_turnos"]) },
                            Cura = new Cura() { CuraHabilidade = Convert.ToInt32(r[$"hab{i}_cura"]) },
                            CuraPorTurno = new CuraPorTurno() { CuraHabilidade = Convert.ToInt32(r[$"hab{i}_cura_por_turno"]), Turnos = Convert.ToInt32(r[$"hab{i}_cura_por_turno_turnos"]) },
                            Armadura = new Armadura() { ArmaduraHabilidade = Convert.ToInt32(r[$"hab{i}_armadura"]) },
                            ArmaduraPorTurno = new ArmaduraPorTurno() { ArmaduraHabilidade = Convert.ToInt32(r[$"hab{i}_armadura_por_turno"]), Turnos = Convert.ToInt32(r[$"hab{i}_armadura_por_turno_turnos"]) },
                            Descricao = r[$"hab{i}_descricao"] is DBNull ? string.Empty : Convert.ToString(r[$"hab{i}_descricao"]),
                            Recarga = Convert.ToInt32(r[$"hab{i}_recarga"]),
                            EnergiaVerde = new EnergiaVerde() { Quantidade = Convert.ToInt32(r[$"hab{i}_verde"]), Ganho = Convert.ToInt32(r[$"hab{i}_ganho_verde"]) },
                            EnergiaAzul = new EnergiaAzul() { Quantidade = Convert.ToInt32(r[$"hab{i}_azul"]), Ganho = Convert.ToInt32(r[$"hab{i}_ganho_azul"]) },
                            EnergiaVermelho = new EnergiaVermelho() { Quantidade = Convert.ToInt32(r[$"hab{i}_vermelho"]), Ganho = Convert.ToInt32(r[$"hab{i}_ganho_vermelho"]) },
                            EnergiaPreto = new EnergiaPreto() { Quantidade = Convert.ToInt32(r[$"hab{i}_preto"]), Ganho = Convert.ToInt32(r[$"hab{i}_ganho_preto"]) },
                            Invulnerabilidade = Convert.ToInt32(r[$"hab{i}_invulnerabilidade"])
                        });
                    }
                    lst.Add(obj);
                }
            }

            return lst;
        }

        #endregion
    }
}
