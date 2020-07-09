using AcessoDados;
using Intermediario.Interfaces;
using Model.Monstro;
using Model.Monstro.Regras.Classes;
using Model.Shared;
using Model.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Intermediario
{
    public class MonstroBLL : IRepository<Monstro>
    {
        #region Atributes

        private readonly Acesso acesso = new Acesso();
        private readonly StringBuilder sql = new StringBuilder();
        private readonly string monstro = MonstroRegras.NomeTabela;
        private readonly string m = MonstroRegras.AliasTabela;
        private readonly int numeroHabilidades = MonstroRegras.NumeroHabilidades;

        #endregion

        #region Methods

        public List<Monstro> Select()
        {
            var lst = new List<Monstro>();

            sql.Append($" SELECT * FROM {monstro} {m} ");
            for (int i = 1; i <= numeroHabilidades; i++)
            {
                sql.Append($" INNER JOIN {monstro}_hab{i} h{i} ON h{i}.hab{i}_id_{monstro} = {m}.id_{monstro} ");
            }

            DataTable dt = acesso.Select(sql.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    var obj = new Monstro()
                    {
                        Id = Convert.ToInt32(r[$"id_{monstro}"]),
                        Nome = Convert.ToString(r[$"nome_{monstro}"]),
                        Descricao = r[$"descricao_{monstro}"] is DBNull ? string.Empty : Convert.ToString(r[$"descricao_{monstro}"]),
                        Foto = (byte[])r[$"foto_{monstro}"]
                    };
                    for (int i = 1; i <= numeroHabilidades; i++)
                    {
                        obj.Habilidades.Add(new HabilidadeMonstro()
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
                            Invulnerabilidade = Convert.ToInt32(r[$"hab{i}_invulnerabilidade"]),
                            Disposicao = Convert.ToInt32(r[$"hab{i}_disposicao"]),
                            Alvo = (EAlvoHabilidade)Convert.ToInt32(r[$"hab{i}_alvo"])
                        });
                    }
                    lst.Add(obj);
                }
            }

            return lst;
        }

        public List<Monstro> GerarMonstrosAleatorios()
        {
            List<Monstro> monstros = Select();
            List<Monstro> selecionados = new List<Monstro>();

            Random rnd = new Random();
            selecionados = monstros.OrderBy(x => rnd.Next(monstros.Count)).Take(3).ToList();

            return selecionados;
        }

        #endregion
    }
}
