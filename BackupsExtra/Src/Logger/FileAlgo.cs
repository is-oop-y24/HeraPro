using System.IO;

namespace BackupsExtra.Logger
{
    public class FileAlgo : IAlgoLog
    {
        public FileAlgo(string path)
        {
            if (File.Exists(path))
                Log = new StreamWriter(path);
            if (Directory.Exists(path))
                Log = new StreamWriter(path + "log.txt");
        }

        public StreamWriter Log { get; }

        public void Write(string message)
        {
            Log.WriteLine(message);
        }
    }
}