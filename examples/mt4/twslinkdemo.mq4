//+------------------------------------------------------------------+
//|                                                  twslinkma.mq4 |
//|                           Copyright © 2012, trade-commander.org. |
//|                                  http://www.trade-commander.org/ |
//+------------------------------------------------------------------+
// WARINING: this stratey is not thought for real trades. its purpose is to
// demonstrate the use of TWSLink2.
// How does it work: 
// a) connects TWSLink2 to TWS or Gateway
// b) registers MSFT as contract for trading or getting market data or...
// c) places a buy 100 MSFT at market order

// NOTE: you may need to copy TWSLink2 into MT4 experts-directory

#property copyright "trade-commander.org"
#property link      "http://www.trade-commander.org/"

#include <twslink2.mqh>
#include <WinUser32.mqh>


///
/// trading parameter
///
extern double Param  = 0;           // the larch


int      uidMSFT         	= 0;     // unique id for MSFT contract
int      uidOrderMSFT       = 0;     // unique id for MSFT order

int init()
{
	// connect to TWS / Gateway at standard port. make sure TWS or Gateway are setup to operate with API clients such as TWSLink2  (http://www.youtube.com/watch?v=53tmypRq5wI)
	CONNECT("127.0.0.1",7496,1,1000);
	// register MSFT
	uidMSFT=REGISTER_CONTRACT("MSFT","STK", "USD", "SMART", "", "","", 0.0, "",0,0.0);
	// place buy 100 shares at market for MSFT		
	uidOrderMSFT=PLACE_ORDER(uidMSFT,0, "BUY", "MKT",100,0.0,0.0,"GTC", 1,0);
	return(1);
} 
void deinit()
{ 
}
 
//+------------------------------------------------------------------+
//| Start function                                                   |
//+------------------------------------------------------------------+
int start()
{
   
   return (1);
}
//+------------------------------------------------------------------