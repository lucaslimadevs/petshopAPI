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
    public class ProntuarioRepository : Repository<Prontuario>
    {
        private readonly AlojamentoRepository alojamentoRepository;
        public ProntuarioRepository(IDbConnection connection, INotification notification, AlojamentoRepository _alojamentoRepository, IConfiguration configuration) : base(connection, notification, configuration)
        {
            alojamentoRepository = _alojamentoRepository;
        }

        public object Filter(ProntuarioFilter filter)
        {
            var SQL = new StringBuilder(@" SELECT * FROM prontuario 
                                           LEFT OUTER JOIN alojamento ON id_alojamento = prontuario_fkalojamento
                                           LEFT OUTER JOIN animal ON id_animal = prontuario_fkanimal ");
            var parameters = new Dictionary<string, object>();

            if (filter != null)
            {
                SQL.Append(WhereFilter(filter));
                SQL.Append($" ORDER BY id_prontuario");
            }

            return new
            {
                conteudo = _connection.Query<Prontuario, Alojamento, Animal, Prontuario>(
                    SQL.ToString(),
                    (prontuario, alojamento, animal) => { prontuario.fkalojamento = alojamento; prontuario.fkanimal = animal; return prontuario; },
                    splitOn: "id_alojamento, id_animal"
                )
            };
        }

        public override IEnumerable<Prontuario> GetAll()
        {
            var SQL = @"SELECT * FROM prontuario 
                        LEFT OUTER JOIN alojamento ON id_alojamento = prontuario_fkalojamento
                        LEFT OUTER JOIN animal ON id_animal = prontuario_fkanimal
                        ORDER BY id_prontuario ";

            var qry = _connection.Query<Prontuario, Alojamento, Animal, Prontuario>(
                    SQL.ToString(),
                    (prontuario, alojamento, animal) => { prontuario.fkalojamento = alojamento; prontuario.fkanimal = animal; return prontuario; },
                    splitOn: "id_alojamento, id_animal"
                );

            return qry;
        }

        public override Prontuario Get(int id)
        {
            var SQL = @"SELECT * FROM prontuario 
                        LEFT OUTER JOIN alojamento ON id_alojamento = prontuario_fkalojamento
                        LEFT OUTER JOIN animal ON id_animal = prontuario_fkanimal
                        WHERE id_prontuario = @sequencia
                        ORDER BY id_prontuario ";

            var qry = _connection.Query<Prontuario, Alojamento, Animal, Prontuario>(
                    SQL.ToString(),
                    (prontuario, alojamento, animal) => { prontuario.fkalojamento = alojamento; prontuario.fkanimal = animal; return prontuario; },
                    splitOn: "id_alojamento, id_animal",
                    param: new { sequencia = id }
                );

            return qry.FirstOrDefault();
        }


        public override Prontuario Insert(Prontuario prontuario)
        {
            try
            {
                if (TestarProntuario(prontuario, "I"))
                    return base.Insert(prontuario);
                return prontuario;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return prontuario;
            }

        }

        public override Prontuario Update(Prontuario prontuario)
        {
            try
            {
                if (TestarProntuario(prontuario, "U"))
                {
                    prontuario = base.Update(prontuario);
                }
                return prontuario;
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
                return prontuario;
            }
        }

        public override void Delete(Prontuario prontuario)
        {
            try
            {
                base.Delete(prontuario);
            }
            catch (Exception e)
            {
                NotificationAdd(e.Message);
            }
        }


        private bool TestarProntuario(Prontuario prontuario, string operation)
        {
            try
            {
                var SQL = "";

                if (prontuario.prontuario_motivo.Trim().Equals(""))
                {
                    NotificationAdd("Motivo é obrigatório");
                }

                if ((prontuario.prontuario_estado < 0) || (prontuario.prontuario_estado > 2))
                {
                    NotificationAdd("Estado de tratamento do animal deve ser (0 = em tratamento, 1 = se recuperando, 2 = recuperado)");
                }

                if (prontuario.prontuario_fkalojamento == 0)
                    NotificationAdd("Alojamento é obrigatório.");
                else
                {
                    SQL = @" SELECT * FROM alojamento WHERE id_alojamento = @id";
                    var alojamento = _connection.Query<Alojamento>(SQL, new { id = prontuario.prontuario_fkalojamento }).FirstOrDefault();

                    if (alojamento == null)
                        NotificationAdd("Alojamento não cadastrado.");

                    if (operation.Equals("I")) // insert
                    {
                        if (alojamento.alojamento_status != 1)
                        {
                            NotificationAdd("Alojamento já ocupado por outro animal");
                        }
                        else
                        {
                            UpdateAlojamentoStatus(alojamento, operation); //atualizar status do alojamento = ocupado
                        }

                    }

                    if (operation.Equals("U")) // Update
                    {
                        if (prontuario.prontuario_estado == 2) //recuperado
                        {
                            UpdateAlojamentoStatus(alojamento, operation); //atualizar status do alojamento = livre
                        }
                    }
                }

                if (prontuario.prontuario_fkanimal == 0)
                    NotificationAdd("Animal é obrigatório.");
                else
                {
                    SQL = @" SELECT * FROM animal WHERE id_animal = @id";
                    var animal = _connection.Query<Animal>(SQL, new { id = prontuario.prontuario_fkanimal }).FirstOrDefault();

                    if (animal == null)
                        NotificationAdd("Animal não cadastrado.");
                }

                return !HaveNotifications();
            }
            catch (Exception e)
            {
                NotificationAdd("Erro ao salvar Prontuario. " + e);
                return false;
            }
        }

        private StringBuilder WhereFilter(ProntuarioFilter filter)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder("");

            if (filter != null)
            {
                if (filter.prontuario_fkanimal != 0)
                {
                    SQL.Append($" {whereAnd} prontuario_fkanimal = {filter.prontuario_fkanimal} ");
                    whereAnd = " AND ";
                }
            }
            return SQL;
        }

        private void UpdateAlojamentoStatus(Alojamento alojamento, string operation)
        {
            if (operation.Equals("I")) // insert
            {
                alojamento.alojamento_status = 0; //alojamento recebe status = ocupado
            }

            if (operation.Equals("U")) // update
            {
                alojamento.alojamento_status = 2; //alojamento recebe status = esperando dono
            }

            alojamentoRepository.Update(alojamento);
        }
    }
}
