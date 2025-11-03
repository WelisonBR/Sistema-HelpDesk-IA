using System;
using System.Configuration;
using System.Data.SqlClient;
using BCrypt.Net;

namespace HelpDeskAdmin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Cadastro de Funcionários Help Desk ---");
            Console.WriteLine("A Matrícula Funcional (FUNCxxx) será gerada automaticamente.");
            Console.WriteLine("-----------------------------------------------------");

            string connectionString = "";
            try
            {
                // Lê a connection string do App.config deste projeto
                connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    MostrarErro("ERRO: ConnectionString 'MinhaConexao' não encontrada ou vazia no App.config.");
                    return;
                }
            }
            catch (Exception ex) 
            {
                MostrarErro($"ERRO ao ler configurações: {ex.Message}");
                return;
            }

            // Obter Dados do Novo Funcionário
            Console.WriteLine("\nInsira os dados do novo funcionário:");

            Console.Write("Nome Completo: ");
            string nomeFuncionario = Console.ReadLine()?.Trim();

            Console.Write("E-mail (será usado no login): ");
            string emailFuncionario = Console.ReadLine()?.Trim();

            string senhaFuncionario = "";
            string confirmaSenha;

            do // Loop até que as senhas coincidam
            {
                Console.Write("Senha Temporária (forte recomendada): ");
                senhaFuncionario = LerSenhaMascarada();
                Console.WriteLine();

                Console.Write("Confirme a Senha Temporária: ");
                confirmaSenha = LerSenhaMascarada();
                Console.WriteLine();

                if (senhaFuncionario != confirmaSenha)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("As senhas não coincidem. Por favor, digite novamente.");
                    Console.ResetColor();
                }
            }
            while (senhaFuncionario != confirmaSenha);

            //  Validações básicas
            if (string.IsNullOrWhiteSpace(nomeFuncionario) || string.IsNullOrWhiteSpace(emailFuncionario) || string.IsNullOrWhiteSpace(senhaFuncionario))
            {
                MostrarErro("ERRO: Nome, Email e Senha são obrigatórios.");
                return;
            }

            // Gera Hash da Senha
            string senhaHash = "";
            try
            {
                senhaHash = BCrypt.Net.BCrypt.HashPassword(senhaFuncionario);
            }
            catch (Exception hashEx)
            {
                MostrarErro($"ERRO CRÍTICO ao gerar hash da senha: {hashEx.Message}");
                return;
            }

            // Inserir no Banco de Dados (em DUAS ETAPAS)
            int novoUsuarioID = 0;
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                }
                catch (Exception ex)
                {
                    MostrarErro($"ERRO ao conectar ao banco: {ex.Message}");
                    return;
                }

                // Inicia a transação para garantir que as inserções funcionem
                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    // Inserção na tabela Usuarios
                    string queryUsuarios = @"INSERT INTO dbo.Usuarios (Email, SenhaHash, Papel)
                                             OUTPUT INSERTED.ID
                                             VALUES (@Email, @SenhaHash, 'Funcionario');";

                    using (SqlCommand comandoUsuarios = new SqlCommand(queryUsuarios, conexao, transacao))
                    {
                        comandoUsuarios.Parameters.AddWithValue("@Email", emailFuncionario);
                        comandoUsuarios.Parameters.AddWithValue("@SenhaHash", senhaHash);

                        object resultado = comandoUsuarios.ExecuteScalar();
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            novoUsuarioID = Convert.ToInt32(resultado);
                        }
                        else
                        {
                            throw new Exception("Não foi possível obter o ID do novo usuário.");
                        }
                    }

                    // Inserção na tabela Funcionários
                    string queryFuncionarios = @"INSERT INTO dbo.Funcionarios (UsuarioID, Nome, MatriculaFuncionario)
                                                 VALUES (@UsuarioID, @Nome, @Matricula);"; // Passa NULL para Matricula

                    using (SqlCommand comandoFuncionarios = new SqlCommand(queryFuncionarios, conexao, transacao))
                    {
                        comandoFuncionarios.Parameters.AddWithValue("@UsuarioID", novoUsuarioID);
                        comandoFuncionarios.Parameters.AddWithValue("@Nome", nomeFuncionario);
                        comandoFuncionarios.Parameters.AddWithValue("@Matricula", DBNull.Value);
                        comandoFuncionarios.ExecuteNonQuery();
                    }

                    // Se ambas funcionaram, confirma (Commit)
                    transacao.Commit();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nSUCESSO: Funcionário '{nomeFuncionario}' ({emailFuncionario}) criado!");
                    Console.WriteLine("A Matrícula Funcional foi gerada automaticamente.");
                    Console.ResetColor();
                    Console.WriteLine("\nLembrete: Comunique a senha temporária ao funcionário de forma segura.");
                }
                catch (SqlException sqlEx)
                {
                    transacao.Rollback();
                    Console.ForegroundColor = ConsoleColor.Red;
                    if ((sqlEx.Number == 2627 || sqlEx.Number == 2601)) 
                    {
                        if (sqlEx.Message.ToLower().Contains("email")) { Console.WriteLine($"ERRO: O email '{emailFuncionario}' já existe."); }
                        else if (sqlEx.Message.ToLower().Contains("matriculafuncionario")) { Console.WriteLine($"ERRO: A Matrícula Funcional gerada já existe."); }
                        else { Console.WriteLine($"ERRO de duplicidade: {sqlEx.Message}"); }
                    }
                    else
                    {
                        Console.WriteLine($"ERRO DE SQL: {sqlEx.Number} - {sqlEx.Message}");
                    }
                    Console.ResetColor();
                }
                catch (Exception ex) 
                {
                    transacao.Rollback();
                    MostrarErro($"ERRO inesperado na transação: {ex.Message}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }

        // Mostra mensagem de erro formatada em cor vermelha
        static void MostrarErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{mensagem}");
            Console.ResetColor();
            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
            Environment.Exit(1);
        }

        // Função que lê senha mascarada
        public static string LerSenhaMascarada()
        {
            string senha = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true); // Lê sem mostrar
                if (!char.IsControl(key.KeyChar))
                {
                    senha += key.KeyChar;
                    Console.Write("*"); // Mostra asterisco
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                    {
                        senha = senha.Substring(0, (senha.Length - 1));
                        Console.Write("\b \b"); // Apaga asterisco
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter); // Continua até Enter
            return senha;
        }
    }
}