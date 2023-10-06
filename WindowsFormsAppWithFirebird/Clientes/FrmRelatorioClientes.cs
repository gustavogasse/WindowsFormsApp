using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;

namespace WindowsFormsAppWithFirebird.Clientes
{
    public partial class FrmRelatorioClientes : Form
    {
        public readonly IClienteService _clienteService;        

        public FrmRelatorioClientes(IClienteService clienteService)
        {
            _clienteService = clienteService;

            InitializeComponent();
        }

        void CarregarGrid()
        {
            var clientes = _clienteService.GetAllHistoricoDeAlteracoes().OrderBy(c => c.Nome);

            clienteRelatorioAlteracaoBindingSource.DataSource = clientes;
            grdClientes.DataSource = clienteRelatorioAlteracaoBindingSource;
        }

        private void FrmRelatorioClientes_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }
    }
}
