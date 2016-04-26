using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BL
{
    class OperationalManager : IOperationalManager
    {
        public Workflow addWorkflow(Message message)
        {
            throw new NotImplementedException();
        }

        public void archiveErrorLines()
        {
            throw new NotImplementedException();
        }

        public void detectInput()
        {
            throw new NotImplementedException();
        }

        public void generateAnswer(Message message, Workflow workflow, WorkflowTemplate workflowTemplate)
        {
            throw new NotImplementedException();
        }

        public Message getMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessages()
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessagesOfFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public Workflow getWorkflow(int workflowId)
        {
            throw new NotImplementedException();
        }

        public List<Workflow> getWorkflows()
        {
            throw new NotImplementedException();
        }

        public List<Workflow> getWorkflowsForTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public Message removeMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public Workflow removeWorkflow(int workflowId)
        {
            throw new NotImplementedException();
        }

        public void validateInput(int messageId)
        {
            throw new NotImplementedException();
        }
    }
}
