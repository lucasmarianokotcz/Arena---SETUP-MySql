using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Intermediario
{
    public class Personagens
    {
        AcessoDados.Personagens PersonagensAcesso;
        private DataTable dadosTabela;

        public DataTable Listar()
        {
            try
            {
                PersonagensAcesso = new AcessoDados.Personagens();
                dadosTabela = PersonagensAcesso.Listar();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarSearch(string Nome)
        {
            try
            {
                PersonagensAcesso = new AcessoDados.Personagens();
                return PersonagensAcesso.ListarSearch(Nome);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
