using N5WebApi.Domain.Abstractions;
using N5WebApi.src.Infrastructure.Data;

namespace N5WebApi.Application.Extensions;
public static class DbServiceExtensions
{
    internal static IQueryable<TEntity> Page<TEntity>(this IQueryable<TEntity> query,
        SearchRequest request) =>
            request.IsExport ? query : query.Skip((request.PageNumber > 0 ? request.PageNumber - 1 : 0) * request.PageSize).Take(request.PageSize);

    public static async Task WaitForSqlServerAsync(Context dbContext, int maxRetries = 10, int delaySeconds = 5)
    {
        var retries = 0;
        while (true)
        {
            try
            {
                if (await dbContext.Database.CanConnectAsync())
                {
                    Console.WriteLine("✅ Conectado exitosamente a SQL Server.");
                    break;
                }
            }
            catch (Exception ex)
            {
                retries++;
                Console.WriteLine($"❌ Intento {retries}/{maxRetries}: SQL Server no está listo aún. Error: {ex.Message}");

                if (retries >= maxRetries)
                {
                    throw new Exception("🚫 No se pudo conectar a SQL Server después de varios intentos.", ex);
                }

                await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
            }
        }
    }
}