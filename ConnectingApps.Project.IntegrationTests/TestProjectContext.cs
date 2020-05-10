using System;
using System.Threading;
using System.Threading.Tasks;
using ConnectingApps.Project.Database;
using Microsoft.EntityFrameworkCore;

namespace ConnectingApps.Project.IntegrationTests
{
    public class TestProjectContext : ProjectContext
    {
        private readonly ITestContext _testContext;

        public TestProjectContext(DbContextOptions<ProjectContext> options, ITestContext testContext) : base(options)
        {
            _testContext = testContext;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            Action updateEntityChanges = () => { };
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var state = entry.State;
                updateEntityChanges += () => _testContext.AddEntityChange(entry.Entity, state);
            }

            var result = await base.SaveChangesAsync(cancellationToken);
            updateEntityChanges();
            return result;
        }
    }
}