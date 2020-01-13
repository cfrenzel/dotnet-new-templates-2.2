using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using Shouldly;

namespace SolutionName.UnitTests
{
    public class SampleTests
    {
     
        public SampleTests()
        {

        }


        [Fact]
        public void Should_meet_requirement()
        {
            0.ShouldBe(1);
        }

    }

}
