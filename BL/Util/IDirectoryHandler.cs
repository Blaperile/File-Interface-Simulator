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
        void CreateFile(string fileName, string content, Directory directory);
        IEnumerable<String> GetFileNamesOfType(string type, Directory directory);
        string GetContentOfFile(string fileName, Directory directory);
        void MoveFile(String fileName, String inPath, String archivePath);
    }
}
