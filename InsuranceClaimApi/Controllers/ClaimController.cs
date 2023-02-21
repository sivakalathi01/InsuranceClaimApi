using AutoMapper;
using InsuranceClaimApi.Models;
using InsuranceClaimApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimApi.Controllers
{
    [Route("api/companies/{companyId}/claim")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly ILogger<ClaimController> _logger;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public ClaimController(ILogger<ClaimController> logger, IInsuranceRepository insuranceRepository, IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _insuranceRepository = insuranceRepository ??
                throw new ArgumentNullException(nameof(insuranceRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(insuranceRepository));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimDto>>> GetClaimsForCompany(int companyId)
        {
            if (!await _insuranceRepository.CompanyExistsAsync(companyId))
            {
                _logger.LogInformation(
                    $"Company with id {companyId} wasn't found when accessing claims.");
                return NotFound();
            }

            var claimEntities = await _insuranceRepository.GetClaimsForCompanyAsync(companyId);
            if (claimEntities == null)
            {
                _logger.LogInformation(
                    $"Claims weren't found for company with id {companyId}.");
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ClaimDto>>(claimEntities));
        }

        [HttpGet("{claimId}")]
        public async Task<ActionResult<ClaimDto>> GetClaimDetailsAsync(int companyId, int claimId)
        {
            if (!await _insuranceRepository.CompanyExistsAsync(companyId))
            {
                _logger.LogInformation(
                    $"Company with id {companyId} wasn't found when accessing claims.");
                return NotFound();
            }

            var claimEntity = await _insuranceRepository.GetClaimDetailsAsync(companyId, claimId);
            if (claimEntity == null)
            {
                _logger.LogInformation(
                   $"Claims wasn't found for company with id {companyId}.");
                return NotFound();
            }

            return Ok(_mapper.Map<ClaimDto>(claimEntity));

        }

        [HttpPut("{claimId}")]
        public async Task<ActionResult> UpdateClaim(int companyId, int claimId,
            ClaimForUpdateDto claim)
        {
            if (!await _insuranceRepository.CompanyExistsAsync(companyId))
            {
                _logger.LogInformation(
                    $"Company with id {companyId} wasn't found when accessing claims.");
                return NotFound();
            }

            var claimEntity = await _insuranceRepository.GetClaimDetailsAsync(companyId, claimId);
            if (claimEntity == null)
            {
                _logger.LogInformation(
                   $"Claim wasn't found for company with id {companyId}.");
                return NotFound();
            }

            _mapper.Map(claim, claimEntity);

            await _insuranceRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{claimId}")]
        public async Task<ActionResult> PartiallyUpdateClaim(int companyId, int claimId,
            JsonPatchDocument<ClaimForUpdateDto> patchDocument)
        {
            if (!await _insuranceRepository.CompanyExistsAsync(companyId))
            {
                _logger.LogInformation(
                    $"Company with id {companyId} wasn't found when accessing claims.");
                return NotFound();
            }

            var claimEntity = await _insuranceRepository.GetClaimDetailsAsync(companyId, claimId);
            if (claimEntity == null)
            {
                _logger.LogInformation(
                   $"Claim wasn't found for company with id {companyId}.");
                return NotFound();
            }

            var claimToPatch = _mapper.Map<ClaimForUpdateDto>(claimEntity);

            patchDocument.ApplyTo(claimToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                _logger.LogInformation(
                   $"Partial update throws error for company with id {companyId} and claim with id {claimId}.");
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(claimToPatch))
            {
                _logger.LogInformation(
                   $"Partial update validation throws error for company with id {companyId} and claim with id {claimId}.");
                return BadRequest(ModelState);
            }

            _mapper.Map(claimToPatch, claimEntity);

            await _insuranceRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
