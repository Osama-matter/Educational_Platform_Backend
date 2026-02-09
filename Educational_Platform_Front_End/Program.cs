using Educational_Platform_Front_End.Services.Courses;
using Educational_Platform_Front_End.Services.Questions;
using Educational_Platform_Front_End.Services.QuizAttempts;
using Educational_Platform_Front_End.Services.Quizzes;
using Educational_Platform_Front_End.Services.Reviews;
using Educational_Platform_Front_End.Services.Enrollments;
using Educational_Platform_Front_End.Services.Forum;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddScoped<Educational_Platform_Front_End.Services.Admin.IAdminAuthService, Educational_Platform_Front_End.Services.Admin.AdminAuthService>();

builder.Services.AddHttpClient<Educational_Platform_Front_End.Services.Admin.ICourseAdminService, Educational_Platform_Front_End.Services.Admin.CourseAdminService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<Educational_Platform_Front_End.Services.Admin.ILessonAdminService, Educational_Platform_Front_End.Services.Admin.LessonAdminService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();

// Configure HttpClient for API services
builder.Services.AddHttpClient<IQuizService, QuizService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddHttpClient<IQuizAttemptService, QuizAttemptService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddHttpClient<ICourseService, CourseService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddHttpClient<IQuestionService, QuestionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();
builder.Services.AddHttpClient<IQuestionOptionService, QuestionOptionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<IReviewService, ReviewService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<IEnrollmentService, EnrollmentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<IForumService, ForumService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7228");
}).AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
