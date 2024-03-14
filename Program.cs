namespace Automatização_De_Folha_De_Pagamento
{
    internal static class Program
    {
        public static TeladeAcesso? Acesso;
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(Acesso =new TeladeAcesso());
        }
    }
}