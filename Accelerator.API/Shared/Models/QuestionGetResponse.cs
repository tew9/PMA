using System;
using System.Collections.Generic;
using System.Text;

namespace Accelerator.API.Shared.Models
{
    public class QuestionGetResponse
    {
        public Info Info { get; set; }
        public Error Error { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
        //list either boolQuestions or 
        public IEnumerable<Questions> Values { get; set; }
    }
}
