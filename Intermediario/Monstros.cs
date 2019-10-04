using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermediario
{
    public class Monstros
    {
        AcessoDados.Monstros MonstrosAcesso;
        private DataTable dadosTabela;

        protected int Monstro_ID;

        public DataTable Listar()
        {
            try
            {
                MonstrosAcesso = new AcessoDados.Monstros();
                dadosTabela = MonstrosAcesso.Listar();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarSearchID()
        {
            try
            {
                GerarMonstroAleatorio();


                MonstrosAcesso = new AcessoDados.Monstros();
                dadosTabela = MonstrosAcesso.ListarSearch(Monstro_ID);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected int[] RetornaIDsExistentes()
        {
            MonstrosAcesso = new AcessoDados.Monstros();

            int[] d = MonstrosAcesso.RetornaIDs();
            return d;
        }

        public void GerarMonstroAleatorio()
        {
            int[] IDs;
            
            IDs = RetornaIDsExistentes();

            int index = 0;
            Random rnd = new Random();

            index = rnd.Next(0, (IDs.Length));

            Monstro_ID = IDs[index];
        }
    }
}
