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
        public ActionResult MessageDetail(int id = 2)
        {
            Message message = operationalManager.GetMessage(id);

            MessageDetailViewModel model = new MessageDetailViewModel()
            {
                CreationDate = message.Date,
                MessageId = id.ToString(),
                MessageState = message.MessageState.ToString(),
                SpecificationFile = message.FileSpecification.Name,
                Type = message.FileSpecification.IsInput? "Input" : "Output",
                HeaderFields = new List<MessageHeaderFieldDetailViewModel>(),
                Transactions = new List<MessageTransactionDetailViewModel>()
            };

            foreach (HeaderField headerField in message.HeaderFields)
            {
                model.HeaderFields.Add(new MessageHeaderFieldDetailViewModel()
                {
                    Code = headerField.HeaderFieldCode,
                    Content = headerField.Description,
                    Datatype = headerField.HeaderCondition.Datatype,
                    Size = headerField.HeaderCondition.Size,
                    ErrorMessage = headerField.ErrorDescription
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
                        transactionModel.Fields.Add(new MessageFieldDetailViewModel()
                        {
                            Code = field.FieldCode,
                            Datatype = field.FileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                            Format = field.FileSpecFieldCondition.FieldSpecFieldCondition.Format.Count() > 0 ? field.FileSpecFieldCondition.FieldSpecFieldCondition.Format : "-",
                            Group = group.GroupCode,
                            Level = field.Level,
                            Name = field.FileSpecFieldCondition.Description,
                            Size = field.FileSpecFieldCondition.FieldSpecFieldCondition.Size,
                            Optional = field.FileSpecFieldCondition.IsOptional ? "O" : "M",
                            Value = field.Value
                        });
                    }
                }

                model.Transactions.Add(transactionModel);
            }

            return View("MessageDetail", model);
        }
    }
}