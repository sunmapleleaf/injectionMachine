using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data;

namespace WindowsFormsApplication1
{
    #region  performa
    class InjectionMachineWithGefranPerfoma : InjectionMachine
    {
        public QualityDataFromGefranPerfoma qualityData;
        public InjectionMachineWithGefranPerfoma()
        {
            clamp = new ClampDataFromGefranPerfoma();
            injection = new InjectionDataFromGefranPerfoma();
            ejector = new EjectorDataFromGefranPerfoma();
            core = new CoreDataFromGefranPerfoma();
            plasticize = new ChargeDataFromGefranPerfoma();
            carriage = new CarriageDataFromGefranPerfoma();
            overView = new OverviewDataFromGefranPerfoma();
            qualityData = new QualityDataFromGefranPerfoma();
        }
    }
    class ClampDataFromGefranPerfoma
    {
        public string[] OP_MOULD_DATA_M__END_POS = new string[2];
        public string[,] CL_MOULD_DATA_M__POS_DS = new string[5, 2];
        public string[,] CL_MOULD_DATA_M__SPD_DS = new string[6, 2];
        public string[,] CL_MOULD_DATA_M__PRE_DS = new string[6, 2];
        public string[] T_MOLD_SAFE = new string[2];
        public string[,] OP_MOULD_DATA_M__POS_DS = new string[5, 2];
        public string[,] OP_MOULD_DATA_M__SPD_DS = new string[6, 2];
        public string[,] OP_MOULD_DATA_M__PRE_DS = new string[5, 2];
    }
    class InjectionDataFromGefranPerfoma
    {
        public string[] INJEC_DELAY = new string[2];
        public string[,] INJEC_DATA_M__POS_DS = new string[8, 2];
        public string[,] INJEC_DATA_M__SPD_DS = new string[8, 2];
        public string[,] INJEC_DATA_M__PRE_DS = new string[8, 2];
        public string[] TA_RIEMP = new string[2];

        public string[] HOLD_SPD = new string[2];
        public string[,] LP_PHT_DS = new string[8, 2];
        public string[,] INJEC_LP_DATA__PRE_DS = new string[8, 2];


    }
    class EjectorDataFromGefranPerfoma
    {

        public string[,] EJECT_FW_DATA_M__POS_DS = new string[3, 2];
        public string[,] EJECT_FW_DATA_M__SPD_DS = new string[3, 2];
        public string[,] EJECT_FW_DATA_M__PRE_DS = new string[3, 2];
        public string[] EJ_FIRSTOUTDELAY = new string[2];

        public string[,] EJECT_BW_DATA_M__POS_DS = new string[3, 2];
        public string[,] EJECT_BW_DATA_M__SPD_DS = new string[3, 2];
        public string[,] EJECT_BW_DATA_M__PRE_DS = new string[3, 2];
        public string[] EJ_FINALBKDELAY = new string[2];

        public string[,] BLOWS_DATA__MODE = new string[4, 2];
        public string[,] BLOWS_DATA__POS_ON_DS = new string[4, 2];
        public string[,] BLOWS_DATA__DLY_ON = new string[4, 2];
        public string[,] BLOWS_DATA__DURATION = new string[4, 2];



    }
    class CoreDataFromGefranPerfoma
    {

        public string[,] ACT_SPEED_A_DS__N = new string[4, 2];
        public string[,] ACT_PRESSURE_A_DS__N = new string[4, 2];
        public string[,] ACT_SPEED_B_DS__N = new string[4, 2];
        public string[,] ACT_PRESSURE_B_DS__N = new string[4, 2];
        public string[,] ACT_ENABLE__N = new string[4, 2];
        public string[,] ACT_START_POSITIONS_MODE_A__N = new string[4, 2];
        public string[,] ACT_START_POSITIONS_MODE_B__N = new string[4, 2];
        public string[,] ACT_SET_POSITION_PHASE3_A__N = new string[4, 2];
        public string[,] ACT_SET_POSITION_PHASE3_B__N = new string[4, 2];
        public string[,] ACT_SET_SAFETY_POSITION_PHASE3_A__N = new string[4, 2];
        public string[,] ACT_SET_SAFETY_POSITION_PHASE3_B__N = new string[4, 2];
        public string[,] ACT_SET_DURATION_TIME_A__N = new string[4, 2];
        public string[,] ACT_SET_DURATION_TIME_B__N = new string[4, 2];
        public string[,] ACT_PRIORITY_A__N = new string[4, 2];
        public string[,] ACT_PRIORITY_B__N = new string[4, 2];
        public string[,] ACT_VALVE_STAY_ON_A__N = new string[4, 2];

    }
    class ChargeDataFromGefranPerfoma
    {
        public string[,] SCREW_DATA_M__POS_DS = new string[8, 2];
        public string[,] SCREW_DATA_M__SPD_DS = new string[8, 2];
        public string[,] SCREW_DATA_M__PRE_DS = new string[8, 2];
    }
    class CarriageDataFromGefranPerfoma
    {
        public string[,] CFW_DATA_M__POS_DS = new string[2, 2];
        public string[,] CFW_DATA_M__SPD_DS = new string[2, 2];
        public string[,] CFW_DATA_M__PRE_DS = new string[2, 2];
        public string[,] CBK_DATA_M__POS_DS = new string[2, 2];
        public string[,] CBK_DATA_M__SPD_DS = new string[2, 2];
        public string[,] CBK_DATA_M__PRE_DS = new string[2, 2];
    }
    class OverviewDataFromGefranPerfoma
    {
        public string[] CURRENT_TOTAL_PRODUCTION = new string[2];
        public string[] TOTAL_PRODUCTION = new string[2];
        public string[] CYCLE_DURATION = new string[2];

    }
    class QualityDataFromGefranPerfoma
    {
        public string[] CUSH_POS_DISP = new string[2];
        public string[] INJ_FILL_T = new string[2];
        public string[] CHG_TIME = new string[2];
        public string[] FINAL_RECOVERY_POS = new string[2];
        public string[] MAX_FILL_PRS = new string[2];
    }

    #endregion
}
