using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;
using System.IO;

namespace FIS.BL.Util.CSV
{
    public class CSVReader : ISpecificationReader
    {
        public FieldSpecification ReadFieldSpecification(string path)
        {
            FieldSpecification fieldspec = new FieldSpecification();
            List<List<String>> fieldconditionLines = ReadFile(path);

            return fieldspec;
        }

        public FileSpecification ReadFileSpecification(string path, FieldSpecification fieldSpecification)
        {
            throw new NotImplementedException();
        }

        public static List<List<String>> ReadFile(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<List<String>> lines = new List<List<String>>();
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                List<String> values = line.Split(';').ToList();

                lines.Add(values);
            }

            return lines;
        }
    }
}
