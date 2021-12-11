using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BFK_S_KA1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


   

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            double alles = 0;
            int zeilen = 0;
            double durschnitt = 0;
            double umsatz = 0;
            


            FileStream fs = new FileStream(@"Ums2020Berlin.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);



            while (sr.EndOfStream == false)
            {
                string textdokument = sr.ReadLine();
                string[] geld = textdokument.Split('#');
                umsatz = Double.Parse(geld[0], CultureInfo.CurrentCulture);

                alles = alles + umsatz;

                zeilen = zeilen + 1;

            }

            durschnitt = umsatz / zeilen;

            lblausgabe.Content = durschnitt;

            fs.Close();
            sr.Close();
        }
    }
}
