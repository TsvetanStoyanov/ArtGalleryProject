using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DonaStoyanova.Startup))]
namespace DonaStoyanova
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
