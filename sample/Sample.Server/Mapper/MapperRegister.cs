using Mapster;

namespace Sample.Server.Mapper
{
    public class MapperRegister : IRegister
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="config"></param>
        public void Register(TypeAdapterConfig config)
        {
            new GRpcMapperRegister().Register(config);
        }
    }
}
