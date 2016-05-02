using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using FIS.BL.Util;
using FIS.BL.Util.XML;
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
        ISpecificationSetupManager specFieldManager;

        public OperationalManager()
        {
            operationalRep = new OperationalRepository();
            specFieldManager = new SpecificationSetupManager();
        }

        public Workflow AddWorkflow(Message message)
        {
            throw new NotImplementedException();
        }

        public void ArchiveErrorLines()
        {
            throw new NotImplementedException();
        }

        public void DetectInput()
        {
            List<Directory> directories = specFieldManager.GetInputDirectories();
            DirectoryHandler directoryHandler = new DirectoryHandler();
            XMLParser xmlParser = new XMLParser();
            foreach(Directory currentDirectory in directories)
            {
                IEnumerable<String> filenames = directoryHandler.GetFileNamesOfType("xml", currentDirectory);
                foreach(String filename in filenames)
                {
                    String content = directoryHandler.GetContentOfFile(filename, currentDirectory);
                    IEnumerable <IElement> elements = xmlParser.GetElements(content);
                    operationalRep.CreateElements(elements);
                }
            }
        
        }

        public void GenerateAnswer(Message message, Workflow workflow, WorkflowTemplate workflowTemplate)
        {
            throw new NotImplementedException();
        }

        public Message GetMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessages()
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessagesOfFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public Workflow GetWorkflow(int workflowId)
        {
            throw new NotImplementedException();
        }

        public List<Workflow> GetWorkflows()
        {
            throw new NotImplementedException();
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

        public void ValidateInput(int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
