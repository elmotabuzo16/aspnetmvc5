using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;

namespace WebApplication2.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //[Display(Name = "Genre")]
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }

        //[Display(Name = "Date Added", Prompt = "1 Jan 2018")]
        public DateTime? DateAdded { get; set; }

        //[Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        //[Display(Name = "Number in Stock", Prompt = "0")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }
    }
}