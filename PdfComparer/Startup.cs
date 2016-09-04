using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PdfComparer.Startup))]
namespace PdfComparer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
