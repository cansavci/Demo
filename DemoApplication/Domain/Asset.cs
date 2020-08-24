using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DemoApplication.Domain
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Data { get; set; }

        public int FolderId { get; set; }

        public List<Variant> Variants { get; set; }

        public Asset()
        {

        }

        public void SetVariants(List<Variant> variantList)
        {
            Variants = variantList;
        }
    }
}
