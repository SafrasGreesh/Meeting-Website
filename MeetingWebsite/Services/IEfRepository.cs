using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingWebsite.Entity;

namespace MeetingWebsite.Services
{
    public interface IEfRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(int? id);
        Task<int?> Add(T entity);
        Task UserUpdate(int id, T entity);
        Task OptionsUpdate(int id, T entity);
        List<T> GetAllEvents();
        T GetEventById(int? id);
    }
}