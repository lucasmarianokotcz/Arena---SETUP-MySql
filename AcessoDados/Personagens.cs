using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AcessoDados
{
    public class Personagens
    {
        MySqlCommand comandoSql;
        StringBuilder sql;
        DataTable dadosTabela;
                
        public DataTable Listar()
        {
            try
            {
                MySqlCommand ComandoSql = new MySqlCommand();
                sql = new StringBuilder();
                dadosTabela = new DataTable();

                using (MySqlConnection conexao = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conexao.Open();

                    sql.Append("USE bmh6qyguc3q2m5pj55e9; ");
                    sql.Append("SELECT * FROM Personagens ");
                    sql.Append("ORDER BY Nome_Personagem ASC");

                    ComandoSql.CommandText = sql.ToString();
                    ComandoSql.Connection = conexao;
                    dadosTabela.Load(ComandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método Listar, tabela Personagens! Caso o problema persista, entre em contato com o Administrador!\n\nMensagem de erro:\n" + ex.Message);
            }
        }
        public DataTable ListarSearch(string Nome)
        {
            try
            {
                comandoSql = new MySqlCommand();
                sql = new StringBuilder();
                dadosTabela = new DataTable();

                using (MySqlConnection conexao = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conexao.Open();

                    sql.Append("USE bmh6qyguc3q2m5pj55e9; ");
                    sql.Append("SELECT * FROM Personagens ");                    
                    sql.Append("WHERE Nome_Personagem LIKE @Nome ");
                    sql.Append("ORDER BY Nome_Personagem ASC");

                    comandoSql.Parameters.Add(new MySqlParameter("@Nome", "%" + Nome + "%"));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método ListarSearch. Se o problema persistir, contate o Administrador.\n\nMensagem de erro:\n" + ex.Message);
            }

        }
    }
}
