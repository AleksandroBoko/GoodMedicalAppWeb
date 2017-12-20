using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.Domain.Models
{
    public class TreatmentReport
    {
        public int Id { get; set; }
        public Treatment CurrentTreatment { get; set; }
        public string Conclusion { get; set; }
        public string Comment { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
    }
}
