using System;
using System.Linq;
using MiscUtils;
using Xunit;

namespace MiscUtilsTests
{
    public class AttributeExtensionsTests
    {
        [Fact]
        public void CanCopyAttribute()
        {
            var attributes = typeof (Character).GetCustomAttributesCopy()
                .Cast<SomethingAttribute>()
                .OrderBy(x => x.NamedField)
                .ToArray();

            Assert.Equal("ROVal1", attributes[0].ReadOnlyProperty);
            Assert.Equal("_x0", attributes[0].ReadOnlyArray[0]);
            Assert.Equal("_x1", attributes[0].ReadOnlyArray[1]);
            Assert.Equal("NVal1", attributes[0].NamedProperty);
            Assert.Equal("Val1", attributes[0].NamedField);
            Assert.Equal("x0", attributes[0].NamedArray[0]);
            Assert.Equal("x1", attributes[0].NamedArray[1]);
            Assert.Equal("ROVal2", attributes[1].ReadOnlyProperty);
            Assert.Equal("_y0", attributes[1].ReadOnlyArray[0]);
            Assert.Equal("_y1", attributes[1].ReadOnlyArray[1]);
            Assert.Equal("NVal2", attributes[1].NamedProperty);
            Assert.Equal("Val2", attributes[1].NamedField);
            Assert.Equal("y0", attributes[1].NamedArray[0]);
            Assert.Equal("y1", attributes[1].NamedArray[1]);
        }

        [Something("ROVal1",
            new[] {"_x0", "_x1"},
            NamedProperty = "NVal1",
            NamedField = "Val1",
            NamedArray = new[] {"x0", "x1"})]
        [Something("ROVal2",
            new[] {"_y0", "_y1"},
            NamedProperty = "NVal2",
            NamedField = "Val2",
            NamedArray = new[] {"y0", "y1"})]
        public class Character
        {
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class SomethingAttribute : Attribute
        {
            public string NamedField;

            public SomethingAttribute(string readOnlyProperty, string[] readOnlyArray)
            {
                ReadOnlyProperty = readOnlyProperty;
                ReadOnlyArray = readOnlyArray;
            }

            public string ReadOnlyProperty { get; private set; }
            public string NamedProperty { get; set; }
            public string[] NamedArray { get; set; }
            public string[] ReadOnlyArray { get; set; }
        }
    }
}