using System.Net;
using System.Net.Mail;
using System.Text;
using FoodDelivery.Domain.Abstract;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Concrete
{
	public class EmailSettings
	{
		public string MailToAddress = "kaptowka1999@mail.ru";
		public string MailFromAddress = "pavel.yaskuts@gmail.com";
		public bool UseSsl = true;
		public string Username = "MySmtpUsername";
		public string Password = "MySmtpPassword";
		public string ServerName = "smtp.example.com";
		public int ServerPort = 587;
		public bool WriteAsFile = true;
		public string FileLocation = @"c:\food_delivery_emails";
	}

	public class EmailOrderProcessor : IOrderProcessor
	{
		private EmailSettings emailSettings;

		public EmailOrderProcessor(EmailSettings settings)
		{
			emailSettings = settings;
		}

		public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
		{
			using (var smtpClient = new SmtpClient())
			{
				smtpClient.EnableSsl = emailSettings.UseSsl;
				smtpClient.Host = emailSettings.ServerName;
				smtpClient.Port = emailSettings.ServerPort;
				smtpClient.UseDefaultCredentials = false;
				smtpClient.Credentials
					= new NetworkCredential(emailSettings.Username, emailSettings.Password);

				if (emailSettings.WriteAsFile)
				{
					smtpClient.DeliveryMethod
						= SmtpDeliveryMethod.SpecifiedPickupDirectory;
					smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
					smtpClient.EnableSsl = false;
				}

				StringBuilder body = new StringBuilder()
					.AppendLine("Новый заказ обработан")
					.AppendLine("---")
					.AppendLine("Заказанная еда:");

				foreach (var line in cart.Lines)
				{
					var subtotal = line.Food.Price * line.Quantity;
					body.AppendFormat("{0} x {1} (итого: {2:c}",
						line.Quantity, line.Food.Name, subtotal);
				}

				body.AppendFormat("Общая стоимость: {0:c}", cart.ComputeTotalValue())
					.AppendLine("---")
					.AppendLine("Доставка:")
					.AppendLine(shippingInfo.Name)
					.AppendLine(shippingInfo.Number.ToString())
					.AppendLine(shippingInfo.City)
					.AppendLine("---")
					.AppendFormat("Оплата карточкой?: {0}",
						shippingInfo.GiftWrap ? "Да" : "Нет");

				MailMessage mailMessage = new MailMessage(
									   emailSettings.MailFromAddress,   // От кого
									   emailSettings.MailToAddress,     // Кому
									   "Новый заказ отправлен!",        // Тема
									   body.ToString());                // Тело письма

				if (emailSettings.WriteAsFile)
				{
					mailMessage.BodyEncoding = Encoding.UTF8;
				}

				smtpClient.Send(mailMessage);
			}
		}
	}
}