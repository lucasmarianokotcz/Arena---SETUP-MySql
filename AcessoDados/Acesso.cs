using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AcessoDados
{
    public class Acesso
    {
        public DataTable Select(string query, List<MySqlParameter> parameters = null)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();
                DataTable dt = new DataTable();

                using (MySqlConnection conn = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conn.Open();
                    sql.Connection = conn;
                    sql.CommandText = query;

                    if (parameters != null && parameters.Count > 0)
                        foreach (MySqlParameter parameter in parameters)
                            sql.Parameters.Add(parameter);

                    dt.Load(sql.ExecuteReader());
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
