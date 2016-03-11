using System;
using System.Threading;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApplication1
{
    class OpcHelper
    {
        public Process thisprocess;			// running OS process
        public string selectedOpcSrv;			// Name (ProgID) of selected OPC-server
        public OpcServer theSrv = null;			// root OPCDA object
        public OpcGroup theGrp = null;			// the only one OPC-Group in this example
        public int itmHandleClient;			// 0 if no current item selected
        public int itmHandleServer;         
        public OPCACCESSRIGHTS itmAccessRights;
        public TypeCode itmTypeCode;				// saved data type of current item
        public bool first_activated = false;	// workaround to show SelServer Form on applic. start
        public bool opc_connected = false;		// flag if connected
        public string rootname = "Root";			// string of TreeView root (dummy)
        public List<string[]> itemsValue = new List<string[]>();   //all items value
        public JObject itemsJO = new JObject();   //all items JOBJECT
        public List<string> nameMap = new List<string>();   //all items value

        protected void theSrv_ServerShutDown(object sender, ShutdownRequestEventArgs e)
        {					
        }

        public bool DoInit()
        {
            try
            {
                thisprocess = Process.GetCurrentProcess();		// see DoConnect for client-name
                SelServer frmSelSrv = new SelServer();		// create form and let user select a name
                frmSelSrv.ShowDialog();
                if (frmSelSrv.selectedOpcSrv == null)
                    frmSelSrv.Close();
                selectedOpcSrv = frmSelSrv.selectedOpcSrv;			// OPC server ProgID
                // ---------------
                theSrv = new OpcServer();
                if (!DoConnect(selectedOpcSrv))
                    return false;

                // add event handler for server shutdown
                theSrv.ShutdownRequested += new ShutdownRequestEventHandler(this.theSrv_ServerShutDown);

                // precreate the only OPC group in this example
                if (!CreateGroup())
                    return false;

                // browse the namespace of the OPC-server
                if (!DoBrowse())
                    return false;
            }
            catch (Exception e)		// exceptions MUST be handled
            {
                return false;
            }
            return true;
        }

        // connect to OPC server via ProgID
        public bool DoConnect(string progid)
        {
            try
            {
                theSrv.Connect(progid);
                Thread.Sleep(100);
                theSrv.SetClientName("DirectOPC " + thisprocess.Id);	// set my client name (exe+process no)

                SERVERSTATUS sts;
                theSrv.GetStatus(out sts);
                // get infos about OPC server
                StringBuilder sb = new StringBuilder(sts.szVendorInfo, 200);
                sb.AppendFormat(" ver:{0}.{1}.{2}", sts.wMajorVersion, sts.wMinorVersion, sts.wBuildNumber);
            }
            catch (COMException)
            {
                return false;
            }
            return true;
        }


        public bool CreateGroup()
        {
            try
            {
                // add our only working group
                theGrp = theSrv.AddGroup("OPCdotNET-Group", true, 500);

                // add event handler for data changes
                theGrp.DataChanged += new DataChangeEventHandler(this.theGrp_DataChange);
                theGrp.WriteCompleted += new WriteCompleteEventHandler(this.theGrp_WriteComplete);
            }
            catch (COMException)
            {
                return false;
            }
            return true;
        }
        delegate void SetValueCallback(string value);
        delegate void SetQulityCallback(string qulity);
        delegate void SetTimestampCallback(string timestamp);

        // event handler: called if any item in group has changed values
        protected void theGrp_DataChange(object sender, DataChangeEventArgs e)
        {
            foreach (OPCItemState s in e.sts)
            {
                if (HRESULTS.Succeeded(s.Error))
                {
                    string value = s.DataValue==null ?null: s.DataValue.ToString();
                    Trace.WriteLine("  val=" + value);
                    //将改变的值给对应Item
                   // itemsValue[s.HandleClient][1] = s.DataValue.ToString();   
                    string [] name = nameMap[s.HandleClient].Split('.');
                    if (s.HandleClient >= 58)
                        name[2] = name[2];
                    itemsJO[name[0]][name[1]][name[2]] = value; 


                }
            }
        }
        protected void theGrp_WriteComplete(object sender, WriteCompleteEventArgs e)
        {
            foreach (OPCWriteResult w in e.res)
            {
                if (w.HandleClient != itmHandleClient)		// only one client handle
                    continue;

            }
        }

        public bool DoBrowse()
        {
            try
            {
                OPCNAMESPACETYPE opcorgi = theSrv.QueryOrganization();

                // fill TreeView with all
                TreeNode tnRoot = new TreeNode(rootname, 0, 1);
                if (opcorgi == OPCNAMESPACETYPE.OPC_NS_HIERARCHIAL)
                {
                    theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_TO, "");	// to root
                    RecurBrowse(tnRoot, 1,"",ref itemsJO);
                }
                tnRoot.ExpandAll();			// expand all nodes ([+] -> [-])
                tnRoot.EnsureVisible();		// make the root visible
            }
            catch (COMException /* eX */ )
            {
                return false;
            }
            return true;
        }

        // recursively call the OPC namespace tree
        public bool RecurBrowse(TreeNode tnParent, int depth, string name,ref JObject childrenJO)
        {
            try
            {
                ArrayList lst;
                string tempStr = "";
                theSrv.Browse(OPCBROWSETYPE.OPC_BRANCH, out lst);
                if (lst == null)
                    return true;
                if (lst.Count < 1)
                    return true;
                JObject joTmp = new JObject();

                foreach (string s in lst)
                {
                    TreeNode tnNext = new TreeNode(s, 0, 1);
                    theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_DOWN, s);

                    string tmpName = (name == "" ? tnNext.Text : name + "." + tnNext.Text);
                    JObject chJO=new JObject();

                    RecurBrowse(tnNext, depth + 1, tmpName,ref chJO);
                    if (tnParent.Text == "Root")
                        theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_TO, "");
                    else
                        theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_TO, tnParent.Text);

                    childrenJO.Add(tnNext.Text, chJO);
                    //叶子节点的时候存储全路径节点名
                    if (tnNext.FirstNode == null)
                    {
                        string[] strTemp = new string[2];
                        strTemp[0] = tmpName;
                        strTemp[1] = "1";
                        itemsValue.Add(strTemp);
                        nameMap.Add(tmpName);
                    }
                    tnParent.Nodes.Add(tnNext);
                }


            }
            catch (COMException eX)
            {
                Console.WriteLine(eX.ToString());
                return false;
            }
            return true;
        }

       
        public bool RemoveItem()
        {
            try
            {
                if (itmHandleClient != 0)
                {
                    itmHandleClient = 0;

                    int[] serverhandles = new int[1] { itmHandleServer };
                    int[] remerrors;
                    theGrp.RemoveItems(serverhandles, out remerrors);
                    itmHandleServer = 0;
                }
            }
            catch (COMException)
            {
                return false;
            }
            return true;
        }


        public TypeCode VT2TypeCode(VarEnum vevt)
        {
            switch (vevt)
            {
                case VarEnum.VT_I1:
                    return TypeCode.SByte;
                case VarEnum.VT_I2:
                    return TypeCode.Int16;
                case VarEnum.VT_I4:
                    return TypeCode.Int32;
                case VarEnum.VT_I8:
                    return TypeCode.Int64;

                case VarEnum.VT_UI1:
                    return TypeCode.Byte;
                case VarEnum.VT_UI2:
                    return TypeCode.UInt16;
                case VarEnum.VT_UI4:
                    return TypeCode.UInt32;
                case VarEnum.VT_UI8:
                    return TypeCode.UInt64;

                case VarEnum.VT_R4:
                    return TypeCode.Single;
                case VarEnum.VT_R8:
                    return TypeCode.Double;

                case VarEnum.VT_BSTR:
                    return TypeCode.String;
                case VarEnum.VT_BOOL:
                    return TypeCode.Boolean;
                case VarEnum.VT_DATE:
                    return TypeCode.DateTime;
                case VarEnum.VT_DECIMAL:
                    return TypeCode.Decimal;
                case VarEnum.VT_CY:				// not supported
                    return TypeCode.Double;
            }

            return TypeCode.Object;
        }

        public void getValueFormHX()
        {
            try
            {
                DoInit();
                int itemsValueCount = nameMap.Count;
                OPCItemDef[] aD = new OPCItemDef[itemsValueCount];

                int i = 0;
                foreach (string key in nameMap) {
                    if (key == "hongxun001.Monitor.tmInjTime")
                        i = i;
                        aD[i] = new OPCItemDef(key, true, i, VarEnum.VT_EMPTY);
                        i++;
                }
                
                //将节点添加到group中
                //for (int i = 0; i < itemsValueCount; i++)
                //{
                //    itmHandleClient = i;

                //    aD[i] = new OPCItemDef(itemsValue[i][0], true, itmHandleClient, VarEnum.VT_EMPTY);
                //}
                SERVERSTATUS status;
                theSrv.GetStatus(out status);
                OPCItemResult[] arrRes;
                theGrp.AddItems(aD, out arrRes);
                if (arrRes == null)
                    return;
                if (arrRes[0].Error != HRESULTS.S_OK)
                    return;

                itmHandleServer = arrRes[0].HandleServer;
                itmAccessRights = arrRes[0].AccessRights;
                itmTypeCode = VT2TypeCode(arrRes[0].CanonicalDataType);

                if ((itmAccessRights & OPCACCESSRIGHTS.OPC_READABLE) != 0)
                {
                    int cancelID;
                    theGrp.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE, 7788, out cancelID);
                }
            }
            catch (COMException)
            {
                return;
            }
        }

        public void OpcClosing()
        {
            if (!opc_connected)
                return;

            if (theGrp != null)
            {
                theGrp.DataChanged -= new DataChangeEventHandler(this.theGrp_DataChange);
                theGrp.WriteCompleted -= new WriteCompleteEventHandler(this.theGrp_WriteComplete);
                RemoveItem();
                theGrp.Remove(false);
                theGrp = null;
            }

            if (theSrv != null)
            {
                theSrv.Disconnect();				// should clean up
                theSrv = null;
            }

            opc_connected = false;
        }


    }
}
