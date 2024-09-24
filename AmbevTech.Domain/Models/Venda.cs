namespace AmbevTech.Domain.Models
{
    public class Venda
    {
        public Venda()
        {
            Itens = new();
        }

        public int NumeroVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public string Cliente { get; set; }
        public decimal ValorTotal { get; set; }
        public string Filial { get; set; }
        public List<ItemVenda> Itens { get; set; }
        public bool Cancelado { get; set; }
    }
}
