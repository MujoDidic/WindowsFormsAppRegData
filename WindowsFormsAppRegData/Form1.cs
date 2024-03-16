using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppRegData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonLesen_Click(object sender, EventArgs e)
        {
            //den Schlüssel
            //HKEY_CURRENT_USER\Software\RegistryDemo öffen

            using (RegistryKey regSchluessel = Registry.CurrentUser.OpenSubKey("Software\\RegistryDemo"))
            {
                //wenn der Schlüssel nicht vorhanden ist,eine Meldung ausgeben
                if (regSchluessel == null)
                {
                    MessageBox.Show("Der Schlüssel konnte nicht geöffner werden!");
                    label1.Text = "Nicht vorhanden!";
                }
                else
                    //sonst den Eintrag lesen und label1 zuweisen
                    label1.Text = Convert.ToString(regSchluessel.GetValue("Eintrag"));

            }
        }

        private void ButtonSchreiben_Click(object sender, EventArgs e)
        {
            //den Schlüssel HKEY_CURRENT_USER\Software\RegistryDemo
            //anlegen bzw. öffen

            using (RegistryKey regSchluessel = Registry.CurrentUser.CreateSubKey("Software\\RegistryDemo"))
            {
                //ddern Wertaus dem Eingabrfekd in den Eintrag schreiben
                regSchluessel.SetValue("Eintrag", textBox1.Text);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //beim Schließen speichern wir die Position und die Große in der Registrierung
            //den Schlüßel HKEY_CURRENT_USER\Software\RegistryDemo 
            //anlegen bzw. öffen

            using (RegistryKey regSchluessel = Registry.CurrentUser.CreateSubKey("Software\\RegistryDemo"))
            {
                regSchluessel.SetValue("Top", this.Top);
                regSchluessel.SetValue("Left", this.Left);
                regSchluessel.SetValue("Height", this.Height);
                regSchluessel.SetValue("Width", this.Width);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //beim Anzeigen lesen wir die Daten wieder zurück
            //den Schlüssel HKEY_CURRENT_USER\Software\RegistryDemo öffen

            using (RegistryKey regSchluessel = Registry.CurrentUser.OpenSubKey("Software\\RegistryDemo"))
            {
                //wenn der Schlüßel nicht vorhanden ist,Meldung ausgeben
                if (regSchluessel == null)
                {
                    MessageBox.Show("Der Schlüssel konnte nicht geöffnet werden!");
                }
                else
                {
                    //sonst dei Einträge lesen und zuwiesen
                    this.Top = Convert.ToInt32(regSchluessel.GetValue("Top"));
                    this.Left = Convert.ToInt32(regSchluessel.GetValue("Left"));
                    this.Height = Convert.ToInt32(regSchluessel.GetValue("Height"));
                    this.Width = Convert.ToInt32(regSchluessel.GetValue("Width"));
                }
            }
        }
    }
}
