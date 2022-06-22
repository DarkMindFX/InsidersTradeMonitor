using ITM.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Function.ImportForm4Reports.Helpers
{
    public class ImportRunDalFacade : IImportRunDalFacade
    {
        private ITM.Services.Dal.IImportRunDal _importRunDal = null;
        private ITM.Services.Dal.IImportRunForm4ReportDal _importRunForm4ReportDal = null;


        public ImportRunDalFacade(  ITM.Services.Dal.IImportRunDal importRunDal,
                                    ITM.Services.Dal.IImportRunForm4ReportDal importRunForm4ReportDal)
        {
            this._importRunDal = importRunDal;
            this._importRunForm4ReportDal = importRunForm4ReportDal;
        }

        #region ImportRunDalWrapper interface

        public ImportRun InsertImportRun(ImportRun importRun)
        {
            var entity = _importRunDal.Insert(importRun);
            return entity;
        }

        public ImportRunForm4Report InsertImportRunForm4Report(ImportRunForm4Report importRunForm4Report)
        {
            var entity = _importRunForm4ReportDal.Insert(importRunForm4Report);
            return entity;
        }

        public ImportRun SetRunFailed(long runId)
        {
            ImportRun entity = _importRunDal.Get(runId);
            entity.TimeEnd = DateTime.UtcNow;
            entity.StateID = (long)EImportRunState.Fail;
            entity = _importRunDal.Update(entity);
            return entity;
        }

        public ImportRun SetRunSucceeded(long runId)
        {
            ImportRun entity = _importRunDal.Get(runId);
            entity.TimeEnd = DateTime.UtcNow;
            entity.StateID = (long)EImportRunState.Success;
            entity = _importRunDal.Update(entity);
            return entity;
        }

        public ImportRun UpdateImportRun(ImportRun importRun)
        {
            var entity = _importRunDal.Update(importRun);
            return entity;
        }

        public ImportRunForm4Report UpdateImportRunForm4Report(ImportRunForm4Report importRunForm4Report)
        {
            var entity = _importRunForm4ReportDal.Update(importRunForm4Report);
            return entity;
        }



        #endregion
    }
}
