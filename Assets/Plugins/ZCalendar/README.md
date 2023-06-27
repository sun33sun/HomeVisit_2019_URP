# unity日历

#### 介绍
操作简单，功能全面的日历选择器，带有自定义配置项；
1、可自定义初始化时间
2、可控制显示前后月的日期显示
3、可控制显示农历日期
4、可做静态日历显示
5、尺寸自适应
6、可控制超出当前日期是否可以点击
7、如果做为弹窗使用，点击周围可隐藏控件
8、可选择时间范围
如有任何可优化的地方，烦请各位大佬友情指出，感恩的♥
交流请加企鹅群：747648080
本人会积极回复交流的♥♥♥

#### 软件架构
软件架构说明


#### 安装教程

1.  下载拖入工程中
2.  将Prefabs/Zcalendar预制体拖入到canvas下
3.  Inspector面板中即可看到所有显示配置项
4.	点击运行即可使用

#### 使用说明
config：
1.  Awake2Init：true => 以当前时间初始化，false => 不初始化，可通过ZCalendar中的Init方法初始化；
2.  AutoFillDate：true => 自动补充前后月日期，false => 不显示前后月日期；
3.  IsUnexpiredTimeCanClick：true => 超出当前时间可点击，false => 超出当前时间不可点击；
4.	Lunar：true => 显示农历日期，false => 不显示农历日期;
5.	IsPopupCalendar：true => 显示时，点击周边或者点击日期可隐藏日历，如果默认为隐藏状态，可关闭子对象bak，本身有管理脚本，不可隐藏，false => 为长显示日历
6.	IsStaticCalendar：true => 为静态日期显示日历，不可操作，false => 可操作日历
7.	AutoSetItemSize：true => 日期显示部分可根据尺寸自适应显示，false => 不会自适应显示
8.	RangeCalendar：true => 可选择时间范围，false => 可选择单天日期

API(ZCalendar中):
1.	UpdateDateEvent（广播事件）：数据更新时，可获取到每一个日期对象；
2.	ChoiceDayEvent（广播事件）：可以获取到点击的某一个日期对象；
3.	RangeTimeEvent（广播事件）：获取选择的区间事件对象；
4.	CompleteEvent（广播事件）：日历加载结束；
5.	CrtTime（属性）：手动获取当前选择日期对象；
6.	Init（方法）：日历初始化，不传参数 => 当前时间，参数类型DateTime => 按照所传参数初始化，参数类型string，YYYY-MM-DD => 按照所传参数初始化


#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
