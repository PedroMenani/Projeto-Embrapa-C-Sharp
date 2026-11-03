using Cadastro_de_usuario.Data;
using Cadastro_de_usuario.Models;
using MySql.Data.MySqlClient;

var repositorio = new UsuarioRepositorio(new Conexao());

try
{
    if (!Login(repositorio)) return;

    MenuPrincipal(repositorio);

}
catch(MySqlException ex)
{
    System.Console.WriteLine($"Erro de Banco de dados: {ex.Message}");
    Console.WriteLine("Verifique a conexão");
}

static bool Login(UsuarioRepositorio repo)
{
    for(int tentativa = 1; tentativa <= 3; tentativa ++)
    {
        Console.Clear();
        Console.WriteLine("=== LOGIN ===");
        string email = Ler("Email: ");
        string senha = Ler("Senha: ");

        var usuario = repo.Autenticar(email,senha);
        if (usuario != null)
        {
            Console.WriteLine($"Bem vindo, {usuario.Nome}");
            return true;
        }
        Console.WriteLine($"Email ou senha invalidos ({tentativa}/3)");
        Pausar();
    }
    Console.WriteLine($"Numero maximo de tentativas atingidos");
    return false;
}

static void MenuPrincipal(UsuarioRepositorio repo)
{


    //IF (USUARIO.DATA.CARGO == ADM){
    while(true)
    {
        Console.Clear();
        Console.WriteLine("   === ADMINISTRADOR === ");
        Console.WriteLine("   1- CADASTRAR USUÁRIOS ");
        Console.WriteLine("   2- VER USUÁRIOS       ");
        Console.WriteLine("   3- EDITAR USUÁRIOS    ");
        Console.WriteLine("   4- EXCLUIR USUÁRIOS   ");
        Console.WriteLine("   0- SAIR               ");
        Console.WriteLine("OPÇÃO:");

        switch (Console.ReadLine())
        {
            case "1": CadastrarUsuario(repo); break;
            case "2": MostrarUsuario(repo); break;
            case "3": EditarUsuario(repo); break;
            case "4": ExcluirUsuario(repo); break;
            case "0": return;
            default: Console.WriteLine("Opção Inválida"); Pausar(); break;

        }

    } 
    //}

    //ELSE IF (USUARIOS.DATA.CARGO == PEQUISADOR){
    

        // Console.Clear();
        // Console.WriteLine("   === PESQUISADOR === ");
        // Console.WriteLine("   1- CRIAR AÇÃO       ");
        // Console.WriteLine("   2- VER MINHAS AÇÕES ");
        // Console.WriteLine("   0- SAIR             ");
        // Console.WriteLine("OPÇÃO:");

        //switch (Console.ReadLine())
        // {
        //     case "1": CriarAcao(repo); break;
        //     case "2": MostrarAcao(repo); break;
        //     case "0": return;
        //     default: Console.WriteLine("Opção Inválida"); Pausar(); break;
        // }
    
    //}

    //ELSE IF (USUARIOS.DATA.CARGO == VALIDADOR){
    
        // Console.Clear();
        // Console.WriteLine("   === VALIDADOR ===    ");
        // Console.WriteLine("   1- AVALIAR           ");
        // Console.WriteLine("   2- MINHAS AVALIAÇÕES ");
        // Console.WriteLine("   0- SAIR              ");
        // Console.WriteLine("OPÇÃO:");

        //switch (Console.ReadLine())
        // {
        //     case "1": MostrarAcoes(repo); break;
        //     case "2": MostrarAvaliacoes(repo); break;
        //     case "0": return;
        //     default: Console.WriteLine("Opção Inválida"); Pausar(); break;
        // }
    
    //}

    //ELSE IF(USUARIOS.DATA.CARGO == COMITE){
    
        // Console.Clear();
        // Console.WriteLine("   === COMITE ===  ");
        // Console.WriteLine("   1- ACESSAR LOG  ");
        // Console.WriteLine("   2- VER USUÁRIOS ");
        // Console.WriteLine("   0- SAIR         ");
        // Console.WriteLine("OPÇÃO:");

        //switch (Console.ReadLine())
        // {
        //     case "1": MostrarLog(repo); break;
        //     case "2": MostrarUsuario(repo); break;
        //     case "0": return;
        //     default: Console.WriteLine("Opção Inválida"); Pausar(); break;
        // }
    
    //}
}


//ADMINISTRADOR
    static void CadastrarUsuario(UsuarioRepositorio repo)
    {
        System.Console.WriteLine("\n--- Novo Usuario ---");
        var u = new Usuarios
        {
            Nome= Ler("Nome: "),
            Email= Ler("Email: "),
            Senha= Ler("Senha: ")
        };
        try
        {
            repo.Cadastrar(u);
            Console.WriteLine("Cadastrado com sucesso");   
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            Console.WriteLine("Usuario existente");
        }
        Pausar();
    }

    static void MostrarUsuario(UsuarioRepositorio repo)
    {
        Console.WriteLine("Usuarios Cadastrados !");
        var lista= repo.Listar();
        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum usuario cadastrado!");
            
        }
        else
        {
            System.Console.WriteLine($"{"ID", -4} {"NOME", -25} Email");

            lista.ForEach(Console.WriteLine);
        }
        Pausar();
    }

    static void EditarUsuario(UsuarioRepositorio repo)
    {
        ListarNaTela(repo);

        if (!int.TryParse(Ler("\nID do usuario a editar: "), out int id))
        {
            Console.WriteLine("ID Invalido");
            Pausar();
            return;
        }
        var u = new Usuarios {Id= id, Nome= Ler("Novo nome: "), Email = Ler("Novo email: ")};
        try
        {
            System.Console.WriteLine(repo.Editar(u) ? "Usuario Atualizado" : "Id não encontrado");
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            System.Console.WriteLine("Email ja cadastrado por outro usuario.");
        }

    }

    static void ListarNaTela(UsuarioRepositorio repo)
    {
        Console.WriteLine("--- Usuarios Cadastrados ---");
        var lista = repo.Listar();
        System.Console.WriteLine($"{"ID", -4} {"NOME", -25} Email");
        lista.ForEach(Console.WriteLine);
    }

    static void ExcluirUsuario(UsuarioRepositorio repo)
    {
        ListarNaTela(repo);
        if (!int.TryParse(Ler("\nID do usuario a excluir: "), out int id))
        {
            Console.WriteLine("ID Invalido");
            Pausar();
            return;
        }
        if (Ler("Tem Certeza?(s/n: )").ToLower() !="s")
            Console.WriteLine("Operação Cancelada");
        else
            System.Console.WriteLine(repo.Exluir(id) ? "Usuario Excluido" : "ID não encontrado");
        Pausar();
    }

    static void Pausar()
    {
        System.Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static string Ler(string texto)
{
    Console.Write(texto);
    return Console.ReadLine()?.Trim()?? "";
}


//PESQUISADOR

    // static void CriarAcao

    // static void MostrarAcao


//VALIDADOR

    // static void MostrarAcoes (Irá listar na tela as acoes em analise para o validador )

    // static void AvaliarAcoes (Irá escolher a acao e avaliar (s/n), será chamado a partir do método acima)

    // static void MostrarAvaliacoes

//COMITE

    //static void MostrarLog

