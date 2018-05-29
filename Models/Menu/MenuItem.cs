using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bolt.Models.Menu
{
    public class MenuItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Spicy { get; set; }
        public enum Espicy { Na = 0, Spicy = 1, VS = 2 }

        [Range(1, int.MaxValue, ErrorMessage ="Price should be > than $1")]
        public Double Price{ get; set; }

        [Display(Name="Category ")]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "SubCategory")]
        public Guid SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }
    }
}
