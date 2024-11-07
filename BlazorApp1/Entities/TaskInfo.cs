using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace BlazorApp1.Entities;

//public class TaskInfo(string title, string term, string state, string details)
public class TaskInfo(string title, string term, string state, string details, string span)
{
    public string Title { get; } = title;
    public string Term { get; } = term;
    public string State { get; } = state;
    public string Details { get; } = details;
    public string span { get; set; } = span;
}
