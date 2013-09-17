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
            Assert.Equal("NVal1", attributes[0].NamedProperty);
            Assert.Equal("Val1", attributes[0].NamedField);
            Assert.Equal("ROVal2", attributes[1].ReadOnlyProperty);
            Assert.Equal("NVal2", attributes[1].NamedProperty);
            Assert.Equal("Val2", attributes[1].NamedField);
        }

        [Something("ROVal1", NamedProperty = "NVal1", NamedField = "Val1")]
        [Something("ROVal2", NamedProperty = "NVal2", NamedField = "Val2")]
        public class Character
        {
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class SomethingAttribute : Attribute
        {
            public string NamedField;

            public SomethingAttribute(string readOnlyProperty)
            {
                ReadOnlyProperty = readOnlyProperty;
            }

            public string ReadOnlyProperty { get; private set; }
            public string NamedProperty { get; set; }
        }
    }
}