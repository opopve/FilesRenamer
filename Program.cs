using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilesRenamer
{
    class MainClass
    {
        static void Main(string[] args) {

            DirectoryInfo pathToFiles = new DirectoryInfo(args.FirstOrDefault() ?? "D:\\Temp\\_ren\\1.log");
            Console.WriteLine(pathToFiles.FullName);

            var path = (pathToFiles.Exists ? pathToFiles : pathToFiles.Parent);

            new Renamer(path, Console.WriteLine).Run();

            Console.WriteLine("Success. Press \"q\" to exit");
            var key = Console.ReadKey();
            if(key.KeyChar == 'q')
                return;
        }
    }
}
