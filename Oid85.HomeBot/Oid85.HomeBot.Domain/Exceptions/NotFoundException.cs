namespace Oid85.HomeBot.Domain.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public string Code { get; set; }
        
    public NotFoundException(string code, string message) : base(message)            
    {
        Code = code;
    }

    public NotFoundException(string code, string message, Exception exception) : base(message, exception)
    {
        Code = code;
    }
}