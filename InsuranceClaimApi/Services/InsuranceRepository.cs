using InsuranceClaimApi.DbContexts;
using InsuranceClaimApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimApi.Services
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly InsuranceContext _context;
        public InsuranceRepository(InsuranceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            var companies = new List<Company>
            {
                new Company
                {
                    Name ="AbcLimitd",
                    Address1 = "255, Hertsmere Road",
                    Address2 = "London",
                    PostCode = "E14 4ED",
                    Country = "UnitedKingdom",
                    Active = true,

                    Claims = new List<Claim>()
                    {
                        new Claim { UCR = "UCR fees data" ,
                                    ClaimDate = new DateTime(2011, 1, 11, 4, 0, 15),
                                    LossDate = new DateTime(2011, 1, 05, 4, 0, 15),
                                    AssuredName = "John Smith",
                                    IncurredLoss = 100,
                                    Closed = false},

                        new Claim { UCR = "UCR fees data1" ,
                                    ClaimDate = new DateTime(2010, 2, 21, 4, 0, 15),
                                    LossDate = new DateTime(2010, 2, 17, 4, 0, 15),
                                    AssuredName = "David Paul",
                                    IncurredLoss = 200,
                                    Closed = false},

                         new Claim { UCR = "UCR fees data2 " ,
                                    ClaimDate = new DateTime(2012, 3, 15, 4, 0, 15),
                                    LossDate = new DateTime(2012, 3, 10, 4, 0, 15),
                                    AssuredName = "Michael Jackson",
                                    IncurredLoss = 300,
                                    Closed = false}
                    }
                },
                new Company
                {
                    Name ="XyzLimitd",
                    Address1 = "10, Hertsmere Road",
                    Address2 = "London",
                    PostCode = "E14 4ED",
                    Country = "UnitedKingdom",
                    Active = true,

                    Claims = new List<Claim>()
                    {
                        new Claim { UCR = "UCR fees data3" ,
                                    ClaimDate = new DateTime(2017, 1, 19, 4, 0, 15),
                                    LossDate = new DateTime(2017, 1, 4, 4, 0, 15),
                                    AssuredName = "John Smith",
                                    IncurredLoss = 100,
                                    Closed = false},

                        new Claim { UCR = "UCR fees data4" ,
                                    ClaimDate = new DateTime(2018, 2, 11, 4, 0, 15),
                                    LossDate = new DateTime(2018, 2,8, 4, 0, 15),
                                    AssuredName = "David Paul",
                                    IncurredLoss = 200,
                                    Closed = false},

                         new Claim { UCR = "UCR fees data5 " ,
                                    ClaimDate = new DateTime(2019, 3, 9, 4, 0, 15),
                                    LossDate = new DateTime(2019, 3,3, 4, 0, 15),
                                    AssuredName = "Michael Jackson",
                                    IncurredLoss = 300,
                                    Closed = false}
                    }
                },
                new Company
                {
                    Name ="ZyxLimitd",
                    Address1 = "300, Hertsmere Road",
                    Address2 = "London",
                    PostCode = "E14 4ED",
                    Country = "UnitedKingdom",
                    Active = true,

                    Claims = new List<Claim>()
                    {
                        new Claim { UCR = "UCR fees data6" ,
                                    ClaimDate = new DateTime(2014, 1, 19, 4, 0, 15),
                                    LossDate = new DateTime(2014, 1, 4, 4, 0, 15),
                                    AssuredName = "David Philips",
                                    IncurredLoss = 100,
                                    Closed = true}

                    }
                }

            };
            _context.Companies.AddRange(companies);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {

            var list = await _context.Companies
                .Include(a => a.Claims)
                .OrderBy(a => a.Name)
                .ToListAsync();

            return list;

        }

        public async Task<Company?> GetCompanyAsync(int companyId, bool includeClaim)
        {
            if (includeClaim)
            {
                return await _context.Companies.Include(a => a.Claims)
                        .Where(c => c.Id == companyId).FirstOrDefaultAsync();
            }

            return await _context.Companies
                        .Where(c => c.Id == companyId).FirstOrDefaultAsync();

        }

        public async Task<bool> CompanyExistsAsync(int companyId)
        {
            return await _context.Companies.AnyAsync(c => c.Id == companyId);
        }

        public async Task<IEnumerable<Claim>> GetClaimsForCompanyAsync(int companyId)
        {
            return await _context.Claims
                    .Where(c => c.CompanyId == companyId)
                    .ToListAsync();
        }

        public async Task<Claim?> GetClaimDetailsAsync(int companyId, int claimId)
        {
            return await _context.Claims
                    .Where(c => c.CompanyId == companyId && c.Id == claimId)
                    .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
