
using ProjetoHelpDesk;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using BCrypt.Net;

namespace ProjetoHelpDesk
{

    public partial class Login : Form
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"]?.ConnectionString;

        // Informações do usuário logado
        public static int UsuarioLogadoId { get; set; }
        public static string UsuarioLogadoNome { get; set; }
        public static string UsuarioLogadoPapel { get; set; }
        public static string UsuarioLogadoEmail { get; set; }
        public static string UsuarioLogadoRA { get; set; }
        public static string UsuarioLogadoCurso { get; set; }


        public Login()
        {
            InitializeComponent();
        }

        // Evento do botão de login
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtEmailLogin.Text.Trim();
            string senhaDigitada = txtSenhaLogin.Text;

            // Validação de email e senha
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senhaDigitada))
            {
                MessageBox.Show("Por favor, preencha o email e a senha.", "Campos Vazios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Consulta para buscar o usuário pelo email
            string query = @"
                SELECT 
                    U.ID AS UsuarioID, U.Email, U.SenhaHash, U.Papel,
                    A.Nome AS NomeAluno, A.RegistroAluno, C.NomeCurso,
                    F.Nome AS NomeFuncionario, F.MatriculaFuncionario
                FROM 
                    Usuarios U
                LEFT JOIN 
                    Alunos A ON U.ID = A.UsuarioID
                LEFT JOIN 
                    Funcionarios F ON U.ID = F.UsuarioID
                LEFT JOIN 
                    Cursos C ON A.CursoID = C.ID
                WHERE 
                    U.Email = @Email";

            using (SqlConnection conexao = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand(query, conexao))
            {
                comando.Parameters.AddWithValue("@Email", email);

                try
                {
                    conexao.Open();
                    SqlDataReader leitor = comando.ExecuteReader();

                    // Verrifica se o usuário foi encontrado
                    if (leitor.Read())
                    {
                        string senhaHashDoBanco = leitor["SenhaHash"].ToString();
                        bool senhaCorreta = false;

                        // Verificação da Senha com hash
                        try
                        { 
                            senhaCorreta = BCrypt.Net.BCrypt.Verify(senhaDigitada, senhaHashDoBanco);
                        }
                        catch (SaltParseException)
                        {
                            MessageBox.Show("Formato de hash inválido no banco de dados para este usuário.", "Erro de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao verificar senha: {ex.Message}", "Erro de Segurança", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (senhaCorreta)
                        {
                            // Pega dados comuns da tabela Usuarios
                            int usuarioId = Convert.ToInt32(leitor["UsuarioID"]);
                            string papel = leitor["Papel"].ToString();
                            string emailUsuario = leitor["Email"].ToString();

                            // Variáveis para guardar os dados específicos
                            string nomeUsuario = "";
                            string registro = "";
                            string nomeCurso = null;

                            // Verifica se o curso é nulo
                            string cursoUsuario = !leitor.IsDBNull(leitor.GetOrdinal("NomeCurso"))
                                                  ? leitor["NomeCurso"].ToString()
                                                  : "Não informado"; // Valor padrão se for nulo

                            if (papel.Equals("Aluno", StringComparison.OrdinalIgnoreCase))
                            {
                                // Lê dados do Aluno (resultado do JOIN com Alunos)
                                nomeUsuario = !leitor.IsDBNull(leitor.GetOrdinal("NomeAluno")) ? leitor["NomeAluno"].ToString() : "";
                                registro = !leitor.IsDBNull(leitor.GetOrdinal("RegistroAluno")) ? leitor["RegistroAluno"].ToString() : "";
                                nomeCurso = !leitor.IsDBNull(leitor.GetOrdinal("NomeCurso")) ? leitor["NomeCurso"].ToString() : "N/A";
                            }
                            else if (papel.Equals("Funcionario", StringComparison.OrdinalIgnoreCase) || papel.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                            {
                                // Lê dados do Funcionário/Admin (resultado do JOIN com Funcionarios)
                                nomeUsuario = !leitor.IsDBNull(leitor.GetOrdinal("NomeFuncionario")) ? leitor["NomeFuncionario"].ToString() : "";
                                registro = !leitor.IsDBNull(leitor.GetOrdinal("MatriculaFuncionario")) ? leitor["MatriculaFuncionario"].ToString() : "";
                                nomeCurso = null;
                            }

                            MessageBox.Show($"Bem-vindo(a), {nomeUsuario}!", "Login bem-sucedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Armazena as informações do usuário logado
                            UsuarioLogadoId = usuarioId;
                            UsuarioLogadoNome = nomeUsuario;
                            UsuarioLogadoPapel = papel;
                            UsuarioLogadoEmail = emailUsuario;
                            UsuarioLogadoRA = registro;
                            UsuarioLogadoCurso = cursoUsuario;

                            leitor.Close();

                            // Direcionamento com base no Papel
                            if (papel.Equals("Aluno", StringComparison.OrdinalIgnoreCase))
                            {
                                PainelAluno painelAluno = new PainelAluno();
                                painelAluno.Show();
                            }
                            else if (papel.Equals("Funcionario", StringComparison.OrdinalIgnoreCase))
                            {
                                PainelFuncionario painelFuncionario = new PainelFuncionario();
                                painelFuncionario.Show();
                            }
                            else
                            {
                                MessageBox.Show("Seu usuário possui um papel desconhecido no sistema. Contate o administrador.", "Erro de Acesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                UsuarioLogadoId = 0;
                                UsuarioLogadoNome = null;
                                UsuarioLogadoPapel = null;
                                return;
                            }

                            this.Hide(); // Esconde a tela de login após abrir o painel correto
                        }
                        else
                        {
                            MessageBox.Show("Senha incorreta. Verifique os dados digitados.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSenhaLogin.Focus();
                            txtSenhaLogin.SelectAll();
                        }
                    }
                    else // Usuário com esse email não encontrado
                    {
                        MessageBox.Show("Email não cadastrado no sistema. Verifique o email digitado ou cadastre-se.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtEmailLogin.Focus();
                        txtEmailLogin.SelectAll();
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"Erro de banco de dados ao tentar fazer login: {sqlEx.Message}\n\nVerifique a string de conexão e o servidor.", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro inesperado durante o login: {ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblCadastreSe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Cadastro telaCadastro = new Cadastro())
            {
                telaCadastro.ShowDialog(this); 
            }
        }

        private void lblEsqueceuSenha_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Funcionalidade para Mudar Senha ainda não implementada. Entre em contato com o administrador",
                "Em Construção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
