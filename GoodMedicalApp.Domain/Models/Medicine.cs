using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.Domain.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string MethodUsing { get; set; }
        public TypeMedicine CurrentTypeMedicine { get; set; }
    }
}
