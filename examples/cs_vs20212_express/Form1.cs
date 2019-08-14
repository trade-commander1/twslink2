#region Using directives
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Globalization;

using twslinkLib;
//using twslinkNET20;
#endregion
namespace cs
{

    /// c# callback type definition
    public delegate void EventCallbackProto(int long1,
                                            int long2,
                                            int primaryContext,
                                            int subContext,
                                            double dvalue1,
                                            double dvalue2,
                                            double dvalue3,
                                            double dvalue4,
                                            string s1,
                                            string s2,
                                            string s3,
                                            string s4,
                                            double dvalue5,
                                            double dvalue6,
                                            int long5,
                                            int long6);
    
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
    {
        #region Member Variables
		// twslink COM-Interface (call all exported functions with <get_>  prefix)
        //private twslinkNET20.comif                                          _twslink;
		private twslinkCom                                               _twslink;
		// C# callback handle for events and market data
        private EventCallbackProto                                  _evCB;
        // grid showing market data
        private SourceGrid.Grid                                     _MDData;

        // maps contract uid to grid row
        private System.Collections.Generic.Dictionary<int,int>      _uid2gridRow;
        // maps grid row to contract uid
        private System.Collections.Generic.Dictionary<int, int>     _gridRow2uid;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private ListBox LBMessage;
        private Button BTCP;
        private Button BTCC;
        private Button _btConnect;
        private Button _btDisconnect;
        private Label _stHost;
        private Label _stPort;
        private Label _stClientId;
        private TextBox _edHost;
        private TextBox _edPort;
        private TextBox _edClientId;
        private Button _btHelp;
        private CheckBox cbStatisticTicks;

        private static Form1 __app; 
        #endregion
        public Form1()
		{
            // init globals;
            __app = this;
            // initialize globals
            G1.Build();
            //
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            SetConnected(false,"");
            // post init
            InitMember();
            SetupDataGrid();
            this.Show();
            this.Cursor = Cursors.WaitCursor;
            SetupTWSLink();        

            // add some contract
            FillGrid();
            // connect to TWS
            //Connect("192.168.0.4", 7496, 1, 100);
           // Connect("127.0.0.1", 7496, 0, 15000);
            // once all contracts has been arrived,
            // set the default size for all indicators            
            this.Cursor = Cursors.Default;
		}
        #region Post Init Functions
        private bool SetupTWSLink()
        {
            // init DLL
            TL().get_SET_DLL_PARAM("setflags", "256");
            // --- callback setup ---
            // create our callback
            _evCB = new EventCallbackProto(Form1.EventCallback);
            // get handle of our callback
            IntPtr pf = Marshal.GetFunctionPointerForDelegate(_evCB);

            TL().get_SET_EVENT_HANDLER(pf.ToInt32(),0x1 + 0x2 + 0x10 + 0x40 + 0x80, 0);

            return true;
        }
        private bool Connect(string host, int port, int clientid, int timeoutms)
        {
            TL().get_CONNECT(host, port, clientid, timeoutms);
            return true;
        }
        private bool SetupDataGrid()
        {
            _MDData.Redim(1, G1.GetColumnCount());
            _MDData.BackColor = G1.clrBack;
            // row selection style
            _MDData.SelectionMode = SourceGrid.GridSelectionMode.Row;
            G1.SetSelectionStyle(_MDData);            
            _MDData.FixedRows = G1.FixedRows;
            _MDData.FixedColumns = G1.FixedColumns;

            SourceGrid.Cells.Controllers.ToolTipText toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();
            //toolTipController.ToolTipTitle = "Column Information";
            //toolTipController.ToolTipIcon = ToolTipIcon.Info;
            toolTipController.IsBalloon = true;

            ColDesc hcol = null;
            for (int c = 0; c < _MDData.ColumnsCount; c++)
            {
                SourceGrid.Cells.ColumnHeader header = new SourceGrid.Cells.ColumnHeader(c);
                _MDData[0, c] = header;
                hcol = G1.GetCol(c);
                if (hcol != null)
                {
                    _MDData[0, c].ToolTipText = hcol.tooltiptext;
                    _MDData[0,c].AddController(toolTipController);
                    header.Value = hcol.Name;
                    header.View = hcol.DefHView;
                    header.Column.Width = hcol.Width;
                }
                header.AutomaticSortEnabled = false;                
            }
            return true;
        }
        private void InitMember()
        {
            _uid2gridRow = new System.Collections.Generic.Dictionary<int, int>();
            _gridRow2uid = new System.Collections.Generic.Dictionary<int, int>();

            _btDisconnect.BackColor = Color.Maroon;
            _btDisconnect.ForeColor = Color.FromArgb(230, 230, 230);
            _btConnect.BackColor = Color.FromArgb(20,100, 20);
            _btConnect.ForeColor = Color.FromArgb(230, 230, 230);
        }
        private bool FillGrid()
        {
            bool bregister = true;
            // stocks

            this.Contract2Row("MSF", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("AAPL", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("AXA", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("GOOG", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("IBM", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("C", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("KO", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("CHK", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("MS", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("UTX", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("LEH", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("MRK", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("KSS", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("PFE", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("XOM", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("VLO", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("GM", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("WMT", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("TXN", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("BHI", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("MRO", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("GE", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("TGT", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("MER", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("BAC", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            this.Contract2Row("INTC", "STK", "USD", "SMART", "", "", "0.0", 100, -1, bregister);
            

            // cash (chain requests)
            this.Contract2Row("EUR", "CASH", "", "IDEALPRO", "", "", "0.0", 30000, -1, bregister);
            this.Contract2Row("USD", "CASH", "", "IDEALPRO", "", "", "0.0", 30000, -1, bregister);
            this.Contract2Row("GBP", "CASH", "", "IDEALPRO", "", "", "0.0", 30000, -1, bregister);           
            
            // futures
            this.Contract2Row("QM", "FUT", "USD", "NYMEX", "200708", "", "0.0", 2, -1, bregister);
            this.Contract2Row("YM", "FUT", "USD", "ECBOT", "200708", "", "0.0", 2, -1, bregister);
            
            // options (IBM chain)
            this.Contract2Row("IBM", "OPT", "USD", "SMART", "200709", "", "0.0", 2, -1, bregister);
            
            // empty row allows adding contract
            this.Contract2Row("", "", "", "", "", "", "0.0", 0, -1, false);
            return true;
        }
        private void SetGridAppearance(int Mode)
        {
            //_MDData.Update
            SourceGrid.Cells.Cell cCell = null;
            ColDesc cd = null;
            for (int j = _MDData.FixedRows; j < _MDData.RowsCount; ++j)
            {

                // data columns
                for (int i = _MDData.FixedColumns; i < (int)ColIdx.enBuyMkt; ++i)
                {
                    cCell = (SourceGrid.Cells.Cell)_MDData[j, i];
                    cd = G1.GetCol(i);
                    if (cd != null && cCell != null)
                    {
                        if (i != (int)ColIdx.enPositions || Mode == 0)
                        {
                            cCell.View = cd.GetStyle(j, Mode);
                        }
                        else
                        {
                            cCell.View = G1.GetPosView(j, Cell2Int(cCell));
                        }
                        this._MDData.InvalidateCell(cCell);
                    }
                }
            }
        }
        #endregion
  
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null) 
				{
					components.Dispose();
				}
			}
            // clean up TWSLink internal threads.
            // (do NOT forget to call this function in net-environments.)
            int ret = TL().get_DISPOSE(0);
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this._MDData = new SourceGrid.Grid();
            this.cbStatisticTicks = new System.Windows.Forms.CheckBox();
            this.LBMessage = new System.Windows.Forms.ListBox();
            this.BTCP = new System.Windows.Forms.Button();
            this.BTCC = new System.Windows.Forms.Button();
            this._btConnect = new System.Windows.Forms.Button();
            this._btDisconnect = new System.Windows.Forms.Button();
            this._stHost = new System.Windows.Forms.Label();
            this._stPort = new System.Windows.Forms.Label();
            this._stClientId = new System.Windows.Forms.Label();
            this._edHost = new System.Windows.Forms.TextBox();
            this._edPort = new System.Windows.Forms.TextBox();
            this._edClientId = new System.Windows.Forms.TextBox();
            this._btHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _MDData
            // 
            this._MDData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._MDData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._MDData.Location = new System.Drawing.Point(-1, 0);
            this._MDData.Name = "_MDData";
            this._MDData.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this._MDData.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this._MDData.Size = new System.Drawing.Size(1272, 372);
            this._MDData.TabIndex = 3;
            this._MDData.TabStop = true;
            this._MDData.ToolTipText = "";
            this._MDData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridMoussClick);
            // 
            // cbStatisticTicks
            // 
            this.cbStatisticTicks.AutoSize = true;
            this.cbStatisticTicks.Location = new System.Drawing.Point(743, 429);
            this.cbStatisticTicks.Name = "cbStatisticTicks";
            this.cbStatisticTicks.Size = new System.Drawing.Size(128, 17);
            this.cbStatisticTicks.TabIndex = 4;
            this.cbStatisticTicks.Text = "Enable Statistic Ticks";
            this.cbStatisticTicks.UseVisualStyleBackColor = true;
            // 
            // LBMessage
            // 
            this.LBMessage.FormattingEnabled = true;
            this.LBMessage.Location = new System.Drawing.Point(-2, 372);
            this.LBMessage.Name = "LBMessage";
            this.LBMessage.Size = new System.Drawing.Size(731, 82);
            this.LBMessage.TabIndex = 1;
            // 
            // BTCP
            // 
            this.BTCP.BackColor = System.Drawing.Color.Maroon;
            this.BTCP.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.BTCP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.BTCP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTCP.ForeColor = System.Drawing.Color.White;
            this.BTCP.Location = new System.Drawing.Point(1018, 377);
            this.BTCP.Name = "BTCP";
            this.BTCP.Size = new System.Drawing.Size(189, 22);
            this.BTCP.TabIndex = 4;
            this.BTCP.Text = "Close Portfolio";
            this.BTCP.UseVisualStyleBackColor = false;
            this.BTCP.Click += new System.EventHandler(this.BTCP_Click);
            // 
            // BTCC
            // 
            this.BTCC.BackColor = System.Drawing.Color.Maroon;
            this.BTCC.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.BTCC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.BTCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTCC.ForeColor = System.Drawing.Color.White;
            this.BTCC.Location = new System.Drawing.Point(1018, 399);
            this.BTCC.Name = "BTCC";
            this.BTCC.Size = new System.Drawing.Size(189, 22);
            this.BTCC.TabIndex = 5;
            this.BTCC.Text = "Close Cash Positions";
            this.BTCC.UseVisualStyleBackColor = false;
            this.BTCC.Click += new System.EventHandler(this.BTCC_Click);
            // 
            // _btConnect
            // 
            this._btConnect.Location = new System.Drawing.Point(932, 379);
            this._btConnect.Name = "_btConnect";
            this._btConnect.Size = new System.Drawing.Size(80, 22);
            this._btConnect.TabIndex = 6;
            this._btConnect.Text = "Connect";
            this._btConnect.UseVisualStyleBackColor = true;
            this._btConnect.Click += new System.EventHandler(this._btConnect_Click);
            // 
            // _btDisconnect
            // 
            this._btDisconnect.Location = new System.Drawing.Point(932, 400);
            this._btDisconnect.Name = "_btDisconnect";
            this._btDisconnect.Size = new System.Drawing.Size(80, 22);
            this._btDisconnect.TabIndex = 7;
            this._btDisconnect.Text = "Disconnect";
            this._btDisconnect.UseVisualStyleBackColor = true;
            this._btDisconnect.Click += new System.EventHandler(this._btDisconnect_Click);
            // 
            // _stHost
            // 
            this._stHost.AutoSize = true;
            this._stHost.Location = new System.Drawing.Point(741, 379);
            this._stHost.Name = "_stHost";
            this._stHost.Size = new System.Drawing.Size(29, 13);
            this._stHost.TabIndex = 8;
            this._stHost.Text = "Host";
            // 
            // _stPort
            // 
            this._stPort.AutoSize = true;
            this._stPort.Location = new System.Drawing.Point(836, 379);
            this._stPort.Name = "_stPort";
            this._stPort.Size = new System.Drawing.Size(26, 13);
            this._stPort.TabIndex = 9;
            this._stPort.Text = "Port";
            // 
            // _stClientId
            // 
            this._stClientId.AutoSize = true;
            this._stClientId.Location = new System.Drawing.Point(884, 379);
            this._stClientId.Name = "_stClientId";
            this._stClientId.Size = new System.Drawing.Size(42, 13);
            this._stClientId.TabIndex = 10;
            this._stClientId.Text = "ClientId";
            // 
            // _edHost
            // 
            this._edHost.Location = new System.Drawing.Point(742, 399);
            this._edHost.MaxLength = 20;
            this._edHost.Name = "_edHost";
            this._edHost.Size = new System.Drawing.Size(86, 20);
            this._edHost.TabIndex = 12;
            this._edHost.Text = "127.0.0.1";
            this._edHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _edPort
            // 
            this._edPort.Location = new System.Drawing.Point(839, 399);
            this._edPort.MaxLength = 8;
            this._edPort.Name = "_edPort";
            this._edPort.Size = new System.Drawing.Size(38, 20);
            this._edPort.TabIndex = 13;
            this._edPort.Text = "7496";
            this._edPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _edClientId
            // 
            this._edClientId.Location = new System.Drawing.Point(886, 399);
            this._edClientId.MaxLength = 2;
            this._edClientId.Name = "_edClientId";
            this._edClientId.Size = new System.Drawing.Size(38, 20);
            this._edClientId.TabIndex = 14;
            this._edClientId.Text = "0";
            this._edClientId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _btHelp
            // 
            this._btHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this._btHelp.ForeColor = System.Drawing.Color.Blue;
            this._btHelp.Location = new System.Drawing.Point(1216, 377);
            this._btHelp.Name = "_btHelp";
            this._btHelp.Size = new System.Drawing.Size(54, 41);
            this._btHelp.TabIndex = 15;
            this._btHelp.Text = "Help";
            this._btHelp.UseVisualStyleBackColor = false;
            this._btHelp.Click += new System.EventHandler(this._btHelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1270, 456);
            this.Controls.Add(this.cbStatisticTicks);
            this.Controls.Add(this._btHelp);
            this.Controls.Add(this._edClientId);
            this.Controls.Add(this._edPort);
            this.Controls.Add(this.LBMessage);
            this.Controls.Add(this._edHost);
            this.Controls.Add(this.BTCC);
            this.Controls.Add(this._stClientId);
            this.Controls.Add(this._btDisconnect);
            this.Controls.Add(this._stPort);
            this.Controls.Add(this._btConnect);
            this.Controls.Add(this._stHost);
            this.Controls.Add(this.BTCP);
            this.Controls.Add(this._MDData);
            this.Name = "Form1";
            this.Text = "TWSLink C# Sample";
            this.SizeChanged += new System.EventHandler(this.FormSizeChange);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
        }
        #region Events
        /// <summary>
		/// Events
		/// </summary>
		private void FormSizeChange(object sender, System.EventArgs e)
		{
            this._MDData.Size = new System.Drawing.Size(this.ClientSize.Width// - 2 * G1._gridpadd
                                                        , this.ClientSize.Height - G1._LBMessageHeight - 2 * G1._gridpadd - 20);

            int new_y_offset = this._MDData.Location.Y + this._MDData.Size.Height + G1._gridpadd; 
            this.LBMessage.Location = new Point(G1._gridpadd,new_y_offset);

            /*
            this.TBNL.Location = new Point(this.TBNL.Location.X, new_y_offset);
            this.TBNLD.Location = new Point(this.TBNLD.Location.X, new_y_offset);
            this.TBMARG.Location = new Point(this.TBMARG.Location.X,new_y_offset);
            */
            this.BTCP.Location = new Point(this.BTCP.Location.X, new_y_offset);
            this.BTCC.Location = new Point(this.BTCP.Location.X, new_y_offset + BTCP.Size.Height);

            this._btConnect.Location = new Point(this._btConnect.Location.X, new_y_offset);
            this._btDisconnect.Location = new Point(this._btDisconnect.Location.X, new_y_offset + _btDisconnect.Size.Height);

            this._btHelp.Location = new Point(this._btHelp.Location.X, new_y_offset);
                
            this._stHost.Location = new Point(this._stHost.Location.X, new_y_offset);
            this._stPort.Location = new Point(this._stPort.Location.X, new_y_offset);
            this._stClientId.Location = new Point(this._stClientId.Location.X, new_y_offset);

            int dist=2;
            this._edHost.Location = new Point(this._edHost.Location.X, new_y_offset + _stHost.Size.Height + dist);
            this._edPort.Location = new Point(this._edPort.Location.X, new_y_offset + _stHost.Size.Height + dist);
            this._edClientId.Location = new Point(this._edClientId.Location.X, new_y_offset + _stHost.Size.Height + dist);

            this.cbStatisticTicks.Location = new Point(this.cbStatisticTicks.Location.X, new_y_offset + _stHost.Size.Height + cbStatisticTicks.Size.Height + 4*dist);
            
        }
        #endregion
        #region Dictonary Operations
        private int UID2Row(int uid)
        {
            int row = -1;
            try
            {
                if (_uid2gridRow.TryGetValue(uid, out row))
                    return row;
            }
            catch (ArgumentNullException)
            {
                ;//Console.WriteLine("Unable to retrieve value for null key.");
            }
            return -1;
        }
        private int Row2UID(int row)
        {
            int uid = 0;
            try
            {
                if (_gridRow2uid.TryGetValue(row, out uid))
                    return uid;
            }
            catch (ArgumentNullException)
            {
                ;//Console.WriteLine("Unable to retrieve value for null key.");
            }
            return -1;
        }
        #endregion
        #region GUI Helper Functions
        void AddAppMessage(string header, string message)
        {
            string lbmessage = "AppMsg:";
            lbmessage += header;
            lbmessage += ": ";
            lbmessage += message;
            this.LBMessage.Items.Insert(0,lbmessage);
        }
        void AddTLMessage(string header, string message)
        {
            string lbmessage = "TWSLinkMsg:";
            lbmessage += header;
            lbmessage += ": ";
            lbmessage += message;
            this.LBMessage.Items.Insert(0,lbmessage);
        }
        #endregion
        #region General Helper Functions
        double TWSString2Double(string twsstring,double faultyval)
        {
            twsstring = twsstring.Replace('.', ',');
            double ret = faultyval;
            if (twsstring.CompareTo("nok") != 0 && 
                twsstring.CompareTo("cnf") != 0 &&
                twsstring.CompareTo("vnf") != 0 &&
                twsstring.CompareTo("onf") != 0 &&
                twsstring.CompareTo("nim") != 0 && 
                twsstring.Length > 0)
            {
                try
                {
                    ret = System.Convert.ToDouble(twsstring);
                }
                catch (Exception e) { ;}
            }
            else
                ret = faultyval;
            return ret;
        }
        int TWSString2Int(string twsstring, int faultyval)
        {
            int ret = faultyval;
            if (twsstring.CompareTo("nok") != 0 && twsstring.CompareTo("cnf") != 0 && twsstring.Length > 0)
            {
                string[] parts = twsstring.Split(',', '.');
                if (parts[0].Length > 0)
                {
                    try
                    {
                        ret = System.Convert.ToInt32(parts[0]);
                    }
                    catch (Exception e) { ;}
                }
            }
            return ret;
        }
        int String2Int(string xstring, int faultyval)
        {
            int ret = faultyval;
            try
            {
                ret = System.Convert.ToInt32(xstring);
            }
            catch (Exception e) { ;}
            return ret;
        }
        string TWSDoubleString2IntString(string twsstring, int faultyval)
        {
            if (twsstring.CompareTo("nok") != 0 && twsstring.CompareTo("cnf") != 0 && twsstring.Length > 0)
            {
                string[] parts = twsstring.Split(',', '.');
                return parts[0];
            }
            return faultyval.ToString();
        }
        int Double2Int(double dval, int faultyval)
        {
            
            int ret = faultyval;
            try
            {
                ret = System.Convert.ToInt32(dval);
            }
            catch (Exception e) { ;}
            return ret;
        }
        #endregion
        #region Grid Helper Functions
        private bool RowValid(int row,bool withHeader)
        {
            return ((withHeader == true ? row < 0 || row >= this._MDData.RowsCount ? false : true : row < _MDData.FixedRows || row >= this._MDData.RowsCount ? false : true));
        }
        private bool ColValid(int col)
        {
            return ((col < 0 || col >= this._MDData.ColumnsCount ? false : true));
        }
        private bool DataCellIdxValid(int row, int col)
        {
            if (RowValid(row, false) == false || ColValid(col) == false)
                return false;
            return true;
        }
        private double Cell2Double(SourceGrid.Cells.Cell x)
        {
            if (x != null && x.Value != null)
            {
                try
                {
                    return Convert.ToDouble(x.Value);
                }
                catch (Exception e) { ;}
            }
            return 0.0;
        }
        private int Cell2Int(SourceGrid.Cells.Cell x)
        {
            if (x != null && x.Value != null)
            {
                try
                {
                    return Convert.ToInt32(x.Value);
                }
                catch (Exception e) 
                { 
                }
            }
            return 0;
        }
        private string Cell2String(SourceGrid.Cells.Cell x)
        {
            if (x != null && x.Value != null && x.Value.ToString().Length > 0)
                return x.Value.ToString();
            return "";
        }
        private void String2Cell(SourceGrid.Cells.Cell x, string y)
        {
            if (x != null)
                x.Value = y;
        }
        private void Double2Cell(SourceGrid.Cells.Cell x, double y)
        {
            if (x != null)
                x.Value = y;
        }
        private void Int2Cell(SourceGrid.Cells.Cell x, int y)
        {
            if (x != null)
                x.Value = y;
        }
        private SourceGrid.Cells.Cell Col(SourceGrid.Grid.GridRow row, int colidx)
        {
            if (row != null)
            {
                try
                {
                    return ((SourceGrid.Cells.Cell)row[_MDData.Columns[colidx]]);
                }
                catch
                {
                    ;
                }
            }
            return null;
        }
        private SourceGrid.Cells.Cell Cell(int rowidx, int colidx)
        {
            try
            {
                return (SourceGrid.Cells.Cell)this._MDData[rowidx, colidx];
            }
            catch
            {
                return null;
            }
        }
        private bool ApplyStyle(int row, int col, SourceGrid.Cells.Views.Cell style)
        {
            if(style != null)
            {
                SourceGrid.Cells.Cell xCell = this.Cell(row, col);
                if (xCell != null)
                {
                    xCell.View = style;
                    return true;
                }
            }
            return false;
        }
        private double GetPrice(int idxcol, int idxrow)
        {
            if (DataCellIdxValid(idxrow, idxcol) == true)
            {
                SourceGrid.Cells.Cell cell = (SourceGrid.Cells.Cell)_MDData[idxrow, idxcol];

                if (cell != null && cell.Value != null)
                {
                    try
                    {
                        double price = Convert.ToDouble(cell.Value);
                        //double price = (double)cell.Value;
                        if (price > 0.0)
                            return price;
                    }
                    catch
                    {
                    }
                }
            }
            return 0.0;
        }
        private int GetOSize(int idxcol, int idxrow)
        {
            if (DataCellIdxValid(idxrow, idxcol) == true)
            {
                SourceGrid.Cells.Cell cell = (SourceGrid.Cells.Cell)_MDData[idxrow, idxcol];
                if (cell != null && cell.Value != null)
                {
                    int osize = (int)cell.Value;
                    if (osize > 0)
                        return osize;
                }
            }
            return 0;
        }
        private void SetReadOnly(SourceGrid.Cells.Cell x, bool creadonly)
        {
            if (x != null && x.Editor != null)
                x.Editor.EnableEdit = !creadonly;
        }
#endregion
        #region Callback Successors 
        private void UpdateAllPositions()
        {
            double pos = 0; 
            for (int i = 0; i < this._MDData.RowsCount - 1; ++i)
            {
                pos = TL().get_GET_POSITIONS(Row2UID(i), "");
                UpdatePositions(i,pos);
            }
        }
        private void UpdatePositions(int row, double positions)
        {            
            if (row > 0)
            {
                // positions
                SourceGrid.Cells.Cell poscell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enPositions];
                if(poscell != null)
                {
                    poscell.View = G1.GetPosView(row, positions);
                    poscell.Value = positions;
                    this._MDData.InvalidateCell(poscell);
                }
            }
        }
        private void UpdateBidDepStatistic(int uidcontract, int row)
        // update all bid tick dependent statistic for this contract
        {
            if (row >= 0)
            {
                // subsequent bid ticks
                int sbids = Double2Int(TL().get_GET_MARKET_DATA(uidcontract, 79, 0, 1), 0);
                SourceGrid.Cells.Cell xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enSEQ_BID];
                if (xcell != null)
                {
                    xcell.View = G1.GetIntView(sbids);
                    xcell.Value = sbids;
                   // this._MDData.InvalidateCell(xcell);
                }
                // subsequent bid ticks n average (def. 200)
                double sbidsa = TL().get_GET_MARKET_DATA(uidcontract, 82, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enSEQ_BID_NAVG];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(sbidsa);
                    xcell.Value = sbidsa;
                   // this._MDData.InvalidateCell(xcell);
                }
            }
        }
        private void UpdateAskDepStatistic(int uidcontract, int row)
        // update all bid tick dependent statistic for this contract
        {
            if (row >= 0)
            {
                // subsequent ask ticks
                int xi = Double2Int(TL().get_GET_MARKET_DATA(uidcontract, 80, 0, 1), 0);
                SourceGrid.Cells.Cell xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enSEQ_ASK];
                if (xcell != null)
                {
                    xcell.View = G1.GetIntView(xi);
                    xcell.Value = xi;
                   // this._MDData.InvalidateCell(xcell);
                }

                // subsequent ask ticks n average (def. 200)
                double xd = TL().get_GET_MARKET_DATA(uidcontract, 83, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enSEQ_ASK_NAVG];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }
                // runtime momentum
                xd = TL().get_GET_MARKET_DATA(uidcontract, 85, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRT_MOM];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    //this._MDData.InvalidateCell(xcell);
                }
                // runtime pip
                xi = Double2Int(TL().get_GET_MARKET_DATA(uidcontract, 86, 0, 1), 0);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRT_MOM_PIP];
                if (xcell != null)
                {
                    xcell.View = G1.GetIntView(xi);
                    xcell.Value = xi;
                    //this._MDData.InvalidateCell(xcell);
                }
                // runtime angle
                xd =TL().get_GET_MARKET_DATA(uidcontract, 87, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRT_ANGLE];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }

                // n1 momentum
                xd = TL().get_GET_MARKET_DATA(uidcontract, 88, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN1_MOM];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n1 pip
                xi = Double2Int(TL().get_GET_MARKET_DATA(uidcontract, 89, 0, 1), 0);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN1_MOM_PIP];
                if (xcell != null)
                {
                    xcell.View = G1.GetIntView(xi);
                    xcell.Value = xi;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n1 angle
                xd = TL().get_GET_MARKET_DATA(uidcontract, 90, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN1_ANGLE];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }

                // n2 momentum
                xd = TL().get_GET_MARKET_DATA(uidcontract, 91, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN2_MOM];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n2 pip
                xi = Double2Int(TL().get_GET_MARKET_DATA(uidcontract, 92, 0, 1), 0);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN2_MOM_PIP];
                if (xcell != null)
                {
                    xcell.View = G1.GetIntView(xi);
                    xcell.Value = xi;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n2 angle
                xd = TL().get_GET_MARKET_DATA(uidcontract, 93, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN2_ANGLE];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n3 momentum
                xd = TL().get_GET_MARKET_DATA(uidcontract, 94, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN3_MOM];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    //  this._MDData.InvalidateCell(xcell);
                }
                // n3 pip
                xi = Double2Int(TL().get_GET_MARKET_DATA(uidcontract, 95, 0, 1), 0);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN3_MOM_PIP];
                if (xcell != null)
                {
                    xcell.View = G1.GetIntView(xi);
                    xcell.Value = xi;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n3 angle
                xd = TL().get_GET_MARKET_DATA(uidcontract, 96, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN3_ANGLE];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    //this._MDData.InvalidateCell(xcell);
                }
                // n1 average
                xd = TL().get_GET_MARKET_DATA(uidcontract, 97, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN1_AVG];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n2 average
                xd = TL().get_GET_MARKET_DATA(uidcontract, 98, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN2_AVG];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }
                // n3 average
                xd = TL().get_GET_MARKET_DATA(uidcontract, 99, 0, 1);
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enN3_AVG];
                if (xcell != null)
                {
                    xcell.View = G1.GetDoubleView(xd);
                    xcell.Value = xd;
                    // this._MDData.InvalidateCell(xcell);
                }

            }
        }
        private void UpdateUnrealizedPL(int uidcontract,double PL,double PLPips)
        // update all bid tick dependent statistic for this contract
        {
            int row = this.UID2Row(uidcontract);
            if (row >= 0)
            {
                // unrealized p&l
                SourceGrid.Cells.Cell xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enURPL];
                if (xcell != null)
                {
                    xcell.View = G1.GetStringViewPL(System.Convert.ToInt32(PL));
                    //xcell.Value = PL.ToString();
                    xcell.Value = System.Convert.ToInt32(PL).ToString();
                    this._MDData.InvalidateCell(xcell);
                }
                // unrealized p&l pips
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enURPLPIPS];
                if (xcell != null)
                {
                    xcell.View = G1.GetStringViewPL(System.Convert.ToInt32(PLPips));
                    //xcell.Value = PLPips.ToString();
                    xcell.Value = System.Convert.ToInt32(PLPips).ToString();
                    this._MDData.InvalidateCell(xcell);
                }
            }
        }
        private void UpdateRealizedPL(int uidcontract, double PL, double PLPips)
        {
            int row = this.UID2Row(uidcontract);
            if (row >= 0)
            {
                // generic realized pl
                SourceGrid.Cells.Cell plcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRPL];
                if (plcell != null)
                {
                    plcell.View = G1.GetStringViewPL(System.Convert.ToInt32(PL));
                    plcell.Value = PL.ToString();
                    this._MDData.InvalidateCell(plcell);
                }
                // generic realized pl pips
                plcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRPLPIPS];
                if (plcell != null)
                {
                    plcell.View = G1.GetStringViewPL(System.Convert.ToInt32(PLPips));
                    plcell.Value = PLPips.ToString();
                    this._MDData.InvalidateCell(plcell);
                }
            }
        }
        private void UpdateRTAvg(int uidcontract, double val, int row)
        {
            if (row >= 0)
            {
                SourceGrid.Cells.Cell tcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRTAvg];
                if (tcell != null)
                {
                    tcell.Value = val;
                    //this._MDData.InvalidateCell(tcell);
                }
            }
        }
        private void UpdateRTHigh(int uidcontract, int row)
        {
            
            if (row >= 0)
            {
                double xval = TL().get_GET_MARKET_DATA(uidcontract, 72, 0, 1);

                SourceGrid.Cells.Cell tcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRTHigh];
                if (tcell != null)
                {
                    tcell.Value = xval;
                    //this._MDData.InvalidateCell(tcell);
                }
            }
        }
        private void UpdateRTLow(int uidcontract, int row)
        {
            if (row >= 0)
            {
                double xval = TL().get_GET_MARKET_DATA(uidcontract, 73, 0, 1);
            
                SourceGrid.Cells.Cell tcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enRTLow];
                if (tcell != null)
                {
                    tcell.Value = xval;
                    //this._MDData.InvalidateCell(tcell);
                }
            }
        }
        private void UpdateTickCount(int uidcontract, int row)
        // single tickcount update
        {
            
            if (row >= 0)
            {
                int ticks = TWSString2Int(TL().get_GET_MARKET_DATA(uidcontract, 77, 0, 1).ToString(), 0);                        
                SourceGrid.Cells.Cell tcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enTotalTicks];
                if (tcell != null)
                {
                    tcell.Value = ticks;
                    //this._MDData.InvalidateCell(tcell);
                }
            }
        }
        private void UpdateTickStat()
        // refresh all per minutes and avg. tick statis
        {
            for (int i = 0; i < this._MDData.RowsCount - 1; ++i)
                UpdateTickStat(Row2UID(i));
        }
        private void UpdateTickStat(int uidcontract)
        {
            double t_per_m =  TL().get_GET_MARKET_DATA(uidcontract, 75, 0, 1);
            double avg_t_per_m =  TL().get_GET_MARKET_DATA(uidcontract, 76, 0, 1);
            int row = this.UID2Row(uidcontract);
            if (row >= 0)
            {
                SourceGrid.Cells.Cell xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enTicksPM];
                if (xcell != null)
                {
                    xcell.Value = t_per_m.ToString("f");
                    this._MDData.InvalidateCell(xcell);
                }
                xcell = (SourceGrid.Cells.Cell)_MDData[row, (int)ColIdx.enAvgTicksPM];
                if (xcell != null)
                {
                    xcell.Value = avg_t_per_m.ToString("f");
                    this._MDData.InvalidateCell(xcell);
                }
            }
        }
        #endregion
        #region Misc
        private void ClearRow(int idxrow, bool full,bool cancelmd)
        {
            if (RowValid(idxrow, false) == true)
            {
                if (cancelmd == true)
                {
                    int prevUID = Row2UID(idxrow);
                    // cancel previous market data
                    if (prevUID > 0)
                    {
                        TL().get_REQ_MARKET_DATA(prevUID, 0, "",0);
                        _uid2gridRow.Remove(prevUID);
                        _gridRow2uid.Remove(idxrow);
                    }
                }
                int begin   = (full == true ? (int)ColIdx.enSymbol : (int)ColIdx.enBid);
                int end = (int)ColIdx.enOptPutOI;
                SourceGrid.Cells.Cell xcell = null;
                for (int i = begin; i <= end; ++i)
                {
                    xcell = (SourceGrid.Cells.Cell) this._MDData[idxrow, i];
                    if(xcell != null)
                        xcell.Value = "";
                }
            }
        }
        private void DeleteRow(int idxrow, bool cancelmd)
        {
            if (RowValid(idxrow, false) == true)
            {
                bool rebuildmaps = true;// (idxrow < this._MDData.RowsCount - 1 ? true : false);
                int prevUID = -1;
                if (cancelmd == true)
                {
                    prevUID = Row2UID(idxrow);
                    // cancel previous market data
                    if (prevUID > 0)
                        TL().get_REQ_MARKET_DATA(prevUID, 0, "", 0);
                }
                if (rebuildmaps == true)
                    RebuildKeyMaps();
                else if (prevUID > 0)
                {
                    _uid2gridRow.Remove(prevUID);
                    _gridRow2uid.Remove(idxrow);
                }
                this._MDData.Rows.Remove(idxrow);
            }
        }
        private void RebuildKeyMaps()
        {
            _uid2gridRow.Clear();
            _gridRow2uid.Clear();
            //int cc = G1.GetColumnCount();
            int uid = 0;
            for (int i = this._MDData.FixedRows; i < this._MDData.RowsCount; ++i)
            {
                uid = Cell2Int((SourceGrid.Cells.Cell) this._MDData[i,(int)ColIdx.enData]);
                if (uid > 0)
                {
                    _uid2gridRow[uid] = i;
                    _gridRow2uid[i] = uid;
                }
            }
        }

        private int AddContract(int idxrow)
        // add contract to grid, register contract and request market data
        {
            int uid = -1;
            if (RowValid(idxrow, false) == true)
            {
                SourceGrid.Grid.GridRow row = this._MDData.Rows[idxrow];
                if (row != null)
                {
                    string symbol = this.Cell2String(Col(row,(int)ColIdx.enSymbol));
                    string type = this.Cell2String(Col(row,(int)ColIdx.enType));
                    // pre check against empty row
                    if (symbol.Length > 0 && type.Length > 0)
                    {
                        string currency = this.Cell2String(Col(row,(int)ColIdx.enCurrency));
                        string exchange = this.Cell2String(Col(row,(int)ColIdx.enExchange));
                        string expiry = this.Cell2String(Col(row,(int)ColIdx.enExpiry)); 
                        string right = this.Cell2String(Col(row,(int)ColIdx.enRight)); 
                        double strike = this.Cell2Double(Col(row,(int)ColIdx.enStrike)); 
                        uid = TL().get_REGISTER_CONTRACT(symbol, type, currency, exchange, "", expiry, right, strike, "", 0,0.0);
                        // single contract registration
                        if (uid > 0)
                        {
                            // cancel previous market data
                            ClearRow(idxrow, false, true);
                            _uid2gridRow[uid] = idxrow;
                            _gridRow2uid[idxrow] = uid;
                            // request market data (fast volle packung)
                           TL().get_REQ_MARKET_DATA(uid, 1, "100,101,104,106", 0);

                            Int2Cell(Col(row,(int)ColIdx.enData), uid);

                        }
                        // chain request: request market data for all affected contracts
                        else if (uid == 0)
                        {
                            //TL().get_WAIT_FOR_REQ_PROCESSED(5000);
                            //TL().get_REQ_MARKET_DATA(0, 1, "100,101,104,106", 0);
                            // delete this row: incoming contracts from chain will be added
                            DeleteRow(idxrow, false);
                        }
                        // if == 0, user register a chain
                    }
                    else
                        this.AddAppMessage("warning","Define at least Symbol and Security Type");
                        
                }
            }
            // empty row allows adding contract
            //if(idxrow == _MDData.RowsCount - 1)
              //  this.Contract2Row("", "", "", "", "", "", 70.0, 2, -1, false);

            return uid;
        }
        private int Contract2Row(string Symbol,
                                 string Type,
                                 string Currency,
                                 string Exchange,
                                 string Expiry,
                                 string Right,
                                 string Strike,
                                 int    OSize,
                                 int    idxrow,
                                 bool   register)
        {
            bool created = false;
            // insert a row
            if(RowValid(idxrow,false) == false)
            {
                _MDData.Rows.Insert(_MDData.RowsCount);
                idxrow = _MDData.RowsCount - 1;
                created = true;
            }
            SourceGrid.Grid.GridRow row = this._MDData.Rows[idxrow];
            if (row != null)
            {
                row.Height = 16;
                if (created == true)
                {
                    SourceGrid.Cells.Cell newCell = null;
                    SourceGrid.Cells.RowHeader rowheader = null;
                    ColDesc cd = null;
                    for (int i = 0; i < _MDData.FixedColumns; ++i)
                    {
                        cd = G1.GetCol(i);
                        rowheader = new SourceGrid.Cells.RowHeader(G1.GetCol(i).defaulttext);
                        if(cd != null)
                            rowheader.View = cd.GetStyle(idxrow,-1);
                        _MDData[idxrow, i] = rowheader;
                    }
                    // data columns
                    for (int i = _MDData.FixedColumns; i < (int)ColIdx.enBuyMkt; ++i)
                    {
                        cd = G1.GetCol(i);
                        newCell = new SourceGrid.Cells.Cell("");
                        if (cd != null)
                        {
                            newCell.View = cd.GetStyle(idxrow,-1);
                            newCell.Editor = cd.edit;
                        }
                        _MDData[idxrow, i] = newCell;
                    }

                    for (int i = (int) ColIdx.enBuyMkt; i < _MDData.ColumnsCount; ++i)
                    {
                        rowheader = new SourceGrid.Cells.RowHeader(G1.GetCol(i).defaulttext);
                        rowheader.View = G1.GetCol(i).GetStyle(idxrow,-1);
                        _MDData[idxrow, i] = rowheader;
                    }

                }
                String2Cell(Col(row,(int)ColIdx.enSymbol),Symbol);
                String2Cell(Col(row,(int)ColIdx.enType) ,Type);
                String2Cell(Col(row,(int)ColIdx.enCurrency),Currency);
                String2Cell(Col(row,(int)ColIdx.enExchange),Exchange);
                String2Cell(Col(row,(int)ColIdx.enExpiry),Expiry);
                String2Cell(Col(row,(int)ColIdx.enRight),Right);
                //Double2Cell(Col(row, (int)ColIdx.enStrike), Strike);
                String2Cell(Col(row, (int)ColIdx.enStrike), Strike);
                Int2Cell(Col(row, (int)ColIdx.enDOSize), G1.GetOSize(Type));
            }
            if(register == true)
                return this.AddContract(idxrow);
            return 0;
        }
        private int ContractStatus(int uid,int conid,double errorsignature)
        // updates or insert a contract
        {
            // get the row for contract
            int row = this.UID2Row(uid);
            // contract import. make your changes here, if you do not want to import
            // contracts.
            if (row < 0)
            {
                double pos = TL().get_GET_POSITIONS(uid, "");
                // import only |positions| >= 1
                if (pos >= 0.0 || pos <= 0.0)
                {
                    row = this._MDData.RowsCount - 1;
                    string symbol = TL().get_GET_CONTRACT_VAL(uid, 0, "", "");
                    string type = TL().get_GET_CONTRACT_VAL(uid, 3, "", "");
                    string currency = TL().get_GET_CONTRACT_VAL(uid, 2, "", "");
                    string exchange = TL().get_GET_CONTRACT_VAL(uid, 1, "", "");
                    string expiry = TL().get_GET_CONTRACT_VAL(uid, 4, "", "");
                    string right = TL().get_GET_CONTRACT_VAL(uid, 5, "", "");
                    string sstrike = TL().get_GET_CONTRACT_VAL(uid, 6, "", "");
                    //double strike = TWSString2Double(sstrike, 0.0);
                    // overwrite last row with new contract, don't register (it is alread registered)
                    Contract2Row(symbol, type, currency, exchange, expiry, right, sstrike, 1000, row, false);
                    // save pair/row
                    _uid2gridRow[uid] = row;
                    _gridRow2uid[row] = uid;
                    // request market data (fast volle packung)
                    TL().get_REQ_MARKET_DATA(uid, 1, "100,101,104,106", 0);
                    // save uid again to be able rebuilding dicitionaries
                    Int2Cell(Cell(row, (int)ColIdx.enData), uid);
                    // add empty row
                    this.Contract2Row("", "", "", "", "", "", "0.0", 0, -1, false);
                }
                else
                {
                    if (errorsignature >= 0.0)
                    {
                        TL().get_REQ_MARKET_DATA(uid, 1, "100,101,104,106", 0);
                    }
                    // most probably error somehow related to market data subscription
                    else
                    {
                    }
                    return 0;
                }
            }
            
            
            // conid == 0 means contract not accepted (wrong definition)
            // -> sign the local symbol col as invalid
            if (conid == 0)
            {
                ApplyStyle(row,(int)ColIdx.enLocalSymbol,G1._vNegPos);
                String2Cell(Cell(row,(int)ColIdx.enLocalSymbol),"invalid");
            }
            else
            {
                UpdatePositions(row, TL().get_GET_POSITIONS(uid, ""));
                String2Cell(Cell(row,(int)ColIdx.enLocalSymbol), TL().get_GET_CONTRACT_VAL(uid, 9, "", ""));
            }
            return 1;
        }
        private bool RemoveContract(int idxrow)
        // cancel market data and remove contract from grid
        {
            DeleteRow(idxrow,true);
            return true;
        }
        private bool ShowMarketData(int uidcontract, int idmd,double newval,int size,int row)
        {
            //DateTime begin = DateTime.Now;
            // get column for market data
            int col = G1.MDD2Col(idmd);
            if (col > 0)
            {
               

                bool    colvalid    = ColValid(col);
                bool rowvalid       = RowValid(row, false);

                // import contracts not yet contained, but having market data.
                // this can happen if requested for a chain and market data for whole chain
                //if (colvalid == false || rowvalid == false)
                  //  ContractStatus(uidcontract,-1,0.0);                
                if (colvalid == true && rowvalid == true)
                {
                    SourceGrid.Cells.Cell xcell = Cell(row, col);

                    if (xcell != null)
                    {
                        double prevVal = Cell2Double(xcell);
                        if (prevVal > newval)
                        {
                            xcell.View = G1.GetValDownView(row);
                        }
                        else if (prevVal < newval)
                        {
                            xcell.View = G1.GetValUpView(row);
                        }
                        xcell.Value = newval;
                        this._MDData.InvalidateCell(xcell);
                    }
                    // implicit size update
                    
                    if (size > 0)
                    {
                        col = 0;
                        // bid size
                        if (idmd == 1)
                            col = G1.MDD2Col(0);
                        // last size
                        else if (idmd == 2)
                            col = G1.MDD2Col(3);
                        // ask size
                        else if (idmd == 4)
                            col = G1.MDD2Col(5);
                        if (col > 0)
                        {
                            xcell = Cell(row, col);
                            if (xcell != null)
                            {
                                int prevVal = Cell2Int(xcell);
                                if (prevVal > size)
                                {
                                    xcell.View = G1.GetValDownView(row);
                                }
                                else if (prevVal < size)
                                {
                                    xcell.View = G1.GetValUpView(row);
                                }
                                xcell.Value = size;
                                this._MDData.InvalidateCell(xcell);
                            }
                        }

                    }
                    
                }
                /*
                DateTime end = DateTime.Now;
                TimeSpan diff = new TimeSpan();
                diff = end - begin;
                Console.WriteLine(diff.TotalMilliseconds.ToString());
                */
            }
            return true;
        }
        private bool ShowSize(int uidcontract, int idmd, int size,int row)
        {
            //DateTime begin = DateTime.Now;
            // get column for market data
            int col = G1.MDD2Col(idmd);
            if (col > 0)
            {
               

                bool colvalid = ColValid(col);
                bool rowvalid = RowValid(row, false);

                // import contracts not yet contained, but having market data.
                // this can happen if requested for a chain and market data for whole chain
                //if (!(ColValid(col) == true && RowValid(row, false) == true))
                    //ContractStatus(uidcontract, -1, 0.0);

                if (colvalid == true && rowvalid == true)
                {
                    SourceGrid.Cells.Cell xcell = Cell(row, col);

                    if (xcell != null)
                    {
                        int prevVal = Cell2Int(xcell);
                        if (prevVal > size)
                        {
                            xcell.View = G1.GetValDownView(row);
                        }
                        else if (prevVal < size)
                        {
                            xcell.View = G1.GetValUpView(row);
                        }
                        xcell.Value = size;
                       this._MDData.InvalidateCell(xcell);
                    }
                }
                /*
                DateTime end = DateTime.Now;
                TimeSpan diff = new TimeSpan();
                diff = end - begin;
                Console.WriteLine(diff.TotalMilliseconds.ToString());
                */
            }
            return true;
        }
        #endregion
        #region Trade Actions
        private bool BuyLimit(int idxrow,int idxcol)
        {
            double price = GetPrice(idxcol, idxrow);
            if (price > 0.0)
            {
                int uid = Row2UID(idxrow);
                if (uid > 0)
                {
                    int osize = GetOSize((int) ColIdx.enDOSize, idxrow);
                    if(osize > 0)
                    {
                        
                        if (TL().get_PLACE_ORDER(uid, 0, "BUY", "LMT", osize,price,0.0, "GTC", 1,0) > 0)
                            return true;
                    }
                }
            }
            return false;
        }
        private bool SellLimit(int idxrow, int idxcol)
        {
            double price = GetPrice(idxcol, idxrow);
            if (price > 0.0)
            {
                int uid = Row2UID(idxrow);
                if (uid > 0)
                {
                    int osize = GetOSize((int) ColIdx.enDOSize, idxrow);
                    if (osize > 0)
                    {
                        if (TL().get_PLACE_ORDER(uid, 0, "SELL", "LMT", osize, price, 0.0, "GTC", 1, 0) > 0)
                            return true;
                    }
                }
            }
            return false;
        }
        private bool BuyMkt(int idxrow)
        {
            int uid = Row2UID(idxrow);
            if (uid > 0)
            {
                int osize = GetOSize((int)ColIdx.enDOSize, idxrow);
                if (osize > 0)
                {
                    if (TL().get_PLACE_ORDER(uid, 0, "BUY", "MKT", osize, 0.0, 0.0, "GTC", 1, 0) > 0)
                        return true;
                }
            }
            return false;
        }
        private bool SellMkt(int idxrow)
        {
            int uid = Row2UID(idxrow);
            if (uid > 0)
            {
                int osize = GetOSize((int)ColIdx.enDOSize, idxrow);
                if (osize > 0)
                {
                    if (TL().get_PLACE_ORDER(uid, 0, "SELL", "MKT", osize, 0.0, 0.0, "GTC", 1, 0) > 0)
                        return true;
                }
            }
            return false;
        }

        #endregion

        #region Misc
        private void SetConnected(bool bConnected,string info)
        {
            if (bConnected == true)
            {
                this.BackColor = Color.FromArgb(220, 255, 220);
                this.Text = "TWSLink C# Sample  - " + info;
                SetGridAppearance(1);
            }
            else
            {
                this.BackColor = Color.FromArgb(255, 220, 220);
                this.Text = "TWSLink C# Sample - not connected to TWS";
                SetGridAppearance(0);
            }
        }
        #endregion
        #region Internal
        private twslinkCom TL()
        // create TWSLink COM instance
        {
            if (_twslink == null)                
                // NOTE: if you have a CRASH here, you might
                // have registered 32 bit TWSLink and run the 64 bit cs sample
                // or you have registered 64 bit TWSLink and run the 32 bit cs sample

                // FIX: Switch configuration above from x64 to Any CPU (or win32) or vice versa
                // or do not change configuration, instead unregister 32 bit TWSLink and register 64 bit
                // TWSLink or vice versa

                _twslink = new twslinkCom();
            return _twslink;
        }
        #endregion
        #region guievents
        #region mouseevent
        private void GridMoussClick(object sender, MouseEventArgs e)
        {
            SourceGrid.Position mp = this._MDData.PositionAtPoint(new Point(e.X, e.Y));
            if (mp.IsEmpty())
                return;
            
            // left click
            if (e.Button == MouseButtons.Left)
            {
                // add contract
                if (mp.Column == (int)ColIdx.enAdd)
                {
                    if (this.AddContract(mp.Row) > 0)
                    {
                        // add a new empty row if last row was used for contract registration
                        if (mp.Row == _MDData.RowsCount - 1)
                            this.Contract2Row("", "", "", "", "", "", "0.0", 0, -1, false);
                    }
                }
                // remove contract
                else if (mp.Column == (int)ColIdx.enRem)
                {
                    // don't remove deleting last row
                    if (mp.Row < _MDData.RowsCount - 1)
                        this.RemoveContract(mp.Row);
                }
                // buy limit bid
                else if (mp.Column == (int)ColIdx.enBid)
                    BuyLimit(mp.Row, mp.Column);
                // buy limit last
                else if (mp.Column == (int)ColIdx.enLast)
                    BuyLimit(mp.Row, mp.Column);
                // buy limit ask
                else if (mp.Column == (int)ColIdx.enAsk)
                    BuyLimit(mp.Row, mp.Column);
                // buy market
                else if (mp.Column == (int)ColIdx.enBuyMkt)
                    BuyMkt(mp.Row);
                // sell market
                else if (mp.Column == (int)ColIdx.enSellMkt)
                    SellMkt(mp.Row);
            }
            // right click
            else if (e.Button == MouseButtons.Right)
            {
                // sell limit bid
                if (mp.Column == (int)ColIdx.enBid)
                    SellLimit(mp.Row, mp.Column);
                // sell limit last
                else if (mp.Column == (int)ColIdx.enLast)
                    SellLimit(mp.Row, mp.Column);
                // sell limit ask
                else if (mp.Column == (int)ColIdx.enAsk)
                    SellLimit(mp.Row, mp.Column);
            }

        }
        // close complete portfolio
        private void BTCP_Click(object sender, EventArgs e)
        {
            TL().get_CLOSE_CONTRACT(0, 0.0, "", "");
        }
        #endregion
        // close all cash contracts
        private void BTCC_Click(object sender, EventArgs e)
        {
            //TL().get_CLOSE_CONTRACT(0, 0.0, "", "CASH");
            TL().get_CLOSE_CASH_POSITION("", 0.0, "");
        }
        private void _btConnect_Click(object sender, EventArgs e)
        {
            int Port = String2Int(_edPort.Text, 0);
            int clientId = String2Int(_edClientId.Text, -2);
            if (Port > 0 && clientId > -2)
            {
                Port = TL().get_CONNECT(_edHost.Text, Port, clientId, 0);
            }            
        }

        private void _btDisconnect_Click(object sender, EventArgs e)
        {
            int res = TL().DISCONNECT;
        }
        private void _btHelp_Click(object sender, EventArgs e)
        {
            Help hWnd = new Help();
            hWnd.Show();
        }
        #endregion

    }
}

