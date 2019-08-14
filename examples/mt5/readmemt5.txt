- On Vista or Windows 7, MetaTrader5 custom experts are located at a somehow secret place below users folder. For instance:

	C:\Users\general\AppData\Roaming\MetaQuotes\Terminal\D0E8D09F77C8CF37AD8BA350E51DF075\MQL5

Normally, you can't see the AppData folder in users folder. To do this, allow to show system files.

Let's say installdir=C:\Users\general\AppData\Roaming\MetaQuotes\Terminal\D0E8D09F77C8CF37AD8BA350E51DF075 , then do the following:

- copy twslink2demo.ex5 to mt5 installdir/MQL5/Experts
- copy twslink2demo.mq5 to mt5 installdir/MQL5/Experts
- copy twslink2.mqh to mt5 installdir/MQL5/Include


Open MetaTrader5 and attach expert advisor
	twslink2demo
to chart.