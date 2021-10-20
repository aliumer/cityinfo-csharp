using CityInfo.Api.Models;
using CityInfo.Api.Repositories.Interfaces;
using CityInfo.Api.Utilities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/students")]
    [EnableCors(Utility.CORE_POLICY)]
    public class StudentController : Controller
    {
        IStudentRepository _StudentRepository;
        public StudentController(IStudentRepository repository)
        {
            _StudentRepository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_StudentRepository.GetAll());
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            try
            {
                Student StudentToReturn = _StudentRepository.GetById(id);

                if (StudentToReturn != null)
                {
                    return Ok(StudentToReturn);
                }

                return NotFound();

            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [HttpPost()]
        public IActionResult Insert([FromBody] Student student)
        {
            try
            {
                var newStudent = _StudentRepository.insert(student);
                return Ok(newStudent);
            }
            catch (System.Exception ex)
            {
                return NoContent();
            }
        }

        [HttpPut()]
        public IActionResult Update([FromBody] Student student)
        {
            try
            {
                var updatedStudent = _StudentRepository.update(student);
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
                if (_StudentRepository.Delete(id))
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
