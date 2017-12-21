工程Quickstart8_EntityFrameworkStorage是在IdentityServer/IdentityServer4.Samples的 Quickstarts/8_EntityFrameworkStorage基础上修改而来。
增加功能：
一、添加角色控制api访问：
	1、接口资源增加角色控制：		   项目Api 控制器IdentityController：修改[Authorize]为[Authorize(Roles ="admin")]
	2、认证服务器为API资源和用户增加角色声明： 项目QuickstartIdentityServer 文件Config：
		api资源增加用户角色声明：函数GetApiResources：修改 new ApiResource("api1", "My API")为new ApiResource("api1", "My API"){ UserClaims = new List<string> {"role"}}
		用户信息增加角色声明：	 函数GetUsers：alice的声明增加 new Claim("role", "admin") bob的声明增加 new Claim("role", "user")
	结果：ResourceOwnerClient和MvcClient都是 alice访问成功，bob失败
二、自定义（可以访问已有数据库）验证用户登录：
	1、项目QuickstartIdentityServer 文件Startup 函数ConfigureServices：注释掉 .AddTestUsers(Config.GetUsers()) 增加用户验证器.AddResourceOwnerValidator<CResourceOwnerPasswordValidator>()。CResourceOwnerPasswordValidator的实现在文件夹Oauth2中。
	2、项目QuickstartIdentityServer 控制器Quickstart/Startup/AccountController 函数Login:修改验证函数_users.ValidateCredentials为CResourceOwnerPasswordValidator.validPwd；调用函数await HttpContext.SignInAsync时，增加声明参数。
	结果：1使得ResourceOwnerClient成功访问；2使得MvcClient成功访问  注：只是增加了alice/password（用户/密码）的实现。

运行步骤：
1、使用vs2017
2、项目Api修改启动端口（项目中已经修改过了）： 文件Program增加 .UseUrls("http://localhost:5001")；如果不是使用vs调试客户端，MvcClient JavaScriptClient同样需要更改端口
3、编译ResourceOwnerClient 、MvcClient 、Api 、 QuickstartIdentityServer
	进入各自生成目录 命令行分别运行： 
		dotnet QuickstartIdentityServer.dll /seed			//参数seed 创建（迁移）数据库并增加基础数据；如果已经创建好则去掉seed参数
		dotnet Api.dll 
	然后运行客户端进行测试。
	如果在生成目录无法运行，则可以先发布到本地文件系统（QuickstartIdentityServer MvcClient JavaScriptClient），进入发布文件夹运行。

修改内存数据库到本地或者服务器：
	添加依赖 Entityframeworkcore.sqlserver ： 工具->nuget包管理器->管理解决方案的nuget程序包...    收索并安装。
	修改连接字符串： 文件Startup connectionString的值设置为： Server=127.0.0.1;Database=IdentityServer4_Sample8;uid=sa;pwd=123456;MultipleActiveResultSets=true
	命令行运行 dotnet QuickstartIdentityServer.dll /seed  生成数据库，并启动程序

如果IdentityServer数据库迁移文件没有生成：
	IdentityServer项目目录下运行：
	dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
	dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb





项目参考文档：
	http://www.cnblogs.com/ideck/p/ids_index.html
	http://www.cnblogs.com/xishuai/category/1025768.html