﻿using ITM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Source.SEC
{
    public class SECSourceItemInfo : ISourceItemInfo
    {
        public string Name
        {
            get;
            set;
        }

        public DateTime LastModified 
        { 
            get; 
            set; 
        }
    }

    public class SECSourceSubmissionsInfoParams : ISourceSubmissionsInfoParams
    {
        public SECSourceSubmissionsInfoParams()
        {
            Items = new List<ISourceItemInfo>();
        }
        public string CIK
        {
            get;
            set;
        }

        public List<ISourceItemInfo> Items
        {
            get;
            set;
        }    
    }

    public class SECSourceSubmissionInfo : ISourceSubmissionInfo
    {
        public SECSourceSubmissionInfo()
        {
            Report = new List<string>();
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime PeriodEnd
        {
            get;
            set;
        }

        public IList<string> Report
        {
            get;
            set;
        }

        public DateTime Submitted
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }
    }

    public class SECSourceSubmissionsInfoResult : ResultBase, ISourceSubmissionsInfoResult
    {
        public SECSourceSubmissionsInfoResult()
        {
            Submissions = new List<ISourceSubmissionInfo>();
        }
        public List<ISourceSubmissionInfo> Submissions
        {
            get;
            set;
        }
    }

    public class SECSourceItem : ISourceItem
    {
        public string CIK
        {
            get;
            set;
        }

        public List<byte> Content
        {
            get;
            set;
        }

        public string FilingName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public class SECSourceValidateParams : ISourceValidateParams
    {
        public string CIK
        {
            get;
            set;
        }

        public DateTime UpdateFromDate
        {
            get;
            set;
        }

        public DateTime UpdateToDate
        {
            get;
            set;
        }
    }

    public class SECSourceFilingsListResult : ResultBase, ISourceFilingsListResult
    {
        public SECSourceFilingsListResult()
        {
            Filings = new List<ISourceItemInfo>();
        }

        public List<ISourceItemInfo> Filings
        {
            get;
            set;
        } 
    }

    public class SECSourceExtractParams : ISourceExtractParams
    {
        public SECSourceExtractParams()
        {
            Items = new List<ISourceItemInfo>();
        }

        public string CIK
        {
            get;
            set;
        }

        public List<ISourceItemInfo> Items
        {
            get;
            set;
        }
    }

    public class SECSourceExtractResult : ResultBase, ISourceExtractResult
    {
        public SECSourceExtractResult()
        {
            Items = new List<ISourceItem>();
        }

        public List<ISourceItem> Items
        {
            get;
            set;
        }
    }

    public class SECSourceExtractFilingItemsParams : ISourceExtractFilingItemsParams
    {
        public SECSourceExtractFilingItemsParams()
        {
            Items = new List<ISourceItemInfo>();
        }
        public string CIK
        {
            get;
            set;
        }

        public ISourceItemInfo Filing
        {
            get;
            set;
        }

        public List<ISourceItemInfo> Items
        {
            get;
            set;
        }
    }

    public class SECSourceInitParams : ISourceInitParams
    {
        public SECSourceInitParams()
        {
            ExtractFromStorage = false;
        }

        public ILogger Logger
        {
            get;
            set;
        }

        public bool ExtractFromStorage
        {
            get;
            set;
        }
    }
}
