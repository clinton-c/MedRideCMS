using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedRideCMS.Startup))]
namespace MedRideCMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
