using Application.Features.Lessons.Dtos;
using Application.Features.StudentLessons.Dtos;

namespace Application.Contracts.Repositories
{
    public interface ILessonRepository
    {
        Task<List<StudentViewsDto>> GetAllStudentsViewsAsync(int courseId, int itemId, CancellationToken cancellationToken);
        Task<List<ViewsOverTime>> GetViewsOverTimeAsync(int itemId, CancellationToken cancellationToken);
        Task<LessonOverviewDto?> GetLessonOverviewAsync(int itemId, CancellationToken cancellationToken);
        Task<DateTime> GetPeakActivityTimeAsync(int itemId, CancellationToken cancellationToken);
<<<<<<< HEAD
        Task<bool> IsFound(int id, int moduleId, int courseId, string subdomain, CancellationToken cancellationToken);
=======
        Task<bool> IsFound(int id, CancellationToken cancellationToken);
        Task<StudentLessonProgressDto> GetStudentLessonProgressAsync(int studentId, int courseId, int itemId, CancellationToken cancellationToken);
>>>>>>> 4c7a93aa4a11710a64ff2df81ec9e472ae2910a1
    }
}