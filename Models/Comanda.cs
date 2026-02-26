namespace Controlador_de_comandas.Models
{
    //Modelo de dados para a entidade "Comanda", representando uma comanda de um cliente em um restaurante
    public class Comanda
    {
        public int Id { get; set; }
        public int MesaId { get; set; }
        public Mesa Mesa { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }

        public List<ItemComanda> Pedidos { get; set; } = new();
    }
}
