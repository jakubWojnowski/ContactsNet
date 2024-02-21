namespace ContactsNet.Core.Validations.Helpers;

internal static class PasswordHelper
{
    private static string _specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
    public static bool HasNumber(string password) => password.Any(char.IsDigit);
    public static bool HasCapitals(string password) => password.Any(char.IsUpper);
    public static bool HasLowercase(string password) => password.Any(char.IsLower);
    public static bool HasSpecialCharacters(string password) => password.Any(ch => _specialCharacters.Contains(ch));
}