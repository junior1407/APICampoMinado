using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Record
    {
        // nome, pontos, tempo, data-hora.
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string dateTime{ get; set; }
    }
}