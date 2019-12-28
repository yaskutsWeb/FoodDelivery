using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
	public class RegisterModel
	{
		[Display(Name = "Email")]
		[Required(ErrorMessage = "Укажите Email")]
		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Неверный формат электронной почты")]
		public string Email { get; set; }


		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Введите пароль")]
		[StringLength(30, MinimumLength = 8, ErrorMessage = "Недопустимая длина пароля")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Подтверждение пароля")]
		[Required(ErrorMessage = "Введите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		public string PasswordConfirm { get; set; }

		[Display(Name = "Имя")]
		[Required(ErrorMessage = "Укажите имя")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Недопустимая длина имени")]
		public string Name { get; set; }


		[Display(Name = "Фамилия")]
		[Required(ErrorMessage = "Укажите фамилию")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Недопустимая длина фамилии")]
		public string Surname { get; set; }
	}
}

