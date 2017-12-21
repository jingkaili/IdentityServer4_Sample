����Quickstart8_EntityFrameworkStorage����IdentityServer/IdentityServer4.Samples�� Quickstarts/8_EntityFrameworkStorage�������޸Ķ�����
���ӹ��ܣ�
һ����ӽ�ɫ����api���ʣ�
	1���ӿ���Դ���ӽ�ɫ���ƣ�		   ��ĿApi ������IdentityController���޸�[Authorize]Ϊ[Authorize(Roles ="admin")]
	2����֤������ΪAPI��Դ���û����ӽ�ɫ������ ��ĿQuickstartIdentityServer �ļ�Config��
		api��Դ�����û���ɫ����������GetApiResources���޸� new ApiResource("api1", "My API")Ϊnew ApiResource("api1", "My API"){ UserClaims = new List<string> {"role"}}
		�û���Ϣ���ӽ�ɫ������	 ����GetUsers��alice���������� new Claim("role", "admin") bob���������� new Claim("role", "user")
	�����ResourceOwnerClient��MvcClient���� alice���ʳɹ���bobʧ��
�����Զ��壨���Է����������ݿ⣩��֤�û���¼��
	1����ĿQuickstartIdentityServer �ļ�Startup ����ConfigureServices��ע�͵� .AddTestUsers(Config.GetUsers()) �����û���֤��.AddResourceOwnerValidator<CResourceOwnerPasswordValidator>()��CResourceOwnerPasswordValidator��ʵ�����ļ���Oauth2�С�
	2����ĿQuickstartIdentityServer ������Quickstart/Startup/AccountController ����Login:�޸���֤����_users.ValidateCredentialsΪCResourceOwnerPasswordValidator.validPwd�����ú���await HttpContext.SignInAsyncʱ����������������
	�����1ʹ��ResourceOwnerClient�ɹ����ʣ�2ʹ��MvcClient�ɹ�����  ע��ֻ��������alice/password���û�/���룩��ʵ�֡�

���в��裺
1��ʹ��vs2017
2����ĿApi�޸������˿ڣ���Ŀ���Ѿ��޸Ĺ��ˣ��� �ļ�Program���� .UseUrls("http://localhost:5001")���������ʹ��vs���Կͻ��ˣ�MvcClient JavaScriptClientͬ����Ҫ���Ķ˿�
3������ResourceOwnerClient ��MvcClient ��Api �� QuickstartIdentityServer
	�����������Ŀ¼ �����зֱ����У� 
		dotnet QuickstartIdentityServer.dll /seed			//����seed ������Ǩ�ƣ����ݿⲢ���ӻ������ݣ�����Ѿ���������ȥ��seed����
		dotnet Api.dll 
	Ȼ�����пͻ��˽��в��ԡ�
	���������Ŀ¼�޷����У�������ȷ����������ļ�ϵͳ��QuickstartIdentityServer MvcClient JavaScriptClient�������뷢���ļ������С�

�޸��ڴ����ݿ⵽���ػ��߷�������
	������� Entityframeworkcore.sqlserver �� ����->nuget��������->������������nuget�����...    ��������װ��
	�޸������ַ����� �ļ�Startup connectionString��ֵ����Ϊ�� Server=127.0.0.1;Database=IdentityServer4_Sample8;uid=sa;pwd=123456;MultipleActiveResultSets=true
	���������� dotnet QuickstartIdentityServer.dll /seed  �������ݿ⣬����������

���IdentityServer���ݿ�Ǩ���ļ�û�����ɣ�
	IdentityServer��ĿĿ¼�����У�
	dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
	dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb





��Ŀ�ο��ĵ���
	http://www.cnblogs.com/ideck/p/ids_index.html
	http://www.cnblogs.com/xishuai/category/1025768.html