using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimApi.Entities
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address1 { get; set; }

        [MaxLength(100)]
        public string? Address2 { get; set; }

        [MaxLength(100)]
        public string? Address3 { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        public bool Active { get; set; }

        public DateTime? InsuranceEndDate { get; set; }

        public ICollection<Claim> Claims { get; set; }
              = new List<Claim>();

    }
}
