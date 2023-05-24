using MeetingWebsite.Entity;
using MeetingWebsite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingWebsite.Services
{
    public class UserRepository<T> : IEfRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;


        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int? id)
        {
            var result = _context.Set<T>().FirstOrDefault(x => x.Id == id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            return result;
        }

        public async Task<int?> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task UserUpdate(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id); // Найти существующую сущность по идентификатору

            if (existingEntity == null)
            {
                // Обработка случая, когда сущность с заданным идентификатором не найдена
                // Можно выбросить исключение или вернуть сообщение об ошибке
                // Например:
                throw new DllNotFoundException("Entity not found");
            }

            // Обновить свойства сущности с помощью значений из новой сущности
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync(); // Сохранить изменения в базе данных
        }

        public async Task OptionsUpdate(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id); // Найти существующую сущность по идентификатору

            if (existingEntity == null)
            {
                // Обработка случая, когда сущность с заданным идентификатором не найдена
                // Можно выбросить исключение или вернуть сообщение об ошибке
                // Например:
                throw new DllNotFoundException("Entity not found");
            }

            // Обновить свойства сущности с помощью значений из новой сущности
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync(); // Сохранить изменения в базе данных
        }



    }
}
