using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using impacta.bootcamp.project.doe.doacoes.core.DTOs;
using impacta.bootcamp.project.doe.doacoes.core.Interfaces.Repositories.Doacoes;
using impacta.bootcamp.project.doe.doacoes.infra.data.Data.Context;

namespace impacta.bootcamp.project.doe.doacoes.infra.data.Data.Repositories
{
    public class CreateDonationRepository:ICreateDonationRepository
    {

        private readonly SqlContext context;
        public CreateDonationRepository(SqlContext sqlContext)
        {
            context = sqlContext;
        }
        public async Task<OperationCreateDonationDTO> create(CreateDonationDTO request)
        {
            string insert =  
$" declare @userId int " +
$" select @userId = us.id from users us  where us.user_name =@userName " +

$"  insert into doacao( " +

$"  userId, " +
$"  campanhaId, " +
$"   valorDoacao, " +
$"  unidadeMedidaId, " +
$"  transactionId, " +
$"  exibeValorDoado, " +
$"  exibirNomeDoador ," +
$"  statusDoacaoId, " +
$"  dataInclusao, " +
$"  formaPagamentoDoacaoId) " +
$" select " +
$"    @userId    ," +
$"    @campanhaId  ," +
$"    @valor ," +
$"    2 ," +
$"    @transactionId," +
$"    @exibeValorDoacao ," +
$"    @exibeNomeDoador ," +
$"    2 ," +
$"    GETDATE() ," +
$"    @formaPagamentId ";


            string insertComentario = $" declare @userId int " +
            $" select @userId = us.id from users us  where us.user_name =@userName " +
             " insert into campanhaComentario (descricao,campanhaId,userId) select @comentario , @campanhaId , @userId";


            try
            {

                var sqlCon = await context.GetConnection();



                using (var transaction = sqlCon.BeginTransaction())
                {

                    try
                    {
                        var doacaoID = sqlCon.Query<int>(insert, request, transaction);

                        if (!string.IsNullOrWhiteSpace(request.comentario) )
                        {
                            sqlCon.Query<int>(insertComentario, request, transaction);

                        }

                        transaction.Commit();

                        return new OperationCreateDonationDTO() { sucesso = true };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
