

using HealthCare.Application;
using HealthCare.Exceptions;
using HealthCare.Service;
using HealthCare.View.ManagerView;
using HealthCare.View.NurseView;
using HealthCareCli.ConsoleUtil;
using HealthCareCli.Exception;
using HealthCareCli.Exceptions;
using HealthCareCli.Menu;
using HealthCareCli.Util;
using System.Windows.Forms;

namespace HealthCareCli
{
    public class Program
    {
        public static void Main()
        {
            Role loginRole = Role.Manager;
            try
            {
                while (true)
                {
                    if (Context.Current is null)
                        loginRole = LoginHandler();
                    else
                        MenuHandler(loginRole);
                }
            } catch (ExitSignal)
            {
                Console.WriteLine("Exit.");
            }
        }

        private static Role LoginHandler()
        {
            string username, password;
            while (true)
            {
                username = Input.ReadLine("Username: ");
                password = Input.ReadLine("Username: ");

                try
                {
                    return Injector.GetService<LoginService>().Login(username, password);
                } catch (LoginException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void MenuHandler(Role loginRole)
        {
            switch (loginRole)
            {
                case Role.Nurse:
                    new NurseHandler().Show();
                    break;
                case Role.Patient:
                    new PatientHandler().Show();
                    break;
                case Role.Manager:
                    new ManagerHandler().Show();
                    break;
                default:
                    Console.WriteLine("Nije implementirano.");
                    Context.Reset();
                    break;
            }
        }
    }
}