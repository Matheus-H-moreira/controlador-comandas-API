namespace Controlador_de_comandas.Models
{
    public class ItemComanda
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }
        public string NomeProdutoPedido { get; set; } = string.Empty;
        public int QuantidadeProduto { get; set; }
    }
}
