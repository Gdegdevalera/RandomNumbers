using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public enum Operation
    {
        Unknown = 0,
        [Display(Name = "+")]
        Add = 1,
        [Display(Name = "-")]
        Subtract = 2,
        [Display(Name = "*")]
        Multiply = 3,
        [Display(Name = "/")]
        Divide = 4
    }
}
