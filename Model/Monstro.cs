using System.Collections.Generic;

namespace Model
{
    public class Monstro : BaseModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Foto { get; set; }
        public List<Habilidade> Habilidades { get; set; }

        public Monstro()
        {
            Habilidades = new List<Habilidade>();
        }
    }
}
