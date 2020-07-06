using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Personagem.Regras.Classes
{
    public class PersonagemValidacao
    {
        private readonly StringBuilder e = new StringBuilder();
        public string Erros => e.ToString();

        public void ValidaPersonagem(Personagem personagem)
        {
            if (personagem is null)
            {
                e.Append("Erro ao definir Personagem.\n");
                return;
            }

            ValidaNome(personagem.Nome, PersonagemRegras.MaximoCaracteresNomePersonagem);
            ValidaFoto(personagem.Foto);
            ValidaHabilidades(personagem.Habilidades);
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

        private void ValidaHabilidades(List<HabilidadePersonagem> habilidades)
        {
            int totalHabs = PersonagemRegras.NumeroHabilidades;
            if (habilidades is null || habilidades.Any(x => x is null) || habilidades.Count < totalHabs || habilidades.Count > totalHabs)
            {
                e.Append($"Um Personagem deve ter {totalHabs} habilidades no total.\n");
                return;
            }

            foreach (HabilidadePersonagem hab in habilidades)
            {
                ValidaNome(hab.Nome, PersonagemRegras.MaximoCaracteresNomeHabilidade);
                ValidaFoto(hab.Foto);
            }
        }
    }
}
