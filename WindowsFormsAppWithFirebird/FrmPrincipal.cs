using System;
using System.Windows.Forms;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;
using WindowsFormsAppWithFirebird.Clientes;

namespace WindowsFormsAppWithFirebird
{
    public partial class FrmPrincipal : Form
    {
        public readonly IClienteService _clienteService;
        public readonly IEnderecoService _enderecoService;

        public FrmPrincipal(IClienteService clienteService, IEnderecoService enderecoService)
        {
            _clienteService = clienteService;
            _enderecoService = enderecoService;
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chamarTelaClientes();
        }        

        private void btnClientes_Click(object sender, EventArgs e)
        {
            this.chamarTelaClientes();
        }

        private void chamarTelaClientes()
        {
            FrmCliente frm = new FrmCliente(_clienteService, _enderecoService);
            frm.ShowDialog();
        }
    }
}
