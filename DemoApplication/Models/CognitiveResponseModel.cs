using System;
using System.Collections.Generic;
using DemoApplication.Domain;

namespace DemoApplication.Models
{
    public class CognitiveResponseModel
    {
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
    }
}
