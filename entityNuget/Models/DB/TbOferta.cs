using System;
using System.Collections.Generic;

#nullable disable

namespace entityNuget.Models.DB
{
    public partial class TbOferta
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int? Precio { get; set; }
    }
}
