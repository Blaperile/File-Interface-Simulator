using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public interface IOperationalManager
    {
        Message GetMessageWithRelatedData(int messageId);
        List<Message> GetMessages();
        List<Message> GetMessagesOfFileSpecification(int specificationId);
        Message RemoveMessage(int messageId);
        Workflow AddWorkflow(Message message);
        Workflow GetWorkflow(int workflowId);
        List<Workflow> GetWorkflows();
        List<Workflow> GetWorkflowsForTemplate(int workflowTemplateId);
        Workflow RemoveWorkflow(int workflowId);
        void DetectInput();
        void ValidateInput(int messageId);
        void ArchiveErrorLines(Message message, FileSpecification fileSpecification, IEnumerable<String> codes);
        void GenerateAnswer(Message message, Workflow workflow, WorkflowTemplate workflowTemplate);

    }
}
