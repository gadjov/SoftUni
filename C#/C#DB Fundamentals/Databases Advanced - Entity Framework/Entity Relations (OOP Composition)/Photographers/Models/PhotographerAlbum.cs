﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Photographers.Models
{
    public enum Role
    {
        Owner, Viewer
    }
    public class PhotographerAlbum
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Photographer")]
        public virtual int Photographer_Id { get; set; }

        public Photographer Photographer { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Album")]
        public int Album_Id { get; set; }

        public virtual Album Album { get; set; }
        public Role Role { get; set; }
    }
}
