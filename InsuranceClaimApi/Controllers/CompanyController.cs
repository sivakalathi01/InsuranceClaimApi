using AutoMapper;
using InsuranceClaimApi.Models;
using InsuranceClaimApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceClaimApi.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public CompanyController(ILogger<CompanyController> logger, IInsuranceRepository insuranceRepository, IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _insuranceRepository = insuranceRepository ??
                throw new ArgumentNullException(nameof(insuranceRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(insuranceRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyWithOutClaimsDto>>> GetCompanies()
        {
            var companyEntities = await _insuranceRepository.GetCompaniesAsync();
            return Ok(_mapper.Map<IEnumerable<CompanyWithOutClaimsDto>>(companyEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(int id, bool includeClaim = false)
        {

            var companyEntity = await _insuranceRepository.GetCompanyAsync(id, includeClaim);
            if (companyEntity == null)
            {
                _logger.LogInformation(
                    $"Company with id {id} wasn't found when accessing companies.");
                return NotFound();
            }

            if (includeClaim)
            {
                return Ok(_mapper.Map<CompanyDto>(companyEntity));
            }

            return Ok(_mapper.Map<CompanyWithOutClaimsDto>(companyEntity));
        }

    }
}
