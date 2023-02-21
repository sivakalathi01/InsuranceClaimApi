using AutoMapper;

namespace InsuranceClaimApi.Profiles
{
    public class ClaimProfile : Profile
    {
        public ClaimProfile()
        {
            CreateMap<Entities.Claim, Models.ClaimDto>();

            CreateMap<Models.ClaimForUpdateDto, Entities.Claim>();

            CreateMap<Entities.Claim, Models.ClaimForUpdateDto>();
        }
    }
}
