using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;



namespace EOD_Saturn_Zapytania
{


    public partial class Form1 : Form
    {


        string sourceDirectoryInbox = @"\\\\***";
        string sourceDirectorySema = @"\\\\***";
        string sourceDirectorySemaOut = @"\\\\***";
        string sourceDirectoryOutBox = @"\\\\***";
        string sourceDirectoryStatus = @"\\\\***";
        string sourceDirectoryArch = @"\\\\***";
        string[] tab = new string[10000];
        string[] zapytaniaSQLDoWyboru = new string[500];
        string[] tablica = new string[100];
        string[] zapytaniaSQL = { "CCD_AUTOMAT_WINDYK.RUN", "CCD_EOD_WPLATY_PRZEDTERM.RUN", "CHECCK_EOM_USGAAP.RUN", "COLLFEE_ALL.RUN", "DATY_WSZYSTKICH_USGAAP.RUN", "DEADCUSTOMERS2KO.RUN", "IND EOD.RUN", "IND INTERFACE ALL2.RUN", "LOAD_FILE_FROM_V_PLUS.RUN", "NMC WERYFIKATOR LIR.RUN", "ORZEL EOD ALL2", "ORZEL EOM 1 ALL2", "ORZEL EOM 2 ALL 3.RUN", "ORZEL EOM 2 ALL2", "ORZEL PEOD2.RUN", "SPRAWDZENIE PLIKÓW INDUS.RUN", "RNL_EOD.RUN", "VIS IN CARDS DETAILS PROCESSING.RUN", "VIS IN GE UPDATE PROCESSING.RUN", "VIS IN GL UPDATE PROCESSING.RUN", "VIS IN NEW SEGMENTS PROCESSING.RUN", "VIS_OUT_RSR FILE.RUN", "VIS OUT ADD SME.RUN", "VIS OUT ADU.RUN", "VIS OUT CUSTOMER UPDATE.RUN", "VIS OUT FUS UPDATE.RUN", "VIS OUT NEW ACCOUNTS.RUN", "VIS OUT PRZETWARZANIE POCZEKALNI.RUN", "VIS OUT WYPOWIEDZENIA.RUN", "WPL NMC KSIEGOWANIE SATURN - ROWNOLEGLE.RUN", "WPL NMC KSIEGOWANIE VISION.RUN", "ZMIANA LIR.RUN", "ZAMYKANIE KREDYTÓW.RUN" };

        List<String> listaPosortowana = new List<string>();

        List<Dane> lista = new List<Dane>();
        zapytania obiektZapytania = new zapytania();
        int n = 0;



        //Funkcja szukania opisu zapytania
        public string poszukaj(string szukana)
        {
            foreach (Dane dane in lista)
            {
                if (dane.Nazwa.Equals(szukana))
                {
                    return dane.Opis;
                    break;
                }

            }
            return null;
        }


        public string zwrocZapytanie(string[] tablica, string doSprawdzenia)
        {
            string zmienna = "";
            foreach (string a in zapytaniaSQL)
            {

                if (doSprawdzenia.ToUpper().Equals(a))
                {
                    zmienna = doSprawdzenia;
                    break;
                }
            }

            return zmienna;

        }

        public Form1()
        {
            InitializeComponent();

            Wyswietl1.Items.Add(sourceDirectoryInbox);
            Wyswietl1.Items.Add(sourceDirectorySema);
            Wyswietl1.Items.Add(sourceDirectorySemaOut);
            Wyswietl1.Items.Add(sourceDirectoryOutBox);
            // Wyswietl1.Items.Add(sourceDirectoryStatus);

            wyswietl2.Items.Add(sourceDirectoryInbox);
            wyswietl2.Items.Add(sourceDirectorySema);
            wyswietl2.Items.Add(sourceDirectorySemaOut);
            wyswietl2.Items.Add(sourceDirectoryOutBox);
            //wyswietl2.Items.Add(sourceDirectoryStatus);

            FileSystemWatcher watcher = new FileSystemWatcher(@"\\bpula1975gecb\Data\SemaOut");
            watcher.Created += new FileSystemEventHandler(watcher_Created);
            watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
            watcher.EnableRaisingEvents = true;

            //Tworzenie i dodawanie obiektów klasy Dane

            /*
            for (int i = 0; i < zapytaniaSQL.Length; i++)
            {
                
                Nazwa.Items.Add(zapytaniaSQL[i]);
                lista.Add(new Dane(zapytaniaSQL[i], tablica[i]));
               
            }
 
            
            */


            Wyswietl1.Text = (sourceDirectorySema);
            wyswietl2.Text = (sourceDirectorySemaOut);


        }

        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            DateTime aktualnaGodzina = DateTime.Now;
            //  Logi.Items.Add("KONIEC |  " + e.Name.ToString() + "  | " + aktualnaGodzina);
            //  dataGridView1.Rows[n].Cells[0].Value = "Koniec";
            //  dataGridView1.Rows[n].Cells[1].Value = e.Name.ToString();
            //  dataGridView1.Rows[n].Cells[2].Value = aktualnaGodzina;

            //  dataGridView1.Rows.Add(1);
            //n++;

            Control.CheckForIllegalCrossThreadCalls = false;
            dataGridView1.GridColor = Color.Red;

            object[] row1 = new object[] { "KONIEC", e.Name.ToString(), aktualnaGodzina };


        }

        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            DateTime aktualnaGodzina = DateTime.Now;

            dataGridView1.GridColor = Color.Green;

            object[] row1 = new object[] { "START", e.Name.ToString(), aktualnaGodzina };

        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string wynikZapytania = poszukaj(Nazwa.SelectedItem.ToString());
            Zapytania.Text = wynikZapytania;
            Clipboard.SetDataObject(wynikZapytania);



        }


        private void Wyswietl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ktoryKatalog = Wyswietl1.SelectedItem.ToString();

            try
            {
                var txtFiles = Directory.EnumerateFiles(ktoryKatalog);
                int i = 0;
                Array.Clear(tab, 0, tab.Length);
                textBox1.Clear();
                foreach (String file in txtFiles)
                {
                    string fileName = Path.GetFileName(file);
                    // Nazwa.Items.Add(fileName);
                    textBox1.Text += fileName;
                    textBox1.Text += "\r\n";
                }

                /*
                foreach (string currentFile in txtFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectorySema.Length +1);
                    tab[i] += fileName + "\r\n";
                    textBox1.Text += tab[i];
                    i++;
                }*/
            }
            finally
            {
                ktoryKatalog = "1";
            }


        }

        private void wyswietl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ktoryKatalog = wyswietl2.SelectedItem.ToString();

            try
            {
                var txtFiles2 = Directory.EnumerateFiles(ktoryKatalog);
                int i = 0;
                Array.Clear(tab, 0, tab.Length);
                textBox2.Clear();
                Nazwa.Items.Clear();
                foreach (String file in txtFiles2)
                {
                    string fileName = Path.GetFileName(file);
                    string wynik = zwrocZapytanie(zapytaniaSQL, fileName);
                    if (wynik != "")
                    {
                        Nazwa.Items.Add(wynik);
                        lista.Add(new Dane(wynik, obiektZapytania.tablica[1]));
                    }
                    textBox2.Text += fileName;
                    textBox2.Text += "\r\n";
                }




            }
            finally
            {
                ktoryKatalog = "1";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {



            if (!checkBox1.Checked)
            {
                string ktoryKatalog = wyswietl2.SelectedItem.ToString();
                try
                {

                    var txtFiles2 = Directory.EnumerateFiles(ktoryKatalog);
                    int i = 0;
                    Array.Clear(tab, 0, tab.Length);
                    textBox2.Clear();
                    Nazwa.Items.Clear();
                    lista.Clear();
                    foreach (String file in txtFiles2)
                    {
                        string fileName = Path.GetFileName(file);
                        string wynik = zwrocZapytanie(zapytaniaSQL, fileName);
                        int ktoryIndeks = 0;


                        if (wynik != "")
                        {
                            Nazwa.Items.Add(wynik);
                            foreach (string indeks in zapytaniaSQL)
                            {
                                if (wynik.ToUpper().Equals(indeks))
                                {
                                    ktoryIndeks = Array.IndexOf(zapytaniaSQL, wynik.ToUpper());
                                    break;
                                }

                            }
                            lista.Add(new Dane(wynik, obiektZapytania.tablica[ktoryIndeks]));
                        }
                        if (fileName.Equals("IND EOD.RUN"))
                        {
                            label1.Text = "INDUS SIE ROZPOCZĄŁ!";
                            label1.ForeColor = Color.White;
                            label1.BackColor = Color.Green;
                        }
                        textBox2.Text += fileName;
                        textBox2.Text += "\r\n";
                    }

                }
                finally
                {
                    ktoryKatalog = "1";
                }

            }
            else
            {
                string ktoryKatalog = wyswietl2.SelectedItem.ToString();
                try
                {
                    var txtFiles2 = Directory.EnumerateFiles(ktoryKatalog);
                    textBox2.Clear();
                    foreach (String file in txtFiles2)
                    {
                        string fileName = Path.GetFileName(file);
                        textBox2.Text += fileName;
                        textBox2.Text += "\r\n";
                    }
                }
                catch
                {
                    ktoryKatalog = "1";
                }

            }
            string ktoryKatalog2 = Wyswietl1.SelectedItem.ToString();

            try
            {
                var txtFiles = Directory.EnumerateFiles(ktoryKatalog2);
                int i = 0;
                Array.Clear(tab, 0, tab.Length);
                textBox1.Clear();
                foreach (String file in txtFiles)
                {
                    string fileName = Path.GetFileName(file);
                    // Nazwa.Items.Add(fileName);
                    textBox1.Text += fileName;
                    textBox1.Text += "\r\n";
                }


            }
            finally
            {
                ktoryKatalog2 = "1";
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < zapytaniaSQL.Length; i++)
            {

                Nazwa.Items.Add(zapytaniaSQL[i]);
                lista.Add(new Dane(zapytaniaSQL[i], obiektZapytania.tablica[i]));

            }
        }


        public void sprawdzPlikiIndus()
        {
            string ktoryKatalog = wyswietl2.SelectedItem.ToString();

            string biezacaData;
            int plikowIndus = 0;
            DateTime thisDay = DateTime.Now.AddDays(-1);
            biezacaData = thisDay.ToShortDateString();
            string formatDaty;
            formatDaty = thisDay.ToString("yMMdd");
            //formatDaty = "150621";
            try
            {
                string[] dirs = Directory.GetFiles(sourceDirectoryArch, "*" + formatDaty + "*" + ".MD5", SearchOption.AllDirectories);
                plikowIndus = dirs.Length;
            }
            catch (Exception)
            {

                throw;
            }

            label2.Text = "Plików Indus: " + plikowIndus.ToString() + " z 7 ";
            if (plikowIndus > 0 && plikowIndus < 3)
                label2.ForeColor = Color.Yellow;
            else if (plikowIndus > 3)
                label2.ForeColor = Color.Green;
            else
                label2.ForeColor = Color.Red;

        }



        public void sprawdzCF()
        {
            DateTime thisDay = DateTime.Today;
            string biezacaData = thisDay.ToShortDateString();
            string formatDaty = thisDay.ToString("ddMMy");


            DateTime wczoraj = DateTime.Now.AddDays(-1);
            biezacaData = thisDay.ToShortDateString();
            string wczorajszaData = wczoraj.ToString("ddMMy");
            formatDaty = thisDay.ToString("yMMdd");
            //formatDaty = "150621";
            CF.Text = "BRAK CF!";
            CF.ForeColor = Color.Red;

            try
            {
                string[] dirs = Directory.GetFiles("\\\\***", "CF" + wczorajszaData + ".txt", SearchOption.AllDirectories);
                foreach (var a in dirs)
                {
                    FileInfo infoPliku = new FileInfo(a);
                    if (infoPliku.CreationTime.ToShortDateString().Equals(biezacaData))
                    {
                        CF.Text = infoPliku.Name + "  " + infoPliku.CreationTime;
                        CF.ForeColor = Color.Green;
                    }
                    else
                    {
                        CF.Text = "BRAK CF!";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public void sprawdzGE()
        {
            DateTime thisDay = DateTime.Today;
            string biezacaData = thisDay.ToShortDateString();
            string formatDaty = thisDay.ToString("ddMMy");


            DateTime wczoraj = DateTime.Now.AddDays(-1);
            biezacaData = thisDay.ToShortDateString();
            string wczorajszaData = wczoraj.ToString("ddMMy");
            formatDaty = thisDay.ToString("yMMd");
            //formatDaty = "150621";
            GE.Text = "BRAK GE!";
            try
            {
                string[] dirs = Directory.GetFiles("\\\\*** ", "GE" + wczorajszaData + ".txt", SearchOption.AllDirectories);
                foreach (var a in dirs)
                {
                    FileInfo infoPliku = new FileInfo(a);
                    if (infoPliku.CreationTime.ToShortDateString().Equals(biezacaData))
                    {
                        GE.Text = infoPliku.Name + "  " + infoPliku.CreationTime;
                        GE.ForeColor = Color.Green;
                    }
                    else
                    {
                        GE.Text = "BRAK GE!";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public void sprawdzGL()
        {
            DateTime thisDay = DateTime.Today;
            string biezacaData = thisDay.ToShortDateString();
            string formatDaty = thisDay.ToString("ddMMy");


            DateTime wczoraj = DateTime.Now.AddDays(-1);
            biezacaData = thisDay.ToShortDateString();
            string wczorajszaData = wczoraj.ToString("ddMMy");
            formatDaty = thisDay.ToString("yMMd");
            //formatDaty = "150621";
            GL.Text = "BRAK GL!";
            try
            {
                string[] dirs = Directory.GetFiles("\\\\***", "GL" + wczorajszaData + ".txt", SearchOption.AllDirectories);
                foreach (var a in dirs)
                {

                    FileInfo infoPliku = new FileInfo(a);
                    if (infoPliku.CreationTime.ToShortDateString().Equals(biezacaData))
                    {
                        GL.Text = infoPliku.Name + "  " + infoPliku.CreationTime;
                        GL.ForeColor = Color.Green;
                    }
                    else
                    {
                        GL.Text = "BRAK GL!";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public void sprawdzCD()
        {
            DateTime thisDay = DateTime.Today;
            string biezacaData = thisDay.ToShortDateString();
            string formatDaty = thisDay.ToString("ddMMy");


            DateTime wczoraj = DateTime.Now.AddDays(-1);
            biezacaData = thisDay.ToShortDateString();
            string wczorajszaData = wczoraj.ToString("ddMMy");
            formatDaty = thisDay.ToString("yMMd");
            //formatDaty = "150621";
            CD.Text = "BRAK CD!";
            try
            {
                string[] dirs = Directory.GetFiles("\\\\***", "CD" + wczorajszaData + ".txt", SearchOption.AllDirectories);
                foreach (var a in dirs)
                {
                    FileInfo infoPliku = new FileInfo(a);
                    if (infoPliku.CreationTime.ToShortDateString().Equals(biezacaData))
                    {
                        CD.Text = infoPliku.Name + "  " + infoPliku.CreationTime;
                        CD.ForeColor = Color.Green;
                    }
                    else
                    {
                        CD.Text = "BRAK CD!";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }



        }

        public void sprawdzNS()
        {
            DateTime thisDay = DateTime.Today;
            string biezacaData = thisDay.ToShortDateString();
            string formatDaty = thisDay.ToString("ddMMy");


            DateTime wczoraj = DateTime.Now.AddDays(-1);
            biezacaData = thisDay.ToShortDateString();
            string wczorajszaData = wczoraj.ToString("ddMMy");
            formatDaty = thisDay.ToString("yMMd");
            //formatDaty = "150621";
            NS.Text = "BRAK NS!";
            try
            {
                string[] dirs = Directory.GetFiles("\\\\***", "NS" + wczorajszaData + ".txt", SearchOption.AllDirectories);
                foreach (var a in dirs)
                {
                    FileInfo infoPliku = new FileInfo(a);
                    if (infoPliku.CreationTime.ToShortDateString().Equals(biezacaData))
                    {
                        NS.Text = infoPliku.Name + "  " + infoPliku.CreationTime;
                        NS.ForeColor = Color.Green;
                    }
                    else
                    {
                        NS.Text = "BRAK NS!";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
