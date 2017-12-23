using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPresentation.Models.Provider
{
    public class AnticipoModel
    {
        public int AnticipoId { get; set; }

        public int ClienteId { get; set; }

        public ClienteModel Cliente { get; set; }
        
        public decimal Valor { get; set; }
       
        public DateTime Fecha { get; set; }
    }
}
