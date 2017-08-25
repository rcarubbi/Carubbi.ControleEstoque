using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Carubbi.ControleEstoque.Web.Startup))]
namespace Carubbi.ControleEstoque.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
