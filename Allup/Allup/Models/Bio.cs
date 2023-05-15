using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allup.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public string HeaderDescription { get; set; }
        public string HeaderImage { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string HeaderPhone { get; set; }
        public string FooterAddress { get; set; }
        public string FooterPhone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string FooterEmail { get; set; }
        public DateTime FooterOpenDay { get; set; }
        public DateTime FooterCloseDay { get; set; }
        public DateTime FooterOpenTime { get; set; }
        public DateTime FooterCloseTime { get; set; }

    }
}
