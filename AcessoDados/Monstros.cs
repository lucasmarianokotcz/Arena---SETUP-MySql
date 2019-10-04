using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AcessoDados
{
    public class Monstros
    {
        MySqlCommand comandoSql;
        StringBuilder sql;
        DataTable dadosTabela;

        public DataTable Listar()
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
                    sql.Append("SELECT * FROM Monstros ");
                    sql.Append("ORDER BY Nome_Personagem ASC");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método Listar, tabela Monstros! Caso o problema persista, entre em contato com o Administrador!\n\n" + ex.Message);
            }
        }
        public DataTable ListarSearch(int ID)
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
                    sql.Append("SELECT * FROM Monstros ");
                    sql.Append("WHERE ID_Monstro = @ID");// + ID.ToString());

                    comandoSql.Parameters.Add(new MySqlParameter("@ID", ID));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no método ListarSearch, tabela Monstros. Se o problema persistir, contate o Administrador.\n\n" + ex.Message);
            }

        }

        public int[] RetornaIDs()
        {
            comandoSql = new MySqlCommand();
            sql = new StringBuilder();
            dadosTabela = new DataTable();            

            using (MySqlConnection conexao = new MySqlConnection(Conexao.StringConexaoMySql))
            {
                conexao.Open();

                sql.Append("USE bmh6qyguc3q2m5pj55e9; ");
                sql.Append("SELECT ID_Monstro FROM Monstros");

                comandoSql.CommandText = sql.ToString();
                comandoSql.Connection = conexao;

                dadosTabela.Load(comandoSql.ExecuteReader());
            }

            int[] d = new int[dadosTabela.Rows.Count];
            int i = 0;

            foreach (DataRow row in dadosTabela.Rows)
            {
                d[i] = (int)row[0]; //row significa uma linha do datatable, o indice 0 significa a coluna com index 0, logo, ID.
                i++;
            }

            return d;
        }

    }
}
