using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllLyrics.Core
{
    public class Admin
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            public string Login { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public Role Role { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime LastChanged { get; set; }
        
    }
}