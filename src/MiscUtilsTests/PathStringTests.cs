using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using MiscUtils;
using Newtonsoft.Json;
using Xunit;
using Xunit.Extensions;

namespace MiscUtilsTests
{
    public class PathStringTests
    {
        public class TheConstructor
        {
            [Theory]
            [InlineData(@"c:\foo\bar")]
            [InlineData(@"c:\foo/bar")]
            [InlineData(@"c:/foo/bar")]
            public void NormalizesSeparator(string pathString)
            {
                var path = new PathString(pathString);
                new PathString(@"c:\foo\bar").Equals(path);
            }

            [Fact]
            public void EnsuresArgumentNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new PathString(null));
            }
        }

        public class TheCombineMethod
        {
            [Theory]
            [InlineData(@"c:\fake", ".git", @"c:\fake\.git")]
            [InlineData(@"c:\fake\", ".git", @"c:\fake\.git")]
            [InlineData(@"c:\fake\", "", @"c:\fake\")]
            public void ComparseHostCaseInsensitive(string pathString, string path, string expected)
            {
                Assert.Equal((PathString)expected, new PathString(pathString).Combine(path));
            }

            [Fact]
            public void ThrowsArgumentNullException()
            {
                Assert.Throws<ArgumentNullException>(() => new PathString(@"c:\").Combine(null));
            }
        }

        public class TheAdditionOperator
        {
            [Theory]
            [InlineData(@"c:\foo", "bar", @"c:\foo\bar")]
            [InlineData(@"c:\foo", @"\bar", @"c:\foo\bar")]
            [InlineData(@"c:\foo\", @"\bar", @"c:\foo\bar")]
            [InlineData(@"c:\foo\", "bar", @"c:\foo\bar")]
            [InlineData(@"c:\foo", @"bar\baz", @"c:\foo\bar\baz")]
            [InlineData(@"c:\foo", @"bar\baz\", @"c:\foo\bar\baz\")]
            [InlineData(@"c:\foo", @"bar/baz/", @"c:\foo\bar\baz\")]
            [InlineData(@"c:\foo", "garb|age", @"c:\foo\garb|age")]
            [InlineData(@"c:\foo\", "/garb|age", @"c:\foo\garb|age")]
            [InlineData(@"c:\foo\", @"\garb|age", @"c:\foo\garb|age")]
            public void CombinesPaths(string pathString, string addition, string expected)
            {
                PathString path = pathString;
                var newPath = path + addition;
                Assert.Equal(expected, (string)newPath);
            }
        }

        public class ImplicitConversionToString
        {
            [Fact]
            public void ConvertsBackToString()
            {
                var pathString = new PathString(@"c:\fake\path\yo");
                string path = pathString;
                Assert.Equal(@"c:\fake\path\yo", path);
            }

            [Fact]
            public void ConvertsNullToNull()
            {
                PathString pathString = null;
                Assert.Null(pathString);
                string path = pathString;
                Assert.Null(path);
            }
        }

        public class ImplicitConversionFromString
        {
            [Fact]
            public void ConvertsToCloneUri()
            {
                PathString pathString = @"c:\fake\random\path";

                Assert.NotNull(pathString);
                Assert.Equal(@"c:\fake\random\path", (string)pathString);
            }

            [Fact]
            public void ConvertsNullToNull()
            {
                PathString pathString = (string)null;
                Assert.Null(pathString);
            }
        }

        public class TheEqualityOperatorAndMethods
        {
            [Theory]
            [InlineData(@"c:\foo\bar", @"c:\foo\bar", true)]
            [InlineData(@"c:\foo\bar", @"c:\FOO\BAR", true)]
            [InlineData(null, null, true)]
            [InlineData(@"", null, false)]
            [InlineData(null, @"", false)]
            [InlineData(@"c:\foo", @"c:\foo\", false)]
            public void ComparesTwoPaths(string path1, string path2, bool expected)
            {
                PathString pathOne = path1;
                PathString pathTwo = path2;

                Assert.Equal(expected, (pathOne == pathTwo));
                if (pathOne != null)
                {
                    Assert.Equal(expected, pathOne.Equals(pathTwo));
                    Assert.Equal(expected, pathOne.Equals((object)pathTwo));
                    Assert.Equal(expected, pathOne.Equals((object)path2));
                    Assert.Equal(expected, pathOne.Equals(path2));
                }
            }
        }

        public class TheInequalityOperator
        {
            [Theory]
            [InlineData(@"c:\foo\bar", @"c:\foo\bar", false)]
            [InlineData(@"c:\foo\bar", @"c:\FOO\BAR", false)]
            [InlineData(null, null, false)]
            [InlineData(@"", null, true)]
            [InlineData(null, @"", true)]
            [InlineData("c:\foo", @"c:\foo\", true)]
            public void ComparesTwoPaths(string path1, string path2, bool expected)
            {
                PathString pathOne = path1;
                PathString pathTwo = path2;
                Assert.Equal(expected, (pathOne != pathTwo));
            }
        }

        public class Serialization
        {
            [Fact]
            public void RoundTripsBinarySerialization()
            {
                var serializer = new BinaryFormatter();
                var stream = new MemoryStream();
                serializer.Serialize(stream, new PathString(@"c:\foo\bar"));
                stream.Position = 0;

                var deserialized = serializer.Deserialize(stream) as PathString;
                Assert.Equal((PathString)@"c:\foo\bar", deserialized);
            }

            [Fact]
            public void RoundTripsJsonSerialization()
            {
                var instance = new CachedObjectV2
                {Id = 1, Foo = "Foobar", OriginalPath = @"c:\fake\original\path", AnotherPath = null};
                var json = JsonConvert.SerializeObject(instance);
                var retrieved = JsonConvert.DeserializeObject<CachedObjectV2>(json);

                Assert.Equal(1, retrieved.Id);
                Assert.Equal("Foobar", retrieved.Foo);
                Assert.Equal(@"c:\fake\original\path", (string)retrieved.OriginalPath);
                Assert.Null(retrieved.AnotherPath);
            }

            [Fact]
            public void RoundTripsJsonSerializationResilientToVersioning()
            {
                var instance = new CachedObjectV1
                {Id = 1, Foo = "Foobar", OriginalPath = @"c:\fake\original\path", AnotherPath = null};
                var json = JsonConvert.SerializeObject(instance);
                var retrieved = JsonConvert.DeserializeObject<CachedObjectV2>(json);

                Assert.Equal(1, retrieved.Id);
                Assert.Equal("Foobar", retrieved.Foo);
                Assert.Equal(@"c:\fake\original\path", (string)retrieved.OriginalPath);
                Assert.Null(retrieved.AnotherPath);
            }

            [Fact]
            public void RoundTripsXmlSerialization()
            {
                var serializer = new XmlSerializer(typeof (PathString));
                var stream = new MemoryStream();
                serializer.Serialize(stream, new PathString("This is a test"));
                stream.Position = 0;

                var deserialized = serializer.Deserialize(stream) as PathString;
                Assert.Equal((PathString)"This is a test", deserialized);
            }

            [Fact]
            public void RoundTripsXmlSerializationAsPropertyOfAnotherObject()
            {
                var instance = new CachedObjectV2
                {Id = 1, Foo = "Foobar", OriginalPath = @"c:\fake\original\path", AnotherPath = null};
                var serializer = new XmlSerializer(typeof (CachedObjectV2));
                var stream = new MemoryStream();
                serializer.Serialize(stream, instance);
                stream.Position = 0;

                var retrieved = serializer.Deserialize(stream) as CachedObjectV2;

                Assert.Equal(1, retrieved.Id);
                Assert.Equal("Foobar", retrieved.Foo);
                Assert.Equal(@"c:\fake\original\path", (string)retrieved.OriginalPath);
                Assert.Null(retrieved.AnotherPath);
            }

            [Fact]
            public void RoundTripsXmlSerializationAsPropertyOfAnotherObjectResilientToVersioning()
            {
                var instance = new CachedObjectV1
                {Id = 1, Foo = "Foobar", OriginalPath = @"c:\fake\original\path", AnotherPath = null};
                var serializer = new XmlSerializer(typeof (CachedObjectV1));
                var stream = new MemoryStream();
                serializer.Serialize(stream, instance);
                stream.Position = 0;

                serializer = new XmlSerializer(typeof (CachedObjectV2));
                var retrieved = serializer.Deserialize(stream) as CachedObjectV2;

                Assert.Equal(1, retrieved.Id);
                Assert.Equal("Foobar", retrieved.Foo);
                Assert.Equal(@"c:\fake\original\path", (string)retrieved.OriginalPath);
                Assert.Null(retrieved.AnotherPath);
            }

            public class CachedObjectV1
            {
                public int Id { get; set; }
                public string Foo { get; set; }
                public string OriginalPath { get; set; }
                public string AnotherPath { get; set; }
            }

            [XmlRoot("CachedObjectV1")]
            public class CachedObjectV2
            {
                public int Id { get; set; }
                public string Foo { get; set; }
                public PathString OriginalPath { get; set; }
                public PathString AnotherPath { get; set; }
            }
        }
    }
}