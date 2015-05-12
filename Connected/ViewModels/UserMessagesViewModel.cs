using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connected.ViewModels
{
    public class UserMessagesViewModel
    {
        public List<UserMessageViewModel> RecievedMessages { get; set; }
        public List<UserMessageViewModel> SentMessages { get; set; }
    }
}