using System.Text;
using VoiceTexterBot;

Console.OutputEncoding = Encoding.Unicode;

// Объект, отвечающий за постоянный жизненный цикл приложения
var host = new HostBuilder()
    .ConfigureServices((hostContext, services) => ConfigureServices(services)) //Задаем конфигурацию
    .UseConsoleLifetime()  //Позволяет поддерживать приложение активным в консоли
    .Build();          //Собираем

Console.WriteLine("Сервис запущен.");
//Запускаем сервис
await host.RunAsync();
Console.WriteLine("Сервис остановлен.");
 

static void ConfigureServices(IServiceCollection services)
{
    //Регистрируем telegramBotClient с токеном подключения
    services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5636623417:AAFCfq375c2qQ5LrTatZqa_TPbnG9JHmb28"));

    //Регистрируем постоянно активный сервис бота
    services.AddHostedService<Bot>();
}