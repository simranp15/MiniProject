using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment_1.Models;

namespace Assignment_1.Controllers
{
    [RoutePrefix("api/country")]

    public class CountryController : ApiController
    {
        static List<Country> countryList = new List<Country>()
        {
            new Country { Id = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { Id = 2, CountryName = "Germany", Capital = "Berlin" },
            new Country { Id = 3, CountryName = "Argentina", Capital = "Amsterdam" },
            new Country { Id = 4, CountryName = "France", Capital = "Paris" }
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<Country> GetAllCountries()
        {
            return countryList;
        }

        [HttpGet]
        [Route("ById/{id}")]
        public IHttpActionResult GetCountryById(int id)
        {
            var country = countryList.Find(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpGet]
        [Route("GetName")]
        public IHttpActionResult GetCountryByName(int id)
        {
            var country = countryList.Where(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddCountry(Country country)
        {
            countryList.Add(country);
            return Ok(countryList);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IHttpActionResult UpdateCountry(int id, Country updatedCountry)
        {
            var country = countryList.FirstOrDefault(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            country.CountryName = updatedCountry.CountryName;
            country.Capital = updatedCountry.Capital;

            return Ok(country); 
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteCountry(int id)
        {
            var country = countryList.FirstOrDefault(c => c.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            countryList.RemoveAt(id-1);
            return Ok(countryList);
        }
    }
}

    
