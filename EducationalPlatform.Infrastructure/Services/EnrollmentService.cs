using EducationalPlatform.Application.DTOs.Enrollments;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class EnrollmentService(IEnrollmentRepository enrollmentRepository , ICourseRepository courseRepository) : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;
        private readonly ICourseRepository _courseRepository = courseRepository;
        public async Task<EnrollmentDto> CreateAsync(Guid studentId, Guid courseId)
        {

            if(await _courseRepository.GetByIdAsync(courseId) == null)
                throw new NotFoundException($"Course with ID {courseId} not found.");



            var exist = await _enrollmentRepository.GetByStudentAndCourseAsync(studentId, courseId);
            if(exist != null)
                throw new InvalidOperationException("The student is already enrolled in this course.");

            var enrollment = new Enrollment(studentId, courseId);


            await _enrollmentRepository.AddAsync(enrollment);

            return new EnrollmentDto(enrollment);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
                throw new NotFoundException($"Enrollment with ID {id} not found.");

            await _enrollmentRepository.DeleteAsync(enrollment);
            return true;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllAsync()
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();
            if (enrollments == null || !enrollments.Any())
                throw new NotFoundException("No enrollments found.");

            return enrollments.Select(e => new EnrollmentDto(e));
        }

        public async Task<EnrollmentDto> GetByIdAsync(Guid id)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
                throw new NotFoundException($"Enrollment with ID {id} not found.");

            return new EnrollmentDto(enrollment);
        }

        public async Task<EnrollmentDto> UpdateAsync(Guid id, UpdateEnrollmentDto request)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(id);
            if (enrollment == null)
                throw new NotFoundException($"Enrollment with ID {id} not found.");

            request.ApplyTo(enrollment);
            await _enrollmentRepository.UpdateAsync(enrollment);

            return new EnrollmentDto(enrollment);
        }
    }
}
