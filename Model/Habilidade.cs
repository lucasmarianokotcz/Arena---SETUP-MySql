using Model.Atributos;

namespace Model
{
    public class Habilidade : BaseModel
    {
        public string Nome { get; set; }
        public byte[] Foto { get; set; }
        public string Descricao { get; set; }
        public Dano Dano { get; set; }
        public DanoPorTurno DanoPorTurno { get; set; }
        public DanoPerfurante DanoPerfurante { get; set; }
        public DanoPerfurantePorTurno DanoPerfurantePorTurno { get; set; }
        public DanoVerdadeiro DanoVerdadeiro { get; set; }
        public DanoVerdadeiroPorTurno DanoVerdadeiroPorTurno { get; set; }
        public Cura Cura { get; set; }
        public CuraPorTurno CuraPorTurno { get; set; }
        public Armadura Armadura { get; set; }
        public ArmaduraPorTurno ArmaduraPorTurno { get; set; }
        public int Recarga { get; set; }
        public int Invulnerabilidade { get; set; }
        public int Disposicao { get; set; }

        public Habilidade()
        {
            Dano = new Dano();
            DanoPorTurno = new DanoPorTurno();
            DanoPerfurante = new DanoPerfurante();
            DanoPerfurantePorTurno = new DanoPerfurantePorTurno();
            DanoVerdadeiro = new DanoVerdadeiro();
            DanoVerdadeiroPorTurno = new DanoVerdadeiroPorTurno();
            Cura = new Cura();
            CuraPorTurno = new CuraPorTurno();
            Armadura = new Armadura();
            ArmaduraPorTurno = new ArmaduraPorTurno();
        }
    }
}
