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
    
    class InjectionMachine
    {
        public string machineID;
        public string sampleTime;
        public string timestamp;
        public object clamp;
        public object injection;
        public object ejector;
        public object core;
        public object plasticize;
        public object carriage;
        public object overView;

        public InjectionMachine()
        {
            overView = new OverView();
            clamp = new Clamp();
            injection = new Injection();
            ejector = new Ejector();
            core = new Core();
            plasticize = new Plasticize();
            carriage = new Carriage();
            overView = new OverView();
        }
        public virtual void getDataFromController(ConnectionOption p)
        {
        }
        public virtual void convertedFromKeba(JObject jo, ref string strMachineData)
        {
            machineID = jo["machineID"].ToString();
            sampleTime = jo["sampleTime"].ToString();
            timestamp = jo["timestamp"].ToString();
            #region //clamp
            ((Clamp)clamp).cycleDelayAct[0] = jo["system.sv_dCycleDelayAct"][0].ToString();
            ((Clamp)clamp).cycleDelayAct[1] = jo["system.sv_dCycleDelayAct"][1].ToString();
            ((Clamp)clamp).cycleDelaySet[0] = jo["system.sv_dCycleDelaySet"][0].ToString();
            ((Clamp)clamp).cycleDelaySet[1] = jo["system.sv_dCycleDelaySet"][1].ToString();

            ((Clamp)clamp).moldCloseNoOfPoints[0] = "sv_MoldFwdProfVisSrc.Profile.iNoOfPoints";
            ((Clamp)clamp).moldCloseNoOfPoints[1] = "5";
            
            ((Clamp)clamp).moldClosePressure[0, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[15].rPressure"][0].ToString();
            ((Clamp)clamp).moldClosePressure[0, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[15].rPressure"][1].ToString();
            ((Clamp)clamp).moldClosePressure[1, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[16].rPressure"][0].ToString();
            ((Clamp)clamp).moldClosePressure[1, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[16].rPressure"][1].ToString();
            ((Clamp)clamp).moldClosePressure[2, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[17].rPressure"][0].ToString();
            ((Clamp)clamp).moldClosePressure[2, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[17].rPressure"][1].ToString();
            ((Clamp)clamp).moldClosePressure[3, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[18].rPressure"][0].ToString();
            ((Clamp)clamp).moldClosePressure[3, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[18].rPressure"][1].ToString();
            ((Clamp)clamp).moldClosePressure[4, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[19].rPressure"][0].ToString();
            ((Clamp)clamp).moldClosePressure[4, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[19].rPressure"][1].ToString();


            ((Clamp)clamp).moldCloseVelocity[0, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[15].rVelocity"][0].ToString();
            ((Clamp)clamp).moldCloseVelocity[0, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[15].rVelocity"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[1, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[16].rVelocity"][0].ToString();
            ((Clamp)clamp).moldCloseVelocity[1, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[16].rVelocity"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[2, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[17].rVelocity"][0].ToString();
            ((Clamp)clamp).moldCloseVelocity[2, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[17].rVelocity"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[3, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[18].rVelocity"][0].ToString();
            ((Clamp)clamp).moldCloseVelocity[3, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[18].rVelocity"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[4, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[19].rVelocity"][0].ToString();
            ((Clamp)clamp).moldCloseVelocity[4, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[19].rVelocity"][1].ToString();

            ((Clamp)clamp).moldClosePosition[0, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[16].rStartPos"][0].ToString();
            ((Clamp)clamp).moldClosePosition[0, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[16].rStartPos"][1].ToString();
            ((Clamp)clamp).moldClosePosition[1, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[17].rStartPos"][0].ToString();
            ((Clamp)clamp).moldClosePosition[1, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[17].rStartPos"][1].ToString();
            ((Clamp)clamp).moldClosePosition[2, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[18].rStartPos"][0].ToString();
            ((Clamp)clamp).moldClosePosition[2, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[18].rStartPos"][1].ToString();
            ((Clamp)clamp).moldClosePosition[3, 0] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[19].rStartPos"][0].ToString();
            ((Clamp)clamp).moldClosePosition[3, 1] = jo["Mold1.sv_MoldFwdProfVisSrc.Profile.Points[19].rStartPos"][1].ToString();

            ((Clamp)clamp).moldOpenNoOfPoints[0] = "sv_MoldBwdProfVis.Profile.iNoOfPoints";
            ((Clamp)clamp).moldOpenNoOfPoints[1] = "5";

            ((Clamp)clamp).moldOpenPressure[0, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[1].rPressure"][0].ToString();
            ((Clamp)clamp).moldOpenPressure[0, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[1].rPressure"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[1, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[2].rPressure"][0].ToString();
            ((Clamp)clamp).moldOpenPressure[1, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[2].rPressure"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[2, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[3].rPressure"][0].ToString();
            ((Clamp)clamp).moldOpenPressure[2, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[3].rPressure"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[3, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[4].rPressure"][0].ToString();
            ((Clamp)clamp).moldOpenPressure[3, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[4].rPressure"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[4, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[5].rPressure"][0].ToString();
            ((Clamp)clamp).moldOpenPressure[4, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[5].rPressure"][1].ToString();

            ((Clamp)clamp).moldOpenVelocity[0, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[1].rVelocity"][0].ToString();
            ((Clamp)clamp).moldOpenVelocity[0, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[1].rVelocity"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[1, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[2].rVelocity"][0].ToString();
            ((Clamp)clamp).moldOpenVelocity[1, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[2].rVelocity"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[2, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[3].rVelocity"][0].ToString();
            ((Clamp)clamp).moldOpenVelocity[2, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[3].rVelocity"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[3, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[4].rVelocity"][0].ToString();
            ((Clamp)clamp).moldOpenVelocity[3, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[4].rVelocity"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[4, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[5].rVelocity"][0].ToString();
            ((Clamp)clamp).moldOpenVelocity[4, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[5].rVelocity"][1].ToString();

            ((Clamp)clamp).moldOpenPosition[0, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[2].rStartPos"][0].ToString();
            ((Clamp)clamp).moldOpenPosition[0, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[2].rStartPos"][1].ToString();
            ((Clamp)clamp).moldOpenPosition[1, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[3].rStartPos"][0].ToString();
            ((Clamp)clamp).moldOpenPosition[1, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[3].rStartPos"][1].ToString();
            ((Clamp)clamp).moldOpenPosition[2, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[4].rStartPos"][0].ToString();
            ((Clamp)clamp).moldOpenPosition[2, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[4].rStartPos"][1].ToString();
            ((Clamp)clamp).moldOpenPosition[3, 0] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[5].rStartPos"][0].ToString();
            ((Clamp)clamp).moldOpenPosition[3, 1] = jo["Mold1.sv_MoldBwdProfVis.Profile.Points[5].rStartPos"][1].ToString();

            ((Clamp)clamp).moldFastClose[0] = jo["Mold1.sv_bFastClose"][0].ToString();
            ((Clamp)clamp).moldFastClose[1] = jo["Mold1.sv_bFastClose"][1].ToString();
            ((Clamp)clamp).moldFastOpen[0] = jo["Mold1.sv_bFastClose"][0].ToString();
            ((Clamp)clamp).moldFastOpen[1] = jo["Mold1.sv_bFastClose"][1].ToString();
            ((Clamp)clamp).useOpenVavleAsFastClose[0] = jo["Mold1.sv_bUseOpenValveAsFastClose"][0].ToString();
            ((Clamp)clamp).useOpenVavleAsFastClose[1] = jo["Mold1.sv_bUseOpenValveAsFastClose"][1].ToString();
            ((Clamp)clamp).moldProtectTimeAct[0] = jo["Mold1.sv_dMoldProtectTimeAct"][0].ToString();
            ((Clamp)clamp).moldProtectTimeAct[1] = jo["Mold1.sv_dMoldProtectTimeAct"][1].ToString();
            ((Clamp)clamp).moldProtectTimeSet[0] = jo["Mold1.sv_dMoldProtectTimeSet"][0].ToString();
            ((Clamp)clamp).moldProtectTimeSet[1] = jo["Mold1.sv_dMoldProtectTimeSet"][1].ToString();

            double computeFwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Mold1.sv_rMaxPressureFwd"][1]);
            double computeFwdVelocityTmp = 100.0 / (double)(jo["Mold1.sv_rMaxSpeedFwd"][1]);
            double computeBwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Mold1.sv_rMaxPressureBwd"][1]);
            double computeBwdVelocityTmp = 100.0 / (double)(jo["Mold1.sv_rMaxSpeedBwd"][1]);
            for (int i = 0; i < 5; i++)
            {
                ((Clamp)clamp).moldClosePressure[i, 1] = (Math.Round(double.Parse(((Clamp)clamp).moldClosePressure[i, 1]) * computeFwdPressureTmp)).ToString();
                ((Clamp)clamp).moldOpenPressure[i, 1] = Math.Round(double.Parse(((Clamp)clamp).moldOpenPressure[i, 1]) * computeBwdPressureTmp).ToString();
                ((Clamp)clamp).moldCloseVelocity[i, 1] = Math.Round(double.Parse(((Clamp)clamp).moldCloseVelocity[i, 1]) * computeFwdVelocityTmp).ToString();
                ((Clamp)clamp).moldOpenVelocity[i, 1] = Math.Round(double.Parse(((Clamp)clamp).moldOpenVelocity[i, 1]) * computeBwdVelocityTmp).ToString();

            }
            #endregion
            #region //ejector
            ((Ejector)ejector).ejectorFwdNoOfPoints[0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.iNoOfPoints"][0].ToString();
            ((Ejector)ejector).ejectorFwdNoOfPoints[1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.iNoOfPoints"][1].ToString();
            ((Ejector)ejector).ejectorFwdPressure[0,0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[1].rPressure"][0].ToString();
            ((Ejector)ejector).ejectorFwdPressure[0,1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[1].rPressure"][1].ToString();
            ((Ejector)ejector).ejectorFwdPressure[1,0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[2].rPressure"][0].ToString();
            ((Ejector)ejector).ejectorFwdPressure[1,1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[2].rPressure"][1].ToString();
            ((Ejector)ejector).ejectorFwdPressure[2,0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[3].rPressure"][0].ToString();
            ((Ejector)ejector).ejectorFwdPressure[2,1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[3].rPressure"][1].ToString();

            ((Ejector)ejector).ejectorFwdVelocity[0, 0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[1].rVelocity"][0].ToString();
            ((Ejector)ejector).ejectorFwdVelocity[0, 1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[1].rVelocity"][1].ToString();
            ((Ejector)ejector).ejectorFwdVelocity[1, 0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[2].rVelocity"][0].ToString();
            ((Ejector)ejector).ejectorFwdVelocity[1, 1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[2].rVelocity"][1].ToString();
            ((Ejector)ejector).ejectorFwdVelocity[2, 0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[3].rVelocity"][0].ToString();
            ((Ejector)ejector).ejectorFwdVelocity[2, 1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[3].rVelocity"][1].ToString();

            ((Ejector)ejector).ejectorFwdPosition[0, 0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[1].rStartPos"][0].ToString();
            ((Ejector)ejector).ejectorFwdPosition[0, 1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[1].rStartPos"][1].ToString();
            ((Ejector)ejector).ejectorFwdPosition[1, 0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[2].rStartPos"][0].ToString();
            ((Ejector)ejector).ejectorFwdPosition[1, 1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[2].rStartPos"][1].ToString();
            ((Ejector)ejector).ejectorFwdPosition[2, 0] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[3].rStartPos"][0].ToString();
            ((Ejector)ejector).ejectorFwdPosition[2, 1] = jo["Ejector1.sv_EjectorFwdVisRel.Profile.Points[3].rStartPos"][1].ToString();

            ((Ejector)ejector).ejectorBwdNoOfPoints[0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.iNoOfPoints"][0].ToString();
            ((Ejector)ejector).ejectorBwdNoOfPoints[1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.iNoOfPoints"][1].ToString();
            ((Ejector)ejector).ejectorBwdPressure[0, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[1].rPressure"][0].ToString();
            ((Ejector)ejector).ejectorBwdPressure[0, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[1].rPressure"][1].ToString();
            ((Ejector)ejector).ejectorBwdPressure[1, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[2].rPressure"][0].ToString();
            ((Ejector)ejector).ejectorBwdPressure[1, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[2].rPressure"][1].ToString();
            ((Ejector)ejector).ejectorBwdPressure[2, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[3].rPressure"][0].ToString();
            ((Ejector)ejector).ejectorBwdPressure[2, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[3].rPressure"][1].ToString();

            ((Ejector)ejector).ejectorBwdVelocity[0, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[1].rVelocity"][0].ToString();
            ((Ejector)ejector).ejectorBwdVelocity[0, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[1].rVelocity"][1].ToString();
            ((Ejector)ejector).ejectorBwdVelocity[1, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[2].rVelocity"][0].ToString();
            ((Ejector)ejector).ejectorBwdVelocity[1, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[2].rVelocity"][1].ToString();
            ((Ejector)ejector).ejectorBwdVelocity[2, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[3].rVelocity"][0].ToString();
            ((Ejector)ejector).ejectorBwdVelocity[2, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[3].rVelocity"][1].ToString();

            ((Ejector)ejector).ejectorBwdPosition[0, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[1].rStartPos"][0].ToString();
            ((Ejector)ejector).ejectorBwdPosition[0, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[1].rStartPos"][1].ToString();
            ((Ejector)ejector).ejectorBwdPosition[1, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[2].rStartPos"][0].ToString();
            ((Ejector)ejector).ejectorBwdPosition[1, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[2].rStartPos"][1].ToString();
            ((Ejector)ejector).ejectorBwdPosition[2, 0] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[3].rStartPos"][0].ToString();
            ((Ejector)ejector).ejectorBwdPosition[2, 1] = jo["Ejector1.sv_EjectorBwdVisRel.Profile.Points[3].rStartPos"][1].ToString();

            ((Ejector)ejector).ejectorMode[0] = jo["Ejector1.sv_EjectorMode"][0].ToString();
            ((Ejector)ejector).ejectorMode[1] = jo["Ejector1.sv_EjectorMode"][1].ToString();
            ((Ejector)ejector).ejectorShakeCounter[0] = jo["Ejector1.sv_iShakeCounter"][0].ToString();
            ((Ejector)ejector).ejectorShakeCounter[1] = jo["Ejector1.sv_iShakeCounter"][1].ToString();

            ((Ejector)ejector).airVavleMode[0,0] = jo["AirValve1.sv_AirValveMode"][0].ToString();
            ((Ejector)ejector).airVavleMode[0, 1] = jo["AirValve1.sv_AirValveMode"][1].ToString();
            ((Ejector)ejector).airVavleMode[1, 0] = jo["AirValve2.sv_AirValveMode"][0].ToString();
            ((Ejector)ejector).airVavleMode[1, 1] = jo["AirValve2.sv_AirValveMode"][1].ToString();
            ((Ejector)ejector).airVavleMode[2, 0] = jo["AirValve3.sv_AirValveMode"][0].ToString();
            ((Ejector)ejector).airVavleMode[2, 1] = jo["AirValve3.sv_AirValveMode"][1].ToString();
            ((Ejector)ejector).airVavleMode[3, 0] = jo["AirValve4.sv_AirValveMode"][0].ToString();
            ((Ejector)ejector).airVavleMode[3, 1] = jo["AirValve4.sv_AirValveMode"][1].ToString();

            ((Ejector)ejector).airVavleStartPosition[0, 0] = jo["AirValve1.sv_rStartPosition"][0].ToString();
            ((Ejector)ejector).airVavleStartPosition[0, 1] = jo["AirValve1.sv_rStartPosition"][1].ToString();
            ((Ejector)ejector).airVavleStartPosition[1, 0] = jo["AirValve2.sv_rStartPosition"][0].ToString();
            ((Ejector)ejector).airVavleStartPosition[1, 1] = jo["AirValve2.sv_rStartPosition"][1].ToString();
            ((Ejector)ejector).airVavleStartPosition[2, 0] = jo["AirValve3.sv_rStartPosition"][0].ToString();
            ((Ejector)ejector).airVavleStartPosition[2, 1] = jo["AirValve3.sv_rStartPosition"][1].ToString();
            ((Ejector)ejector).airVavleStartPosition[3, 0] = jo["AirValve4.sv_rStartPosition"][0].ToString();
            ((Ejector)ejector).airVavleStartPosition[3, 1] = jo["AirValve4.sv_rStartPosition"][1].ToString();

            ((Ejector)ejector).airVavleSetDelayTime[0, 0] = jo["AirValve1.sv_AirValveTimesSet.dSetDelayTime"][0].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[0, 1] = jo["AirValve1.sv_AirValveTimesSet.dSetDelayTime"][1].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[1, 0] = jo["AirValve2.sv_AirValveTimesSet.dSetDelayTime"][0].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[1, 1] = jo["AirValve2.sv_AirValveTimesSet.dSetDelayTime"][1].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[2, 0] = jo["AirValve3.sv_AirValveTimesSet.dSetDelayTime"][0].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[2, 1] = jo["AirValve3.sv_AirValveTimesSet.dSetDelayTime"][1].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[3, 0] = jo["AirValve4.sv_AirValveTimesSet.dSetDelayTime"][0].ToString();
            ((Ejector)ejector).airVavleSetDelayTime[3, 1] = jo["AirValve4.sv_AirValveTimesSet.dSetDelayTime"][1].ToString();

            ((Ejector)ejector).airVavleSetMoveTime[0, 0] = jo["AirValve1.sv_AirValveTimesSet.dSetMoveTime"][0].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[0, 1] = jo["AirValve1.sv_AirValveTimesSet.dSetMoveTime"][1].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[1, 0] = jo["AirValve2.sv_AirValveTimesSet.dSetMoveTime"][0].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[1, 1] = jo["AirValve2.sv_AirValveTimesSet.dSetMoveTime"][1].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[2, 0] = jo["AirValve3.sv_AirValveTimesSet.dSetMoveTime"][0].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[2, 1] = jo["AirValve3.sv_AirValveTimesSet.dSetMoveTime"][1].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[3, 0] = jo["AirValve4.sv_AirValveTimesSet.dSetMoveTime"][0].ToString();
            ((Ejector)ejector).airVavleSetMoveTime[3, 1] = jo["AirValve4.sv_AirValveTimesSet.dSetMoveTime"][1].ToString();

            computeFwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Ejector1.sv_rMaxPressureFwd"][1]);
            computeFwdVelocityTmp = 100.0 / (double)(jo["Ejector1.sv_rMaxSpeedFwd"][1]);
             computeBwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Ejector1.sv_rMaxPressureBwd"][1]);
             computeBwdVelocityTmp = 100.0 / (double)(jo["Ejector1.sv_rMaxSpeedBwd"][1]);
            for (int i = 0; i < 3; i++)
            {
                ((Ejector)ejector).ejectorFwdPressure[i, 1] = Math.Round(double.Parse(((Ejector)ejector).ejectorFwdPressure[i, 1]) * computeFwdPressureTmp).ToString();
                ((Ejector)ejector).ejectorBwdPressure[i, 1] = Math.Round(double.Parse(((Ejector)ejector).ejectorBwdPressure[i, 1]) * computeBwdPressureTmp).ToString();
                ((Ejector)ejector).ejectorFwdVelocity[i, 1] = Math.Round(double.Parse(((Ejector)ejector).ejectorFwdVelocity[i, 1]) * computeFwdVelocityTmp).ToString();
                ((Ejector)ejector).ejectorBwdVelocity[i, 1] = Math.Round(double.Parse(((Ejector)ejector).ejectorBwdVelocity[i, 1]) * computeBwdVelocityTmp).ToString();

            }
            #endregion

            #region //core
            ((Core)core).coreUse[0, 0] = jo["Core1.sv_CoreMode.CoreType"][0].ToString();
            ((Core)core).coreUse[0, 1] = jo["Core1.sv_CoreMode.CoreType"][1].ToString();
            ((Core)core).coreUse[1, 0] = jo["Core2.sv_CoreMode.CoreType"][0].ToString();
            ((Core)core).coreUse[1, 1] = jo["Core2.sv_CoreMode.CoreType"][1].ToString();
            ((Core)core).coreUse[2, 0] = jo["Core3.sv_CoreMode.CoreType"][0].ToString();
            ((Core)core).coreUse[2, 1] = jo["Core3.sv_CoreMode.CoreType"][1].ToString();
            ((Core)core).coreUse[3, 0] = jo["Core4.sv_CoreMode.CoreType"][0].ToString();
            ((Core)core).coreUse[3, 1] = jo["Core4.sv_CoreMode.CoreType"][1].ToString();

            ((Core)core).coreOutMode[0, 0] = jo["CentralCoordination1.sv_CoreData[1].OutMode"][0].ToString();
            ((Core)core).coreOutMode[0, 1] = jo["CentralCoordination1.sv_CoreData[1].OutMode"][1].ToString();
            ((Core)core).coreOutMode[1, 0] = jo["CentralCoordination1.sv_CoreData[2].OutMode"][0].ToString();
            ((Core)core).coreOutMode[1, 1] = jo["CentralCoordination1.sv_CoreData[2].OutMode"][1].ToString();
            ((Core)core).coreOutMode[2, 0] = jo["CentralCoordination1.sv_CoreData[3].OutMode"][0].ToString();
            ((Core)core).coreOutMode[2, 1] = jo["CentralCoordination1.sv_CoreData[3].OutMode"][1].ToString();
            ((Core)core).coreOutMode[3, 0] = jo["CentralCoordination1.sv_CoreData[4].OutMode"][0].ToString();
            ((Core)core).coreOutMode[3, 1] = jo["CentralCoordination1.sv_CoreData[4].OutMode"][1].ToString();

            ((Core)core).coreInMode[0, 0] = jo["CentralCoordination1.sv_CoreData[1].InMode"][0].ToString();
            ((Core)core).coreInMode[0, 1] = jo["CentralCoordination1.sv_CoreData[1].InMode"][1].ToString();
            ((Core)core).coreInMode[1, 0] = jo["CentralCoordination1.sv_CoreData[2].InMode"][0].ToString();
            ((Core)core).coreInMode[1, 1] = jo["CentralCoordination1.sv_CoreData[2].InMode"][1].ToString();
            ((Core)core).coreInMode[2, 0] = jo["CentralCoordination1.sv_CoreData[3].InMode"][0].ToString();
            ((Core)core).coreInMode[2, 1] = jo["CentralCoordination1.sv_CoreData[3].InMode"][1].ToString();
            ((Core)core).coreInMode[3, 0] = jo["CentralCoordination1.sv_CoreData[4].InMode"][0].ToString();
            ((Core)core).coreInMode[3, 1] = jo["CentralCoordination1.sv_CoreData[4].InMode"][1].ToString();

            ((Core)core).coreHold[0, 0] = jo["Core1.sv_CoreMode.Hold"][0].ToString();
            ((Core)core).coreHold[0, 1] = jo["Core1.sv_CoreMode.Hold"][1].ToString();
            ((Core)core).coreHold[1, 0] = jo["Core2.sv_CoreMode.Hold"][0].ToString();
            ((Core)core).coreHold[1, 1] = jo["Core2.sv_CoreMode.Hold"][1].ToString();
            ((Core)core).coreHold[2, 0] = jo["Core3.sv_CoreMode.Hold"][0].ToString();
            ((Core)core).coreHold[2, 1] = jo["Core3.sv_CoreMode.Hold"][1].ToString();
            ((Core)core).coreHold[3, 0] = jo["Core4.sv_CoreMode.Hold"][0].ToString();
            ((Core)core).coreHold[3, 1] = jo["Core4.sv_CoreMode.Hold"][1].ToString();

            ((Core)core).coreInSetScrewCount[0, 0] = jo["Core1.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreInSetScrewCount[0, 1] = jo["Core1.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();
            ((Core)core).coreInSetScrewCount[1, 0] = jo["Core2.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreInSetScrewCount[1, 1] = jo["Core2.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();
            ((Core)core).coreInSetScrewCount[2, 0] = jo["Core3.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreInSetScrewCount[2, 1] = jo["Core3.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();
            ((Core)core).coreInSetScrewCount[3, 0] = jo["Core4.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreInSetScrewCount[3, 1] = jo["Core4.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();

            ((Core)core).coreOutSetScrewCount[0, 0] = jo["Core1.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreOutSetScrewCount[0, 1] = jo["Core1.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();
            ((Core)core).coreOutSetScrewCount[1, 0] = jo["Core2.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreOutSetScrewCount[1, 1] = jo["Core2.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();
            ((Core)core).coreOutSetScrewCount[2, 0] = jo["Core3.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreOutSetScrewCount[2, 1] = jo["Core3.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();
            ((Core)core).coreOutSetScrewCount[3, 0] = jo["Core4.sv_CoreScrewSetValues.iSetRoundsCount"][0].ToString();
            ((Core)core).coreOutSetScrewCount[3, 1] = jo["Core4.sv_CoreScrewSetValues.iSetRoundsCount"][1].ToString();

            ((Core)core).coreInActScrewCount[0, 0] = jo["Core1.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreInActScrewCount[0, 1] = jo["Core1.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();
            ((Core)core).coreInActScrewCount[1, 0] = jo["Core2.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreInActScrewCount[1, 1] = jo["Core2.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();
            ((Core)core).coreInActScrewCount[2, 0] = jo["Core3.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreInActScrewCount[2, 1] = jo["Core3.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();
            ((Core)core).coreInActScrewCount[3, 0] = jo["Core4.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreInActScrewCount[3, 1] = jo["Core4.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();

            ((Core)core).coreOutActScrewCount[0, 0] = jo["Core1.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreOutActScrewCount[0, 1] = jo["Core1.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();
            ((Core)core).coreOutActScrewCount[1, 0] = jo["Core2.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreOutActScrewCount[1, 1] = jo["Core2.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();
            ((Core)core).coreOutActScrewCount[2, 0] = jo["Core3.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreOutActScrewCount[2, 1] = jo["Core3.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();
            ((Core)core).coreOutActScrewCount[3, 0] = jo["Core4.sv_CoreScrewActValues.iActRoundsCount"][0].ToString();
            ((Core)core).coreOutActScrewCount[3, 1] = jo["Core4.sv_CoreScrewActValues.iActRoundsCount"][1].ToString();

            ((Core)core).coreInSetMoveTime[0, 0] = jo["Core1.sv_CoreSetTimes.MoveIn.dSetMoveTime"][0].ToString();
            ((Core)core).coreInSetMoveTime[0, 1] = jo["Core1.sv_CoreSetTimes.MoveIn.dSetMoveTime"][1].ToString();
            ((Core)core).coreInSetMoveTime[1, 0] = jo["Core2.sv_CoreSetTimes.MoveIn.dSetMoveTime"][0].ToString();
            ((Core)core).coreInSetMoveTime[1, 1] = jo["Core2.sv_CoreSetTimes.MoveIn.dSetMoveTime"][1].ToString();
            ((Core)core).coreInSetMoveTime[2, 0] = jo["Core3.sv_CoreSetTimes.MoveIn.dSetMoveTime"][0].ToString();
            ((Core)core).coreInSetMoveTime[2, 1] = jo["Core3.sv_CoreSetTimes.MoveIn.dSetMoveTime"][1].ToString();
            ((Core)core).coreInSetMoveTime[3, 0] = jo["Core4.sv_CoreSetTimes.MoveIn.dSetMoveTime"][0].ToString();
            ((Core)core).coreInSetMoveTime[3, 1] = jo["Core4.sv_CoreSetTimes.MoveIn.dSetMoveTime"][1].ToString();

            ((Core)core).coreInActMoveTime[0, 0] = jo["Core1.sv_CoreActTimes.MoveIn.dActMoveTime"][0].ToString();
            ((Core)core).coreInActMoveTime[0, 1] = jo["Core1.sv_CoreActTimes.MoveIn.dActMoveTime"][1].ToString();
            ((Core)core).coreInActMoveTime[1, 0] = jo["Core2.sv_CoreActTimes.MoveIn.dActMoveTime"][0].ToString();
            ((Core)core).coreInActMoveTime[1, 1] = jo["Core2.sv_CoreActTimes.MoveIn.dActMoveTime"][1].ToString();
            ((Core)core).coreInActMoveTime[2, 0] = jo["Core3.sv_CoreActTimes.MoveIn.dActMoveTime"][0].ToString();
            ((Core)core).coreInActMoveTime[2, 1] = jo["Core3.sv_CoreActTimes.MoveIn.dActMoveTime"][1].ToString();
            ((Core)core).coreInActMoveTime[3, 0] = jo["Core4.sv_CoreActTimes.MoveIn.dActMoveTime"][0].ToString();
            ((Core)core).coreInActMoveTime[3, 1] = jo["Core4.sv_CoreActTimes.MoveIn.dActMoveTime"][1].ToString();

            ((Core)core).coreOutSetMoveTime[0, 0] = jo["Core1.sv_CoreSetTimes.MoveOut.dSetMoveTime"][0].ToString();
            ((Core)core).coreOutSetMoveTime[0, 1] = jo["Core1.sv_CoreSetTimes.MoveOut.dSetMoveTime"][1].ToString();
            ((Core)core).coreOutSetMoveTime[1, 0] = jo["Core2.sv_CoreSetTimes.MoveOut.dSetMoveTime"][0].ToString();
            ((Core)core).coreOutSetMoveTime[1, 1] = jo["Core2.sv_CoreSetTimes.MoveOut.dSetMoveTime"][1].ToString();
            ((Core)core).coreOutSetMoveTime[2, 0] = jo["Core3.sv_CoreSetTimes.MoveOut.dSetMoveTime"][0].ToString();
            ((Core)core).coreOutSetMoveTime[2, 1] = jo["Core3.sv_CoreSetTimes.MoveOut.dSetMoveTime"][1].ToString();
            ((Core)core).coreOutSetMoveTime[3, 0] = jo["Core4.sv_CoreSetTimes.MoveOut.dSetMoveTime"][0].ToString();
            ((Core)core).coreOutSetMoveTime[3, 1] = jo["Core4.sv_CoreSetTimes.MoveOut.dSetMoveTime"][1].ToString();

            ((Core)core).coreOutActMoveTime[0, 0] = jo["Core1.sv_CoreActTimes.MoveOut.dActMoveTime"][0].ToString();
            ((Core)core).coreOutActMoveTime[0, 1] = jo["Core1.sv_CoreActTimes.MoveOut.dActMoveTime"][1].ToString();
            ((Core)core).coreOutActMoveTime[1, 0] = jo["Core2.sv_CoreActTimes.MoveOut.dActMoveTime"][0].ToString();
            ((Core)core).coreOutActMoveTime[1, 1] = jo["Core2.sv_CoreActTimes.MoveOut.dActMoveTime"][1].ToString();
            ((Core)core).coreOutActMoveTime[2, 0] = jo["Core3.sv_CoreActTimes.MoveOut.dActMoveTime"][0].ToString();
            ((Core)core).coreOutActMoveTime[2, 1] = jo["Core3.sv_CoreActTimes.MoveOut.dActMoveTime"][1].ToString();
            ((Core)core).coreOutActMoveTime[3, 0] = jo["Core4.sv_CoreActTimes.MoveOut.dActMoveTime"][0].ToString();
            ((Core)core).coreOutActMoveTime[3, 1] = jo["Core4.sv_CoreActTimes.MoveOut.dActMoveTime"][1].ToString();

            ((Core)core).coreInVelocity[0, 0] = jo["Core1.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInVelocity[0, 1] = jo["Core1.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][1].ToString();
            ((Core)core).coreInVelocity[1, 0] = jo["Core2.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInVelocity[1, 1] = jo["Core2.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][1].ToString();
            ((Core)core).coreInVelocity[2, 0] = jo["Core3.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInVelocity[2, 1] = jo["Core3.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][1].ToString();
            ((Core)core).coreInVelocity[3, 0] = jo["Core4.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInVelocity[3, 1] = jo["Core4.sv_CoreOutput.NormalIn.Velocity.Output.rOutputValue"][1].ToString();

            ((Core)core).coreInPressure[0, 0] = jo["Core1.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInPressure[0, 1] = jo["Core1.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][1].ToString();
            ((Core)core).coreInPressure[1, 0] = jo["Core2.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInPressure[1, 1] = jo["Core2.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][1].ToString();
            ((Core)core).coreInPressure[2, 0] = jo["Core3.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInPressure[2, 1] = jo["Core3.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][1].ToString();
            ((Core)core).coreInPressure[3, 0] = jo["Core4.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreInPressure[3, 1] = jo["Core4.sv_CoreOutput.NormalIn.Pressure.Output.rOutputValue"][1].ToString();

            ((Core)core).coreOutVelocity[0, 0] = jo["Core1.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutVelocity[0, 1] = jo["Core1.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][1].ToString();
            ((Core)core).coreOutVelocity[1, 0] = jo["Core2.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutVelocity[1, 1] = jo["Core2.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][1].ToString();
            ((Core)core).coreOutVelocity[2, 0] = jo["Core3.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutVelocity[2, 1] = jo["Core3.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][1].ToString();
            ((Core)core).coreOutVelocity[3, 0] = jo["Core4.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutVelocity[3, 1] = jo["Core4.sv_CoreOutput.NormalOut.Velocity.Output.rOutputValue"][1].ToString();

            ((Core)core).coreOutPressure[0, 0] = jo["Core1.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutPressure[0, 1] = jo["Core1.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][1].ToString();
            ((Core)core).coreOutPressure[1, 0] = jo["Core2.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutPressure[1, 1] = jo["Core2.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][1].ToString();
            ((Core)core).coreOutPressure[2, 0] = jo["Core3.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutPressure[2, 1] = jo["Core3.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][1].ToString();
            ((Core)core).coreOutPressure[3, 0] = jo["Core4.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][0].ToString();
            ((Core)core).coreOutPressure[3, 1] = jo["Core4.sv_CoreOutput.NormalOut.Pressure.Output.rOutputValue"][1].ToString();

            ((Core)core).coreInMonitorPosition[0, 0] = jo["Core1.sv_rCoreInMonitorPos"][0].ToString();
            ((Core)core).coreInMonitorPosition[0, 1] = jo["Core1.sv_rCoreInMonitorPos"][1].ToString();
            ((Core)core).coreInMonitorPosition[1, 0] = jo["Core2.sv_rCoreInMonitorPos"][0].ToString();
            ((Core)core).coreInMonitorPosition[1, 1] = jo["Core2.sv_rCoreInMonitorPos"][1].ToString();
            ((Core)core).coreInMonitorPosition[2, 0] = jo["Core3.sv_rCoreInMonitorPos"][0].ToString();
            ((Core)core).coreInMonitorPosition[2, 1] = jo["Core3.sv_rCoreInMonitorPos"][1].ToString();
            ((Core)core).coreInMonitorPosition[3, 0] = jo["Core4.sv_rCoreInMonitorPos"][0].ToString();
            ((Core)core).coreInMonitorPosition[3, 1] = jo["Core4.sv_rCoreInMonitorPos"][1].ToString();

            ((Core)core).coreOutMonitorPosition[0, 0] = jo["Core1.sv_rCoreOutMonitorPos"][0].ToString();
            ((Core)core).coreOutMonitorPosition[0, 1] = jo["Core1.sv_rCoreOutMonitorPos"][1].ToString();
            ((Core)core).coreOutMonitorPosition[1, 0] = jo["Core2.sv_rCoreOutMonitorPos"][0].ToString();
            ((Core)core).coreOutMonitorPosition[1, 1] = jo["Core2.sv_rCoreOutMonitorPos"][1].ToString();
            ((Core)core).coreOutMonitorPosition[2, 0] = jo["Core3.sv_rCoreOutMonitorPos"][0].ToString();
            ((Core)core).coreOutMonitorPosition[2, 1] = jo["Core3.sv_rCoreOutMonitorPos"][1].ToString();
            ((Core)core).coreOutMonitorPosition[3, 0] = jo["Core4.sv_rCoreOutMonitorPos"][0].ToString();
            ((Core)core).coreOutMonitorPosition[3, 1] = jo["Core4.sv_rCoreOutMonitorPos"][1].ToString();

            ((Core)core).coreInActPosition[0, 0] = jo["CentralCoordination1.sv_CoreData[1].rCoreInPosition"][0].ToString();
            ((Core)core).coreInActPosition[0, 1] = jo["CentralCoordination1.sv_CoreData[1].rCoreInPosition"][1].ToString();
            ((Core)core).coreInActPosition[1, 0] = jo["CentralCoordination1.sv_CoreData[2].rCoreInPosition"][0].ToString();
            ((Core)core).coreInActPosition[1, 1] = jo["CentralCoordination1.sv_CoreData[2].rCoreInPosition"][1].ToString();
            ((Core)core).coreInActPosition[2, 0] = jo["CentralCoordination1.sv_CoreData[3].rCoreInPosition"][0].ToString();
            ((Core)core).coreInActPosition[2, 1] = jo["CentralCoordination1.sv_CoreData[3].rCoreInPosition"][1].ToString();
            ((Core)core).coreInActPosition[3, 0] = jo["CentralCoordination1.sv_CoreData[4].rCoreInPosition"][0].ToString();
            ((Core)core).coreInActPosition[3, 1] = jo["CentralCoordination1.sv_CoreData[4].rCoreInPosition"][1].ToString();

            ((Core)core).coreOutActPosition[0, 0] = jo["CentralCoordination1.sv_CoreData[1].rCoreOutPosition"][0].ToString();
            ((Core)core).coreOutActPosition[0, 1] = jo["CentralCoordination1.sv_CoreData[1].rCoreOutPosition"][1].ToString();
            ((Core)core).coreOutActPosition[1, 0] = jo["CentralCoordination1.sv_CoreData[2].rCoreOutPosition"][0].ToString();
            ((Core)core).coreOutActPosition[1, 1] = jo["CentralCoordination1.sv_CoreData[2].rCoreOutPosition"][1].ToString();
            ((Core)core).coreOutActPosition[2, 0] = jo["CentralCoordination1.sv_CoreData[3].rCoreOutPosition"][0].ToString();
            ((Core)core).coreOutActPosition[2, 1] = jo["CentralCoordination1.sv_CoreData[3].rCoreOutPosition"][1].ToString();
            ((Core)core).coreOutActPosition[3, 0] = jo["CentralCoordination1.sv_CoreData[4].rCoreOutPosition"][0].ToString();
            ((Core)core).coreOutActPosition[3, 1] = jo["CentralCoordination1.sv_CoreData[4].rCoreOutPosition"][1].ToString();

            ((Core)core).coreInPriority[0, 0] = jo["CentralCoordination1.sv_CoreData[1].iCoreInPriority"][0].ToString();
            ((Core)core).coreInPriority[0, 1] = jo["CentralCoordination1.sv_CoreData[1].iCoreInPriority"][1].ToString();
            ((Core)core).coreInPriority[1, 0] = jo["CentralCoordination1.sv_CoreData[2].iCoreInPriority"][0].ToString();
            ((Core)core).coreInPriority[1, 1] = jo["CentralCoordination1.sv_CoreData[2].iCoreInPriority"][1].ToString();
            ((Core)core).coreInPriority[2, 0] = jo["CentralCoordination1.sv_CoreData[3].iCoreInPriority"][0].ToString();
            ((Core)core).coreInPriority[2, 1] = jo["CentralCoordination1.sv_CoreData[3].iCoreInPriority"][1].ToString();
            ((Core)core).coreInPriority[3, 0] = jo["CentralCoordination1.sv_CoreData[4].iCoreInPriority"][0].ToString();
            ((Core)core).coreInPriority[3, 1] = jo["CentralCoordination1.sv_CoreData[4].iCoreInPriority"][1].ToString();

            ((Core)core).coreOutPriority[0, 0] = jo["CentralCoordination1.sv_CoreData[1].iCoreOutPriority"][0].ToString();
            ((Core)core).coreOutPriority[0, 1] = jo["CentralCoordination1.sv_CoreData[1].iCoreOutPriority"][1].ToString();
            ((Core)core).coreOutPriority[1, 0] = jo["CentralCoordination1.sv_CoreData[2].iCoreOutPriority"][0].ToString();
            ((Core)core).coreOutPriority[1, 1] = jo["CentralCoordination1.sv_CoreData[2].iCoreOutPriority"][1].ToString();
            ((Core)core).coreOutPriority[2, 0] = jo["CentralCoordination1.sv_CoreData[3].iCoreOutPriority"][0].ToString();
            ((Core)core).coreOutPriority[2, 1] = jo["CentralCoordination1.sv_CoreData[3].iCoreOutPriority"][1].ToString();
            ((Core)core).coreOutPriority[3, 0] = jo["CentralCoordination1.sv_CoreData[4].iCoreOutPriority"][0].ToString();
            ((Core)core).coreOutPriority[3, 1] = jo["CentralCoordination1.sv_CoreData[4].iCoreOutPriority"][1].ToString();
            computeFwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Core1.sv_rMaxPressureFwd"][1]);
            computeFwdVelocityTmp = 100.0 / (double)(jo["Core1.sv_rMaxSpeedFwd"][1]);
            computeBwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Core1.sv_rMaxPressureBwd"][1]);
            computeBwdVelocityTmp = 100.0 / (double)(jo["Core1.sv_rMaxSpeedBwd"][1]);
            for (int i = 0; i < 4; i++)
            {
                ((Core)core).coreInPressure[i, 1] = Math.Round(double.Parse(((Core)core).coreInPressure[i, 1]) * computeFwdPressureTmp).ToString();
                ((Core)core).coreOutPressure[i, 1] = Math.Round(double.Parse(((Core)core).coreOutPressure[i, 1]) * computeBwdPressureTmp).ToString();
                ((Core)core).coreInVelocity[i, 1] = Math.Round(double.Parse(((Core)core).coreInVelocity[i, 1]) * computeFwdVelocityTmp).ToString();
                ((Core)core).coreOutVelocity[i, 1] = Math.Round(double.Parse(((Core)core).coreOutVelocity[i, 1]) * computeBwdVelocityTmp).ToString();

            }
            #endregion
            #region //injection
            ((Injection)injection).injectionNoOfPoints[0] = jo["Injection1.sv_InjectProfVis.Profile.iNoOfPoints"][0].ToString();
            ((Injection)injection).injectionNoOfPoints[1] = jo["Injection1.sv_InjectProfVis.Profile.iNoOfPoints"][1].ToString();
            ((Injection)injection).injectionVelocity[0, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[1].rVelocity"][0].ToString();
            ((Injection)injection).injectionVelocity[0, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[1].rVelocity"][1].ToString();
            ((Injection)injection).injectionVelocity[1, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[2].rVelocity"][0].ToString();
            ((Injection)injection).injectionVelocity[1, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[2].rVelocity"][1].ToString();
            ((Injection)injection).injectionVelocity[2, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[3].rVelocity"][0].ToString();
            ((Injection)injection).injectionVelocity[2, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[3].rVelocity"][1].ToString();
            ((Injection)injection).injectionVelocity[3, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[4].rVelocity"][0].ToString();
            ((Injection)injection).injectionVelocity[3, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[4].rVelocity"][1].ToString();
            ((Injection)injection).injectionVelocity[4, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[5].rVelocity"][0].ToString();
            ((Injection)injection).injectionVelocity[4, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[5].rVelocity"][1].ToString();
            ((Injection)injection).injectionVelocity[5, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[6].rVelocity"][0].ToString();
            ((Injection)injection).injectionVelocity[5, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[6].rVelocity"][1].ToString();

            ((Injection)injection).injectionPressure[0, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[1].rPressure"][0].ToString();
            ((Injection)injection).injectionPressure[0, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[1].rPressure"][1].ToString();
            ((Injection)injection).injectionPressure[1, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[2].rPressure"][0].ToString();
            ((Injection)injection).injectionPressure[1, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[2].rPressure"][1].ToString();
            ((Injection)injection).injectionPressure[2, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[3].rPressure"][0].ToString();
            ((Injection)injection).injectionPressure[2, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[3].rPressure"][1].ToString();
            ((Injection)injection).injectionPressure[3, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[4].rPressure"][0].ToString();
            ((Injection)injection).injectionPressure[3, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[4].rPressure"][1].ToString();
            ((Injection)injection).injectionPressure[4, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[5].rPressure"][0].ToString();
            ((Injection)injection).injectionPressure[4, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[5].rPressure"][1].ToString();
            ((Injection)injection).injectionPressure[5, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[6].rPressure"][0].ToString();
            ((Injection)injection).injectionPressure[5, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[6].rPressure"][1].ToString();

            ((Injection)injection).injectionPosition[0, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[1].rStartPos"][0].ToString();
            ((Injection)injection).injectionPosition[0, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[1].rStartPos"][1].ToString();
            ((Injection)injection).injectionPosition[1, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[2].rStartPos"][0].ToString();
            ((Injection)injection).injectionPosition[1, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[2].rStartPos"][1].ToString();
            ((Injection)injection).injectionPosition[2, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[3].rStartPos"][0].ToString();
            ((Injection)injection).injectionPosition[2, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[3].rStartPos"][1].ToString();
            ((Injection)injection).injectionPosition[3, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[4].rStartPos"][0].ToString();
            ((Injection)injection).injectionPosition[3, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[4].rStartPos"][1].ToString();
            ((Injection)injection).injectionPosition[4, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[5].rStartPos"][0].ToString();
            ((Injection)injection).injectionPosition[4, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[5].rStartPos"][1].ToString();
            ((Injection)injection).injectionPosition[5, 0] = jo["Injection1.sv_InjectProfVis.Profile.Points[6].rStartPos"][0].ToString();
            ((Injection)injection).injectionPosition[5, 1] = jo["Injection1.sv_InjectProfVis.Profile.Points[6].rStartPos"][1].ToString();

            ((Injection)injection).injectionHoldNoOfPoints[0] = jo["Injection1.sv_HoldProfVis.Profile.iNoOfPoints"][0].ToString();
            ((Injection)injection).injectionHoldNoOfPoints[1] = jo["Injection1.sv_HoldProfVis.Profile.iNoOfPoints"][1].ToString();
            ((Injection)injection).injectionHoldVelocity[0, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[1].rVelocity"][0].ToString();
            ((Injection)injection).injectionHoldVelocity[0, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[1].rVelocity"][1].ToString();
            ((Injection)injection).injectionHoldVelocity[1, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[2].rVelocity"][0].ToString();
            ((Injection)injection).injectionHoldVelocity[1, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[2].rVelocity"][1].ToString();
            ((Injection)injection).injectionHoldVelocity[2, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[3].rVelocity"][0].ToString();
            ((Injection)injection).injectionHoldVelocity[2, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[3].rVelocity"][1].ToString();
            //((Injection)injection).injectionHoldVelocity[3, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[4].rVelocity"][0].ToString();
            //((Injection)injection).injectionHoldVelocity[3, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[4].rVelocity"][1].ToString();

            ((Injection)injection).injectionHoldPressure[0, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[1].rPressure"][0].ToString();
            ((Injection)injection).injectionHoldPressure[0, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[1].rPressure"][1].ToString();
            ((Injection)injection).injectionHoldPressure[1, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[2].rPressure"][0].ToString();
            ((Injection)injection).injectionHoldPressure[1, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[2].rPressure"][1].ToString();
            ((Injection)injection).injectionHoldPressure[2, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[3].rPressure"][0].ToString();
            ((Injection)injection).injectionHoldPressure[2, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[3].rPressure"][1].ToString();
            //((Injection)injection).injectionHoldPressure[3, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[4].rPressure"][0].ToString();
            //((Injection)injection).injectionHoldPressure[3, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[4].rPressure"][1].ToString();

            ((Injection)injection).injectionHoldPosition[0, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[1].rStartPos"][0].ToString();
            ((Injection)injection).injectionHoldPosition[0, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[1].rStartPos"][1].ToString();
            ((Injection)injection).injectionHoldPosition[1, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[2].rStartPos"][0].ToString();
            ((Injection)injection).injectionHoldPosition[1, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[2].rStartPos"][1].ToString();
            ((Injection)injection).injectionHoldPosition[2, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[3].rStartPos"][0].ToString();
            ((Injection)injection).injectionHoldPosition[2, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[3].rStartPos"][1].ToString();
            //((Injection)injection).injectionHoldPosition[3, 0] = jo["Injection1.sv_HoldProfVis.Profile.Points[4].rStartPos"][0].ToString();
            //((Injection)injection).injectionHoldPosition[3, 1] = jo["Injection1.sv_HoldProfVis.Profile.Points[4].rStartPos"][1].ToString();

            ((Injection)injection).cutOffUseInjectTime[0] = jo["Injection1.sv_CutOffParams.bUseTimer"][0].ToString();
            ((Injection)injection).cutOffUseInjectTime[1] = jo["Injection1.sv_CutOffParams.bUseTimer"][1].ToString();
            ((Injection)injection).cutOffInjectTime[0] = jo["Injection1.sv_InjectTimesAct.dActMoveTime"][0].ToString();
            ((Injection)injection).cutOffInjectTime[1] = jo["Injection1.sv_InjectTimesAct.dActMoveTime"][1].ToString();
            ((Injection)injection).cutOffThresholdInjectTime[0] = jo["Injection1.sv_CutOffParams.dTimeThreshold"][0].ToString();
            ((Injection)injection).cutOffThresholdInjectTime[1] = jo["Injection1.sv_CutOffParams.dTimeThreshold"][1].ToString();

            ((Injection)injection).cutOffUseInjectPressure[0] = jo["Injection1.sv_CutOffParams.bUseInjectPressure"][0].ToString();
            ((Injection)injection).cutOffUseInjectPressure[1] = jo["Injection1.sv_CutOffParams.bUseInjectPressure"][1].ToString();
            ((Injection)injection).cutOffInjectPressure[0] = jo["Injection1.sv_rCutOffPressure"][0].ToString();
            ((Injection)injection).cutOffInjectPressure[1] = jo["Injection1.sv_rCutOffPressure"][1].ToString();
            ((Injection)injection).cutOffThresholdInjectPressure[0] = jo["Injection1.sv_CutOffParams.rInjectPressureThreshold"][0].ToString();
            ((Injection)injection).cutOffThresholdInjectPressure[1] = jo["Injection1.sv_CutOffParams.rInjectPressureThreshold"][1].ToString();

            ((Injection)injection).cutOffUseScrewPosition[0] = jo["Injection1.sv_CutOffParams.bUsePosition"][0].ToString();
            ((Injection)injection).cutOffUseScrewPosition[1] = jo["Injection1.sv_CutOffParams.bUsePosition"][1].ToString();
            ((Injection)injection).cutOffScrewPosition[0] = jo["Injection1.sv_rCutOffPosition"][0].ToString();
            ((Injection)injection).cutOffScrewPosition[1] = jo["Injection1.sv_rCutOffPosition"][1].ToString();
            ((Injection)injection).cutOffThresholdInjectPressure[0] = jo["Injection1.sv_CutOffParams.rPositionThreshold"][0].ToString();
            ((Injection)injection).cutOffThresholdInjectPressure[1] = jo["Injection1.sv_CutOffParams.rPositionThreshold"][1].ToString();

            ((Injection)injection).injectionActPressure[0] = jo["Injection1.sv_rActPressure"][0].ToString();
            ((Injection)injection).injectionActPressure[1] = jo["Injection1.sv_rActPressure"][1].ToString();
            ((Injection)injection).screwPosition[0] = jo["Injection1.sv_rScrewPosition"][0].ToString();
            ((Injection)injection).screwPosition[1] = jo["Injection1.sv_rScrewPosition"][1].ToString();
            ((Injection)injection).actCoolingTime[0] = jo["CoolingTime1.sv_dRemainTime"][0].ToString();
            ((Injection)injection).actCoolingTime[1] = jo["CoolingTime1.sv_dRemainTime"][1].ToString();
            ((Injection)injection).setCoolingTime[0] = jo["CoolingTime1.sv_dCoolingTime"][0].ToString();
            ((Injection)injection).setCoolingTime[1] = jo["CoolingTime1.sv_dCoolingTime"][1].ToString();
            computeFwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Injection1.sv_rMaxPressureFwd"][1]);
            computeFwdVelocityTmp = 100.0 / (double)(jo["Injection1.sv_rMaxSpeedFwdSpec"][1]);
            double computeHoldTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Injection1.sv_rMaxHoldPresAllow"][1]);
            for (int i = 0; i < 6; i++)
            {
                ((Injection)injection).injectionPressure[i, 1] = Math.Round(double.Parse(((Injection)injection).injectionPressure[i, 1]) * computeFwdPressureTmp).ToString();
                ((Injection)injection).injectionVelocity[i, 1] = Math.Round(double.Parse(((Injection)injection).injectionVelocity[i, 1]) * computeFwdVelocityTmp).ToString();
                if (i < 3)
                {
                    ((Injection)injection).injectionHoldVelocity[i, 1] = Math.Round(double.Parse(((Injection)injection).injectionHoldVelocity[i, 1]) * computeFwdVelocityTmp).ToString();
                    ((Injection)injection).injectionHoldPressure[i, 1] = Math.Round(double.Parse(((Injection)injection).injectionHoldPressure[i, 1]) * computeHoldTmp).ToString();

                }
            }
            #endregion
            #region //Plasticize
            ((Plasticize)plasticize).plasticizeNoOfPoints[0] = jo["Injection1.sv_PlastProfVis.Profile.iNoOfPoints"][0].ToString();
            ((Plasticize)plasticize).plasticizeNoOfPoints[1] = jo["Injection1.sv_PlastProfVis.Profile.iNoOfPoints"][1].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[0, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[1].rRotation"][0].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[0, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[1].rRotation"][1].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[1, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[2].rRotation"][0].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[1, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[2].rRotation"][1].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[2, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[3].rRotation"][0].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[2, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[3].rRotation"][1].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[3, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[4].rRotation"][0].ToString();
            ((Plasticize)plasticize).plasticizeVelocity[3, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[4].rRotation"][1].ToString();

            ((Plasticize)plasticize).plasticizePressure[0, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[1].rBackPressure"][0].ToString();
            ((Plasticize)plasticize).plasticizePressure[0, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[1].rBackPressure"][1].ToString();
            ((Plasticize)plasticize).plasticizePressure[1, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[2].rBackPressure"][0].ToString();
            ((Plasticize)plasticize).plasticizePressure[1, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[2].rBackPressure"][1].ToString();
            ((Plasticize)plasticize).plasticizePressure[2, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[3].rBackPressure"][0].ToString();
            ((Plasticize)plasticize).plasticizePressure[2, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[3].rBackPressure"][1].ToString();
            ((Plasticize)plasticize).plasticizePressure[3, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[4].rBackPressure"][0].ToString();
            ((Plasticize)plasticize).plasticizePressure[3, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[4].rBackPressure"][1].ToString();

            ((Plasticize)plasticize).plasticizePosition[0, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[1].rStartPos"][0].ToString();
            ((Plasticize)plasticize).plasticizePosition[0, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[1].rStartPos"][1].ToString();
            ((Plasticize)plasticize).plasticizePosition[1, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[2].rStartPos"][0].ToString();
            ((Plasticize)plasticize).plasticizePosition[1, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[2].rStartPos"][1].ToString();
            ((Plasticize)plasticize).plasticizePosition[2, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[3].rStartPos"][0].ToString();
            ((Plasticize)plasticize).plasticizePosition[2, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[3].rStartPos"][1].ToString();
            ((Plasticize)plasticize).plasticizePosition[3, 0] = jo["Injection1.sv_PlastProfVis.Profile.Points[4].rStartPos"][0].ToString();
            ((Plasticize)plasticize).plasticizePosition[3, 1] = jo["Injection1.sv_PlastProfVis.Profile.Points[4].rStartPos"][1].ToString();

            ((Plasticize)plasticize).beforePlasticizeMode[0] = jo["Injection1.sv_DecompBefPlastSettings.Mode"][0].ToString();
            ((Plasticize)plasticize).beforePlasticizeMode[1] = jo["Injection1.sv_DecompBefPlastSettings.Mode"][1].ToString();
            ((Plasticize)plasticize).beforePlasticizeVelocity[0] = jo["Injection1.sv_DecompAftPlastSettings.ConstOutput.Velocity.Output.rOutputValue"][0].ToString();
            ((Plasticize)plasticize).beforePlasticizeVelocity[1] = jo["Injection1.sv_DecompAftPlastSettings.ConstOutput.Velocity.Output.rOutputValue"][1].ToString();
            ((Plasticize)plasticize).beforePlasticizePressure[0] = jo["Injection1.sv_DecompAftPlastSettings.ConstOutput.Pressure.Output.rOutputValue"][0].ToString();
            ((Plasticize)plasticize).beforePlasticizePressure[1] = jo["Injection1.sv_DecompAftPlastSettings.ConstOutput.Pressure.Output.rOutputValue"][1].ToString();
            ((Plasticize)plasticize).beforePlasticizePosition[0] = jo["Injection1.sv_DecompAftPlastSettings.rDecompPos"][0].ToString();
            ((Plasticize)plasticize).beforePlasticizePosition[1] = jo["Injection1.sv_DecompAftPlastSettings.rDecompPos"][1].ToString();
            ((Plasticize)plasticize).beforePlasticizeTime[0] = jo["Injection1.sv_DecompAftPlastSettings.dDecompTime"][0].ToString();
            ((Plasticize)plasticize).beforePlasticizeTime[1] = jo["Injection1.sv_DecompAftPlastSettings.dDecompTime"][1].ToString();

            ((Plasticize)plasticize).afterPlasticizeVelocity[0] = jo["Injection1.sv_DecompBefPlastSettings.ConstOutput.Velocity.Output.rOutputValue"][0].ToString();
            ((Plasticize)plasticize).afterPlasticizeVelocity[1] = jo["Injection1.sv_DecompBefPlastSettings.ConstOutput.Velocity.Output.rOutputValue"][1].ToString();
            ((Plasticize)plasticize).afterPlasticizePressure[0] = jo["Injection1.sv_DecompBefPlastSettings.ConstOutput.Pressure.Output.rOutputValue"][0].ToString();
            ((Plasticize)plasticize).afterPlasticizePressure[1] = jo["Injection1.sv_DecompBefPlastSettings.ConstOutput.Pressure.Output.rOutputValue"][1].ToString();
            ((Plasticize)plasticize).afterPlasticizePosition[0] = jo["Injection1.sv_DecompBefPlastSettings.rDecompPos"][0].ToString();
            ((Plasticize)plasticize).afterPlasticizePosition[1] = jo["Injection1.sv_DecompBefPlastSettings.rDecompPos"][1].ToString();
            ((Plasticize)plasticize).afterPlasticizeTime[0] = jo["Injection1.sv_DecompBefPlastSettings.dDecompTime"][0].ToString();
            ((Plasticize)plasticize).afterPlasticizeTime[1] = jo["Injection1.sv_DecompBefPlastSettings.dDecompTime"][1].ToString();
            #endregion
            #region //carriage

            ((Carriage)carriage).carriageFwdNoOfPoints[0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.iNoOfPoints"][0].ToString();
            ((Carriage)carriage).carriageFwdNoOfPoints[1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.iNoOfPoints"][1].ToString();
            ((Carriage)carriage).carriageFwdVelocity[0, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[1].rVelocity"][0].ToString();
            ((Carriage)carriage).carriageFwdVelocity[0, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[1].rVelocity"][1].ToString();
            ((Carriage)carriage).carriageFwdVelocity[1, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[2].rVelocity"][0].ToString();
            ((Carriage)carriage).carriageFwdVelocity[1, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[2].rVelocity"][1].ToString();
            ((Carriage)carriage).carriageFwdVelocity[1, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[3].rVelocity"][0].ToString();
            ((Carriage)carriage).carriageFwdVelocity[1, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[3].rVelocity"][1].ToString();

            ((Carriage)carriage).carriageFwdPressure[0, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[1].rPressure"][0].ToString();
            ((Carriage)carriage).carriageFwdPressure[0, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[1].rPressure"][1].ToString();
            ((Carriage)carriage).carriageFwdPressure[1, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[2].rPressure"][0].ToString();
            ((Carriage)carriage).carriageFwdPressure[1, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[2].rPressure"][1].ToString();
            ((Carriage)carriage).carriageFwdPressure[1, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[3].rPressure"][0].ToString();
            ((Carriage)carriage).carriageFwdPressure[1, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[3].rPressure"][1].ToString();

            ((Carriage)carriage).carriageFwdPosition[0, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[1].rStartPos"][0].ToString();
            ((Carriage)carriage).carriageFwdPosition[0, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[1].rStartPos"][1].ToString();
            ((Carriage)carriage).carriageFwdPosition[1, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[2].rStartPos"][0].ToString();
            ((Carriage)carriage).carriageFwdPosition[1, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[2].rStartPos"][1].ToString();
            ((Carriage)carriage).carriageFwdPosition[1, 0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[3].rStartPos"][0].ToString();
            ((Carriage)carriage).carriageFwdPosition[1, 1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.Points[3].rStartPos"][1].ToString();

            ((Carriage)carriage).carriageBwdNoOfPoints[0] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.iNoOfPoints"][0].ToString();
            ((Carriage)carriage).carriageBwdNoOfPoints[1] = jo["Nozzle1.sv_NozzleFwdProfVis.Profile.iNoOfPoints"][1].ToString();
            ((Carriage)carriage).carriageBwdVelocity[0, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[1].rVelocity"][0].ToString();
            ((Carriage)carriage).carriageBwdVelocity[0, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[1].rVelocity"][1].ToString();
            ((Carriage)carriage).carriageBwdVelocity[1, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[2].rVelocity"][0].ToString();
            ((Carriage)carriage).carriageBwdVelocity[1, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[2].rVelocity"][1].ToString();
            ((Carriage)carriage).carriageBwdVelocity[1, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[3].rVelocity"][0].ToString();
            ((Carriage)carriage).carriageBwdVelocity[1, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[3].rVelocity"][1].ToString();

            ((Carriage)carriage).carriageBwdPressure[0, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[1].rPressure"][0].ToString();
            ((Carriage)carriage).carriageBwdPressure[0, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[1].rPressure"][1].ToString();
            ((Carriage)carriage).carriageBwdPressure[1, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[2].rPressure"][0].ToString();
            ((Carriage)carriage).carriageBwdPressure[1, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[2].rPressure"][1].ToString();
            ((Carriage)carriage).carriageBwdPressure[1, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[3].rPressure"][0].ToString();
            ((Carriage)carriage).carriageBwdPressure[1, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[3].rPressure"][1].ToString();

            ((Carriage)carriage).carriageBwdPosition[0, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[1].rStartPos"][0].ToString();
            ((Carriage)carriage).carriageBwdPosition[0, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[1].rStartPos"][1].ToString();
            ((Carriage)carriage).carriageBwdPosition[1, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[2].rStartPos"][0].ToString();
            ((Carriage)carriage).carriageBwdPosition[1, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[2].rStartPos"][1].ToString();
            ((Carriage)carriage).carriageBwdPosition[1, 0] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[3].rStartPos"][0].ToString();
            ((Carriage)carriage).carriageBwdPosition[1, 1] = jo["Nozzle1.sv_NozzleBwdProfVis.Profile.Points[3].rStartPos"][1].ToString();
           
            
            computeFwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Nozzle1.sv_rMaxPressureFwd"][1]);
            computeFwdVelocityTmp = 100.0 / (double)(jo["Nozzle1.sv_rMaxSpeedFwd"][1]);
            computeBwdPressureTmp = (double)(jo["system.sv_rMaximumPressure"][1]) / (double)(jo["Nozzle1.sv_rMaxPressureBwd"][1]);
            computeBwdVelocityTmp = 100.0 / (double)(jo["Nozzle1.sv_rMaxSpeedBwd"][1]);
            for (int i = 0; i < 2; i++)
            {
                ((Carriage)carriage).carriageFwdPressure[i, 1] = Math.Round(double.Parse(((Carriage)carriage).carriageFwdPressure[i, 1]) * computeFwdPressureTmp).ToString();
                ((Carriage)carriage).carriageBwdPressure[i, 1] = Math.Round(double.Parse(((Carriage)carriage).carriageBwdPressure[i, 1]) * computeBwdPressureTmp).ToString();
                ((Carriage)carriage).carriageFwdVelocity[i, 1] = Math.Round(double.Parse(((Carriage)carriage).carriageFwdVelocity[i, 1]) * computeFwdVelocityTmp).ToString();
                ((Carriage)carriage).carriageBwdVelocity[i, 1] = Math.Round(double.Parse(((Carriage)carriage).carriageBwdVelocity[i, 1]) * computeBwdVelocityTmp).ToString();
            }
            #endregion

            #region //overview
            ((OverView)overView).alarm[0] = jo["OperationMode1.sv_iSetAlarmSignalCount"][0].ToString();
            ((OverView)overView).alarm[1] = jo["OperationMode1.sv_iSetAlarmSignalCount"][1].ToString();
            ((OverView)overView).productCounterAct[0] = jo["system.sv_iProdCounterAct"][0].ToString();
            ((OverView)overView).productCounterAct[1] = jo["system.sv_iProdCounterAct"][1].ToString();
            ((OverView)overView).productCounterSet[0] = jo["system.sv_iProdCounterSet"][0].ToString();
            ((OverView)overView).productCounterSet[1] = jo["system.sv_iProdCounterSet"][1].ToString();
            ((OverView)overView).productTimeAct[0] = jo["OperationMode1.sv_rProdTimeAct"][0].ToString();
            ((OverView)overView).productTimeAct[1] = jo["OperationMode1.sv_rProdTimeAct"][1].ToString();
            ((OverView)overView).productTimeSet[0] = jo["OperationMode1.sv_rProdTimeTotal"][0].ToString();
            ((OverView)overView).productTimeSet[1] = jo["OperationMode1.sv_rProdTimeTotal"][1].ToString();
            ((OverView)overView).lastCycleTime[0] = jo["system.sv_dLastCycleTime"][0].ToString();
            ((OverView)overView).lastCycleTime[1] = jo["system.sv_dLastCycleTime"][1].ToString();
            ((OverView)overView).moldDataName[0] = jo["system.sv_sMoldData"][0].ToString();
            ((OverView)overView).moldDataName[1] = jo["system.sv_sMoldData"][1].ToString();
            ((OverView)overView).moldPosition[1] = jo["Mold1.sv_rMoldPosition"][1].ToString();
            ((OverView)overView).ejectorPosition[1] = jo["Ejector1.sv_rEjectorPositionRel"][1].ToString();
            ((OverView)overView).injectionPosition[1] = jo["Injection1.sv_rScrewPosition"][1].ToString();
            ((OverView)overView).carriagePosition[1] = jo["Nozzle1.sv_rNozzlePosition"][1].ToString();
            //((OverView)overView).pump1Velocity[1] = jo["Pump1.sv_rVelocity"][1].ToString();
            //((OverView)overView).pump1Pressure[1] = jo["Pump1.sv_rPressure"][1].ToString();
            //((OverView)overView).pump2Velocity[1] = jo["Pump2.sv_rVelocity"][1].ToString();
            //((OverView)overView).pump2Pressure[1] = jo["Pump2.sv_rPressure"][1].ToString();
            ((OverView)overView).backPressure[1] = jo["Injection1.sv_rBackPressure"][1].ToString();
            ((OverView)overView).actScrewRpm[1] = jo["Injection1.sv_rActScrewRpm"][1].ToString();

            #endregion
            strMachineData = JsonConvert.SerializeObject((object)this);
            JObject deleteValName = (JObject)JsonConvert.DeserializeObject(strMachineData);
            foreach (var item in deleteValName)
            {
                if (item.Value.ToString().IndexOf("\":") != -1)
                {
                    foreach (var itemValue in (JObject)item.Value)
                    {
                        string valueName = itemValue.Key;
                        if (deleteValName[item.Key][itemValue.Key][0].Count() >= 2)
                        {
                            for (int j = 0; j < deleteValName[item.Key][itemValue.Key].Count(); j++)
                            {
                 
                                    deleteValName[item.Key][itemValue.Key][j][0] = "";
                            }
                        }
                        else
                        {

                                deleteValName[item.Key][itemValue.Key][0] = "";
                        }
                    }
                }
            }


        }


        public virtual void convertedFromGefranPerforma(JObject jo, ref string strMachineData)
        {
            InjectionMachineWithGefranPerfoma machineData = JsonConvert.DeserializeObject<InjectionMachineWithGefranPerfoma>(jo.ToString());
            machineID = machineData.machineID;
            sampleTime = machineData.sampleTime;
            timestamp = machineData.timestamp;
            for (int i = 0; i < 8; i++)
            {
                ((Injection)injection).injectionPosition[i, 1] = ((InjectionDataFromGefranPerfoma)machineData.injection).INJEC_DATA_M__POS_DS[i, 1];
                ((Injection)injection).injectionPressure[i, 1] = ((InjectionDataFromGefranPerfoma)machineData.injection).INJEC_DATA_M__PRE_DS[i, 1];
                ((Injection)injection).injectionVelocity[i, 1] = ((InjectionDataFromGefranPerfoma)machineData.injection).INJEC_DATA_M__SPD_DS[i, 1];




                if (i < 5)
                { 
                ((Clamp)clamp).moldClosePosition[i, 1] = ((ClampDataFromGefranPerfoma)machineData.clamp).CL_MOULD_DATA_M__POS_DS[4-i, 1];
                ((Clamp)clamp).moldClosePressure[i, 1] = ((ClampDataFromGefranPerfoma)machineData.clamp).CL_MOULD_DATA_M__PRE_DS[4-i, 1];
                ((Clamp)clamp).moldCloseVelocity[i, 1] = ((ClampDataFromGefranPerfoma)machineData.clamp).CL_MOULD_DATA_M__SPD_DS[4-i, 1];
                ((Clamp)clamp).moldOpenPosition[i, 1] = ((ClampDataFromGefranPerfoma)machineData.clamp).OP_MOULD_DATA_M__POS_DS[i, 1];
                ((Clamp)clamp).moldOpenPressure[i, 1] = ((ClampDataFromGefranPerfoma)machineData.clamp).OP_MOULD_DATA_M__PRE_DS[i, 1];
                ((Clamp)clamp).moldOpenVelocity[i, 1] = ((ClampDataFromGefranPerfoma)machineData.clamp).OP_MOULD_DATA_M__SPD_DS[i, 1];

                ((Injection)injection).injectionHoldVelocity[0,1]= ((InjectionDataFromGefranPerfoma)machineData.injection).HOLD_SPD[1];
                ((Injection)injection).injectionHoldPressure[i,1]= ((InjectionDataFromGefranPerfoma)machineData.injection).INJEC_LP_DATA__PRE_DS[i,1];
                ((Injection)injection).injectionHoldPosition[i, 1] = ((InjectionDataFromGefranPerfoma)machineData.injection).LP_PHT_DS[i, 1];
               
                }
                if (i < 4)
                {
                    ((Core)core).coreInVelocity[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SPEED_A_DS__N[i,1];
                    ((Core)core).coreInPressure[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_PRESSURE_A_DS__N[i, 1];
                    ((Core)core).coreOutVelocity[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SPEED_B_DS__N[i, 1];
                    ((Core)core).coreOutPressure[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_PRESSURE_B_DS__N[i, 1];
                    ((Core)core).coreUse[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_ENABLE__N[i, 1];
                    ((Core)core).coreInActPosition[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SET_POSITION_PHASE3_A__N[i, 1];
                    ((Core)core).coreOutActPosition[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SET_POSITION_PHASE3_B__N[i, 1];
                    ((Core)core).coreInMonitorPosition[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SET_SAFETY_POSITION_PHASE3_A__N[i, 1];
                    ((Core)core).coreOutMonitorPosition[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SET_SAFETY_POSITION_PHASE3_B__N[i, 1];
                    ((Core)core).coreInSetMoveTime[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SET_DURATION_TIME_A__N[i, 1];
                    ((Core)core).coreOutSetMoveTime[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_SET_DURATION_TIME_B__N[i, 1];
                    ((Core)core).coreInPriority[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_PRIORITY_A__N[i, 1];
                    ((Core)core).coreOutPriority[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_PRIORITY_B__N[i, 1];
                    ((Core)core).coreInMode[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_START_POSITIONS_MODE_A__N[i, 1];
                    ((Core)core).coreOutMode[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_START_POSITIONS_MODE_B__N[i, 1];
                    ((Core)core).coreHold[i, 1] = ((CoreDataFromGefranPerfoma)machineData.core).ACT_VALVE_STAY_ON_A__N[i, 1];


                    ((Ejector)ejector).airVavleMode[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).BLOWS_DATA__MODE[i, 1];
                    ((Ejector)ejector).airVavleSetDelayTime[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).BLOWS_DATA__DLY_ON[i, 1];
                    ((Ejector)ejector).airVavleSetMoveTime[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).BLOWS_DATA__DURATION[i, 1];
                    ((Ejector)ejector).airVavleStartPosition[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).BLOWS_DATA__POS_ON_DS[i, 1];



                }
                if (i < 3)
                {
                    ((Ejector)ejector).ejectorFwdPosition[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).EJECT_FW_DATA_M__POS_DS[i, 1];
                    ((Ejector)ejector).ejectorFwdPressure[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).EJECT_FW_DATA_M__PRE_DS[i, 1];
                    ((Ejector)ejector).ejectorFwdVelocity[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).EJECT_FW_DATA_M__SPD_DS[i, 1];
                    ((Ejector)ejector).ejectorBwdPosition[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).EJECT_BW_DATA_M__POS_DS[2-i, 1];
                    ((Ejector)ejector).ejectorBwdPressure[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).EJECT_BW_DATA_M__PRE_DS[2-i, 1];
                    ((Ejector)ejector).ejectorBwdVelocity[i, 1] = ((EjectorDataFromGefranPerfoma)machineData.ejector).EJECT_BW_DATA_M__SPD_DS[2-i, 1];

                
                }
                if (i < 2)
                {
                    ((Carriage)carriage).carriageFwdPosition[i, 1] = ((CarriageDataFromGefranPerfoma)machineData.carriage).CFW_DATA_M__POS_DS[i, 1];
                    ((Carriage)carriage).carriageFwdPressure[i, 1] = ((CarriageDataFromGefranPerfoma)machineData.carriage).CFW_DATA_M__PRE_DS[i, 1];
                    ((Carriage)carriage).carriageFwdVelocity[i, 1] = ((CarriageDataFromGefranPerfoma)machineData.carriage).CFW_DATA_M__SPD_DS[i, 1];
                    ((Carriage)carriage).carriageBwdPosition[i, 1] = ((CarriageDataFromGefranPerfoma)machineData.carriage).CBK_DATA_M__POS_DS[i, 1];
                    ((Carriage)carriage).carriageBwdPressure[i, 1] = ((CarriageDataFromGefranPerfoma)machineData.carriage).CBK_DATA_M__PRE_DS[i, 1];
                    ((Carriage)carriage).carriageBwdVelocity[i, 1] = ((CarriageDataFromGefranPerfoma)machineData.carriage).CBK_DATA_M__SPD_DS[i, 1];
                  
                
                }
            }
            
          //  ((OverView)overView).alarm[1] = jo["OperationMode1.sv_iSetAlarmSignalCount"][1].ToString();
            ((OverView)overView).productCounterAct[1] = ((OverviewDataFromGefranPerfoma)machineData.overView).CURRENT_TOTAL_PRODUCTION[1];
            ((OverView)overView).productCounterSet[1] = ((OverviewDataFromGefranPerfoma)machineData.overView).TOTAL_PRODUCTION[1];
           // ((OverView)overView).productTimeAct[1] = jo["OperationMode1.sv_rProdTimeAct"][1].ToString();
          //  ((OverView)overView).productTimeSet[1] = jo["OperationMode1.sv_rProdTimeTotal"][1].ToString();
            ((OverView)overView).lastCycleTime[1] = ((OverviewDataFromGefranPerfoma)machineData.overView).CYCLE_DURATION[1];
          //  ((OverView)overView).moldDataName[1] = jo["system.sv_sMoldData"][1].ToString();
            //((OverView)overView).moldPosition[1] = jo["Mold1.sv_rMoldPosition"][1].ToString();
            //((OverView)overView).ejectorPosition[1] = jo["Ejector1.sv_rEjectorPositionRel"][1].ToString();
            //((OverView)overView).injectionPosition[1] = jo["Injection1.sv_rScrewPosition"][1].ToString();
            //((OverView)overView).carriagePosition[1] = jo["Nozzle1.sv_rNozzlePosition"][1].ToString();

            strMachineData = JsonConvert.SerializeObject((object)this);

        }

        public virtual void convertedFromGefranVedo(JObject jo, ref string strMachineData)
        {
            #region //clamp
            ((Clamp)clamp).moldClosePressure[0, 1] = jo["clamp"]["sPR_MC01"][1].ToString();
            ((Clamp)clamp).moldClosePressure[1, 1] = jo["clamp"]["sPR_MC02"][1].ToString();
            ((Clamp)clamp).moldClosePressure[2, 1] = jo["clamp"]["sPR_MC03"][1].ToString();
            ((Clamp)clamp).moldClosePressure[3, 1] = jo["clamp"]["sPR_MC04"][1].ToString();
            ((Clamp)clamp).moldClosePressure[4, 1] = jo["clamp"]["sPR_MC05"][1].ToString();

            ((Clamp)clamp).moldCloseVelocity[0, 1] = jo["clamp"]["sSP_MC01"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[1, 1] = jo["clamp"]["sSP_MC02"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[2, 1] = jo["clamp"]["sSP_MC03"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[3, 1] = jo["clamp"]["sSP_MC04"][1].ToString();
            ((Clamp)clamp).moldCloseVelocity[4, 1] = jo["clamp"]["sSP_MC05"][1].ToString();

            ((Clamp)clamp).moldClosePosition[0, 1] = jo["clamp"]["sPO_MC01"][1].ToString();
            ((Clamp)clamp).moldClosePosition[1, 1] = jo["clamp"]["sPO_MC02"][1].ToString();
            ((Clamp)clamp).moldClosePosition[2, 1] = jo["clamp"]["sPO_MC03"][1].ToString();
            ((Clamp)clamp).moldClosePosition[3, 1] = jo["clamp"]["sPO_MC04"][1].ToString();

            ((Clamp)clamp).moldOpenPressure[0, 1] = jo["clamp"]["sPR_MO01"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[1, 1] = jo["clamp"]["sPR_MO02"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[2, 1] = jo["clamp"]["sPR_MO03"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[3, 1] = jo["clamp"]["sPR_MO04"][1].ToString();
            ((Clamp)clamp).moldOpenPressure[4, 1] = jo["clamp"]["sPR_MO05"][1].ToString();

            ((Clamp)clamp).moldOpenVelocity[0, 1] = jo["clamp"]["sSP_MO01"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[1, 1] = jo["clamp"]["sSP_MO02"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[2, 1] = jo["clamp"]["sSP_MO03"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[3, 1] = jo["clamp"]["sSP_MO04"][1].ToString();
            ((Clamp)clamp).moldOpenVelocity[4, 1] = jo["clamp"]["sSP_MO05"][1].ToString();

            ((Clamp)clamp).moldOpenPosition[0, 1] = jo["clamp"]["sPO_MO01"][1].ToString();
            ((Clamp)clamp).moldOpenPosition[1, 1] = jo["clamp"]["sPO_MO02"][1].ToString();
            ((Clamp)clamp).moldOpenPosition[2, 1] = jo["clamp"]["sPO_MO03"][1].ToString();
            ((Clamp)clamp).moldOpenPosition[3, 1] = jo["clamp"]["sPO_MO04"][1].ToString();
            #endregion

            #region //ejector
            ((Ejector)ejector).ejectorFwdPressure[0, 1] = jo["ejector"]["sSP_EJF2"][1].ToString();
            ((Ejector)ejector).ejectorFwdPressure[1, 1] = jo["ejector"]["sPR_EJF1"][1].ToString();

            ((Ejector)ejector).ejectorFwdVelocity[0, 1] = jo["ejector"]["sPO_EJFW"][1].ToString();
            ((Ejector)ejector).ejectorFwdVelocity[1, 1] = jo["ejector"]["sSP_EJF1"][1].ToString();

            ((Ejector)ejector).ejectorFwdPosition[0, 1] = jo["ejector"]["sPR_EJB1"][1].ToString();
            ((Ejector)ejector).ejectorFwdPosition[1, 1] = jo["ejector"]["sPO_EJF1"][1].ToString();

            ((Ejector)ejector).ejectorBwdPressure[0, 1] = jo["ejector"]["sPR_EJB2"][1].ToString();
            ((Ejector)ejector).ejectorBwdPressure[1, 1] = jo["ejector"]["sSP_EJB1"][1].ToString();

            ((Ejector)ejector).ejectorBwdVelocity[0, 1] = jo["ejector"]["sSP_EJB2"][1].ToString();
            ((Ejector)ejector).ejectorBwdVelocity[1, 1] = jo["ejector"]["sPO_EJB1"][1].ToString();

            ((Ejector)ejector).ejectorBwdPosition[0, 1] = jo["ejector"]["sPO_EJBK"][1].ToString();
            ((Ejector)ejector).ejectorFwdPosition[1, 1] = jo["ejector"]["aPO_EJEC"][1].ToString();

            #endregion
            #region //injection
            ((Injection)injection).injectionPressure[0, 1] = jo["injection"]["sPR_IN01"][1].ToString();
            ((Injection)injection).injectionPressure[1, 1] = jo["injection"]["sPR_IN02"][1].ToString();
            ((Injection)injection).injectionPressure[2, 1] = jo["injection"]["sPR_IN03"][1].ToString();
            ((Injection)injection).injectionPressure[3, 1] = jo["injection"]["sPR_IN04"][1].ToString();
            ((Injection)injection).injectionPressure[4, 1] = jo["injection"]["sPR_IN05"][1].ToString();
            ((Injection)injection).injectionPressure[5, 1] = jo["injection"]["sPR_IN06"][1].ToString();
            ((Injection)injection).injectionPressure[6, 1] = jo["injection"]["sPR_IN07"][1].ToString();
            ((Injection)injection).injectionPressure[7, 1] = jo["injection"]["sPR_IN08"][1].ToString();
            ((Injection)injection).injectionPressure[8, 1] = jo["injection"]["sPR_IN09"][1].ToString();
            ((Injection)injection).injectionPressure[9, 1] = jo["injection"]["sPR_IN10"][1].ToString();

            ((Injection)injection).injectionVelocity[0, 1] = jo["injection"]["sSP_IN01"][1].ToString();
            ((Injection)injection).injectionVelocity[1, 1] = jo["injection"]["sSP_IN02"][1].ToString();
            ((Injection)injection).injectionVelocity[2, 1] = jo["injection"]["sSP_IN03"][1].ToString();
            ((Injection)injection).injectionVelocity[3, 1] = jo["injection"]["sSP_IN04"][1].ToString();
            ((Injection)injection).injectionVelocity[4, 1] = jo["injection"]["sSP_IN05"][1].ToString();
            ((Injection)injection).injectionVelocity[5, 1] = jo["injection"]["sSP_IN06"][1].ToString();
            ((Injection)injection).injectionVelocity[6, 1] = jo["injection"]["sSP_IN07"][1].ToString();
            ((Injection)injection).injectionVelocity[7, 1] = jo["injection"]["sSP_IN08"][1].ToString();
            ((Injection)injection).injectionVelocity[8, 1] = jo["injection"]["sSP_IN09"][1].ToString();
            ((Injection)injection).injectionVelocity[9, 1] = jo["injection"]["sSP_IN10"][1].ToString();

            ((Injection)injection).injectionPosition[0, 1] = jo["injection"]["sPO_IN01"][1].ToString();
            ((Injection)injection).injectionPosition[1, 1] = jo["injection"]["sPO_IN02"][1].ToString();
            ((Injection)injection).injectionPosition[2, 1] = jo["injection"]["sPO_IN03"][1].ToString();
            ((Injection)injection).injectionPosition[3, 1] = jo["injection"]["sPO_IN04"][1].ToString();
            ((Injection)injection).injectionPosition[4, 1] = jo["injection"]["sPO_IN05"][1].ToString();
            ((Injection)injection).injectionPosition[5, 1] = jo["injection"]["sPO_IN06"][1].ToString();
            ((Injection)injection).injectionPosition[6, 1] = jo["injection"]["sPO_IN07"][1].ToString();
            ((Injection)injection).injectionPosition[7, 1] = jo["injection"]["sPO_IN08"][1].ToString();
            ((Injection)injection).injectionPosition[8, 1] = jo["injection"]["sPO_IN09"][1].ToString();
            ((Injection)injection).injectionPosition[9, 1] = jo["injection"]["sPO_IN10"][1].ToString();
            #endregion

            #region //carriage
            ((Carriage)carriage).carriageFwdPressure[0, 1] = jo["carriage"]["sPR_CAF1"][1].ToString();
            ((Carriage)carriage).carriageFwdPressure[1, 1] = jo["carriage"]["sPR_CAF2"][1].ToString();

            ((Carriage)carriage).carriageFwdVelocity[0, 1] = jo["carriage"]["sSP_CAF1"][1].ToString();
            ((Carriage)carriage).carriageFwdVelocity[1, 1] = jo["carriage"]["sSP_CAF2"][1].ToString();

            ((Carriage)carriage).carriageFwdPosition[0, 1] = jo["carriage"]["sPO_CAF1"][1].ToString();
            ((Carriage)carriage).carriageFwdPosition[1, 1] = jo["carriage"]["sPO_CAFW"][1].ToString();

            ((Carriage)carriage).carriageBwdPressure[0, 1] = jo["carriage"]["sPR_CAB1"][1].ToString();
            ((Carriage)carriage).carriageBwdPressure[1, 1] = jo["carriage"]["sPR_CAB2"][1].ToString();

            ((Carriage)carriage).carriageBwdVelocity[0, 1] = jo["carriage"]["sSP_CAB1"][1].ToString();
            ((Carriage)carriage).carriageBwdVelocity[1, 1] = jo["carriage"]["sSP_CAB2"][1].ToString();

            ((Carriage)carriage).carriageBwdPosition[0, 1] = jo["carriage"]["sPO_CAB1"][1].ToString();
            ((Carriage)carriage).carriageBwdPosition[1, 1] = jo["carriage"]["sPO_CABK"][1].ToString();
            #endregion

            #region //overView
           // ((OverView)overView).alarm[1] = jo["OperationMode1.sv_iSetAlarmSignalCount"][1].ToString();
            ((OverView)overView).productCounterAct[1] = jo["overView"]["TOTPR"][1].ToString();
            ((OverView)overView).productCounterSet[1] = jo["overView"]["sSW_CNPZ"][1].ToString();
            ((OverView)overView).productTimeAct[1] = jo["overView"]["PRODHR"][1].ToString();
            //((OverView)overView).productTimeSet[1] = jo["overView"]["TOTPR"][1].ToString();
            ((OverView)overView).lastCycleTime[1] = jo["overView"]["vCQ_TCYC"][1].ToString();
            //((OverView)overView).moldDataName[1] = jo["overView"]["TOTPR"][1].ToString();

            #endregion

            strMachineData = JsonConvert.SerializeObject((object)this);
        }

        public virtual void convertedFromHX(JObject jo, ref string strMachineData)
        {
            try
            {
                if (jo == null)
                    return;

                #region //clamp
                ((Clamp)clamp).moldClosePressure[0, 1] = jo["MoldsetMldCls"]["tmClpClsPress1"].ToString();
                ((Clamp)clamp).moldClosePressure[1, 1] = jo["MoldsetMldCls"]["tmClpClsPress2"].ToString();
                ((Clamp)clamp).moldClosePressure[2, 1] = jo["MoldsetMldCls"]["tmClpClsPress3"].ToString();
                ((Clamp)clamp).moldClosePressure[3, 1] = jo["MoldsetMldCls"]["tmClpClsPress4"].ToString();
                ((Clamp)clamp).moldClosePressure[4, 1] = jo["MoldsetMldCls"]["tmClpClsPress5"].ToString();

                ((Clamp)clamp).moldCloseVelocity[0, 1] = jo["MoldsetMldCls"]["tmClpClsSpeed1"].ToString();
                ((Clamp)clamp).moldCloseVelocity[1, 1] = jo["MoldsetMldCls"]["tmClpClsSpeed2"].ToString();
                ((Clamp)clamp).moldCloseVelocity[2, 1] = jo["MoldsetMldCls"]["tmClpClsSpeed3"].ToString();
                ((Clamp)clamp).moldCloseVelocity[3, 1] = jo["MoldsetMldCls"]["tmClpClsSpeed4"].ToString();
                ((Clamp)clamp).moldCloseVelocity[4, 1] = jo["MoldsetMldCls"]["tmClpClsSpeed5"].ToString();

                ((Clamp)clamp).moldClosePosition[0, 1] = jo["MoldsetMldCls"]["tmClpClsPosi1"].ToString();
                ((Clamp)clamp).moldClosePosition[1, 1] = jo["MoldsetMldCls"]["tmClpClsPosi2"].ToString();
                ((Clamp)clamp).moldClosePosition[2, 1] = jo["MoldsetMldCls"]["tmClpClsPosi3"].ToString();
                ((Clamp)clamp).moldClosePosition[3, 1] = jo["MoldsetMldCls"]["tmClpClsPosi4"].ToString();

                ((Clamp)clamp).moldOpenPressure[0, 1] = jo["MoldsetMldOpn"]["tmClpOpnPress1"].ToString();
                ((Clamp)clamp).moldOpenPressure[1, 1] = jo["MoldsetMldOpn"]["tmClpOpnPress2"].ToString();
                ((Clamp)clamp).moldOpenPressure[2, 1] = jo["MoldsetMldOpn"]["tmClpOpnPress3"].ToString();
                ((Clamp)clamp).moldOpenPressure[3, 1] = jo["MoldsetMldOpn"]["tmClpOpnPress4"].ToString();
                ((Clamp)clamp).moldOpenPressure[4, 1] = jo["MoldsetMldOpn"]["tmClpOpnPress5"].ToString();

                ((Clamp)clamp).moldOpenVelocity[0, 1] = jo["MoldsetMldOpn"]["tmClpOpnSpeed1"].ToString();
                ((Clamp)clamp).moldOpenVelocity[1, 1] = jo["MoldsetMldOpn"]["tmClpOpnSpeed2"].ToString();
                ((Clamp)clamp).moldOpenVelocity[2, 1] = jo["MoldsetMldOpn"]["tmClpOpnSpeed3"].ToString();
                ((Clamp)clamp).moldOpenVelocity[3, 1] = jo["MoldsetMldOpn"]["tmClpOpnSpeed4"].ToString();
                ((Clamp)clamp).moldOpenVelocity[4, 1] = jo["MoldsetMldOpn"]["tmClpOpnSpeed5"].ToString();

                ((Clamp)clamp).moldOpenPosition[0, 1] = jo["MoldsetMldOpn"]["tmClpOpnPosi1"].ToString();
                ((Clamp)clamp).moldOpenPosition[1, 1] = jo["MoldsetMldOpn"]["tmClpOpnPosi2"].ToString();
                ((Clamp)clamp).moldOpenPosition[2, 1] = jo["MoldsetMldOpn"]["tmClpOpnPosi3"].ToString();
                ((Clamp)clamp).moldOpenPosition[3, 1] = jo["MoldsetMldOpn"]["tmClpOpnPosi4"].ToString();
                #endregion
                #region //ejector
                ((Ejector)ejector).ejectorFwdPressure[0, 1] = jo["MoldsetEject"]["tmEjectAdvPress1"].ToString();
                ((Ejector)ejector).ejectorFwdPressure[1, 1] = jo["MoldsetEject"]["tmEjectAdvPress2"].ToString();

                ((Ejector)ejector).ejectorFwdVelocity[0, 1] = jo["MoldsetEject"]["tmEjectAdvSpeed1"].ToString();
                ((Ejector)ejector).ejectorFwdVelocity[1, 1] = jo["MoldsetEject"]["tmEjectAdvSpeed2"].ToString();

                ((Ejector)ejector).ejectorFwdPosition[0, 1] = jo["MoldsetEject"]["tmEjectAdvPosi1"].ToString();
                ((Ejector)ejector).ejectorFwdPosition[1, 1] = jo["MoldsetEject"]["tmEjectAdvPosi2"].ToString();

                ((Ejector)ejector).ejectorBwdPressure[0, 1] = jo["MoldsetEject"]["tmEjectRetPress1"].ToString();
                ((Ejector)ejector).ejectorBwdPressure[1, 1] = jo["MoldsetEject"]["tmEjectRetPress2"].ToString();

                ((Ejector)ejector).ejectorBwdVelocity[0, 1] = jo["MoldsetEject"]["tmEjectRetSpeed1"].ToString();
                ((Ejector)ejector).ejectorBwdVelocity[1, 1] = jo["MoldsetEject"]["tmEjectRetSpeed2"].ToString();

                ((Ejector)ejector).ejectorBwdPosition[0, 1] = jo["MoldsetEject"]["tmEjectRetPosi1"].ToString();
                ((Ejector)ejector).ejectorFwdPosition[1, 1] = jo["MoldsetEject"]["tmEjectRetPosi2"].ToString();

                #endregion
                #region //core









                ((Core)core).coreInPressure[0, 1] = jo["MoldsetCore"]["tmCoreAInPress"].ToString();
                ((Core)core).coreInPressure[1, 1] = jo["MoldsetCore"]["tmCoreBInPress"].ToString();
                ((Core)core).coreInPressure[2, 1] = jo["MoldsetCore"]["tmCoreCInPress"].ToString();

                ((Core)core).coreInVelocity[0, 1] = jo["MoldsetCore"]["tmCoreAInSpeed"].ToString();
                ((Core)core).coreInVelocity[1, 1] = jo["MoldsetCore"]["tmCoreBInSpeed"].ToString();
                ((Core)core).coreInVelocity[2, 1] = jo["MoldsetCore"]["tmCoreCInSpeed"].ToString();

                ((Core)core).coreInMonitorPosition[0, 1] = jo["MoldsetCore"]["tmCoreAInPosi"].ToString();
                ((Core)core).coreInMonitorPosition[1, 1] = jo["MoldsetCore"]["tmCoreBInPosi"].ToString();
                ((Core)core).coreInMonitorPosition[2, 1] = jo["MoldsetCore"]["tmCoreCInPosi"].ToString();

                ((Core)core).coreInSetMoveTime[0, 1] = jo["MoldsetCore"]["tmCoreAInTime"].ToString();
                ((Core)core).coreInSetMoveTime[1, 1] = jo["MoldsetCore"]["tmCoreBInTime"].ToString();
                ((Core)core).coreInSetMoveTime[2, 1] = jo["MoldsetCore"]["tmCoreCInTime"].ToString();

                ((Core)core).coreOutPressure[0, 1] = jo["MoldsetCore"]["tmCoreAOutPress"].ToString();
                ((Core)core).coreOutPressure[1, 1] = jo["MoldsetCore"]["tmCoreBOutPress"].ToString();
                ((Core)core).coreOutPressure[2, 1] = jo["MoldsetCore"]["tmCoreCOutPress"].ToString();

                ((Core)core).coreOutVelocity[0, 1] = jo["MoldsetCore"]["tmCoreAOutSpeed"].ToString();
                ((Core)core).coreOutVelocity[1, 1] = jo["MoldsetCore"]["tmCoreBOutSpeed"].ToString();
                ((Core)core).coreOutVelocity[2, 1] = jo["MoldsetCore"]["tmCoreCOutSpeed"].ToString();


                ((Core)core).coreOutMonitorPosition[0, 1] = jo["MoldsetCore"]["tmCoreAOutPosi"].ToString();
                ((Core)core).coreOutMonitorPosition[1, 1] = jo["MoldsetCore"]["tmCoreBOutPosi"].ToString();
                ((Core)core).coreOutMonitorPosition[2, 1] = jo["MoldsetCore"]["tmCoreCOutPosi"].ToString();

                ((Core)core).coreOutSetMoveTime[0, 1] = jo["MoldsetCore"]["tmCoreAOutTime"].ToString();
                ((Core)core).coreOutSetMoveTime[1, 1] = jo["MoldsetCore"]["tmCoreBOutTime"].ToString();
                ((Core)core).coreOutSetMoveTime[2, 1] = jo["MoldsetCore"]["tmCoreCOutTime"].ToString();


                #endregion

                #region //injection
                ((Injection)injection).injectionPressure[0, 1] = jo["MoldsetInject"]["tmInjPress1"].ToString();
                ((Injection)injection).injectionPressure[1, 1] = jo["MoldsetInject"]["tmInjPress2"].ToString();
                ((Injection)injection).injectionPressure[2, 1] = jo["MoldsetInject"]["tmInjPress3"].ToString();
                ((Injection)injection).injectionPressure[3, 1] = jo["MoldsetInject"]["tmInjPress4"].ToString();
                ((Injection)injection).injectionPressure[4, 1] = jo["MoldsetInject"]["tmInjPress5"].ToString();
                ((Injection)injection).injectionPressure[5, 1] = jo["MoldsetInject"]["tmInjPress6"].ToString();


                ((Injection)injection).injectionVelocity[0, 1] = jo["MoldsetInject"]["tmInjSpeed1"].ToString();
                ((Injection)injection).injectionVelocity[1, 1] = jo["MoldsetInject"]["tmInjSpeed2"].ToString();
                ((Injection)injection).injectionVelocity[2, 1] = jo["MoldsetInject"]["tmInjSpeed3"].ToString();
                ((Injection)injection).injectionVelocity[3, 1] = jo["MoldsetInject"]["tmInjSpeed4"].ToString();
                ((Injection)injection).injectionVelocity[4, 1] = jo["MoldsetInject"]["tmInjSpeed5"].ToString();
                ((Injection)injection).injectionVelocity[5, 1] = jo["MoldsetInject"]["tmInjSpeed6"].ToString();


                ((Injection)injection).injectionPosition[0, 1] = jo["MoldsetInject"]["tmInjPosi1"].ToString();
                ((Injection)injection).injectionPosition[1, 1] = jo["MoldsetInject"]["tmInjPosi2"].ToString();
                ((Injection)injection).injectionPosition[2, 1] = jo["MoldsetInject"]["tmInjPosi3"].ToString();
                ((Injection)injection).injectionPosition[3, 1] = jo["MoldsetInject"]["tmInjPosi4"].ToString();
                ((Injection)injection).injectionPosition[4, 1] = jo["MoldsetInject"]["tmInjPosi5"].ToString();
                ((Injection)injection).injectionPosition[5, 1] = jo["MoldsetInject"]["tmInjPosi6"].ToString();
  
                #endregion

                #region //carriage
                #endregion

                #region //overView
                ((OverView)overView).alarm[1] = jo["Alarm"]["tmAlarmState"].ToString();
                ((OverView)overView).productCounterAct[1] = jo["Basic"]["tmFinishCount"].ToString();
                ((OverView)overView).productCounterSet[1] = jo["Basic"]["tmPlanCount"].ToString();
               // ((OverView)overView).productTimeAct[1] = jo["Basic"]["PRODHR"][1].ToString();
                //((OverView)overView).productTimeSet[1] = jo["overView"]["TOTPR"][1].ToString();
                ((OverView)overView).lastCycleTime[1] = jo["Monitor"]["tmCycleTime"].ToString();
                ((OverView)overView).moldDataName[1] = jo["Basic"]["tmCraftID"].ToString();

                #endregion
                strMachineData = JsonConvert.SerializeObject((object)this);
            }
            catch
            {
                MessageBox.Show("宏讯数据转换失败");
            }
            
        }
        public static void getQualityDataFromKeba(JObject jo, ref string qualityData) {

            QualityData qd = new QualityData();
            qd.moldDataName[1] = jo["system.sv_sMoldData"][1].ToString();
            qd.systemShotCounterAct[1] = jo["system.sv_iShotCounterAct"][1].ToString();
            qd.injection1CutOffPosition[1] = jo["Injection1.sv_rCutOffPosition"][1].ToString();   //重复
            qd.injection1Cushion[1] = jo["Injection1.sv_rCushion"][1].ToString();        //重复
            qd.injection1ActMoveTime[1] = jo["Injection1.sv_InjectTimesAct.dActMoveTime"][1].ToString();
            qd.injection1CutOffPressure[1] = jo["Injection1.sv_rCutOffPressure"][1].ToString();
            qd.systemCycleTimeMachine[1] = jo["system.sv_dCycleTimeMachine"][1].ToString();
            qd.systemShotTimeAct[1] = jo["system.sv_dShotTimeAct"][1].ToString();
            qd.plastActMoveTime[1] = jo["Injection1.sv_PlastTimesAct.dActMoveTime"][1].ToString();
            qd.injection1PlastEndPosition[1] = jo["Injection1.sv_rPlastEndPosition"][1].ToString();
            qd.injection1InjPeakPressure[1] = jo["Injection1.sv_rInjPeakPressure"][1].ToString();
            qualityData = JsonConvert.SerializeObject(qd);
        }
        public static void getQualityDataFromGefranPerforma(JObject jo, ref string qualityData)
        {

            QualityData qd = new QualityData();
            qd.systemShotCounterAct[1] = jo["overView"]["CURRENT_TOTAL_PRODUCTION"][1].ToString();
           // qd.injection1CutOffPosition[1] = jo["qualityData"]["vCQ_PHOL"][1].ToString();   //保压切换位置
            qd.injection1Cushion[1] = jo["qualityData"]["CUSH_POS_DISP"][1].ToString();        //螺杆终点
            qd.injection1ActMoveTime[1] = jo["qualityData"]["INJ_FILL_T"][1].ToString();//注射时间
            //qd.injection1CutOffPressure[1] = jo["qualityData"]["Injection1.sv_rCutOffPressure"][1].ToString();//切换压力
            qd.systemCycleTimeMachine[1] = jo["overView"]["CYCLE_DURATION"][1].ToString();//周期
            //qd.systemShotTimeAct[1] = jo["qualityData"]["vCQ_TCHA"][1].ToString();
            qd.plastActMoveTime[1] = jo["qualityData"]["CHG_TIME"][1].ToString();//熔胶时间
            qd.injection1PlastEndPosition[1] = jo["qualityData"]["FINAL_RECOVERY_POS"][1].ToString();//熔胶终点
            qd.injection1InjPeakPressure[1] = jo["qualityData"]["MAX_FILL_PRS"][1].ToString();//最大射压
            qualityData = JsonConvert.SerializeObject(qd);
        }

        public static void getQualityDataFromGefranVedo(JObject jo, ref string qualityData)
        {
            QualityData qd = new QualityData();
            qd.systemShotCounterAct[1] = jo["overView"]["TOTPR"][1].ToString();
            qd.injection1CutOffPosition[1] = jo["qualityData"]["vCQ_PHOL"][1].ToString();   //保压切换位置
            qd.injection1Cushion[1] = jo["qualityData"]["vCQ_CUSC"][1].ToString();        //螺杆终点
            qd.injection1ActMoveTime[1] = jo["qualityData"]["vCQ_TFIL"][1].ToString();//注射时间
            //qd.injection1CutOffPressure[1] = jo["qualityData"]["Injection1.sv_rCutOffPressure"][1].ToString();//切换压力
            qd.systemCycleTimeMachine[1] = jo["qualityData"]["vCQ_TCYC"][1].ToString();//周期
            //qd.systemShotTimeAct[1] = jo["qualityData"]["vCQ_TCHA"][1].ToString();
            qd.plastActMoveTime[1] = jo["qualityData"]["vCQ_TCHA"][1].ToString();//熔胶时间
            qd.injection1PlastEndPosition[1] = jo["qualityData"]["vCQ_PCHA"][1].ToString();//熔胶终点
            qd.injection1InjPeakPressure[1] = jo["qualityData"]["vCQ_MXFI"][1].ToString();//最大射压
            qualityData = JsonConvert.SerializeObject(qd);
        }

        public static void getQualityDataFromHX(JObject jo, ref string qualityData)
        {
            QualityData qd = new QualityData();
            qd.systemShotCounterAct[1] = jo["Basic"]["tmShotCount"].ToString();
            qd.injection1CutOffPosition[1] = jo["Monitor"]["tmTurnPosi"].ToString();   //保压切换位置
           // qd.injection1Cushion[1] = jo["qualityData"]["vCQ_CUSC"][1].ToString();        //螺杆终点
            qd.injection1ActMoveTime[1] = jo["Monitor"]["tmInjTime"][1].ToString();//注射时间
            qd.injection1CutOffPressure[1] = jo["Monitor"]["tmTurnPress"][1].ToString();//切换压力
            qd.systemCycleTimeMachine[1] = jo["Monitor"]["tmCycleTime"][1].ToString();//周期
            //qd.systemShotTimeAct[1] = jo["qualityData"]["vCQ_TCHA"][1].ToString();
            qd.plastActMoveTime[1] = jo["Monitor"]["tmChargeTime"][1].ToString();//熔胶时间
            qd.injection1PlastEndPosition[1] = jo["Monitor"]["tmInjStartPosi"].ToString();//熔胶终点
           // qd.injection1InjPeakPressure[1] = jo["qualityData"]["vCQ_MXFI"][1].ToString();//最大射压
            qualityData = JsonConvert.SerializeObject(qd);
        }



    }
    #region father
    class Clamp {
        public string[] moldCloseNoOfPoints = new string[2];
        public string[,] moldCloseVelocity = new string[10,2];
        public string[,] moldClosePressure = new string[10,2];
        public string[,] moldClosePosition = new string[10,2];
        public string[] moldOpenNoOfPoints = new string[2];
        public string[,] moldOpenVelocity = new string[10,2];
        public string[,] moldOpenPressure = new string[10,2];
        public string[,] moldOpenPosition = new string[10,2];
        public string[] moldFastClose = new string[2];
        public string[] useOpenVavleAsFastClose = new string[2];
        public string[] moldFastOpen = new string[2];
        public string[] cycleDelaySet = new string[2];
        public string[] cycleDelayAct = new string[2];
        public string[] moldProtectTimeSet = new string[2];     
        public string[] moldProtectTimeAct = new string[2];
    }
    class OverView {
        public string[] alarm = new string[2];
        public string[] productCounterAct = new string[2];
        public string[] productCounterSet = new string[2];
        public string[] productTimeAct = new string[2];
        public string[] productTimeSet = new string[2];
        public string[] lastCycleTime = new string[2];
        public string[] moldDataName = new string[2];
        public string[] moldPosition = new string[2];
        public string[] ejectorPosition = new string[2];
        public string[] injectionPosition = new string[2];
        public string[] carriagePosition = new string[2];
        //public string[] pump1Velocity = new string[2];
        //public string[] pump1Pressure = new string[2];
        //public string[] pump2Velocity = new string[2];
        //public string[] pump2Pressure = new string[2];
        public string[] backPressure = new string[2];
        public string[] actScrewRpm = new string[2];

    }
    class Ejector {
        public string[] ejectorFwdNoOfPoints = new string[2];
        public string[] ejectorBwdNoOfPoints = new string[2];
        public string[] ejectorMode = new string[2];
        public string[] ejectorShakeCounter = new string[2];

        public string[,] ejectorFwdVelocity = new string[10, 2];
        public string[,] ejectorFwdPressure = new string[10, 2];
        public string[,] ejectorFwdPosition = new string[10, 2];
        public string[,] ejectorBwdVelocity = new string[10, 2];
        public string[,] ejectorBwdPressure = new string[10, 2];
        public string[,] ejectorBwdPosition = new string[10, 2];
        public string[,] airVavleMode = new string[10, 2];
        public string[,] airVavleStartPosition = new string[10, 2];
        public string[,] airVavleSetDelayTime = new string[10, 2];
        public string[,] airVavleSetMoveTime = new string[10, 2];

    }
    class Core
    {
        public string[,] coreUse = new string[4, 2];
        public string[,] coreOutMode = new string[4, 2];
        public string[,] coreInMode = new string[4, 2];
        public string[,] coreHold = new string[4, 2];            //11.27 coreInHold 修改为coreHold
        public string[,] coreInControlMode = new string[4, 2];   //time or limit switch
        public string[,] coreOutControlMode = new string[4, 2];
        public string[,] coreInVelocity = new string[4, 2];
        public string[,] coreOutVelocity = new string[4, 2];
        public string[,] coreInPressure = new string[4, 2];
        public string[,] coreOutPressure = new string[4, 2];
        public string[,] coreInActPosition = new string[4, 2];
        public string[,] coreOutActPosition = new string[4, 2];
        public string[,] coreInMonitorPosition = new string[4, 2];
        public string[,] coreOutMonitorPosition = new string[4, 2];
        public string[,] coreInActMoveTime = new string[4, 2];
        public string[,] coreOutActMoveTime = new string[4, 2];
        public string[,] coreInSetMoveTime = new string[4, 2];
        public string[,] coreOutSetMoveTime = new string[4, 2];
        public string[,] coreInSetScrewCount = new string[4, 2];
        public string[,] coreOutSetScrewCount = new string[4, 2];
        public string[,] coreInActScrewCount = new string[4, 2];
        public string[,] coreOutActScrewCount = new string[4, 2];
        public string[,] coreInPriority = new string[4, 2];
        public string[,] coreOutPriority = new string[4, 2];
    }

    class Injection
    {
        public string[] injectionNoOfPoints = new string[2];
        public string[,] injectionVelocity = new string[10,2];
        public string[,] injectionPressure = new string[10, 2];
        public string[,] injectionPosition = new string[10, 2];
        public string[] cutOffUseScrewPosition = new string[2];
        public string[] cutOffScrewPosition = new string[2];
        public string[] cutOffScrewThresholdPosition = new string[2];

        public string[] cutOffUseInjectTime = new string[2];
        public string[] cutOffInjectTime = new string[2];
        public string[] cutOffThresholdInjectTime = new string[2];

        public string[] cutOffUseInjectPressure = new string[2];
        public string[] cutOffInjectPressure = new string[2];
        public string[] cutOffThresholdInjectPressure = new string[2];

        public string[] injectionHoldNoOfPoints = new string[2];     //11.7 增加
        public string[,] injectionHoldVelocity = new string[10, 2];
        public string[,] injectionHoldPressure = new string[10, 2];
        public string[,] injectionHoldPosition = new string[10, 2];

        public string[] injectionActPressure = new string[2];
        public string[] screwPosition = new string[2];
        public string[] actCoolingTime = new string[2];
        public string[] setCoolingTime = new string[2];
    } 
    class Plasticize
    {
        public string[]  plasticizeNoOfPoints = new string[2];
        public string[,] plasticizeVelocity = new string[10, 2];
        public string[,] plasticizePressure = new string[10, 2];
        public string[,] plasticizePosition = new string[10, 2];
        public string[]  beforePlasticizeMode = new string[2];
        public string[] beforePlasticizePressure = new string[2];
        public string[] beforePlasticizeVelocity = new string[2];
        public string[] beforePlasticizePosition = new string[2];
        public string[] beforePlasticizeTime = new string[2];
        public string[] afterPlasticizeMode = new string[2];
        public string[] afterPlasticizePressure = new string[2];
        public string[] afterPlasticizeVelocity = new string[2];
        public string[] afterPlasticizePosition = new string[2];
        public string[] afterPlasticizeTime = new string[2];

    }
    class Carriage
    {
        public string[] carriageFwdNoOfPoints = new string[2];
        public string[,] carriageFwdVelocity = new string[10, 2];
        public string[,] carriageFwdPressure = new string[10, 2];
        public string[,] carriageFwdPosition = new string[10, 2];
        public string[] carriageBwdNoOfPoints = new string[2];
        public string[,] carriageBwdVelocity = new string[10, 2];
        public string[,] carriageBwdPressure = new string[10, 2];
        public string[,] carriageBwdPosition = new string[10, 2];
        public string[] carriageBwdMode = new string[2];

    }
    class QualityData
    {
        public string[] systemShotCounterAct = new string[2];
        public string[] moldDataName = new string[2];  //重复
        public string[] injection1CutOffPosition = new string[2];  //重复
        public string[] injection1Cushion = new string[2];         //重复
        public string[] injection1ActMoveTime = new string[2];     //重复
        public string[] injection1CutOffPressure = new string[2];
        public string[] systemCycleTimeMachine = new string[2];
        public string[] systemShotTimeAct = new string[2];
        public string[] plastActMoveTime = new string[2];
        public string[] injection1PlastEndPosition = new string[2];
        public string[] injection1InjPeakPressure = new string[2];
    }
    //class testClass
    //{
    //    public string[] aPO_INJE = new string[2];
    //    public string[,] DeadBand =new string[10,2]; 
    //}
    #endregion 
    

    
       
}
