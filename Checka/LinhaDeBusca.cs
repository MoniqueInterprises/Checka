namespace Checka
{
    internal class LinhaDeBusca
    {
       public string Conc;
        public List<string> RazaoSocial;
        public string Valor;
        public double ValorDouble;
        public List<string> ValorList;  

        public LinhaDeBusca(string conc, List<string> razaoSocial, string valor)
        {
            Conc = conc;
            RazaoSocial = razaoSocial;
            Valor = valor;
        }
        public LinhaDeBusca()
        {

        }
    }
}
