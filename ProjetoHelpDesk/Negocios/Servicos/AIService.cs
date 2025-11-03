using GenerativeAI;
using GenerativeAI.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoHelpDesk
{
    // Gere a inicialização e a comunicação com a API Google Generative AI (Gemini).
    public class AIService
    {
        // Guarada o cliente de IA para fazer as chamadas
        private GenerativeModel _googleAIModel;
        // guarda o nome do modelo (gemini-pro)
        private string _modelNameFromConfig;

        // Indica se o serviço foi inicializado com sucesso
        public bool IsInitialized { get; private set; } = false;
        // Guarda a mensagem de erro se a inicialização falhar
        public string InitializationError { get; private set; } = null;

        public AIService()
        {
            InitializeClient();
        }

        // Lê as chaves do App.config e configura o cliente da API.
        private void InitializeClient()
        {
            try
            {
                // Lê a chave da API do App.config
                string apiKey = ConfigurationManager.AppSettings["GoogleAI_ApiKey"];
                // Lê o nome do modelo (usa "gemini-pro" como padrão)
                _modelNameFromConfig = ConfigurationManager.AppSettings["GoogleAI_Model"] ?? "gemini-pro";

                // Verifica se a chave foi fornecida
                if (string.IsNullOrEmpty(apiKey))
                {
                    InitializationError = "Chave da API Google AI não encontrada ou vazia no App.config.";
                    Console.WriteLine($"WARN: {InitializationError}");
                    IsInitialized = false;
                    return;
                }

                // Cria o cliente/modelo usando o NOME BASE (sem prefixo "models/")
                _googleAIModel = new GenerativeModel(apiKey, model: _modelNameFromConfig);

                IsInitialized = true;
                Console.WriteLine($"INFO: Cliente Google GenerativeAI ({_modelNameFromConfig}) inicializado.");
            }
            catch (Exception ex)
            {
                // Guarda o erro se a inicialização falhar
                InitializationError = $"Erro ao inicializar cliente Google: {ex.Message}";
                Console.WriteLine($"ERRO: {InitializationError}");
                IsInitialized = false;
            }
        }

        // Envia um prompt único para a IA e retorna a resposta.
        public async Task<string> ObterSugestaoAsync(string promptUsuario, string systemPrompt = "Você é um assistente prestativo.")
        {
            // Verifica se o serviço está pronto
            if (!IsInitialized || _googleAIModel == null)
            {
                return InitializationError ?? "Erro: Serviço de IA não inicializado.";
            }

            try
            {
                // Monta o prompt completo
                string promptCompleto = $"{systemPrompt}\n\nAnalise o seguinte problema:\n{promptUsuario}\n\nSugestão:";

                // Envia a consulta como uma única chamada
                GenerateContentResponse response = await _googleAIModel.GenerateContentAsync(promptCompleto);

                // Retorna o texto da resposta
                return response.Text ?? "A IA respondeu, mas o conteúdo estava vazio.";
            }
            catch (Exception ex)
            {
                // Retorna uma mensagem de erro se a chamada falhar
                Console.WriteLine($"ERRO API Google: {ex.ToString()}");
                return $"Erro ao contactar a IA. Detalhes: {ex.Message}";
            }
        }
    }
}
