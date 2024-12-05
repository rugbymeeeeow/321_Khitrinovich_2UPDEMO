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


namespace _321_Khitrinovich_2UP.AllPages
{
    /// <summary>
    /// Логика взаимодействия для AddRedactDataPartners.xaml
    /// </summary>
    public partial class AddRedactDataPartners : Page
    {
       // Для редактирования
        private Partners _currentPartner = new Partners();
        // Конструктор для добавления нового партнера
        public AddRedactDataPartners(Partners selectedPartner)
        {
            InitializeComponent();
            // Инициализация комбобокса для выбора типа партнера
            TypeComboBox.ItemsSource = new[] { "ЗАО", "ООО", "ПАО", "ОАО" }; // Здесь укажите реальные типы
            if (selectedPartner != null)
            {
                _currentPartner = selectedPartner;
                if (_currentPartner.Type.ToString() == "ЗАО")
                {
                    TypeComboBox.SelectedItem = TypeComboBox.Items[0];
                }
                if (_currentPartner.Type.ToString() == "ООО")
                {
                    TypeComboBox.SelectedItem = TypeComboBox.Items[1];
                }
                if (_currentPartner.Type.ToString() == "ПАО")
                {
                    TypeComboBox.SelectedItem = TypeComboBox.Items[2];
                }
                else { TypeComboBox.SelectedItem = TypeComboBox.Items[3]; }
            }
            DataContext = _currentPartner;
        }

        // Обработчик для кнопки "Сохранить"
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPartner.NamePartners))
                errors.AppendLine("Укажите наименование!");
            if (string.IsNullOrWhiteSpace(_currentPartner.Contacts))
                errors.AppendLine("Укажите контакты!");
            if (string.IsNullOrWhiteSpace(_currentPartner.Email))
                errors.AppendLine("Укажите почту!");
            if (string.IsNullOrWhiteSpace(_currentPartner.Director))
                errors.AppendLine("Укажите директора!");
            if (!int.TryParse(_currentPartner.Raiting.ToString(), out int rating) || rating <= 0)
            {
                errors.AppendLine("Рейтинг должен быть положительным целым числом!");
            }
            //if (Convert.ToInt32(_currentPartner.Raiting) <= 0)
            //    errors.AppendLine("Рейтинг не может быть отрицательным!");
            if ((_currentPartner.Type == null) || (TypeComboBox.Text == ""))
                    errors.AppendLine("Выберите тип!");
            else
                _currentPartner.Type = TypeComboBox.Text;
            if (string.IsNullOrWhiteSpace(_currentPartner.UrAddress))
                errors.AppendLine("Укажите адрес");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentPartner.ID == 0)
                Entities.GetContext().Partners.Add(_currentPartner);
            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
