    using System;
using System.Collections.Generic;

namespace APIOnly.Models
{
    public partial class TblCommunity
    {
        public int Id { get; set; }
        public string? GivenName { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
    }
}
