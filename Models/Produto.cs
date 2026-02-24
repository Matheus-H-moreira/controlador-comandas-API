namespace Controlador_de_comandas.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal PrecoProduto { get; set; }
    }
}
