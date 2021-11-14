
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DemoContext>();

builder.Services
    .AddGraphQLServer()
    .AddConvention<IFilterConvention>(new FilterConvention(x =>
        x.AddDefaults()))
    .AddConvention<IFilterConvention>(new FilterConventionExtension(descriptor =>
    {
        descriptor.ArgumentName("agarrame_las_bolas");
    }))
    .AddQueryType<Query>()
    .ConfigureResolverCompiler(c => c.AddService<DemoContext>())
    .AddFiltering()
    .AddSorting()
    .AddProjections();



var app = builder.Build();

app.MapGraphQL();


await using (var serviceScope = app.Services.CreateAsyncScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<DemoContext>();
    if (await context.Database.EnsureCreatedAsync())
    {
        context.Courses.Add(new Course
        {
            Title = "Computer Science",
            Enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    Student = new Student
                    {
                        LastName = "Doe",
                        FirstName = "John",
                        EnrollmentDate = DateTime.Now
                    }

                },
               new Enrollment
                {
                    Student = new Student
                    {
                        LastName = "Willy",
                        FirstName = "CojeRubias",
                        EnrollmentDate = DateTime.Now
                    }

                }
            }
        });

        await context.SaveChangesAsync();
    }
}

await app.RunAsync();
