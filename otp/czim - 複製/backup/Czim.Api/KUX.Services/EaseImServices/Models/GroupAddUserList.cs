using System.Collections.Generic;

namespace KUX.Services.EaseImServices.Models
{
    public class GroupAddUserList
    {
        public List<string> newmembers { get; set; }

        public string groupid { get; set; }

        public string action { get; set; }
    }
}