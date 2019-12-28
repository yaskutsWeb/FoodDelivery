
using System.ComponentModel.DataAnnotations;


namespace FoodDelivery.Models
{
	public class LoginModel
	{
		[Display(Name = "Email")]
		[Required(ErrorMessage = "Введите Email")]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Неверный формат электронной почты")]
		public string Email { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Введите пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}