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
    /// Логика взаимодействия для HistoryRealization.xaml
    /// </summary>
    public partial class HistoryRealization : Page
    {
        public HistoryRealization()
        {
            InitializeComponent();
            LoadPartners(); // Загружаем список партнеров
            LoadHistory();  // Загружаем всю историю реализации при запуске страницы

        }

        // Загружаем список партнеров в ComboBox
        private void LoadPartners()
        {
            var partners = Entities.GetContext().Partners.Select(p => new
            {
                p.ID,
                p.NamePartners // Предположим, что поле Name существует у Partner
            }).ToList();

            PartnerComboBox.ItemsSource = partners;
            PartnerComboBox.SelectedValuePath = "ID"; // Это значение будет использоваться как ID при выборе
        }

        // Загружаем всю историю реализации продукции
        private void LoadHistory(int? partnerId = null)
        {
            var query = Entities.GetContext().PartnerProducts.AsQueryable();

            if (partnerId.HasValue)
            {
                query = query.Where(pp => pp.NameOfPartner == partnerId.Value); // Фильтрация по выбранному партнеру
            }

            var partnerProducts = query.Select(pp => new
            {
                ProductName = pp.Products.NameProduct,
                ProductType = pp.Products.ProductType.TypeProduct,
                pp.NumProduct,
                pp.DateProdaj
            }).ToList();

            // Выводим количество записей в MessageBox для проверки
            MessageBox.Show($"Загружено {partnerProducts.Count} записей");

            if (partnerProducts.Count == 0)
            {
                MessageBox.Show("Данные не найдены.");
            }
            else
            {
                // Отображаем данные в DataGrid
                HistoryDataGrid.ItemsSource = partnerProducts;
            }
        }

        // Обработчик события изменения выбора партнера в ComboBox
        private void PartnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartnerComboBox.SelectedValue != null)
            {
                int partnerId = (int)PartnerComboBox.SelectedValue;
                LoadHistory(partnerId); // Загружаем историю реализации для выбранного партнера
            }
            else
            {
                LoadHistory(); // Если партнер не выбран, загружаем всю историю
            }
        }
    }
}
