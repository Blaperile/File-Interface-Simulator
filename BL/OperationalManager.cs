using FIS.BL.Domain.Operational;
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

        public OperationalManager()
        {
            operationalRep = new OperationalRepository();
            specSetupManager = new SpecificationSetupManager();
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
                contentOfErrorFile += code + Environment.NewLine;
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
                    Workflow workflow = AddWorkflow(message, fileSpecification.WorkflowTemplate);
                    ValidateInput(message.MessageId, fileSpecification.FileSpecificationId);
                }
            }
        
        }

        public void GenerateAnswer(Message message, Workflow workflow, WorkflowTemplate workflowTemplate)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Message RemoveMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public Workflow RemoveWorkflow(int workflowId)
        {
            throw new NotImplementedException();
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
