using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WisconsinTrackClubWebsite.Startup))]
namespace WisconsinTrackClubWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
