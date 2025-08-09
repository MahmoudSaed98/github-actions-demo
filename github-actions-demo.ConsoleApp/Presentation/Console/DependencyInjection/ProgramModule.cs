using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Core.Repositories;
using github_actions_demo.ConsoleApp.Core.Services;
using github_actions_demo.ConsoleApp.Infrastructure;
using github_actions_demo.ConsoleApp.Infrastructure.Persistence;
using github_actions_demo.ConsoleApp.Presentation.Console.Commands;
using github_actions_demo.ConsoleApp.Presentation.Console.Host;
using Microsoft.Extensions.DependencyInjection;

namespace github_actions_demo.ConsoleApp.Presentation.Console.DependencyInjection;

public static class ProgramModule
{
    public static IServiceProvider Build()
    {
        var serviceCollection = new ServiceCollection()
                               .AddScoped<IUserRepository, InMemoryUserRepository>()
                               .AddScoped<IUserService, UserService>()
                               .AddSingleton<IConsoleReader, ConsoleWrapper>()
                               .AddSingleton<IConsoleWriter, ConsoleWrapper>()
                               .AddSingleton<IConsoleView, ConsoleView>()
                               .AddSingleton<IApplicationHost, ApplicationHost>()
                               .AddTransient<IConsoleCommand, RegisterUserCommand>()
                               .AddTransient<IConsoleCommand, GetUserByUsernameCommand>()
                               .AddTransient<IConsoleCommand, GetUserByEmailCommand>()
                               .AddTransient<IConsoleCommand, DeleteUserCommand>()
                               .AddTransient<IConsoleCommand, GetAllUsersCommand>();

        return serviceCollection.BuildServiceProvider();
    }
}