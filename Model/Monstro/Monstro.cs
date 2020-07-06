using System.Collections.Generic;

namespace Model.Monstro
{
    public class Monstro : BaseModel
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
        public List<HabilidadeMonstro> Habilidades { get; set; }

        public Monstro()
        {
            Habilidades = new List<HabilidadeMonstro>();
        }
    }
}
