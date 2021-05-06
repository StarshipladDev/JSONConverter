using JSONChangeNotesReaderApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JSONChangeNotesReaderApp
{
    public partial class Form1 : Form
    {
        string fileLocation = "changes.json";
        public Form1()
        {
            InitializeComponent();

        }
        /// <summary>
        /// AddColorBig is for formatting an input String as in-line stylizied HTML content. This method emboldens the text 900%;
        /// </summary>
        /// <param name="inputString">The raw string to be represented as a styled in-line element</param>
        /// <param name="color">The color of the text</param>
        /// <param name="size">The size of the text in percentage values</param>
        /// <returns></returns>
        static string AddColorBig(string inputString, string color, int size = 180)

        {

            return "<a style=' color:" + color + ";font-size:" + size + "%;font-weight: 900;'>" + inputString + "</a>";

        }

        /// <summary>

        /// CheckColors is used to highlight words that are between the ['] character,for stylistic purposes.

        /// </summary>

        /// <param name="inputString">The string being checked and stylized </param>

        /// <returns></returns>

        public static string CheckColors(string inputString)

        {



            Debug.WriteLine("Colouring input is " + inputString);

            if (inputString.Length > 1)

            {

                int colorBit = inputString.IndexOf('\'');

                if (colorBit == -1)

                {

                    return inputString;

                }

                Debug.WriteLine("ColourBit is " + colorBit + " of length:" + inputString.Length);

                String bitToColor = inputString.Substring(colorBit, inputString.Length - colorBit);

                Debug.WriteLine("Bit to color is" + bitToColor);

                int nextBitToColor = bitToColor.Substring(1).IndexOf('\'');

                Debug.WriteLine("Next Color Bit is" + nextBitToColor);

                bitToColor = bitToColor.Substring(0, nextBitToColor + 2);

                Debug.WriteLine("Start of new bit to color is  is " + bitToColor);

                Debug.WriteLine("Coloring : " + AddColorBig(bitToColor, "black", 100));

                Debug.WriteLine("Reurning : " + inputString.Substring(0, colorBit) + AddColorBig(bitToColor, "black", 110) + CheckColors(inputString.Substring(colorBit + bitToColor.Length)));

                return inputString.Substring(0, colorBit) + AddColorBig(bitToColor, "black", 110) + CheckColors(inputString.Substring(colorBit + bitToColor.Length));



            }

            else

            {

                return "";

            }



        }

        /// <summary>

        /// GetJSONInternet gets a String representation of DoomScrools's JSON changenotes

        /// </summary>

        /// <returns>A string representation of the JSON changenotes found at www.starshiplad.com</returns>

        public static string GetJSONInternet()

        {

            string updateURL = Uri.EscapeUriString("https://www.starshiplad.com/Testing/API/GetNotes.json");

            string doc = "";

            using (System.Net.WebClient client = new System.Net.WebClient()) // WebClient class inherits IDisposable

            {

                doc = client.DownloadString(updateURL);

            }

            return doc;

        }

        /// <summary>

        /// APICallAsync is a test function to JSON reading and parsing

        /// </summary>

        /// <param name="b">The text bix control to be updated if event notification required</param>

        /// <param name="fileLocation">The URI of a JSON file to read. If left blank, will use GetJSONInternet()</param>

        /// <returns></returns>

        public static void APICallAsync(TextBox b, string fileLocation = "")

        {



            string doc = "";

            if (fileLocation.Equals(""))

            {

                doc = GetJSONInternet();

            }

            else

            {



                Debug.WriteLine("Reading :" + fileLocation);

                using (StreamReader r = new StreamReader(fileLocation))

                {

                    doc = r.ReadToEnd();

                }

            }



            Debug.WriteLine("Deserializing  :" + doc);

            UpdateNoteList unl = JsonConvert.DeserializeObject<UpdateNoteList>(doc);

            //unl.updates = JsonConvert.DeserializeObject<List<UpdateNoteNode>>(doc);

            //List<UpdateNotes> updateNotes = JsonConvert.DeserializeObject<List<UpdateNotes>>(doc);

            String htmlFile = "<!DOCTYPE html><html><head><Title>Oscar Test</Title></head><body>";

            Debug.WriteLine(String.Concat(Enumerable.Repeat("-", 20)));

            htmlFile += String.Concat(Enumerable.Repeat("-", 20));

            htmlFile += "<br>" + AddColorBig("Title : ", "green") + unl.title;

            htmlFile += "<br>" + AddColorBig("Summary: ", "green") + unl.summary;

            htmlFile += "<br>";

            for (int z = 0; z < unl.updates.Count; z++)

            {

                htmlFile += "<br><br>" + AddColorBig("Date : ", "blue") + AddColorBig(unl.updates[z].date, "black") + "->";

                for (int f = 0; f < unl.updates[z].updateNotes.Count; f++)
                {
                    htmlFile += "<br> &emsp; " + AddColorBig("Area: ", "red", 150) + AddColorBig(unl.updates[z].updateNotes[f].area, "black", 150) + "<br>";
                    for (int i = 0; i < unl.updates[z].updateNotes[f].changes.Count; i++)
                    {
                        htmlFile += "<br> &emsp; &emsp;" + CheckColors(unl.updates[z].updateNotes[f].changes[i]);
                        htmlFile += "<br>";

                    }



                    htmlFile += "<br>";

                }

            }


            htmlFile += "<br>" + AddColorBig("TODO: ", "red");
            for (int z = 0; z < unl.updates.Count; z++)
            {
                htmlFile += "<br> &emsp; " + unl.toDo[z];
            }

            htmlFile += "</body></html>";

            System.IO.File.WriteAllText("yoursite.html", htmlFile);

            System.Diagnostics.Process.Start("yoursite.html");



        }
        void button1_Click(object sender, EventArgs e)

        {
            this.label1.Text = "JSON File Location: "+fileLocation;
            APICallAsync(null, fileLocation);
        }

        void label1_Click(object sender, EventArgs e)
        {

        }

        void button2_ClickAsync(object sender, EventArgs e)
        {
            
            using (FileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";
                fd.FilterIndex = 2;
                fd.RestoreDirectory = true;

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file

                    fileLocation = fd.FileName;
                    this.label1.Text = "JSON File Location: " + fileLocation;
                    this.label1.Text = fileLocation;
                }
            }
        }

    }
}
