using System.IO;
using Microsoft.AspNet.Builder;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;

namespace WebApplication1
{
    public class Startup
    {
        private readonly IApplicationEnvironment _env;

        public Startup(IApplicationEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (ctx) =>
            {
                var filePath = Path.Combine(_env.ApplicationBasePath, "Testfile");
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024 * 64,
                    FileOptions.Asynchronous | FileOptions.SequentialScan))
                {
                    await fileStream.CopyToAsync(ctx.Response.Body);
                }
            });
        }
    }
}
