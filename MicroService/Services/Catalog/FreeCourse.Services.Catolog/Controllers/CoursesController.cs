using FreeCourse.Services.Catolog.Dtos;
using FreeCourse.Services.Catolog.Services;
using FreeCourse.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : CustomBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }




        [HttpGet]
        public async Task<IActionResult> GetAll(string id)
        {
            var response = await _courseService.GetAllAsync();
            return CreateActionResultInstance(response);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }




        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserId(userId);
            return CreateActionResultInstance(response);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CourseCrateDto courseCrateDto)
        {
            var response = await _courseService.CreateAsync(courseCrateDto);
            return CreateActionResultInstance(response);
        }



        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateAsync(courseUpdateDto);
            return CreateActionResultInstance(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }
    }
}
