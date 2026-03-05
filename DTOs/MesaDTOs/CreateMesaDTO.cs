namespace Controlador_de_comandas.DTOs.MesaDTOs
{
    public class CreateMesaDTO
    {
        public int NumeroMesa { get; set; }
        public int QuantidadeMaxPessoas { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}   
