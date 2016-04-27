using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util
{
    public class DirectoryHandler: IDirectoryHandler
    {
        public Directory Directory { get; set; }

        public void CreateFile(string fileName, string content)
        {
            throw new NotImplementedException();
        }

        public string GetContentOfFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetFileNamesOfType(string type)
        {
            throw new NotImplementedException();
        }
    }
}
