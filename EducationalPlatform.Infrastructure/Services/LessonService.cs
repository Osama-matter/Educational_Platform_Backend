using EducationalPlatform.Application.DTOs.Lessons;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Repositories;
using huzcodes.Extensions.Exceptions;
using NuGet.Packaging.Signing;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class LessonService(ILessonRepository lessonRepository) : ILessonService
    {
        private readonly ILessonRepository _lessonRepository = lessonRepository;

        //summary
        // Creates a new lesson.
        //1- Converts the CreateLessonDto to a Lesson entity.
        //2- Calls the repository to add the new lesson to the data store.
        //3- Converts the created Lesson entity back to a LessonDto and returns it.
        //summary>
        public async Task<LessonDto> CreateAsync(CreateLessonDto request)
        {
            var OData = request.ToEntity();
            await _lessonRepository.AddAsync(OData);
            var OResultDto = new LessonDto
            {
                Id = OData.Id,
                CourseId = OData.CourseId,
                Title = OData.Title,
                Content = OData.Content,
                OrderIndex = OData.OrderIndex,
                DurationMinutes = OData.DurationMinutes,
                CreatedAt = OData.CreatedAt,
                UpdatedAt = OData.UpdatedAt
            };
            return OResultDto;

        }
        // summary
        // Deletes a lesson by its ID.
        //1- Calls the repository to delete the Lesson entity with the specified ID from the data store.
        //2- Returns true if the deletion was successful, otherwise false.
        // summary>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var OData = await _lessonRepository.GetByIdAsync(id);
            if (OData == null)
            {
                throw new NotFoundException($"Lesson with ID {id} not found.");

            }
            await _lessonRepository.DeleteAsync(OData);
            return true;

        }
        //summary
        // Retrieves all lessons.
        //1- Calls the repository to get all Lesson entities from the data store.
        //2- Converts the list of Lesson entities to a list of LessonDto and returns it.
        //summary>
        public async Task<IEnumerable<LessonDto>> GetAllAsync()
        {
            var OData =await _lessonRepository.GetAllAsync();
            if(OData == null)
            {
                throw new NotFoundException("No lessons found.");
            }
            var OResult = OData.Select(lesson => new LessonDto
            {
                Id = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Content = lesson.Content,
                OrderIndex = lesson.OrderIndex,
                DurationMinutes = lesson.DurationMinutes,
                CreatedAt = lesson.CreatedAt,
                UpdatedAt = lesson.UpdatedAt
            });

            return OResult;
        }
        //summary
        // Retrieves a lesson by its ID.
        //1- Calls the repository to get the Lesson entity with the specified ID from the data store.
        //2- If the lesson is not found, throws a NotFoundException.
        //3- Converts the Lesson entity to a LessonDto and returns it.
        //summary>
        public async Task<LessonDto> GetByIdAsync(Guid id)
        {
            var OData =await _lessonRepository.GetByIdAsync(id);
            if(OData == null)
            {
                throw new NotFoundException($"Lesson with ID {id} not found.");
            }
            var OResultDto = new LessonDto
            {
                Id = OData.Id,
                CourseId = OData.CourseId,
                Title = OData.Title,
                Content = OData.Content,
                OrderIndex = OData.OrderIndex,
                DurationMinutes = OData.DurationMinutes,
                CreatedAt = OData.CreatedAt,
                UpdatedAt = OData.UpdatedAt
            };
            return OResultDto;
        }
        //summary
        // Updates an existing lesson.
        //1- Calls the repository to get the existing Lesson entity with the specified ID from the data store.
        //2- If the lesson is not found, throws a NotFoundException.
        //3- Updates the Lesson entity with the data from the UpdateLessonDto.
        //4- Calls the repository to save the updated Lesson entity to the data store.
        //5- Converts the updated Lesson entity to a LessonDto and returns it.
        //summary>
        public  async Task<LessonDto> UpdateAsync(Guid id, UpdateLessonDto request)
        {
            var OData = _lessonRepository.GetByIdAsync(id).Result;
            if (OData == null)
            {
                throw new NotFoundException($"Lesson with ID {id} not found.");
            }
            OData.Title = request.Title;
            OData.Content = request.Content;
            OData.OrderIndex = request.OrderIndex;
            OData.DurationMinutes = request.DurationMinutes;
            OData.UpdatedAt = DateTime.UtcNow;
            await _lessonRepository.UpdateAsync(OData);

            var OResultDto = new LessonDto
            {
                Id = OData.Id,
                CourseId = OData.CourseId,
                Title = OData.Title,
                Content = OData.Content,
                OrderIndex = OData.OrderIndex,
                DurationMinutes = OData.DurationMinutes,
                CreatedAt = OData.CreatedAt,
                UpdatedAt = OData.UpdatedAt
            };
            return OResultDto;
        }
    }
}
