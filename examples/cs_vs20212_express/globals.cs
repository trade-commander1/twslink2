using System;
using System.Windows.Forms;
using System.Drawing;
using SourceGrid;

namespace cs
{
    #region Special Cell Style Class
    public class CellBackColorAlternate : SourceGrid.Cells.Views.Cell
    {
        public CellBackColorAlternate(Color firstColor, Color secondColor)
        {
            FirstBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(firstColor);
            SecondBackground = new DevAge.Drawing.VisualElements.BackgroundSolid(secondColor);
        }

        private DevAge.Drawing.VisualElements.IVisualElement mFirstBackground;
        public DevAge.Drawing.VisualElements.IVisualElement FirstBackground
        {
            get { return mFirstBackground; }
            set { mFirstBackground = value; }
        }

        private DevAge.Drawing.VisualElements.IVisualElement mSecondBackground;
        public DevAge.Drawing.VisualElements.IVisualElement SecondBackground
        {
            get { return mSecondBackground; }
            set { mSecondBackground = value; }
        }

        protected override void PrepareView(SourceGrid.CellContext context)
        {
            base.PrepareView(context);

            if (Math.IEEERemainder(context.Position.Row, 2) == 0)
                Background = FirstBackground;
            else
                Background = SecondBackground;
        }
    }
    #endregion
    #region Column Descriptor Class
    public class ColDesc
    {
        public ColDesc(string xname,
                        ColIdx xid,
                        int MarketDataId,
                        int xwidth,
                        SourceGrid.Cells.Views.Cell vh,
                        SourceGrid.Cells.Views.Cell vr,
                        string dtext,
                        SourceGrid.Cells.Editors.TextBox xed,
                        string ttext)
        {
            Name = xname;
            ID = xid;
            MDID = MarketDataId;
            Width = xwidth;
            Width = xwidth;
            DefHView = vh;
            DefRView = vr;
            // DefRHView = vrh;
            defaulttext = dtext;
            edit = xed;
            tooltiptext = ttext;
        }
        public int GetID()
        {
            return (int)ID;
        }
        public SourceGrid.Cells.Views.Cell GetStyle(int rowid, int connected)
        {
            // unchanged
            if (connected < 0)
            {
                if (rowid >= G1.FixedRows)
                    return DefRView;
                else
                    return DefHView;
            }
            // not connected
            else if (connected == 0)
            {
                if (rowid >= G1.FixedRows)
                    return DefRViewNC;
                else
                    return DefHView;
            }
            // right reconnected
            else
            {
                if (rowid >= G1.FixedRows)
                    return DefRViewC;
                else
                    return DefHView;
            }
        }

        public string Name;
        // column index
        public ColIdx ID;
        // id of market data associated with this column
        // (twslink documentation table 6.12)
        public int MDID;
        // column width
        public int Width;
        // default row style
        public SourceGrid.Cells.Views.Cell DefRView;
        // default row style if not connected
        static public SourceGrid.Cells.Views.Cell DefRViewNC;
        // default row style if right reconnected
        static public SourceGrid.Cells.Views.Cell DefRViewC;

        // default column header style
        //public SourceGrid.Cells.Views.ColumnHeader DefHView;
        public SourceGrid.Cells.Views.Cell DefHView;

        // editor of column
        public SourceGrid.Cells.Editors.TextBox edit;
        // default text
        public string defaulttext;
        // tooltiptext
        public string tooltiptext;
    }
#endregion Column Descriptor 
    #region Column Identifiers
    public enum ColIdx
    {
        enAdd = 0,
        enRem,
        enSymbol,
        enType,
        enCurrency,
        enExchange,
        enExpiry,
        enRight,
        enStrike,
        enLocalSymbol,
        enPositions,
        enRPL,
        enURPL,
        enRPLPIPS,
        enURPLPIPS,
        enBid,
        enBidSize,
        enLast,
        enLastSize,
        enAsk,
        enAskSize,
        enOptCallOI,
        enOptPutOI,
        /*
        enBidIV,
        enBidDelta,
        enLastIV,
        enLastDelta,
        enAskIV,
        enAskDelta,
        */
        enClose,
        enOpen,
        enLow,
        enHigh,
        enVolume,
        enRTLow,
        enRTHigh,
        enRTAvg,

        enTicksPM,
        enAvgTicksPM,
        enTotalTicks,

        enRT_OPEN,        // runtime open
        enSEQ_BID,        // bid consecutive up/down ticks
        enSEQ_ASK,        // ask consecutive up/down ticks
        enSEQ_LAST,       // last consecutive up/down ticks
        enSEQ_BID_NAVG,   // bid consecutive up/down ticks n average
        enSEQ_ASK_NAVG,   // ask consecutive up/down ticks n average
        enSEQ_LAST_NAVG,  // last consecutive up/down ticks n average
        enRT_MOM,         // runtime momentum
        enRT_MOM_PIP,     // runtime momentum in Pips
        enRT_ANGLE,       // runtime angle
        enN1_MOM,         // n1 momentum
        enN1_MOM_PIP,     // n1 momentum pip
        enN1_ANGLE,       // n1 angle
        enN2_MOM,         // n2 momentum
        enN2_MOM_PIP,     // n2 momentum pip
        enN2_ANGLE,      
        enN3_MOM,        
        enN3_MOM_PIP,    
        enN3_ANGLE,      
        enN1_AVG,        
        enN2_AVG,        
        enN3_AVG,        

        enDOSize,
        enBuyMkt,
        enSellMkt,
        enData
    };

    #endregion
    /// <summary>
	/// Summary description for globals.
	/// </summary>
    public sealed class G1 
    {
        private G1()
        {
        }
        public static bool Build()
        // never forget to call this function
        {
            // take its allocation status as initialization status
            if (_mdGridCol == null)
            {
                #region dicitionaries
                _mdGridCol = new System.Collections.Generic.Dictionary<int, ColDesc>();
                _mdid2gridCol = new System.Collections.Generic.Dictionary<int, ColDesc>();
                _mdTypeOSize = new System.Collections.Generic.Dictionary<string,int>();
                #endregion
                #region editors
                // 2 decimal point read only
                _edDouble2DPro = new SourceGrid.Cells.Editors.TextBox(typeof(double));
                _edDouble2DPro.EnableEdit = false;
                DevAge.ComponentModel.Converter.NumberTypeConverter FormatDouble2DP;
                FormatDouble2DP = new DevAge.ComponentModel.Converter.NumberTypeConverter(typeof(double));
                FormatDouble2DP.Format = "#.00";
                _edDouble2DPro.TypeConverter = FormatDouble2DP;

                // 6 decimal point read only
                _edDouble6DPro = new SourceGrid.Cells.Editors.TextBox(typeof(double));
                _edDouble6DPro.EnableEdit = false;
                DevAge.ComponentModel.Converter.NumberTypeConverter FormatDouble6DP;
                FormatDouble6DP = new DevAge.ComponentModel.Converter.NumberTypeConverter(typeof(double));
                FormatDouble6DP.Format = "#.000000";
                _edDouble6DPro.TypeConverter = FormatDouble6DP;

                _eCell = new SourceGrid.Cells.Editors.TextBox(typeof(string));
                _eCellRO = new SourceGrid.Cells.Editors.TextBox(typeof(string));
                _eCellRO.EnableEdit = false;

                #endregion
                #region ordersizes
                _mdTypeOSize["CASH"]=30000;
                _mdTypeOSize["STK"]=100;
                _mdTypeOSize["FUT"]=2;
                _mdTypeOSize["OPT"]=2;
                _mdTypeOSize["FOP"]=2;
                #endregion
                #region Special Cell Sytles
                _vPriceDown = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                _vPriceDown.ForeColor = Color.Red;
                _vPriceDown.BackColor = G1.clrBack;
                _vPriceDown.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vPriceDown.Border = cellBorder;

                _vPriceUp = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                _vPriceUp.ForeColor = Color.LightGreen;
                _vPriceUp.BackColor = G1.clrBack;
                _vPriceUp.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vPriceUp.Border = cellBorder;

                _vPosPos = new CellBackColorAlternate(Color.DarkBlue, Color.DarkBlue);
                _vPosPos.ForeColor = Color.Yellow;
                _vPosPos.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vPosPos.Border = cellBorder;

                _vNegPos = new CellBackColorAlternate(Color.DarkRed, Color.DarkRed);
                _vNegPos.ForeColor = Color.Yellow;
                _vNegPos.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vNegPos.Border = cellBorder;

                _vNoPos = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                _vNoPos.ForeColor = Color.Yellow;
                _vNoPos.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vNoPos.Border = cellBorder;


                _vPosPL = new CellBackColorAlternate(Color.FromArgb(0, 102, 204), Color.FromArgb(0, 102, 204));
                _vPosPL.ForeColor = Color.Yellow;
                _vPosPL.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vPosPL.Border = cellBorder;

                _vNegPL = new CellBackColorAlternate(Color.FromArgb(204, 102, 0), Color.FromArgb(204, 102, 0));
                _vNegPL.ForeColor = Color.Yellow;
                _vNegPL.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                _vNegPL.Border = cellBorder;

                _vGridBk = new SourceGrid.Cells.Views.Cell();
                _vGridBk.ForeColor = Color.White;
                _vGridBk.BackColor = Color.Black;
                _vGridBk.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                #endregion
                #region Standard Cell Styles
                // border
                DevAge.Drawing.BorderLine border = new DevAge.Drawing.BorderLine(G1.clrBack, 1);
                cellBorder = new DevAge.Drawing.RectangleBorder(border, border);

                DevAge.Drawing.BorderLine bordernc = new DevAge.Drawing.BorderLine(Color.Pink, 1);
                cellBorderNC = new DevAge.Drawing.RectangleBorder(border, bordernc);                

                // Column header symbol def
                DevAge.Drawing.VisualElements.ColumnHeader backHSymbol = new DevAge.Drawing.VisualElements.ColumnHeader();
                backHSymbol.Border = cellBorder;// DevAge.Drawing.RectangleBorder.NoBorder;
                backHSymbol.BackColor = Color.DarkGreen;

                // Column header market data
                DevAge.Drawing.VisualElements.ColumnHeader backHMDDate = new DevAge.Drawing.VisualElements.ColumnHeader();
                backHMDDate.Border = cellBorder;
                backHMDDate.BackColor = Color.DarkBlue;

                // Column header statistic
                DevAge.Drawing.VisualElements.ColumnHeader backHStat = new DevAge.Drawing.VisualElements.ColumnHeader();
                backHStat.Border = cellBorder;
                backHStat.BackColor = Color.DarkGray;

                // Column header action param
                DevAge.Drawing.VisualElements.ColumnHeader backHap = new DevAge.Drawing.VisualElements.ColumnHeader();
                backHap.Border = cellBorder;
                backHap.BackColor = Color.DeepPink;

                // row header Register
                DevAge.Drawing.VisualElements.RowHeader rowHeadReg = new DevAge.Drawing.VisualElements.RowHeader();
                rowHeadReg.Border = DevAge.Drawing.RectangleBorder.NoBorder;
                rowHeadReg.BackColor = Color.Blue;

                // row header remove
                DevAge.Drawing.VisualElements.RowHeader rowHeadRem = new DevAge.Drawing.VisualElements.RowHeader();
                rowHeadRem.BackColor = Color.Red;

                //backHSymbol.BackgroundColorStyle = DevAge.Drawing.BackgroundColorStyle.None;
                
                #endregion
                #region columndescriptors
                // (if you want to extend grid with columns -> extend ColIdx and insert a column definition block here.)

                // connection dependent static styles
                ColDesc.DefRViewNC = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                ColDesc.DefRViewNC.ForeColor = Color.FromArgb(80, 80, 80);
                ColDesc.DefRViewNC.BackColor = G1.clrBackSym;
                ColDesc.DefRViewNC.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                ColDesc.DefRViewNC.Border = cellBorderNC;

                ColDesc.DefRViewC = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                ColDesc.DefRViewC.ForeColor = Color.White;
                ColDesc.DefRViewC.BackColor = G1.clrBackSym;
                ColDesc.DefRViewC.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                ColDesc.DefRViewC.Border = cellBorder;

                // add colum
                SourceGrid.Cells.Views.Cell cviewh = new SourceGrid.Cells.Views.Cell();// ColumnHeader();
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewh.BackColor = G1.clrBack;
                cviewh.ForeColor = G1.clrBack;
                cviewh.Border = cellBorder;

                SourceGrid.Cells.Views.Cell cviewr = new SourceGrid.Cells.Views.RowHeader();
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewr.Background = rowHeadReg;
                cviewr.ForeColor = Color.White;
                cviewr.Border = cellBorder;

                ColDesc col = new ColDesc("", ColIdx.enAdd,-1,80,cviewh,cviewr,"Register",_eCellRO,"");
                G1.SetMDId(col);                

                // remove colum
                cviewh = new SourceGrid.Cells.Views.Cell();
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewh.BackColor = G1.clrBack;
                cviewh.ForeColor = G1.clrBack;
                cviewh.Border = cellBorder;

                cviewr = new SourceGrid.Cells.Views.RowHeader();
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewr.Background = rowHeadRem;
                cviewr.ForeColor = Color.White;
                cviewr.Border = cellBorder;
                col = new ColDesc("", ColIdx.enRem, -1, 20, cviewh, cviewr,  "x",_eCellRO,"");
                G1.SetMDId(col);

                // symbol column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewh.Background = backHSymbol;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Symbol", ColIdx.enSymbol, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // type column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Type", ColIdx.enType, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // currency column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Currency", ColIdx.enCurrency, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // exchange column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Exchange", ColIdx.enExchange, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // expiry column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Expiry", ColIdx.enExpiry, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // right column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Right", ColIdx.enRight, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // strike column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Strike", ColIdx.enStrike, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // local symbol
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.Yellow;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.Yellow;
                cviewr.BackColor = G1.clrBackSym;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Local Symbol", ColIdx.enLocalSymbol,-1, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // positions colum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.Yellow;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.Yellow;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Positions", ColIdx.enPositions,-1, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // r pl colum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.Yellow;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.Yellow;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;
                col = new ColDesc("R. P&L", ColIdx.enRPL, -1, 80, cviewh, cviewr, "", _edDouble2DPro,"");
                G1.SetMDId(col);

                // ur pl colum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.Yellow;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.Yellow;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;
                col = new ColDesc("UR. P&L", ColIdx.enURPL, -1, 80, cviewh, cviewr, "", _edDouble2DPro,"");
                G1.SetMDId(col);

                // r pl colum pips
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.Yellow;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.Yellow;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;
                col = new ColDesc("R. P&L p", ColIdx.enRPLPIPS, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // ur pl colum pips
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.Yellow;
                cviewh.Background = backHSymbol;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.Yellow;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;
                col = new ColDesc("UR. P&L p ", ColIdx.enURPLPIPS, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // bid column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Bid", ColIdx.enBid,IDBid, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // bid size column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Bid Size", ColIdx.enBidSize,IDBidS, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // last column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Last", ColIdx.enLast, IDLast,80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // last size column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Last Size", ColIdx.enLastSize,IDLastS, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // ask column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Ask", ColIdx.enAsk,IDAsk, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // ask size column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Ask Size", ColIdx.enAskSize,IDAskS, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // option call open interest column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Opt. Call OI", ColIdx.enOptCallOI,IDOptCallOI, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);


                // option put open interest column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHMDDate;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Opt. Put OI", ColIdx.enOptPutOI,IDOptPutOI, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // close column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                
                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Close", ColIdx.enClose,IDClose, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // open column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Open", ColIdx.enOpen,IDOpen, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // low column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Low", ColIdx.enLow,IDLow, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // high column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("High", ColIdx.enHigh,IDHigh, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // volume column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Volume", ColIdx.enVolume,IDVolume, 80,cviewh,cviewr,"",_eCellRO,"");
                G1.SetMDId(col);

                // runtime Low
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT Low", ColIdx.enRTLow, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // runtime High
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT High", ColIdx.enRTHigh, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // runtime Average
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT Avg.", ColIdx.enRTAvg, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                
                // ticks/minute
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Ticks/Min.", ColIdx.enTicksPM, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // avg. ticks/minute
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Avg. Ticks/Min.", ColIdx.enAvgTicksPM, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // total bid,last and ask ticks
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("Total Ticks", ColIdx.enTotalTicks, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // runtime open
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT. Open", ColIdx.enRT_OPEN, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // consecutive bid ticks
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("CST. Bid", ColIdx.enSEQ_BID, -1, 80, cviewh, cviewr, "", _eCellRO,"Consecutive Up/Down Bid Ticks");
                G1.SetMDId(col);

                // consecutive ask ticks
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("CST. Ask", ColIdx.enSEQ_ASK, -1, 80, cviewh, cviewr, "", _eCellRO, "Consecutive Up/Down Ask Ticks");
                G1.SetMDId(col);

                // consecutive last ticks
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("CST. Last", ColIdx.enSEQ_LAST, -1, 80, cviewh, cviewr, "", _eCellRO, "Consecutive Up/Down Last Ticks");
                G1.SetMDId(col);

                // consecutive bid ticks n1 avg.
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("CST. n1a Bid", ColIdx.enSEQ_BID_NAVG, -1, 80, cviewh, cviewr, "", _edDouble2DPro, "Consecutive Up/Down Bid n1 Average Ticks");
                G1.SetMDId(col);

                // consecutive ask ticks n1 avg.
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("CST. n1a Ask", ColIdx.enSEQ_ASK_NAVG, -1, 80, cviewh, cviewr, "", _edDouble2DPro, "Consecutive Up/Down Ask n1 Average Ticks");
                G1.SetMDId(col);

                // consecutive last ticks n1 avg.
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("CST. n1a Last", ColIdx.enSEQ_LAST_NAVG, -1, 80, cviewh, cviewr, "", _edDouble2DPro, "Consecutive Up/Down Last n1 Average Ticks");
                G1.SetMDId(col);

                // runtime momentum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                // cviewr.Clone() would have been smarter...
                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT Mom.", ColIdx.enRT_MOM, -1, 80, cviewh, cviewr, "", _edDouble2DPro,"");
                G1.SetMDId(col);

                // runtime momentum in pips
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT Mom.P.", ColIdx.enRT_MOM_PIP, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // runtime angle
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("RT Agl.", ColIdx.enRT_ANGLE, -1, 80, cviewh, cviewr, "",_edDouble2DPro,"");// _eCellRO,"");
                G1.SetMDId(col);

                // n1 momentum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N1 Mom.", ColIdx.enN1_MOM, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // n1 momentum in pips
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N1 Mom.P.", ColIdx.enN1_MOM_PIP, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // n1 angle
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N1 Agl.", ColIdx.enN1_ANGLE, -1, 80, cviewh, cviewr, "", _edDouble2DPro,"");
                G1.SetMDId(col);

                // n2 momentum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N2 Mom.", ColIdx.enN2_MOM, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // n2 momentum in pips
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N2 Mom.P.", ColIdx.enN2_MOM_PIP, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // n2 angle
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N2 Agl.", ColIdx.enN2_ANGLE, -1, 80, cviewh, cviewr, "", _edDouble2DPro,"");
                G1.SetMDId(col);

                // n3 momentum
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N3 Mom.", ColIdx.enN3_MOM, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // n3 momentum in pips
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N3 Mom.P.", ColIdx.enN3_MOM_PIP, -1, 80, cviewh, cviewr, "", _eCellRO,"");
                G1.SetMDId(col);

                // n3 angle
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N3 Agl.", ColIdx.enN3_ANGLE, -1, 80, cviewh, cviewr, "", _edDouble2DPro,"");
                G1.SetMDId(col);

                // n1 ask average
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N1 Avg.", ColIdx.enN1_AVG, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // n2 ask average
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N2 Avg.", ColIdx.enN2_AVG, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // n3 ask average
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.DarkBlue;
                cviewh.Background = backHStat;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack, G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("N3 Avg.", ColIdx.enN3_AVG, -1, 80, cviewh, cviewr, "", _edDouble6DPro,"");
                G1.SetMDId(col);

                // default order size column
                cviewh = new SourceGrid.Cells.Views.ColumnHeader();
                cviewh.ForeColor = Color.White;
                cviewh.Background = backHap;
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("O.Size", ColIdx.enDOSize, -1, 80, cviewh, cviewr, "", _eCell, "");
                G1.SetMDId(col);

                // buy mkt column
                cviewh = new SourceGrid.Cells.Views.Cell();// ColumnHeader();
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewh.BackColor = G1.clrBack;
                cviewh.ForeColor = G1.clrBack;
                cviewh.Border = cellBorder;

                cviewr = new SourceGrid.Cells.Views.RowHeader();
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewr.Background = rowHeadReg;
                cviewr.ForeColor = Color.White;
                cviewr.Border = cellBorder;

                col = new ColDesc("", ColIdx.enBuyMkt, -1, 80, cviewh, cviewr,  "Buy Mkt.",_eCellRO,"");
                G1.SetMDId(col);

                // sell mkt column
                cviewh = new SourceGrid.Cells.Views.Cell();// ColumnHeader();
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewh.BackColor = G1.clrBack;
                cviewh.ForeColor = G1.clrBack;
                cviewh.Border = cellBorder;

                cviewr = new SourceGrid.Cells.Views.RowHeader();
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewr.Background = rowHeadRem;
                cviewr.ForeColor = Color.White;
                cviewr.Border = cellBorder;

                col = new ColDesc("", ColIdx.enSellMkt, -1, 80, cviewh, cviewr, "Sell Mkt.",_eCellRO,"");
                G1.SetMDId(col);

                // contract uid  column
                cviewh = new SourceGrid.Cells.Views.Cell();// ColumnHeader();
                cviewh.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
                cviewh.BackColor = G1.clrBack;
                cviewh.ForeColor = G1.clrBack;
                cviewh.Border = cellBorder;

                cviewr = new CellBackColorAlternate(G1.clrBack,G1.clrBack2);
                cviewr.ForeColor = Color.White;
                cviewr.BackColor = G1.clrBack;
                cviewr.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight;
                cviewr.Border = cellBorder;

                col = new ColDesc("", ColIdx.enData, -1, 0, cviewh, cviewr, "",_eCellRO,"");
                G1.SetMDId(col);

                #endregion            
            }
            return true;
        }
        #region Private Service Methods
        private static void SetMDId(ColDesc col)
        {
            if (col != null)
            {
                if (col.MDID >= 0)
                    _mdid2gridCol[col.MDID] = col;
                _mdGridCol[col.GetID()] = col;
            }
        }
        #endregion
        #region Public Service Methods
        public static int MDD2Col(int marketdataid)
        {
            ColDesc val = null;
            try
            {
                if (_mdid2gridCol.TryGetValue(marketdataid, out val))
                    return val.GetID();
            }
            catch (ArgumentNullException)
            {
                ;//Console.WriteLine("Unable to retrieve value for null key.");
            }
            return -1;
        }
        public static ColDesc GetCol(int idx)
        {
            ColDesc val = null;
            try
            {
                if (_mdGridCol.TryGetValue(idx, out val))
                    return val;
            }
            catch (ArgumentNullException)
            {
                ;//Console.WriteLine("Unable to retrieve value for null key.");
            }
            return null;
        }
        public static int GetOSize(string sectype)
        {
            int val = 0;
            try
            {
                if (_mdTypeOSize.TryGetValue(sectype, out val))
                    return val;
            }
            catch (ArgumentNullException)
            {
                ;//Console.WriteLine("Unable to retrieve value for null key.");
            }
            return 0;
        }
        public static void SetSelectionStyle(SourceGrid.Grid sg)
        {
            if (sg != null)
            {
                DevAge.Drawing.RectangleBorder SelBorder = new DevAge.Drawing.RectangleBorder();
                SelBorder.SetColor(Color.LightGray);
                SelBorder.SetWidth(1);
                sg.Selection.BackColor = Color.FromArgb(58, Color.FromArgb(255,0,0,128));
                sg.Selection.FocusBackColor = Color.FromArgb(30, Color.Red);
                sg.Selection.Border = SelBorder;
            }
        }
        public static int GetColumnCount()
        {
            return _mdGridCol.Count;
        }
        public static SourceGrid.Cells.Views.Cell GetValUpView(int roidx)
        {
            return _vPriceUp;
        }
        public static SourceGrid.Cells.Views.Cell GetValDownView(int roidx)
        {
            return _vPriceDown;
        }
        public static SourceGrid.Cells.Views.Cell GetPosView(int roidx, double Pos)
        {
            if (Pos == 0.0)
            {
                return _vNoPos;
            }
            else if (Pos > 0.0)
            {
                return _vPosPos;
            }
            else
            {
                return _vNegPos;
            }
        }
        public static SourceGrid.Cells.Views.Cell GetDoubleView(double x)
        {
            if (x == 0)
            {
                return _vNoPos;
            }
            else if (x > 0.0)
            {
                return _vPriceUp;
            }
            else
            {
                return _vPriceDown;
            }
        }
        public static SourceGrid.Cells.Views.Cell GetIntView(int x)
        {
            if (x == 0)
            {
                return _vNoPos;
            }
            else if (x > 0)
            {
                return _vPriceUp;
            }
            else
            {
                return _vPriceDown;
            }
        }
        public static SourceGrid.Cells.Views.Cell GetStringView(string x)
        {
            if (x.Length == 0)
            {
                return _vNoPos;
            }
            else if (x[0] == '-')
            {
                return _vPriceDown; 
            }
            else
            {
                return _vPriceUp;
            }
        }
        public static SourceGrid.Cells.Views.Cell GetStringViewPL(int x)
        {
            if (x < 0)
            {
                return _vNegPL; 
            }
            else if (x > 0)
            {
                return _vPosPL;
            }
            else
                return _vNoPos;
        }        
        #endregion
        #region static members
        #region const and static members
        public const int _formwidth = 1270;
        public const int _formheight = 460;
        public const int _gridpadd = 2;
        // grid distance form form bottom
        public const int _gridBottomDistance = 30;
        // gui elem size
        public static int _LBMessageWidth = 731;
        public static int _LBMessageHeight = 43;
        public static int _TBWidth = 114;
        public static int _TBHeight = 43;
        public static int _BTWidth = 189;
        public static int _BTHeight = 22;

        public static Color clrBack     = Color.FromArgb(10,10,10);
        public static Color clrBack2 = Color.FromArgb(60, 60, 60);
        public static Color clrBackSym = Color.Magenta;
        public static Color clrBackSym2 = Color.DarkMagenta;
        public static Color clrBackStat = Color.FromArgb(230, 240, 200);
        public static Color clrBackStat2 = Color.FromArgb(180,190,150);

        public static int FixedRows = 1;
        public static int FixedColumns = 2;
        // font height header
        public static int _HFontHeight = 9;
        // font height Data
        public static int _DFontHeight = 8;
        public static string _defaultFont = "Verdana";

        #endregion
        #region datagrid cellstyles

        public static SourceGrid.Cells.Views.Cell _vPriceUp;
        public static SourceGrid.Cells.Views.Cell _vPriceDown;


        public static SourceGrid.Cells.Views.Cell _vPosPos;
        public static SourceGrid.Cells.Views.Cell _vNegPos;
        public static SourceGrid.Cells.Views.Cell _vNoPos;

        public static SourceGrid.Cells.Views.Cell _vPosPL;
        public static SourceGrid.Cells.Views.Cell _vNegPL;

        public static SourceGrid.Cells.Views.Cell _vGridBk;

        public static SourceGrid.Cells.Editors.TextBox _eCell;
        // read only editor
        public static SourceGrid.Cells.Editors.TextBox _eCellRO;
        // default cell border
        private static DevAge.Drawing.RectangleBorder cellBorder;
        // default cell border not connected
        private static DevAge.Drawing.RectangleBorder cellBorderNC;
        // default selection border
        public static DevAge.Drawing.RectangleBorder _SelBorder; 
        // default selection
        public static SourceGrid.Selection.SelectionBase _SelBase;

        // md
        public const int IDBid = 1;
        public const int IDBidS = 0;
        public const int IDLast = 4;
        public const int IDLastS = 5;
        public const int IDAsk = 2;
        public const int IDAskS = 3;
        public const int IDHigh = 6;
        public const int IDLow = 7;
        public const int IDVolume = 8;
        public const int IDClose = 9;
        public const int IDOpen = 14;
        public const int IDOptCallOI = 27;
        public const int IDOptPutOI = 28;

        // maps market data id to grid column
        private static System.Collections.Generic.Dictionary<int, ColDesc> _mdid2gridCol;
        // maps market data id to grid col description
        public static System.Collections.Generic.Dictionary<int,ColDesc> _mdGridCol;
        // maps market sectype to default ordersize
        public static System.Collections.Generic.Dictionary<string,int> _mdTypeOSize;
        ///
        /// editors
        ///
        // double editor 2 decimal points
        public static SourceGrid.Cells.Editors.TextBox _edDouble2DPro;
        // double editor 6 decimal points
        public static SourceGrid.Cells.Editors.TextBox _edDouble6DPro;

    #endregion
        #endregion
    }
}

