using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using MajorProject.Controllers;
using MajorProject.Models;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace ItemTestApixUnit
{
    public partial class ItemCategoriesApiTests
    {
        [Fact]
        public async void UpdateCategory_OkResult01()
        {

            var dbName = nameof(ItemCategoriesApiTests.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int editCategoryID = 4;

            IssueCategory originalCategory, changedCategory;

            changedCategory = new IssueCategory
            {
                IssueCategoryId = editCategoryID,
                Issue = "New One"
                
            };

            IActionResult actionresultget = await controller.GetIssueCategory(editCategoryID);

            OkObjectResult OkResult = actionresultget.Should().BeOfType<OkObjectResult>().Subject;

            originalCategory = OkResult.Value.Should().BeAssignableTo<IssueCategory>().Subject;

            Assert.NotNull(originalCategory);

            _testoutputHelper.WriteLine("Retrived the Data from the Api");

            try
            {
                var actionResultPutAttempt1 = await controller.PutIssueCategory(editCategoryID, changedCategory);
                Assert.IsType<OkResult>(actionResultPutAttempt1);
                _testoutputHelper.WriteLine("Updated the changes back in api");
            }
            catch(System.InvalidOperationException exp)
            {
                _testoutputHelper.WriteLine("Failed to update the change back to the API - using a new object");
                _testoutputHelper.WriteLine($"Exception Type: {exp.GetType()}");
                _testoutputHelper.WriteLine($"Exception Message: {exp.Message}");
                _testoutputHelper.WriteLine($"Exception Source: {exp.Source}");
                _testoutputHelper.WriteLine($"Exception TargetSite: {exp.TargetSite}");
            }

        }

        [Fact]
        public async void UpdateCategory_OkResult02()
        {

            var dbName = nameof(ItemCategoriesApiTests.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int editCategoryID = 4;

            IssueCategory originalCategory;
            string changedCategoryName = "New One";


            IActionResult actionresultget = await controller.GetIssueCategory(editCategoryID);

            OkObjectResult OkResult = actionresultget.Should().BeOfType<OkObjectResult>().Subject;

            originalCategory = OkResult.Value.Should().BeAssignableTo<IssueCategory>().Subject;

            Assert.NotNull(originalCategory);

            _testoutputHelper.WriteLine("Retrived the Data from the Api");
            originalCategory.Issue = changedCategoryName;
            
            
                var actionResultPutAttempt2 = await controller.PutIssueCategory(editCategoryID, originalCategory);
                Assert.IsType<NoContentResult>(actionResultPutAttempt2);
                _testoutputHelper.WriteLine("Updated the changes back in api");
            
        }


    }
}
