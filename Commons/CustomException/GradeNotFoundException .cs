namespace Commons.CustomException;

public class GradeNotFoundException : Exception
{
    public GradeNotFoundException(string message) : base(message){}
}
