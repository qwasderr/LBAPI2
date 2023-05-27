using Xunit;
using LBAPI2.Controllers;
using LBAPI2.Models;
using LBAPI2.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc.Routing;

namespace LBAPI2.Tests
{
    public class CountriesControllerTests
    {
        [Fact]
        public async void TestGetCountries()
        {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries)
                .ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            var result = (await controller.GetCountries()).Value;
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public async void TestGetCountryExist()
        {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries)
                .ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            var result = (await controller.GetCountry(1)).Value;
            Assert.NotNull(result);
            Assert.Equal("Argentina", result.Name);
            Assert.Equal(121, result.Rating);
        }
        [Fact]
        public async void TestGetCountryNonExist()
        {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries)
                .ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            var result = (await controller.GetCountry(5)).Value;
            Assert.Null(result);
        }
        [Fact]
        public async void TestAddCountry()
            {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries).ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            Country countrypost = new Country { Id = 3, Name = "qwqw", Rating = 12 };
            var response = (await controller.PostCountry(countrypost)).Result;
            Assert.IsType<CreatedAtActionResult>(response);
            var item = response as CreatedAtActionResult;
            var country = item.Value as Country;
            Assert.Equal(countrypost.Id, country.Id);
            Assert.Equal(countrypost.Name, country.Name);
            Assert.Equal(countrypost.Rating, country.Rating);
        }
        [Fact]
        public async void TestAddCountrySameName()
        {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries).ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            Country countrypost = new Country { Id = 3, Name = "Argentina", Rating = 12 };
            var response = (await controller.PostCountry(countrypost)).Result;
            Assert.IsType<ObjectResult>(response);
        }
        [Fact]
        public async void TestDeleteExistCountry()
        {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries).ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            var response = (await controller.DeleteCountry(1));
            Assert.IsType<NoContentResult>(response);
        }
        [Fact]
        public async void TestDeleteNonExistCountry()
        {
            var _context = new Mock<LBAPI2Context>();
            _context.Setup<DbSet<Country>>(x => x.Countries).ReturnsDbSet(GetCountriesList());
            CountriesController controller = new CountriesController(_context.Object);
            var response = (await controller.DeleteCountry(5));
            Assert.IsType<NotFoundResult>(response);
        }
        private List<Country> GetCountriesList()
            {
                return new List<Country>()
    {
        new Country
        {
            Id = 1,
            Name = "Argentina",
            Rating=121
        },
        new Country
        {
            Id = 2,
            Name = "Brazil",
            Rating=1345
        }
    };
            }
        }
    }