using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingWebsite.Entity;

namespace MeetingWebsite.Services
{
    public interface IEfRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        List<T> GetAllEvents();
        T GetById(int? id);
        T GetEventById(int? id);
        Task<int?> Add(T entity);
        
    }
}