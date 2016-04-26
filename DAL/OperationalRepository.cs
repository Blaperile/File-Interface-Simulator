using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Operational;
using FIS.DAL.EF;

namespace FIS.DAL
{
    public class OperationalRepository : IOperationalRepository
    {
        private readonly FISEFDbContext ctx;

        public OperationalRepository()
        {
            ctx = FISEFDbContext.Instance;
        }

        public Message CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Message ReadMessage(int messageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> ReadMessages()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> ReadMessagesOfFileSpecification(int specificationId)
        {
            throw new NotImplementedException();
        }

        public Message UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Message DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }

       /* public Element CreateElement(Element element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Element> GetElements(int messageId)
        {
            throw new NotImplementedException();
        }*/

        public Workflow CreateWorkflow(Workflow workflow)
        {
            throw new NotImplementedException();
        }

        public Workflow UpdateWorkflow(Workflow workflow)
        {
            throw new NotImplementedException();
        }

        public Workflow ReadWorkflow(int workflowId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workflow> ReadWorkflows()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Workflow> ReadWorkflowsForTemplate(int workflowTemplateId)
        {
            throw new NotImplementedException();
        }

        public Workflow DeleteWorkflow(int workflowId)
        {
            throw new NotImplementedException();
        }
    }
}
