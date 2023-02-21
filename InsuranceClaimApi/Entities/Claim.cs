using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimApi.Entities
{
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string UCR { get; set; }

        [ForeignKey("CompanyId")]
        public int CompanyId { get; set; }

        [Required]
        public DateTime ClaimDate { get; set; }

        [Required]
        public DateTime LossDate { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Assured Name", TypeName = "VARCHAR(100)")]
        public string AssuredName { get; set; } = string.Empty;

        [Column("Incurred Loss", TypeName = "Decimal(15,2)")]
        public Decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }

    }
}
