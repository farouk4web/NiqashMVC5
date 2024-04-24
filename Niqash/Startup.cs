using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Niqash.Startup))]
namespace Niqash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
