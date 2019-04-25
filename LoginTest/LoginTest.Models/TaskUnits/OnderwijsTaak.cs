using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inzetsysteem.Models
{
    public class OnderwijsTaak : Taak
    {
        [Required]
        public string Periode { get; set; }
        [Required]
        public int BegroteUren { get; set; }
        [Required]
        public int Factor { get; set; }
        public string Onderdeel { get; set; }
    }
}
