using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MoviesDto
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
    
        
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
     
        public DateTime ReleasedDate { get; set; }
     
        public DateTime DateAdded { get; set; }
   
        public GenreDto Genre { get; set; }
        public int stock { get; set; }
    }
}