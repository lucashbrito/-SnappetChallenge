using AutoMapper;
using SnappetChallenge.Report.API.Controllers.Report.v1.Model;
using SnappetChallenge.Report.Domain;

namespace SnappetChallenge.Report.API.AutoMapperConfig
{
    public class ChildResultMapperConfig : Profile
    {
        public ChildResultMapperConfig()
        {
            AllowNullCollections = true;

            CreateMap<ChildResultRequestV1, ChildResult>(MemberList.None)
                  .BeforeMap((src, dest) =>
                  {
                      src.SubmitDateTime = src.SubmitDateTime.Trim();
                  });

            CreateMap<ChildResult, ChildResultRequestV1>(MemberList.None);

        }
    }
}
