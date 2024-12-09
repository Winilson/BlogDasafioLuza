namespace Desafio.Blog.Luza.Core.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco) || !endereco.Contains("@"))
                throw new ArgumentException("E-mail inválido.");

            Endereco = endereco;
        }
    }
}
