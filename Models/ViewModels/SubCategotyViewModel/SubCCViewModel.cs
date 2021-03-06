﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bolt.Models.SubCategotyViewModel
{
    public class SubCCViewModel
    {
        public SubCategory SubCategory { get; set; }

        public IEnumerable<Category> CategoryList { get; set; }

        public List<string> SubCategoryList { get; set; }

        [Display(Name="New Sub Category")]
        public bool IsNew { get; set; }

        public string StatusMessage { get; set; }
    }
}
