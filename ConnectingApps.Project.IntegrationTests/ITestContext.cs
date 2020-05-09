using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ConnectingApps.Project.IntegrationTests
{
    public interface ITestContext
    {
        void AddEntityChange(object newEntity, EntityState entityState);
    }
}
