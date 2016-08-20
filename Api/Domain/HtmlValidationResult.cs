using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Domain
{
    public class HtmlValidationResult
    {
        public bool IsValid { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }

        public HtmlValidationResult(bool isValid, IEnumerable<string> messages = null)
        {
            IsValid = isValid;
            ErrorMessages = messages;
        }

        public static HtmlValidationResult ValidResult => new HtmlValidationResult(true);

        public static HtmlValidationResult ErrorResult(IEnumerable<string> messages) => new HtmlValidationResult(false, messages);
    }
}