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
            List<List<String>> fieldConditionLines = ReadFile(path);
            List<FieldSpecFieldCondition> fieldSpecFieldConditions = new List<FieldSpecFieldCondition>();
            foreach (var fieldConditionLine in fieldConditionLines)
            {
                FieldSpecFieldCondition fieldSpecFieldCondition = new FieldSpecFieldCondition();
                fieldSpecFieldCondition.FieldCode = fieldConditionLine[0];
                fieldSpecFieldCondition.Datatype = fieldConditionLine[1];
                fieldSpecFieldCondition.Size = Int32.Parse(fieldConditionLine[2]);
                fieldSpecFieldCondition.Format = fieldConditionLine[3];
                List<String> stringAllowedValues = fieldConditionLine[4].Split('-').ToList();
                List<AllowedValue> allowedValues = new List<AllowedValue>();
                foreach(var stringAllowedValue in stringAllowedValues)
                {
                    AllowedValue allowedValue = new AllowedValue();
                    allowedValue.Value = stringAllowedValue;
                    allowedValues.Add(allowedValue);
                }
                fieldSpecFieldCondition.AllowedValues = allowedValues;
                fieldSpecFieldConditions.Add(fieldSpecFieldCondition);
            }
            fieldspec.FieldSpecFieldConditions = fieldSpecFieldConditions;
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
