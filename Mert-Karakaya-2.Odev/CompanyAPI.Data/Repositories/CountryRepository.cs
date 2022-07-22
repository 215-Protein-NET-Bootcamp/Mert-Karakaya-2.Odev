﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CompanyAPI.Core;
using CompanyAPI.Data.Context;
using CompanyAPI.Data.Model;
using Dapper;

namespace CompanyAPI.Data.Repositories.Concrete
{
    class CountryRepository : IBaseRepository<Country>
    {
        private readonly DapperDbContext dapperDbContext;

        public CountryRepository(DapperDbContext dapperDbContext) : base()
        {
            this.dapperDbContext = dapperDbContext;
        }
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            var sql = "SELECT * FROM country";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Country>(sql);
                return result;
            }
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Country WHERE countryid = @Id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstAsync<Country>(query, new { id });
                return result;
            }
        }

        public async Task InsertAsync(Country entity)
        {
            var query = "INSERT INTO country (countryname, continent,currency) " +
                        "VALUES (@countryname, @continent,@currency)";

            var parameters = new DynamicParameters();
            parameters.Add("countryname", entity.CountryName, DbType.String);
            parameters.Add("continent", entity.Continent, DbType.String);
            parameters.Add("currency", entity.Currency, DbType.String);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public void Update(Country entity)
        {
            var query = "UPDATE country SET countryname = @countryname , continent = @continent , currency = @currency" +
                        "WHERE countryid = @countryid";

            var parameters = new DynamicParameters();
            parameters.Add("countryname", entity.CountryName, DbType.String);
            parameters.Add("continent", entity.Continent, DbType.String);
            parameters.Add("currency", entity.Currency, DbType.String);
            parameters.Add("countryid", entity.CountryId, DbType.Int32);

            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(query, parameters);
            }
        }

        public void Delete(Country entity)
        {
            var query = "DELETE FROM country WHERE countryid = @Id";
            using (var connection = dapperDbContext.CreateConnection())
            {
                connection.Open();
                connection.Execute(query, new { entity.CountryId });
            }
        }
        public async Task<IEnumerable<Country>> Where(Expression<Func<Country, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
