using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SceneOfCustoms.Startup))]
namespace SceneOfCustoms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
