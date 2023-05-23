using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingWebsite.Entity;

namespace MeetingWebsite.Services
{
    public interface IEfRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T GetById(string? id);
        Task<string> Add(T entity);
        Task UserUpdate(string id, T entity);
        Task OptionsUpdate(int id, T entity);
    }
}