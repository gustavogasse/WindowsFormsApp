using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows.Forms;
using WindowsFormsAppWithFirebird.Domain.Services;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;
using WindowsFormsAppWithFirebird.Clientes;
using WindowsFormsAppWithFirebird.Domain.Data;
using WindowsFormsAppWithFirebird.Infra.Repositories;
using WindowsFormsAppWithFirebird.Infra;

namespace WindowsFormsAppWithFirebird
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<FrmPrincipal>());
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<DataContext>();
                    services.AddTransient<IClienteRepository, ClienteRepository>();
                    services.AddTransient<IEnderecoRepository, EnderecoRepository>();
                    services.AddTransient<IClienteHistoricoRepository, ClienteHistoricoRepository>();
                    services.AddTransient<IEnderecoHistoricoRepository, EnderecoHistoricoRepository>();
                    services.AddTransient<IClienteService, ClienteService>();
                    services.AddTransient<IEnderecoService, EnderecoService>();
                    services.AddTransient<FrmPrincipal>();
                    services.AddTransient<FrmCliente>();
                    services.AddTransient<FrmCadastroCliente>();
                });
        }
    }
}
