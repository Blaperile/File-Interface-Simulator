using File_Interface_Simulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIS.BL;
using FIS.BL.Domain.Operational;

namespace File_Interface_Simulator.Controllers
{
    public class OperationalController : Controller
    {
        private readonly IOperationalManager operationalManager = new OperationalManager();

        [HttpGet]
        public ActionResult MessageDetail(int id = 1)
        {
            Message message = operationalManager.GetMessage(id);

            MessageDetailViewModel model = new MessageDetailViewModel()
            {
                CreationDate = message.Date,
                MessageId = id.ToString(),
                MessageState = message.MessageState.ToString(),
                SpecificationFile = message.FileSpecification.Name,
                HeaderFields = new List<MessageHeaderFieldDetailViewModel>(),
                Transactions = new List<MessageTransactionDetailViewModel>()
            };

            if (message.FileSpecification.IsInput)
            {
                model.Type = "Input";
            }
            else
            {
                model.Type = "Output";
            }

            foreach (HeaderField headerField in message.HeaderFields)
            {
                model.HeaderFields.Add(new MessageHeaderFieldDetailViewModel()
                {
                    Code = headerField.HeaderFieldCode,
                    Content = headerField.Description,
                    Datatype = headerField.HeaderCondition.Datatype,
                    Size = headerField.HeaderCondition.Size
                });
            }

            foreach (Transaction transaction in message.Transactions)
            {
                MessageTransactionDetailViewModel transactionModel = new MessageTransactionDetailViewModel()
                {
                    Fields = new List<MessageFieldDetailViewModel>(),
                    Groups = new List<MessageGroupDetailViewModel>()
                };

                foreach (Group group in transaction.Groups)
                {
                    transactionModel.Groups.Add(new MessageGroupDetailViewModel()
                    {
                        Code = group.GroupCode,
                        AmountOfFields = group.Fields.Count(),
                        Count = transaction.Groups.Where(g => g.GroupCode.Equals(group.GroupCode)).Count(),
                        Description = group.GroupCondition.Description,
                        Level = group.Level
                    });

                    foreach (Field field in group.Fields)
                    {
                        MessageFieldDetailViewModel fieldModel = new MessageFieldDetailViewModel()
                        {
                            Code = field.FieldCode,
                            Datatype = field.FileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                            Format = field.FileSpecFieldCondition.FieldSpecFieldCondition.Format,
                            Group = group.GroupCode,
                            Level = field.Level,
                            Name = field.FileSpecFieldCondition.Description,
                            Size = field.FileSpecFieldCondition.FieldSpecFieldCondition.Size,
                            Value = field.Value
                        };

                        if (field.FileSpecFieldCondition.IsOptional)
                        {
                            fieldModel.Optional = "O";
                        }
                        else
                        {
                            fieldModel.Optional = "M";
                        }

                        transactionModel.Fields.Add(fieldModel);
                    }
                }
            }

            return View("MessageDetail", model);
        }
    }
}