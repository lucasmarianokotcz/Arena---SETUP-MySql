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

        public bool Insert(Personagem obj)
        {
            bool aux;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            #region Query
            sql.Append($" INSERT INTO {personagem} VALUES ");
            sql.Append(" (NULL, ");
            sql.Append($" @nome_{personagem}, ");
            sql.Append($" @descricao_{personagem}, ");
            sql.Append($" @foto_{personagem}) ");
            #endregion

            #region Parameters            
            parameters[$"@nome_{personagem}"] = obj.Nome;
            parameters[$"@descricao_{personagem}"] = obj.Descricao;
            parameters[$"@foto_{personagem}"] = obj.Foto;
            #endregion

            obj.Id = acesso.Insert_(sql.ToString(), parameters);
            aux = obj.Id > 0;

            for (int i = 1; i <= numeroHabilidades; i++)
            {
                if (!aux)
                    return aux;

                #region Query
                sql.Clear();
                sql.Append($" INSERT INTO {personagem}_hab{i} VALUES ");
                sql.Append(" (NULL, ");
                sql.Append($" @hab{i}_nome, ");
                sql.Append($" @hab{i}_foto, ");
                sql.Append($" @hab{i}_dano, ");
                sql.Append($" @hab{i}_dano_por_turno, ");
                sql.Append($" @hab{i}_dano_por_turno_turnos, ");
                sql.Append($" @hab{i}_dano_perfurante, ");
                sql.Append($" @hab{i}_dano_perfurante_por_turno, ");
                sql.Append($" @hab{i}_dano_perfurante_por_turno_turnos, ");
                sql.Append($" @hab{i}_dano_verdadeiro, ");
                sql.Append($" @hab{i}_dano_verdadeiro_por_turno, ");
                sql.Append($" @hab{i}_dano_verdadeiro_por_turno_turnos, ");
                sql.Append($" @hab{i}_cura, ");
                sql.Append($" @hab{i}_cura_por_turno, ");
                sql.Append($" @hab{i}_cura_por_turno_turnos, ");
                sql.Append($" @hab{i}_armadura, ");
                sql.Append($" @hab{i}_armadura_por_turno, ");
                sql.Append($" @hab{i}_armadura_por_turno_turnos, ");
                sql.Append($" @hab{i}_descricao, ");
                sql.Append($" @hab{i}_recarga, ");
                sql.Append($" @hab{i}_verde, ");
                sql.Append($" @hab{i}_azul, ");
                sql.Append($" @hab{i}_vermelho, ");
                sql.Append($" @hab{i}_preto, ");
                sql.Append($" @hab{i}_invulnerabilidade, ");
                sql.Append($" @hab{i}_ganho_verde, ");
                sql.Append($" @hab{i}_ganho_azul, ");
                sql.Append($" @hab{i}_ganho_vermelho, ");
                sql.Append($" @hab{i}_ganho_preto, ");
                sql.Append($" @hab{i}_id_personagem) ");
                #endregion

                #region Parameters
                parameters.Clear();
                parameters[$"@hab{i}_nome"] = obj.Habilidades[i - 1].Nome;
                parameters[$"@hab{i}_foto"] = obj.Habilidades[i - 1].Foto;
                parameters[$"@hab{i}_dano"] = obj.Habilidades[i - 1].Dano.DanoHabilidade;
                parameters[$"@hab{i}_dano_por_turno"] = obj.Habilidades[i - 1].DanoPorTurno.DanoHabilidade;
                parameters[$"@hab{i}_dano_por_turno_turnos"] = obj.Habilidades[i - 1].DanoPorTurno.Turnos;
                parameters[$"@hab{i}_dano_perfurante"] = obj.Habilidades[i - 1].DanoPerfurante.DanoHabilidade;
                parameters[$"@hab{i}_dano_perfurante_por_turno"] = obj.Habilidades[i - 1].DanoPerfurantePorTurno.DanoHabilidade;
                parameters[$"@hab{i}_dano_perfurante_por_turno_turnos"] = obj.Habilidades[i - 1].DanoPerfurantePorTurno.Turnos;
                parameters[$"@hab{i}_dano_verdadeiro"] = obj.Habilidades[i - 1].DanoVerdadeiro.DanoHabilidade;
                parameters[$"@hab{i}_dano_verdadeiro_por_turno"] = obj.Habilidades[i - 1].DanoVerdadeiroPorTurno.DanoHabilidade;
                parameters[$"@hab{i}_dano_verdadeiro_por_turno_turnos"] = obj.Habilidades[i - 1].DanoVerdadeiroPorTurno.Turnos;
                parameters[$"@hab{i}_cura"] = obj.Habilidades[i - 1].Cura.CuraHabilidade;
                parameters[$"@hab{i}_cura_por_turno"] = obj.Habilidades[i - 1].CuraPorTurno.CuraHabilidade;
                parameters[$"@hab{i}_cura_por_turno_turnos"] = obj.Habilidades[i - 1].CuraPorTurno.Turnos;
                parameters[$"@hab{i}_armadura"] = obj.Habilidades[i - 1].Armadura.ArmaduraHabilidade;
                parameters[$"@hab{i}_armadura_por_turno"] = obj.Habilidades[i - 1].ArmaduraPorTurno.ArmaduraHabilidade;
                parameters[$"@hab{i}_armadura_por_turno_turnos"] = obj.Habilidades[i - 1].ArmaduraPorTurno.Turnos;
                parameters[$"@hab{i}_descricao"] = obj.Habilidades[i - 1].Descricao;
                parameters[$"@hab{i}_recarga"] = obj.Habilidades[i - 1].Recarga;
                parameters[$"@hab{i}_verde"] = obj.Habilidades[i - 1].EnergiaVerde.Quantidade;
                parameters[$"@hab{i}_azul"] = obj.Habilidades[i - 1].EnergiaAzul.Quantidade;
                parameters[$"@hab{i}_vermelho"] = obj.Habilidades[i - 1].EnergiaVermelho.Quantidade;
                parameters[$"@hab{i}_preto"] = obj.Habilidades[i - 1].EnergiaPreto.Quantidade;
                parameters[$"@hab{i}_invulnerabilidade"] = obj.Habilidades[i - 1].Invulnerabilidade;
                parameters[$"@hab{i}_ganho_verde"] = obj.Habilidades[i - 1].EnergiaVerde.Ganho;
                parameters[$"@hab{i}_ganho_azul"] = obj.Habilidades[i - 1].EnergiaAzul.Ganho;
                parameters[$"@hab{i}_ganho_vermelho"] = obj.Habilidades[i - 1].EnergiaVermelho.Ganho;
                parameters[$"@hab{i}_ganho_preto"] = obj.Habilidades[i - 1].EnergiaPreto.Ganho;
                parameters[$"@hab{i}_id_{personagem}"] = obj.Id;
                #endregion

                aux = acesso.Insert(sql.ToString(), parameters);
            }

            return aux;
        }

        public bool Update(Personagem obj)
        {
            bool aux;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            #region Query
            sql.Append($" UPDATE {personagem} SET ");
            sql.Append($" nome_{personagem} = @nome_{personagem}, ");
            sql.Append($" descricao_{personagem} = @descricao_{personagem}, ");
            sql.Append($" foto_{personagem} = @foto_{personagem} ");
            sql.Append($" WHERE id_{personagem} = @id_{personagem} ");
            #endregion

            #region Parameters            
            parameters[$"@nome_{personagem}"] = obj.Nome;
            parameters[$"@descricao_{personagem}"] = obj.Descricao;
            parameters[$"@foto_{personagem}"] = obj.Foto;
            parameters[$"@id_{personagem}"] = obj.Id;
            #endregion

            aux = acesso.Update(sql.ToString(), parameters);

            for (int i = 1; i <= numeroHabilidades; i++)
            {
                if (!aux)
                    return aux;

                #region Query
                sql.Clear();
                sql.Append($" UPDATE {personagem}_hab{i} SET ");
                sql.Append($" hab{i}_nome = @hab{i}_nome, ");
                sql.Append($" hab{i}_foto = @hab{i}_foto, ");
                sql.Append($" hab{i}_dano = @hab{i}_dano, ");
                sql.Append($" hab{i}_dano_por_turno = @hab{i}_dano_por_turno, ");
                sql.Append($" hab{i}_dano_por_turno_turnos = @hab{i}_dano_por_turno_turnos, ");
                sql.Append($" hab{i}_dano_perfurante = @hab{i}_dano_perfurante, ");
                sql.Append($" hab{i}_dano_perfurante_por_turno = @hab{i}_dano_perfurante_por_turno, ");
                sql.Append($" hab{i}_dano_perfurante_por_turno_turnos = @hab{i}_dano_perfurante_por_turno_turnos, ");
                sql.Append($" hab{i}_dano_verdadeiro = @hab{i}_dano_verdadeiro, ");
                sql.Append($" hab{i}_dano_verdadeiro_por_turno = @hab{i}_dano_verdadeiro_por_turno, ");
                sql.Append($" hab{i}_dano_verdadeiro_por_turno_turnos = @hab{i}_dano_verdadeiro_por_turno_turnos, ");
                sql.Append($" hab{i}_cura = @hab{i}_cura, ");
                sql.Append($" hab{i}_cura_por_turno = @hab{i}_cura_por_turno, ");
                sql.Append($" hab{i}_cura_por_turno_turnos = @hab{i}_cura_por_turno_turnos, ");
                sql.Append($" hab{i}_armadura = @hab{i}_armadura, ");
                sql.Append($" hab{i}_armadura_por_turno = @hab{i}_armadura_por_turno, ");
                sql.Append($" hab{i}_armadura_por_turno_turnos = @hab{i}_armadura_por_turno_turnos, ");
                sql.Append($" hab{i}_descricao = @hab{i}_descricao, ");
                sql.Append($" hab{i}_recarga = @hab{i}_recarga, ");
                sql.Append($" hab{i}_verde = @hab{i}_verde, ");
                sql.Append($" hab{i}_azul = @hab{i}_azul, ");
                sql.Append($" hab{i}_vermelho = @hab{i}_vermelho, ");
                sql.Append($" hab{i}_preto = @hab{i}_preto, ");
                sql.Append($" hab{i}_invulnerabilidade = @hab{i}_invulnerabilidade, ");
                sql.Append($" hab{i}_ganho_verde = @hab{i}_ganho_verde, ");
                sql.Append($" hab{i}_ganho_azul = @hab{i}_ganho_azul, ");
                sql.Append($" hab{i}_ganho_vermelho = @hab{i}_ganho_vermelho, ");
                sql.Append($" hab{i}_ganho_preto = @hab{i}_ganho_preto ");
                sql.Append($" WHERE id_hab{i} = @id_hab{i} ");
                #endregion

                #region Parameters
                parameters.Clear();
                parameters[$"@hab{i}_nome"] = obj.Habilidades[i - 1].Nome;
                parameters[$"@hab{i}_foto"] = obj.Habilidades[i - 1].Foto;
                parameters[$"@hab{i}_dano"] = obj.Habilidades[i - 1].Dano.DanoHabilidade;
                parameters[$"@hab{i}_dano_por_turno"] = obj.Habilidades[i - 1].DanoPorTurno.DanoHabilidade;
                parameters[$"@hab{i}_dano_por_turno_turnos"] = obj.Habilidades[i - 1].DanoPorTurno.Turnos;
                parameters[$"@hab{i}_dano_perfurante"] = obj.Habilidades[i - 1].DanoPerfurante.DanoHabilidade;
                parameters[$"@hab{i}_dano_perfurante_por_turno"] = obj.Habilidades[i - 1].DanoPerfurantePorTurno.DanoHabilidade;
                parameters[$"@hab{i}_dano_perfurante_por_turno_turnos"] = obj.Habilidades[i - 1].DanoPerfurantePorTurno.Turnos;
                parameters[$"@hab{i}_dano_verdadeiro"] = obj.Habilidades[i - 1].DanoVerdadeiro.DanoHabilidade;
                parameters[$"@hab{i}_dano_verdadeiro_por_turno"] = obj.Habilidades[i - 1].DanoVerdadeiroPorTurno.DanoHabilidade;
                parameters[$"@hab{i}_dano_verdadeiro_por_turno_turnos"] = obj.Habilidades[i - 1].DanoVerdadeiroPorTurno.Turnos;
                parameters[$"@hab{i}_cura"] = obj.Habilidades[i - 1].Cura.CuraHabilidade;
                parameters[$"@hab{i}_cura_por_turno"] = obj.Habilidades[i - 1].CuraPorTurno.CuraHabilidade;
                parameters[$"@hab{i}_cura_por_turno_turnos"] = obj.Habilidades[i - 1].CuraPorTurno.Turnos;
                parameters[$"@hab{i}_armadura"] = obj.Habilidades[i - 1].Armadura.ArmaduraHabilidade;
                parameters[$"@hab{i}_armadura_por_turno"] = obj.Habilidades[i - 1].ArmaduraPorTurno.ArmaduraHabilidade;
                parameters[$"@hab{i}_armadura_por_turno_turnos"] = obj.Habilidades[i - 1].ArmaduraPorTurno.Turnos;
                parameters[$"@hab{i}_descricao"] = obj.Habilidades[i - 1].Descricao;
                parameters[$"@hab{i}_recarga"] = obj.Habilidades[i - 1].Recarga;
                parameters[$"@hab{i}_verde"] = obj.Habilidades[i - 1].EnergiaVerde.Quantidade;
                parameters[$"@hab{i}_azul"] = obj.Habilidades[i - 1].EnergiaAzul.Quantidade;
                parameters[$"@hab{i}_vermelho"] = obj.Habilidades[i - 1].EnergiaVermelho.Quantidade;
                parameters[$"@hab{i}_preto"] = obj.Habilidades[i - 1].EnergiaPreto.Quantidade;
                parameters[$"@hab{i}_invulnerabilidade"] = obj.Habilidades[i - 1].Invulnerabilidade;
                parameters[$"@hab{i}_ganho_verde"] = obj.Habilidades[i - 1].EnergiaVerde.Ganho;
                parameters[$"@hab{i}_ganho_azul"] = obj.Habilidades[i - 1].EnergiaAzul.Ganho;
                parameters[$"@hab{i}_ganho_vermelho"] = obj.Habilidades[i - 1].EnergiaVermelho.Ganho;
                parameters[$"@hab{i}_ganho_preto"] = obj.Habilidades[i - 1].EnergiaPreto.Ganho;
                parameters[$"@id_hab{i}"] = obj.Habilidades[i - 1].Id;
                #endregion

                aux = acesso.Update(sql.ToString(), parameters);
            };

            return aux;
        }

        public bool Delete(int id)
        {
            bool aux = true;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            for (int i = 1; i <= numeroHabilidades; i++)
            {
                if (!aux)
                    return aux;

                #region Query
                sql.Clear();
                sql.Append($" DELETE FROM {personagem}_hab{i} ");
                sql.Append($" WHERE hab{i}_id_{personagem} = @hab{i}_id_{personagem} ");
                #endregion

                #region Parameters
                parameters.Clear();
                parameters[$"@hab{i}_id_{personagem}"] = id;
                #endregion

                aux = acesso.Delete(sql.ToString(), parameters);
            };

            #region Query
            sql.Clear();
            sql.Append($" DELETE FROM {personagem} ");
            sql.Append($" WHERE id_{personagem} = @id_{personagem} ");
            #endregion

            #region Parameters       
            parameters.Clear();
            parameters[$"@id_{personagem}"] = id;
            #endregion

            aux = acesso.Delete(sql.ToString(), parameters);

            return aux;
        }

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
