﻿using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using FIS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FIS.BL
{
    class OperationalManager : IOperationalManager
    {
        IOperationalRepository operationalRep;
        ISpecificationSetupManager specFieldManager;

        OperationalManager()
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
