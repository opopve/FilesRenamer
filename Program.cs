using System;

namespace FilesRenamer
{
    class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Join('\n', args));
            var key = Console.ReadKey();
            if (key.KeyChar == 'e')
                return;
        }
    }
}
