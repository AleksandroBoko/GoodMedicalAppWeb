using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.Domain.Models
{
    public class OperationTransfer
    {
        public int Id { get; set; }
        public TypeOperation CurrentTypeOperation { get; set; }
        public int TreatmentId { get; set; }
        public IList<int> Medicines { get; set; }
    }
}
