using System;
using System.Collections.Generic;
using System.Text;
using AdlinkMockController.Properties;

namespace AdlinkMockController
{
    class CIOBase
    {//IO Base Class

        #region CONSTANT
        public const short I_STATUS_NOK = 0;
        public const short I_STATUS_OK = 0;
        public const short I_MAX_IO = 128;
        public const short I_BIT_1 = 1;
        public const short I_BIT_0 = 0;
        #endregion

        //PRIVATE
        private static bool bHwCardActive = false; //indicate if the Hw card is active

        //PROPERTY
        public static bool m_bHwCardActive
        {//Property
            get { return bHwCardActive; }
            set { bHwCardActive = value; }
        }

        public virtual short InitializeHardwareCard(bool bWithMotion)
        {
            return -1;
        }

        public virtual short DeactivateHardwareCard()
        {
            return -1;
        }

        public virtual short ReadIOPointStatus()
        {
            return -1;
        }

        public virtual short ReadChannelInputStatus(short shModuleNo, short shInputChnl, ref bool bBitResult)
        {
            return -1;
        }

        public virtual short WriteIOPointOutput(short shIO_ID, bool bOn)
        {
            return -1;
        }

        public virtual bool IsIOCardOnline(short shAxisNo)
        {
            return false;//CErrorHandle.ERR_MOTION_INIT;
        }

        public virtual string GetMockStateJsonFilePath()
        {
            return string.Empty;
        }

        public virtual short SetMockStateJsonFilePath(string filepath)
        {
            return -1;
        }
    }
}
