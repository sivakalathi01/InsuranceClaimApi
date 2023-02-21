using InsuranceClaimApi.Entities;

namespace InsuranceClaimApi.Services
{
    public interface IInsuranceRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyAsync(int companyId, bool includeClaim);
        Task<bool> CompanyExistsAsync(int companyId);
        Task<IEnumerable<Claim>> GetClaimsForCompanyAsync(int companyId);
        Task<Claim?> GetClaimDetailsAsync(int companyId, int claimId);
        Task<bool> SaveChangesAsync();
    }
}