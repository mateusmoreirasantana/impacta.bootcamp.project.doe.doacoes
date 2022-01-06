using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using impacta.bootcamp.project.doe.doacoes.api.Models.Doacoes;
using impacta.bootcamp.project.doe.doacoes.infra.data.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace impacta.bootcamp.project.doe.doacoes.api.Endpoints.Doacoes
{
    public class GetHistory : BaseEndPoint<List<HistoricoDoacoesResponse>>
    {

        private readonly SqlContext context;
        public GetHistory(SqlContext sqlContext)
        {
            context = sqlContext;
        }
        [HttpGet("doacao/history")]
        public async override Task<ActionResult<List<HistoricoDoacoesResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            try
            {

                var resp = await getAllResponse(((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Email)?.Value);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                string msg = $"Erro ao  campanha" + ex.ToString();

                var resp = new HistoricoDoacoesResponse()
                {
                    status = "500",
                    error = new Models.Errors.Error() { errorCode = "MODVAL01", errorMessage = msg }

                };
                return StatusCode(StatusCodes.Status500InternalServerError, resp);

            }
        }

            private async Task<List<HistoricoDoacoesResponse>> getAllResponse(string user)
        {
            StringBuilder resp = new StringBuilder();
            List<HistoricoDoacoesResponse> campanhaGetAllResponse = new List<HistoricoDoacoesResponse>();
            using (SqlConnection conn = await context.GetConnection())
            {
                using (SqlCommand sqlCommand = new SqlCommand("pr_historico_doacao_sel", conn))
                {

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
                    sqlCommand.Parameters["@username"].Value = user;

                    
                    if (sqlCommand.Connection.State.ToString() == "Closed")
                    {
                        conn.Open();
                    }
                    var sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (sqlDataReader.Read())
                    {
                        resp.Append(sqlDataReader.GetString(0));
                    }
                    Console.WriteLine(resp.ToString());


                    campanhaGetAllResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HistoricoDoacoesResponse>>(resp.ToString());
                }


                return campanhaGetAllResponse;
            }
        }

    }
}
