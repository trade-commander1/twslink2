'Option Strict On
Option Explicit On

#Region "imports"
Imports System.Runtime.InteropServices
Imports System.Collections
Imports twslinkLib
#End Region


' define the type of the TWSLink callback function
Public Delegate Sub EventCallbackProto(ByVal long1 As Integer, _
                                    ByVal long2 As Integer, _
                                    ByVal long3 As Integer, _
                                    ByVal long4 As Integer, _
                                    ByVal val1 As Double, _
                                    ByVal val2 As Double, _
                                    ByVal val3 As Double, _
                                    ByVal val4 As Double, _
                                    ByVal s1 As String, _
                                    ByVal s2 As String, _
                                    ByVal s3 As String, _
                                    ByVal s4 As String, _
                                    ByVal val5 As Double, _
                                    ByVal val6 As Double, _
                                    ByVal long5 As Integer, _
                                    ByVal long6 As Integer)



Public Class Form1
#Region "Member"
    ' handle to your TWSLink com instance
    Private _twslink As twslinkCom = Nothing
    ' handle to your TWSLink callback
    Private _evCB As EventCallbackProto = Nothing

    ' conversion helper
    Private _convert As Conversions = New Conversions
    ' misc helper
    Private _services As Services = New Services

    ' unique id of currently registered contract
    Private _uid As Long = 0
    ' unique id of latest order placed
    Private _oid As Long = 0

    ' market data value buffers
    Private _Bid As Double = 0.0
    Private _BidSize As Long = 0
    Private _Last As Double = 0.0
    Private _LastSize As Long = 0
    Private _Ask As Double = 0.0
    Private _AskSize As Long = 0

    ' positions buffer
    Private _positions As Double = 0.0

    'Call SetupForm1()
#End Region

#Region "various setup function"
    Public Sub SetupForm1()

        ' connection setup
        edIP.Text = "127.0.0.1"
        edPort.Text = "7496"

        ' contract default setup for a simple cash contract
        edSymbol.Text = "EUR"
        edCurrency.Text = "USD"
        edExchange.Text = "IDEALPRO"
        edSize.Text = "50000"
        edPositions.Text = "-"

        cbType.Items.Add("STK")
        cbType.Items.Add("FUT")
        cbType.Items.Add("CASH")
        cbType.Items.Add("OPT")
        cbType.Items.Add("FOP")
        cbType.SelectedIndex = 2

        cbRight.Items.Add("CALL")
        cbRight.Items.Add("PUT")
        cbRight.SelectedIndex = 1

        ' order default setup
        cbSide.Items.Add("BUY")
        cbSide.Items.Add("SELL")
        cbSide.SelectedIndex = 1
        edTif.Text = "GTC" ' tif is good till cancel
        edOrderStatus.Text = "initial"

        cbOrderType.Items.Add("MKT")
        cbOrderType.Items.Add("LMT")
        cbOrderType.Items.Add("STP")
        cbOrderType.Items.Add("STP LMT")
        cbOrderType.SelectedIndex = 0

        'Dim expiryFormat As System.Windows.Forms.DateTimePickerFormat = New DateTimePickerFormat
        dtExpiry.Format = DateTimePickerFormat.Custom
        dtExpiry.CustomFormat = "yyyyMMdd"

        ' market data
        Call ResetMarketData()

        'Dim test As String
        'test = cbRight.Text
        'test = dtExpiry.Text
        ' function called on startup
        ' create and setup TWSLink com instance
        Call SetupTWSLink()
    End Sub
    Private Sub ResetMarketData()
        _Bid = 0.0
        _BidSize = 0
        _Last = 0.0
        _LastSize = 0
        _Ask = 0.0
        _AskSize = 0
        edBidSize.Text = "-"
        edBid.Text = "-"
        edLast.Text = "-"
        edLastSize.Text = "-"
        edAsk.Text = "-"
        edAskSize.Text = "-"
    End Sub
#End Region
#Region "TWSLink stuff"
    Private Function SetupTWSLink() As Boolean
        ' setup TWSLink COM instance
        Dim ret As Long
        Dim pf As Object
        Dim TWSLinkLogLevel As Integer

        TWSLinkLogLevel = 3

        ' --- callback setup ---
        ' create our callback
        _evCB = AddressOf EventCallback
        pf = Marshal.GetFunctionPointerForDelegate(_evCB)
        ' hook callback into fucking TWSLink
        Try
            ret = TL().SET_EVENT_HANDLER(pf.ToInt32(), 1 + 2 + 4 + 8 + 16 + 64 + 128 + 512, 0)
            SetupTWSLink = True
        Catch ex As Exception
            MsgBox(ex.ToString)
            Application.Exit()
            SetupTWSLink = False
        End Try


    End Function
    Private Function TL() As twslinkCom
        Try
            ' create TWSLink COM instance
            If _twslink Is Nothing Then
                _twslink = CreateObject("twslink2.twslinkCom")
            End If
            TL = _twslink
        Catch ex As Exception
            MsgBox(ex.ToString)
            'Application.Exit()
            TL = Nothing            
        End Try

    End Function
    Private Function Status2String(ByVal status As Long) As String
        ' NOTE: or see TWSLink/documentation/twsmanual.htm table 6.7
        Select Case status
            ' bid Size
            Case 0
                Status2String = "undefined"
            Case 1
                Status2String = "initial"
            Case 2
                Status2String = "transmitted"
            Case 3
                Status2String = "cancel transmitted"
            Case 4
                Status2String = "inactive"
            Case 5
                Status2String = "pending submit"
            Case 6
                Status2String = "pending cancel"
            Case 7
                Status2String = "presubmitted"
            Case 8
                Status2String = "submitted"
            Case 9
                Status2String = "filled"
            Case 10
                Status2String = "cancelled"
            Case 11
                Status2String = "partially filled"
            Case 12
                Status2String = "erroneous."

            Case Else
                Status2String = "undefined"
        End Select
    End Function
    Private Sub UpdatePositions(ByVal uid As Long, ByVal positions As Double)
        If uid = _uid Then
            If positions > 0 Then
                edPositions.ForeColor = Color.Green
            ElseIf positions < 0 Then
                edPositions.ForeColor = Color.Red
            Else
                edPositions.ForeColor = Color.Yellow
            End If
            _positions = positions
            edPositions.Text = _positions.ToString()
        End If
    End Sub
#End Region
#Region "Form Delegates"
    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Dim ret As Long
        'ret = TL().DISPOSE(0)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ret As Long
        ret = ret + 1
    End Sub
#End Region

#Region "Form Events"
#End Region

#Region "Callback"
    ' -------------------------------------------------------------------------------
    ' NOTE for more information see TWSLink-root-directory/documentation/callback.htm
    ' -------------------------------------------------------------------------------

    ' this callback receives all events coming from TWSLink instance.
    ' you get market data here, order states, general message, etc.
    ' process messages here depending on their category.
    ' the category is stored in long1.
    Public Sub EventCallback(ByVal long1 As Integer, _
                                    ByVal long2 As Integer, _
                                    ByVal long3 As Integer, _
                                    ByVal long4 As Integer, _
                                    ByVal val1 As Double, _
                                    ByVal val2 As Double, _
                                    ByVal val3 As Double, _
                                    ByVal val4 As Double, _
                                    ByVal s1 As String, _
                                    ByVal s2 As String, _
                                    ByVal s3 As String, _
                                    ByVal s4 As String, _
                                    ByVal val5 As Double, _
                                    ByVal val6 As Double, _
                                    ByVal long5 As Integer, _
                                    ByVal long6 As Integer)
        On Error Resume Next
        Dim Ret As Long
        Dim uidrow As Long
        Ret = 0
        ' market data received
        If long1 = 1 Then
            ' NOTE: market_data_2 (receive market data from IB or errors through callback)
            Call UpdateMarketData(long2, long3, long5, val1)
            ' event of general interest
        ElseIf long1 = 2 Then
            ' account update sequence has finshed
            If long2 = 11 Then
                'Call ThisWorkbook.Sheets("Strategy").UpdatePositions
                ' connection status changed
            ElseIf long2 = 1 Or long2 = 2 Or long2 = 3 Then
                ' NOTE: connect_2 (receive connection status)
                Call UpdateConnectionStatus(long2)
                'Call ThisWorkbook.Sheets("Initialisation").UpdateAccountStatus
                'Call ThisWorkbook.Sheets("Strategy").UpdateContractStates
                ' disconnected from tws
                ' contract status available
            ElseIf long2 = 13 Then
                ' NOTE: contract registration_2 (receive contract validation through callback)
                Call UpdateContractStatus(long3, long4, s1)
                ' detail request responsed with an error (contract invalid)
                If long4 = 0 Then
                End If
                ' contract detail sequence complete (response for chain request)
            ElseIf long2 = 21 Then
            End If
            ' positions update
        ElseIf long1 = 4 Then
            If long5 = 1 Then
                Call UpdatePositions(long2, val5)
            End If
            ' order status
        ElseIf long1 = 8 Then
            Call UpdateOrderStatus(long2, long4)
            ' message from tws: error, warning, hint
        ElseIf long1 = 16 Then
            ' general message from IB,TWS or TWSLink internal
            Call UpdateErrorMessage(long2, long3, long4, s1, "API")
            ' realized p&l (and trade)
        ElseIf long1 = 64 Then
            If long2 = 15 Then
                ' pl update
                ' trace trade
            End If
            ' gen unrealized p&l
        ElseIf long1 = 128 Then
            ' account value
        ElseIf long1 = 512 Then
            Call UpdateAccountStatus(long2, s1, s3)
            ' sql command response
        ElseIf long1 = 1024 Then
        End If
        If Err.Number <> 0 Then
        End If
    End Sub
#End Region
#Region "Callback Helpers"
    Private Sub UpdateConnectionStatus(ByVal status As Integer)
        ' NOTE: connection_3 (show the status of connection to TWS. 1,2 is connected, 0 not)
        If status = 1 Or status = 2 Then
            edConnStatus.Text = "connected"
            edConnStatus.BackColor = Color.Green
        Else
            edConnStatus.Text = "connection failed"
            edConnStatus.BackColor = Color.Red
        End If
    End Sub
    Private Sub UpdateContractStatus(ByVal uid As Long, ByVal conid As Long, ByVal localSymbol As String)
        ' NOTE: contract registration_3 (finally show contract data like conid and local symbol)
        edConid.Text = conid.ToString()
        If conid > 0 Then
            edLocalSymbol.Text = localSymbol
            edLocalSymbol.BackColor = Color.LightBlue

            ' to show immediately current positions, query them explicitly here
            UpdatePositions(uid, TL().GET_POSITIONS(_uid, ""))
            ' contract specification was wrong
        Else
            edLocalSymbol.Text = "invalid"
            edLocalSymbol.BackColor = Color.LightPink
            _uid = 0
        End If
    End Sub
    Private Sub UpdateMarketData(ByVal uid As Long, ByVal mdid As Long, ByVal size As Long, ByVal price As Double)
        ' NOTE: market_data_3 (finally show market data)
        ' price update
        Select Case mdid
            ' bid
            Case 1
                If price > _Bid Then
                    edBid.ForeColor = Color.Green
                ElseIf price < _Bid Then
                    edBid.ForeColor = Color.Red
                Else
                    edBid.ForeColor = Color.Yellow
                End If
                _Bid = price
                edBid.Text = price.ToString()
                ' ask
            Case 2
                If price > _Ask Then
                    edAsk.ForeColor = Color.Green
                ElseIf price < _Ask Then
                    edAsk.ForeColor = Color.Red
                Else
                    edAsk.ForeColor = Color.Yellow
                End If
                _Ask = price
                edAsk.Text = price.ToString()
                ' last
            Case 4
                If price > _Last Then
                    edLast.ForeColor = Color.Green
                ElseIf price < _Last Then
                    edLast.ForeColor = Color.Red
                Else
                    edLast.ForeColor = Color.Yellow
                End If
                _Last = price
                edLast.Text = price.ToString()
            Case Else
                ' skipp
        End Select

        ' size update
        If size > 0 Then
            Select Case mdid
                ' bid Size
                Case 0
                    If size > _BidSize Then
                        edBidSize.ForeColor = Color.Green
                    ElseIf size < _BidSize Then
                        edBidSize.ForeColor = Color.Red
                    Else
                        edBidSize.ForeColor = Color.Yellow
                    End If
                    _BidSize = size
                    edBidSize.Text = size.ToString()
                    ' ask size
                Case 3
                    If size > _AskSize Then
                        edAskSize.ForeColor = Color.Green
                    ElseIf size < _AskSize Then
                        edAskSize.ForeColor = Color.Red
                    Else
                        edAskSize.ForeColor = Color.Yellow
                    End If
                    _AskSize = size
                    edAskSize.Text = size.ToString()
                    ' last size
                Case 5
                    If size > _LastSize Then
                        edLastSize.ForeColor = Color.Green
                    ElseIf size < _LastSize Then
                        edLastSize.ForeColor = Color.Red
                    Else
                        edLastSize.ForeColor = Color.Yellow
                    End If
                    _LastSize = size
                    edLastSize.Text = size.ToString()

                Case Else
                    ' skipp
            End Select
        End If
        'Debug.WriteLine("Not between 1 and 10, inclusive")
    End Sub
    Private Sub UpdateOrderStatus(ByVal oid As Long, ByVal status As Long)
        edOrderStatus.Text = Status2String(status)
    End Sub
    Private Sub UpdateErrorMessage(ByVal severity As Long, ByVal id As Long, ByVal code As Long, ByVal message As String, ByVal origin As String)
        Dim entry As String
        entry = TimeString + "   " + severity.ToString() + "   " + id.ToString() + "   " + code.ToString() + "  " + origin

        If edMessages.Text.Length > 0 Then
            edMessages.Text = edMessages.Text + vbCrLf
        End If
        edMessages.Text = edMessages.Text + entry
    End Sub
    Private Sub UpdateAccountStatus(ByVal accid As Long, ByVal value As String, ByVal acc As String)
        If accid = 12 Then
            edNL.Text = value
        ElseIf accid = 74 Then
            edMaintenance.Text = value
        End If
        edAccount.Text = acc
    End Sub
#End Region
    Private Sub btConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConnect.Click
        ' NOTE: contract connection_1 (connect to TWS)
        Dim ret As Long
        Dim Port As Long
        edConnStatus.Text = "connecting..."
        edConnStatus.BackColor = Color.Yellow

        Port = _convert.Str2Int(edPort.Text)
        ret = TL().CONNECT(edIP.Text, Port, 72343, 500)
    End Sub

    Private Sub btRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRegister.Click
        ' NOTE: contract registration_1 (register contract to get a compact id for refer to this contract.
        '       the registration makes also a request to TWS to get more information about contract and
        '       makes by the way a validation.
        '       but important is only the return uid.  you need this id for various action, like
        '       requesting market data,placing orders,etc
        edLocalSymbol.BackColor = Color.LightGray
        ' for parameter info please see TWSLink/documentation/twslinkmanual.htm function REQ_MARKET_DATA
        _uid = TL().REGISTER_CONTRACT(_edSymbol.Text.ToString(), cbType.Text.ToString(), _edCurrency.Text.ToString(), _edExchange.Text.ToString(), "", dtExpiry.Text, cbRight.Text.ToString(), _convert.Str2Double(_edStrike.Text.ToString()), _edMultiplier.Text.ToString(), 0, 0.0)
        edUID.Text = _uid.ToString()
    End Sub

    Private Sub btRequestMarketData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btRequestMarketData.Click
        ' NOTE: market_data_1 (make the request to get realtime to our contract registered)
        If _uid <= 0 Then
            MessageBox.Show("You need to register contract first, before requesting market data")
            Exit Sub
        End If
        Dim ret As Long
        ' for parameter info please see TWSLink/documentation/twslinkmanual.htm function REQ_MARKET_DATA
        ret = TL().REQ_MARKET_DATA(_uid, 1, "", -1)
    End Sub

    Private Sub btPlaceOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btPlaceOrder.Click
        ' NOTE: place_order_1 (send order place request to TWS. as always, the callback gives us response)
        If _uid <= 0 Then
            MessageBox.Show("You need to register contract first, before placing an order")
            Exit Sub
        End If

        _oid = _convert.Str2Int(edOid.Text.ToString())
        ' previous placing caused an error
        If _oid < 0 Then
            _oid = 0
        End If
        ' NOTE: if _oid = 0, then an new order gets placed. if _oid > 0, the underlying order
        '       gets modified. so resting _oid to zero (text box right), places a new order
        ' for parameter info please see TWSLink/documentation/twslinkmanual.htm function PLACE_ORDER
        _oid = TL().PLACE_ORDER(_uid, _oid, cbSide.Text, cbOrderType.Text, _convert.Str2Int(edSize.Text.ToString()), _convert.Str2Double(edLimit.Text.ToString()), _convert.Str2Double(edStop.Text.ToString()), edTif.Text.ToString(), 1, 0)
        If _oid > 0 Then
            edOrderStatus.Text = Status2String(2)
            edOid.Text = _oid
        Else
            edOrderStatus.Text = Status2String(12)
            edOid.Text = _oid
        End If
    End Sub
End Class
