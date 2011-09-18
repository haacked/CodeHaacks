using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Web.Mvc.Extensibility;

namespace MvcHaack.ViewMobilizer {
    public partial class ViewMobilizerForm : Form {
        ViewMobilizerModel model = null;
        public ViewMobilizerForm(ViewMobilizerModel model) {
            InitializeComponent();
            foreach (var view in model.Views.Values) {
                viewsCheckBoxList.Items.Add(view.RelativePath, false);
            }
            this.model = model;
        }

        private void mobilizeButton_Click(object sender, System.EventArgs e) {
            foreach (string item in viewsCheckBoxList.CheckedItems) {
                var file = this.model.Views[item];
                string mobileName = file.FullName.Substring(0, file.FullName.Length - ".cshtml".Length) + model.DeviceSpecificExtension;

                this.model.SelectedViews.Add(new Tuple<ProjectFile, string>(file, mobileName));
            }
        }

        private void OnSuffixTextBoxLeave(object sender, EventArgs e) {
            this.model.Populate(suffixTextBox.Text);
            viewsCheckBoxList.Items.Clear();
            foreach (var view in model.Views.Values) {
                viewsCheckBoxList.Items.Add(view.RelativePath, false);
            }
        }
    }
}
