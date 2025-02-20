using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MvcMovie.Models;

public class Movie
{

    public int Id { get; set; }
    public string? Title { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public string? Genre { get; set; }
    public decimal Price { get; set; }

     
}