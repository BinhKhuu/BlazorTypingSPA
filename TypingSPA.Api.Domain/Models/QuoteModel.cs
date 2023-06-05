using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingSPA.Api.Domain.Models
{
    public class QuoteModel
    {
        public int Id { get; set; } = 0;
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
        public string AuthorSlug { get; set; } = string.Empty;
        public int Length { get; set; } = 0;
        public string DateAdded { get; set; } = string.Empty;
        public string dateModfied { get; set; } = string.Empty;
    }
}
