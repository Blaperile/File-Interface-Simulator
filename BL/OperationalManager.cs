﻿using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using FIS.BL.Util;
using FIS.BL.Util.XML;
using FIS.BL.Util.XML.Validation;
using FIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public class OperationalManager : IOperationalManager
    {
        IOperationalRepository operationalRep;
        ISpecificationSetupManager specSetupManager;
        IWorkflowTemplateSetupManager workflowTemplateSetupManager;

        public OperationalManager()
        {
            operationalRep = new OperationalRepository();
            specSetupManager = new SpecificationSetupManager();
            workflowTemplateSetupManager = new WorkflowTemplateSetupManager();
        }

        public Workflow AddWorkflow(Message message, WorkflowTemplate workflowTemplate)
        {
            Workflow workflow = new Workflow()
            {
                Date = DateTime.Now,
                IsSuccessful = false,
                Messages = new List<Message>(),
                WorkflowTemplate = workflowTemplate
            };
            
            if (workflowTemplate.Workflows == null)
            {
                workflowTemplate.Workflows = new List<Workflow>();
            }

            workflowTemplate.Workflows.Add(workflow);

            workflow.Messages.Add(message);

            message.Workflow = workflow;

            return operationalRep.CreateWorkflow(workflow);
        }

        public void ArchiveErrorLines(Message message, FileSpecification fileSpecification, IEnumerable<String> codes)
        {
            DirectoryHandler directoryHandler = new DirectoryHandler();
            string contentOfErrorFile = "";
            foreach (string code in codes)
            {
                contentOfErrorFile += String.Format("{0} is an element not recognized by the specification. {1}", code, Environment.NewLine);
            }
            directoryHandler.CreateFile(message.Name + ".txt", contentOfErrorFile, fileSpecification.Directories.Where(d => d.Name.Equals("error")).First());
        }

        public void DetectInput()
        {
            List<Directory> directories = specSetupManager.GetInputDirectories();
            DirectoryHandler directoryHandler = new DirectoryHandler();
            XMLParser xmlParser = new XMLParser();
            foreach(Directory currentDirectory in directories)
            {
                IEnumerable<String> filenames = directoryHandler.GetFileNamesOfType("xml", currentDirectory);
                foreach(String filename in filenames)
                {
                    String content = directoryHandler.GetContentOfFile(filename, currentDirectory);
                    Message message = new Message();
                    message.Date = DateTime.Now;
                    message.Name = filename;
                    message.MessageState = MessageState.Created;
                    IEnumerable <IElement> elements = xmlParser.GetElements(message, content);
                    elements = operationalRep.CreateElements(elements);

                    IEnumerable<XMLElement> xmlElements = operationalRep.GetElements(message.MessageId);
                    XMLElement flowIdElement = xmlElements.Where(e => e.Code.Equals("FLOWID")).Single();
                    FileSpecification fileSpecification = specSetupManager.GetFileSpecificationAtStartWorkflowTemplateWithName(flowIdElement.Value);
                    if (fileSpecification != null)
                    {
                        WorkflowTemplate workflowTemplate = fileSpecification.WorkflowTemplateSteps.Where(wt => wt.StepNumber == 1).Where(wt => wt.WorkflowTemplate.IsChosen == true).FirstOrDefault().WorkflowTemplate;

                        WorkflowTemplateStep workflowTemplateStep = workflowTemplateSetupManager.GetWorkflowTemplateStep(workflowTemplate.WorkflowTemplateId, 2);

                        Workflow workflow = AddWorkflow(message, workflowTemplate);
                        ValidateInput(message.MessageId, fileSpecification.FileSpecificationId);
                        GenerateAnswer(message, workflow, workflowTemplateStep, directoryHandler);
                    }
                }
            }
        
        }

        public void GenerateAnswer(Message message, Workflow workflow, WorkflowTemplateStep workflowTemplateStep, DirectoryHandler directoryHandler)
        {
            FileSpecification outputFileSpecification = workflowTemplateStep.fileSpecification; 

            IAnswerGenerator answerGenerator = new AnswerGenerator();
            Message answerMessage = answerGenerator.GenerateAnswer(message, outputFileSpecification);

            IXMLGenerator xmlGenerator = new XMLGenerator();
            string answerXmlString = xmlGenerator.GenerateXmlString(answerMessage);

            Directory outDirectory = outputFileSpecification.Directories.Where(d => d.Name.Equals("out")).Single();

            directoryHandler.CreateFile(answerMessage.Name, answerXmlString, outDirectory);

            answerMessage.Workflow = workflow;
            workflow.Messages.Add(answerMessage);

            operationalRep.UpdateWorkflow(workflow);
           
        }

        public Message GetMessage(int messageId)
        {
            return operationalRep.ReadMessage(messageId);
        }

        public Message GetMessageWithRelatedData(int messageId)
        {
            return operationalRep.ReadMessageWithRelatedData(messageId);
        }

        public List<Message> GetMessages()
        {
            return operationalRep.ReadMessages();
        }

        public List<Message> GetMessagesOfFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public Workflow GetWorkflow(int workflowId)
        {
            return operationalRep.ReadWorkflow(workflowId);
        }

        public List<Workflow> GetWorkflows()
        {
            return operationalRep.ReadWorkflows();
        }

        public List<Workflow> GetWorkflowsForTemplate(int workflowTemplateId)
        {
            return operationalRep.ReadWorkflowsForTemplate(workflowTemplateId).ToList();
        }

        public Message RemoveMessage(int messageId)
        {
            return operationalRep.DeleteMessage(messageId);
        }

        public Workflow RemoveWorkflow(int workflowId)
        {
            return operationalRep.DeleteWorkflow(workflowId);
        }

        public void ValidateInput(int messageId, int fileSpecificationId)
        {
            FileSpecification fileSpecification = specSetupManager.GetFileSpecification(fileSpecificationId);
            IEnumerable<XMLElement> elements = operationalRep.GetElements(messageId);
            Message message = GetMessage(messageId);
            XMLValidator validator = new XMLValidator(elements, fileSpecification, message);
            message.FileSpecification = fileSpecification;
            fileSpecification.Messages.Add(message);
            ArchiveErrorLines(message, fileSpecification, validator.Codes);
            operationalRep.UpdateMessage(message);
        }

        public Group GetGroupWithRelatedDate(int groupId)
        {
            return operationalRep.ReadGroupWithRelatedDate(groupId);
        }

        public Field GetFieldWithRelatedData(int fieldId)
        {
            return operationalRep.ReadFieldWithRelatedData(fieldId);
        }
    }
}
