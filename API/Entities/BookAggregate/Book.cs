using System.ComponentModel.DataAnnotations;
using API.Entities.BaseEntities;
using API.Entities.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace API.Entities.BookAggregate;

public class Book : BaseEntity 
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    
    public string HighlightText { get; set; }
    
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    public ICollection<Author> Authors { get; set; }

    public int LanguageId { get; set; }
    public Language Language { get; set; }
    
    public int PageCount { get; set; }
    public DateTime PublicationDate { get; set; }
    public string ISBN { get; set; }
}