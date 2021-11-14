namespace Demo.Types;
public class Query
{
    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Student> GetStudentById(int id, DemoContext context)
        => context.Students.Where(t => t.Id == id);

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Student> GetStudents(DemoContext context)
        => context.Students;

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Course> GetCourseById(int id, DemoContext context)
        => context.Courses.Where(t => t.CourseId == id);

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Course> GetCourses(DemoContext context)
        => context.Courses;
}