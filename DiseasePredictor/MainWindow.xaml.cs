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

// TODO : create a clear button
// MainWindow.UpdateComboboxesDiseaseList
// !WARNING:  RemoveSymptom_Click does not work propely (NullIndexError)


namespace DiseasePredictor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Predictor _Predictor;
        private List<string> _Symptoms;
        private HashSet<string> _SymptomsSelected;
        private List<ComboBox> _ComboBoxesSymptoms;
        private Dictionary<string, string> _SymptomsNames;
        public MainWindow()
        {
            InitializeComponent();
            // Init members Value
            this._Predictor = new Predictor();
            this._Symptoms = new List<string>();
            this._SymptomsSelected = new HashSet<string>();  // Input Vector for prediction
            this._ComboBoxesSymptoms = new List<ComboBox>();
            this._SymptomsNames = new Dictionary<string, string>(); // real Symptoms names

            // Load symptoms list using Loader Object
            Loader DiseaseLoader = new Loader();
            this._Symptoms.AddRange(this._Predictor.Symptoms.Keys);
            
            // Prettify the content for printing
            for(int i = 0; i < this._Symptoms.Count; i++)
            {
                string name = this._Symptoms[i];
                this._Symptoms[i] = this._Symptoms[i].Replace('_', ' ');
                this._SymptomsNames[this._Symptoms[i]] = name;
            }
            this._Symptoms = this._Symptoms.OrderBy(key => key).ToList<string>();


            // Populates comboboxes
            this.ComboSelector0.ItemsSource = this._Symptoms;  // Add symptoms to combobox_0
            this.ComboSelector1.ItemsSource = this._Symptoms;  // Add symptoms to combobox_1
            this.ComboSelector2.ItemsSource = this._Symptoms;  // Add symptoms to combobox_1

        }

        
        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            /// Test
            //List<string> symptoms = new List<string>{
            //    "stomach_pain","acidity", "vomiting","cough","chest_pain"
            //};
            //this._Predictor.Predict(symptoms).ForEach(delegate (string s)
            //{
            //    Trace.WriteLine(s);
            //});

            List<string> selects = new List<string>();
            foreach(string s in this._SymptomsSelected.ToList<string>()) 
            {
                selects.Add(this._SymptomsNames[s]);
            }
            //this._Predictor.Predict(selects).ForEach(delegate (string disease) { Trace.WriteLine(disease); });
            
            //string results = string.Join("\n", this._Predictor.Predict(selects));
            //string msg_title_txt = "Predictions Results";
            //MessageBox.Show(results, msg_title_txt);
            SearchResultWindow popupResultWindow = new SearchResultWindow(this._Predictor.Predict(selects));
            popupResultWindow.Show();
        }
        private void Add_ComboBox()
        {
            const int MaximumSymptomsInput = 17 - 3;  // !WARNING hardcoded maximumSymptomsInput, (3 is the minimal inputs)
            if (this._ComboBoxesSymptoms.Count + 1 > MaximumSymptomsInput){ return; } // input limit
            // Test Adding a new Combobox to UI
            ComboBox c = new ComboBox();
            c.Name = $"ComboSelector{this._ComboBoxesSymptoms.Count + 3}";
            c.Width = 210;
            c.Height = 20;
            const int columns = 3;

            // Alignement
            int i, j;
            // Line Alignement
            i = (int)(this._ComboBoxesSymptoms.Count) / columns;
            // Column Alignement
            j = (this._ComboBoxesSymptoms.Count + 1) % columns;
            j = j != 0 ? j - 1 : columns - 1;
            // -i * 40 to go down
            // Thickness(Left, Top, Right, Bottom)
            c.Margin = new Thickness(0, -60 * -i, 455 - 455 * j, 0);

            // Populate the ComboBoxSymptom.Items
            c.ItemsSource = this._Symptoms;


            // Attach a controler to the combobox
            c.SelectionChanged += this.ComboBox_SelectionChanged;

            this.Sub_GridMainLayout.Children.Add(c);
            this._ComboBoxesSymptoms.Add(c);
        }
        private void AddSymptom_Click(object sender, RoutedEventArgs e)
        {
            this.Add_ComboBox();
        }

        private void _UpdateComboboxes()
        {
            try
            {
                //this._Symptoms = this._Symptoms.OrderBy(key => key).ToList<string>();  // unefficient
                // Save Selections of each comboboxes
                string Selection0 = (string)this.ComboSelector0.SelectedItem,
                    Selection1 = (string)this.ComboSelector1.SelectedItem,
                    Selection2 = (string)this.ComboSelector2.SelectedItem;
                // Retrieve selection from dynamics comboboxes
                List<string> Selections = new List<string>();
                foreach (ComboBox b in this._ComboBoxesSymptoms)
                {
                    Selections.Add((string)b.SelectedItem);
                }
                // Update combos datasources
                this.ComboSelector0.ItemsSource = this._Symptoms;
                this.ComboSelector1.ItemsSource = this._Symptoms;
                this.ComboSelector2.ItemsSource = this._Symptoms;
                for (int i = 0; i < this._ComboBoxesSymptoms.Count; i++)
                {

                    //this._ComboBoxesSymptoms[i].ItemsSource = null;
                    ComboBox _c = this._ComboBoxesSymptoms[i];
                    // !Warning: Is this thread safe ?
                    _c.ItemsSource = this._Symptoms;

                    //this._ComboBoxesSymptoms[i].ItemsSource = this._Symptoms;
                    // !Warning: NullReferenceException (?? use an observable ?) 
                    //_c.GetBindingExpression(ComboBox.ItemsSourceProperty).UpdateTarget();


                }

                // Revert Selections
                this.ComboSelector0.SelectedItem = Selection0;
                this.ComboSelector1.SelectedItem = Selection1;
                this.ComboSelector2.SelectedItem = Selection2;
                for (int i = 0; i < this._ComboBoxesSymptoms.Count; i++)
                {
                    this._ComboBoxesSymptoms[i].SelectedItem = Selections[i];
                }
            }
            catch (Exception ex) 
            {
                // [Ostrich Algorithm] pass
            }
            finally { 
            
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = sender as ComboBox;
            var s = c.SelectedItem;
            this._SymptomsSelected.Add((string)s);
            this._Symptoms.Remove((string)s);
            if (c == this.ComboSelector0)
            {

                // TODO: Update combobox items
                this.ComboSelector1.ItemsSource = this._Symptoms;
                this.ComboSelector2.ItemsSource = this._Symptoms;
                return;
            }
            else if(c == this.ComboSelector1)
            {
                this.ComboSelector0.ItemsSource = this._Symptoms;
                this.ComboSelector2.ItemsSource = this._Symptoms;
                return;
            }
            else if( c == this.ComboSelector2){
                this.ComboSelector0.ItemsSource = this._Symptoms;
                this.ComboSelector1.ItemsSource = this._Symptoms;
                return;
            }
            for(int i = 0; i < this._ComboBoxesSymptoms.Count; i++)
            {
                ComboBox combo = this._ComboBoxesSymptoms[i];
                if (combo != c)
                {
                    this._ComboBoxesSymptoms[i].ItemsSource = this._Symptoms;
                }
            }
            
            Trace.WriteLine($"Selected : '{s}'");
        }
        private void __NULL__(object sender, SelectionChangedEventArgs e) { }
        private void RemoveSymptom_Click(object sender, RoutedEventArgs e)
        {
            if (this._ComboBoxesSymptoms.Count > 0)
            {
                int LayoutIndex = this.Sub_GridMainLayout.Children.Count - 1;
                int ComboIndex = this._ComboBoxesSymptoms.Count - 1;
                this._ComboBoxesSymptoms[ComboIndex].SelectionChanged += __NULL__;
                ComboBox c = this._ComboBoxesSymptoms[ComboIndex];
                
                var symptom = c.SelectedItem;
                if (symptom != null)
                {
                    // Remove selected symptom from selectedSymptoms
                    this._SymptomsSelected.Remove((string)symptom);
                    this._Symptoms.Add((string)symptom);

                }

                this.Sub_GridMainLayout.Children.RemoveAt(LayoutIndex);
                this._ComboBoxesSymptoms.RemoveAt(ComboIndex);
                this._UpdateComboboxes();
            }
            
            return;
        }
    }
}
