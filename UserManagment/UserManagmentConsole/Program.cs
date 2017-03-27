using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserManagmentLib.DataModels;

namespace UserManagmentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите свое имя пользователя:");
            var inputString = Console.ReadLine();
            var userName = inputString;

            Console.WriteLine("Добро пожаловать, {0}", userName);

            int choice;
            do
            {
                PrintMainMenu();
                choice = UserSelection();

                switch ( choice )
                {
                    case 1:
                        DisplayRoles();
                        break;
                    case 2:
                        DisplayUsers();
                        break;
                    default:
                        Console.WriteLine("До свидания, {0}", userName);
                        break;
                }

            } while ( choice != 3 );

            Console.ReadKey();
        }

        private static void PrintMainMenu()
        {
            Console.WriteLine("Выберите пункт:");
            Console.WriteLine("1. Вывести список ролей.");
            Console.WriteLine("2. Вывести список всех пользователей");
            Console.WriteLine("3. Выход");
        }

        private static int UserSelection()
        {
            bool isChoiceRight;
            int choice;

            do
            {
                Console.Write("Введите номер пункта: ");
                var inputString = Console.ReadLine();

                if ( int.TryParse(inputString, out choice) && choice >= 1 && choice <= 3 )
                {
                    isChoiceRight = true;
                }
                else
                {
                    Console.WriteLine("Вы должны ввести число от 1 до 3");
                    isChoiceRight = false;
                }

            } while ( !isChoiceRight );

            return choice;
        }

        private static void DisplayRoles()
        {
            Console.WriteLine("====== ALL ROLES ======");

            using ( var db = new FirstDB() )
            {
                var allRoles = (
                                from r in db.Roles
                                select r
                               ).ToList();

                foreach ( var role in allRoles )
                {
                    Console.WriteLine("ID: {0}", role.Id);
                    Console.WriteLine("NAME: {0}", role.Name);
                    Console.WriteLine("----------------");
                }
            }

            Console.WriteLine("=====================");
        }

        private static void DisplayUsers()
        {
            Console.WriteLine("====== ALL USERS ======");

            using ( var db = new FirstDB() )
            {
                var allUsers = (
                                from r in db.Users
                                select r
                               ).ToList();

                foreach ( var user in allUsers )
                {
                    Console.WriteLine("ID: {0}", user.Id);
                    Console.WriteLine("NAME: {0} {1} {2}", user.Surname, user.Firstname, user.Patronymic);
                    Console.WriteLine("----------------");
                }
            }

            Console.WriteLine("=====================");
        }
    }
}
