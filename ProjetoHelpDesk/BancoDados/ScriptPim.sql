-- // SCRIPT PARA CRIAR ESTRUTURA COMPLETA DO BANCO (Execução Única)
-- // Estrutura: Tabelas Usuarios, Alunos, Funcionarios separadas
-- // Inclui: Criação de Tabelas, Inserção de Dados Iniciais e Triggers

-- Cria a tabela 'Cursos' e insere dados dos cursos desejados
CREATE TABLE dbo.Cursos (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NomeCurso NVARCHAR(150) UNIQUE NOT NULL
);
INSERT INTO dbo.Cursos (NomeCurso) VALUES 
    ('Análise e Desenvolvimento de Sistemas'), 
    ('Ciência da Computação'), 
    ('Engenharia de Software'), 
    ('Marketing'), 
    ('Nutrição'), 
    ('Medicina'), 
    ('Gestão de TI'), 
    ('Direito');
PRINT 'Tabela Cursos criada e populada.';
GO

-- Cria a tabela 'Categorias' e insira nomes das categorias desejadas
CREATE TABLE dbo.Categorias (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NomeCategoria NVARCHAR(100) NOT NULL UNIQUE
);
INSERT INTO dbo.Categorias (NomeCategoria) VALUES 
    ('Acesso'), 
    ('Financeiro'), 
    ('Email'), 
    ('Rede'), 
    ('Hardware'), 
    ('Software'), 
    ('Outros');
PRINT 'Tabela Categorias criada e populada.';
GO

-- Cria a Tabela Central 'Usuarios' (Apenas Login e Papel)
CREATE TABLE dbo.Usuarios (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) UNIQUE NOT NULL,    
    SenhaHash NVARCHAR(255) NOT NULL,       
    Papel NVARCHAR(50) NOT NULL 
        CONSTRAINT CK_Usuarios_Papel CHECK (Papel IN ('Aluno', 'Funcionario', 'Admin')), -- Constraint definida aqui
    DataCadastro DATETIME NOT NULL DEFAULT GETDATE()
);
PRINT 'Tabela Usuarios criada.';
GO

-- Cria a Tabela 'Alunos' 
CREATE TABLE dbo.Alunos (
    UsuarioID INT PRIMARY KEY,              
    Nome NVARCHAR(255) NOT NULL,
    RegistroAluno NVARCHAR(7) NOT NULL UNIQUE, 
    CursoID INT NOT NULL,                   

    CONSTRAINT FK_Alunos_Usuarios FOREIGN KEY (UsuarioID) REFERENCES dbo.Usuarios(ID) ON DELETE CASCADE,
    CONSTRAINT FK_Alunos_Cursos FOREIGN KEY (CursoID) REFERENCES dbo.Cursos(ID) ON DELETE NO ACTION
);
PRINT 'Tabela Alunos criada.';
GO

-- Cria a Tabela 'Funcionarios'
CREATE TABLE dbo.Funcionarios (
    UsuarioID INT PRIMARY KEY,             
    Nome NVARCHAR(255) NOT NULL,
    MatriculaFuncionario NVARCHAR(50) NOT NULL UNIQUE, 

    CONSTRAINT FK_Funcionarios_Usuarios FOREIGN KEY (UsuarioID) REFERENCES dbo.Usuarios(ID) ON DELETE CASCADE
);
PRINT 'Tabela Funcionarios criada.';
GO

-- Cria a tabela 'Chamados'
CREATE TABLE dbo.Chamados (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT NOT NULL, -- FK para Usuarios.ID
    CategoriaID INT NULL,   -- FK para Categorias.ID
    Titulo NVARCHAR(255) NOT NULL,
    Descricao NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Aberto' 
        CONSTRAINT CK_Chamados_Status CHECK (Status IN ('Aberto', 'Em andamento', 'Aguardando resposta', 'Resolvido', 'Fechado', 'Concluído')),
    Prioridade NVARCHAR(50) NOT NULL DEFAULT 'Baixa' 
        CONSTRAINT CK_Chamados_Prioridade CHECK (Prioridade IN ('Baixa', 'Média', 'Alta', 'Urgente')),
    DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
    DataAtualizacao DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Chamados_Usuarios FOREIGN KEY (UsuarioID) REFERENCES dbo.Usuarios(ID) ON DELETE CASCADE,
    CONSTRAINT FK_Chamados_Categorias FOREIGN KEY (CategoriaID) REFERENCES dbo.Categorias(ID) ON DELETE NO ACTION
);
PRINT 'Tabela Chamados criada.';
GO

-- Cria a tabela 'Respostas'
CREATE TABLE dbo.Respostas (
    ID INT PRIMARY KEY IDENTITY(1,1),
    ChamadoID INT NOT NULL, -- FK para Chamados.ID
    UsuarioID INT NOT NULL, -- FK para Usuarios.ID
    Mensagem NVARCHAR(MAX) NOT NULL,
    DataEnvio DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Respostas_Chamados FOREIGN KEY (ChamadoID) REFERENCES dbo.Chamados(ID) ON DELETE CASCADE,
    CONSTRAINT FK_Respostas_Usuarios FOREIGN KEY (UsuarioID) REFERENCES dbo.Usuarios(ID) ON DELETE NO ACTION 
);
PRINT 'Tabela Respostas criada.';
GO


-- Cria a trigger para gerar Matrícula do Funcionário (FUNCxxx)
EXEC('
CREATE TRIGGER trg_GerarMatriculaFuncionario
ON dbo.Funcionarios
INSTEAD OF INSERT 
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NovoUsuarioID INT;
    DECLARE @NomeFuncionario NVARCHAR(255);
    DECLARE @MatriculaFornecida NVARCHAR(50);

    SELECT 
        @NovoUsuarioID = UsuarioID, 
        @NomeFuncionario = Nome,
        @MatriculaFornecida = MatriculaFuncionario
        -- , @Departamento = Departamento -- Exemplo
    FROM inserted; 

    DECLARE @MatriculaFinal NVARCHAR(50);

    IF @MatriculaFornecida IS NULL OR (@MatriculaFornecida NOT LIKE ''FUNC[0-9][0-9][0-9]%'' AND @MatriculaFornecida NOT LIKE ''ADMIN%'')
    BEGIN
        DECLARE @ProximoNumero INT;
        
        SELECT @ProximoNumero = ISNULL(MAX(CAST(SUBSTRING(MatriculaFuncionario, 5, 3) AS INT)), 0) + 1
        FROM dbo.Funcionarios
        WHERE MatriculaFuncionario LIKE ''FUNC[0-9][0-9][0-9]''; 

        SET @MatriculaFinal = ''FUNC'' + FORMAT(@ProximoNumero, ''000'');
    END
    ELSE 
    BEGIN
        SET @MatriculaFinal = @MatriculaFornecida; 
    END

    INSERT INTO dbo.Funcionarios (UsuarioID, Nome, MatriculaFuncionario /*, Departamento*/)
    VALUES (@NovoUsuarioID, @NomeFuncionario, @MatriculaFinal /*, @Departamento*/);
END;
');
PRINT 'Trigger INSTEAD OF trg_GerarMatriculaFuncionario criado.';
GO

-- Cria a trigger para atualizar data do Chamado ao ser atualizado
EXEC('
CREATE TRIGGER trg_AtualizaDataChamado 
ON dbo.Chamados 
AFTER UPDATE 
AS
BEGIN
    -- Só atualiza se colunas específicas mudarem E se a tabela inserted não estiver vazia (update real ocorreu)
    IF (UPDATE(Status) OR UPDATE(Prioridade) OR UPDATE(Titulo) OR UPDATE(Descricao)) AND EXISTS (SELECT 1 FROM inserted)
    BEGIN
        UPDATE C 
        SET DataAtualizacao = GETDATE() 
        FROM dbo.Chamados C 
        INNER JOIN inserted I ON C.ID = I.ID
        -- Garante que não atualize se a data já for a atual (evita loop se trigger chamar outro update)
        WHERE C.DataAtualizacao <> GETDATE(); 
    END
END;
');
PRINT 'Trigger trg_AtualizaDataChamado criado.';
GO

-- Cria a trigger para atualizar data do Chamado ao receber nova resposta
EXEC('
CREATE TRIGGER trg_AtualizaDataChamadoEmResposta 
ON dbo.Respostas 
AFTER INSERT 
AS
BEGIN
    SET NOCOUNT ON; -- Boa prática em triggers
    UPDATE C 
    SET DataAtualizacao = GETDATE() 
    FROM dbo.Chamados C 
    INNER JOIN inserted I ON C.ID = I.ChamadoID;
END;
');
PRINT 'Trigger trg_AtualizaDataChamadoEmResposta criado.';
GO

PRINT 'Script de Criação Única do Banco Concluído';