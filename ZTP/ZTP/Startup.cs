using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZTP.Startup))]
namespace ZTP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
