using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace ItemTestApixUnit
{
    public partial class ItemCategoriesApiTests
    {
        private readonly ITestOutputHelper _testoutputHelper;

        public ItemCategoriesApiTests(ITestOutputHelper outputHelper)
        {
            _testoutputHelper = outputHelper;
        }
    }
}
