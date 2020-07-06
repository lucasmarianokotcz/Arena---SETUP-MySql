namespace Model.Shared
{
    public class Dano
    {
        public int DanoHabilidade { get; set; }
    }

    public class DanoPorTurno : Dano
    {
        public int Turnos { get; set; }
    }

    public class DanoPerfurante : Dano { }
    public class DanoPerfurantePorTurno : DanoPorTurno { }
    public class DanoVerdadeiro : Dano { }
    public class DanoVerdadeiroPorTurno : DanoPorTurno { }
}
