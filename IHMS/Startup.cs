using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IHMS.Models;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // 建立 Configuration 物件並將設定檔加入到 Configuration 中
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // 將 Configuration 服務註冊到 DI 容器中，這樣其他地方就可以注入使用
        services.AddSingleton(configuration);

        // 注册数据库上下文
        string connectionString = configuration.GetConnectionString("IHMSConnection");
        services.AddDbContext<IhmsContext>(options =>
            options.UseSqlServer(connectionString));

        // 其他服務的設定...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // 在這裡設定中介軟體（Middleware）
        // 例如，加入錯誤處理中介軟體、日誌紀錄中介軟體、設定路由、加入認證和授權等等
    }
    

}
