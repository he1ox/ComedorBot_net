using System;
using System.Collections.Generic;

#nullable disable

namespace entityNuget.Models.DB
{
    public partial class TbProducto
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public int? Precio { get; set; }
        public string ImageUrl { get; set; }
    }
}
