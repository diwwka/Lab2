using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Lab2.dal;
using Lab2.dal.Entities;

namespace Lab2.Services
{
    public class UpdateService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UpdateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Створюємо область видимості для роботи з базою
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mongoService = scope.ServiceProvider.GetRequiredService<StudentService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<LabDbContext>();

                // Використовуємо транзакцію
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        // Отримуємо студентів з SQL
                        var students = dbContext.Students.ToList();

                        foreach (var student in students)
                        {
                            // Створюємо об'єкт для Mongo
                            var studentDto = new StudentDto
                            {
                                Name = $"{student.FirstName} {student.LastName}",
                                Birth = student.BirthDate
                            };

                            // Генеруємо унікальний ID
                            studentDto.Id = $"{studentDto.Name}_{studentDto.Birth.ToShortDateString()}_{Guid.NewGuid()}";

                            // Записуємо в Mongo
                            await mongoService.CreateAsync(studentDto);
                        }

                        // Завершуємо транзакцію
                        transaction.Complete();
                    }
                    catch (Exception ex)
                    {
                        transaction.Dispose();
                        Console.WriteLine($"Error during sync: {ex.Message}");
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}