using AutoMapper;
using System.Net;

namespace ETSystem.Cashing.Common
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<CookieContainer, CookieContainer>();
        }
    }
}
