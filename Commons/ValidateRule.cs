using System.Text.RegularExpressions;

namespace Commons;

public static class ValidateRule
{
    public static bool ValidateBirthday(string strDate)
    {
        DateTime date;
        return DateTime.TryParse(strDate, out date);
    }
    public static bool ValidateGradeName(string gradeName)
    {
        string gradeNum = gradeName.Substring(0, gradeName.Length - 2);
        string pattern = @"^(?:[1-9]|1[0-9]|10)$";
        return Regex.IsMatch(gradeNum, pattern);
    }
    public static bool ValidateSectionName(string sectionName)
    {
        string sectionNum = sectionName.Substring(0, sectionName.Length - 1);
        string pattern = @"^(?:[1-9]|1[0-9]|20)$";
        var match = Regex.IsMatch(sectionNum, pattern);
        return match;
    }
}
