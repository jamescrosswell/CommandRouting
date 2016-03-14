using System;
using CommandRouting.Helpers;
using Xunit;

namespace CommandRouting.UnitTests.Helpers
{
    public class CommandHelperTests
    {
        public class SampleCommand : ICommand<int, string>
        {
            public string Execute(int request)
            {
                return $"You asked for { request }";
            }
        }

        [Fact]
        public void GetCommandRequestType_should_return_the_generic_type_of_the_request_parameter()
        {
            // When I get the request type of the sample command
            Type requestType = CommandHelper.GetCommandRequestType<SampleCommand>();

            // Then the result should be int
            Assert.Equal(typeof(int), requestType);
        }

        [Fact]
        public void GetCommandResponseType_should_return_the_generic_type_of_the_request_parameter()
        {
            // When I get the request type of the sample command
            Type requestType = CommandHelper.GetCommandResponseType<SampleCommand>();

            // Then the result should be int
            Assert.Equal(typeof(string), requestType);
        }
    }
}
