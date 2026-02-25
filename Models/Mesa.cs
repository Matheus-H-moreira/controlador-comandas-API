namespace Controlador_de_comandas.Models
{
    //Modelo de dados para a entidade "Mesa", representando uma mesa em um restaurante
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int QuantidadeMax { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<Comanda> ListaComanda { get; set; } = new();
    }
}
