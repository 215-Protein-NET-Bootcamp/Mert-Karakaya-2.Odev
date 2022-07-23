using System;
using System.Collections.Generic;
using System.Text;
using CompanyAPI.Core;
using CompanyAPI.Data.Model;
using CompanyAPI.Data.Repositories;

namespace CompanyAPI.Service.Services
{
    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly ICountryRepository countryRepository;
        public CountryService(ICountryRepository repository, IUnitofWork unitofWork) : base(repository, unitofWork)
        {
            this.countryRepository = repository;
        }
    }
}
