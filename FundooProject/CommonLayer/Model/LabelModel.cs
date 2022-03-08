using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class LabelModel
    {
        [Required]
        public string labelName { get; set; }
    }
}
