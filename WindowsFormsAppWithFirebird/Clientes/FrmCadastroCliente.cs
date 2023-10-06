using System;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppWithFirebird.Domain.Entities;
using WindowsFormsAppWithFirebird.Domain.Extensions;
using WindowsFormsAppWithFirebird.Domain.Services.Interfaces;
using WindowsFormsAppWithFirebird.ViaCEP;

namespace WindowsFormsAppWithFirebird.Clientes
{
    public partial class FrmCadastroCliente : Form
    {
        public readonly IClienteService _clienteService;
        public readonly IEnderecoService _enderecoService;

        private Endereco _endereco;
        private Cliente _cliente;
        private WindowsFormsAppWithFirebird.ViaCEP.ApiViaCEP apiViaCep = new WindowsFormsAppWithFirebird.ViaCEP.ApiViaCEP();
        private Guid _clienteId;

        public FrmCadastroCliente(IClienteService clienteService, IEnderecoService enderecoService, Guid clienteId) 
        {
            _clienteService = clienteService;
            _enderecoService = enderecoService;
            _clienteId = clienteId;
            InitializeComponent();
        }

        private void btnGravarCliente_Click(object sender, EventArgs e)
        {
            InstanciarClienteDaTela();
            InstanciarEnderecoDaTela();

            try
            {
                if (_clienteId == Guid.Empty)
                {
                    _clienteService.Add(_cliente, _endereco);
                }
                else
                {
                    _clienteService.Update(_cliente, _endereco);
                }
                MessageBox.Show("Cliente gravado com sucesso");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void btnPesquisarCEP_Click(object sender, EventArgs e)
        {
            if (ValidarCEP(txtCEP.Text))
            {
                BuscaLogradouro(txtCEP.Text);
            }
        }

        private bool ValidarCEP(string cep)
        {
            return !cep.IsNullOrWhiteSpace() && cep.RemoverCaracteres().Length == 8;
        }

        private async Task BuscaLogradouro(string cep)
        {
            try
            {
                RetornoViaCepParaEndereco(await apiViaCep.BuscaLogradouro(cep));                
            }
            catch (Exception error)
            {
                MessageBox.Show("Erro na consulta de cep: " + error.Message);
            }
        }

        private void InstanciarClienteDaTela()
        {
            if (_cliente == null)
            {
                _cliente = new Cliente(
                    txtNome.Text,
                    txtDocumento.Text,
                    txtEmail.Text
                );
            }
            else
            {
                _cliente.Nome = txtNome.Text;
                _cliente.Documento = txtDocumento.Text;
                _cliente.Email = txtEmail.Text;
            }
        }

        private void InstanciarEnderecoDaTela()
        {
            if (_endereco == null)
            {
                _endereco = new Endereco(
                    txtCEP.Text,
                    txtLogradouro.Text,
                    txtComplemento.Text,
                    txtNumero.Text,
                    txtBairro.Text,
                    txtCidade.Text,
                    txtEstado.Text
                );
            }
            else
            {
                _endereco.CEP = txtCEP.Text;
                _endereco.Logradouro = txtLogradouro.Text;
                _endereco.Complemento = txtComplemento.Text;
                _endereco.Numero = txtNumero.Text;
                _endereco.Bairro = txtBairro.Text;
                _endereco.Cidade = txtCidade.Text;
                _endereco.Estado = txtEstado.Text;
                _endereco.ClienteId = _cliente.Id;
            }
        }

        private void RetornoViaCepParaEndereco(CepResponse endereco)
        {
            _endereco = new Endereco(
                endereco.Cep,
                endereco.Logradouro,
                endereco.Complemento,
                "",
                endereco.Bairro,
                endereco.Localidade,
                endereco.Uf
            );
            CarregarCamposTela();
        }

        private void CarregarCamposTela()
        {
            if (_cliente != null)
            {
                txtNome.Text = _cliente.Nome;
                txtDocumento.Text = _cliente.Documento;
                txtEmail.Text = _cliente.Email;
            }

            if (_endereco != null)
            {
                txtCEP.Text = _endereco.CEP;
                txtLogradouro.Text = _endereco.Logradouro;
                txtComplemento.Text = _endereco.Complemento;
                txtNumero.Text = _endereco.Numero;
                txtBairro.Text = _endereco.Bairro;
                txtCidade.Text = _endereco.Cidade;
                txtEstado.Text = _endereco.Estado;
            }
        }

        private void txtDocumento_Leave(object sender, EventArgs e)
        {
            if (!txtDocumento.Text.EhValido())
            {                
                txtDocumento.Focus();
                MessageBox.Show("O documento informado não é um documento válido.");
            }

            txtDocumento.Text = txtDocumento.Text.FormatarCPF();
            txtClientes_Leave(sender, e);
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            if (!txtCEP.Text.RemoverCaracteres().Equals(_endereco.CEP.RemoverCaracteres()))
            {
                btnPesquisarCEP_Click(sender, e);
            }
        }

        private void txtClientes_Leave(object sender, EventArgs e)
        {
            InstanciarClienteDaTela();
        }

        private void FrmCadastroCliente_Load(object sender, EventArgs e)
        {
            if (_clienteId != Guid.Empty)
            {
                _cliente = _clienteService.GetById(_clienteId);
                _endereco = _enderecoService.GetByClienteId(_clienteId);

                CarregarCamposTela();                
            }
            else
            {
                tabControl1.TabPages.Remove(tabHistoricoAlteracoes);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void CarregarGrid()
        {
            var clientes = _clienteService.GetHistoricoDeAlteracoes(_clienteId);

            clienteHistoricoBindingSource.DataSource = clientes;
            grdHistoricoAlteracoes.DataSource = clienteHistoricoBindingSource;
        }

        private void tabHistoricoAlteracoes_Enter(object sender, EventArgs e)
        {
            CarregarGrid();
        }
    }
}
