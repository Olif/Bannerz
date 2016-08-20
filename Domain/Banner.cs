using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public class Banner
    {
        public int Id { get; private set; }
        public string Html { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public Banner(string html, int id)
        {
            Id = id;
            Html = html;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }

        public static Banner Create(string html, IIdGenerator idGenerator)
        {
            return new Banner(html, idGenerator.Next());
        }

        public void UpdateHtml(string html)
        {
            Html = html;
            Modified = DateTime.UtcNow;
        }
    }
}