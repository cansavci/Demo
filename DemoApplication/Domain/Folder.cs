using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApplication.Domain
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? OwnerFolderId { get; set; }
    }
}
