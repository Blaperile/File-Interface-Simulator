using FIS.BL.Domain.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.DAL
{
    public interface IOperationalRepository
    {
        Message CreateMessage(Message message);
        Message ReadMessage(int messageId);
        IEnumerable<Message> ReadMessages();
        IEnumerable<Message> ReadMessagesOfFileSpecification(int specificationId);
        Message UpdateMessage(Message message);
        Message DeleteMessage(int messageId);
        IEnumerable<IElement> CreateElements(IEnumerable<IElement> element);
        IEnumerable<IElement> GetElements(int messageId);
        Workflow CreateWorkflow(Workflow workflow);
        Workflow UpdateWorkflow(Workflow workflow);
        Workflow ReadWorkflow(int workflowId);
        IEnumerable<Workflow> ReadWorkflows();
        IEnumerable<Workflow> ReadWorkflowsForTemplate(int workflowTemplateId);
        Workflow DeleteWorkflow(int workflowId);
    }
}
