namespace Controlador_de_comandas.Models
{
    //Modelo de dados para a entidade "Produto", representando um produto disponível para pedido em um restaurante
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal PrecoProduto { get; set; }
    }
}
