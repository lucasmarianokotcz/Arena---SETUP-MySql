namespace Model.Shared
{
    public class Armadura
    {
        public int ArmaduraHabilidade { get; set; }
    }

    public class ArmaduraPorTurno : Armadura
    {
        public int Turnos { get; set; }
    }
}
