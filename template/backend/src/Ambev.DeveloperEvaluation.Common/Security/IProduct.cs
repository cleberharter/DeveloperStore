namespace Ambev.DeveloperEvaluation.Common.Security;

public interface IProduct
{
    /// <summary>
    /// Obtém o identificador único do usuário.
    /// </summary>
    /// <returns>O ID do usuário como uma string.</returns>
    public string Id { get; }
}
