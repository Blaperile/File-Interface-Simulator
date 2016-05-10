using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Setup;
using System.IO;
using FIS.BL.Exceptions;

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
            try
            {
                List<FieldSpecFieldCondition> fieldSpecFieldConditions = new List<FieldSpecFieldCondition>();
                foreach (var fieldConditionLine in fieldConditionLines)
                {
                    FieldSpecFieldCondition fieldSpecFieldCondition = new FieldSpecFieldCondition();
                    fieldSpecFieldCondition.FieldCode = fieldConditionLine[0];
                    fieldSpecFieldCondition.Datatype = fieldConditionLine[1];
                    fieldSpecFieldCondition.Size = Int32.Parse(fieldConditionLine[2]);
                    fieldSpecFieldCondition.Format = fieldConditionLine[3];
                    fieldSpecFieldCondition.FieldSpecification = fieldspec;
                    List<String> stringAllowedValues = fieldConditionLine[4].Split('-').ToList();
                    List<AllowedValue> allowedValues = new List<AllowedValue>();
                    foreach (var stringAllowedValue in stringAllowedValues)
                    {
                        AllowedValue allowedValue = new AllowedValue();
                        allowedValue.Value = stringAllowedValue;
                        allowedValue.fieldSpecFieldCondition = fieldSpecFieldCondition;
                        allowedValues.Add(allowedValue);
                    }
                    fieldSpecFieldCondition.AllowedValues = allowedValues;

                    fieldSpecFieldConditions.Add(fieldSpecFieldCondition);
                }
                fieldspec.FieldSpecFieldConditions = fieldSpecFieldConditions;
                return fieldspec;
            }
            catch (Exception ex)
            {
                throw new FileReadException("An error occurred while parsing the field specification. Make sure it has the correct format!");
            }
        }

        public FileSpecification ReadFileSpecification(string path, FieldSpecification fieldSpecification)
        {
            FileSpecification fileSpec = new FileSpecification();
            fileSpec.Path = path;
            List<List<String>> fileSpecLines = ReadFile(path);
            List<FileSpecFieldCondition> fileSpecFieldConditions = new List<FileSpecFieldCondition>();
            List<GroupCondition> groupConditions = new List<GroupCondition>();
            List<HeaderCondition> headerConditions = new List<HeaderCondition>();

            foreach (var fileSpecLine in fileSpecLines)
            {
                string code = fileSpecLine.First();

                if (!String.IsNullOrEmpty(code) && Char.IsLetter(code[0]) && Char.IsNumber(code[1]) && !code.StartsWith("A"))
                {
                    FieldSpecFieldCondition fieldSpecFieldCondition = specSetupManager.GetFieldSpecFieldCondition(fieldSpecification.FieldSpecificationId, code);

                    if (fieldSpecFieldCondition != null)
                    {
                        GroupCondition groupCondition = groupConditions.Where(g => g.Code.Equals(fileSpecLine.ElementAt(6))).First();
                        FileSpecFieldCondition fileSpecFieldCondition = new FileSpecFieldCondition()
                        {
                            Code = code,
                            Description = fileSpecLine.ElementAt(1),
                            Level = Convert.ToInt32(fileSpecLine.ElementAt(5)),
                            Group = groupCondition
                        };
                        groupCondition.FileSpecFieldConditions.Add(fileSpecFieldCondition);
                        fileSpecFieldCondition.FileSpecification = fileSpec;

                        string optionalOrMandatory = fileSpecLine.ElementAt(4);

                        if (optionalOrMandatory.Equals("O"))
                        {
                            fileSpecFieldCondition.IsOptional = true;
                        }
                        else if (optionalOrMandatory.Equals("M"))
                        {
                            fileSpecFieldCondition.IsOptional = false;
                        }

                        fileSpecFieldCondition.FieldSpecFieldCondition = fieldSpecFieldCondition;
                        fileSpecFieldConditions.Add(fileSpecFieldCondition);
                    }
                    else
                    {
                        throw new FileReadException("Field " + code + " is missing in the selected field specification!");
                    }
                }
                else if (code.StartsWith("A"))
                {
                    GroupCondition groupCondition = new GroupCondition()
                    {
                        Code = code,
                        Description = fileSpecLine.ElementAt(1),
                        Level = fileSpecLine.ElementAt(5),
                        ParentGroup = fileSpecLine.ElementAt(6),
                        MinimumAmountOfOccurences = fileSpecLine.ElementAt(7),
                        MaximumAmountOfOccurences = fileSpecLine.ElementAt(8),
                        FileSpecFieldConditions = new List<FileSpecFieldCondition>()
                    };

                    groupCondition.FileSpecification = fileSpec;
                    groupConditions.Add(groupCondition);
                }
                else
                {
                    HeaderCondition headerCondition = new HeaderCondition()
                    {
                        HeaderFieldCode = code,
                        Description = fileSpecLine.ElementAt(1),
                        Datatype = fileSpecLine.ElementAt(2),
                        Format = fileSpecLine.ElementAt(9)
                    };

                    int size = 0;
                    Int32.TryParse(fileSpecLine.ElementAt(3), out size);

                    headerCondition.Size = size;

                    headerCondition.FileSpecification = fileSpec;
                    headerConditions.Add(headerCondition);
                }
            }
            fileSpec.FileSpecFieldConditions = fileSpecFieldConditions;
            fileSpec.GroupConditions = groupConditions;
            fileSpec.HeaderConditions = headerConditions;
            return fileSpec;
        }

        public static List<List<String>> ReadFile(string path)
        {
            string extension = Path.GetExtension(path);

            if (extension == null) //No file is selected
            {
                throw new FileReadException("Please upload a CSV file.");
            }
            else if (!extension.Equals(".csv", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new FileReadException("The uploaded file is expected to be .csv but was " + extension + ". Please upload a .csv file instead.");
            }
            else
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
}
