using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using static _321_Khitrinovich_2UP.AllPages.ListPartners;

namespace _321_Khitrinovich_2UP.AllPages
{

    public class PartnerWithDiscount
    {
        public string Type { get; set; }
        public string NamePartners { get; set; }
        public string Director { get; set; }
        public string Contacts { get; set; }
        public decimal TotalSales { get; set; }
        public string Raiting { get; set; }
        public decimal Discount { get; set; }

        public void CalculateDiscount()
        {
            if (TotalSales <= 10000)
            {
                Discount = 0;
            }
            else if (TotalSales <= 50000)
            {
                Discount = 5;
            }
            else if (TotalSales <= 300000)
            {
                Discount = 10;
            }
            else
            {
                Discount = 15;
            }
        }
    }
    public partial class ListPartners : Page
    {

        private void AddPartnerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRedactDataPartners(null));
        }
        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                var currentUsers = Entities.GetContext().Partners.ToList();
                ListUser.ItemsSource = currentUsers;
                var skidkiPachet = Entities.GetContext().PartnerProducts.ToList();

                var groupedByPartner = skidkiPachet.GroupBy(x => x.Partners);
                var partners = Entities.GetContext().Partners.ToList();

                // Получаем данные о продажах партнера
                var partnerProducts = Entities.GetContext().PartnerProducts.ToList();

                var partnerSales = partnerProducts
                    .GroupBy(pp => pp.ID)
                    .Select(g => new
                    {
                        PartnerId = g.Key,
                        TotalSales = g.Sum(pp => pp.NumProduct)
                    })
                    .ToList();
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                var partnersWithDiscount = partners.Select(partner =>
                {
                    // Находим общие продажи для текущего партнера
                    var totalSales = partnerSales
                        .FirstOrDefault(ps => ps.PartnerId == partner.ID)?.TotalSales ?? 0;

                    var partnerWithDiscount = new PartnerWithDiscount
                    {
                        Type = partner.Type,
                        NamePartners = partner.NamePartners,
                        Director = partner.Director,
                        Contacts = partner.Contacts,
                        TotalSales = totalSales,
                        Raiting = partner.Raiting.ToString(),
                    };

                    partnerWithDiscount.CalculateDiscount();

                    return partnerWithDiscount;
                }).ToList();

                ListUser.ItemsSource = partnersWithDiscount;
                ListUser.Items.Refresh();
            }
        }

        private void ListUserSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListUser.SelectedItem != null)
            {
                var selectedPartner = (PartnerWithDiscount)ListUser.SelectedItem;
                var partner = Entities.GetContext().Partners.First(p => p.NamePartners == selectedPartner.NamePartners);
                NavigationService.Navigate(new AddRedactDataPartners(partner));
            }
        }
        public ListPartners()
        {
            InitializeComponent();
            var currentUsers = Entities.GetContext().Partners.ToList();
            ListUser.ItemsSource = currentUsers;
            var skidkiPachet = Entities.GetContext().PartnerProducts.ToList();

            var groupedByPartner = skidkiPachet.GroupBy(x => x.Partners);
            var partners = Entities.GetContext().Partners.ToList();

            // Получаем данные о продажах партнера
            var partnerProducts = Entities.GetContext().PartnerProducts.ToList();

            var partnerSales = partnerProducts
                .GroupBy(pp => pp.ID)
                .Select(g => new
                {
                    PartnerId = g.Key,
                    TotalSales = g.Sum(pp => pp.NumProduct) 
                })
                .ToList();

            var partnersWithDiscount = partners.Select(partner =>
            {
                // Находим общие продажи для текущего партнера
                var totalSales = partnerSales
                    .FirstOrDefault(ps => ps.PartnerId == partner.ID)?.TotalSales ?? 0;

                var partnerWithDiscount = new PartnerWithDiscount
                {
                    Type = partner.Type,
                    NamePartners = partner.NamePartners,
                    Director = partner.Director,
                    Contacts = partner.Contacts,
                    TotalSales = totalSales,
                    Raiting = partner.Raiting.ToString(), 
                };

                partnerWithDiscount.CalculateDiscount();

                return partnerWithDiscount;
            }).ToList();

            ListUser.ItemsSource = partnersWithDiscount;
            ListUser.Items.Refresh();

        }

    }
}
