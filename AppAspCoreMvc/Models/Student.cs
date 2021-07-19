using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppAspCoreMvc.Models
{
    public class Student
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La fecha de nacimeinto es requerido")]
        public DateTime DateBirth { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Address> Address { get; set; }
        //[NotMapped]
        public ICollection<Assigment> Assigments { get; set; }

        public ICollection<Survey> Surveys { get; set; }
    }
}
