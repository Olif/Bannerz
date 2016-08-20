using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain
{
    public class BannerDTO
    {
        public int Id { get; set; }

        public string Html { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}