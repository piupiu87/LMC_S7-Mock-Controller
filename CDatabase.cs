using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdlinkMockController
{
    public static class CDatabase
    {
        private const string S_DATABASE_PATH = @"C:\Nutek\MachConfig\MachineConfig.mdb";

        #region Column Name for Digital IO
        private const string S_COL_IO_ID = "IO_ID";
        private const string S_COL_MOD_NO = "ModuleNo";
        private const string S_COL_CHNL_NO = "ChannelNo";
        private const string S_COL_IO_TYP = "IO_Type";
        private const string S_COL_IO_NAME = "IO_Name";
        private const string S_COL_IO_DESCRIPTION = "IO_Description";
        private const string S_COL_NRML_OPN = "NormallyOpen";
        private const string S_IO_TYP_INPUT = "Input";
        #endregion

        private static string CONNECTION_STRING = (System.Environment.Is64BitProcess ? 
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                : @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") + S_DATABASE_PATH + ";";

        public static List<StIOPointDef> LoadDigitalIO()
        {
            List<StIOPointDef> ioList = new List<StIOPointDef>();

            using (OleDbConnection conn = new OleDbConnection(CONNECTION_STRING))
            {
                conn.Open();

                string query = "SELECT * FROM DigitalIO";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StIOPointDef io = new StIOPointDef
                        {
                            shIO_ID = Convert.ToInt16(reader[S_COL_IO_ID]),
                            shModNo = Convert.ToInt16(reader[S_COL_MOD_NO]),
                            shChnlNo = Convert.ToInt16(reader[S_COL_CHNL_NO]),
                            enIOTyp = reader[S_COL_IO_TYP]?.ToString() == S_IO_TYP_INPUT ? EnIOType.Input : EnIOType.Output,
                            sIOName = reader[S_COL_IO_NAME]?.ToString(),
                            sIODescription = reader[S_COL_IO_DESCRIPTION]?.ToString(),
                            bNormallyOpen = reader[S_COL_NRML_OPN] != DBNull.Value && Convert.ToBoolean(reader[S_COL_NRML_OPN])
                        };

                        ioList.Add(io);
                    }
                }
            }

            return ioList;
        }
    }
}
