using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using ConnectingApps.Project.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ConnectingApps.Project.IntegrationTests
{
    public abstract class TestBase<TTestType> : IDisposable, ITestContext, IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient HttpClient;

        protected TestBase(WebApplicationFactory<Startup> webApplicationFactory, int portNumber, string newConnectionString)
        {
            HttpClient = webApplicationFactory.WithWebHostBuilder(whb =>
            {
                whb.ConfigureAppConfiguration((context, configbuilder) =>
                {
                    configbuilder.AddInMemoryCollection(new Dictionary<string,string>()
                    {
                        {"ConnectionString",newConnectionString}
                    });
                });
                whb.ConfigureTestServices(sc =>
                {
                    sc.AddSingleton<ITestContext>(this);
                    ReplaceDbContext(sc, newConnectionString);
                    var scope = sc.BuildServiceProvider().CreateScope();
                    var testInstance = scope.ServiceProvider.GetService<TTestType>();
                    SetTestInstance(testInstance);
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost:{portNumber}")
            });
        }

        public abstract void AddEntityChange(object newEntity, EntityState entityState);

        private void ReplaceDbContext(IServiceCollection serviceCollection, string newConnectionString)
        {
            var serviceDescriptor = serviceCollection.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ProjectContext));
            serviceCollection.Remove(serviceDescriptor);
            serviceCollection.AddDbContext<ProjectContext, TestProjectContext>();
        }

        protected abstract void SetTestInstance(TTestType testInstance);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HttpClient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
