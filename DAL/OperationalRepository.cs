using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Operational;
using FIS.DAL.EF;
using FIS.BL.Domain.Setup;
using System.Xml;
using System.Data.Entity;

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
            ctx.Messages.Add(message);
            ctx.SaveChanges();
            return message;
        }

        public Message ReadMessage(int messageId)
        {
            return ctx.Messages.Find(messageId);
        }

        public Message ReadMessageWithRelatedData(int messageId)
        {
            Message message = ReadMessage(messageId);
            ctx.Entry<Message>(message).Reference<FileSpecification>(m => m.FileSpecification).Load();
            LoadHeaderFields(message);
            LoadTransactions(message);
            return message;
        }

        private void LoadHeaderFields(Message message)
        {
            ctx.Entry<Message>(message).Collection<HeaderField>(m => m.HeaderFields).Load();

            foreach (HeaderField headerField in message.HeaderFields)
            {
                ctx.Entry<HeaderField>(headerField).Reference<HeaderCondition>(hf => hf.HeaderCondition).Load();
            }
        }

        private void LoadTransactions(Message message)
        {
            ctx.Entry<Message>(message).Collection<Transaction>(m => m.Transactions).Load();

            foreach(Transaction transaction in message.Transactions)
            {
               LoadGroups(transaction);
            }
        }

        private void LoadGroups(Transaction transaction)
        {
            ctx.Entry<Transaction>(transaction).Collection<Group>(t => t.Groups).Load();

            foreach (Group group in transaction.Groups)
            {
                ctx.Entry<Group>(group).Reference<GroupCondition>(g => g.GroupCondition).Load();
                LoadFields(group);
            }
        }

        private void LoadFields(Group group)
        {
            ctx.Entry<Group>(group).Collection<Field>(g => g.Fields).Load();

            foreach (Field field in group.Fields)
            {
                ctx.Entry<Field>(field).Reference<FileSpecFieldCondition>(f => f.FileSpecFieldCondition).Load();
                ctx.Entry<FileSpecFieldCondition>(field.FileSpecFieldCondition).Reference<FieldSpecFieldCondition>(fsfc => fsfc.FieldSpecFieldCondition).Load();
            }
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
            ctx.Messages.Attach(message);
            ctx.Entry(message).State = EntityState.Modified;
            ctx.SaveChanges();
            return message;
        }

        public Message DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
        }

       public IEnumerable<IElement> CreateElements(IEnumerable<IElement> elements)
        {
            foreach(XMLElement element in elements)
            {
                ctx.XmlElements.Add(element);   
            }
            ctx.SaveChanges();
            return elements;
        }

        public IEnumerable<XMLElement> GetElements(int messageId)
        {
            return ctx.XmlElements.Where(e => e.Message.MessageId == messageId).ToList();
        }

        public Workflow CreateWorkflow(Workflow workflow)
        {
            ctx.Workflows.Add(workflow);
            ctx.SaveChanges();
            return workflow;
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
