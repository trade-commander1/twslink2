//+------------------------------------------------------------------+
//|                                                 twslink2demo.mq5 |
//|                              Copyright 2012, trade-commander.org |
//|                                   http://www.trade-commander.com |
//+------------------------------------------------------------------+
// function U2A is used to convert unicode strings as used in MT5 to ansi string used in TWSLink
#property copyright "Copyright 2012, trade-commander.org"
#property link      "http://www.trade-commander.com"
#property version   "1.00"

#include <twslink2.mqh>

int      uidMSFT         	= 0;        // unique id for MSFT contract
int      uidOrderMSFT       = 0;       // unique id for MSFT order

//+------------------------------------------------------------------+
//| Expert initialization function                                   |
//+------------------------------------------------------------------+
int OnInit()
  {
// connect to TWS / Gateway at standard port. make sure TWS or Gateway are setup to operate with API clients such as TWSLink2  (http://www.youtube.com/watch?v=53tmypRq5wI)
	CONNECT(U2A("127.0.0.1"),7496,1,1000);
	// register MSFT
	uidMSFT=REGISTER_CONTRACT(U2A("MSFT"),U2A("STK"), U2A("USD"), U2A("SMART"), U2A(""), U2A(""),U2A(""), 0.0, U2A(""),0,0.0);
	// place buy 100 shares at market for MSFT		
	uidOrderMSFT=PLACE_ORDER(uidMSFT,0, U2A("BUY"), U2A("MKT"),100,0.0,0.0,U2A("GTC"), 1,0);
	return(0);
  }
//+------------------------------------------------------------------+
//| Expert deinitialization function                                 |
//+------------------------------------------------------------------+
void OnDeinit(const int reason)
  {
//---
   
  }
//+------------------------------------------------------------------+
//| Expert tick function                                             |
//+------------------------------------------------------------------+
void OnTick()
  {
//---
   
  }
//+------------------------------------------------------------------+
