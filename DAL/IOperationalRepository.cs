using FIS.BL.Domain.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FIS.DAL
{
    public interface IOperationalRepository
    {
        Message CreateMessage(Message message);
        Message ReadMessage(int messageId);
        Message ReadMessageWithRelatedData(int messageId);
        List<Message> ReadMessages();
        IEnumerable<Message> ReadMessagesOfFileSpecification(int specificationId);
        Message UpdateMessage(Message message);
        Message DeleteMessage(int messageId);
        Group ReadGroupWithRelatedDate(int groupId);
        Field ReadFieldWithRelatedData(int fieldId);
        IEnumerable<IElement> CreateElements(IEnumerable<IElement> element);
        IEnumerable<XMLElement> GetElements(int messageId);
        Workflow CreateWorkflow(Workflow workflow);
        Workflow UpdateWorkflow(Workflow workflow);
        Workflow ReadWorkflow(int workflowId);
        List<Workflow> ReadWorkflows();
        IEnumerable<Workflow> ReadWorkflowsForTemplate(int workflowTemplateId);
        Workflow DeleteWorkflow(int workflowId);
    }
}
