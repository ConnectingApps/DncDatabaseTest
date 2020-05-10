using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConnectingApps.Project.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            Action entityChanges = () => { };
            var ca = this.Database.GetDbConnection().ConnectionString;
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                var state = entry.State;
                entityChanges += () => _testContext.AddEntityChange(entry.Entity, state);
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            entityChanges();
            return result;
        }

        private static Dictionary<string, string> ToDictionary(EntityEntry entityEntry)
        {
            return typeof(EntityEntry).GetProperties()
                .Select(a => (a.Name, a.GetValue(entityEntry)))
                .ToDictionary(a => a.Name, a => a.Item2.ToString());
        }

        private static ExpandoObject ToExpandoObject(EntityEntry entityEntry)
        {
            var dict = ToDictionary(entityEntry);
            var serializedData = JsonConvert.SerializeObject(dict);
            var expConverter = new ExpandoObjectConverter();
            var result = JsonConvert.DeserializeObject<ExpandoObject>(serializedData, expConverter);
            return result;
        }
    }
}
