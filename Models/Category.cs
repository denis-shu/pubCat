using System;
using System.ComponentModel.DataAnnotations;

namespace Bolt.Models
{
    public class Category
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category Order")]
        public int DisplayOrder { get; set; }
    }
}
