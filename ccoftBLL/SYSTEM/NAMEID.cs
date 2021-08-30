/////////////////////////////////////////////////
//// Developer              : Cihan COPUR    ////
//// Creation Date          : 28.08.2021     ////
//// Last Update Date       : 28.08.2021     ////
//// All Rights Reserved ©                   ////
/////////////////////////////////////////////////
using System.Data;

namespace ccoftBLL.SYSTEM
{
    public class NAMEID
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public NAMEID()
        {
            ID = 0;
            NAME = "";
        }
        public NAMEID(int p_iId, string p_sName)
        {
            ID = p_iId;
            NAME = p_sName;
        }
        public NAMEID(DataRow p_row)
        {
            ID = p_row.getInt("ID");
            NAME = p_row.getString("NAME");
        }
    }
}
