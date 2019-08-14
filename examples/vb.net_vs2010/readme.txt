Running sample.

- First you need to connect to TWS. In order to do this, TWS must accept API client
connection. For this, make sure API settings are correctly in TWS.
-> TWSLink\documentation\TWSSetup.doc

- Once connected, you need to register your Contract. Contract Registration returns, if successfull,
  a unique number to identify this contract. this makes it a lot easier to reference a contract in
  each function your need contract specification (placing orders, requesting market data,etc.)
  
  if the registration is successfull - so TWS accept it - the unique id is stored to the form.member
  _uid.
  this _uid is used to be passed for market data and place order function.
  
- to get market data, you need contract to be registered. then press <Request Data>.
  the market data are passed to the callback, which routes the data to function
	
	UpdateMarketData
	
- to place an order, you also need a valid contract first. fill out size and limit or stop (or chose)
  market and press <Place Order>. if order was sucessfully placed, you get an order uid returned:
  _oid.
  if you want to modify order, simply leave this _oid in the edit field (blue). to place a new order
  set this field to 0.
  
  
  
  the source code contains some comments beginning with
  
  NOTE: ...
  
  each note has subject along with a number. the number is to be seen as ordinal of an action
  in the specific task, for instance: requesting market data_1 comments the first step to get
  and process market data.