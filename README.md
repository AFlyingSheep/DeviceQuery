# 完结撒花🎉🎉🎉 #
## 1.6版本如约而至！！ ##
- 版本介绍
	- 
	- 随着1.6.4版本的完成，本项目核心代码部分基本完成，功能已经全部实现，撒花！🎉

- 版本说明
	- 
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
	- v1.6
		* 增加了start按钮与stop按钮只有一个可用的规则
		* 重构了udp解包后添加设备的代码，设备实现树形存储，并加上了图标
		* 修改了在按钮的每个模式下的Enable属性
		* 增加刷新按钮，可用频率根据轮询时间确定
		* 将combobox的默认值放在了form窗体的加载事件中，防止再次被顶没
			 - 这个默认值消失气死我了！ 
			 - 莫非每次添加新控件都会重置初始化窗体代码？
		* 晨晨giegie的奶茶真好喝，谢谢晨晨哥哥！