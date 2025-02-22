using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiFinacias.Model;

public class ErrorModel 
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string InnerException { get; set; }

}

