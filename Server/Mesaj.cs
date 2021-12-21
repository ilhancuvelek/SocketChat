using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class Mesaj
    {
        public string Gonderen { get; set; }
        public string Icerik { get; set; }
        public DateTime GonderimTarihi { get; set; }

        public override string ToString()
        {
            return Gonderen+" :  >>> "+Icerik+"         "+$"({GonderimTarihi.Hour}:{GonderimTarihi.Minute})";
        }
    }
}
