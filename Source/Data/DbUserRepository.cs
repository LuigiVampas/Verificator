using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Model;
using MySql.Data.MySqlClient;
using Presentation;
using Presentation.ErrorProvider;

namespace Data
{
    /// <summary>
    /// Репозиторий, в котором хранятся объекты класса User, основанный на базе данных
    /// </summary>
    public class DbUserRepository : IUserRepository
    {
        private readonly IErrorProvider _errorProvider;

        /// <summary>
        /// Строка подключения к базе данных, параметры которой берутся из app.config.
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["LocalUserDatabase"].ConnectionString;

        public DbUserRepository(IErrorProvider errorProvider)
        {
            _errorProvider = errorProvider;
        }

        /// <summary>
        /// Возвращает объект класса User с заданным id, если он существует.
        /// </summary>
        /// <param name="id">id искомого объекта класса User.</param>
        /// <returns>Объект класса User с заданным id. Если такого нет то null.</returns>
        public User GetUser(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var contextDb = new UserDbContext(connection, false))
                    {
                        contextDb.Database.CreateIfNotExists();
                    }

                    connection.Open();

                    var transaction = connection.BeginTransaction();

                    User user;

                    try
                    {
                        using (var context = new UserDbContext(connection, false))
                        {

                            context.Database.UseTransaction(transaction);

                            user = context.Users.SingleOrDefault(u => u.Id == id);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }

                    return user;
                }
            }
            catch (Exception e)
            {
                _errorProvider.ShowDbConnectionErrorMessage(e);
                return null;
            }
        }

        /// <summary>
        /// Возвращает все объекты класса User, хранящиеся в репозитории.
        /// </summary>
        /// <returns>Все объекты класса User, хранящиеся в репозитории.</returns>
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var contextDb = new UserDbContext(connection, false))
                    {
                        contextDb.Database.CreateIfNotExists();
                    }

                    connection.Open();

                    var transaction = connection.BeginTransaction();

                    IEnumerable<User> users;

                    try
                    {
                        using (var context = new UserDbContext(connection, false))
                        {

                            context.Database.UseTransaction(transaction);

                            users = context.Users.ToArray();
                        }

                        transaction.Commit();
                    }
                    catch 
                    {
                        transaction.Rollback();
                        throw;
                    }

                    return users;
                }
            }
            catch (Exception e)
            {
                _errorProvider.ShowDbConnectionErrorMessage(e);
                return null;
            }
            
        }

        /// <summary>
        /// Добавляет указанного юзера в репозиторий.
        /// </summary>
        /// <param name="user">Добавляемый объекта класса User.</param>
        public void AddUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var contextDb = new UserDbContext(connection, false))
                    {
                        contextDb.Database.CreateIfNotExists();
                    }

                    connection.Open();

                    var transaction = connection.BeginTransaction();

                    try
                    {
                        using (var context = new UserDbContext(connection, false))
                        {
                            context.Database.UseTransaction(transaction);

                            context.Users.Add(user);

                            SaveChanges(context);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _errorProvider.ShowDbConnectionErrorMessage(e);
            }
            
        }

        /// <summary>
        /// Удаляет из репозитория указанный объект класса User.
        /// </summary>
        /// <param name="user">Удаляемый объект класса User.</param>
        public void DeleteUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var contextDb = new UserDbContext(connection, false))
                    {
                        contextDb.Database.CreateIfNotExists();
                    }

                    connection.Open();

                    var transaction = connection.BeginTransaction();

                    try
                    {
                        using (var context = new UserDbContext(connection, false))
                        {

                            context.Database.UseTransaction(transaction);

                            context.Entry(user).State = EntityState.Deleted;

                            SaveChanges(context);
                        }

                        transaction.Commit();
                    }
                    catch 
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _errorProvider.ShowDbConnectionErrorMessage(e);
            }
        }

        /// <summary>
        /// Обновляет поля указанный объект класса User.
        /// </summary>
        /// <param name="user">Объект класса User, поля которого нужно обновить.</param>
        public void UpdateUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    using (var contextDb = new UserDbContext(connection, false))
                    {
                        contextDb.Database.CreateIfNotExists();
                    }

                    connection.Open();

                    var transaction = connection.BeginTransaction();

                    try
                    {
                        using (var context = new UserDbContext(connection, false))
                        {

                            context.Database.UseTransaction(transaction);

                            context.Entry(user).State = EntityState.Modified;

                            SaveChanges(context);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _errorProvider.ShowDbConnectionErrorMessage(e);
            }
            
        }

        /// <summary>
        /// Сохраняет внесенные в контекст изменения в базе данных.
        /// </summary>
        /// <param name="context">Контекст, в котором произошли изменения.</param>
        private void SaveChanges(UserDbContext context)
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.Single().Reload();
                }

            } while (saveFailed); 

        }
    }
}
