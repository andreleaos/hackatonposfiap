
using hackatonposfiap.domain.Enums;

namespace hackatonposfiap.infrastructure.Queries
{
    public static class SQlManager
    {
        public static string GetSql(TSqlQueryType sqlQueryType)
        {
            string sql = "";

            switch (sqlQueryType)
            {
                case TSqlQueryType.CREATE_VIDEO:
                    sql = "insert into Video (CaminhoArquivo, NomeArquivo, Intervalo) values ('@CaminhoArquivo', '@NomeArquivo', '@Intervalo');";
                    break;

                case TSqlQueryType.CREATE_PICTURE:
                    sql = "insert into filme (IdVideo, CaminhoArquivo, NomeArquivo) values ('@IdVideo', '@CaminhoArquivo', '@NomeArquivo');";
                    break;


                case TSqlQueryType.LIST_VIDEOS:
                    sql = "select Id, CaminhoArquivo, NomeArquivo, Intervalo from Video order by Id;";
                    break;

                case TSqlQueryType.LIST_PICTURES:
                    sql = "select Id, IdVideo, CaminhoArquivo, NomeArquivo from Imagem order by IdVideo, Id;";
                    break;

                case TSqlQueryType.GET_VIDEO:
                    sql = "select Id, CaminhoArquivo, NomeArquivo, Intervalo from Video where Id = @Id;";
                    break;

                case TSqlQueryType.GET_PICTURE:
                    sql = "select Id, IdVideo, CaminhoArquivo, NomeArquivo from Imagem where Id = @Id;";
                    break;
            }

            return sql;
        }
    }
}
