namespace LayoutSpecificationApi;

public class Result
{
    public static Result Failure(string specificationNotFound)
    {
        throw new NotImplementedException();
    }

    public bool IsFailure { get; set; }
    public bool Value { get; set; }
    public object? Error { get; set; }

    public static Result Success(string b)
    {
        throw new NotImplementedException();
    }
    
    public static Result Success(bool b)
    {
        throw new NotImplementedException();
    }
}