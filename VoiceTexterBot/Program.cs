using System.Text;
using VoiceTexterBot;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Controllers;
using VoiceTexterBot.Services;

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
    AppSettings appSettings = BuildAppSettings();
    services.AddSingleton(BuildAppSettings);

    //Подключаем контроллеры сообщений и кнопок
    services.AddTransient<DefaultMessageController>();
    services.AddSingleton<VoiceMessageController>();
    services.AddTransient<TextMessageController>();
    services.AddTransient<InlineKeyboardController>();

    services.AddSingleton<IStorage, MemoryStorage>();


    //Регистрируем telegramBotClient с токеном подключения
    services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5636623417:AAFCfq375c2qQ5LrTatZqa_TPbnG9JHmb28"));

    //Регистрируем постоянно активный сервис бота
    services.AddHostedService<Bot>();
}
static AppSettings BuildAppSettings() 
{
    return new AppSettings()
    {
        BotToken = "5636623417:AAFCfq375c2qQ5LrTatZqa_TPbnG9JHmb28"
    };
}