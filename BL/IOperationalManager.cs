using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using FIS.BL.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL
{
    public interface IOperationalManager
    {
        Message GetMessageWithWorkflow(int messageId);
        Message GetMessageWithRelatedData(int messageId);
        List<Message> GetMessages();
        Message RemoveMessage(int messageId);
        Group GetGroupWithRelatedDate(int groupId);
        Field GetFieldWithRelatedData(int fieldId);
        Workflow AddWorkflow(Message message, WorkflowTemplate workflowTemplate);
        Workflow GetWorkflow(int workflowId);
        Workflow GetWorkflowForMessage(int messageId);
        List<Workflow> GetWorkflows();
        List<Workflow> GetWorkflowsForTemplate(int workflowTemplateId);
        Workflow RemoveWorkflow(int workflowId);
        void DetectInput();
        void ValidateInput(int messageId, int fileSpecificationId);
        void ArchiveErrorLines(Message message, FileSpecification fileSpecification, IEnumerable<String> codes);
        void GenerateAnswer(Message message, Workflow workflow, WorkflowTemplateStep workflowTemplateStep, DirectoryHandler directoryHandler);

    }
}
