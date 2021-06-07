using System;
using System.Collections.Generic;

#nullable disable

namespace entityNuget.Models.DB
{
    public partial class TbBebida
    {
        public int Id { get; set; }
        public string NombreBebida { get; set; }
        public string Descripcion { get; set; }
        public int? Precio { get; set; }
        public string ImageUrl { get; set; }
    }
}
