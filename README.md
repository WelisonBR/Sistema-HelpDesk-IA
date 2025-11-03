# Sistema Inteligente de Suporte Técnico de Gestão de Chamados Integrado com IA

## 📕 Sobre o projeto

- Sistema desenvolvido para o Projeto Integrado Multidisciplinar (PIM) da faculdade, com o objetivo de gerenciar chamados dos Alunos, relacionado a dúvidas a sua jornada acadêmica e o uso da plataforma da instituição.
- O grande diferencial do projeto é a **integração com a IA do Google Gemini**, que oferece uma primeira linha de suporte inteligente, respondendo a dúvidas comuns e permitindo que a equipe de administradores (funcionários) foque em chamados mais complexos.

## 🛠️ Tecnologias Utilizadas

- C#
- .NET Framework
- Windows Forms (Visual Studio)
- SQL Server
- API do Google Gemini (para o suporte com IA)

## 🧾 Funcionalidades

- ProjetoDeskHelp (Painel do Aluno e Funcionário):

  - Criação de novos chamados
  - Acompanhamento de status dos chamados
  - Consulta das dúvidas poderam ser feitas na IA
  - Conversa bidirecional entre Aluno e Funcionário

- HelpDeskAdmin (Painel do Administrador)
  - Cadastro de novos funcionários ao sistema
  - Acesso exclusivo para administradores

## 🚀 Como Executar (Máquina Local)

Para executar este projeto em sua máquina, siga os passos abaixo:

1.  **Clone o repositório:**

    ```bash
    git clone https://github.com/WelisonBR/Sistema-HelpDesk-IA.git
    ```

2.  **Abra a Solução:**

    - Abra o arquivo `.sln` no Visual Studio.

3.  **Configure os Segredos (Connection Strings e API Key):**

    - Utilizo nesse projeto arquivos `.config` locais que não são enviados ao GitHub por segurança.
    - **No projeto `ProjetoHelpDesk`:**
      - Crie um arquivo chamado `connectionStrings.config`.
      - Crie um arquivo chamado `appSettings.config`.
      - Configure-os com sua string de conexão do SQL Server e sua chave da API do Gemini, respectivamente.
    - **No projeto `HelpDeskAdmin`:**
      - Crie um arquivo chamado `connectionStrings.config`.
      - (Se ele usar a API também) Crie um arquivo `appSettings.config`.

4.  **Propriedades dos Arquivos `.config`:**

    - Para cada arquivo de segredo que você criou (ex: `connectionStrings.config`), clique nele no Visual Studio, vá em **Propriedades** e mude em Copia para Diretório de Saídas de **"Não Copiar"** para **"Copiar se for mais novo"**.

5.  **Execute o Banco de Dados:**

    - Execute o script `.sql` (disponível na pasta `/BancoDeDados`), no seu SQL Server para criar as tabelas."
