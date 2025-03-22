using AutoMapper;

namespace ETSystem.Cashing.Common
{
    public class ObjectMapper
    {
        private static readonly object s_object = new object();

        private static ObjectMapper _defalut = null;

        public IMapper Mapper { get; set; }

        public static ObjectMapper Default
        {
            get
            {
                if (_defalut == null)
                {
                    lock (s_object)
                    {
                        if (_defalut == null)
                        {
                            _defalut = new ObjectMapper();
                        }
                    }
                }

                return _defalut;
            }
        }

        public ObjectMapper() 
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapProfile>();
            });

            Mapper = configuration.CreateMapper();
        }
    }
}
