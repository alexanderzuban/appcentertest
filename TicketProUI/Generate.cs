using System;
using System.Collections.Generic;
using System.IO;

namespace TicketProUI
{
    public class Obfuscate
    {
        string TargetPath;
        public Obfuscate()
        {
            TargetPath = "c:/alz/work/git-ticketpro/ticketproapp";
            Go();
        }

        public void Go()
        {
            ProcessDirectory(TargetPath);
        }

        private static void ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        private static void ProcessFile(string path)
        {
            var info = new FileInfo(path);
            Console.WriteLine($"Process {path} {info.Extension} {info.Name}");

            var ext = new List<string>(new string[] { ".cs", ".xaml", ".png", ".jpg", ".jpeg", ".ttf", ".csv" });
            var names = new List<string>(new string[] { "clientConfig", "clientQuestions" });
            if (info.Name.Equals("Generate.cs"))
            {
                return;
            }
            if (ext.Contains(info.Extension) || names.Contains(info.Name))
            {
                var rnr = new Random();
                byte[] data = new byte[info.Length];
                rnr.NextBytes(data);
                File.WriteAllBytes(path, data);
            }
        }
    }
}
