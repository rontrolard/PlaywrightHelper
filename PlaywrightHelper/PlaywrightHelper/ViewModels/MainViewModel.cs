using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Microsoft.Playwright;
using PlaywrightHelper.Views;

namespace PlaywrightHelper.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public ObservableCollection<FileDisplay> ScriptSource { get; }
        private string rootPath;
        public async void RecordingStarted()
        {
            var exitCode = Microsoft.Playwright.Program.Main(new[] {"install"});
            if (exitCode != 0)
            {
                throw new Exception($"Playwright exited with code {exitCode}");
            }            
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false,
                TracesDir = rootPath
            });
            var context = await browser.NewContextAsync(); // Pass any options
            var page = await browser.NewPageAsync();
            await page.PauseAsync();

        }

        public MainViewModel()
        {
            var home = Environment.GetEnvironmentVariable("HOME");
            if(string.IsNullOrEmpty(home))
                home = Environment.GetEnvironmentVariable("USER_PROFILE");
            rootPath = $"{home}{Path.DirectorySeparatorChar}playwrightScripts";
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            var pathInfo = new DirectoryInfo(rootPath);
            ScriptSource = BuildPath(pathInfo);
            
        }
        public ObservableCollection<FileDisplay> BuildPath(DirectoryInfo pathInfo, ObservableCollection<FileDisplay>? files = null)
        {
            if (files == null)
                files = new ObservableCollection<FileDisplay>();
            foreach (var fsi in pathInfo.GetFileSystemInfos())
            {
                if((fsi.Attributes & FileAttributes.Directory) > 0)
                    files.Add(new FileDisplay()
                    {
                        Display = fsi.Name,
                        FileInfo = fsi,
                        Children = BuildPath(new DirectoryInfo(fsi.FullName))
                    });
                else
                    files.Add(new FileDisplay()
                    {
                        Display = fsi.Name,
                        FileInfo = fsi,
                    });
                    
            }

            return files;

        }
    }
}