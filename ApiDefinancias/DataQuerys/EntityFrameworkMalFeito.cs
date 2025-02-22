// Usando os namespaces necessarios para o codigo
using System.Data;
using System.Text;
using ApiFinacias.Model;
using ApiFinacias.Security;
using MySql.Data.MySqlClient;

// coloco a classe referente ao crud para a tabela de usuarios dentro de um namespace
namespace ApiFinacias.DataQuerys;

public class EntityFrameworkMalFeitoUsers // Refiz o entityFramework kkkkkkkkkkkkkk
{
    // string de connexão
    public readonly string ConnectionString = "Server=localhost;Database=mubank_db;Uid=root;Pwd=Z7$J8v2mL@pQxW4tY#RkN9f6S!bG5A;";
    
    //metodo Create do CRUD
    public async Task<IResult> Create(string nome, string Email, string senha, int CPF)
    {
        // Instancio a classe contendo os metodos de criptografia
        Encrypt crypt = new Encrypt();

        // e criptografo as senhas inseridas e retorno dentro da string
        string senhacriptografada = await crypt.EncryptTextAsync(senha);
        
        // abro um bloco using do objeto do MySqlConnection
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            // e abro a conexão com o banco de dados de forma assincrona
            await conn.OpenAsync();

            // Crio uma instancia do MySqlCommand usando o objeto a conexão com o banco de dados
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                // Crio a Query Para inserir os dados dentro do banco de dados
                cmd.CommandText = "INSERT INTO users_tb(Nome, Email, Senha, CPF) VALUES(@Nome, @Email, @Senha, @CPF);";

                // abro um bloco try-catch
                try
                {
                    // adiciono os parametros referente aos parametros de entrada do metodo
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Senha", senhacriptografada);
                    cmd.Parameters.AddWithValue("@CPF", CPF);

                    // e depois executo a query tmb de forma assincrona
                    await cmd.ExecuteNonQueryAsync();

                    // e retorno um codigo de status 200(ok) com uma mensagem a mais
                    return Results.Ok("deu certo paizão");
                }
                catch (MySqlException sqlex) // Caso haja um errro dentro do banco de dados
                {
                    // retorno um codigo 500 de erro interno no servidor
                    return Results.InternalServerError($"Deu erro aq no server adm, n se preocupa n paizao, aq O erro: {sqlex.Message}");
                }
                catch (Exception e) // caso haja qualquer erro
                {
                    // retorno um codigo de status 400 de requisição falha
                    return Results.BadRequest(e.Message);
                }
            }
        }
            
    }

    // Metodo update 
    public async Task<IResult> Update(string nome, string email, string senha, int CPF)
    {
        // Instancio a classe contendo os metodos de criptografia
        Encrypt crypt = new Encrypt();

        // e criptografo as senhas inseridas e retorno dentro da string
        string senhacriptografada = await crypt.EncryptTextAsync(senha);
        
        // abro um bloco using do objeto do MySqlConnection
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            // e abro a conexão com o banco de dados de forma assincrona
            await conn.OpenAsync();

            // Crio uma instancia do MySqlCommand usando o objeto a conexão com o banco de dados
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                // Crio a Query Para atualizar os dados dentro do banco de dados
                cmd.CommandText = "UPDATE users_tb SET Nome=@Nome, Senha=@Senha, Email=@Email WHERE CPF=@CPF;";

                // abro um bloco try catch
                try
                {
                    // adiciono os parametros referente aos parametros de entrada do metodo
                    cmd.Parameters.AddWithValue("@Nome", nome);
                    cmd.Parameters.AddWithValue("@Senha", senhacriptografada);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@CPF", CPF);

                    // e executo a query de forma assincrona
                    await cmd.ExecuteNonQueryAsync();

                    // e retorno um codigo de status 200(ok)
                    return Results.Ok("Banco de dados atualizado com sucesso");
                }
                catch (MySqlException sqlex) // caso haja algum erro relacionado ao servidor
                {
                    // retorno um codigo 500 de erro interno no servidor
                    return Results.InternalServerError("erro aq dentro, perai");
                }
                catch (Exception e) // agr, se der qualquer erro relacionado a execução do codigo
                {
                    // retorno um codigo de status 400
                    return Results.BadRequest(e.Message);
                }
            }
        }
    }
    
    // metodo delete
    public async Task<IResult> Delete(int CPF)
    {
        // abro uma conexão com o bancos de dados
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            await conn.OpenAsync(); // de forma assincrona obviamente

            // crio um mysqlcommand usando o objeto conn
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                // abro um bloco try-catch
                try
                {
                    // crio uma query que basicamente, deleta algo em users_tb referente ao CPF, caso ele seja igual ao CPF insererido na where, ai ele apaga
                    cmd.CommandText = "DELETE FROM users_tb WHERE CPF=@CPF;";
                    
                    // Adiciono o parametro referente ao CPF
                    cmd.Parameters.AddWithValue("@CPF", CPF);
                    
                    // e executo ela de forma assincrona
                    await cmd.ExecuteNonQueryAsync();
                    
                    // e retorno um codigo 200 de status(ok)
                    return Results.Ok("Deletado com sucesso");
                }
                catch (Exception e) // caso de qualquer erro
                {
                    // exibo este erro e retorno um codigo 400
                    return Results.BadRequest(e.Message);
                }
            }
        }
    }

    // Metodo reader
    public async Task<IResult> Reader(string Key)
    {
        // verifico se a chave inserida esta incorreta
        if (Key != "ChaveMuitoSegura")
        {// caso esteja, retorno uma requisição falha dizendo ao cliente que ele não inseriu a chave correta
            return Results.BadRequest("Não inseriu a chave correta :D");
        }

        // crio uma lista de models, que representara os usuarios presentes no banco de dados
        List<UsersModel> users = new List<UsersModel>();

        // crio uma conexão com o banco de dados
        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
        {
            // e abro de forma assincrona
            await conn.OpenAsync();

            // depois disso, crio um objeto do MySqlCommand usando o objeto da conexão com o banco de dados
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                // crio uma query bem simples, de basicamente, pegar tudo que dentro do banco de dados
                cmd.CommandText = "SELECT * FROM users_tb";

                // depois crio uma reader do MySql para ler os dados contidos dentro do servidor
                using (MySqlDataReader reader = cmd.ExecuteReader())  // No caso, eu executo a query como se fosse uma reader
                {
                    // crio um objetoda classe referente aos metodos de criptografia
                    Descrypto descrypto = new Descrypto();

                    // variavel i, e só
                    int i = 0;
                    
                    // abro um bloco while que, Enquanto houver linhas de dados dentro da tabela para serem lidas
                    while (await reader.ReadAsync())  // Correção: ReadAsync, Para ter mais eficiencia
                    {
                        // pego a senha contida no banco de dados
                        string senhasalva = reader["Senha"] as string; 

                        // verifico se a senha e diferente de nulo
                        if (senhasalva != null)
                        {   // se for
                            
                            // Criar um novo objeto para cada iteração
                            UsersModel model = new UsersModel();  
                            
                            // adiciono os dados contido no servidor nas propriedades da model
                            model.cpf = reader.GetInt32("CPF");
                            model.Email = reader.GetString("Email");
                            model.Name = reader.GetString("Nome");
                            model.Password = await descrypto.Decrypt(senhasalva);  // Decripta os dados Criptografados
                            model.Dinheiro = reader.GetFloat("Dinheiro");
                            
                            users.Add(model);  // Adiciona o modelo à lista
                        }
                    }
                }
            }
        }

        // verifico tmb quandos usuarios tem, caso isto seja igual a 0
        if (users.Count == 0)
        {
            // retorno um famoso 404
            return Results.NotFound("Nenhum usuário encontrado.");
        }

        // mas caso contrario, retorno um json com os dados dos usuarios
        return Results.Ok(users);  
    }

}