using Dapper;
using Domain.Entities;
using Domain.Entities.Filter;
using Domain.Infraestructure.Notification;
using Domain.Infraestructure.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Domain.Repository
{
    public class ClienteRepository : Repository<Cliente>
    {

        public ClienteRepository(IDbConnection connection, INotification notification, IConfiguration configuration) : base(connection, notification, configuration) 
        { }

        public override IEnumerable<Cliente> GetAll()
        {
            var SQL = @"SELECT * FROM cliente
                        ORDER BY cliente_nome";

            var qry = _connection.Query<Cliente>(SQL);

            return qry;
        }

        public override Cliente Get(int id)
        {
            var SQL = @"SELECT * FROM cliente                        
                        WHERE id_cliente = @sequencia
                        ORDER BY cliente_nome";

            var qry = _connection.Query<Cliente>
              (
                  SQL,
                  param: new { sequencia = id }
              );

            return qry.FirstOrDefault();
        }


        public override Cliente Insert(Cliente cliente)
        {
            try
            {
                if (TestarCliente(cliente, "I"))
                    return base.Insert(cliente);
                return cliente;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return cliente;
            }

        }

        public override Cliente Update(Cliente cliente)
        {
            try
            {
                if (TestarCliente(cliente, "U"))
                {                    
                    cliente = base.Update(cliente);
                }
                return cliente;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return cliente;
            }
        }

        public override void Delete(Cliente cliente)
        {
            try
            {
                base.Delete(cliente);
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
            }
        }


        private bool TestarCliente(Cliente cliente, string operation)
        {
            try
            {

                if (cliente.cliente_nome.Trim().Equals(""))
                {
                    NotificationAdd("Nome é obrigatório");
                }

                if (cliente.cliente_rua.Trim().Equals(""))
                {
                    NotificationAdd("Rua é obrigatória");
                }

                if (cliente.cliente_bairro.Trim().Equals(""))
                {
                    NotificationAdd("Bairro é obrigatório");
                }

                if (cliente.cliente_numero.Trim().Equals(""))
                {
                    NotificationAdd("Número é obrigatório");
                }

                if (cliente.cliente_telefone.Trim().Equals(""))
                {
                    NotificationAdd("Telefone é obrigatório");
                }

                if (operation.Equals("I")) // insert
                {


                }

                if (operation.Equals("U")) // Update
                {


                }

                return !HaveNotifications();
            }
            catch (Exception e)
            {
                NotificationAdd("Erro ao salvar Cliente. " + e);
                return false;
            }
        }



    }
}
