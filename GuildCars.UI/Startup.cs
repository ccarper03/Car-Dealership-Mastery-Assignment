using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuildCars.UI.Startup))]
namespace GuildCars.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
