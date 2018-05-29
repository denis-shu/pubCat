using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bolt.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name="SubCategory")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid  CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
