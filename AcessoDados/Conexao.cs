using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcessoDados
{
    public class Conexao
    {
        private static string ConexaoMySql = @"Server=bmh6qyguc3q2m5pj55e9-mysql.services.clever-cloud.com;Database=bmh6qyguc3q2m5pj55e9;Uid=uyxmznkbb9o31umh;Pwd=B2r3ozXSEo6YengDgkDT;SslMode=none";

        public static string StringConexaoMySql
        {
            get { return ConexaoMySql; }
        }
    }
}
