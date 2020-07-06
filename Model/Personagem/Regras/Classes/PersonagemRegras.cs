namespace Model.Personagem.Regras.Classes
{
    public static class PersonagemRegras
    {
        public static int NumeroHabilidades => 4;
        public static string NomeTabela => "personagem";
        public static string AliasTabela => "p";
        public static int MaximoCaracteresNomePersonagem => 30;
        public static int MaximoCaracteresNomeHabilidade => 30;
    }
}
