//+------------------------------------------------------------------+
//| DLL imports                                                      |
//+------------------------------------------------------------------+
#property copyright "Copyright � 2012, www.Trade-Commander.org"
#property link      "http://www.Trade-Commander.org"
#import "twslink2.dll"
int REGISTER_CONTRACT(string symbol,string sectype,string currency,string exchange,string primaryExchange,string expiry,string right,double strike,string multiplier,int expired,double mintick);
int REGISTER_CONTRACT2(string symbol,string sectype,string currency,string exchange,string expiry,string right,double strike,string multiplier);
int REGISTER_CONTRACT3(int conid,string exchange);
int REGISTER_CONTRACT_XML(string pathandfile,int overwrite_load_flag);
int REGISTER_CONTRACT_XML_STYLE(string pathandfile);
string GET_CONTRACTUID_LIST(string symbol,string sectype,string currency,string exchange,string primaryExchange,string expiry,string right,double strike,string multiplier,string delimiter,string alias);
int WAIT_FOR_ACCEPTED(int uicontract,int timeoutmilliseconds);
int ADD_COMBO_LEG(int uidcombo,int conid,int ratio,int openClose,string action,string exchange,int shortsaleslot,string designatedlocation,int xexemptCode);
int REM_COMBO_LEG(int uidcombo);
int REQ_MARKET_DATA(int contractuid,int subscribe,string generictick,int snapshot_flag);
int REQ_MARKET_DATA_TYPE(int type);
int REQ_MARKET_DEPTH(int contractuid,int subscribe,int numrows);
int REQ_BAR_DATA(int contractuid,int barsize,int whatotshow,int rthonly_flag);
int CANCEL_BAR_DATA(int reqid);
double GET_MARKET_DATA(int contractuid,int type,int subtype,int latest_flag);
double GET_BAR_DATA(int contractuid,int type,int valueid,int size,int idx);
string GET_MARKET_DEPTH(int contractuid,int side,int position,int type);
double GET_MARKET_DEPTH2(int contractuid,int side,int position,int type);
int GET_TICK(int contractuid,int timeout);
int WAIT_FOR_TICK(int uidfilter,int tickfilter,int timeout);
double VALIDATE_PRICE(int contractid,double Price,int ceil_flag);
int PIPS(int contractid,double Price);
string EXTRACT_ARRAY_VALUE(int idx,string quotestream,string delim);
int REQ_ACC_UPDATE(string accname,int subscribe,int timeoutmilliseconds);
int REQ_OPEN_ORDERS(int include_extern_clients);
int WAITDLL(int timeoutmilliseconds);
int WAIT_FOR_EVENT(int timeout);
string GET_EVENT_VAL(int uid,int valueid);
int GET_EVENT_VAL_I(int uid,int valueid);
double GET_EVENT_VAL_D(int uid,int valueid);
int START_APPLICATION(string commandline,string startupdir,int timeout);
int GET_CONTRACT_STATUS(int uidcontract);
int PLACE_ORDER(int contractid,int uidorder,string action,string ordertype,int totalquantitiy,double limitprice,double auxprice,string tif,int transmit,int uidparentorder);
int TOGGLE_ORDER(int contractid,int uidorder,int lmtpips,int stppips,int newSize,int transmit);
int TOGGLE_ORDER_PCT(int contractid,int uidorder,double absolute_percentage_change,double stppct,int newSize,int transmit);
int SET_ORDER_VAL(int uidorder,int valueid,string val,int transmit);
string GET_ORDER_VAL(int uidorder,int valueid,string extraparam);
int TRANSMIT(int uidorder);
string GET_ORDERUID_LIST(int contractid,string action,string type,int status,int isdead,double lowerprice,double upperprice,string exchange,string orderref);
string GET_OPEN_ORDERUID_LIST(int contractid,string action,string type,double priceabove,double pricebelow,string exchange,string orderref);
double GET_EXECUTION_PRICE(int uidorder,int type,int idx);
int CONVERT_ID(int sourceid,int conversiontype);
string CONVERT_ID_STRING(string sourcestring,int conversiontype);
int SET_FA_MEMBERS(int uidorder,string account,string faGroup,string faProfile,string faMethod,string faPercentage,int transmit);
int WAIT_FOR_ORDER_STATUS(int uidorder,int statusid,int timeoutmilliseconds,int statusidbreak);
int WAIT_FOR_FILLED(int uidorder,int timeoutmilliseconds);
int WAIT_FOR_SUBMITTED(int uidorder,int timeoutmilliseconds,int sub_stat_only);
int WAIT_FOR_ORDER_STATUS_RNG(int uidorder,int lowerstatusid,int upperstatusid,int timeoutmilliseconds,int statusidbreak);
int GET_ORDER_STATUS(int uidorder);
int GET_ORDER_ERROR(int uidorder);
int ORDER_MODIFIABLE(int uidorder);
string GET_ACC_VAL(int idaccval,string currency,string accountlist,int valtype);
double GET_MARKET_VAL(string currency,int idaccval,string accounts);
double GET_PORTFOLIO_VAL(int uid,int idaccval,string accounts);
string GET_CONTRACT_VAL(int contractid,int idaccval,string param,string accounts);
int SET_CONTRACT_VAL(int uid,int idvalue,string pValue);
string GET_BASECURRENCY(string accname);
double GET_CASH_BALANCE(string currency,string accname);
double GET_EXCHANGE_RATE(string currency1,string currency2);
string GET_ACC_NAME(int index);
double GET_POSITIONS(int contractid,string accname);
int CANCEL_ORDER(int uidorder,int contractid,string otypefilter,int pending,string side);
int CANCEL_ORDER_SYNC(int uidorder,int contractid,string otypefilter,int pending,string side,int timeout_ms);
int CANCEL_SINGLE_ORDER(int uidorder,int timeout);
int CANCEL_ORDER_LIST(string uidlist,int timeout_ms);
int CANCEL_GLOBAL();
int CANCEL_SELECT(string where_clause,int wait_for_return);
int CLOSE_CONTRACT(int contractid,double price,string account,string typefilter);
double CANCEL_AND_CLOSE(string account,int timeout);
int CLOSE_CASH_POSITION(string currency,double price,string account);
int FORCE_CONTRACT(int contractid,double newSize,int isRelative,double price,string account,string typefilter);
int CONNECT_XML(int xmlid,int timeout);
int CONNECT(string sHost,int iport,int startingClientID,int timeout);
int CONNECT_STR(string sHost,int iport,string clientident,int timeout);
int DISCONNECT();
int SET_DLL_PARAM(string sParam,string sVal);
int DISPOSE(int code);
string GET_DLL_PARAM(string sParam,int lParam,string sParam2);
int GET_CONNECTED();
int CUSTOMCOMMENT(string sComment,int loglevel,int errorlevel);
int CUSTOMLOG(string logfilename,string sComment,int filesize);
int DELETE_CUSTOMLOG(string logfilename);
int SEND_MAIL(string originator,string recipients,string subject,string text,string attachments,string smtpserver,int smtp_port,int need_authentication,string user,string userpw);
int CREATE_MAIL_CFG(string cfg_name,string smtpserver,int smtp_port,int encryption,string user,string userpw,string mailer,string sender,string recipients);
int SEND_MAIL_CFG(string cfg_name,string subject,string text,string attachments,string sender,string recipients);
int CREATE_FTP_CFG(string cfg_name,string ftp_ip,int ftp_port,string ftp_user,string ftp_password,string remote_path,int anonymus,int passiv);
int FTP_UPLOAD(string cfg_name,string filename);
int FTP_DOWNLOAD(string cfg_name,string filename);
int TO_FILE(string sFileName,string sText,int write_mode);
string FROM_FILE(string sFileName,int remove_control_char_flag);
int GET_INT_VAR(string Name);
double GET_DBL_VAR(string Name);
string GET_STR_VAR(string Name);
int SET_INT_VAR(string Name,int val);
int SET_DBL_VAR(string Name,double val);
int SET_STR_VAR(string Name,string val);
int REM_INT_VAR(string Name);
int REM_DBL_VAR(string Name);
int REM_STR_VAR(string Name);
int GET_INT(string Name,int fail);
double GET_DBL(string Name,double fail);
string GET_STR(string Name,string fail);
int ADD_INT(string Name,int val);
double ADD_DBL(string Name,double val);
int RND_UNIFORM_INT(int lower,int upper);
string RND_UNIFORM_INT_VEC(int nbr_rnd,int lower,int upper);
int NEWS_BULLETIN(int request,int allmessages);
int REQ_HIST_DATA(int uidcontract,string endTime,string startTime,int barSizeCode,string whatToShow,int useRTH,int formatDate,int forc);
double GET_HIST_DATA_IDX(int idatavector,int idx,int iddata);
double GET_HIST_DATA_DATE(int idatavector,double datesecs,int iddata);
int REQ_CURRENT_TIME(int timeout);
string REQ_SCANNER_PARAMETER(int force);
int REQ_SCANNER_SUBSCRIPTION(int numberOfRows,string instrument,string locationCode,string scanCode,double abovePrice,double belowPrice,int aboveVolume,double marketCapAbove,double marketCapBelow,string stockTypeFilter,string moodyRatingAbove,string moodyRatingBelow,string spRatingAbove,string spRatingBelow,int excludeConvertible,int averageOptionVolumeAbove,string scannerSettingPairs,double couponRateAbove,double couponRateBelow,string maturityDateAbove,string maturityDateBelow,string options);
int CANCEL_SCANNER_SUBSCRIPTION(int id);
int DISCONNECT_DB(int uid);
int CONNECT_DB(int uid,string password);
int SQL(int uid,string query,string sep,int timeout,string filename);
string SQL2(int uid,string query,string colsep,string rowsep);
int SET_EVENT_HANDLER(int callback_handle,int subscription_mask,int traget_thread_id);
int SET_EVENT_HANDLER_OLE(int callback_handle,int subscription_mask,int traget_thread_id);
int REM_EVENT_HANDLER(int pcb,int threadid);
string TIME_STRING(int time,string format);
int TIME_COMPONENT(int time,int component);
double TIMEZONE_CONVERSION(double source_time,string source_time_zone,string dest_time_zone);
int SECONDS_1970();
double MILLISECONDS_1970();
int SET_TIMER(int delay);
int KILL_TIMER(int id);
int PLAY_SOUND(string soundfilename,int loop,int exclusive);
int REQ_FUNDAMENTAL_DATA(int uidcontract,string reportype,string filename);
int REQ_EXECUTION_DATA(int clientid,string account,string time,string symbol,string sectype,string exchange,string side);
int PARSE_XML(string data,int isfile);
string GET_EXECUTION_DATA(string execid,int property_id);
int GET_EXECUTION_DATA_SQL(string sql);
string GET_EXECUTION_DATA_SQL2(string sql);
string QUERY_DATABASE(int databaseid,string sql,string colsep,string rowsep);
int QUERY_DATABASE_CB(int databaseid,string sql,string colsep);
string INIT_TWSLINK(string setupfile,string logroot);
string U2A(string su);
 A2U(string su);
int RISK_SET(int type,string account,int active,double value,double nlv_offset,int hour,int weekday,int monthday,int trail,double trailamount);
int RISK_ENABLE(int type,string account,int enable);
int RISK_TRIGGERED(string account);
double RISK_VALUE(int type,string account,int valueid);
int RISK_RESET(int type,string account,double nlv_offset);
int SET_HTML_REPORT(int active,string reportname,string ftp_config,string mail_config,int periodminutes);
int MAKE_HASH(string mystring);
string LOGGER(int i1,int i2,double d1,double d2,string s1,string s2);
int LOGGER2(int i1,int i2,int i3,int i4,int i5,int i6,int i7,int i8);
string TEST_1(int i,int ipt,double d,string s);
#import
