using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FIS.BL.Util
{
    public class DirectoryHandler: IDirectoryHandler
    {

        public void CreateFile(string fileName, string content, Domain.Setup.Directory directory)
        {
            string path = directory.Location + @"\" + fileName;
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
        }

        public string GetContentOfFile(string fileName, Domain.Setup.Directory currentDirectory)
        {
            return File.ReadAllText(currentDirectory.Location + @"\" + fileName);
        }

        public IEnumerable<string> GetFileNamesOfType(string type, Domain.Setup.Directory currentDirectory)
        {
            IList<String> fileNames = new List<String>();
            foreach (string pathToFile in System.IO.Directory.GetFiles(currentDirectory.Location, "*." + type))
            {
                fileNames.Add(Path.GetFileName(pathToFile));
            }
            return fileNames;
        }
        
        public void MoveFile(String fileName,  String inPath, String archivePath)
        {
            inPath += fileName;
            archivePath += fileName;
            File.Move(inPath, archivePath);
        }
    }
}
