﻿using System;
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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void WatchPartnersButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListPartners());
        }

        private void WatchHistoryButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HistoryRealization());
        }
    }
}
