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
        public void GetCategoryByID_NotFoundResult()
        {

            var dbName = nameof(ItemCategoriesApiTests.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int findCategoryID = 900;

            IActionResult actionresult = controller.GetIssueCategory(findCategoryID).Result;

            Assert.IsType<NotFoundResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound; //404
            var actualStatusCode = (actionresult as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
        
        
        [Fact]
        public void GetCategoryByID_BadFoundResult()
        {
            var dbName = nameof(ItemCategoriesApiTests.GetCategoryByID_BadFoundResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int? findCategoryID = null;

            IActionResult actionresult = controller.GetIssueCategory(findCategoryID).Result;

            Assert.IsType<BadRequestResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest; //404
            var actualStatusCode = (actionresult as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategoryById_OkResult()
        {

            var dbName = nameof(ItemCategoriesApiTests.GetCategoryByID_BadFoundResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int findCategoryID = 4;

            IActionResult actionresult = controller.GetIssueCategory(findCategoryID).Result;

            Assert.IsType<OkObjectResult>(actionresult);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK; //200
            var actualStatusCode = (actionresult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategoryById_CorrectResult()
        {

            var dbName = nameof(ItemCategoriesApiTests.GetCategoryByID_BadFoundResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int findCategoryID = 3;

            IssueCategory expectedCategory = DbMocker.TestCollectionOfIsssues
                                            .SingleOrDefault(c => c.IssueCategoryId == findCategoryID);



            IActionResult actionresult = controller.GetIssueCategory(findCategoryID).Result;

            OkObjectResult result = actionresult.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsType<IssueCategory>(result.Value);

            IssueCategory pc = result.Value.Should().BeAssignableTo<IssueCategory>().Subject;//actual category
            _testoutputHelper.WriteLine($"Found:  Id : {pc.IssueCategoryId},  Name : {pc.Issue}");

            Assert.NotNull(pc);



            Assert.Equal<int>(expected: expectedCategory.IssueCategoryId, actual: pc.IssueCategoryId);


            Assert.Equal(expected: expectedCategory.Issue, actual: pc.Issue);




        }

    }
}
