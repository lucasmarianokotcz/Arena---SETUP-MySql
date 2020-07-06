using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace AcessoDados
{
    public class Acesso
    {
        public bool Insert(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();

                using (MySqlConnection conn = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conn.Open();
                    sql.Connection = conn;
                    sql.CommandText = query;

                    if (parameters != null && parameters.Count > 0)
                        foreach (var parameter in parameters)
                            sql.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    return sql.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ ex.Message } - Método: Insert");
            }
        }

        public int Insert_(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();

                using (MySqlConnection conn = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conn.Open();
                    sql.Connection = conn;
                    sql.CommandText = query + "; SELECT LAST_INSERT_ID(); ";

                    if (parameters != null && parameters.Count > 0)
                        foreach (var parameter in parameters)
                            sql.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    return Convert.ToInt32(sql.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ ex.Message } - Método: Insert");
            }
        }

        public bool Update(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();

                using (MySqlConnection conn = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conn.Open();
                    sql.Connection = conn;
                    sql.CommandText = query;

                    if (parameters != null && parameters.Count > 0)
                        foreach (var parameter in parameters)
                            sql.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    return sql.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ ex.Message } - Método: Update");
            }
        }

        public bool Delete(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                MySqlCommand sql = new MySqlCommand();

                using (MySqlConnection conn = new MySqlConnection(Conexao.StringConexaoMySql))
                {
                    conn.Open();
                    sql.Connection = conn;
                    sql.CommandText = query;

                    if (parameters != null && parameters.Count > 0)
                        foreach (var parameter in parameters)
                            sql.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    return sql.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ ex.Message } - Método: Delete");
            }
        }

        public DataTable Select(string query, Dictionary<string, object> parameters = null)
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
                        foreach (var parameter in parameters)
                            sql.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    dt.Load(sql.ExecuteReader());
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ ex.Message } - Método: Select");
            }
        }
    }
}
