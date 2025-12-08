using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Seeders.Plan
{
    public interface IPlanSeeder
    {
        Task SeedAsync();
    }
}
