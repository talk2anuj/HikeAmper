using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HikeAmper.Startup))]
namespace HikeAmper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
