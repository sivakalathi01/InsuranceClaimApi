using System.ComponentModel.DataAnnotations;

namespace InsuranceClaimApi.Models
{
    public class ClaimForUpdateDto
    {
        [Required(ErrorMessage = "UCR must be provided")]
        [MaxLength(20)]
        public string UCR { get; set; } = string.Empty;

        [Required(ErrorMessage = "Claim date must be provided")]
        public DateTime ClaimDate { get; set; }

        [Required(ErrorMessage = "LossDate must be provided")]
        public DateTime LossDate { get; set; }

        [Required(ErrorMessage = "Assured Name must be provided")]
        [MaxLength(100)]
        public string AssuredName { get; set; } = string.Empty;

        public Decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
