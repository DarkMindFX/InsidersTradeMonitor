using ITM.Interfaces;
using ITM.Interfaces.Entities;
using ITM.Services.Dal;
using System;
using System.Linq;

namespace ITM.Function.ImportForm4Reports.Helpers
{
    public class Form4DalWrapper : IForm4DalWrapper
    {
        private ITM.Services.Dal.IForm4ReportDal _form4ReportDal = null;
        private ITM.Services.Dal.IDerivativeTransactionDal _derivTransDal = null;
        private ITM.Services.Dal.INonDerivativeTransactionDal _nonDerivTransDal = null;
        private ITM.Services.Dal.IEntityDal _entityDal = null;
        private ITM.Services.Dal.IEntityTypeDal _entityTypeDal = null;
        private ITM.Services.Dal.IOwnershipTypeDal _ownershipTypeDal = null;
        private ITM.Services.Dal.ITransactionCodeDal _transCodeDal = null;
        private ITM.Services.Dal.ITransactionTypeDal _transTypeDal = null;

        public Form4DalWrapper(ITM.Services.Dal.IForm4ReportDal form4ReportDal,
                                ITM.Services.Dal.IDerivativeTransactionDal derivTransDal,
                                ITM.Services.Dal.INonDerivativeTransactionDal nonDerivTransDal,
                                ITM.Services.Dal.IEntityDal entityDal,
                                ITM.Services.Dal.IEntityTypeDal entityTypeDal,
                                ITM.Services.Dal.IOwnershipTypeDal ownershipTypeDal,
                                ITM.Services.Dal.ITransactionCodeDal transCodeDal,
                                ITM.Services.Dal.ITransactionTypeDal transTypeDal)
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

            int cik = Int32.Parse(form4Report.IssuerCIK);
            int ownerCik = Int32.Parse(form4Report.OwnerCIK);

            form4.IssuerID = (long)GetEntityIDByCIK(cik, (long)EEntityType.Company);
            form4.ReporterID = (long)GetEntityIDByCIK(ownerCik, (long)EEntityType.Person, true, form4Report);
            form4.Is10PctHolder = form4Report.IsTenPercentHolder;
            form4.IsDirector = form4Report.IsDirector;
            form4.IsOfficer = form4Report.IsOfficer;
            form4.IsOther = form4Report.IsOther;
            form4.OfficerTitle = form4Report.OfficerTitle;
            form4.OtherText = form4Report.OwnerOtherText;
            form4.Date = form4Report.PeriodOfReport;

            var result = _form4ReportDal.Insert(form4);

            foreach(var r in form4Report.NonDerivativeTransactions)
            {
                var ndt = Convert(r);
                ndt.Form4ReportID = (long)result.ID;
                var ndtResult = _nonDerivTransDal.Insert(ndt);
            }

            foreach (var r in form4Report.DerivativeTransactions)
            {
                var dt = Convert(r);
                dt.Form4ReportID = (long)result.ID;
                var dtResult = _derivTransDal.Insert(dt);
            }

            return (long)result.ID;
        }

        protected NonDerivativeTransaction Convert(ITM.Parser.Form4.NonDerivativeTransaction trans)
        {
            var result = new NonDerivativeTransaction();
            result.TransactionTypeID = (long)GetTransactionTypeIDByCode(trans.TransactionADType);
            result.TransactionCodeID = (long)GetTransactionCodeIDByCode(trans.TransactionCode);
            result.OwnershipTypeID = (long)GetOwnershipTypeIDByCode(trans.OwnershipType);
            result.AmountFollowingReport = trans.AmountFollowingReport;
            result.DeemedExecDate = trans.DeemedExecDate;
            result.EarlyVoluntarilyReport = trans.EarlyVoluntarilyReport;
            result.NatureOfIndirectOwnership = trans.NatureOfIndirectOwnership;
            result.Price = trans.Price;
            result.SharesAmount = trans.SharesAmount;
            result.TitleOfSecurity = trans.TitleOfSecurity;
            result.TransactionDate = trans.TransactionDate;

            return result;
        }

        protected DerivativeTransaction Convert(ITM.Parser.Form4.DerivativeTransaction trans)
        {
            var result = new DerivativeTransaction();
            result.TransactionTypeID = (long)GetTransactionTypeIDByCode(trans.TransactionADType);
            result.TransactionCodeID = (long)GetTransactionCodeIDByCode(trans.TransactionCode);
            result.OwnershipTypeID = (long)GetOwnershipTypeIDByCode(trans.OwnershipType);
            result.AmountFollowingReport = trans.AmountFollowingReport;
            result.ConversionExercisePrice = trans.ConversionExcercisePrice;
            result.DateExercisable = result.DateExercisable;
            result.DerivativeSecurityPrice = result.DerivativeSecurityPrice;
            result.EarlyVoluntarilyReport = trans.EarlyVoluntarilyReport;
            result.ExpirationDate = trans.ExpirationDate;
            result.NatureOfIndirectOwnership = trans.NatureOfIndirectOwnership;
            result.SharesAmount = trans.SharesAmount;
            result.TitleOfDerivative = trans.TitleOfDerivative;
            result.TransactionDate = trans.TransactionDate;
            result.UnderlyingSharesAmount = trans.UnderlyingSharesAmount;
            result.UnderlyingTitle = trans.UnderlyingTitle;

            return result;
        } 

        protected long UpsertReporter(ITM.Parser.Form4.Form4Report report)
        {
            long? id = GetEntityIDByCIK(Int32.Parse(report.OwnerCIK), (long)EEntityType.Person);

            if(id == null)
            {
                var dtoEntity = new ITM.Interfaces.Entities.Entity()
                {
                    CIK = Int32.Parse(report.OwnerCIK),
                    Name = report.OwnerName,
                    EntityTypeID = (long)EEntityType.Person                    
                };

                var dtoNewReporter = _entityDal.Insert(dtoEntity);
                id = dtoNewReporter.ID;
            }

            return (long)id;
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

        protected long? GetEntityIDByCIK(int cik, long entityTypeId, bool insertIfNotExist = false, ITM.Parser.Form4.Form4Report form4Report = null)
        {
            var entity = _entityDal.GetByEntityTypeID(entityTypeId).FirstOrDefault(e => e.CIK == cik);
            if(entity == null)
            {
                entity = InsertNewEntityIDByCIK(form4Report, entityTypeId);
            }
            return entity != null ? entity.ID : null;
        }

        protected ITM.Interfaces.Entities.Entity InsertNewEntityIDByCIK(ITM.Parser.Form4.Form4Report form4Report, long entityTypeId)
        {
            var entity = new ITM.Interfaces.Entities.Entity()
            {
                CIK = entityTypeId == (long)EEntityType.Person ? Int32.Parse(form4Report.OwnerCIK) : Int32.Parse(form4Report.IssuerCIK),
                EntityTypeID = entityTypeId,
                IsMonitored = false,
                Name = entityTypeId == (long)EEntityType.Person ? form4Report.OwnerName : form4Report.IssuerName,
                TradingSymbol = entityTypeId == (long)EEntityType.Company ? form4Report.IssuerSymbol : null
            };
            entity = _entityDal.Insert(entity);
            return entity;
        }
    }
}
