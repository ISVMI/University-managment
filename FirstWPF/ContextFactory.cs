using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWPF
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<UniversityContext>
    {
        public UniversityContext CreateDbContext(string[] args)
        {
            // Создание построителя опций для контекста
            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();

            // Получение строки подключения
            string connectionString = "Server=DESKTOP-4CPJD5R;Database=UniversityDb;Trusted_Connection=True;TrustServerCertificate=True;";

            // Настройка провайдера базы данных
            optionsBuilder.UseSqlServer(connectionString,
                // Указание сборки для миграций
                options => options.MigrationsAssembly(typeof(UniversityContext).Assembly.FullName));

            // Создание и возврат экземпляра контекста
            return new UniversityContext(optionsBuilder.Options);
        }
    }
}
