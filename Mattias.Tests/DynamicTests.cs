using System;
using Xunit;
using Xunit.Abstractions;

namespace Mattias.Tests
{
    public class DynamicTests
    {
        private ITestOutputHelper _outputHelper;
        public DynamicTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
        [Fact]
        public void Test1()
        {
            var serie = new Serie { Score = 300 };
            var lane = new Lane { Name = "Lane" };
            Save(serie);
            Save(lane);
        }
        private void Save(object o)
        {
            var name = o.GetType().Name;
            _outputHelper.WriteLine(name);
            foreach (var prop in o.GetType().GetProperties())
            {
                object value = prop.GetValue(o);
                _outputHelper.WriteLine(value.ToString());
            }
        }
    }
}
