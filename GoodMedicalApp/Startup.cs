using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GoodMedicalApp.Startup))]
namespace GoodMedicalApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
