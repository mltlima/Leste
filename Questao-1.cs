// Você está trabalhando em um aplicativo financeiro em C# que precisa calcular o 
// Valor Presente Líquido (VPL) de uma série de fluxos de caixa. 
// Explique como você implementaria essa funcionalidade em C#. 
// Considere aspectos como entrada de dados, cálculo do VPL e apresentação dos resultados. 
// Além disso, discuta como lidaria com cenários de erro e como garantiria a 
// eficiência do cálculo para grandes conjuntos de dados.

// Resposta:
// Para implementar a funcionalidade de cálculo do Valor Presente Líquido (VPL) em C#,
// podemos seguir os seguintes passos:

using System;
using System.Collections.Generic;

public class NPVCalculator
{
    public decimal CalculateNPV(decimal initialInvestment, decimal[] cashFlows, decimal discountRate)
    {
        try
        {
            // Validação dos parâmetros de entrada
            ValidateInputs(initialInvestment, cashFlows, discountRate);

            decimal npv = initialInvestment; // Começa com o investimento inicial
            
            // Calcula o VPL para cada fluxo de caixa
            for (int year = 0; year < cashFlows.Length; year++)
            {
                decimal presentValue = cashFlows[year] / (decimal)Math.Pow((1 + (double)discountRate), year + 1);
                npv += presentValue;
            }

            return Math.Round(npv, 2);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"Erro na validação dos dados: {ex.Message}");
        }
        catch (OverflowException)
        {
            throw new OverflowException("Erro de overflow durante os cálculos. Verifique os valores de entrada.");
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro inesperado ao calcular o VPL: {ex.Message}");
        }
    }

    private void ValidateInputs(decimal initialInvestment, decimal[] cashFlows, decimal discountRate)
    {
        // Validação do investimento inicial
        if (initialInvestment >= 0)
        {
            throw new ArgumentException("O investimento inicial deve ser um valor negativo.");
        }

        // Validação dos fluxos de caixa
        if (cashFlows == null || cashFlows.Length == 0)
        {
            throw new ArgumentException("É necessário fornecer pelo menos um fluxo de caixa.");
        }

        // Validação da taxa de desconto
        if (discountRate <= 0 || discountRate >= 1)
        {
            throw new ArgumentException("A taxa de desconto deve estar entre 0 e 1 (exclusive).");
        }
    }
}

// Código principal executado diretamente
var calculator = new NPVCalculator();

try
{
    // Exemplo de uso
    decimal initialInvestment = -1000m; // Investimento inicial de \$1000
    decimal[] cashFlows = new decimal[] { 200m, 300m, 400m, 500m }; // Fluxos de caixa futuros
    decimal discountRate = 0.1m; // Taxa de desconto de 10%

    decimal npv = calculator.CalculateNPV(initialInvestment, cashFlows, discountRate);

    Console.WriteLine($"O Valor Presente Líquido (VPL) é: ${npv}");

    // Análise do resultado
    if (npv > 0)
    {
        Console.WriteLine("O projeto é financeiramente viável.");
    }
    else if (npv < 0)
    {
        Console.WriteLine("O projeto não é financeiramente viável.");
    }
    else
    {
        Console.WriteLine("O projeto está no ponto de equilíbrio.");
    }
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro de validação: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

// Tratamento de Cenários de Erro
// Abordando o tratamento de erros em cálculos de VPL considerando múltiplas camadas de proteção:

// 1.1. Validações Preventivas
// Verificação de Dados de Entrada: Análise rigorosa do investimento inicial, fluxos de caixa e taxa de desconto
// Limites Operacionais: Estabelecimento de thresholds para evitar overflow em cálculos
// Consistência Temporal: Validação da sequência cronológica dos fluxos de caixa
// 1.2. Gestão de Exceções
// Hierarquia de Exceções: Categorização clara de erros (validação, cálculo, sistema)
// Contexto Detalhado: Captura e enriquecimento de informações de erro
// Recuperação: Estratégias de fallback quando apropriado

// 2. Otimização para Grandes Conjuntos
// 2.1. Estratégias de Performance
// Processamento Paralelo: Utilização de recursos multicore para cálculos independentes
// Gerenciamento de Memória: Uso de estruturas de dados eficientes e garbage collection consciente
// Caching Inteligente: Armazenamento de resultados intermediários frequentemente utilizados
// 2.2. Escalabilidade
// Processamento em Lotes: Divisão de grandes conjuntos em chunks gerenciáveis
// Assincronicidade: Execução não-bloqueante para melhor resposta do sistema
// Monitoramento de Recursos: Controle de consumo de CPU e memória

// 3. Boas Práticas de Implementação
// 3.1. Arquitetura
// Separação de Responsabilidades: Módulos distintos para cálculo, validação e tratamento de erros
// Configuração Flexível: Parâmetros ajustáveis para diferentes cenários de uso
// Logging Estruturado: Registro detalhado de operações e erros
// 3.2. Monitoramento
// Métricas de Performance: Tempo de execução, uso de recursos, taxa de erros
// Alertas: Notificações para situações críticas
// Diagnóstico: Ferramentas para análise de problemas em produção
