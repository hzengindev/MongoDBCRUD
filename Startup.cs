using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoDbSample.Startup))]
namespace MongoDbSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
