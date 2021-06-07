using System;
using System.Collections.Generic;

#nullable disable

namespace entityNuget.Models.DB
{
    public partial class TbUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mensaje { get; set; }
    }
}
