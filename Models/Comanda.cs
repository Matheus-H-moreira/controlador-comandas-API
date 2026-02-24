namespace Controlador_de_comandas.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }

        public List<ItemComanda> Pedidos { get; set; }

    }
}
