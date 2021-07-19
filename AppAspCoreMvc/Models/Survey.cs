using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAspCoreMvc.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MyProperty { get; set; }

        //public ICollection<Question> Questions { get; set; }

        //public ICollection<Student> Students { get; set; }
    }
}
