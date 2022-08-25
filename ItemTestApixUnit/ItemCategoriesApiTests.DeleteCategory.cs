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
        public void DeleteCategory_NotFoundResult()
        {

            var dbName = nameof(ItemCategoriesApiTests.DeleteCategory_NotFoundResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int findCategoryID = 900;

            IActionResult actionresultDelete = controller.DeleteIssueCategory(findCategoryID).Result;

            Assert.IsType<NotFoundResult>(actionresultDelete);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound; //404
            var actualStatusCode = (actionresultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCategory_BadRequestResult()
        {


            var dbName = nameof(ItemCategoriesApiTests.DeleteCategory_BadRequestResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int? findCategoryID = null;

            IActionResult actionresultDelete = controller.DeleteIssueCategory(findCategoryID).Result;

            Assert.IsType<BadRequestResult>(actionresultDelete);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest; //400
            var actualStatusCode = (actionresultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCategory_OkResult()
        {

            var dbName = nameof(ItemCategoriesApiTests.DeleteCategory_OkResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);
            int findCategoryID = 2;

            IActionResult actionresultDelete = controller.DeleteIssueCategory(findCategoryID).Result;

            Assert.IsType<OkObjectResult>(actionresultDelete);


            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK; //400
            var actualStatusCode = (actionresultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }


    }
}
