using AutoMapper;

namespace InsuranceClaimApi.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Entities.Company, Models.CompanyWithOutClaimsDto>();

            CreateMap<Entities.Company, Models.CompanyDto>();
        }
    }
}
