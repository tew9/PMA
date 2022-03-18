using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Models
{
    public class QuestionPostResponse
    {
        public Info Info { get; set; }
        public Error Error { get; set; }
        public string Status { get; set; }
        
    }
}
