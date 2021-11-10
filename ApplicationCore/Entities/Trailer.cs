using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public int MovieId { get; set; }    // Will automatically detect and create the foreign key for us (Because we have a table called Movie and movie table has a column called Id)
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        public Movie Movie { get; set; }    // Navigation Property (one trailer belongs to one movie)
    }
}
