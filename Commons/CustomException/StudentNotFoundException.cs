namespace Commons.CustomException;

public class StudentNotFoundException:Exception
{
    public StudentNotFoundException(string message):base(message){}
}
