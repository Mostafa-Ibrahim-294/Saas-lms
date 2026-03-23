using Application.Features.Assignments.Dtos;
using Application.Features.Lessons.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    internal sealed class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _dbContext;
        public AssignmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<StudentSubmissionDto>> GetSubmissionsAsync(int courseId, int itemId, CancellationToken cancellationToken)
        {
            return await _dbContext.Students.Where(s => s.Enrollments.Any(c => c.CourseId == courseId))
                 .LeftJoin(_dbContext.AssignmentSubmissions.Where(sv => sv.AssignmentId == itemId),
                     student => student.Id,
                     studentSubmission => studentSubmission.StudentId,
                     (student, studentSubmission) => new StudentSubmissionDto
                     {
                         Id = studentSubmission != null ? studentSubmission.Id : 0,
                         StudentName = student.User.FirstName + " " + student.User.LastName,
                         ProfilePicture = student.User.ProfilePicture,
                         Status = studentSubmission != null ? studentSubmission.Status : AssignmentStatus.NotSubmitted,
                         SubmittedAt = studentSubmission != null ? studentSubmission.SubmittedAt : null,
                         EarnedMarks = studentSubmission != null ? studentSubmission.EarnedMarks : null,
                         Feedback = studentSubmission != null ? studentSubmission.Feedback : null,
                         Link = studentSubmission != null ? studentSubmission.Link : null,
                         Text = studentSubmission != null ? studentSubmission.Text : null,
                         TotalMarks = studentSubmission != null ? studentSubmission.Assignment.Marks : 0,
                         Files = studentSubmission != null ? studentSubmission.Files!.Select(f => new FileDto
                         {
                             FileName = f.Name,
                             Url = f.Url
                         }).ToList() : null
                     }).ToListAsync(cancellationToken);
        }
    }
}
