using System;
using System.Collections.Generic;
using System.Text;
using APS_HSL_DIO_WRAP;
using APS_PCI_7856_WRAP;


namespace AdlinkMockController
{
    /// <summary>
    /// Class of IO for Adlink
    /// </summary>
    class CIOAdlink : CIOBase
    {




        #region CONSTANTS
        public const short I_ADLINK_CARD_ID = 0;
        public const short I_ADLINK_CONN_INDEX_IO = 0;                          //HSL IO Index
        public const short I_ADLINK_CONN_INDEX_MTN = 1;                     //HSL Motion Index

        public const short I_CARD_CONN_INDEX_INVALID = -1;

        public const short I_MAX_MOD_NO = 8;                                                //Max Slave Module
        public const short I_MAX_IO_READ32BIT_BLK = 4;
        public const short I_IO_MOD_INPUT1_2 = 1;                                   //Input-32Bit IO points
        public const short I_IO_MOD_INPUT3_4 = 3;                                   //Input-32Bit IO points
        public const short I_IO_MOD_OUTPUT5_6 = 5;                              //Output Module-32Bit IO points
        public const short I_IO_MOD_OUTPUT7_8 = 7;                              //Output Module-32Bit IO points
        public const short I_IO_MOD_INPUT1 = 1;
        public const short I_IO_MOD_INPUT2 = 2;
        public const short I_IO_MOD_INPUT3 = 3;
        public const short I_IO_MOD_INPUT4 = 4;
        public const short I_IO_MOD_OUTPUT5 = 5;
        public const short I_IO_MOD_OUTPUT6 = 6;
        public const short I_IO_MOD_OUTPUT7 = 7;
        public const short I_IO_MOD_OUTPUT8 = 8;

        public const short I_BIT_NO_PER_IO_MOD = 32;                            //32 Bits per IO Modules
        public const short I_BIT_NO_PER_WORD = 16;
        public const ushort I_IO_OFF = 0;
        public const ushort I_IO_ON = 1;
        #endregion

        //STRUCT
        //public struct stIORdStatDef {			//IO Read Status Definition: Directly from uint32 integer
        //   public short shModNo;
        //   public short shChnlNo;
        //   public bool bStatus;								//for Both Input and Output Status respective type
        //   }

        //public static stIORdStatDef[] stRdIOStatBlk = new stIORdStatDef[I_MAX_IO];

        //PRIVATE

        public static short shCardID = 0;
        public static short shIOConnectIndex = I_CARD_CONN_INDEX_INVALID;
        public static short shMotionConnectIndex = I_CARD_CONN_INDEX_INVALID;

        uint[] uiAReadIOStatCur = new uint[I_MAX_IO_READ32BIT_BLK]; //Read IO 32bit block array
        uint[] uiAReadIOStatPrv = new uint[I_MAX_IO_READ32BIT_BLK]; //Read IO 32bit block array
        bool[,] b2ARdStatModChnl = new bool[I_MAX_MOD_NO, I_BIT_NO_PER_WORD];

        //CONSTRUCTOR
        public CIOAdlink() { }

        public CIOAdlink(short shCardIDNew, short shIOConnectIndexNew)
        {
            shCardID = shCardIDNew;
            shIOConnectIndex = shIOConnectIndexNew;
        }

        public CIOAdlink(short shCardIDNew, short shIOConnectIndexNew, short shMotionConnectIndexNew)
        {
            shCardID = shCardIDNew;
            shIOConnectIndex = shIOConnectIndexNew;
            shMotionConnectIndex = shMotionConnectIndexNew;
        }

        public CIOAdlink(string[] sAHwParm)
        { //Hardware Parameters
        }

        //OVERIDING CIO's Functions
        public override short ReadIOPointStatus()
        {
            short shRetStat = I_STATUS_OK;

            shRetStat = hslReadInput(I_IO_MOD_INPUT1_2, ref uiAReadIOStatCur[0]);
            if (shRetStat == I_STATUS_OK)
            {
                if (uiAReadIOStatCur[0] != uiAReadIOStatPrv[0])
                {
                    BuildIOBitStatus(I_IO_MOD_INPUT1_2, uiAReadIOStatCur[0]);
                    uiAReadIOStatPrv[0] = uiAReadIOStatCur[0];
                }
            }

            shRetStat = hslReadInput(I_IO_MOD_INPUT3_4, ref uiAReadIOStatCur[1]);
            if (shRetStat == I_STATUS_OK)
            {
                if (uiAReadIOStatCur[1] != uiAReadIOStatPrv[1])
                {
                    BuildIOBitStatus(I_IO_MOD_INPUT3_4, uiAReadIOStatCur[1]);
                    uiAReadIOStatPrv[1] = uiAReadIOStatCur[1];
                }
            }

            shRetStat = hslReadOutput(I_IO_MOD_OUTPUT5_6, ref uiAReadIOStatCur[2]);
            if (shRetStat == I_STATUS_OK)
            {
                if (uiAReadIOStatCur[2] != uiAReadIOStatPrv[2])
                {
                    BuildIOBitStatus(I_IO_MOD_OUTPUT5_6, uiAReadIOStatCur[2]);
                    uiAReadIOStatPrv[2] = uiAReadIOStatCur[2];
                }
            }
            shRetStat = hslReadOutput(I_IO_MOD_OUTPUT7_8, ref uiAReadIOStatCur[3]);
            if (shRetStat == I_STATUS_OK)
            {
                if (uiAReadIOStatCur[3] != uiAReadIOStatPrv[3])
                {
                    BuildIOBitStatus(I_IO_MOD_OUTPUT7_8, uiAReadIOStatCur[3]);
                    uiAReadIOStatPrv[3] = uiAReadIOStatCur[3];
                }
            }

            SetIOPointBitStatus();
            return shRetStat;
        }


        public override short ReadChannelInputStatus(short shModuleNo, short shInputChnl, ref bool bBitResult)
        {
            short shRetStat = I_STATUS_OK;
            //ushort ushRdChnlRes = 0;
            //shRetStat = hslReadChannelInput(shModuleNo, shInputChnl, ref ushRdChnlRes);
            //if (shRetStat == I_STATUS_OK)
            //{
            //    if (ushRdChnlRes == I_BIT_1)
            //        bBitResult = true;
            //    else
            //        bBitResult = false;
            //}
            return shRetStat;
        }

        public override short InitializeHardwareCard(bool bWithMotion)
        {
            short shRetStat = I_STATUS_OK;

            shRetStat = hslInitializeHSL(shCardID);
            if (shRetStat != I_STATUS_OK)
                return -1;

            shRetStat = StartHSLCardIOConnectIndex(bWithMotion);
            if (shRetStat != I_STATUS_OK)
                return shRetStat;

            return shRetStat;
        }

        public override bool IsIOCardOnline(short shAxisNo)
        {
            int iLiveData = 0;
            APS_PCI_7856_Wrapper.Get_Slave_Online_Status(0, 0, (int)shAxisNo, ref iLiveData);

            if (iLiveData == 0)
                return false;
            else
                return true;
        }

        public short DeactivateHardwareCard()
        {
            short shRetStat = I_STATUS_OK;
            short shRetStat2 = I_STATUS_OK;

            shRetStat = (short)APS_PCI_7856_Wrapper.Stop_Field_Bus(shCardID, shMotionConnectIndex);
            shRetStat2 = (short)APS_PCI_7856_Wrapper.Stop_Field_Bus(shCardID, shIOConnectIndex);
            if (shRetStat != I_STATUS_OK)
                return shRetStat;
            if (shRetStat2 != I_STATUS_OK)
                return shRetStat2;

            return shRetStat;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="shIO_ID"></param>
        /// <param name="bOn"></param>
        /// <returns></returns>
        public override short WriteIOPointOutput(short shIO_ID, bool bOn)
        {
            short i, shRetStat = I_STATUS_OK;
            for (i = 0; i < I_MAX_IO; i++)
            {
                if (Main.ioList[i].shIO_ID == shIO_ID)
                {
                    if (!Main.ioList[i].bNormallyOpen)
                    {
                        bOn = !bOn;
                    }
                    if (Main.ioList[i].enIOTyp == EnIOType.Input)
                    {
                        shRetStat = CAps168.APS_mock_set_di_bit_modno(Main.ioList[i].shModNo, Main.ioList[i].shChnlNo, bOn);
                    }
                    else
                    {
                        shRetStat = hslWriteOutput(Main.ioList[i].shModNo, Main.ioList[i].shChnlNo, bOn);
                    }
                    break;
                }
            }
            return shRetStat;
        }


        public override short SetMockStateJsonFilePath(string filepath)
        {
            return CAps168.APS_mock_set_state_path(filepath);
        }

        public override string GetMockStateJsonFilePath()
        {
            var sb = new StringBuilder(260);
            int rc = CAps168.APS_mock_get_state_path(sb, sb.Capacity);
            if (rc == 0)
                return sb.ToString();
            else
                return string.Empty;
        }

        //public void SetIOBitStatus(short shModuleNo,uint uiIOReadStatus) {//32bits IO Status
        //   short i, iIndex=0;

        //   if(shModuleNo == I_IO_MOD_INPUT1)						iIndex = 0;																//0 Start-Index
        //   else if(shModuleNo ==I_IO_MOD_INPUT2)			iIndex = I_BIT_NO_PER_IO_MOD;			//32	
        //   else if(shModuleNo== I_IO_MOD_OUTPUT3)		iIndex = I_BIT_NO_PER_IO_MOD*2;		//32x2				
        //   else if(shModuleNo== I_IO_MOD_OUTPUT4)		iIndex = I_BIT_NO_PER_IO_MOD * 3; //32x3

        //   for (i = 0; i < I_BIT_NO_PER_IO_MOD; i++)			{
        //       if ((uiIOReadStatus >> i) % 2 == 0)	 stIOPoint[iIndex + i].bStatus = false;
        //       else stIOPoint[iIndex + i].bStatus = true;
        //   }
        //}

        //public void SetIOBitStatus(short shModuleNo,uint uiIOReadStatus) {//32bits IO Status
        //   short i, iIndex = 0;

        //if(shModuleNo == I_IO_MOD_INPUT1_2)	iIndex = 0;																//0 Start-Index		
        //else if(shModuleNo ==I_IO_MOD_INPUT3_4)			iIndex = I_BIT_NO_PER_IO_MOD;			//32	
        //else if(shModuleNo== I_IO_MOD_OUTPUT5_6)		iIndex = I_BIT_NO_PER_IO_MOD*2;		//32x2				
        //else if(shModuleNo== I_IO_MOD_OUTPUT7_8)		iIndex = I_BIT_NO_PER_IO_MOD * 3; //32x3

        //for (i = 0; i < I_MAX_IO; i++)			{//Iterate from 0-127
        //if(stRdIOStatBlk[i].shModNo==st
        //if(i<I_BIT_NO_PER_WORD)		stIORdStatBlk[iIndex + i].shModNo = shModuleNo;
        //else stIORdStatBlk[iIndex + i].shModNo = (short)(shModuleNo +1);	

        //stIORdStatBlk[iIndex + i].shChnlNo = (short)(i % I_BIT_NO_PER_WORD);
        //if ((uiIOReadStatus >> i) % 2 == 0) stIORdStatBlk[iIndex + i].bStatus = false;
        //else stIOPoint[iIndex + i].bStatus = true;
        //   }
        //}

        //public void SetReadIOStatusByWordBlock(short shModuleNo, uint uiIOReadStatus)		{//32bits IO Status
        //   short i, iIndex = 0;

        //   if (shModuleNo == I_IO_MOD_INPUT1_2) iIndex = 0;																//0 Start-Index		
        //   else if (shModuleNo == I_IO_MOD_INPUT3_4) iIndex = I_BIT_NO_PER_IO_MOD;			//32	
        //   else if (shModuleNo == I_IO_MOD_OUTPUT5_6) iIndex = I_BIT_NO_PER_IO_MOD * 2;		//32x2				
        //   else if (shModuleNo == I_IO_MOD_OUTPUT7_8) iIndex = I_BIT_NO_PER_IO_MOD * 3; //32x3

        //   for (i = 0; i < I_BIT_NO_PER_IO_MOD; i++)			{//Iterate from 0-31	
        //      if (i < I_BIT_NO_PER_WORD) stRdIOStatBlk[iIndex + i].shModNo = shModuleNo;
        //      else stRdIOStatBlk[iIndex + i].shModNo = (short)(shModuleNo + 1);

        //      stRdIOStatBlk[iIndex + i].shChnlNo = (short)(i % I_BIT_NO_PER_WORD);
        //      if ((uiIOReadStatus >> i) % 2 == 0) stRdIOStatBlk[iIndex + i].bStatus = false;
        //      else stIOPoint[iIndex + i].bStatus = true;
        //   }
        //}

        //public short SetIOConfiguration(DataTable dtIOCnfg /*, ref string sErrInfo*/) {
        //   short i=0,shRetStat = I_STATUS_OK;
        //   try {
        //      foreach (DataRow drIO in dtIOCnfg.Rows) {
        //         stIOPoint[i].shIO_ID = Convert.ToInt16(drIO[S_COL_IO_ID].ToString());
        //         stIOPoint[i].shModNo = Convert.ToInt16(drIO[S_COL_MOD_NO].ToString());
        //         stIOPoint[i].shChnlNo = Convert.ToInt16(drIO[S_COL_CHNL_NO].ToString());
        //         stIOPoint[i].sIOName = drIO[S_COL_IO_NAME].ToString();
        //         stIOPoint[i].bNormallyOpen = Convert.ToBoolean(drIO[S_COL_NRML_OPN]);

        //         if (drIO[S_COL_IO_TYP].ToString() == S_IO_TYP_INPUT) stIOPoint[i].enIOTyp = enIOType.Input;
        //         else stIOPoint[i].enIOTyp = enIOType.Output;
        //         i++;
        //         }
        //      }
        //   catch(Exception) {				//sErrInfo = ex.Message;
        //      shRetStat = CResrc.ERR_IO_SET_CFG;
        //      }
        //   return shRetStat;
        //}

        //public override short GetIOConfiguration() {
        //   short shRetStat = I_STATUS_OK;

        //   InitializeIOPointStructure();

        //   CDbAccess CDbAcc = new CDbAccess(CResrc.S_IO_FILE_PATH);
        //   CDbAcc.GetTableRecord(S_IO_TBL_NM, S_IO_SQL_SEL);

        //   shRetStat = SetIOConfiguration(CDbAcc.dtTblCur /*, ref sErrInfo*/);
        //   return shRetStat;
        //}	

        void SetIOPointBitStatus()
        {// IO Status
            short i = 0, shModNo = 0, shChnlNo;     //i is up to IO_MAX=128

            for (shModNo = 1; shModNo <= I_MAX_MOD_NO; shModNo++)
            {
                for (shChnlNo = 0; shChnlNo < I_BIT_NO_PER_WORD; shChnlNo++)
                {
                    var targetIO = Main.ioList.FindAll(x => (x.shModNo == (shModNo % 2 == 1 ? shModNo : shModNo - 1) && x.shChnlNo == (shChnlNo + (shModNo % 2 == 0 ? I_BIT_NO_PER_WORD : 0))));
                    foreach (var io in targetIO)
                    {
                        io.bStatus = b2ARdStatModChnl[shModNo - 1, shChnlNo];
                    }
                    //Main.ioList[i++].bStatus = b2ARdStatModChnl[shModNo, shChnlNo]; //i: Channel
                }
                shChnlNo = 0;//Reset
            }
        }

        void BuildIOBitStatus(short shModuleNo, uint uiIOReadStatus)
        {//32bits IO Status
            short i, shModNoCur, shChnlNoCur;

            for (i = 0; i < I_BIT_NO_PER_IO_MOD; i++)
            {   //Iterate from 0-31			
                if (i >= I_BIT_NO_PER_WORD)
                {
                    shModNoCur = (short)(shModuleNo);
                    shChnlNoCur = (short)(i % I_BIT_NO_PER_WORD);
                }
                else
                {
                    shModNoCur = (short)(shModuleNo - 1);
                    shChnlNoCur = i;
                }
                if ((uiIOReadStatus >> i) % 2 == 0) b2ARdStatModChnl[shModNoCur, shChnlNoCur] = false;
                else b2ARdStatModChnl[shModNoCur, shChnlNoCur] = true;
            }
        }

        short StartHSLCardIOConnectIndex(bool bWithMotion)
        {
            short shRetStat = I_STATUS_OK;

            shRetStat = hslStartHSL(shCardID, shIOConnectIndex); //Start IO Card
            if (shRetStat != I_STATUS_OK)
                return -1;

            //With Motion Card Connect Index
            if (bWithMotion)
            {
                if (shMotionConnectIndex != I_CARD_CONN_INDEX_INVALID)
                {
                    shRetStat = hslStartHSL(shCardID, shMotionConnectIndex, 0); //Start Motion Card

                    if (shRetStat != I_STATUS_OK)
                        return -1;

                    //shRetStat = hslStartHSL(shCardID, shMotionConnectIndex); //Start Motion Card

                    //if (shRetStat != I_STATUS_OK)
                    //    return CErrorHandle.ErrHardware.ERR_HSL_START_MTN;

                    //shRetStat = hslStartHSL(shCardID, shMotionConnectIndex);//Start Motion Card
                    //if (shRetStat != I_STATUS_OK) return CErrorHandle.ErrHardware.ERR_HSL_START_MTN;
                }
            }
            return shRetStat;
        }

        uint powerN(short iPower)
        {
            int i;
            uint l = 1;

            for (i = 0; i < iPower; i++)
                l *= 2;

            return l;
        }

        //HSL LOW LEVEL API
        short hslWriteOutput(short shModNo, short shChnlNo, bool bOnOff)
        {
            short shRetStat = I_STATUS_OK;

            int iOutput = 0;
            shRetStat = (short)APS_HSL_DIO_Wrapper.Get_D_Output(shCardID, shIOConnectIndex, shModNo, ref iOutput);
            if (shRetStat != I_STATUS_OK)
                return shRetStat;

            if (bOnOff)
                iOutput |= 1 << shChnlNo;
            else
                iOutput &= ~(1 << shChnlNo);

            shRetStat = (short)APS_HSL_DIO_Wrapper.Set_D_Output(shCardID, shIOConnectIndex, shModNo, iOutput);
            return shRetStat;
        }

        short hslReadInput(short shModuleNo, ref uint uiReadResult)
        {
            short shRetStat = I_STATUS_OK;
            int iReadResult = 0;
            shRetStat = (short)APS_HSL_DIO_Wrapper.Get_D_Input(shCardID, shIOConnectIndex, shModuleNo, ref iReadResult);
            uiReadResult = (uint)iReadResult;
            return shRetStat;
        }
        short hslReadOutput(short shModuleNo, ref uint uiReadResult)
        {
            short shRetStat = I_STATUS_OK;
            int iReadResult = 0;
            shRetStat = (short)APS_HSL_DIO_Wrapper.Get_D_Output(shCardID, shIOConnectIndex, shModuleNo, ref iReadResult);
            uiReadResult = (uint)iReadResult;
            return shRetStat;
        }

        short hslInitializeHSL(short shCardID)
        {            //Initializing HSL Card
            short shResult = I_STATUS_OK;
            if (!m_bHwCardActive)
            {//if HSL card is not activated
                int iCardID = (int)shCardID;
                shResult = (short)APS_PCI_7856_Wrapper.Initialise(ref iCardID, 0);	    //return 0 unless there is an error
                if (shResult == I_STATUS_OK)
                    m_bHwCardActive = true;		//to prevent initializing again
                else m_bHwCardActive = false;
                //this.shCardID = (short )iCardID ;
            }
            else
            {
                shResult = I_STATUS_OK;
            }
            return shResult;
        }

        short hslStartHSL(short shCard, short shIndex)
        {
            short shRet = (short)APS_PCI_7856_Wrapper.Start_Field_Bus(shCard, shIndex, I_MAX_MOD_NO);
            return shRet;
        }
        short hslStartHSL(short shCard, short shIndex, int iAxis)
        {
            short shRet = (short)APS_PCI_7856_Wrapper.Start_Field_Bus(shCard, shIndex, iAxis);
            return shRet;
        }

    }
}
