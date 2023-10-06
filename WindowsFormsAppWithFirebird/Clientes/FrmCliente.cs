using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;

namespace WindowsFormsAppWithFirebird.Clientes
{
    public partial class FrmCliente : Form
    {
        public readonly IClienteService _clienteService;
        public readonly IEnderecoService _enderecoService;

        private Guid _clienteId;

        public FrmCliente(IClienteService clienteService, IEnderecoService enderecoService)
        {
            _clienteService = clienteService;
            _enderecoService = enderecoService;

            _clienteId = Guid.Empty;
            InitializeComponent();
        }

        void CarregarCadastroDoCliente()
        {
            FrmCadastroCliente frm = new FrmCadastroCliente(_clienteService, _enderecoService, _clienteId);
            frm.ShowDialog();

            CarregarGrid();
        }

        void CarregarGrid()
        {
            var clientes = _clienteService.GetAll().OrderBy(c => c.Nome);

            clienteBindingSource.DataSource = clientes;
            grdClientes.DataSource = clienteBindingSource;
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            _clienteId = Guid.Empty;
            CarregarCadastroDoCliente();
        }

        private void grdClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                EditarCliente();
            }
        }

        private void grdClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditarCliente();
        }

        void EditarCliente()
        {
            var clienteAtual = grdClientes.CurrentRow.Cells["Id"].Value;
            if (clienteAtual != null)
            {
                _clienteId = new Guid(clienteAtual.ToString());
                CarregarCadastroDoCliente();
            }
        }

        private void btnRelacaoAlteracoes_Click(object sender, EventArgs e)
        {
            FrmRelatorioClientes frm = new FrmRelatorioClientes(_clienteService);
            frm.ShowDialog();
        }
    }
}
