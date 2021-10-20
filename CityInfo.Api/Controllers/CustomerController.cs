using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Models;
using CityInfo.Api.Repositories.Interfaces;
using CityInfo.Api.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    [EnableCors(Utility.CORE_POLICY)]
    public class CustomerController : Controller
    {
        ICustomerRepository _Repository;
        public CustomerController(ICustomerRepository repository)
        {
            _Repository = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_Repository.GetAll());
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                Customer CustomerToReturn = _Repository.GetById(id);

                if (CustomerToReturn != null)
                {
                    return Ok(CustomerToReturn);
                }

                return NotFound();

            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [HttpPost()]
        public IActionResult Insert([FromBody] Customer customer)
        {
            try
            {
                var newCustomer = _Repository.insert(customer);
                return Ok(newCustomer);
            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] Customer customer)
        {
            try
            {
                var updatedStudent = _Repository.update(customer);
                return Ok(updatedStudent);
            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [EnableCors(Utility.CORE_POLICY)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_Repository.Delete(id))
                {
                    return Ok("Record archived successfull.");
                }
                return NotFound();

            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

    }
}