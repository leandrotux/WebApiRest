using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiMarcas.Models
{
    [Table("tb_marca")]
    public class Marca
    {   
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "A descrição da marca é obrigatória.")]
        [MaxLength(100, ErrorMessage ="A descrição da marca só permite no máximo 100 caracteres.")]
        public string DescricaoMarca { get; set; }
    }
}