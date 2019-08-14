/* 
 ----------------------------------------------------------------------
 * This file implements the TWSLink callback. Function EventCallback
 * is called from within TWSLink whenever something is to be reported
 * back, like market data, order status, account information, etc.
 * As we have a callback, this cs sample demonstrates the passive event
 * handling.
 * The meaning of all parameter of EventCallback is listed in callback.pdf
 * of TWSLink help (programs menu)
 ----------------------------------------------------------------------
*/
namespace cs
{
    public partial class Form1 : System.Windows.Forms.Form
    {

        #region Callback
        /// Callback passed to TWSLink
        public static void EventCallback(   int long1,
                                            int long2,
                                            int long3,
                                            int long4,
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
                                            int long6)
        {
            #region Market Data Update long1=1
            // market data received
            if (long1 == 1)
            {
                bool updateRow = false;
                int row = __app.UID2Row(long2);

                // long2 is Contract UID
                __app.ShowMarketData(long2, long3, dvalue1, long5, row);

                // bid size update
                if (long3 == 0)
                {
                    __app.ShowSize(long2, long3, long5, row);
                   
                    // dvalue1 = bid size
                }
                // bid update
                else if (long3 == 1)
                {
                    // dvalue1 = bid
                    // dvalue2 = size
                    // dvalue3 = can autoexecute flag
                    // dvalue4 = new runtime average
                    // new runtime low update
                    // new tickcount update

                    if (__app.cbStatisticTicks.Checked == true)
                    {
                        __app.UpdateTickCount(long2, row);
                        __app.UpdateRTLow(long2, row);
                        __app.UpdateRTAvg(long2, dvalue4, row);
                        __app.UpdateBidDepStatistic(long2, row);
                        updateRow = true;
                    }
                      
                }
                // ask update
                else if (long3 == 2)
                {
                    // dvalue1 = ask
                    // dvalue2 = size
                    // dvalue3 = can autoexecute flag
                    // dvalue4 = new runtime average
                    // new runtime high update
                    // new tickcount update
                    if (__app.cbStatisticTicks.Checked == true)
                    {
                        __app.UpdateTickCount(long2, row);
                        __app.UpdateRTHigh(long2, row);
                        __app.UpdateRTAvg(long2, dvalue4, row);
                        __app.UpdateAskDepStatistic(long2, row);
                        updateRow = true;
                    }
                }
                // ask size update
                else if (long3 == 3)
                {
                    __app.ShowSize(long2, long3, long5,row);
                   
                }
                // last  update
                else if (long3 == 4)
                {
                    //__app.UpdateTickCount(long2);
                    // dvalue1 = last
                    // dvalue2 = size
                    // dvalue3 = can autoexecute flag
                    // new tickcount update
                }
                // last size update
                else if (long3 == 5)
                {
                    __app.ShowSize(long2, long3, long5, row);
                   
                }
                // high update
                else if (long3 == 6)
                {
                    // dvalue1 = high
                    //System.Console.WriteLine("HIGH="+dvalue1.ToString());
                }
                // low update
                else if (long3 == 7)
                {
                    // dvalue1 = low
                    //System.Console.WriteLine("LOW=" + dvalue1.ToString());
                }
                // bid option computation
                else if (long3 == 10)
                {
                    // dvalue1 = implied volatility
                    // dvalue2 = delta
                }
                // ask option computation
                else if (long3 == 11)
                {
                    // dvalue1 = implied volatility
                    // dvalue2 = delta
                }
                // last option computation
                else if (long3 == 12)
                {
                    // dvalue1 = implied volatility
                    // dvalue2 = delta
                }
                // model option
                else if (long3 == 12)
                {
                    // dvalue1 = implied volatility
                    // dvalue2 = delta
                    // dvalue3 = model price
                    // dvalue4 = pv dividend
                }

                // option call open interest
                else if (long3 == 27)
                {
                    // dvalue1 = option call open interest
                }
                // option put open interest
                else if (long3 == 28)
                {
                    // dvalue1 = option put open interest
                }
                // option call volume
                else if (long3 == 29)
                {
                    // dvalue1 = option call volume
                }
                // option put volume
                else if (long3 == 30)
                {
                    // dvalue1 = option put volume
                }
                // index future premium
                else if (long3 == 31)
                {
                    // dvalue1 = index future premium
                }

                       // mdata range
                if (updateRow == true)
                {
                    SourceGrid.Range range_update = new SourceGrid.Range(row, (int)ColIdx.enBid, row, (int)ColIdx.enN3_AVG);
                    __app._MDData.InvalidateRange(range_update);
                }
            }
            #endregion
            #region General Event long1=2
            // event of general interest
            else if (long1 == 2)
            {
                // account update sequence has finshed
                if (long2 == 11)
                {
                    __app.UpdateAllPositions();
                }
                // connected
                else if (long2 == 1)
                {
                    __app.SetConnected(true, s1);
                }
                // passive/active disconnection
                else if (long2 == 2 || long2 == 3)
                {
                    __app.SetConnected(false, "");
                }
                // contract status available
                else if (long2 == 13)
                {
                    // long3 = contract uid
                    // long4 = conid OR ZERO, in case of error (contract definition is wrong -> not accepted)
                    __app.ContractStatus(long3, long4, dvalue4);
                }
                // tick statistic update (ticks per minute for every contract: counted are bid,last,ask)
                else if (long2 == 14)
                {
                    __app.UpdateTickStat();
                }

            }
            #endregion
            #region Single Position Update long1=4
            else if (long1 == 4)
            {
                // long2 = contract uid
                // long3 = new size
                __app.UpdatePositions(__app.UID2Row(long2), long3);
            }
            #endregion
            #region Order Status long1=8
            else if (long1 == 8)
            {
                // long2 = order uid
                // long3 = contract uid
                // long4 = new order status
                // dvalue1 = new size as double
                // dvalue2 = last fill price
                // dvalue3 = avg. fill price
                // dvalue4 = filled size (last fill) as double
                // s1 = action (BUY,SELL)
                // s2 = order snapshot
                //Call ThisWorkbook.Sheets("Orders").UpdateOrderStatus(long2, long4, val2, val3, CInt(val4), s1, long3, s2)
            }
            #endregion
            #region TWS message long1=16
            else if (long1 == 16)
            {
                __app.AddTLMessage("note", (s1 != null ? s1 : "unkown message"));
            }
            #endregion
            #region Realized P&L long1=64
            else if (long1 == 64)
            {
                if (long2 == 15)
                {
                    // this would be recent realizedly (-> trade)
                    //__app.UpdateRealizedPL(long5, dvalue1, dvalue2);
                    // this is runtime accumulated realized p&l
                    __app.UpdateRealizedPL(long5, dvalue5, dvalue6);
                }
            }
            #endregion
            #region Unrealized P&L long1=128
            else if (long1 == 128)
            {
                __app.UpdateUnrealizedPL(long2, dvalue1, long3);
            }
            #endregion
            // dump the tick
            /*
            string update = "-- callback: evid=";

            update += long1.ToString();
            update += " obid=";
            update += long2.ToString();
            update += " primc=";
            update += long3.ToString();
            update += " secc=";
            update += long4.ToString();
            update += " d1=";
            update += dvalue1.ToString();
            update += " d2=";
            update += dvalue2.ToString();
            update += " d3=";
            update += dvalue3.ToString();
            update += " d4=";
            update += dvalue4.ToString();
            update += " s1=";
            update += s1;
            update += " s2=";
            update += s2;
            System.Console.WriteLine(update);
            */
            return;
        }
    }
#endregion
}