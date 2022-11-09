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
    public class AlojamentoRepository : Repository<Alojamento>
    {

        public AlojamentoRepository(IDbConnection connection, INotification notification, IConfiguration configuration) : base(connection, notification, configuration) 
        { }

        //public object filter(AlojamentoFilter filter)
        //{
        //    var SQL = new StringBuilder(" SELECT * FROM alojamento ");
        //    var parametros = new Dictionary<string, object>();

        //    if (filter != null)
        //    {
        //        SQL.Append(WhereFilter(filter));
        //        SQL.Append($" ORDER BY alojamento_descricao");
        //    }

        //    return new
        //    {
        //        conteudo = LerTabela(SQL.ToString(), parametros),
        //        totalElementos = CalcularTotalElementos(filtro)
        //    };
        //}

        public override IEnumerable<Alojamento> GetAll()
        {
            var SQL = @"SELECT * FROM alojamento
                        ORDER BY alojamento_descricao";

            var qry = _connection.Query<Alojamento>(SQL);

            return qry;
        }

        public override Alojamento Get(int id)
        {
            var SQL = @"SELECT * FROM alojamento                        
                        WHERE id_alojamento = @sequencia
                        ORDER BY alojamento_descricao";

            var qry = _connection.Query<Alojamento>
              (
                  SQL,
                  param: new { sequencia = id }
              );

            return qry.FirstOrDefault();
        }


        public override Alojamento Insert(Alojamento alojamento)
        {
            try
            {
                if (TestarAlojamento(alojamento, "I"))
                    return base.Insert(alojamento);
                return alojamento;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return alojamento;
            }

        }

        public override Alojamento Update(Alojamento alojamento)
        {
            try
            {
                if (TestarAlojamento(alojamento, "U"))
                {
                    alojamento = base.Update(alojamento);
                }
                return alojamento;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return alojamento;
            }
        }

        public override void Delete(Alojamento alojamento)
        {
            try
            {
                base.Delete(alojamento);
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
            }
        }


        private bool TestarAlojamento(Alojamento alojamento, string operation)
        {
            try
            {
                if ((alojamento.alojamento_status < 0) || (alojamento.alojamento_status > 2))
                {
                    NotificationAdd("Status do alojamento deve ser (0 = ocupado, 1 = livre, 2 = esperando o dono)");
                }

                if (alojamento.alojamento_descricao.Trim().Equals(""))
                {
                    NotificationAdd("Descrição é obrigatória");
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
                NotificationAdd("Erro ao salvar Alojamento. " + e);
                return false;
            }
        }



    }
}
