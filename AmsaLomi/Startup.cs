using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmsaLomi.Startup))]
namespace AmsaLomi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
