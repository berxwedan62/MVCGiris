using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGiris.Models
{
    public class OrtakModel
    {
        public IEnumerable<HaberModel> HaberModels { get; set; }
        public IEnumerable<MesajModel> MesajModels { get; set; }
    }
}