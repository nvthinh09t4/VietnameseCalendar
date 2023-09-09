using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    //con giáp
    public enum eZodiac
    {
        [Display(Name = "Tý")]
        Tys,
        [Display(Name = "Sửu")]
        Suu,
        [Display(Name = "Dần")]
        Dan,
        [Display(Name = "Mão")]
        Mao,
        [Display(Name = "Thìn")]
        Thin,
        [Display(Name = "Tỵ")]
        Ty,
        [Display(Name = "Ngọ")]
        Ngo,
        [Display(Name = "Mùi")]
        Mui,
        [Display(Name = "Thân")]
        Than,
        [Display(Name = "Dậu")]
        Dau,
        [Display(Name = "Tuất")]
        Tuat,
        [Display(Name = "Hợi")]
        Hoi
    }
}
