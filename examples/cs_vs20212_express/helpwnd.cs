using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cs
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            InitMember();
        }
        private void InitMember()
        {
            _edHelp.Text = "- Left Click on Price Cell for Buy\r\n";
            _edHelp.Text += "- Right Click on Price Cell for Sell\r\n";
            _edHelp.Text += "- To add a Contract, type Properties in last empty Row and click <Register>\r\n";
            _edHelp.Text += "- To make a Chain request, let some Contract Fields empty\r\n";
            _edHelp.Text += "- <Close Cash Positions> closes all Cash Balances of Market Value Grid (TWS)\r\n";
        }

    }
}