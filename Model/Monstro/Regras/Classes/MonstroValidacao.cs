using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Monstro.Regras.Classes
{
    public class MonstroValidacao
    {
        private readonly StringBuilder e = new StringBuilder();
        public string Erros => e.ToString();

        public void ValidaMonstro(Monstro monstro)
        {
            if (monstro is null)
            {
                e.Append("Erro ao definir Monstro.\n");
                return;
            }

            ValidaNome(monstro.Nome, MonstroRegras.MaximoCaracteresNomeMonstro);
            ValidaFoto(monstro.Foto);
            ValidaHabilidades(monstro.Habilidades);
        }

        private void ValidaNome(string nome, int maximoCaracteres)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                e.Append("Nome não pode ser vazio.\n");
            }
            else if (nome.Length > maximoCaracteres)
            {
                e.Append($"Nome não pode ter mais que {maximoCaracteres} caracteres.\n");
            }
        }

        private void ValidaFoto(byte[] foto)
        {
            if (foto is null)
            {
                e.Append("Foto inválida.\n");
            }
        }

        private void ValidaHabilidades(List<HabilidadeMonstro> habilidades)
        {
            int totalHabs = MonstroRegras.NumeroHabilidades;
            if (habilidades is null || habilidades.Any(x => x is null) || habilidades.Count < totalHabs || habilidades.Count > totalHabs)
            {
                e.Append($"Um Monstro deve ter {totalHabs} habilidades no total.\n");
                return;
            }

            foreach (HabilidadeMonstro hab in habilidades)
            {
                ValidaNome(hab.Nome, MonstroRegras.MaximoCaracteresNomeHabilidade);
                ValidaFoto(hab.Foto);
            }
        }
    }
}
