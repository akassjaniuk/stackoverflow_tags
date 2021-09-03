using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stackoverflow_tags.Models
{
    public class items
    {
        public List<collectives> collectives { get; set; }
        public string has_synonyms { get; set; }
        public string is_moderator_only { get; set; }
        public string is_required { get; set; }
        [Display(Name ="Ilość użyć (popularność)")]
        public int count { get; set; }
        [Display(Name ="Tags")]
        public string name { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.00000000000000000000}", ApplyFormatInEditMode = true)]
        [Display(Name ="Procent w populacji")]
        public decimal procent { get; set; }
    }

    public class external_links
    {
        public string type { get; set; }
        public string link { get; set; }
    }

    public class tags
    {
        public string tag { get; set; }
    }

    public class collectives
    {
        public string[] tags { get; set; }
        public List<external_links> external_links { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

    public class RootObject : items
    {
        public List<items> items { get; set; }
        public string has_more { get; set; }
        public string quota_max { get; set; }
        public string quota_remaining { get; set; }
    }
}
