using System.Collections.Generic;

namespace Model.Personagem
{
    public class Personagem : BaseModel
    {
        private string _descricao;

        public string Nome { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _descricao = null;
                }
                else
                {
                    _descricao = value;
                }
            }
        }
        public byte[] Foto { get; set; }
        public List<HabilidadePersonagem> Habilidades { get; set; }

        public Personagem()
        {
            Habilidades = new List<HabilidadePersonagem>();
        }

        // IN-GAME
        public bool IsInvulneravel { get; set; } = false;
    }
}
