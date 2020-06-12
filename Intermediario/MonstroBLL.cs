using AcessoDados;
using Model;
using Model.Atributos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Intermediario
{
    public class MonstroBLL
    {
        private readonly Acesso acesso = new Acesso();

        public List<Monstro> Select()
        {
            List<Monstro> lst = new List<Monstro>();
            StringBuilder sql = new StringBuilder(" ");

            sql.Append(" SELECT * FROM monstro m ");
            sql.Append(" INNER JOIN monstro_hab1 h1 ON h1.hab1_id_monstro = m.id_monstro ");
            sql.Append(" INNER JOIN monstro_hab2 h2 ON h2.hab2_id_monstro = m.id_monstro ");
            sql.Append(" INNER JOIN monstro_hab3 h3 ON h3.hab3_id_monstro = m.id_monstro ");
            sql.Append(" INNER JOIN monstro_hab4 h4 ON h4.hab4_id_monstro = m.id_monstro ");
            sql.Append(" ORDER BY m.nome_monstro ASC ");

            string query = sql.ToString();

            DataTable dt = acesso.Select(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    Monstro monstro = new Monstro()
                    {
                        Id = Convert.ToInt32(r["id_monstro"]),
                        Nome = Convert.ToString(r["nome_monstro"]),
                        Descricao = r["descricao_monstro"] is DBNull ? string.Empty : Convert.ToString(r["descricao_monstro"]),
                        Foto = (byte[])r["foto_monstro"]
                    };
                    Habilidade hab1 = new Habilidade()
                    {
                        Id = Convert.ToInt32(r["id_hab1"]),
                        Nome = Convert.ToString(r["hab1_nome"]),
                        Foto = (byte[])r["hab1_foto"],
                        Dano = new Dano() { DanoHabilidade = Convert.ToInt32(r["hab1_dano"]) },
                        DanoPorTurno = new DanoPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab1_dano_por_turno"]), Turnos = Convert.ToInt32(r["hab1_dano_por_turno_turnos"]) },
                        DanoPerfurante = new DanoPerfurante() { DanoHabilidade = Convert.ToInt32(r["hab1_dano_perfurante"]) },
                        DanoPerfurantePorTurno = new DanoPerfurantePorTurno() { DanoHabilidade = Convert.ToInt32(r["hab1_dano_perfurante_por_turno"]), Turnos = Convert.ToInt32(r["hab1_dano_perfurante_por_turno_turnos"]) },
                        DanoVerdadeiro = new DanoVerdadeiro() { DanoHabilidade = Convert.ToInt32(r["hab1_dano_verdadeiro"]) },
                        DanoVerdadeiroPorTurno = new DanoVerdadeiroPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab1_dano_verdadeiro_por_turno"]), Turnos = Convert.ToInt32(r["hab1_dano_verdadeiro_por_turno_turnos"]) },
                        Cura = new Cura() { CuraHabilidade = Convert.ToInt32(r["hab1_cura"]) },
                        CuraPorTurno = new CuraPorTurno() { CuraHabilidade = Convert.ToInt32(r["hab1_cura_por_turno"]), Turnos = Convert.ToInt32(r["hab1_cura_por_turno_turnos"]) },
                        Armadura = new Armadura() { ArmaduraHabilidade = Convert.ToInt32(r["hab1_armadura"]) },
                        ArmaduraPorTurno = new ArmaduraPorTurno() { ArmaduraHabilidade = Convert.ToInt32(r["hab1_armadura_por_turno"]), Turnos = Convert.ToInt32(r["hab1_armadura_por_turno_turnos"]) },
                        Descricao = r["hab1_descricao"] is DBNull ? string.Empty : Convert.ToString(r["hab1_descricao"]),
                        Recarga = Convert.ToInt32(r["hab1_recarga"]),
                        Invulnerabilidade = Convert.ToInt32(r["hab1_invulnerabilidade"]),
                        Disposicao = Convert.ToInt32(r["hab1_disposicao"])
                    };
                    Habilidade hab2 = new Habilidade()
                    {
                        Id = Convert.ToInt32(r["id_hab2"]),
                        Nome = Convert.ToString(r["hab2_nome"]),
                        Foto = (byte[])r["hab2_foto"],
                        Dano = new Dano() { DanoHabilidade = Convert.ToInt32(r["hab2_dano"]) },
                        DanoPorTurno = new DanoPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab2_dano_por_turno"]), Turnos = Convert.ToInt32(r["hab2_dano_por_turno_turnos"]) },
                        DanoPerfurante = new DanoPerfurante() { DanoHabilidade = Convert.ToInt32(r["hab2_dano_perfurante"]) },
                        DanoPerfurantePorTurno = new DanoPerfurantePorTurno() { DanoHabilidade = Convert.ToInt32(r["hab2_dano_perfurante_por_turno"]), Turnos = Convert.ToInt32(r["hab2_dano_perfurante_por_turno_turnos"]) },
                        DanoVerdadeiro = new DanoVerdadeiro() { DanoHabilidade = Convert.ToInt32(r["hab2_dano_verdadeiro"]) },
                        DanoVerdadeiroPorTurno = new DanoVerdadeiroPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab2_dano_verdadeiro_por_turno"]), Turnos = Convert.ToInt32(r["hab2_dano_verdadeiro_por_turno_turnos"]) },
                        Cura = new Cura() { CuraHabilidade = Convert.ToInt32(r["hab2_cura"]) },
                        CuraPorTurno = new CuraPorTurno() { CuraHabilidade = Convert.ToInt32(r["hab2_cura_por_turno"]), Turnos = Convert.ToInt32(r["hab2_cura_por_turno_turnos"]) },
                        Armadura = new Armadura() { ArmaduraHabilidade = Convert.ToInt32(r["hab2_armadura"]) },
                        ArmaduraPorTurno = new ArmaduraPorTurno() { ArmaduraHabilidade = Convert.ToInt32(r["hab2_armadura_por_turno"]), Turnos = Convert.ToInt32(r["hab2_armadura_por_turno_turnos"]) },
                        Descricao = r["hab2_descricao"] is DBNull ? string.Empty : Convert.ToString(r["hab2_descricao"]),
                        Recarga = Convert.ToInt32(r["hab2_recarga"]),
                        Invulnerabilidade = Convert.ToInt32(r["hab2_invulnerabilidade"]),
                        Disposicao = Convert.ToInt32(r["hab2_disposicao"])
                    };
                    Habilidade hab3 = new Habilidade()
                    {
                        Id = Convert.ToInt32(r["id_hab3"]),
                        Nome = Convert.ToString(r["hab3_nome"]),
                        Foto = (byte[])r["hab3_foto"],
                        Dano = new Dano() { DanoHabilidade = Convert.ToInt32(r["hab3_dano"]) },
                        DanoPorTurno = new DanoPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab3_dano_por_turno"]), Turnos = Convert.ToInt32(r["hab3_dano_por_turno_turnos"]) },
                        DanoPerfurante = new DanoPerfurante() { DanoHabilidade = Convert.ToInt32(r["hab3_dano_perfurante"]) },
                        DanoPerfurantePorTurno = new DanoPerfurantePorTurno() { DanoHabilidade = Convert.ToInt32(r["hab3_dano_perfurante_por_turno"]), Turnos = Convert.ToInt32(r["hab3_dano_perfurante_por_turno_turnos"]) },
                        DanoVerdadeiro = new DanoVerdadeiro() { DanoHabilidade = Convert.ToInt32(r["hab3_dano_verdadeiro"]) },
                        DanoVerdadeiroPorTurno = new DanoVerdadeiroPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab3_dano_verdadeiro_por_turno"]), Turnos = Convert.ToInt32(r["hab3_dano_verdadeiro_por_turno_turnos"]) },
                        Cura = new Cura() { CuraHabilidade = Convert.ToInt32(r["hab3_cura"]) },
                        CuraPorTurno = new CuraPorTurno() { CuraHabilidade = Convert.ToInt32(r["hab3_cura_por_turno"]), Turnos = Convert.ToInt32(r["hab3_cura_por_turno_turnos"]) },
                        Armadura = new Armadura() { ArmaduraHabilidade = Convert.ToInt32(r["hab3_armadura"]) },
                        ArmaduraPorTurno = new ArmaduraPorTurno() { ArmaduraHabilidade = Convert.ToInt32(r["hab3_armadura_por_turno"]), Turnos = Convert.ToInt32(r["hab3_armadura_por_turno_turnos"]) },
                        Descricao = r["hab3_descricao"] is DBNull ? string.Empty : Convert.ToString(r["hab3_descricao"]),
                        Recarga = Convert.ToInt32(r["hab3_recarga"]),
                        Invulnerabilidade = Convert.ToInt32(r["hab3_invulnerabilidade"]),
                        Disposicao = Convert.ToInt32(r["hab3_disposicao"])
                    };
                    Habilidade hab4 = new Habilidade()
                    {
                        Id = Convert.ToInt32(r["id_hab4"]),
                        Nome = Convert.ToString(r["hab4_nome"]),
                        Foto = (byte[])r["hab4_foto"],
                        Dano = new Dano() { DanoHabilidade = Convert.ToInt32(r["hab4_dano"]) },
                        DanoPorTurno = new DanoPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab4_dano_por_turno"]), Turnos = Convert.ToInt32(r["hab4_dano_por_turno_turnos"]) },
                        DanoPerfurante = new DanoPerfurante() { DanoHabilidade = Convert.ToInt32(r["hab4_dano_perfurante"]) },
                        DanoPerfurantePorTurno = new DanoPerfurantePorTurno() { DanoHabilidade = Convert.ToInt32(r["hab4_dano_perfurante_por_turno"]), Turnos = Convert.ToInt32(r["hab4_dano_perfurante_por_turno_turnos"]) },
                        DanoVerdadeiro = new DanoVerdadeiro() { DanoHabilidade = Convert.ToInt32(r["hab4_dano_verdadeiro"]) },
                        DanoVerdadeiroPorTurno = new DanoVerdadeiroPorTurno() { DanoHabilidade = Convert.ToInt32(r["hab4_dano_verdadeiro_por_turno"]), Turnos = Convert.ToInt32(r["hab4_dano_verdadeiro_por_turno_turnos"]) },
                        Cura = new Cura() { CuraHabilidade = Convert.ToInt32(r["hab4_cura"]) },
                        CuraPorTurno = new CuraPorTurno() { CuraHabilidade = Convert.ToInt32(r["hab4_cura_por_turno"]), Turnos = Convert.ToInt32(r["hab4_cura_por_turno_turnos"]) },
                        Armadura = new Armadura() { ArmaduraHabilidade = Convert.ToInt32(r["hab4_armadura"]) },
                        ArmaduraPorTurno = new ArmaduraPorTurno() { ArmaduraHabilidade = Convert.ToInt32(r["hab4_armadura_por_turno"]), Turnos = Convert.ToInt32(r["hab4_armadura_por_turno_turnos"]) },
                        Descricao = r["hab4_descricao"] is DBNull ? string.Empty : Convert.ToString(r["hab4_descricao"]),
                        Recarga = Convert.ToInt32(r["hab4_recarga"]),
                        Invulnerabilidade = Convert.ToInt32(r["hab4_invulnerabilidade"]),
                        Disposicao = Convert.ToInt32(r["hab4_disposicao"])
                    };

                    monstro.Habilidades.Add(hab1);
                    monstro.Habilidades.Add(hab2);
                    monstro.Habilidades.Add(hab3);
                    monstro.Habilidades.Add(hab4);
                    lst.Add(monstro);
                }
            }

            return lst;
        }
    }
}
