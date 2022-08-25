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
            public void InsertCategory_OkResult()
            {
            // ARRANGE

            var dbName = nameof(ItemCategoriesApiTests.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<IssueCategoriesController>>();
            using var dbContext = DbMocker.GetMajorProjectDbContext(dbName);
            // Disposable!

            var controller = new IssueCategoriesController(dbContext, logger);
                IssueCategory categoryToAdd = new IssueCategory
                {
                    IssueCategoryId = 5,
                    Issue = null,           // INVALID!  CategoryName is REQUIRED
                };

                // ACT
                IActionResult actionResultPost = controller.PostIssueCategory(categoryToAdd).Result;

                // ASSERT - check if the IActionResult is Ok
                Assert.IsType<OkObjectResult>(actionResultPost);

                // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
                int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
                var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
                Assert.Equal<int>(expectedStatusCode, actualStatusCode);

                // Extract the result from the IActionResult object.
                var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

                // ASSERT - if the result is a CreatedAtActionResult
                Assert.IsType<CreatedAtActionResult>(postResult.Value);

                // Extract the inserted Category object
                IssueCategory actualCategory = (postResult.Value as CreatedAtActionResult).Value
                                          .Should().BeAssignableTo<IssueCategory>().Subject;

                // ASSERT - if the inserted Category object is NOT NULL
                Assert.NotNull(actualCategory);

                Assert.Equal(categoryToAdd.IssueCategoryId, actualCategory.IssueCategoryId);
                Assert.Equal(categoryToAdd.Issue, actualCategory.Issue);
            }

        
    }
}
