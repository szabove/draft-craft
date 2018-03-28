using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DraftCraft.Startup))]
namespace DraftCraft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
