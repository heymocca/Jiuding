using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(airtton.Startup))]
namespace airtton
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
