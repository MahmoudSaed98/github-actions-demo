using github_actions_demo.ConsoleApp.Core.Interfaces;
using github_actions_demo.ConsoleApp.Presentation.Console.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


IServiceProvider serviceProvider = ProgramModule.Build();

var app = serviceProvider.GetRequiredService<IApplicationHost>();

await app.RunAsync();