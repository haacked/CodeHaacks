using Microsoft.VisualStudio.Web.Mvc.Extensibility;
using Moq;
using MvcHaack.ViewMobilizer;
using Xunit;

namespace ViewMobilizerTests {
    public class ViewMobilizerModelTests {
        public class TheConstructor {
            [Fact]
            public void ShowsViewsWithNoMobileEquivalent() {
                var file = new Mock<ProjectFile>();
                file.Setup(f => f.RelativePath).Returns("Views/Foo.cshtml");
                var files = new[] { file.Object };
                var folder = new Mock<ProjectFolder>() { CallBase = true };
                folder.Setup(f => f.Files).Returns(files);
                folder.Setup(f => f.Folders).Returns(new ProjectFolder[] { });

                var model = new ViewMobilizerModel(folder.Object);

                Assert.Equal(1, model.Views.Count);
                Assert.True(model.Views.ContainsKey("Views/Foo.cshtml"));
            }

            [Fact]
            public void FiltersOutViewsWithMobileEquivalent() {
                var file = new Mock<ProjectFile>();
                file.Setup(f => f.RelativePath).Returns("Views/Foo.cshtml");
                var mobileFile = new Mock<ProjectFile>();
                mobileFile.Setup(f => f.RelativePath).Returns("Views/Foo.Mobile.cshtml");
                var anotherFile = new Mock<ProjectFile>();
                anotherFile.Setup(f => f.RelativePath).Returns("Views/Bar.cshtml");
                var files = new[] { file.Object, mobileFile.Object, anotherFile.Object };
                var folder = new Mock<ProjectFolder>() { CallBase = true };
                folder.Setup(f => f.Files).Returns(files);
                folder.Setup(f => f.Folders).Returns(new ProjectFolder[] { });

                var model = new ViewMobilizerModel(folder.Object);

                Assert.Equal(1, model.Views.Count);
                Assert.True(model.Views.ContainsKey("Views/Bar.cshtml"));
            }

            [Fact]
            public void FiltersOutViewsWithSuffixedEquivalent() {
                var file = new Mock<ProjectFile>();
                file.Setup(f => f.RelativePath).Returns("Views/Foo.cshtml");
                var mobileFile = new Mock<ProjectFile>();
                mobileFile.Setup(f => f.RelativePath).Returns("Views/Foo.iphone.cshtml");
                var anotherFile = new Mock<ProjectFile>();
                anotherFile.Setup(f => f.RelativePath).Returns("Views/Bar.cshtml");
                var files = new[] { file.Object, anotherFile.Object, mobileFile.Object };
                var folder = new Mock<ProjectFolder>() { CallBase = true };
                folder.Setup(f => f.Files).Returns(files);
                folder.Setup(f => f.Folders).Returns(new ProjectFolder[] { });

                var model = new ViewMobilizerModel(folder.Object, "iphone");

                Assert.Equal(1, model.Views.Count);
                Assert.True(model.Views.ContainsKey("Views/Bar.cshtml"));
            }

            [Fact]
            public void FiltersOutViewsWithAlphaPreviousSuffixedEquivalent() {
                var file = new Mock<ProjectFile>();
                file.Setup(f => f.RelativePath).Returns("Views/Foo.cshtml");
                var mobileFile = new Mock<ProjectFile>();
                mobileFile.Setup(f => f.RelativePath).Returns("Views/Foo.aphone.cshtml");
                var anotherFile = new Mock<ProjectFile>();
                anotherFile.Setup(f => f.RelativePath).Returns("Views/Bar.cshtml");
                var files = new[] { mobileFile.Object, file.Object, anotherFile.Object };
                var folder = new Mock<ProjectFolder>() { CallBase = true };
                folder.Setup(f => f.Files).Returns(files);
                folder.Setup(f => f.Folders).Returns(new ProjectFolder[] { });

                var model = new ViewMobilizerModel(folder.Object, "aphone");

                Assert.Equal(1, model.Views.Count);
                Assert.True(model.Views.ContainsKey("Views/Bar.cshtml"));
            }
        }
    }
}
