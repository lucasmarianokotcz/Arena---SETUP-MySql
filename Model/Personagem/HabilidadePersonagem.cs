using Model.Personagem.Energias;

namespace Model.Personagem
{
    public class HabilidadePersonagem : Habilidade
    {
        public EnergiaVerde EnergiaVerde { get; set; }
        public EnergiaAzul EnergiaAzul { get; set; }
        public EnergiaVermelho EnergiaVermelho { get; set; }
        public EnergiaPreto EnergiaPreto { get; set; }

        public HabilidadePersonagem()
        {
            EnergiaVerde = new EnergiaVerde();
            EnergiaAzul = new EnergiaAzul();
            EnergiaVermelho = new EnergiaVermelho();
            EnergiaPreto = new EnergiaPreto();
        }
    }
}
