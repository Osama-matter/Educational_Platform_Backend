using Azure.Core;
using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.External_services;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    internal class CourseService(ICourseRepository course_Repository , IImageService imageService) : ICourseService
    {
        private readonly ICourseRepository _courseRepository = course_Repository;
        private readonly IImageService _imageService = imageService;
        public async Task<CourseDto> CreateAsync(CreateCourseDto request)
        {
            if (request is not Application.DTOs.Courses.CreateCourseDto createCourseDto)
            {
                throw new ArgumentException("Invalid request type", nameof(request));
            }
            // Upload image and get URL
            string  imageUrl = await _imageService.SaveCourseImageAsync(createCourseDto.imageFile);

            var course = new Course(createCourseDto.Title, createCourseDto.Description, createCourseDto.InstructorId, createCourseDto.EstimatedDurationHours, createCourseDto.IsActive , imageUrl);
            await _courseRepository.AddAsync(course);

            return new Application.DTOs.Courses.CourseDto(course) as dynamic;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            // Delete associated image
            bool isDeleted = await _imageService.DeleteCourseImageAsync(course.Image_URl);
            if(!isDeleted)
            {
                throw new Exception("Failed to delete course image");
            }

            await _courseRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            if (courses == null || !courses.Any())
            {
                return Enumerable.Empty<CourseDto>();
            }
            var OResualt = courses.Select(c => new Application.DTOs.Courses.CourseDto(c));
            return OResualt as dynamic; 
        }

        public async Task<CourseDto> GetByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new ValidationException("Course not found");
            }
            return new Application.DTOs.Courses.CourseDto(course) as dynamic;
        }



        public async  Task <CourseDto>UpdateAsync(Guid id, UpdateCourseDto request)
        {
            if (request is not Application.DTOs.Courses.UpdateCourseDto updateCourseDto)
            {
                throw new ArgumentException("Invalid request type", nameof(request));
            }

            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new ValidationException("Course not found");
            }
            // If there's a new image, update it
            string imageUrl = await _imageService.UpdateCourseImageAsync(course.Image_URl, updateCourseDto.Image_form);

            course.Title = updateCourseDto.Title;
            course.Description = updateCourseDto.Description;
            course.EstimatedDurationHours = updateCourseDto.EstimatedDurationHours;
            course.IsActive = updateCourseDto.IsActive;
            course.Image_URl = imageUrl;
            course.UpdatedAt = DateTime.UtcNow;
            await _courseRepository.UpdateAsync(course);
            return new Application.DTOs.Courses.CourseDto(course) as dynamic;
        }


        //public async Task<TResponse> GetByIdAsync(Guid id)
        //{
        //    var ORS = new Domain.Entities.Course.Specification.ReadCourseByIdSpecification(id);
        //    var OData = await _courseRepository.GetBySpecAsync(ORS);
        //    return new Application.DTOs.Courses.CourseDto(OData) as dynamic;
        //}

        //public async  Task<TResponse> UpdateAsync(Guid id, TRequest request)
        //{
        //    var Contract   = request as Application.DTOs.Courses.UpdateCourseDto;
        //    var ORS = new Domain.Entities.Course.Specification.ReadCourseByIdSpecification(id);
        //    var OData = await _courseRepository.GetBySpecAsync(ORS);
        //    if(OData == null)
        //    {
        //        throw new ValidationException("Course not found");
        //    }

        //    OData.Title = Contract.Title;
        //    OData.Description = Contract.Description;
        //    OData.EstimatedDurationHours = Contract.EstimatedDurationHours;
        //    OData.IsActive = Contract.IsActive;
        //    OData.UpdatedAt = DateTime.UtcNow;
        //    await _courseRepository.UpdateAsync(OData);
        //    return new Application.DTOs.Courses.CourseDto(OData) as dynamic;
        //}

    }
}
