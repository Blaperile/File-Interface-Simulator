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
                foreach (Group groupInList in message.Transactions.ElementAt(0).Groups.Where(g => g.Level.Equals(groupCondition.Level)))
                {
                    Group group = new Group()
                    {
                        GroupCode = groupCondition.Code,
                        Transaction = answerMessage.Transactions.ElementAt(0),
                        //  ParentGroup = groupCondition.ParentGroup,
                        Level = groupCondition.Level,
                        GroupCondition = groupCondition,
                        Fields = new List<Field>()
                    };

                    foreach (FileSpecFieldCondition fileSpecFieldCondition in groupCondition.FileSpecFieldConditions)
                    {
                        Field field = new Field()
                        {
                            FieldCode = fileSpecFieldCondition.Code,
                            Level = Convert.ToString(fileSpecFieldCondition.Level),
                            Group = group,
                            FileSpecFieldCondition = fileSpecFieldCondition,
                            Value = message.Transactions.ElementAt(0).Groups.Where(g => g.GroupId == groupInList.GroupId).FirstOrDefault().Fields.Where(f=>f.FieldCode == fileSpecFieldCondition.Code).FirstOrDefault().Value
                        };

                        group.Fields.Add(field);
                    }

                    answerMessage.Transactions.ElementAt(0).Groups.Add(group);
                }
            }

            return answerMessage;
        }
    }
}
