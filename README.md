# iBlog
美观大方、功能全面的个人博客系统。

### 技术构成
* 后端 [ASP.NET MVC 5](http://www.asp.net/mvc)
* 前端 [jQuery](http://jquery.com/)、[Bootstrap](http://getbootstrap.com/)
* 持久化 [MongoDB](https://www.mongodb.org/)
* 缓存 [Redis](http://redis.io/)

### 页面预览
* 前台预览  
查看[我的博客](http://www.skysun.name)
* 后台管理预览  
![image](https://github.com/eshengsky/iBlog/blob/master/iBlog/iBlog.WebUI/Content/Img/newarticle.png)

### 快速开始
* 准备条件  
在Windows上安装[IIS](http://www.iis.net/)、[MongoDB](https://www.mongodb.org/)、[Redis](https://github.com/MSOpenTech/redis/releases)。
* 发布站点  
使用Release模式编译iBlog解决方案，将iBlog.WebUI作为网站目录发布到IIS。
* 参数配置  
在iBlog.WebUI下的web.config中，查找并修改MongoDB连接信息：  
`<add name="MongoDB" connectionString="mongodb://localhost:27017/iBlog" />`  
Redis的配置信息：  
`<RedisConfig WriteServerList="192.168.1.101:6379" ReadServerList="192.168.1.101:6379" MaxWritePoolSize="60" MaxReadPoolSize="60" AutoStart="true"/>`  
站点成功启动后，可以在前台任一页面的底部找到"后台管理"链接，默认管理员账号：admin，密码：123456，可在web.config中修改：   
`<add key="UserName" value="admin" />`    
`<add key="PwdMd5" value="e10adc3949ba59abbe56e057f20f883e" />`  
在"后台管理-系统设置"页面中，可以配置其它参数。  
Enjoy it!
 

### 许可协议
基于[GPL](https://github.com/eshengsky/iBlog/blob/master/LICENSE)开源许可协议。

