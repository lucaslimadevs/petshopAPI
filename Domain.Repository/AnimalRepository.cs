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
    public class AnimalRepository : Repository<Animal>
    {

        public AnimalRepository(IDbConnection connection, INotification notification, IConfiguration configuration) : base(connection, notification, configuration) 
        { }

        public object Filter(AnimalFilter filter)
        {
            var SQL = new StringBuilder(@" SELECT * FROM animal 
                                           LEFT OUTER JOIN cliente ON id_cliente = animal_fkcliente ");
            var parameters = new Dictionary<string, object>();

            if (filter != null)
            {
                SQL.Append(WhereFilter(filter));
                SQL.Append($" ORDER BY animal_nome");
            }

            return new
            {
                conteudo = _connection.Query<Animal, Cliente, Animal>(
                    SQL.ToString(),
                    (animal, cliente) => { animal.fkcliente = cliente; return animal; },
                    splitOn: "id_cliente"
                )
                
            };
        }

        public override IEnumerable<Animal> GetAll()
        {
            var SQL = @"SELECT * FROM animal
                        LEFT OUTER JOIN cliente ON id_cliente = animal_fkcliente
                        ORDER BY animal_nome";

            var qry = _connection.Query<Animal, Cliente, Animal>(
                    SQL,
                    (animal, cliente) => { animal.fkcliente = cliente; return animal; },
                    splitOn: "id_cliente"
                );

            return qry;
        }

        public override Animal Get(int id)
        {
            var SQL = @"SELECT * FROM animal      
                        LEFT OUTER JOIN cliente ON id_cliente = animal_fkcliente
                        WHERE id_animal = @sequencia
                        ORDER BY animal_nome";

            var qry = _connection.Query<Animal, Cliente, Animal>(
                    SQL,
                    (animal, cliente) => { animal.fkcliente = cliente; return animal; },
                    splitOn: "id_cliente",
                    param: new { sequencia = id }
              );

            return qry.FirstOrDefault();
        }


        public override Animal Insert(Animal animal)
        {
            try
            {
                if (TestarAnimal(animal, "I"))
                    return base.Insert(animal);
                return animal;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return animal;
            }

        }

        public override Animal Update(Animal animal)
        {
            try
            {
                if (TestarAnimal(animal, "U"))
                {
                    animal = base.Update(animal);
                }
                return animal;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return animal;
            }
        }

        public override void Delete(Animal animal)
        {
            try
            {
                base.Delete(animal);
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
            }
        }


        private bool TestarAnimal(Animal animal, string operation)
        {
            try
            {
                var SQL = "";

                if (animal.animal_nome.Trim().Equals(""))
                {
                    NotificationAdd("Nome do animal é obrigatório");
                }

                if (animal.animal_fkcliente == 0)
                    NotificationAdd("Dono é obrigatório.");
                else
                {
                    SQL = @" SELECT * FROM cliente WHERE id_cliente = @id";
                    var cliente = _connection.Query<Animal>(SQL, new { id = animal.animal_fkcliente}).FirstOrDefault();

                    if (cliente == null)
                        NotificationAdd("Cliente não cadastrado.");
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
                NotificationAdd("Erro ao salvar Animal. " + e);
                return false;
            }
        }

        private StringBuilder WhereFilter(AnimalFilter filter)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder("");

            if (filter != null)
            {
                if (!String.IsNullOrEmpty(filter.animal_nome))
                {
                    SQL.Append($" {whereAnd} UPPER(animal_nome) LIKE '{filter.animal_nome.ToUpper() + "%"}'");
                    whereAnd = " AND ";
                }
            }
            return SQL;
        }




    }
}
