﻿using GoodMedicalApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodMedicalApp.BusinessServices.Services
{
    public interface ITreatmentReportService : IService<TreatmentReport>
    {
        TreatmentReportTransfer GetTransferItemById(int id);
        TreatmentReport GetItemFromTransferItem(TreatmentReportTransfer treatmentReportTransfer);
    }
}
