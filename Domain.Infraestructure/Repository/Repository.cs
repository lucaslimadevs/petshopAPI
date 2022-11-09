using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using Domain.Infraestructure.Notification;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Domain.Infraestructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly IDbConnection _connection;
        protected readonly INotification _notification;
        protected string stringConexaoBanco;

        public Repository(IDbConnection connection, INotification notification, IConfiguration configuration)
        {
            this._connection = connection;
            this.stringConexaoBanco = configuration.GetConnectionString("CONNECTION");
            this._connection.ConnectionString = this.stringConexaoBanco;
            this._notification = notification;
        }


        public virtual T Get(int id)
        {
            return this._connection.Get<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this._connection.GetAll<T>();
        }

        public virtual void Delete(T entity)
        {
            this._connection.Delete(entity);
        }

        public virtual T Insert(T entity)
        {
            try
            {
                var chave = _connection.Insert<T>(entity);
                return entity;
            }
            catch (MySqlException ex)
            {
                NotificationAdd("Erro ao incluir dados. " + entity.ToString() + " " + ex.Message);
                return entity;
            }
            catch (Exception e)
            {
                var msgErro = "Erro ao incluir dados. " + entity.ToString() + " " + e.Message;
                NotificationAdd(msgErro);                
                return entity;
            }
        }

        public virtual T Update(T entity)
        {
            try
            {
                this._connection.Update<T>(entity);
                return entity;

            }
            catch (MySqlException ex)
            {
                NotificationAdd("Erro ao atualizar dados em transação. " + entity.ToString() + " " + ex.Message);
                return entity;
            }
            catch (Exception e)
            {
                var msgErro = "Erro ao atualizar dados em transação. " + entity.ToString() + " " + e.Message;
                NotificationAdd(msgErro);
                return entity;
            }
        }

        protected void NotificationAdd(string message)
        {
            this._notification.NotificationAdd(message);
        }

        protected bool HaveNotifications()
        {
            return this._notification.HaveNotifications();
        }

        protected List<string> ListAll()
        {
            return this._notification.ListAll();
        }

        public virtual IEnumerable<T> ReadTable(string SQL, Dictionary<string, object> parameters)
        {
            return _connection.Query<T>(SQL, param: parameters);
        }


    }
}
