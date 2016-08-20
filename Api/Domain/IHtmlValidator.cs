using System.Threading.Tasks;

namespace Api.Domain
{
    public interface IHtmlValidator
    {
        Task<HtmlValidationResult> ValidateAsync(string html);
    }
}