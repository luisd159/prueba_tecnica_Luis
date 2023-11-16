namespace prueba_tecnica.Models
{
    public class Personas
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Numero_Identificacion { get; set; }
        public string Email { get; set; }
        public string Tipo_Identificacion { get; set; }
        public string Fecha_Creacion { get; set; }
        public string? Cnombre { get; }
        public string? Cidentificacion{ get; }
    }
}
