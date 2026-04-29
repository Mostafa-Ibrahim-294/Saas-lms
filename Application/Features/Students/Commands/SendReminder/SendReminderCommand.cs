using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Students.Commands.SendReminder
{
    public sealed record SendReminderCommand(IEnumerable<int> StudentIds, string Message) : IRequest;
}
