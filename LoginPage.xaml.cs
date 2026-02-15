using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prac2
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введённые данные
            string login = LoginInput.Text.Trim();
            string password = PasswordInput.Password;

            // Проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Предупреждение",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Используем контекст базы данных (синглтон)
            using (var context = Data.AuthEntities1.GetContext())
            {
                // Ищем пользователя с таким логином и паролем
                var user = context.Users
                    .FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    // Успешная авторизация – переходим на домашнюю страницу
                    Manager.MainFrame.Navigate(new homePage());
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
