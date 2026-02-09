using EducationalPlatform.Application.DTOs.Certificate;
using EducationalPlatform.Application.Interfaces.External_services;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Services.External_services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
 
        private readonly IMatterHubCertificateGenerator _certificateGenerator;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailService _emailService;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public CertificateService(
            ICertificateRepository certificateRepository,
            ICourseRepository courseRepository,
            IUserRepository userRepository,
            IMatterHubCertificateGenerator certificateGenerator,
            IWebHostEnvironment webHostEnvironment,
            IEnrollmentRepository enrollmentRepository,
            IEmailService emailService)
        {
            _certificateRepository = certificateRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _certificateGenerator = certificateGenerator;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
            _enrollmentRepository =enrollmentRepository;
        }

        /// <summary>
        /// Checks if a certificate already exists for a given user and course.
        /// </summary>
        public async Task<bool> CertificateExistsAsync(Guid userId, Guid courseId)
        {
            return await _certificateRepository.ExistsAsync(userId, courseId);
        }

        /// <summary>
        /// Retrieves the detailed information of a certificate by its ID.
        /// </summary>
        public async Task<CertificateDetailsDto> GetCertificateDetailsAsync(Guid certificateId)
        {
            var certificate = await _certificateRepository.GetByIdAsync(certificateId);
            if (certificate == null)
                throw new Exception("Certificate not found");

            return new CertificateDetailsDto
            {
                CertificateNumber = certificate.CertificateNumber,
                CourseTitle = certificate.Course.Title,
                IsRevoked = certificate.IsRevoked,
                IssuedAt = certificate.IssuedAt,
                UserFullName = $"{certificate.User.FirstName} {certificate.User.LastName}"
            };
        }

        /// <summary>
        /// Retrieves all certificates for a specific user.
        /// </summary>
        public async Task<List<CertificateDto>> GetUserCertificatesAsync(Guid userId)
        {
            var certificates = await _certificateRepository.GetByUserIdAsync(userId);
            if (certificates == null || !certificates.Any())
                throw new Exception("Certificates not found");

            return certificates.Select(c => new CertificateDto
            {
                Id = c.Id,
                CertificateNumber = c.CertificateNumber,
                CourseTitle = c.Course.Title,
                IssuedAt = c.IssuedAt,
                IsRevoked = c.IsRevoked
            }).ToList();
        }

        /// <summary>
        /// Issues a new certificate for a user who completed a course.
        /// </summary>
        public async Task<CertificateDto> IssueCertificateAsync(CreateCertificateDto createDto)
        {
            // Check if course exists
            var course = await _courseRepository.GetByIdAsync(createDto.CourseId);
            if (course == null)
                throw new Exception("Course not found");



            // Check if certificate already exists
            var exists = await _certificateRepository.ExistsAsync(createDto.UserId, createDto.CourseId);
            if (exists)
                throw new Exception("Certificate already issued for this user and course");

            //check if  user  already in this course

            var  enrollment = await _enrollmentRepository.GetByStudentAndCourseAsync(createDto.UserId, createDto.CourseId);
            if(enrollment == null)
                throw new Exception("User is not enrolled in this course");

            // Generate certificate number and verification code
            var certificateNumber = GenerateCertificationData.GenerateCertificateNumber();
            var verificationCode = GenerateCertificationData.GenerateVerificationCode();

            var user = await _userRepository.GetByIdAsync(createDto.UserId);
            if (user == null) throw new Exception("User not found");

            var certificate = new Certificate(
                createDto.UserId,
                createDto.CourseId,
                certificateNumber,
                verificationCode
               
            );

                await _certificateRepository.AddAsync(certificate);

            var (certificateBytes, _) = await DownloadCertificateAsync(certificate.Id);
            await _emailService.SendCertificateEmailAsync(user.Email, user.UserName, course.Title, certificateBytes); 


            return new CertificateDto
            {
                Id = certificate.Id,
                CertificateNumber = certificate.CertificateNumber,
                CourseTitle = course.Title,
                IssuedAt = certificate.IssuedAt,
                IsRevoked = certificate.IsRevoked
            };
        }

        /// <summary>
        /// Revokes an existing certificate.
        /// </summary>
        public async Task RevokeCertificateAsync(Guid certificateId, string reason = null)
        {
            var certificate = await _certificateRepository.GetByIdAsync(certificateId);
            if (certificate == null)
                throw new Exception("Certificate not found");

            await _certificateRepository.RevokeAsync(certificate);
        }

        /// <summary>
        /// Verifies a certificate using its verification code.
        /// </summary>
        public async Task<(byte[] fileContents, string fileName)> DownloadCertificateAsync(Guid certificateId)
        {
            var certificate = await _certificateRepository.GetByIdAsync(certificateId);
            if (certificate == null) throw new Exception("Certificate not found");

            var user = await _userRepository.GetByIdAsync(certificate.UserId);
            if (user == null) throw new Exception("User not found");

            var course = await _courseRepository.GetByIdAsync(certificate.CourseId);
            if (course == null) throw new Exception("Course not found");

            var logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "certification", "Logo", "lOGO.png");

            var certificateDetails = new CertificateDetailsDto
            {
                UserFullName = $"{user.FirstName} {user.LastName}",
                CourseTitle = course.Title,
                CertificateNumber = certificate.CertificateNumber,
                VerificationCode = certificate.VerificationCode,
                IssuedAt = certificate.IssuedAt,
                InstructorName = course.Instructor != null ? $"{course.Instructor.FirstName} {course.Instructor.LastName}" : null,
                VerificationUrl = $"https://matterhub.com/verify/{certificate.VerificationCode}",
                LogoPath = logoPath
            };

            var fileContents = await _certificateGenerator.GenerateCertificatePdfBytesAsync(certificateDetails);
            var fileName = $"Certificate-{user.FirstName}_{user.LastName}-{course.Title.Replace(" ", "_")}.pdf";

            return (fileContents, fileName);
        }

        public async Task<VerifyCertificateDto> VerifyCertificateAsync(string verificationCode)
        {
            var certificate = await _certificateRepository.GetByVerificationCodeAsync(verificationCode);
            if (certificate == null)
                throw new Exception("Certificate not found");

            return new VerifyCertificateDto
            {
                CertificateNumber = certificate.CertificateNumber,
                StudentName = $"{certificate.User.FirstName} {certificate.User.LastName}",
                CourseTitle = certificate.Course.Title,
                IssuedAt = certificate.IssuedAt,
                IsValid = !certificate.IsRevoked
            };
        }
    }
}
