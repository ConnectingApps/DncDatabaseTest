﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConnectingApps.Project.Database;
using ConnectingApps.Project.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ConnectingApps.Project.IntegrationTests
{
    public class TodoRepositoryTest : TestBase<ITodoRepository>
    {
        private ITodoRepository _todoRepository;

        private List<(object Entity, EntityState EntityState)> _entityChanges = new List<(object Entity, EntityState entityState)>();

        public TodoRepositoryTest(WebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory, 
            5347, @"Data Source=../../../../project3.db")
        {
        }

        [Fact]
        public async Task SaveItemTest()
        {
            // run this commandline statement from sln folder first
            // dotnet ef database update --project ConnectingApps.Project
            var todoItem = new TodoItem()
            {
                Todo = "TestItem"
            };
            var savedEntity = await _todoRepository.SaveItem(todoItem);
            Assert.NotNull(savedEntity);
            Assert.NotEqual(0, savedEntity.Id);
            Assert.Equal(todoItem.Todo, savedEntity.Todo);
            var onlyAddedItem = _entityChanges.Single();
            Assert.Equal(EntityState.Added,onlyAddedItem.EntityState);
            var addedEntity = (Database.Entities.TodoItem)onlyAddedItem.Entity;
            Assert.Equal(addedEntity.Id, savedEntity.Id);
        }

        public override void AddEntityChange(object newEntity, EntityState entityState)
        {
            _entityChanges.Add((newEntity, entityState));
        }

        protected override void SetTestInstance(ITodoRepository testInstance)
        {
            _todoRepository = testInstance;
        }
    }
}
