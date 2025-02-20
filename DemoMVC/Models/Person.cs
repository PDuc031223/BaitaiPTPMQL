using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MvcMovie.Models;

public class Person
{
    public string PersonID { get; set; }
    public string FullName { get; set; }
    public string Address { get; set; }
}