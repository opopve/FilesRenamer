using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesRenamer
{
    class Renamer
    {
        DirectoryInfo path;
        List<Task> tasks = new List<Task>();
        Action<string> showMessageAction;

        public Renamer(DirectoryInfo path, Action<string> showMessageAction) {
            this.path = path;
            this.showMessageAction = showMessageAction ?? throw new ArgumentNullException(nameof(showMessageAction));
        }

        public void Run() {
            foreach(var item in path.GetFileSystemInfos()) {
                if(item is DirectoryInfo dir) {
                    var task = new Task(new Renamer(dir, showMessageAction).Run);
                    tasks.Add(task);
                    task.Start();
                    continue;
                }

                int counter = -1;
                if(item is FileInfo file) {
                    var fileWriteTime = file.LastWriteTime.ToString("yyyyMMdd_HHmmssffff");
                    var fileCreationTime = file.CreationTime.ToString("yyyyMMdd_HHmmssffff");
                    var sourcePath = file.FullName;
                    string destinationPath;
                    do
                        destinationPath = PrepareDestinationPath(file, fileWriteTime, counter++);
                    while(new FileInfo(destinationPath).Exists);
                    File.Move(sourcePath, destinationPath);
                    counter = -1;
                }
            }
            Task.WaitAll(tasks.ToArray());
        }

        string PrepareDestinationPath(FileInfo file, string newName, int counter = -1) => string.Join(string.Empty, file.DirectoryName, Path.DirectorySeparatorChar, newName, counter < 0 ? string.Empty : "_" + counter, file.Extension);
    }
}
