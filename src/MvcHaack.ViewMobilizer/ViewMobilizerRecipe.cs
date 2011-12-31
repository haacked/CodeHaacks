using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.Web.Mvc.Extensibility;
using Microsoft.VisualStudio.Web.Mvc.Extensibility.Recipes;
using MvcHaack.ViewMobilizer.Properties;

namespace MvcHaack.ViewMobilizer
{
    [Export(typeof(IRecipe))]
    public class ViewMobilizerRecipe : IFolderRecipe
    {
        public bool Execute(ProjectFolder folder)
        {
            var model = new ViewMobilizerModel(folder);
            var form = new ViewMobilizerForm(model);

            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (var view in model.SelectedViews)
                {
                    var file = view.Item1;
                    string mobileFileName = view.Item2;

                    File.Copy(file.FullName, mobileFileName);
                    folder.DteProjectItems.AddFromFile(mobileFileName);
                }
            }
            return true;
        }

        public bool IsValidTarget(ProjectFolder folder)
        {
            return true;
        }

        public string Description
        {
            get { return "A package for creating display mode views."; }
        }

        public Icon Icon
        {
            get { return Resources.ViewMobilizer; }
        }

        public string Name
        {
            get { return "View Mobilizer"; }
        }
    }
}

