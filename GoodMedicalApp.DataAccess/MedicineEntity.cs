//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoodMedicalApp.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class MedicineEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicineEntity()
        {
            this.Operations = new HashSet<OperationEntity>();
            this.TreatmentReports = new HashSet<TreatmentReportEntity>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public string MethodUsing { get; set; }
        public int TypeMedicineId { get; set; }
    
        public virtual TypeMedicineEntity TypeMedicine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OperationEntity> Operations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentReportEntity> TreatmentReports { get; set; }
    }
}
