namespace InsuranceClaimApi.Models
{
    public class ClaimDto
    {
        public int Id { get; set; }
        public string UCR { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; } = string.Empty;
        public Decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int ClaimNumberOfDaysOld => (int)DateTime.UtcNow.Subtract(ClaimDate).TotalDays;

    }
}
