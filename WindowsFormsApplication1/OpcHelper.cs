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

namespace WindowsFormsApplication1
{
    class OpcHelper
    {
        public Process thisprocess;			// running OS process
        public string selectedOpcSrv;			// Name (ProgID) of selected OPC-server
        public OpcServer theSrv = null;			// root OPCDA object
        public OpcGroup theGrp = null;			// the only one OPC-Group in this example
        public string itmFullID;					// fully qualified OPC namespace path
        public int itmHandleClient;			// 0 if no current item selected
        public int itmHandleServer;
        public OPCACCESSRIGHTS itmAccessRights;
        public TypeCode itmTypeCode;				// saved data type of current item
        public bool first_activated = false;	// workaround to show SelServer Form on applic. start
        public bool opc_connected = false;		// flag if connected
        public string rootname = "Root";			// string of TreeView root (dummy)
        public string selectednode;
        private TextBox showValue;
        private Button getValue;
        private ColumnHeader hdrListCol3;
        public string selecteditem;				// item in ListView
        public List<string[]> itemsValue = new List<string[]>();   //all items value

        protected void theSrv_ServerShutDown(object sender, ShutdownRequestEventArgs e)
        {					// event: the OPC server shuts down
            //MessageBox.Show(this, "OPC server shuts down because:" + e.shutdownReason, "ServerShutDown", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
               // MessageBox.Show(this, "init error! " + e.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //txtServerInfo.Text = sb.ToString();

                // set status bar text to show server state
                //sbpTimeStart.Text = DateTime.FromFileTime(sts.ftStartTime).ToString();
                //sbpStatus.Text = sts.eServerState.ToString();
            }
            catch (COMException)
            {
                //MessageBox.Show(this, "connect error!", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                //MessageBox.Show(this, "create group error!", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            Trace.WriteLine("theGrp_DataChange  id=" + e.transactionID.ToString() + " me=0x" + e.masterError.ToString("X"));
            string[] name = { "192_168_1_210.Alarm.tmAlarmState", "192_168_1_210.Temperature.tmTemp1_Set" };
            int i = 0;

            int[] ids;
            this.theSrv.QueryAvailableLocaleIDs(out ids);
            foreach (OPCItemState s in e.sts)
            {
              //  if (s.HandleClient != itmHandleClient)		// only one client handle
              //      continue;

                Trace.WriteLine("  item error=0x" + s.Error.ToString("X"));

                if (HRESULTS.Succeeded(s.Error))
                {
                    Trace.WriteLine("  val=" + s.DataValue.ToString());
                    // value += name[i]+":" + s.DataValue.ToString()+";\n";
                    itemsValue[s.HandleClient][1] = s.DataValue.ToString();
                    
                    i++;
                    // string qulity = OpcGroup.QualityToString(s.Quality);
                    // string timestamp = DateTime.FromFileTime( s.TimeStamp ).ToString();
                    //  SetValue(value);
                    //  SetQulity(qulity);
                    //   SetTimestamp(timestamp);
                    //txtItemValue.Text = temp;		// update screen
                    //txtItemQual.Text	= OpcGroup.QualityToString( s.Quality );
                    //txtItemTimeSt.Text  = DateTime.FromFileTime( s.TimeStamp ).ToString();
                }
                else
                {
                    //txtItemValue.Text = "ERROR 0x" + s.Error.ToString("X");
                    //txtItemQual.Text = "error";
                    //txtItemTimeSt.Text = "error";
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
                //treeOpcItems.Nodes.Clear();
                TreeNode tnRoot = new TreeNode(rootname, 0, 1);
                if (opcorgi == OPCNAMESPACETYPE.OPC_NS_HIERARCHIAL)
                {
                    theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_TO, "");	// to root
                    RecurBrowse(tnRoot, 1);
                }
                //treeOpcItems.Nodes.Add(tnRoot);

                tnRoot.ExpandAll();			// expand all nodes ([+] -> [-])
                tnRoot.EnsureVisible();		// make the root visible

                // preselect root (dummy)
                //treeOpcItems.SelectedNode = tnRoot;		// force treeOpcItems_AfterSelect
            }
            catch (COMException /* eX */ )
            {
                //MessageBox.Show(this, "browse error!", "DoBrowse", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // recursively call the OPC namespace tree
        public bool RecurBrowse(TreeNode tnParent, int depth, string name = "")
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
                foreach (string s in lst)
                {
                    if (s == "192_168_1_211")
                        tempStr = s;
                    TreeNode tnNext = new TreeNode(s, 0, 1);
                    theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_DOWN, s);

                    string tmpName = (name == "" ? tnNext.Text : name + "." + tnNext.Text);
                    RecurBrowse(tnNext, depth + 1, tmpName);
                    if (tnParent.Text == "Root")
                        theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_TO, "");
                    else
                        theSrv.ChangeBrowsePosition(OPCBROWSEDIRECTION.OPC_BROWSE_TO, tnParent.Text);
                    if (tnNext.FirstNode == null)
                    {

                        string[] strTemp = new string[2];
                        strTemp[0] = tmpName;
                        strTemp[1] = "1";
                        itemsValue.Add(strTemp);
                    }

                    //TreeNode previous = tnParent;

                    tnParent.Nodes.Add(tnNext);
                }


            }
            catch (COMException eX)
            {
                Console.WriteLine(eX.ToString());
                //MessageBox.Show(this, "browse error!", "RecurBrowse", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        public bool ViewItem(string opcid)
        {
            try
            {
                RemoveItem();		// first remove previous item if any
                itmHandleClient = 1234;
                OPCItemDef[] aD = new OPCItemDef[1];
                aD[0] = new OPCItemDef(opcid, true, itmHandleClient, VarEnum.VT_EMPTY);
                OPCItemResult[] arrRes;
                theGrp.AddItems(aD, out arrRes);
                if (arrRes == null)
                    return false;
                if (arrRes[0].Error != HRESULTS.S_OK)
                    return false;

                //btnItemMore.Enabled = true;
                itmHandleServer = arrRes[0].HandleServer;
                itmAccessRights = arrRes[0].AccessRights;
                itmTypeCode = VT2TypeCode(arrRes[0].CanonicalDataType);

                //txtItemID.Text = opcid;
                //txtItemDataType.Text = DUMMY_VARIANT.VarEnumToString(arrRes[0].CanonicalDataType);

                if ((itmAccessRights & OPCACCESSRIGHTS.OPC_READABLE) != 0)
                {
                    int cancelID;
                    theGrp.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE, 7788, out cancelID);
                }
                //else ;
                    //txtItemValue.Text = "no read access";

                if (itmTypeCode != TypeCode.Object)				// Object=failed!
                {
                    // check if write is premitted
                    if ((itmAccessRights & OPCACCESSRIGHTS.OPC_WRITEABLE) != 0) ;
                        //btnItemWrite.Enabled = true;
                }
            }
            catch (COMException)
            {
                //MessageBox.Show(this, "AddItem OPC error!", "ViewItem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        // remove previous OPC item if any
        public bool RemoveItem()
        {
            try
            {
                if (itmHandleClient != 0)
                {
                    itmHandleClient = 0;
                    //txtItemID.Text = "";		// clear screen texts
                    //txtItemValue.Text = "";
                    //txtItemDataType.Text = "";
                    //txtItemQual.Text = "";
                    //txtItemTimeSt.Text = "";
                    //txtItemSendValue.Text = "";
                    //txtItemWriteRes.Text = "";
                    //btnItemWrite.Enabled = false;
                    //btnItemMore.Enabled = false;

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
                int itemsValueCount = itemsValue.Count;
                OPCItemDef[] aD = new OPCItemDef[itemsValueCount];
                for (int i = 0; i < itemsValueCount; i++)
                {
                    itmHandleClient = i;

                    aD[i] = new OPCItemDef(itemsValue[i][0], true, itmHandleClient, VarEnum.VT_EMPTY);
                }

                //int[] ids;
                //this.theSrv.QueryAvailableLocaleIDs(out ids);
                //string strtmp;
                //strtmp = theSrv.GetItemProperties((ids[0].ToString());
                SERVERSTATUS status;
                theSrv.GetStatus(out status);
                OPCItemResult[] arrRes;
                theGrp.AddItems(aD, out arrRes);
                if (arrRes == null)
                    return;
                if (arrRes[0].Error != HRESULTS.S_OK)
                    return;

                //btnItemMore.Enabled = true;
                itmHandleServer = arrRes[0].HandleServer;
                itmAccessRights = arrRes[0].AccessRights;
                itmTypeCode = VT2TypeCode(arrRes[0].CanonicalDataType);

                //   txtItemID.Text = opcid;
                //    txtItemDataType.Text = DUMMY_VARIANT.VarEnumToString(arrRes[0].CanonicalDataType);

                if ((itmAccessRights & OPCACCESSRIGHTS.OPC_READABLE) != 0)
                {
                    int cancelID;
                    theGrp.Refresh2(OPCDATASOURCE.OPC_DS_DEVICE, 7788, out cancelID);
                }
                else ;
                    //txtItemValue.Text = "no read access";

                if (itmTypeCode != TypeCode.Object)				// Object=failed!
                {
                    // check if write is premitted
                    //if ((itmAccessRights & OPCACCESSRIGHTS.OPC_WRITEABLE) != 0)
                    //    btnItemWrite.Enabled = true;
                }
            }
            catch (COMException)
            {
                //MessageBox.Show(this, "AddItem OPC error!", "ViewItem", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
