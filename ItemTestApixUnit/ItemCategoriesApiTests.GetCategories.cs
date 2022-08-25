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

namespace ItemTestApixUnit
{
    public partial class ItemCategoriesApiTests
    {
        [Fact]
        public void GetCategories_OkResult()
        {
            var dbName = nameof(ItemCategoriesApiTests.GetCategories_OkResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);

            IActionResult actionresult = controller.GetIssueCategories().Result;

            Assert.IsType<OkObjectResult>(actionresult);

            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionresult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }


        [Fact]
        public void GetCategories_CheckCorrectResult()
        {
            var dbName = nameof(ItemCategoriesApiTests.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            var controller = new IssueCategoriesController(dbContext, logger);

            IActionResult actionresult = controller.GetIssueCategories().Result;

            Assert.IsType<OkObjectResult>(actionresult);

            var okResult = actionresult.Should().BeOfType<OkObjectResult>().Subject;

            Assert.IsAssignableFrom<List<IssueCategory>>(okResult.Value); //error can be found

            var categories = okResult.Value.Should().BeAssignableTo<List<IssueCategory>>().Subject;

            Assert.NotNull(categories);

            Assert.Equal(expected: DbMocker.TestCollectionOfIsssues.Length,
                        actual: categories.Count);


            int ndx = 0;
            foreach (IssueCategory Category in DbMocker.TestCollectionOfIsssues)
            {
                Assert.Equal<int>(expected: Category.IssueCategoryId,
                    actual: categories[ndx].IssueCategoryId);

                Assert.Equal(expected: Category.Issue,
                    actual: categories[ndx].Issue);

                _testoutputHelper.WriteLine($"Row # {ndx} Result is !!!  Issue Id- {Category.IssueCategoryId} Issue - {Category.Issue}");
                ndx++;
            }

        }
    }
}
