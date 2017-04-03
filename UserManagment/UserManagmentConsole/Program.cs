using System;
using System.Globalization;
using System.Linq;

using LinqToDB;

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
                    case 3:
                        InsertRole();
                        break;
                    case 4:
                        InsertUser();
                        break;
                    default:
                        Console.WriteLine("До свидания, {0}", userName);
                        break;
                }
            } while ( choice != 5 );

            Console.ReadKey();
        }

        private static void PrintMainMenu()
        {
            Console.WriteLine("Выберите пункт:");
            Console.WriteLine("1. Вывести список ролей.");
            Console.WriteLine("2. Вывести список всех пользователей");
            Console.WriteLine("3. Ввести новую роль");
            Console.WriteLine("4. Ввести нового пользователя");
            Console.WriteLine("5. Выход");
        }

        private static int UserSelection()
        {
            bool isChoiceRight;
            int choice;

            do
            {
                Console.Write("Введите номер пункта: ");
                var inputString = Console.ReadLine();

                if ( int.TryParse(inputString, out choice) && choice >= 1 && choice <= 5 )
                {
                    isChoiceRight = true;
                }
                else
                {
                    Console.WriteLine("Вы должны ввести число от 1 до 5");
                    isChoiceRight = false;
                }

            } while ( !isChoiceRight );

            return choice;
        }

        private static void DisplayRoles()
        {
            Console.WriteLine("====== ВСЕ РОЛИ ======");

            using ( var db = new FirstDB() )
            {
                var allRoles = (
                                from r in db.Roles
                                select r
                               ).ToList();

                foreach ( var role in allRoles )
                {
                    Console.WriteLine("ID: {0}", role.Id);
                    Console.WriteLine("Имя: {0}", role.Name);
                    Console.WriteLine("----------------");
                }
            }

            Console.WriteLine("=====================");
        }

        private static void DisplayUsers()
        {
            Console.WriteLine("====== ВСЕ ПОЛЬЗОВАТЕЛИ ======");

            using ( var db = new FirstDB() )
            {
                var allUsers = (
                                from r in db.Users
                                select r
                               ).ToList();

                foreach ( var user in allUsers )
                {
                    Console.WriteLine("ID: {0}", user.Id);
                    Console.WriteLine("ФИО: {0} {1} {2}", user.Surname, user.Firstname, user.Patronymic);
                    Console.WriteLine("Дата рождения: {0}", user.BirthDate.HasValue ? Convert.ToDateTime(user.BirthDate).ToString("dd.MM.yyyy") : string.Empty);
                    Console.WriteLine("----------------");
                }
            }

            Console.WriteLine("=====================");
        }

        private static void InsertRole()
        {
            Console.WriteLine("====== НОВАЯ РОЛЬ ======");
            Console.Write("Имя роли: ");
            string roleName = Console.ReadLine();

            if ( ValidateRole(roleName) )
            {
                using (var db = new FirstDB())
                {
                    db.Roles.Insert(() => new Role { Name = roleName });
                    Console.WriteLine("Новая роль добавлена.");
                }
            }

            Console.WriteLine("======================");
        }

        private static bool ValidateRole(string roleName)
        {
            if ( string.IsNullOrWhiteSpace(roleName) )
            {
                DisplayFieldMustNotNull("Имя роли");
                return false;
            }

            return true;
        }

        private static void InsertUser()
        {
            Console.WriteLine("====== НОВЫЙ ПОЛЬЗОВАТЕЛЬ ======");
            
            Console.Write("Логин пользователя: ");
            string userName = Console.ReadLine();
            
            Console.Write("Фамилия пользователя: ");
            string surname = Console.ReadLine();
            
            Console.Write("Имя пользователя: ");
            string firstName = Console.ReadLine();
            
            Console.Write("Отчество пользователя: ");
            string patronymic = Console.ReadLine();
            
            Console.Write("Электронная почта: ");
            string email = Console.ReadLine();
            
            Console.Write("Дата рождения (ДД.ММ.ГГГГ): ");
            string birthDateStr = Console.ReadLine();
            
            Console.Write("Пол (M или F): ");
            string sex = Console.ReadLine();

            Console.Write("Мобил.номер: ");
            string mobNumber = Console.ReadLine();

            Console.Write("ID роли: ");
            string roleIdStr = Console.ReadLine();
            
            if ( ValidateUser(userName, surname, firstName, email, birthDateStr, sex, mobNumber, roleIdStr) )
            {
                DateTime birthDate;
                DateTime.TryParseExact(birthDateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

                int roleId;
                int.TryParse(roleIdStr, out roleId);

                using ( var db = new FirstDB() )
                {
                    db.Users.Insert(() =>
                                    new User
                                    {
                                        UserName     = userName,
                                        Surname      = surname,
                                        Firstname    = firstName,
                                        Patronymic   = patronymic,
                                        BirthDate    = birthDate,
                                        Email        = email,
                                        Sex          = sex,
                                        MobileNumber = mobNumber,
                                        RoleId       = roleId
                                    });

                    Console.WriteLine("Новый пользователь добавлен.");
                }
            }
            Console.WriteLine("======================");

        }

        private static bool ValidateUser(string userName,
                                         string surname,
                                         string firstName,
                                         string email,
                                         string birthDateStr,
                                         string sex,
                                         string mobNumber,
                                         string roleIdStr)
        {
            bool isOk = true;
            string fieldStr;

            // Проверка логина (user_name)
            if ( string.IsNullOrWhiteSpace(userName) )
            {
                fieldStr = "Логин пользователя";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }

            // Проверка фамилии (surname)
            if ( string.IsNullOrWhiteSpace(surname) )
            {
                fieldStr = "Фамилия пользователя";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }

            // Проверка имени (first_name)
            if ( string.IsNullOrWhiteSpace(firstName) )
            {
                fieldStr = "Имя пользователя";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }

            // Проверка электронной почты (email)
            if ( string.IsNullOrWhiteSpace(email) )
            {
                fieldStr = "Электронная почта";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }

            // Проверка даты рождения (birth_date)
            if ( !string.IsNullOrWhiteSpace(birthDateStr) )
            {
                DateTime dt;
                // Проверяем, введенная строка в формате DD.MM.YYYY или нет, и можно ли получить из строки дату
                if ( !DateTime.TryParseExact(birthDateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) )
                {
                    fieldStr = "Дата рождения";
                    DisplayWrongFormat(fieldStr);
                    isOk = false;
                }
            }

            // Проверка пола (sex)
            if ( string.IsNullOrWhiteSpace(sex) )
            {
                fieldStr = "Пол";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }
            else
            {
                if ( !( sex == "M"  |  sex == "F" ) )
                {
                    fieldStr = "Пол";
                    DisplayFieldMustEqualOneOfThis(fieldStr, "M", "F");
                    isOk = false;
                }
            }

            // Проверка мобильного номера (mobile_number)
            if ( string.IsNullOrWhiteSpace(mobNumber) )
            {
                fieldStr = "Мобил.номер";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }

            // Проверка ID роли (role_id)
            if ( string.IsNullOrWhiteSpace(roleIdStr) )
            {
                fieldStr = "ID роли";
                DisplayFieldMustNotNull(fieldStr);
                isOk = false;
            }
            return isOk;
        }

        private static void DisplayFieldMustNotNull(string fieldStr)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Поле '{0}' не может быть пустым", fieldStr);
            Console.ResetColor();
        }

        private static void DisplayFieldMustEqualOneOfThis(string fieldStr, params string[] values)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Поле '{0}' может принимать одно из следующих значений: {1}", fieldStr, string.Join(", ", values));
            Console.ResetColor();
        }

        private static void DisplayWrongFormat(string fieldStr)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Поле '{0}' имеет неправильный формат", fieldStr);
            Console.ResetColor();
        }
    }
}
