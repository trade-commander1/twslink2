# file: sample2.py (python 3 compatible)

# -------------------------------------------------------
# This file demonstrates market data access with twslink
# together with placing order based on a moronic strategy
# -------------------------------------------------------

import twslink2pt
import time
import sys

# array of lows
low_dict = {-1 : -1.0};
# array of highs
high_dict = {-1 : -1.0};
# array of minticks
mintick_dict = {-1 : -1.0};
# array of positions
positions_dict = {-1 : -1.0};
# array of target positions
positions_target_dict = {-1 : -1.0};
# array of delta from high in pips
delta_h_pips = {-1 : 0};
# array of delta from high in pips
delta_l_pips = {-1 : 0};
# array of orderids
oids_pips = {-1 : 0};
# array of orderstates
o_states = {-1 : 0};
# last entry prices
entries = {-1 : 0.0};
# trigger pips
triggerpips = 3;


# --- subroutines ---
def getnow():
    return time.asctime()

def TWSStr2Float(valastring,valonfail):
  if valastring != "cnf" and valastring != "inf" and valastring != "infa" and valastring != "nok":
    return float(valastring)
  return float(valonfail)

# --- end subroutines ---

# ----- Object creation -----
print ("Creating twslink2")
tws = twslink2pt.TWSLink2Wrap("",1)
# print ("    Created twslink2", tws)

# ----- conntect to TWS at standard port -----
host = "127.0.0.1" ;
print( "Connecting . . . \n")
tws.CONNECT(host,7496,5,100);

print "Register MSFT\n"
uidMSFT = tws.REGISTER_CONTRACT("MSFT","STK","USD","SMART","","","",0.0,"",0,0.0) 
print "uid MSFT=uidMSFT\n"

print "Place buy market 100 MSFT\n"
uidOrderMSFT = tws.PLACE_ORDER(uidMSFT,0,"BUY","MKT",100,0.0,0.0,"GTC",1,0) 
print "uid order MSFT=$uidOrderMSFT\n";
tws.WAIT_FOR_FILLED(uidOrderMSFT,20000)

print ("finished")



