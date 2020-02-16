using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityManager.Mvc.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Mitarbeiter Id")]
        [Required(ErrorMessage ="Mitarbeiter Id ist ein Pflichtfeld")]
        public int EmployeeID { get; set; }

        [Column("FirstName")]
        [Required]
        [Display(Name="Vorname")]
        [StringLength(10, ErrorMessage ="Der Vorname darf nicht länger als 10 Zeichen sein")]
        public string FirstName { get; set; }

        [Column("LastName")]
        [Required]
        [Display(Name ="Nachname")]
        [StringLength(20, ErrorMessage ="Nachname darf max. 20 Zeichen lang sein")]
        public string LastName { get; set; }

        [Column("Title")]
        [Required]
        [Display(Name ="Titel")]
        [StringLength(30)]
        public string Title { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }

        public string Country { get; set; }

        [StringLength(500)]
        [Display(Name ="Bemerkungen")]
        public string Notes { get; set; }
    }
}
