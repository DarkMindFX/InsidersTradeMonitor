


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITM.Interfaces.Entities
{
    public class Form4Report
    {
        public System.Int64? ID { get; set; }

        public System.Int64 IssuerID { get; set; }

        public System.Int64 ReporterID { get; set; }

        public System.Boolean IsOfficer { get; set; }

        public System.Boolean IsDirector { get; set; }

        public System.Boolean Is10PctHolder { get; set; }

        public System.Boolean IsOther { get; set; }

        public System.String OtherText { get; set; }

        public System.String OfficerTitle { get; set; }

        public System.DateTime Date { get; set; }


    }
}
