using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Web.Mvc.Extensibility;

namespace MvcHaack.ViewMobilizer
{
    public class ViewMobilizerModel
    {
        public ViewMobilizerModel(ProjectFolder folder)
            : this(folder, "mobile")
        {
        }

        public ViewMobilizerModel(ProjectFolder folder, string suffix)
        {
            Folder = folder;
            Populate(suffix);
        }

        public ProjectFolder Folder { get; set; }

        public string Suffix { get; private set; }

        public string DeviceSpecificExtension { get; private set; }

        public void Populate(string suffix)
        {
            Suffix = suffix;
            DeviceSpecificExtension = String.Format(".{0}.cshtml", Suffix);
            var views = new Dictionary<string, ProjectFile>(StringComparer.OrdinalIgnoreCase);
            FilterViews(Folder, views);
            Views = views;
            SelectedViews = new List<Tuple<ProjectFile, string>>();
        }



        private void FilterViews(ProjectFolder folder, Dictionary<string, ProjectFile> views)
        {
            // Collect
            Traverse(folder, file => views.Add(file.RelativePath, file));

            // Filter
            Traverse(folder, file => RemoveDeviceSpecificWithCorrespondingDesktop(file, views));
        }

        private void Traverse(ProjectFolder folder, Action<ProjectFile> action)
        {
            foreach (var file in folder.Files.Where(file => file.RelativePath.EndsWith(".cshtml", StringComparison.OrdinalIgnoreCase)))
            {
                action(file);
            }
            foreach (var subfolder in folder.Folders)
            {
                Traverse(subfolder, action);
            }
        }

        private void RemoveDeviceSpecificWithCorrespondingDesktop(ProjectFile file, Dictionary<string, ProjectFile> views)
        {
            string path = file.RelativePath;
            
            if (!path.EndsWith(DeviceSpecificExtension, StringComparison.OrdinalIgnoreCase)) return;
            
            int preSuffixIndex = path.Length - DeviceSpecificExtension.Length;
            string desktopPath = path.Substring(0, preSuffixIndex) + ".cshtml";
            if (views.ContainsKey(desktopPath))
            {
                views.Remove(desktopPath);
                views.Remove(path);
            }
        }

        public Dictionary<string, ProjectFile> Views { get; private set; }

        public List<Tuple<ProjectFile, string>> SelectedViews { get; private set; }
    }
}
