using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // 在這裡設定服務（Services）
        // 例如，註冊資料庫上下文、加入身份驗證服務、設定記憶體快取、依賴注入等等
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // 在這裡設定中介軟體（Middleware）
        // 例如，加入錯誤處理中介軟體、日誌紀錄中介軟體、設定路由、加入認證和授權等等
    }
}
