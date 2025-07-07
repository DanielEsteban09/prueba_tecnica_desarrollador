using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Respuesta
    {
        public string mensaje { get; set; }

        [Key]
        public decimal identificador { get; set; }
        public string estado { get; set; }
    }
}
