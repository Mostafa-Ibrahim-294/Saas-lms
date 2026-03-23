using Application.Features.Lessons.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    internal sealed class LessonRepository : ILessonRepository
    {
        private readonly AppDbContext _dbContext;
        public LessonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<StudentViewsDto>> GetAllStudentsViewsAsync(int courseId, int itemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Students.Where(s => s.Enrollments.Any(c => c.CourseId == courseId))
                .LeftJoin(_dbContext.LessonViews.Where(sv => sv.ModuleItemId == itemId),
                    student => student.Id,
                    studentView => studentView.StudentId,
                    (student, studentView) => new StudentViewsDto
                    {
                        Id = studentView != null ? studentView.Id : 0,
                        StudentId = student.Id,
                        StudentName = student.User.FirstName + " " + student.User.LastName,
                        ProfilePicture = student.User.ProfilePicture,
                        Status = studentView != null ? studentView.Status : ViewStatus.NotStarted,
                        LatestProgress = studentView != null ? studentView.LastPositionSeconds : null,
                        TotalWatchTime = studentView != null ? studentView.WatchedSeconds : 0,
                        LastViewTime = studentView != null ? studentView.LastWatchedAt : null,
                        Device = studentView != null ? studentView.Device : null
                    }).ToListAsync(cancellationToken);
        }

        public async Task<LessonOverviewDto?> GetLessonOverviewAsync(int courseId, int itemId, CancellationToken cancellationToken)
        {
            return await _dbContext.LessonViews.Where(lv => lv.ModuleItemId == itemId)
                .GroupBy(lv => lv.ModuleItemId)
                .Select(g => new LessonOverviewDto
                {
                    TotalViews = g.Sum(s => s.ViewCount),
                    CompletionRate = g.Count(c => c.Status == ViewStatus.Completed) * 1.0 / g.Count(),
                    AverageWatchTime = g.Average(s => s.WatchedSeconds),
                    TotalStudents = g.Select(s => s.StudentId).Count()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<DateTime> GetPeakActivityTimeAsync(int itemId, CancellationToken cancellationToken)
        {
           return await _dbContext.LessonViews.Where(lv => lv.ModuleItemId == itemId)
                .GroupBy(lv => lv.CreatedAt)
                .Select(g => new
                {
                    TimeSlot = g.Key,
                    ViewCount = g.Sum(s => s.ViewCount)
                })
                .OrderByDescending(g => g.ViewCount)
                .Select(g => (DateTime)g.TimeSlot)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ViewsOverTime>> GetViewsOverTimeAsync(int itemId, CancellationToken cancellationToken)
        {
            return await _dbContext.LessonViews.Where(lv => lv.ModuleItemId == itemId && lv.CreatedAt >= DateTime.UtcNow.AddDays(-7))
                .GroupBy(lv => lv.CreatedAt.Date)
                .Select(g => new ViewsOverTime
                {
                    Date = g.Key,
                    TotalViews = g.Sum(s => s.ViewCount)
                }).ToListAsync(cancellationToken);
        }

        public async Task<bool> IsFound(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Lessons.AnyAsync(l => l.ModuleItemId == id, cancellationToken);
        }
    }
}
