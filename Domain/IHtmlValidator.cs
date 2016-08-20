using System.Threading.Tasks;

namespace Domain
{
    public interface IHtmlValidator
    {
        Task<HtmlValidationResult> ValidateAsync(string html);
    }
}