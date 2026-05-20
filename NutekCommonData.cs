using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlinkMockController
{
    public class StIOPointDef
    {   //IO Point Definition
        public short shIO_ID { get; set; }
        public short shModNo { get; set; }
        public short shChnlNo { get; set; }
        public EnIOType enIOTyp { get; set; }
        public string sIOName { get; set; }
        public string sIODescription { get; set; }
        public bool bNormallyOpen { get; set; }
        public bool bStatus { get; set; } //for Both Input and Output Status respective type
    }

    public enum EnIOType
    {
        Input,
        Output
    }
}
