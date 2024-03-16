using Dapper;
using hackatonposfiap.domain.Entities;
using hackatonposfiap.domain.Enums;
using hackatonposfiap.domain.Interfaces;
using hackatonposfiap.infrastructure.Queries;
using System.Data;

namespace hackatonposfiap.infrastructure.Repositories
{
    public class GerenciadorImagemRepository : IGerenciadorImagemRepository
    {
        private readonly IDbConnection _dbConnection;

        public GerenciadorImagemRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Create(GerenciadorImagemItem entity)
        {
            TSqlQueryType queryType = TSqlQueryType.CREATE_PICTURE;
            string sql = SQlManager.GetSql(queryType);
            sql = ReplaceQueryParameters(queryType, entity, sql);
            var result = await _dbConnection.ExecuteAsync(sql);
        }

        public async Task<List<GerenciadorImagemItem>> GetAll()
        {
            TSqlQueryType queryType = TSqlQueryType.LIST_PICTURES;
            string sql = SQlManager.GetSql(queryType);
            var result = await _dbConnection.QueryAsync<GerenciadorImagemItem>(sql);
            return result.ToList();
        }

        public async Task<GerenciadorImagemItem> GetById(int id)
        {
            TSqlQueryType queryType = TSqlQueryType.GET_PICTURE;
            string sql = SQlManager.GetSql(queryType);
            sql = ReplaceQueryParameters(queryType, new GerenciadorImagemItem { Id = id }, sql);
            var result = await _dbConnection.QueryFirstOrDefaultAsync<GerenciadorImagemItem>(sql);
            return result;
        }

        private string ReplaceQueryParameters(TSqlQueryType queryType, GerenciadorImagemItem entity, string querySql)
        {
            string sql = querySql;

            switch (queryType)
            {
                case TSqlQueryType.CREATE_PICTURE:
                    sql = sql
                        .Replace("@IdVideo", entity.IdVideo.ToString())
                        .Replace("@NomeArquivo", entity.NomeArquivo)
                        .Replace("@CaminhoArquivo", entity.CaminhoArquivo);
                    break;

                case TSqlQueryType.GET_PICTURE:
                    sql = sql
                        .Replace("@Id", entity.Id.ToString());
                    break;
            }

            return sql;
        }

    }
}
