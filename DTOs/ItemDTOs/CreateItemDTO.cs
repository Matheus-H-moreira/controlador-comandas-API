namespace Controlador_de_comandas.DTOs.ItemDTOs
{
    public class CreateItemDTO
    {
        public int ComandaId { get; set; }
        public string NomeProdutoPedido { get; set; } = string.Empty;
        public int QuantidadeProduto { get; set; }
    }
}
