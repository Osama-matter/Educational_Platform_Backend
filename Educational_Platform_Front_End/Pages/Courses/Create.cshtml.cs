using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Courses
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateCourseViewModel Course { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = Request.Cookies["jwt_token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(Course.Title), "Title");
                    content.Add(new StringContent(Course.Description), "Description");
                    if (Course.EstimatedDurationHours.HasValue)
                    {
                        content.Add(new StringContent(Course.EstimatedDurationHours.Value.ToString()), "EstimatedDurationHours");
                    }
                    content.Add(new StringContent(Course.IsActive.ToString()), "IsActive");
                    content.Add(new StringContent(Course.Price.ToString()), "Price");
                    content.Add(new StringContent(Course.NumberOfSections.ToString()), "NumberOfSections");

                    if (Course.ImageFile != null)
                    {
                        var streamContent = new StreamContent(Course.ImageFile.OpenReadStream());
                        streamContent.Headers.ContentType = new MediaTypeHeaderValue(Course.ImageFile.ContentType);
                        content.Add(streamContent, "imageFile", Course.ImageFile.FileName);
                    }

                    var response = await client.PostAsync("https://matterhub.runasp.net/api/courses", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToPage("/Courses/Index");
                    }
                    else
                    {
                        // Handle error
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the course.");
                        return Page();
                    }
                }
            }
        }
    }
}
