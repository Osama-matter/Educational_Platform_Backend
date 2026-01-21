using Azure.Core;
using EducationalPlatform.Application.Interfaces;
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
    internal class CourseService<TRequest, TResponse>(ICourseRepository course_Repository) : ICourseService<TRequest, TResponse>
    {
        private readonly ICourseRepository _courseRepository = course_Repository;
        public async Task<TResponse> CreateAsync(TRequest request)
        {
            if (request is not Application.DTOs.Courses.CreateCourseDto createCourseDto)
            {
                throw new ArgumentException("Invalid request type", nameof(request));
            }

            var course = new Course(createCourseDto.Title, createCourseDto.Description, createCourseDto.InstructorId, createCourseDto.EstimatedDurationHours, createCourseDto.IsActive);
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
            await _courseRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TResponse>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            if (courses == null || !courses.Any())
            {
                return Enumerable.Empty<TResponse>();
            }
            var OResualt = courses.Select(c => new Application.DTOs.Courses.CourseDto(c));
            return OResualt as dynamic; 
        }

        public async Task<TResponse> GetByIdAsync(Guid id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                throw new ValidationException("Course not found");
            }
            return new Application.DTOs.Courses.CourseDto(course) as dynamic;
        }

        public async Task<bool> UpdateAsync(Guid id, TRequest request)
        {
            if (request is not Application.DTOs.Courses.UpdateCourseDto updateCourseDto)
            {
                throw new ArgumentException("Invalid request type", nameof(request));
            }

            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return false;
            }

            course.Title = updateCourseDto.Title;
            course.Description = updateCourseDto.Description;
            course.EstimatedDurationHours = updateCourseDto.EstimatedDurationHours;
            course.IsActive = updateCourseDto.IsActive;

            await _courseRepository.UpdateAsync(course);
            return true;
        }

        Task<TResponse> IGenericServices<TRequest, TResponse>.UpdateAsync(Guid id, TRequest request)
        {
            throw new NotImplementedException();
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
