namespace Model.Shared
{
    public class Cura
    {
        public int CuraHabilidade { get; set; }
    }

    public class CuraPorTurno : Cura
    {
        public int Turnos { get; set; }
    }
}
