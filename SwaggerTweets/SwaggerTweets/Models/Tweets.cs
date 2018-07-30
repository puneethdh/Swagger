using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SwaggerTweets.Models
{
    public class Tweets
    {
        [Key]
        public string iD { get; set; }
        [Display(Name ="Tweet Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime stamp { get; set; }
        [Display(Name  = "Tweet")]
        public string text { get; set; }
        [Display(Name = "Tweeter Link")]
        public string Link { get; set; }

    }
}