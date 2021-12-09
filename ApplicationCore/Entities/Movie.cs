﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Tagline { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? CreatedBy { get; set; }

        // need rating property but not want it as a col in the table
        [NotMapped]
        public decimal? Rating { get; set; }

        //navigation property
        public List<Trailer> Trailers { get; set; }
        public List<MovieGenre> GenresOfMovie { get; set; }
        public List<Favorite> Favorites { get; set; }
        public List<Purchase> Purchase { get; set; }
        public List<MovieCrew> CrewsOfMovie { get; set; }
        public List<MovieCast> CastsOfMovie { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
