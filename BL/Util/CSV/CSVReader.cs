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
        private ISpecificationSetupManager specSetupManager;

        public CSVReader(ISpecificationSetupManager specSetupManager)
        {
            this.specSetupManager = specSetupManager;
        }

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
            FileSpecification fileSpec = new FileSpecification();
            List<List<String>> fileSpecLines = ReadFile(path);
            List<FileSpecFieldCondition> fileSpecFieldConditions = new List<FileSpecFieldCondition>();
            List<GroupCondition> groupConditions = new List<GroupCondition>();

            foreach (var fileSpecLine in fileSpecLines)
            {
                string code = fileSpecLine.First();

                if (code.StartsWith("K"))
                {
                    FileSpecFieldCondition fileSpecFieldCondition = new FileSpecFieldCondition()
                    {
                     Code = code,
                     Description = fileSpecLine.ElementAt(1),
                     Level = Convert.ToInt32(fileSpecLine.ElementAt(3)),
                     Group = fileSpecLine.ElementAt(4),
                    };
                    fileSpecFieldCondition.FileSpecification = fileSpec;

                    string optionalOrMandatory = fileSpecLine.ElementAt(2);

                    if (optionalOrMandatory.Equals("O"))
                    {
                        fileSpecFieldCondition.IsOptional = true;
                    } else if (optionalOrMandatory.Equals("M"))
                    {
                        fileSpecFieldCondition.IsOptional = false;
                    }

                    FieldSpecFieldCondition fieldSpecFieldCondition = specSetupManager.GetFieldSpecFieldCondition(fieldSpecification.FieldSpecificationId, code);
                    fileSpecFieldCondition.FieldSpecFieldCondition = fieldSpecFieldCondition;
                    fileSpecFieldConditions.Add(fileSpecFieldCondition);
                } else if (code.StartsWith("A"))
                {
                    GroupCondition groupCondition = new GroupCondition()
                    {
                        Code = code,
                        Description = fileSpecLine.ElementAt(1),
                        Level = fileSpecLine.ElementAt(3),
                        ParentGroup = fileSpecLine.ElementAt(4),
                        MinimumAmountOfOccurences = Convert.ToInt32(fileSpecLine.ElementAt(5)),
                        MaximumAmountOfOccurences = Convert.ToInt32(fileSpecLine.ElementAt(6))
                    };

                    groupCondition.FileSpecification = fileSpec;
                    groupConditions.Add(groupCondition);
                }
            }
            fileSpec.FileSpecFieldConditions = fileSpecFieldConditions;
            fileSpec.GroupConditions = groupConditions;
            return fileSpec;
        }

        public static List<List<String>> ReadFile(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<List<String>> lines = new List<List<String>>();
            reader.ReadLine(); //prevents header line from being read
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
