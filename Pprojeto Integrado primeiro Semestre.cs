//============================================================================
// Name        : PIM
// Author      : Grupo PIM
// Version     : 1.0
// Copyright   :
// Description : Sistema de gestão para startup em logística
//============================================================================

#include <iostream>
#include <list>
#include <locale>
#include <locale.h>
#include <string>

using namespace std;

// Declarando structs de Usuário, Cliente e Produto
struct Usuario
{
    int id;
    string usuario;
    string senha;

    void exibirInformacoesDoUsuario()
    {
        cout << "ID: " << id << " | Usuário: " << usuario << endl;
        cout << "-----------------------------------------------" << endl;
    }
};

struct Cliente
{
    int id;
    string nome;
    string empresa;
    string numeroContato;
    string emailContato;
    string ramoAtuacao;
    string cnpj;

    void exibirInformacoesDoCliente()
    {
        cout << "-----------------------------------------------" << endl;
        cout << "ID: " << id << " | Nome: " << nome << " | Empresa: " << empresa << endl;
        cout << "Número de contato: " << numeroContato << " | E-mail de contato: " << emailContato << endl;
        cout << "Ramo de atuação: " << ramoAtuacao << " | CNPJ: " << cnpj << endl;
        cout << "-----------------------------------------------" << endl;
    }
};

struct Produto
{
    int id;
    string nome;
    string tipo;
    string producao;
    string dataDoRecebimento = " - ";
    string dataDaSaida = " - ";
    string dataDaEntrega = " - ";
    string periodoEmEstoque = " - ";
    int quantidade;
    int tamanhoDoLoteEmMetros;
    float pesoDoLoteEmKg;

    void exibirInformacoesDoProduto()
    {
        cout << "-----------------------------------------------" << endl;
        cout << "ID: " << id << " | Nome: " << nome << endl;
        cout << "Quantidade: " << quantidade << endl;
        cout << "Produção: " << producao << " | Tipo: " << tipo << endl;
        cout << "Tamanho do lote (em metros): " << tamanhoDoLoteEmMetros << " | Peso do lote (em Kg): " << pesoDoLoteEmKg << endl;
        cout << "Data do recebimento do lote:" << dataDoRecebimento << " | Data da saída do lote: " << dataDaSaida << endl;
        cout << "Data da entrega: " << dataDaEntrega << " | Período em estoque: " << periodoEmEstoque << endl;
        cout << "-----------------------------------------------" << endl;
    }
};

// Declarando variável no escopo global de struct para armazenar o usuário logado no sistema
Usuario usuarioLogado;

// Declarando listas no escopo global para armazenar cada tipo composto (struct) de usuário, cliente e produto
list<Usuario> usuariosCadastrados;
list<Cliente> clientesCadastrados;
list<Produto> produtosCadastrados;

// Declarando variáveis no escopo global do tipo integer para gerar identificadores de cada tipo composto (struct de usuário, cliente e produto
int idUsuario = 0;
int idCliente = 0;
int idProduto = 0;

//============================================================================
// Inicialização das funções
//
// Funcionários | Login
void inicial(string mensagem = "----------- BEM VINDO -----------\n\nSelecione uma das opções abaixo para navegar:");
void acesso();
void acessoInvalido();
void validarAcesso(string usuario, string senha);
void cadastro(string mensagem = "----------- CADASTRO -----------");
void validarCadastro(string nomeUsuario, string senha);
void cadastroOpcaoInvalida();
void acessoInterno();
void acessoInternoOpcoes(string mensagem = "Selecione uma opção para navegar:");
void listarUsuariosCadastrados();
string informarSenha();
Usuario buscarUsuario(int id = 0, string nomeUsuario = "");
// Clientes
void gerirClientes();
void gerirClientesOpcoes();
void cadastrarCliente();
void editarCliente(Cliente cliente);
void finalizarEdicao(Cliente cliente);
void selecionarCliente(bool edicao = false, bool remocao = false);
void removerCliente(Cliente cliente);
void listarClientes();
void selecionarClientePorId(bool edicao = false, bool remocao = false);
void selecionarClientePorCNPJ(bool edicao = false, bool remocao = false);
void atualizarClientes(Cliente cliente);
Cliente buscarCliente(int id = 0, string cnpj = "");
// Produtos
void gerirProdutos();
void cadastrarProduto();
void editarProduto(Produto produto);
void finalizarEdicaoProduto(Produto produto);
void selecionarProduto(bool edicao = false, bool remocao = false);
void removerProduto(Produto produto);
void listarProdutos();
void selecionarProdutoPorId(bool edicao = false, bool remocao = false);
void selecionarProdutoPorNome(bool edicao = false, bool remocao = false);
void gerirProdutos();
void atualizarProdutos(Produto produto);
Produto buscarProduto(int id = 0, string nome = "");
// Relatórios
void gerarRelatorios();
//
//============================================================================


int retornarOpcaoValida(string opcao)
{
    return atoi(opcao.c_str());
}

int main()
{
    setlocale(LC_ALL, "Portuguese");

    inicial();

    return 0;
}

// =============================================================================================================
// ================================    Login | Funcionários | Menu principal    ================================

void inicial(string mensagem)
{
    cout << mensagem << endl;

    string opcao = "";

    cout << "\n[1] Acessar o sistema\n[2] Realizar cadastro de funcionário\n" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            acesso();
            break;
        case 2:
            cadastro();
            break;
        default:
            inicial("Opção inválida. Por gentileza, selecione uma das opções abaixo para navegar:");
    }

}

void cadastro(string mensagem)
{
    cout << mensagem << endl;

    string usuario = "";
    string senha = "";

    cout << "Informe nome de usuário para cadastro: " << endl;

    cin >> usuario;

    senha = informarSenha();

    validarCadastro(usuario, senha);
}

void validarCadastro(string nomeUsuario, string senha)
{
    Usuario usuariojaCadastrado = buscarUsuario(0, nomeUsuario);

    if (usuariojaCadastrado.id == 0)
    {
        idUsuario++;

        Usuario novoUsuario;
        novoUsuario.id = idUsuario;
        novoUsuario.usuario = nomeUsuario;
        novoUsuario.senha = senha;

        usuariosCadastrados.push_back(novoUsuario);

        cout << "-----------------------------" << endl;

        cout << "Usuário " << novoUsuario.usuario << " cadastrado com êxito." << endl;

        cout << "-----------------------------" << endl;

        inicial("Selecione uma das opções abaixo para navegar:");
    }
    else
    {
        string opcao = "";

        cout << "Já há um usuário cadastrado com este nome. Deseja tentar novamente ou retornar para tela inicial?" << endl;

        cout << "[1] Tentar cadastro novamente\n[2] Retornar para tela inicial\n" << endl;

        cin >> opcao;

        switch (retornarOpcaoValida(opcao))
        {
            case 1:
                cadastro();
                break;
            case 2:
                inicial();
                break;
            default:
                cadastroOpcaoInvalida();
        }
    }
}

void cadastroOpcaoInvalida()
{
    cout << "Opção inválida. Deseja realizar novo cadastro ou retornar para tela inicial?" << endl;

    string opcao = "";

    cout << "[1] Tentar cadastro novamente\n[2] Retornar para tela inicial" << endl;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            inicial();
            break;
        case 2:
            cadastro();
            break;
        default:
            cadastroOpcaoInvalida();
    }
}

string informarSenha()
{

    string senha = "";
    string confirmacaoDeSenha = "";

    cout << "Informe sua senha: " << endl;

    cin >> senha;

    cout << "Confirme sua senha: " << endl;

    cin >> confirmacaoDeSenha;

    if (confirmacaoDeSenha == senha)
    {
        return senha;
    }
    else
    {
        cout << "Senhas não coicidem. Por gentileza, informe novamente sua senha." << endl;
        informarSenha();
    }

    return "";
}

void acesso()
{

    cout << "----------- LOGIN -----------" << endl;

    string usuario = "";
    string senha = "";

    cout << "Usuário: ";
    cin >> usuario;

    cout << "Senha: ";
    cin >> senha;

    validarAcesso(usuario, senha);
}

void validarAcesso(string usuario, string senha)
{
    Usuario usuarioExistente = buscarUsuario(0, usuario);

    if (usuarioExistente.id == 0)
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Usuário não encontrado." << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        acessoInvalido();
    }

    if (usuarioExistente.senha == senha)
    {
        usuarioLogado.id = usuarioExistente.id;
        usuarioLogado.usuario = usuarioExistente.usuario;
        acessoInterno();
    }
    else
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Senha inválida." << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        acessoInvalido();
    }

}

void acessoInvalido()
{

    cout << "Deseja tentar acesso novamente ou retornar ao menu principal?" << endl;

    cout << "[1] Tentar acesso novamente\n[2] Retornar ao menu principal" << endl;

    string opcao = "";

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            acesso();
        case 2:
            inicial("Selecione uma das opções abaixo para navegar:");
        default:
            acessoInvalido();
    }

}

void acessoInterno()
{

    cout << "\n\n\n\n----------- Sistema de gestão -----------\n" << endl;

    cout << "Bem-vindo " << usuarioLogado.usuario << "! Selecione uma opção para navegar:" << endl;

    acessoInternoOpcoes(" ");

}

void acessoInternoOpcoes(string mensagem)
{
    cout << mensagem << endl;

    string opcao = "";

    cout << "\n[1] Listar usuários cadastrados\n[2] Gerir clientes\n[3] Gerir produtos\n[4] Gerar relatórios\n[5] Sair" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            listarUsuariosCadastrados();
            acessoInternoOpcoes();
            break;
        case 2:
            gerirClientes();
        case 3:
            gerirProdutos();
        case 4:
            gerarRelatorios();
        case 5:
            inicial();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            acessoInternoOpcoes();
    }
}

void listarUsuariosCadastrados()
{
    cout << "\n\n----------- Usuários cadastrados -----------" << endl;
    for (Usuario usuario : usuariosCadastrados)
    {
        usuario.exibirInformacoesDoUsuario();
    }
    cout << "\n" << endl;
}

Usuario buscarUsuario(int id, string nomeUsuario)
{
    Usuario buscaUsuario;
    buscaUsuario.id = 0;
    for (Usuario usuario : usuariosCadastrados)
    {
        if (
            (id != 0 && usuario.id == id)
            || (nomeUsuario != "" && usuario.usuario == nomeUsuario)
        )
        {
            buscaUsuario = usuario;
        }
    }

    return buscaUsuario;
}
// ============================================================================================================

// ============================================================================================================
// ==============================================    Clientes    ==============================================

void gerirClientes()
{
    cout << "\n\n----------- Gerir clientes ----------- " << endl;

    cout << "\n\n Selecione uma das opções:\n" << endl;

    string opcao = "";

    cout << "\n\n[1] Cadastrar cliente\n[2] Editar cliente\n[3] Remover cliente\n[4] Listar clientes cadastrados\n[5] Retornar ao menu principal" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            cadastrarCliente();
        case 2:
            selecionarCliente(true, false);
        case 3:
            selecionarCliente(false, true);
        case 4:
            listarClientes();
        case 5:
            acessoInterno();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            gerirClientesOpcoes();
    }

}

void gerirClientesOpcoes()
{
    cout << "\n\n Selecione uma das opções:\n" << endl;

    string opcao = "";

    cout << "\n\n[1] Cadastrar cliente\n[2] Editar cliente\n[3] Remover cliente\n[4] Listar clientes cadastrados\n[5] Retornar ao menu principal" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            cadastrarCliente();
        case 2:
            selecionarCliente(true, false);
        case 3:
            selecionarCliente(false, true);
        case 4:
            listarClientes();
        case 5:
            acessoInterno();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            gerirClientesOpcoes();
    }
}

void cadastrarCliente()
{

    Cliente novoCliente;

    string nome = "";
    string empresa = "";
    string numeroContato = "";
    string emailContato = "";
    string ramoAtuacao = "";
    string cnpj = "";

    cout << "Informe o nome do cliente: " << endl;

    cin.ignore();
    getline(cin, nome, '\n');

    cout << "Informe a empresa: " << endl;

    cin.ignore();
    getline(cin, empresa, '\n');

    cout << "Informe o número de contato: " << endl;

    cin.ignore();
    getline(cin, numeroContato, '\n');

    cout << "Informe o e-mail de contato: " << endl;

    cin.ignore();
    getline(cin, emailContato, '\n');

    cout << "Informe o ramo de atuação: " << endl;

    cin.ignore();
    getline(cin, ramoAtuacao, '\n');

    cout << "Informe o CNPJ: " << endl;

    cin.ignore();
    getline(cin, cnpj, '\n');

    idCliente++;

    novoCliente.id = idCliente;
    novoCliente.nome = nome;
    novoCliente.empresa = empresa;
    novoCliente.numeroContato = numeroContato;
    novoCliente.emailContato = emailContato;
    novoCliente.ramoAtuacao = ramoAtuacao;
    novoCliente.cnpj = cnpj;

    clientesCadastrados.push_back(novoCliente);

    cout << "-----------------------------" << endl;

    cout << "Cliente " << novoCliente.nome << " cadastrado com êxito." << endl;

    cout << "-----------------------------" << endl;


    gerirClientes();

}

void selecionarCliente(bool edicao, bool remocao)
{

    string acao = "";

    if (edicao == true)
    {
        acao = "edição";
    }

    if (remocao == true)
    {
        acao = "remoção";
    }

    cout << "Deseja selecionar o cliente para " << acao << " por ID ou por CNPJ?" << endl;

    cout << "\n[1] ID\n[2] CNPJ\n[3] Retornar ao menu anterior" << endl;

    string opcao = "";

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            selecionarClientePorId(edicao, remocao);
        case 2:
            selecionarClientePorCNPJ(edicao, remocao);
        case 3:
            gerirClientesOpcoes();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            selecionarCliente(edicao, remocao);
    }
}

void selecionarClientePorId(bool edicao, bool remocao)
{

    Cliente cliente;

    cout << "\nInforme o ID do cliente: " << endl;
    int id = 0;
    cin >> id;

    cliente = buscarCliente(id);

    if (cliente.id != 0)
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Cliente encontrado!" << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        if (edicao == true)
        {
            editarCliente(cliente);
        }
        if (remocao == true)
        {
            removerCliente(cliente);
        }
    }
    else
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Cliente não encontrado." << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        selecionarCliente(edicao, remocao);
    }
}

void selecionarClientePorCNPJ(bool edicao, bool remocao)
{

    Cliente cliente;

    cout << "\nInforme o CNPJ do cliente: " << endl;

    string cnpj = "";
    cin >> cnpj;

    cliente = buscarCliente(0, cnpj);

    if (cliente.id != 0)
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Cliente encontrado!" << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        if (edicao == true)
        {
            editarCliente(cliente);
        }
        if (remocao == true)
        {
            removerCliente(cliente);
        }
    }
    else
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Cliente não encontrado." << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        selecionarCliente(edicao, remocao);
    }
}

void editarCliente(Cliente cliente)
{

    cout << "Informe o dado do cliente que deseja editar: " << endl;

    cout << "[1] Nome\n[2] Empresa\n[3] Número de contato\n[4] E-mail de contato\n[5] Ramo de atuação\n[6] CNPJ\n[7] Voltar ao menu anterior" << endl;

    string opcao = "";

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            cout << "Informe o nome para o cliente: " << endl;
            cin.ignore();
            getline(cin, cliente.nome, '\n');
            finalizarEdicao(cliente);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Cliente editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
        case 2:
            cout << "Informe a empresa para o cliente: " << endl;
            cin.ignore();
            getline(cin, cliente.empresa, '\n');
            finalizarEdicao(cliente);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Cliente editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
        case 3:
            cout << "Informe o número de contato para o cliente: " << endl;
            cin.ignore();
            getline(cin, cliente.numeroContato, '\n');
            finalizarEdicao(cliente);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Cliente editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
        case 4:
            cout << "Informe o e-mail de contato para o cliente: " << endl;
            cin.ignore();
            getline(cin, cliente.emailContato, '\n');
            finalizarEdicao(cliente);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Cliente editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
        case 5:
            cout << "Informe o ramo de atuação para o cliente: " << endl;
            cin.ignore();
            getline(cin, cliente.ramoAtuacao, '\n');
            finalizarEdicao(cliente);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Cliente editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
        case 6:
            cout << "Informe o CNPJ para o cliente: " << endl;
            cin.ignore();
            getline(cin, cliente.cnpj, '\n');
            finalizarEdicao(cliente);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Cliente editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
        case 7:
            selecionarCliente(true, false);
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarCliente(cliente);
    }

}

void finalizarEdicao(Cliente cliente)
{
    atualizarClientes(cliente);
    clientesCadastrados.push_back(cliente);
}

void atualizarClientes(Cliente cliente)
{
    list<Cliente> tempClientes;
    for (Cliente clienteCadastrado : clientesCadastrados)
    {
        if (cliente.id != clienteCadastrado.id)
        {
            tempClientes.push_back(clienteCadastrado);
        }
    }
    clientesCadastrados.clear();
    for (Cliente clienteAtualizado : tempClientes)
    {
        clientesCadastrados.push_back(clienteAtualizado);
    }
    tempClientes.clear();
}

void removerCliente(Cliente cliente)
{

    cout << "Deseja realmente remover o cliente " << cliente.nome << "?" << endl;

    string opcao = "";

    cout << "[1] Sim\n[2] Não" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            atualizarClientes(cliente);
            gerirClientes();
        case 2:
            gerirClientes();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            removerCliente(cliente);
    }

}

void listarClientes()
{
    cout << "\n\n----------- Clientes cadastrados -----------" << endl;
    for (Cliente cliente : clientesCadastrados)
    {
        cliente.exibirInformacoesDoCliente();
    }
    cout << "\n" << endl;

    gerirClientes();
}

Cliente buscarCliente(int id, string cnpj)
{
    Cliente buscaCliente;
    buscaCliente.id = 0;
    for (Cliente cliente : clientesCadastrados)
    {
        if (
            (id != 0 && cliente.id == id)
            || (cnpj != "" && cliente.cnpj == cnpj)
        )
        {
            buscaCliente = cliente;
        }
    }

    return buscaCliente;
}

// ============================================================================================================

// ============================================================================================================
// ==============================================    Produtos    ==============================================


void gerirProdutos()
{
    cout << "\n\n----------- Gerir produtos ----------- " << endl;

    cout << "\n\n Selecione uma das opções:\n" << endl;

    string opcao = "";

    cout << "\n\n[1] Cadastrar produto\n[2] Editar produto\n[3] Remover produto\n[4] Listar produtos cadastrados\n[5] Retornar ao menu principal" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            cadastrarProduto();
        case 2:
            selecionarProduto(true, false);
        case 3:
            selecionarProduto(false, true);
        case 4:
            listarProdutos();
        case 5:
            acessoInterno();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            gerirProdutos();
    }

}

void cadastrarProduto()
{

    Produto novoProduto;

    string nome;
    string tipo;
    string producao;
    string dataDoRecebimento = "";
    int diaDeRecebimento = 0;
    int mesDeRecebimento = 0;
    int anoDeRecebimento = 0;
    int quantidade;
    int tamanhoDoLoteEmMetros;
    float pesoDoLoteEmKg;

    cout << "Informe o nome do produto: " << endl;

    cin.ignore();
    getline(cin, nome, '\n');

    cout << "Informe o tipo do produto: " << endl;

    cin.ignore();
    getline(cin, tipo, '\n');

    cout << "Informe se a produção é nacional ou internacional:" << endl;

    cin.ignore();
    getline(cin, producao, '\n');

    cout << "Informe a data de recebimento do produto: " << endl;

    cout << "Dia: ";

    cin >> diaDeRecebimento;

    cout << "\nMês: ";

    cin >> mesDeRecebimento;

    cout << "\nAno: ";

    cin >> anoDeRecebimento;

    dataDoRecebimento = to_string(diaDeRecebimento) + "/" + to_string(mesDeRecebimento) + "/" + to_string(anoDeRecebimento);

    cout << "Informe a quantidade do produto recebida: " << endl;

    cin >> quantidade;

    cout << "Informe o tamanho do lote em metros: " << endl;

    cin >> tamanhoDoLoteEmMetros;

    cout << "Informe o peso do lote em Kg: " << endl;

    cin >> pesoDoLoteEmKg;

    idProduto++;

    novoProduto.id = idProduto;
    novoProduto.nome = nome;
    novoProduto.tipo = tipo;
    novoProduto.producao = producao;
    novoProduto.dataDoRecebimento = dataDoRecebimento;
    novoProduto.quantidade = quantidade;
    novoProduto.tamanhoDoLoteEmMetros = tamanhoDoLoteEmMetros;
    novoProduto.pesoDoLoteEmKg = pesoDoLoteEmKg;

    produtosCadastrados.push_back(novoProduto);

    cout << "-----------------------------" << endl;

    cout << "Produto " << novoProduto.nome << " cadastrado com êxito." << endl;

    cout << "-----------------------------" << endl;


    gerirProdutos();

}

void selecionarProduto(bool edicao, bool remocao)
{

    string acao = "";

    if (edicao == true)
    {
        acao = "edição";
    }

    if (remocao == true)
    {
        acao = "remoção";
    }

    cout << "Deseja selecionar o produto para " << acao << " por ID ou por nome?" << endl;

    cout << "\n[1] ID\n[2] Nome\n[3] Retornar ao menu anterior" << endl;

    string opcao = "";

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            selecionarProdutoPorId(edicao, remocao);
        case 2:
            selecionarProdutoPorNome(edicao, remocao);
        case 3:
            gerirProdutos();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            selecionarProduto(edicao, remocao);
    }
}

void selecionarProdutoPorId(bool edicao, bool remocao)
{

    Produto produto;

    cout << "\nInforme o ID do produto: " << endl;
    int id = 0;
    cin >> id;

    produto = buscarProduto(id);

    if (produto.id != 0)
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Produto encontrado!" << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        if (edicao == true)
        {
            editarProduto(produto);
        }
        if (remocao == true)
        {
            removerProduto(produto);
        }
    }
    else
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Produto não encontrado." << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        selecionarProduto(edicao, remocao);
    }
}

void selecionarProdutoPorNome(bool edicao, bool remocao)
{

    Produto produto;

    cout << "\nInforme o nome do produto: " << endl;

    string nome = "";
    cin >> nome;

    produto = buscarProduto(0, nome);

    if (produto.id != 0)
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Produto encontrado!" << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        if (edicao == true)
        {
            editarProduto(produto);
        }
        if (remocao == true)
        {
            removerProduto(produto);
        }
    }
    else
    {
        cout << "-----------------------------------------------------------" << endl;
        cout << "Produto não encontrado." << endl;
        cout << "-----------------------------------------------------------\n" << endl;
        selecionarProduto(edicao, remocao);
    }
}

void editarProduto(Produto produto)
{

    cout << "Informe o dado do produto que deseja editar: " << endl;

    cout << "[1] Nome\n[2] Tipo\n[3] Produção\n[4] Data do recebimento\n[5] Data da saída\n[6] Data da entrega" << endl;
    cout << "[7] Quantidade\n[8] Tamanho do lote em metros\n[9] Peso do lote em Kg\n[10] Voltar ao menu anterior" << endl;

    string opcao = "";

    cin >> opcao;

    int dia = 0;
    int mes = 0;
    int ano = 0;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            cout << "Informe o nome para o produto: " << endl;
            cin.ignore();
            getline(cin, produto.nome, '\n');
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 2:
            cout << "Informe o tipo para o produto: " << endl;
            cin.ignore();
            getline(cin, produto.tipo, '\n');
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 3:
            cout << "Informe a produção para o produto: " << endl;
            cin.ignore();
            getline(cin, produto.producao, '\n');
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 4:
            cout << "Informe a data do recebimento do produto: " << endl;
            cout << "Dia: ";
            cin >> dia;
            cout << "\nMês: ";
            cin >> mes;
            cout << "\nAno: ";
            cin >> ano;
            produto.dataDoRecebimento = to_string(dia) + "/" + to_string(mes) + "/" + to_string(ano);
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 5:
            cout << "Informe a data da saída do produto: " << endl;
            cout << "Dia: ";
            cin >> dia;
            cout << "\nMês: ";
            cin >> mes;
            cout << "\nAno: ";
            cin >> ano;
            produto.dataDaSaida = to_string(dia) + "/" + to_string(mes) + "/" + to_string(ano);
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 6:
            cout << "Informe a data da entrega do produto: " << endl;
            cout << "Dia: ";
            cin >> dia;
            cout << "\nMês: ";
            cin >> mes;
            cout << "\nAno: ";
            cin >> ano;
            produto.dataDaEntrega = to_string(dia) + "/" + to_string(mes) + "/" + to_string(ano);
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 7:
            cout << "Informe a quantidade para o produto: " << endl;
            cin >> produto.quantidade;
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 8:
            cout << "Informe o tamanho em metros do lote para o produto: " << endl;
            cin >> produto.tamanhoDoLoteEmMetros;
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 9:
            cout << "Informe o peso do lote em Kg para o produto: " << endl;
            cin >> produto.pesoDoLoteEmKg;
            finalizarEdicaoProduto(produto);
            cout << "-----------------------------------------------------------" << endl;
            cout << "Produto editado com êxito!" << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
        case 10:
            selecionarProduto(true, false);
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            editarProduto(produto);
    }

}

void finalizarEdicaoProduto(Produto produto)
{
    atualizarProdutos(produto);
    produtosCadastrados.push_back(produto);
}

void atualizarProdutos(Produto produto)
{
    list<Produto> tempProdutos;
    for (Produto produtoCadastrado : produtosCadastrados)
    {
        if (produto.id != produtoCadastrado.id)
        {
            tempProdutos.push_back(produtoCadastrado);
        }
    }
    produtosCadastrados.clear();
    for (Produto produtoAtualizado : tempProdutos)
    {
        produtosCadastrados.push_back(produtoAtualizado);
    }
    tempProdutos.clear();
}

void removerProduto(Produto produto)
{

    cout << "Deseja realmente remover o produto " << produto.nome << "?" << endl;

    string opcao = "";

    cout << "[1] Sim\n[2] Não" << endl;

    cin >> opcao;

    switch (retornarOpcaoValida(opcao))
    {
        case 1:
            atualizarProdutos(produto);
            gerirProdutos();
        case 2:
            gerirProdutos();
        default:
            cout << "-----------------------------------------------------------" << endl;
            cout << "Opção inválida. Por gentileza, tente novamente." << endl;
            cout << "-----------------------------------------------------------\n" << endl;
            removerProduto(produto);
    }

}

void listarProdutos()
{
    cout << "\n\n----------- Produtos cadastrados -----------" << endl;
    for (Produto produto: produtosCadastrados)
    {
        produto.exibirInformacoesDoProduto();
    }
    cout << "\n" << endl;

    gerirProdutos();
}

Produto buscarProduto(int id, string nome)
{
    Produto buscaProduto;
    buscaProduto.id = 0;
    for (Produto produto : produtosCadastrados)
    {
        if (
            (id != 0 && produto.id == id)
            || (nome != "" && produto.nome == nome)
        )
        {
            buscaProduto = produto;
        }
    }

    return buscaProduto;
}

// ============================================================================================================

// ============================================================================================================
// ==============================================    Relatórios    ==============================================

void gerarRelatorios()
{
    cout << "Em construção" << endl;

    gerirProdutos();
}
