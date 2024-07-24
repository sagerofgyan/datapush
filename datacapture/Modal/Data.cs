using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace datacapture.Modal
{
    public class Data
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  id {  get; set; }
        [Display(Name = "First_Name")]
        [StringLength(20, MinimumLength = 1)] //(optional, any alphanumeric character - maximum 20)
        public string First_Name { get; set; }
        [Display(Name = "Last_Name")]
        [Required,StringLength(20, MinimumLength = 1, ErrorMessage = "*")] //(optional, any alphanumeric character - maximum 20)
        public string Last_Name { get; set; }//mandatory, any alphanumeric character - maximum 20)
        [Required, StringLength(100, MinimumLength = 1, ErrorMessage = "*")] //(mandatory, any alphanumeric character - maximum 100)
        public string Description { get; set; }
        [Range(1, 20, ErrorMessage = "*")]
        public int Quantity { get; set; }   //(number between 1 and 20)

 
    }
}
