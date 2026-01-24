var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
builder.Services.AddScoped<Educational_Platform_Front_End.Services.Admin.IAdminAuthService, Educational_Platform_Front_End.Services.Admin.AdminAuthService>();
builder.Services.AddScoped<Educational_Platform_Front_End.Services.Admin.ILessonAdminService, Educational_Platform_Front_End.Services.Admin.LessonAdminService>();
builder.Services.AddScoped<Educational_Platform_Front_End.Services.Admin.ICourseAdminService, Educational_Platform_Front_End.Services.Admin.CourseAdminService>();

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
