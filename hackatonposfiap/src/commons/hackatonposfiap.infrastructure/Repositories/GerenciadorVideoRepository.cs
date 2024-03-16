using Dapper;
using hackatonposfiap.domain.Entities;
using hackatonposfiap.domain.Enums;
using hackatonposfiap.domain.Interfaces;
using hackatonposfiap.infrastructure.Queries;
using System.Data;

namespace hackatonposfiap.infrastructure.Repositories
{
    public class GerenciadorVideoRepository : IGerenciadorVideoRepository
    {
        private readonly IDbConnection _dbConnection;

        public GerenciadorVideoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Create(GerenciadorVideoItem entity)
        {
            TSqlQueryType queryType = TSqlQueryType.CREATE_VIDEO;
            string sql = SQlManager.GetSql(queryType);
            sql = ReplaceQueryParameters(queryType, entity, sql);
            var result = await _dbConnection.ExecuteAsync(sql);
        }

        public async Task<List<GerenciadorVideoItem>> GetAll()
        {
            TSqlQueryType queryType = TSqlQueryType.LIST_VIDEOS;
            string sql = SQlManager.GetSql(queryType);
            var result = await _dbConnection.QueryAsync<GerenciadorVideoItem>(sql);
            return result.ToList();
        }

        public async Task<GerenciadorVideoItem> GetById(int id)
        {
            TSqlQueryType queryType = TSqlQueryType.GET_VIDEO;
            string sql = SQlManager.GetSql(queryType);
            sql = ReplaceQueryParameters(queryType, new GerenciadorVideoItem { Id = id }, sql);
            var result = await _dbConnection.QueryFirstOrDefaultAsync<GerenciadorVideoItem>(sql);
            return result;
        }

        public async Task<GerenciadorVideoItem> GetByName(string name)
        {
            TSqlQueryType queryType = TSqlQueryType.GET_VIDEO;
            string sql = SQlManager.GetSql(queryType);
            sql = ReplaceQueryParameters(queryType, new GerenciadorVideoItem { NomeArquivo = name }, sql);
            var result = await _dbConnection.QueryFirstOrDefaultAsync<GerenciadorVideoItem>(sql);
            return result;
        }


        private string ReplaceQueryParameters(TSqlQueryType queryType, GerenciadorVideoItem entity, string querySql)
        {
            string sql = querySql;

            switch (queryType)
            {
                case TSqlQueryType.CREATE_VIDEO:
                    sql = sql
                        .Replace("@NomeArquivo", entity.NomeArquivo)
                        .Replace("@NaminhoArquivo", entity.CaminhoArquivo)
                        .Replace("@Intervalo", entity.Intervalo);
                    break;

                case TSqlQueryType.GET_VIDEO:
                    sql = sql
                        .Replace("@Id", entity.Id.ToString());
                    break;

                case TSqlQueryType.GET_VIDEO_NAME:
                    sql = sql
                        .Replace("@Id", entity.Id.ToString());
                    break;
            }

            return sql;
        }

    }
}
