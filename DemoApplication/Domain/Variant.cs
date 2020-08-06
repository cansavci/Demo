using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.Domain
{
    public class Variant
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int AssetId { get; set; }
    }
}
