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
        public ActionResult MessageOverview()
        {
            IEnumerable<Message> messages = operationalManager.GetMessages();
            ICollection<MessageOverviewDetailViewModel> model = new List<MessageOverviewDetailViewModel>();

            foreach (Message message in messages)
            {
                MessageOverviewDetailViewModel messageModel = new MessageOverviewDetailViewModel()
                {
                    Id = message.MessageId,
                    Name = message.Name,
                    MessageState = message.MessageState.ToString(),
                    CreationDate = message.Date,
                    AmountOfErrors = message.AmountOfErrors
                };

                if (message.FileSpecification != null) {
                    messageModel.Type = message.FileSpecification.IsInput ? "Input" : "Output";
                }

                model.Add(messageModel);
            }

            return View("MessageOverview", model);
        }

        public HttpStatusCodeResult RemoveMessageRPC(int id)
        {
            try
            {
                Message message = operationalManager.RemoveMessage(id);
                return new HttpStatusCodeResult(200, "Succes");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult MessageDetail(int id = 1)
        {
            try
            {
                Message message = operationalManager.GetMessageWithRelatedData(id);

                MessageDetailViewModel model = new MessageDetailViewModel()
                {
                    CreationDate = message.Date,
                    MessageId = id.ToString(),
                    MessageState = message.MessageState.ToString(),
                    HeaderFields = new List<MessageHeaderFieldDetailViewModel>(),
                    Transactions = new List<MessageTransactionDetailViewModel>(),
                    HeaderError = message.HeaderErrorDescription,
                    AmountOfHeaderErrors = 0,
                    AmountOfErrors = message.AmountOfErrors
                };

                if(message.FileSpecification != null)
                {
                    model.SpecificationFile = message.FileSpecification.Name;
                    model.Type = message.FileSpecification.IsInput ? "Input" : "Output";
                }

                if (!String.IsNullOrEmpty(model.HeaderError))
                {
                    model.AmountOfHeaderErrors++;
                }

                foreach (HeaderField headerField in message.HeaderFields)
                {
                    MessageHeaderFieldDetailViewModel headerFieldModel = new MessageHeaderFieldDetailViewModel()
                    {
                        Code = headerField.HeaderFieldCode,
                        Content = headerField.Description,
                        Datatype = headerField.HeaderCondition.Datatype,
                        Size = headerField.HeaderCondition.Size.ToString(),
                        ErrorMessage = headerField.ErrorDescription
                    };

                    if (!String.IsNullOrEmpty(headerFieldModel.ErrorMessage))
                    {
                        model.AmountOfHeaderErrors++;
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
                        GroupsErrorMessage = transaction.GroupsErrorDescription,
                        Groups = new List<MessageGroupDetailViewModel>(),
                    };

                    foreach (Group group in transaction.Groups)
                    {
                        MessageGroupDetailViewModel groupModel = new MessageGroupDetailViewModel()
                        {
                            Id = group.GroupId,
                            Code = group.GroupCode,
                            AmountOfFields = group.Fields.Count(),
                            Count = transaction.Groups.Where(g => g.GroupCode.Equals(group.GroupCode)).Count(),
                            Description = group.GroupCondition.Description,
                            Level = group.Level,
                            ErrorMessage = group.ErrorDescription
                        };

                        if (!String.IsNullOrEmpty(groupModel.ErrorMessage))
                        {
                            transactionModel.AmountOfGroupErrors++;
                        }

                        transactionModel.Groups.Add(groupModel);



                        foreach (Field field in group.Fields)
                        {
                            MessageFieldDetailViewModel fieldModel = new MessageFieldDetailViewModel()
                            {
                                Id = field.FieldId,
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
                            }

                            transactionModel.Fields.Add(fieldModel);
                        }
                    }

                    model.Transactions.Add(transactionModel);
                }

                return View("MessageDetail", model);
            } catch (Exception ex)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    ErrorMessage = ex.Message
                };

                return RedirectToAction("Error", "Home", model);
            }
        }

        public ActionResult GroupDetail(int id = 1)
        {
            try
            {
                Group group = operationalManager.GetGroupWithRelatedDate(id);
                GroupDetailViewModel groupDetailModel = new GroupDetailViewModel
                {
                    Code = group.GroupCode,
                    Description = group.GroupCondition.Description,
                    Range = group.GroupCondition.MinimumAmountOfOccurences + "-" + group.GroupCondition.MaximumAmountOfOccurences,
                    Level = group.Level,
                    ErrorMessage = group.ErrorDescription,
                    AmountOfErrorMessages = group.Fields.Where(f => f.ErrorDescription != null).Count(),
                    AmountOfFields = group.Fields.Count(),
                    Transactie = "T1",/*group.Transaction.Name,*/
                    Fields = new List<MessageFieldDetailViewModel>()
                };
                foreach (Field field in group.Fields)
                {
                    MessageFieldDetailViewModel fieldDetailModel = new MessageFieldDetailViewModel
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
                    groupDetailModel.Fields.Add(fieldDetailModel);
                }

                return View("GroupDetail", groupDetailModel);

            } catch (Exception e)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    ErrorMessage = e.Message
                };

                return RedirectToAction("Error", "Home", model);
            }
        }

        public ActionResult FieldDetail(int id = 1)
        {
            Field field = operationalManager.GetFieldWithRelatedData(id);
            MessageFieldDetailViewModel model = new MessageFieldDetailViewModel
            {
                Code = field.FieldCode,
                Datatype = field.FileSpecFieldCondition.FieldSpecFieldCondition.Datatype,
                Format = field.FileSpecFieldCondition.FieldSpecFieldCondition.Format.Count() > 0 ? field.FileSpecFieldCondition.FieldSpecFieldCondition.Format : "-",
                Group = "G1", //field.Group.GroupCode,
                Level = field.Level,
                Name = field.FileSpecFieldCondition.Description,
                Size = field.FileSpecFieldCondition.FieldSpecFieldCondition.Size,
                Optional = field.FileSpecFieldCondition.IsOptional ? "O" : "M",
                Value = field.Value,
                ErrorMessage = field.ErrorDescription
            };

            return View("FieldDetail", model);
        }

        [HttpGet]
        public ActionResult WorkflowOverview()
        {
            IEnumerable<Workflow> workflows = operationalManager.GetWorkflows();

            ICollection<WorkflowOverviewDetailViewModel> model = new List<WorkflowOverviewDetailViewModel>();

            foreach (Workflow workflow in workflows)
            {
                WorkflowOverviewDetailViewModel workflowModel = new WorkflowOverviewDetailViewModel()
                {
                    Id = workflow.WorkflowId,
                    CreationDate = workflow.Date,
                    TemplateName = workflow.WorkflowTemplate.Name,
                    IsFinished = workflow.IsFinished,
                    IsSuccesfull = workflow.IsSuccessful,
                    AmountOfErrors = 0
                };

                foreach (Message message in workflow.Messages)
                {
                    workflowModel.AmountOfErrors += message.AmountOfErrors;
                }

                model.Add(workflowModel);
            }

            return View("WorkflowOverview", model);
        }

        public HttpStatusCodeResult RemoveWorkflowRPC(int id)
        {
            Workflow workflow = operationalManager.RemoveWorkflow(id);

            if (workflow != null)
            {
                return new HttpStatusCodeResult(200, "Succes");
            }

            return new HttpStatusCodeResult(500, "An error occurred while deleting the workflow.");
        }

        [HttpGet]
        public ActionResult WorkflowDetail(int id = 1)
        {
            try
            {
                Workflow workflow = operationalManager.GetWorkflow(id);

                WorkflowDetailViewModel model = new WorkflowDetailViewModel()
                {
                    Id = workflow.WorkflowId,
                    CreationDate = workflow.Date,
                    TemplateWorkflow = workflow.WorkflowTemplate.Name,
                    ErrorCount = 0,
                    Successful = workflow.IsSuccessful ? "Yes" : "No",
                    Messages = new List<WorkflowDetailMessageDetailViewModel>()
                };

                foreach (Message message in workflow.Messages)
                {
                    model.ErrorCount += message.AmountOfErrors;

                    model.Messages.Add(new WorkflowDetailMessageDetailViewModel()
                    {
                        Name = message.Name,
                        CreationDate = message.Date,
                        Type = message.FileSpecification.IsInput ? "Input" : "Output",
                        MessageState = message.MessageState.ToString(),
                        ErrorCount = message.AmountOfErrors
                    });
                }

                return View("WorkflowDetail", model);
            } catch (Exception ex)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    ErrorMessage = ex.Message
                };

                return RedirectToAction("Error", "Home", model);
            }
        }

    }
}