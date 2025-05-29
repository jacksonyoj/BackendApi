// Models/ServiceResult.cs
using Microsoft.VisualBasic;

public class ServiceResult
{
    public bool Success => Errors.Count == 0;
    public List<string> Errors { get; set; } = new();
    public object? Data { get; set; }
}