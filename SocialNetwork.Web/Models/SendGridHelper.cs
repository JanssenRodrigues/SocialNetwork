using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace SocialNetwork.Web.Models
{
    public class SendGridHelper
    {
        public static async Task sendEmail(string email)
        {
            var client = new SendGridClient(Properties.Resources.SendGridAPI);
            var message = new SendGridMessage();
            message.AddTo(email);
            message.SetFrom(new EmailAddress("noreply@janssen.com", "Teste de Performance"));;
            message.Subject = "Recupere sua senha agora";
            message.HtmlContent = "Nao se lembra da sua senha? Nao se preocupe! <br> Voce pode reseta-la agora mesmo e sem nenhuma dificuldade. Basta acessar o link abaixo e preencher o formulario! <br> http://localhost:56323/Account/RecoveryPass ";

            var response = await client.SendEmailAsync(message);
        }
    }
}