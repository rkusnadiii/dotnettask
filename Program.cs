using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using examplemvc.Models;
using examplemvc;
using System;
using System.Collections.Generic;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices((context, services) =>
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        })
        .Configure((context, app) =>
        {
            app.UseSession(); // Add this line to enable session

            if (!context.HostingEnvironment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Seed data before running the application
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    SeedData(dbContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
                }
            }
        });
    });

var app = builder.Build();

app.Run();

// Seeding data method
void SeedData(ApplicationDbContext dbContext)
{
    var post1 = new Post { Title = "Post 1", Body = "Body 1", CreatedAt = DateTime.Now };
    var post2 = new Post { Title = "Post 2", Body = "Body 2", CreatedAt = DateTime.Now };

    var tag1 = new Tag { Name = "Tag1" };
    var tag2 = new Tag { Name = "Tag2" };

    dbContext.Posts.AddRange(post1, post2);
    dbContext.Tags.AddRange(tag1, tag2);

    dbContext.SaveChanges();
}
