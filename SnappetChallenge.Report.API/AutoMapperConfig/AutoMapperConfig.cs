using AutoMapper;

namespace SnappetChallenge.Report.API.AutoMapperConfig
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<ChildResultMapperConfig>();
            });

            return mappingConfig.CreateMapper();
        }
    }
}
