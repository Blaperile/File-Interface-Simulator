﻿using File_Interface_Simulator.Models;
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
                Transactions = new List<MessageTransactionDetailViewModel>(),
                AmountOfHeaderErrors = 0,
                AmountOfErrors = 0
            };

            foreach (HeaderField headerField in message.HeaderFields)
            {
                MessageHeaderFieldDetailViewModel headerFieldModel = new MessageHeaderFieldDetailViewModel()
                {
                    Code = headerField.HeaderFieldCode,
                    Content = headerField.Description,
                    Datatype = headerField.HeaderCondition.Datatype,
                    Size = headerField.HeaderCondition.Size,
                    ErrorMessage = headerField.ErrorDescription
                };

                if (!String.IsNullOrEmpty(headerFieldModel.ErrorMessage))
                {
                    model.AmountOfHeaderErrors++;
                    model.AmountOfErrors++;
                }

                model.HeaderFields.Add(headerFieldModel);
             
            }

            foreach (Transaction transaction in message.Transactions)
            {
                MessageTransactionDetailViewModel transactionModel = new MessageTransactionDetailViewModel()
                {
                     AmountOfFieldErrors = 0,
                    Fields = new List<MessageFieldDetailViewModel>(),
                    AmountOfGroupErrors = 0,
                    Groups = new List<MessageGroupDetailViewModel>(),
                };

                foreach (Group group in transaction.Groups)
                {
                    MessageGroupDetailViewModel groupModel = new MessageGroupDetailViewModel()
                    {
                        Code = group.GroupCode,
                        AmountOfFields = group.Fields.Count(),
                        Count = transaction.Groups.Where(g => g.GroupCode.Equals(group.GroupCode)).Count(),
                        Description = group.GroupCondition.Description,
                        Level = group.Level,
                        ErrorMessage = group.ErrorDescription
                    };

                    if(!String.IsNullOrEmpty(groupModel.ErrorMessage))
                    {
                        transactionModel.AmountOfGroupErrors++;
                        model.AmountOfErrors++;
                    }

                    transactionModel.Groups.Add(groupModel);

                    

                    foreach (Field field in group.Fields)
                    {
                        MessageFieldDetailViewModel fieldModel = new MessageFieldDetailViewModel()
                        {
                            Code = field.FieldCode,
                            Datatype = field.FileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                            Format = field.FileSpecFieldCondition.FieldSpecFieldCondition.Format.Count() > 0 ? field.FileSpecFieldCondition.FieldSpecFieldCondition.Format : "-",
                            Group = group.GroupCode,
                            Level = field.Level,
                            Name = field.FileSpecFieldCondition.Description,
                            Size = field.FileSpecFieldCondition.FieldSpecFieldCondition.Size,
                            Optional = field.FileSpecFieldCondition.IsOptional ? "O" : "M",
                            Value = field.Value,
                            ErrorMessage = field.ErrorDescription
                        };

                        if (!String.IsNullOrEmpty(fieldModel.ErrorMessage))
                        {
                            transactionModel.AmountOfFieldErrors++;
                            model.AmountOfErrors++;
                        }

                        transactionModel.Fields.Add(fieldModel);
                    }
                }

                model.Transactions.Add(transactionModel);
            }

            return View("MessageDetail", model);
        }
    }
}