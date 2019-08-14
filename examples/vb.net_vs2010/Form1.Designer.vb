<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.edConnStatus = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.btConnect = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.edPort = New System.Windows.Forms.TextBox
        Me.edIP = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.edPositions = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.edConid = New System.Windows.Forms.TextBox
        Me.edLocalSymbol = New System.Windows.Forms.TextBox
        Me.btRegister = New System.Windows.Forms.Button
        Me.edUID = New System.Windows.Forms.TextBox
        Me.edMultiplier = New System.Windows.Forms.TextBox
        Me.edStrike = New System.Windows.Forms.TextBox
        Me.cbRight = New System.Windows.Forms.ComboBox
        Me.dtExpiry = New System.Windows.Forms.DateTimePicker
        Me.edExchange = New System.Windows.Forms.TextBox
        Me.edCurrency = New System.Windows.Forms.TextBox
        Me.cbType = New System.Windows.Forms.ComboBox
        Me.edSymbol = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.edAccount = New System.Windows.Forms.TextBox
        Me.edMaintenance = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.edNL = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btRequestMarketData = New System.Windows.Forms.Button
        Me.edAskSize = New System.Windows.Forms.TextBox
        Me.edLastSize = New System.Windows.Forms.TextBox
        Me.edAsk = New System.Windows.Forms.TextBox
        Me.edLast = New System.Windows.Forms.TextBox
        Me.edBidSize = New System.Windows.Forms.TextBox
        Me.edBid = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.edOrderStatus = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.cbSide = New System.Windows.Forms.ComboBox
        Me.edOid = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.btPlaceOrder = New System.Windows.Forms.Button
        Me.edTif = New System.Windows.Forms.TextBox
        Me.edSize = New System.Windows.Forms.TextBox
        Me.edStop = New System.Windows.Forms.TextBox
        Me.edLimit = New System.Windows.Forms.TextBox
        Me.cbOrderType = New System.Windows.Forms.ComboBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.edMessages = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.edConnStatus)
        Me.GroupBox1.Controls.Add(Me.Label28)
        Me.GroupBox1.Controls.Add(Me.btConnect)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.edPort)
        Me.GroupBox1.Controls.Add(Me.edIP)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(11, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(384, 77)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Connect to TWS"
        '
        'edConnStatus
        '
        Me.edConnStatus.Location = New System.Drawing.Point(196, 41)
        Me.edConnStatus.Name = "edConnStatus"
        Me.edConnStatus.ReadOnly = True
        Me.edConnStatus.Size = New System.Drawing.Size(94, 20)
        Me.edConnStatus.TabIndex = 7
        Me.edConnStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(195, 22)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(37, 13)
        Me.Label28.TabIndex = 6
        Me.Label28.Text = "Status"
        '
        'btConnect
        '
        Me.btConnect.Location = New System.Drawing.Point(294, 39)
        Me.btConnect.Name = "btConnect"
        Me.btConnect.Size = New System.Drawing.Size(84, 24)
        Me.btConnect.TabIndex = 4
        Me.btConnect.Text = "Connect"
        Me.btConnect.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(130, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Port"
        '
        'edPort
        '
        Me.edPort.Location = New System.Drawing.Point(127, 41)
        Me.edPort.Name = "edPort"
        Me.edPort.Size = New System.Drawing.Size(59, 20)
        Me.edPort.TabIndex = 2
        Me.edPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edIP
        '
        Me.edIP.Location = New System.Drawing.Point(7, 41)
        Me.edIP.Name = "edIP"
        Me.edIP.Size = New System.Drawing.Size(116, 20)
        Me.edIP.TabIndex = 1
        Me.edIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "IP"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.edPositions)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.edConid)
        Me.GroupBox2.Controls.Add(Me.edLocalSymbol)
        Me.GroupBox2.Controls.Add(Me.btRegister)
        Me.GroupBox2.Controls.Add(Me.edUID)
        Me.GroupBox2.Controls.Add(Me.edMultiplier)
        Me.GroupBox2.Controls.Add(Me.edStrike)
        Me.GroupBox2.Controls.Add(Me.cbRight)
        Me.GroupBox2.Controls.Add(Me.dtExpiry)
        Me.GroupBox2.Controls.Add(Me.edExchange)
        Me.GroupBox2.Controls.Add(Me.edCurrency)
        Me.GroupBox2.Controls.Add(Me.cbType)
        Me.GroupBox2.Controls.Add(Me.edSymbol)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(11, 95)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(828, 99)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Contract- / Symbol Specification"
        '
        'edPositions
        '
        Me.edPositions.BackColor = System.Drawing.Color.Black
        Me.edPositions.ForeColor = System.Drawing.Color.White
        Me.edPositions.Location = New System.Drawing.Point(582, 73)
        Me.edPositions.Name = "edPositions"
        Me.edPositions.ReadOnly = True
        Me.edPositions.Size = New System.Drawing.Size(121, 20)
        Me.edPositions.TabIndex = 22
        Me.edPositions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(520, 75)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(49, 13)
        Me.Label30.TabIndex = 21
        Me.Label30.Text = "Positions"
        '
        'edConid
        '
        Me.edConid.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.edConid.Location = New System.Drawing.Point(99, 68)
        Me.edConid.Name = "edConid"
        Me.edConid.ReadOnly = True
        Me.edConid.Size = New System.Drawing.Size(63, 20)
        Me.edConid.TabIndex = 20
        Me.edConid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edLocalSymbol
        '
        Me.edLocalSymbol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.edLocalSymbol.Location = New System.Drawing.Point(10, 68)
        Me.edLocalSymbol.Name = "edLocalSymbol"
        Me.edLocalSymbol.ReadOnly = True
        Me.edLocalSymbol.Size = New System.Drawing.Size(82, 20)
        Me.edLocalSymbol.TabIndex = 19
        Me.edLocalSymbol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btRegister
        '
        Me.btRegister.Location = New System.Drawing.Point(733, 40)
        Me.btRegister.Name = "btRegister"
        Me.btRegister.Size = New System.Drawing.Size(84, 24)
        Me.btRegister.TabIndex = 18
        Me.btRegister.Text = "Register"
        Me.btRegister.UseVisualStyleBackColor = True
        '
        'edUID
        '
        Me.edUID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.edUID.Location = New System.Drawing.Point(644, 41)
        Me.edUID.Name = "edUID"
        Me.edUID.ReadOnly = True
        Me.edUID.Size = New System.Drawing.Size(59, 20)
        Me.edUID.TabIndex = 17
        Me.edUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edMultiplier
        '
        Me.edMultiplier.Location = New System.Drawing.Point(581, 41)
        Me.edMultiplier.Name = "edMultiplier"
        Me.edMultiplier.Size = New System.Drawing.Size(57, 20)
        Me.edMultiplier.TabIndex = 16
        Me.edMultiplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edStrike
        '
        Me.edStrike.Location = New System.Drawing.Point(518, 41)
        Me.edStrike.Name = "edStrike"
        Me.edStrike.Size = New System.Drawing.Size(56, 20)
        Me.edStrike.TabIndex = 15
        Me.edStrike.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbRight
        '
        Me.cbRight.FormattingEnabled = True
        Me.cbRight.Location = New System.Drawing.Point(453, 41)
        Me.cbRight.Name = "cbRight"
        Me.cbRight.Size = New System.Drawing.Size(57, 21)
        Me.cbRight.TabIndex = 14
        '
        'dtExpiry
        '
        Me.dtExpiry.CustomFormat = ""
        Me.dtExpiry.Location = New System.Drawing.Point(338, 41)
        Me.dtExpiry.Name = "dtExpiry"
        Me.dtExpiry.Size = New System.Drawing.Size(108, 20)
        Me.dtExpiry.TabIndex = 13
        '
        'edExchange
        '
        Me.edExchange.Location = New System.Drawing.Point(228, 41)
        Me.edExchange.Name = "edExchange"
        Me.edExchange.Size = New System.Drawing.Size(103, 20)
        Me.edExchange.TabIndex = 12
        Me.edExchange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edCurrency
        '
        Me.edCurrency.Location = New System.Drawing.Point(168, 41)
        Me.edCurrency.Name = "edCurrency"
        Me.edCurrency.Size = New System.Drawing.Size(54, 20)
        Me.edCurrency.TabIndex = 11
        Me.edCurrency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbType
        '
        Me.cbType.FormattingEnabled = True
        Me.cbType.Location = New System.Drawing.Point(99, 41)
        Me.cbType.Name = "cbType"
        Me.cbType.Size = New System.Drawing.Size(63, 21)
        Me.cbType.TabIndex = 10
        '
        'edSymbol
        '
        Me.edSymbol.Location = New System.Drawing.Point(10, 41)
        Me.edSymbol.Name = "edSymbol"
        Me.edSymbol.Size = New System.Drawing.Size(83, 20)
        Me.edSymbol.TabIndex = 9
        Me.edSymbol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(641, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(26, 13)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "UID"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(579, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Multiplier"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(516, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "Strike"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(450, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Right"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(337, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Expiry"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(228, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Exchange"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(167, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Currency"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(100, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Type"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Symbol"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.edAccount)
        Me.GroupBox3.Controls.Add(Me.edMaintenance)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.edNL)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Location = New System.Drawing.Point(401, 7)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(437, 77)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Account Data"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(237, 21)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(47, 13)
        Me.Label29.TabIndex = 5
        Me.Label29.Text = "Account"
        '
        'edAccount
        '
        Me.edAccount.Location = New System.Drawing.Point(238, 40)
        Me.edAccount.Name = "edAccount"
        Me.edAccount.ReadOnly = True
        Me.edAccount.Size = New System.Drawing.Size(100, 20)
        Me.edAccount.TabIndex = 4
        Me.edAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edMaintenance
        '
        Me.edMaintenance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.edMaintenance.Location = New System.Drawing.Point(126, 40)
        Me.edMaintenance.Name = "edMaintenance"
        Me.edMaintenance.ReadOnly = True
        Me.edMaintenance.Size = New System.Drawing.Size(105, 20)
        Me.edMaintenance.TabIndex = 3
        Me.edMaintenance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(125, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(104, 13)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "Maintenance Margin"
        '
        'edNL
        '
        Me.edNL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.edNL.Location = New System.Drawing.Point(13, 40)
        Me.edNL.Name = "edNL"
        Me.edNL.ReadOnly = True
        Me.edNL.Size = New System.Drawing.Size(105, 20)
        Me.edNL.TabIndex = 1
        Me.edNL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(11, 21)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(108, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Net Liquidation Value"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GroupBox4.Controls.Add(Me.btRequestMarketData)
        Me.GroupBox4.Controls.Add(Me.edAskSize)
        Me.GroupBox4.Controls.Add(Me.edLastSize)
        Me.GroupBox4.Controls.Add(Me.edAsk)
        Me.GroupBox4.Controls.Add(Me.edLast)
        Me.GroupBox4.Controls.Add(Me.edBidSize)
        Me.GroupBox4.Controls.Add(Me.edBid)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox4.Location = New System.Drawing.Point(11, 201)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(828, 99)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Market Data"
        '
        'btRequestMarketData
        '
        Me.btRequestMarketData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btRequestMarketData.Location = New System.Drawing.Point(733, 24)
        Me.btRequestMarketData.Name = "btRequestMarketData"
        Me.btRequestMarketData.Size = New System.Drawing.Size(84, 24)
        Me.btRequestMarketData.TabIndex = 12
        Me.btRequestMarketData.Text = "Request Data"
        Me.btRequestMarketData.UseVisualStyleBackColor = True
        '
        'edAskSize
        '
        Me.edAskSize.BackColor = System.Drawing.Color.Black
        Me.edAskSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edAskSize.ForeColor = System.Drawing.Color.White
        Me.edAskSize.Location = New System.Drawing.Point(227, 72)
        Me.edAskSize.Name = "edAskSize"
        Me.edAskSize.ReadOnly = True
        Me.edAskSize.Size = New System.Drawing.Size(70, 23)
        Me.edAskSize.TabIndex = 11
        Me.edAskSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edLastSize
        '
        Me.edLastSize.BackColor = System.Drawing.Color.Black
        Me.edLastSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edLastSize.ForeColor = System.Drawing.Color.White
        Me.edLastSize.Location = New System.Drawing.Point(151, 72)
        Me.edLastSize.Name = "edLastSize"
        Me.edLastSize.ReadOnly = True
        Me.edLastSize.Size = New System.Drawing.Size(70, 23)
        Me.edLastSize.TabIndex = 10
        Me.edLastSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edAsk
        '
        Me.edAsk.BackColor = System.Drawing.Color.Black
        Me.edAsk.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edAsk.ForeColor = System.Drawing.Color.White
        Me.edAsk.Location = New System.Drawing.Point(227, 27)
        Me.edAsk.Name = "edAsk"
        Me.edAsk.ReadOnly = True
        Me.edAsk.Size = New System.Drawing.Size(70, 23)
        Me.edAsk.TabIndex = 9
        Me.edAsk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edLast
        '
        Me.edLast.BackColor = System.Drawing.Color.Black
        Me.edLast.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edLast.ForeColor = System.Drawing.Color.White
        Me.edLast.Location = New System.Drawing.Point(152, 27)
        Me.edLast.Name = "edLast"
        Me.edLast.ReadOnly = True
        Me.edLast.Size = New System.Drawing.Size(70, 23)
        Me.edLast.TabIndex = 8
        Me.edLast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edBidSize
        '
        Me.edBidSize.BackColor = System.Drawing.Color.Black
        Me.edBidSize.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edBidSize.ForeColor = System.Drawing.Color.White
        Me.edBidSize.Location = New System.Drawing.Point(75, 72)
        Me.edBidSize.Name = "edBidSize"
        Me.edBidSize.ReadOnly = True
        Me.edBidSize.Size = New System.Drawing.Size(70, 23)
        Me.edBidSize.TabIndex = 7
        Me.edBidSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edBid
        '
        Me.edBid.BackColor = System.Drawing.Color.Black
        Me.edBid.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edBid.ForeColor = System.Drawing.Color.White
        Me.edBid.Location = New System.Drawing.Point(76, 27)
        Me.edBid.Name = "edBid"
        Me.edBid.ReadOnly = True
        Me.edBid.Size = New System.Drawing.Size(70, 23)
        Me.edBid.TabIndex = 6
        Me.edBid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(226, 55)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(48, 13)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "Ask Size"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(226, 11)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(25, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Ask"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(149, 55)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 13)
        Me.Label15.TabIndex = 3
        Me.Label15.Text = "Last Size"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(149, 11)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(27, 13)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Last"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(74, 55)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Bid Size"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(76, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(22, 13)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Bid"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label27)
        Me.GroupBox5.Controls.Add(Me.edOrderStatus)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Controls.Add(Me.cbSide)
        Me.GroupBox5.Controls.Add(Me.edOid)
        Me.GroupBox5.Controls.Add(Me.Label25)
        Me.GroupBox5.Controls.Add(Me.btPlaceOrder)
        Me.GroupBox5.Controls.Add(Me.edTif)
        Me.GroupBox5.Controls.Add(Me.edSize)
        Me.GroupBox5.Controls.Add(Me.edStop)
        Me.GroupBox5.Controls.Add(Me.edLimit)
        Me.GroupBox5.Controls.Add(Me.cbOrderType)
        Me.GroupBox5.Controls.Add(Me.Label24)
        Me.GroupBox5.Controls.Add(Me.Label23)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.Label21)
        Me.GroupBox5.Controls.Add(Me.Label20)
        Me.GroupBox5.ForeColor = System.Drawing.Color.Black
        Me.GroupBox5.Location = New System.Drawing.Point(11, 306)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(828, 71)
        Me.GroupBox5.TabIndex = 4
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Place Order"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(16, 20)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(28, 13)
        Me.Label27.TabIndex = 17
        Me.Label27.Text = "Side"
        '
        'edOrderStatus
        '
        Me.edOrderStatus.Location = New System.Drawing.Point(600, 41)
        Me.edOrderStatus.Name = "edOrderStatus"
        Me.edOrderStatus.ReadOnly = True
        Me.edOrderStatus.Size = New System.Drawing.Size(114, 20)
        Me.edOrderStatus.TabIndex = 16
        Me.edOrderStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(597, 21)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(37, 13)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "Status"
        '
        'cbSide
        '
        Me.cbSide.FormattingEnabled = True
        Me.cbSide.Location = New System.Drawing.Point(14, 41)
        Me.cbSide.Name = "cbSide"
        Me.cbSide.Size = New System.Drawing.Size(72, 21)
        Me.cbSide.TabIndex = 14
        '
        'edOid
        '
        Me.edOid.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.edOid.Location = New System.Drawing.Point(529, 41)
        Me.edOid.Name = "edOid"
        Me.edOid.Size = New System.Drawing.Size(65, 20)
        Me.edOid.TabIndex = 13
        Me.edOid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(527, 21)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(23, 13)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Oid"
        '
        'btPlaceOrder
        '
        Me.btPlaceOrder.Location = New System.Drawing.Point(733, 40)
        Me.btPlaceOrder.Name = "btPlaceOrder"
        Me.btPlaceOrder.Size = New System.Drawing.Size(84, 24)
        Me.btPlaceOrder.TabIndex = 11
        Me.btPlaceOrder.Text = "Place Order"
        Me.btPlaceOrder.UseVisualStyleBackColor = True
        '
        'edTif
        '
        Me.edTif.Location = New System.Drawing.Point(445, 41)
        Me.edTif.Name = "edTif"
        Me.edTif.Size = New System.Drawing.Size(72, 20)
        Me.edTif.TabIndex = 10
        Me.edTif.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edSize
        '
        Me.edSize.Location = New System.Drawing.Point(367, 41)
        Me.edSize.Name = "edSize"
        Me.edSize.Size = New System.Drawing.Size(72, 20)
        Me.edSize.TabIndex = 9
        Me.edSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edStop
        '
        Me.edStop.Location = New System.Drawing.Point(289, 41)
        Me.edStop.Name = "edStop"
        Me.edStop.Size = New System.Drawing.Size(72, 20)
        Me.edStop.TabIndex = 8
        Me.edStop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'edLimit
        '
        Me.edLimit.Location = New System.Drawing.Point(207, 41)
        Me.edLimit.Name = "edLimit"
        Me.edLimit.Size = New System.Drawing.Size(72, 20)
        Me.edLimit.TabIndex = 7
        Me.edLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbOrderType
        '
        Me.cbOrderType.FormattingEnabled = True
        Me.cbOrderType.Location = New System.Drawing.Point(104, 41)
        Me.cbOrderType.Name = "cbOrderType"
        Me.cbOrderType.Size = New System.Drawing.Size(97, 21)
        Me.cbOrderType.TabIndex = 6
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(446, 21)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(19, 13)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "Tif"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(364, 21)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(27, 13)
        Me.Label23.TabIndex = 4
        Me.Label23.Text = "Size"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(286, 21)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(29, 13)
        Me.Label22.TabIndex = 3
        Me.Label22.Text = "Stop"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(204, 21)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(28, 13)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Limit"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(101, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(53, 13)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Ordertype"
        '
        'edMessages
        '
        Me.edMessages.AcceptsReturn = True
        Me.edMessages.Location = New System.Drawing.Point(14, 383)
        Me.edMessages.Multiline = True
        Me.edMessages.Name = "edMessages"
        Me.edMessages.Size = New System.Drawing.Size(820, 85)
        Me.edMessages.TabIndex = 5
        Me.edMessages.WordWrap = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 484)
        Me.Controls.Add(Me.edMessages)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "TWSLink Visual Basic .NET sample"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btConnect As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents edPort As System.Windows.Forms.TextBox
    Friend WithEvents edIP As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbType As System.Windows.Forms.ComboBox
    Friend WithEvents edSymbol As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents edUID As System.Windows.Forms.TextBox
    Friend WithEvents edMultiplier As System.Windows.Forms.TextBox
    Friend WithEvents edStrike As System.Windows.Forms.TextBox
    Friend WithEvents cbRight As System.Windows.Forms.ComboBox
    Friend WithEvents dtExpiry As System.Windows.Forms.DateTimePicker
    Friend WithEvents edExchange As System.Windows.Forms.TextBox
    Friend WithEvents edCurrency As System.Windows.Forms.TextBox
    Friend WithEvents btRegister As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents edLocalSymbol As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents edNL As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents edAskSize As System.Windows.Forms.TextBox
    Friend WithEvents edLastSize As System.Windows.Forms.TextBox
    Friend WithEvents edAsk As System.Windows.Forms.TextBox
    Friend WithEvents edLast As System.Windows.Forms.TextBox
    Friend WithEvents edBidSize As System.Windows.Forms.TextBox
    Friend WithEvents edBid As System.Windows.Forms.TextBox
    Friend WithEvents edMaintenance As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents edTif As System.Windows.Forms.TextBox
    Friend WithEvents edSize As System.Windows.Forms.TextBox
    Friend WithEvents edStop As System.Windows.Forms.TextBox
    Friend WithEvents edLimit As System.Windows.Forms.TextBox
    Friend WithEvents cbOrderType As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents btPlaceOrder As System.Windows.Forms.Button
    Friend WithEvents btRequestMarketData As System.Windows.Forms.Button
    Friend WithEvents edConid As System.Windows.Forms.TextBox
    Friend WithEvents edOid As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cbSide As System.Windows.Forms.ComboBox
    Friend WithEvents edOrderStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents edMessages As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents edConnStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents edAccount As System.Windows.Forms.TextBox
    Friend WithEvents edPositions As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label

End Class
