namespace Model.Arena.Regras.Classes
{
    public static class ArenaRegras
    {
        public static int EnergiasPorRound => 5; //Energias que o personagem ganha por round.
        public static int ChanceDeComecar => 5000;// 5000;   //Chance de começar o jogo. Valor Exclusive. Quando vale 2 -> 1/2 chances de começar. Quando vale 3 -> 2/3 chances de começar.
        public static int TempoMaxPassando => 5 * 2;    //Controla o tempo (em segundos) máximo que demora pro Timer de tempo acabar. O produto da multiplicação deve ser maior que 8.
        public static int EnergiasIguaisMinimasParaTroca => 2; //Controla a quantidade de energias iguais mínimas requeridas para realizar a troca de energia.
    }
}
