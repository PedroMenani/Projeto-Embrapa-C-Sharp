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
 
static void CriarAcao(int matriculaPesquisador)
{
    Console.Clear();
    Console.WriteLine("=== CRIAR AÇÃO ===\n");
    string titulo = Ler("Nome da ação: ");
    string categoria = Ler("Categoria da ação: ");
    string descricao = Ler("Descrição: ");
    string data = Ler("Data de realização (DD-MM-AAAA): ");
    string comprovante = Ler("Tipo de comprovante: ");
    string local = Ler("Local: ");

    if (!DateTime.TryParse(data, out DateTime dataRealizacao))
    {
        Console.WriteLine("Data inválida!");
        Pausar();
        return;
    }

    if (string.IsNullOrWhiteSpace(titulo) ||
        string.IsNullOrWhiteSpace(categoria) ||
        string.IsNullOrWhiteSpace(descricao) ||
        string.IsNullOrWhiteSpace(comprovante) ||
        string.IsNullOrWhiteSpace(local))
    {
        Console.WriteLine("Preencha todos os campos!");
        Pausar();
        return;
    }

    using var con = new Conexao().Criar();
    con.Open();

    string sql = @" INSERT INTO postagem
        (titulo, categoria, descricao, data_realizacao, tipo_comprovante, local, data_hora, status, matricula_pesquisador) VALUES (@titulo, @categoria, @descricao, @data_realizacao, @comprovante, @local, @data_hora, @status, @matricula); 
        SELECT LAST_INSERT_ID();";

    using var cmd = new MySqlCommand(sql, con);

    cmd.Parameters.AddWithValue("@titulo", titulo);
    cmd.Parameters.AddWithValue("@categoria", categoria);
    cmd.Parameters.AddWithValue("@descricao", descricao);
    cmd.Parameters.AddWithValue("@data_realizacao", dataRealizacao);
    cmd.Parameters.AddWithValue("@comprovante", comprovante);
    cmd.Parameters.AddWithValue("@local", local);
    cmd.Parameters.AddWithValue("@data_hora", DateTime.Now);
    cmd.Parameters.AddWithValue("@status", "Em análise");
    cmd.Parameters.AddWithValue("@matricula", matriculaPesquisador);

    int id = Convert.ToInt32(cmd.ExecuteScalar());

    Console.WriteLine("\nAção cadastrada com sucesso!");
    Console.WriteLine($"ID da ação: {id}");
    Console.WriteLine("Status: Em análise");

    Pausar();
}


static void MostrarAcao(int matriculaPesquisador)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== MINHAS AÇÕES ===\n");

        if (!int.TryParse(Ler("Digite o ID da ação (0 para sair): "), out int id))
        {
            Console.WriteLine("ID inválido!");
            Pausar();
            continue;
        }

        if (id == 0)
            return;

        using var con = new Conexao().Criar();
        con.Open();

        string sql = @"SELECT id, titulo, categoria, descricao, data_realizacao, tipo_comprovante, local, status FROM postagem WHERE id = @id AND matricula_pesquisador = @matricula;";

        using var cmd = new MySqlCommand(sql, con);

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@matricula", matriculaPesquisador);

        using var leitor = cmd.ExecuteReader();

        if (leitor.Read())
        {
            Console.WriteLine("\n=== INFORMAÇÕES DA AÇÃO ===\n");
            Console.WriteLine($"ID: {leitor["id"]}");
            Console.WriteLine($"Nome: {leitor["titulo"]}");
            Console.WriteLine($"Categoria: {leitor["categoria"]}");
            Console.WriteLine($"Descrição: {leitor["descricao"]}");
            Console.WriteLine($"Data de realização: {Convert.ToDateTime(leitor["data_realizacao"]):dd/MM/yyyy}");
            Console.WriteLine($"Comprovante: {leitor["tipo_comprovante"]}");
            Console.WriteLine($"Local: {leitor["local"]}");
            Console.WriteLine($"Status: {leitor["status"]}");
        }
        else
        {
            Console.WriteLine("\nAção não encontrada!");
        }

        leitor.Close();
        Console.WriteLine("\nPressione ENTER para continuar.");
        Console.WriteLine("Digite 0 para sair.");

        if (Console.ReadLine()?.Trim() == "0")
            return;
    }
}

//VALIDADOR

    // static void MostrarAcoes (Irá listar na tela as acoes em analise para o validador )

    // static void AvaliarAcoes (Irá escolher a acao e avaliar (s/n), será chamado a partir do método acima)

    // static void MostrarAvaliacoes

//COMITE

    //static void MostrarLog

