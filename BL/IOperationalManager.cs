using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    interface IOperationalManager
    {
        Message getMessage(int messageId);
        List<Message> getMessages();
        List<Message> getMessagesOfFileSpecification(int specificationId);
        Message removeMessage(int messageId);
        Workflow addWorkflow(Message message);
        Workflow getWorkflow(int workflowId);
        List<Workflow> getWorkflows();
        List<Workflow> getWorkflowsForTemplate(int workflowTemplateId);
        Workflow removeWorkflow(int workflowId);
        void detectInput();
        void validateInput(int messageId);
        void archiveErrorLines();
        void generateAnswer(Message message, Workflow workflow, WorkflowTemplate workflowTemplate);

    }
}
