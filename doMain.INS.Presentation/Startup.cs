using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(doMain.INS.Presentation.Startup))]
namespace doMain.INS.Presentation
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
