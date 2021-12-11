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
using DocumentFormat.OpenXml;

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
            double alles = 0;                                                                                     //verschiedene Deklarationen
            double durschnitt = 0;
            double umsatz = 0;
            double maxumsatz = 0;

            string monat;
            string bestermonat = "";
            string schlechtmonat = "";

            int zeilen = 1;


            FileStream fs = new FileStream(@"Ums2020Berlin.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string textdokument0 = sr.ReadLine();                                                                 //Zeile wird ausgelesen
            string[] linie0 = textdokument0.Split('#');                                                           //Zeile wird zerhakt überall wo ein # ist und macht es in einen Array[]

            double minimalumsatz = Double.Parse(linie0[0], CultureInfo.CurrentCulture);                           //der erste wert wird als minimalumsatz eingestellt, damit dieser keine fehler macht wenn ich ihn wie die anderen mit 0 mache, da kein umsatz unter 0 sein wird


            while (sr.EndOfStream == false)                                                                       //Wiederholung in er schleife bis das Ende des txt erreicht ist
            {

                string textdokument = sr.ReadLine();                                                              //alles wie oben, auslesen etc,
                string[] linie = textdokument.Split('#');

                umsatz = Double.Parse(linie[0], CultureInfo.CurrentCulture);
                monat = Convert.ToString(linie[1]);

                if (maxumsatz < umsatz)                                                                           //if schleife, wenn der monate wert größer ist als der maxumsatz wird die if klammer ausgeführt
                {
                    maxumsatz = umsatz;                                                                           //minimal umsatz wird ersetzt mit dem kleineren wert
                    bestermonat = monat;                                                                          //der Monat wird dann auch mit übernommen
                }

                if (umsatz < minimalumsatz)                                                                       //if schleife, wenn der monate wert kleiner ist als der minimalwert wird die if klammer ausgeführt
                {
                    minimalumsatz = umsatz;                                                                       //minimal umsatz wird ersetzt mit dem kleineren wert
                    schlechtmonat = monat;                                                                        //der Monat wird dann auch mit übernommen
                }

                zeilen = zeilen + 1;                                                                              //Bei jeder neuem durchlauf, durchläuft er eine neue Zeile, damit das Programm weiß wieviele Zeilen es gibt
                alles = alles + umsatz;                                                                           //jeder neue Umsatz wird addiert auf den davor
            }


            if (zeilen > 12)                                                                                      //In der Datei handelt es sich um einen Jahresumsatz, ein Jahr hat 12 Monate,                                                                
            {                                                                                                     //wenn in dem Dokument mehr als 12 Zeilen/Monate sind wird alles abgebrochen 

                lblausgabe.Content = "Fehlerhafte Textdatei, mehr als 12 Monate vorhanden";
                goto Exit;                                                                                        //Der Befehl springt zu Exit und beendet alles
            }

            if (zeilen < 12)                                                                                      //wie oben
            {
                lblausgabe.Content = "Fehlerhafte Textdatei, weniger als 12 Monate vorhanden";
                goto Exit;
            }


            durschnitt = umsatz / zeilen;                                                                         //durschnitt berechnen
            durschnitt = Math.Round(durschnitt, 2);                                                               //Runden auf die zweite Nachkommastelle
            maxumsatz = Math.Round(maxumsatz, 2);
            minimalumsatz = Math.Round(minimalumsatz, 2);

            lblausgabe.Content = "Der Durschnittsumsatz für das Jahr 2020 beträgt: " + durschnitt + "€" + Environment.NewLine +                             //der Ganze Text der Ausgegeben wird...       
                "Der beste Monat war der " + bestermonat + " mit einem Umsatz von " + maxumsatz + "€" +
                Environment.NewLine + "Der schlechteste Monat war der " + schlechtmonat + " mit einem Umsatz von " + minimalumsatz + "€";

        Exit: { }


            fs.Close();                                                                                          //immer schön den FileStream und Reader schließen, ansonsten gibts stress
            sr.Close();


        }
    }
}
