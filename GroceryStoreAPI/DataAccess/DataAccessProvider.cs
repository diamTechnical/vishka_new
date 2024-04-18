using GroceryStoreAPI.Model;
using System.Threading;

namespace GroceryStoreAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public void AddConsumerRecord(Consumer consumer)
        {
            _context.consumers.Add(consumer);
            _context.SaveChanges();
        }

        public void UpdateConsumerRecord(Consumer consumer)
        {
            _context.consumers.Update(consumer);
            _context.SaveChanges();
        }

        public void DeleteConsumerRecord(int id)
        {
            var entity = _context.consumers.FirstOrDefault(t => t.id == id);
            _context.consumers.Remove(entity);
            _context.SaveChanges();
        }

        public Consumer GetConsumerSingleRecord(int id)
        {
            return _context.consumers.FirstOrDefault(t => t.id == id);
        }

        public List<Consumer> GetConsumerSearch(string task_name, string tag, string status)
        {
            if (!String.IsNullOrEmpty(task_name))
                return _context.consumers.Where(p => p.task_record.task_name == task_name).ToList();
            else if (!String.IsNullOrEmpty(tag))
                return _context.consumers.Where(p => p.task_record.tags.tagName == tag).ToList();
            else if (!String.IsNullOrEmpty(status))
                return _context.consumers.Where(p => p.task_record.status == status).ToList();
            else return null;
        }

        public List<Consumer> GetConsumerRecords()
        {
            return _context.consumers.ToList();
        }
    }
}
