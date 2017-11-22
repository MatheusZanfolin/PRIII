using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sistema_Consultório_Médico_PRIII.Startup))]
namespace Sistema_Consultório_Médico_PRIII
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
