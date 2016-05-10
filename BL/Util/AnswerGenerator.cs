using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;

namespace FIS.BL.Util
{
    public class AnswerGenerator : IAnswerGenerator
    {
        public Message GenerateAnswer(Message message, FileSpecification fileSpecification)
        {
            Message answerMessage = new Message()
            {
                FileSpecification = fileSpecification,
                Date = DateTime.Now,
                Name = message.Name,
                MessageState = MessageState.Created,
                Transactions = new List<Transaction>(),
                HeaderFields = new List<HeaderField>()
            };

            foreach (HeaderCondition headerCondition in fileSpecification.HeaderConditions)
            {
                HeaderField headerField = new HeaderField()
                {
                    HeaderFieldCode = headerCondition.HeaderFieldCode,
                    Description = headerCondition.Description,
                    HeaderCondition = headerCondition,
                    Message = answerMessage
                };

                if (String.IsNullOrEmpty(headerField.Description))
                {
                    IEnumerable<HeaderField> headerFields = message.HeaderFields.Where(hf => hf.HeaderFieldCode.Equals(headerField.HeaderFieldCode));
                    if (headerFields.Count() == 1)
                    {
                        headerField.Description = headerFields.Single().Description;
                    }
                }

                answerMessage.HeaderFields.Add(headerField);
            }

            foreach (Transaction transaction in message.Transactions)
            {
                answerMessage.Transactions.Add(new Transaction()
                {
                    Message = answerMessage,
                    Name = transaction.Name,
                    Groups = new List<Group>()
                });
            }

            foreach (GroupCondition groupCondition in fileSpecification.GroupConditions)
            {
                foreach (Group groupInList in message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.Code)))
                {
                    Group group = new Group()
                    {
                        GroupCode = groupCondition.Code,
                        Transaction = answerMessage.Transactions.ElementAt(0),
                        Level = groupCondition.Level,
                        GroupCondition = groupCondition,
                        Fields = new List<Field>(),
                    };

                    if (!group.GroupCode.Equals("A01"))
                    {
                        group.ParentGroup = answerMessage.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.ParentGroup)).Last();
                       
                    }

                    foreach (FileSpecFieldCondition fileSpecFieldCondition in groupInList.GroupCondition.FileSpecFieldConditions)
                    {
                        if (Int32.Parse(fileSpecFieldCondition.Group.MinimumAmountOfOccurences) > 0 || groupInList.Fields.Count() > 0)
                        {
                            if ((fileSpecFieldCondition.IsOptional == true && groupInList.Fields.Where(f => f.FieldCode.Equals(fileSpecFieldCondition.Code)).Count() > 0) || fileSpecFieldCondition.IsOptional == false)
                            {
                                Field field = new Field()
                                {
                                    FieldCode = fileSpecFieldCondition.Code,
                                    Level = Convert.ToString(fileSpecFieldCondition.Level),
                                    Group = group,
                                    FileSpecFieldCondition = fileSpecFieldCondition,
                                    Value = groupInList.Fields.Where(f => f.FieldCode == fileSpecFieldCondition.Code).FirstOrDefault().Value
                                };

                                group.Fields.Add(field);
                            }
                        }
                       
                    }
                    answerMessage.Transactions.ElementAt(0).Groups.Add(group);
                }
            }

            return answerMessage;
        }
    }
}
