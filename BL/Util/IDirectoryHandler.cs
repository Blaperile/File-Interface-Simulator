﻿using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util
{
    public interface IDirectoryHandler
    {
        void CreateFile(string fileName, string content);
        IEnumerable<String> GetFileNamesOfType(string type, Directory directory);
        string GetContentOfFile(string fileName, Directory directory);
    }
}
