using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Api.Controllers
{

    [Route("api/students")]
    public class ExameController : Controller
    {
        [HttpGet("{studentId}/exames")]
        public IActionResult GetAll(int studentId)
        {
            //var studentRepo = new object()
            //var student = studentRepo.GetStudentById(studentId).GetExames();
            //if (student)
            //{
            //    return Ok(student);
            //}
            return NotFound();
        }

        [HttpGet("{studentId}/exames/{exameId}")]
        public IActionResult GetExame(int studentId, int exameId)
        {
            //var studentRepo = new object()
            //var student = studentRepo.GetStudentById(studentId).GetExames();
            //if (student)
            //{
            //    return Ok(student);
            //}
            return NotFound();
        }
    }
}
