using InsuranceClaimApi.Entities;

namespace InsuranceClaimApi.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string PostCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool Active { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public bool ActiveInsurancePolicy => Active;
        public ICollection<Claim> Claims { get; set; }
             = new List<Claim>();

    }
}
