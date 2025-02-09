﻿namespace Oid85.HomeBot.Domain.Exceptions;

[Serializable]
public class CustomBusinessException : Exception
{
    public string Code { get; set; }
        
    public CustomBusinessException(string code, string message) : base(message)            
    {
        Code = code;
    }

    public CustomBusinessException(string code, string message, Exception exception) : base(message, exception)
    {
        Code = code;
    }
}