# 1.5版本 正式发布的最后一个版本！ #
	- 版本说明

		- v1.0
			* 更新了IP框的默认地址
			* 更新在已经启动后再点START或未启动时点STOP，会给用户警告

		- v1.1
			* 增加了设备的查看
			* 增加了设备类
		
		- v1.2
			* 重构代码，修复TCP接受不全bug
			* 加入接收udp报文解析（版本号等，名字尚未解析，明天上班再干）
			* 重构师准备加点工资，希望PM考虑一下

		- v1.3
			* 增加查看设备属性的Event
			* 将Udp关闭从Stop按钮事件类移动到接收完毕即关闭
			* 增加定时器线程，为5s刷新做准备
			* 完成了5s刷新的初步构想，但发现实现都失败了:-(
			* 架构师喝了一瓶无糖可乐
			* 移除了更新日志，更新说明全放在了这里
			
		- v1.4
			* 增加本地p2p功能
			* 完善了用户界面，使其更加合理
			* 加入并完善了setTime轮询时间设置功能
			* 架构师中午吃了川蜀百味，表示还不错，毕竟已经连着一周吃了

		- v1.5
			* 增加了远程子网轮询的功能
			* 加了注释！
			
		==================

		- 已知Bug
			* 在子网广播返回设备列表时顺序可能不太对