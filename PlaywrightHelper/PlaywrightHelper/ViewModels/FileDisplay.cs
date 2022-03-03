using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Metadata;

namespace PlaywrightHelper.ViewModels;

    public class FileDisplay
    {
        [Content] 
        public string Display { get; set; }
        public FileSystemInfo FileInfo { get; set; }
        public ObservableCollection<FileDisplay> Children { get; set; }

    }
