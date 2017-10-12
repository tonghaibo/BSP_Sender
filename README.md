# ChatServer
c# tcp server

release目录下的文件是erlang版的模拟程序，模拟设备定时发送指令，每台设备每15s发送一次。
具体使用方法请修改release\eze_simulator_th目录下的sys.config文件。
配置文件格式：
[
  { eze_simulator_th, [{num,1},{mul, 10000},{sta, 1300000}, {ip,"127.0.0.1"},{port,10003}]}
].
需要修改的部分参数说明：
mul表示模拟设备数，num表示设备倍数，总模拟设备数等于num*mul,ip和端口为需要送的服务器ip和端口号，sta表示设备编号起始值，后面设备编号自增，其他参数暂不做修改。
修改好参数后运行release\eze_simulator_th目录下的run_win.cmd文件即可。

