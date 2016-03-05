using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RetireHappy.Startup))]
namespace RetireHappy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
