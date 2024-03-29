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
using System.Windows.Shapes;

namespace DiseasePredictor
{
    /// <summary>
    /// Interaction logic for SearchResultWindow.xaml
    /// </summary>
    
    public partial class SearchResultWindow : Window
    {
        private List<string> Diseases { get; init; }
        public SearchResultWindow(List<string> diseases)
        {
            InitializeComponent();
            Diseases = diseases;
            listViewResult.ItemsSource = Diseases;
        }
    }
}
