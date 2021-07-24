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

// Debug
using System.Diagnostics;


namespace DiseasePredictor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Predictor _Predictor = new Predictor();
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Trace.WriteLine("you clicked");
            List<string> symptoms = new List<string>{
                "stomach_pain","acidity", "vomiting","cough","chest_pain"
            };
            this._Predictor.Predict(symptoms).ForEach(delegate (string s)
            {
                Trace.WriteLine(s);
            });
            //symptoms.ForEach(delegate (string s)
            //{
            //    Trace.WriteLine(s);
            //});
        }
    }
}
