using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Shouldly;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using FakeItEasy;
using System.Data.SqlClient;
using Xunit.Abstractions;

namespace SolutionName.IntegrationTests
{
    using static IntegrationTestFixture;
    
    [Collection("Sequential")]
    public class SampleTests : IntegrationTestBase, IClassFixture<SampleTests.Fixture>
    {
        private readonly Fixture _fixture;
        private readonly ITestOutputHelper _log;

        public SampleTests(Fixture fixture, ITestOutputHelper log)
        {
            this._fixture = fixture;
            this._log = log;
            //using (var scope = NewScope())
            //{
            //}
        }

        public class Fixture
        {
            public Fixture()
            {
            
            }
        }

        [Fact]
        public async Task Should_meet_requirement()
        {
            1.ShouldBe(2);
        }
      
    }
}
