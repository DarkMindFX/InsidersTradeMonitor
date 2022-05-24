using ITM.Interfaces;
using ITM.Interfaces.Entities;

namespace ITM.Service.DataImporter.Helpers
{
    public class Form4DalWrapper : IForm4DalWrapper
    {
        private ITM.Interfaces.IForm4ReportDal _form4ReportDal = null;
        private ITM.Interfaces.IDerivativeTransactionDal _derivTransDal = null;
        private ITM.Interfaces.INonDerivativeTransactionDal _nonDerivTransDal = null;
        private ITM.Interfaces.IEntityDal _entityDal = null;
        private ITM.Interfaces.IEntityTypeDal _entityTypeDal = null;
        private ITM.Interfaces.IOwnershipTypeDal _ownershipTypeDal = null;
        private ITM.Interfaces.ITransactionCodeDal _transCodeDal = null;
        private ITM.Interfaces.ITransactionTypeDal _transTypeDal = null;

        public Form4DalWrapper( IForm4ReportDal form4ReportDal,
                                IDerivativeTransactionDal derivTransDal,
                                INonDerivativeTransactionDal nonDerivTransDal,
                                IEntityDal entityDal,
                                IEntityTypeDal entityTypeDal,
                                IOwnershipTypeDal ownershipTypeDal,
                                ITransactionCodeDal transCodeDal,
                                ITransactionTypeDal transTypeDal)
        {
            _form4ReportDal = form4ReportDal;
            _derivTransDal = derivTransDal;
            _nonDerivTransDal = nonDerivTransDal;
            _entityDal = entityDal;
            _entityTypeDal = entityTypeDal;
            _ownershipTypeDal = ownershipTypeDal;
            _transCodeDal = transCodeDal;
            _transTypeDal = transTypeDal;
        }

        public long InsertReport(IStatement form4Statement)
        {
            var form4Report = form4Statement as ITM.Parser.Form4.Form4Report;

            var form4 = new Form4Report();

            form4.IssuerID = (long)GetEntityIDByCIK(form4Report.IssuerCIK, (long)EEntityType.Company);
            form4.ReporterID = (long)GetEntityIDByCIK(form4Report.OwnerCIK, (long)EEntityType.Person);
            form4.Is10PctHolder = form4Report.IsTenPercentHolder;
            form4.IsDirector = form4Report.IsDirector;
            form4.IsOfficer = form4Report.IsOfficer;
            form4.IsOther = form4Report.IsOther;
            form4.OfficerTitle = form4Report.OfficerTitle;
            form4.OtherText = form4Report.OwnerOtherText;
            form4.Date = form4Report.PeriodOfReport;

            var result = _form4ReportDal.Insert(form4);

            return (long)result.ID;
        }

        protected NonDerivativeTransaction Convert(ITM.Parser.Form4.NonDerivativeTransaction trans)
        {
            var result = new NonDerivativeTransaction();
            result.TransactionTypeID = (long)GetTransactionTypeIDByCode(trans.TransactionADType);
            result.TransactionCodeID = (long)GetTransactionCodeIDByCode(trans.TransactionCode);
            result.OwnershipTypeID = (long)GetOwnershipTypeIDByCode(trans.OwnershipType);

            return result;
        }

        protected long? GetTransactionCodeIDByCode(string transCode)
        {
            var entity = _transCodeDal.GetAll().FirstOrDefault(e => e.Code == transCode);
            return entity != null ? entity.ID : null;
        }

        protected long? GetTransactionTypeIDByCode(string transType)
        {
            var entity = _transTypeDal.GetAll().FirstOrDefault(e => e.Code == transType);
            return entity != null ? entity.ID : null;
        }

        protected long? GetOwnershipTypeIDByCode(string ownershipType)
        {
            var entity = _ownershipTypeDal.GetAll().FirstOrDefault(e => e.Code == ownershipType);
            return entity != null ? entity.ID : null;
        }

        protected long? GetEntityIDByCIK(string cik, long entityTypeId)
        {
            var entity = _entityDal.GetByEntityTypeID(entityTypeId).FirstOrDefault(e => e.CIK == cik);
            return entity != null ? entity.ID : null;
        }

        
    }
}
