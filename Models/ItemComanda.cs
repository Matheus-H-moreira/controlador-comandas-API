namespace Controlador_de_comandas.Models
{
    //Modelo de dados para a entidade "ItemComanda", representando um item pedido em uma comanda
    public class ItemComanda
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }
        public string NomeProdutoPedido { get; set; } = string.Empty;
        public int QuantidadeProduto { get; set; }
    }
}
