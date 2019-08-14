# file: get_market_data.pt

# -------------------------------------------------------
# This file demonstrates market data access with twslink
# together with placing order based on a moronic strategy
# -------------------------------------------------------

import twslink2pt
import time

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



# --- subroutines ---
def getnow():
    return time.asctime()

def TWSStr2Float(valastring,valonfail):
  if valastring != "cnf" and valastring != "inf" and valastring != "infa" and valastring != "nok":
    return float(valastring)
  return float(valonfail)

# --- end subroutines ---

# ----- Object creation -----

print ("Creating twslink")
tws = twslinkpt.TWSLinkWrap(1)
print ("    Created twslink", tws)

# ----- vorspiel -----
print ("Initializing Dll . . . \n")
logf = "#log\python_sample1.log" 


triggerpips = 3;
print( "trigger pips=" + str(triggerpips))

host = "127.0.0.1" ;
print( "Connecting . . . \n")
tws.CONNECT(host,7496,5,100);

label=tws.GET_DLL_STATUS("connstring",0,"");

# --- register some contracts ---
print ("Register Contracts . . . \n" );
# don't wait for confirmation here. we assume, that specifications are all right.
# eur/usd forex
eurusdid=tws.REGISTER_CONTRACT("EUR","CASH","","IDEALPRO","","","",0.0,"",0,0.0) ;
print ("eurusd id=" + str(eurusdid));

# microsoft stock
msftid=tws.REGISTER_CONTRACT("MSFT","STK","USD","SMART","","","",0.0,"",0,0.0) ;
print ("msft id=" + str(msftid));

# may oil future
oilfutid=tws.REGISTER_CONTRACT("QM","FUT","USD","NYMEX","","200908","",0.0,"",0,0.0) ;
print ("oil future id=" + str(oilfutid));

# IBM stock chain
#tws.REGISTER_CONTRACT("IBM","STK","USD","SMART","","","",0.0,"",0) ;

# IBM option chain
#tws.REGISTER_CONTRACT("IBM","OPT","USD","","","200704","",0.0,"",0) ;

# give IB/TWS 5 seconds to send us all contract specific data
tws.WAIT_FOR_REQ_PROCESSED(5000);

# --- get the ident strings and build dictionaries ---
# --- eurusd ---
ident_dict = {-1 : 'none'};
eurusdid_ident =  tws.GET_CONTRACT_VAL(eurusdid,9,"","");
ident_dict[eurusdid] = eurusdid_ident;
mintick_dict[eurusdid] = TWSStr2Float(tws.GET_CONTRACT_VAL(eurusdid,20,"",""),0.0);
positions_dict[eurusdid] = TWSStr2Float(tws.GET_CONTRACT_VAL(eurusdid,8,"",""),0.0);
positions_target_dict[eurusdid] =  50000;
low_dict[eurusdid] =  1000000.0;
high_dict[eurusdid] = 0.0;
delta_h_pips[eurusdid] = 0;
delta_l_pips[eurusdid] = 0;
oids_pips[eurusdid] = 0;
o_states[eurusdid] = 0;
entries[eurusdid] = 0;
# --- setup msft ---
msftid_ident =  tws.GET_CONTRACT_VAL(msftid,9,"","");
ident_dict[msftid] = msftid_ident;
mintick_dict[msftid] = TWSStr2Float(tws.GET_CONTRACT_VAL(msftid,20,"",""),0.0);
positions_dict[msftid] = TWSStr2Float(tws.GET_CONTRACT_VAL(msftid,8,"",""),0.0);
positions_target_dict[msftid] =  50;
low_dict[msftid] =  1000000.0;
high_dict[msftid] = 0.0;
delta_h_pips[msftid] = 0;
delta_l_pips[msftid] = 0;
oids_pips[msftid] = 0;
o_states[msftid] = 0;
entries[msftid] = 0;
# --- setup qm ---
oilfutid_ident =  tws.GET_CONTRACT_VAL(oilfutid,9,"","");
ident_dict[oilfutid] = oilfutid_ident;
print ("\ncontractval="+tws.GET_CONTRACT_VAL(oilfutid,20,"",""))
mintick_dict[oilfutid] = TWSStr2Float(tws.GET_CONTRACT_VAL(oilfutid,20,"",""),0.0);
positions_dict[oilfutid] = TWSStr2Float(tws.GET_CONTRACT_VAL(oilfutid,8,"",""),0.0);
positions_target_dict[oilfutid] =  2;
low_dict[oilfutid] =  1000000.0;
high_dict[oilfutid] = 0.0;
delta_h_pips[oilfutid] = 0;
delta_l_pips[oilfutid] = 0;
oids_pips[oilfutid] = 0;
o_states[oilfutid] = 0;
entries[oilfutid] = 0;


# request market data for ALL contracts
tws.REQ_MARKET_DATA(0,1,"",0) ;



currPositions = TWSStr2Float(tws.GET_CONTRACT_VAL(eurusdid,8,"",""),0.0);
localPositions =  currPositions
# minimum positions on exchange IDEALPRO
minPositions = 20000
# save the inital net liquidation
startupNetLiquidation=float(0.0);
finalNetLiquidation=float(0.0);
startupNetLiquidation=TWSStr2Float(tws.GET_ACC_VAL(12,"#","",0),0.0)
finalNetLiquidation=startupNetLiquidation;
# id of contract which had the latest tick
idofcontract=0;
print ("--------------------------------------------------")
print ("startup net liquidation = "+str(startupNetLiquidation) + " " + tws.GET_BASECURRENCY(""))
print ("--------------------------------------------------")

next_side  = 0  # 1 = int, -1 = short, 0 do nothing
next_size  = 0
curr_ident = ""
val = 0.0
counter = 0;

# python ist ganz klar eine sprache fuer hausfrauen - schlicht zum *****!
# get the next 100 ticks
while counter < 10000000:

    # wait at the most of 100 seconds for the next tick and get id
    # of contract which has ticked
    idofcontract=tws.WAIT_FOR_TICK(-1,-1,10000);
    if ident_dict[idofcontract] == "":
        curr_ident =  tws.GET_CONTRACT_VAL(idofcontract,9,"","")
        ident_dict[idofcontract] = curr_ident
        entries[idofcontract] = 0
    else:
        curr_ident =   ident_dict[idofcontract]

    if entries[idofcontract] == 0:
        entries[idofcontract] = 0
    if mintick_dict[idofcontract] == 0:
        mintick_dict[idofcontract] = 0.0
    if delta_h_pips[idofcontract] == 0:
        delta_h_pips[idofcontract] = 0.0

    if idofcontract > 0: # other keys are possible due to
                                                               # contract import by portfolio or
                                                               # account updates
       tickid = 0;
       while tickid >= 0:
             tickid = tws.GET_TICK(idofcontract,-1);
       #while (tickid = tws.GET_TICK(idofcontract,0)) >= 0:
           # update extrema, positions if one the ticks below occur
             if tickid == 1 or tickid == 2 or tickid == 4:

                if counter % 50 == 0:
                    print ("\nident                                           positions    time                       bid        last       ask       delta    ostatus")
                    print ("----------------------------------------------------------------------------------------------------------------------------------------")
                    print ("\n *** Current Runtime P&L = " + tws.GET_ACC_VAL(12,"#","",1) + " " + tws.GET_BASECURRENCY("") + " ***\n")

                # update positions
                positions_dict[idofcontract] = int(TWSStr2Float(tws.GET_CONTRACT_VAL(idofcontract,8,"",""),0.0));
                val = tws.GET_MARKET_DATA(idofcontract,tickid,0,-1);
                # initialize entry
                if entries[idofcontract] == 0:
                  entries[idofcontract] =  val;
                
                if  mintick_dict[idofcontract] > 0.0:
                   delta_h_pips[idofcontract] = int((val-entries[idofcontract])/mintick_dict[idofcontract]);
                # we are more than 10 pips away from local low -> go short
                if  delta_h_pips[idofcontract] < -triggerpips :
                   next_side = 1
                   entries[idofcontract] =  val;
                # we are more than 10 pips away from local high -> go int
                elif delta_h_pips[idofcontract] > triggerpips :
                    next_side = -1
                    entries[idofcontract] =  val;
                else:
                    next_side = 0
                
                # check/reset the order status first
                if oids_pips.has_key(idofcontract):
                  o_states[idofcontract]=tws.GET_ORDER_STATUS(oids_pips[idofcontract]);
                
                  if o_states[idofcontract] == 9 or o_states[idofcontract] == 10 or o_states[idofcontract] == 12:
                      o_states[idofcontract]=0;
                else:
                    o_states[idofcontract]=0;
                # open / extend int
                if next_side == 1 and o_states[idofcontract] == 0: #and int(positions_dict[idofcontract]) < int(positions_target_dict[idofcontract]/2
                  next_size = int(positions_target_dict[idofcontract]-positions_dict[idofcontract]);
                  #print "side="+str(next_side)+" size="+str(next_size);
                # open / extend short
                elif next_side == -1 and o_states[idofcontract] == 0: #and int(positions_dict[idofcontract]) > -int(positions_target_dict[idofcontract] / 2):
                  next_size=-1*(int(positions_target_dict[idofcontract] + positions_dict[idofcontract]));
                  #print "side="+str(next_side)+" size="+str(next_size);
                else:
                  next_size = 0;
                if next_size < 0:
                  next_size = next_size * -1;
                  oids_pips[idofcontract] = tws.PLACE_ORDER(idofcontract,0,"SELL","MKT",next_size,0.0,0.0,"GTC",1,0,"","",-1);
                  o_states[idofcontract]=tws.WAIT_FOR_ORDER_STATUS(oids_pips[idofcontract],9,1500,12);
                  print (str(curr_ident).ljust(48) + "-- SELL -- " + str(next_size));
                  # cancel order if not filled within 1.5 seconds
                  if o_states[idofcontract] != 9:
                     tws.CANCEL_ORDER(oids_pips[idofcontract],0,"",-1);
                     print ("cancel order...");
                elif next_size > 0:
                  oids_pips[idofcontract] = tws.PLACE_ORDER(idofcontract,0,"BUY","MKT",next_size,0.0,0.0,"GTC",1,0,"","",-1);
                  o_states[idofcontract]=tws.WAIT_FOR_ORDER_STATUS(oids_pips[idofcontract],9,1500,12);
                  print (str(curr_ident).ljust(48) + "++ BUY ++ " + str(next_size));
                  # cancel order if not filled within 1.5 seconds
                  if o_states[idofcontract] != 9:
                     tws.CANCEL_ORDER(oids_pips[idofcontract],0,"",-1);
                     print ("cancel order...");

                # show data if a new tick has occured
                if tickid == 1:  # bid
                    print (str(curr_ident).ljust(48) + str(positions_dict[idofcontract]).ljust(13) + str(getnow()).ljust(27) + str(val).ljust(32) + str(delta_h_pips[idofcontract]).ljust(14) + str(o_states[idofcontract]))
                elif tickid == 2:  # ask
                    print (str(curr_ident).ljust(48) + str(positions_dict[idofcontract]).ljust(13) + str(getnow()).ljust(49) + str(val).ljust(10)+  str(delta_h_pips[idofcontract]).ljust(14) + str(o_states[idofcontract]))
                elif tickid == 4:  # last
                    print (str(curr_ident).ljust(48) + str(positions_dict[idofcontract]).ljust(13) + str(getnow()).ljust(38) + str(val).ljust(21)+  str(delta_h_pips[idofcontract]).ljust(14) + str(o_states[idofcontract]))

                counter=counter+1

print (" ")
print ("---------------------------------------------------------")
print ("Total Runtime P&L = " + tws.GET_ACC_VAL(12,"#","",1) + " " + tws.GET_BASECURRENCY(""));
print ("---------------------------------------------------------")
tws.CUSTOMCOMMENT("script finished",0);
del tws

print ("finished")



