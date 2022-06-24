using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Api.Validations.ValidationAttributes
{
    public class EmailAttribute : DataTypeAttribute
    {
        private static bool EnableFullDomainLiterals { get; } =
            AppContext.TryGetSwitch("System.Net.AllowFullDomainLiterals", out bool enable) && enable;

        public EmailAttribute() : base(DataType.EmailAddress)
        {
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string valueAsString))
            {
                return false;
            }

            if (string.IsNullOrEmpty(valueAsString))
            {
                return true;
            }

            if (!EnableFullDomainLiterals && (valueAsString.Contains('\r') || valueAsString.Contains('\n')))
            {
                return false;
            }

            // only return true if there is only 1 '@' character
            // and it is neither the first nor the last character
            int index = valueAsString.IndexOf('@');

            return
                index > 0 &&
                index != valueAsString.Length - 1 &&
                index == valueAsString.LastIndexOf('@');
        }
    }
}
