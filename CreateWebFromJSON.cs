using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;




namespace TestApplication.Functions

{

    class CreateWebFromJSON

    {

        /// <summary>

        /// AddColorBig is for formatting an input String as in-line stylizied HTML content. This method emboldens the text 900%;

        /// </summary>

        /// <param name="inputString">The raw string to be represented as a styled in-line element</param>

        /// <param name="color">The color of the text</param>

        /// <param name="size">The size of the text in percentage values</param>

        /// <returns></returns>

        public static string AddColorBig(string inputString, string color, int size = 180, int boldness = 900)

        {

            return "<a style=' color:" + color + ";font-size:" + size + "%;font-weight: " + boldness + ";'>" + inputString + "</a>";

        }

        /// <summary>

        /// AddBackgroundColor is for formatting an input String to have a specified background Color;

        /// </summary>

        /// <param name="inputString">The raw string to be represented as a styled in-line element</param>

        /// <param name="color">The color of the background</param>

        /// <returns>A Html Element containing the input string with a specified background color</returns>

        public static string AddBackgroundColor(string inputString, string color)

        {

            return "<div style=' margin:0px; background-color:" + color + "; width:100%;'>" + inputString + "</div>";

        }

        /// <summary>

        /// CheckColors is used to highlight words that are between the ['] character,for stylistic purposes.

        /// </summary>

        /// <param name="inputString">The string being checked and stylized </param>

        /// <returns></returns>

        public static string CheckColors(string inputString, int minimumSize = 100)

        {



            Debug.WriteLine("Colouring input is " + inputString);

            if (inputString.Length > 1)

            {

                int colorBit = inputString.IndexOf('\'');

                if (colorBit == -1)

                {

                    return AddColorBig(inputString, "black", minimumSize, minimumSize);

                }

                Debug.WriteLine("ColourBit is " + colorBit + " of length:" + inputString.Length);

                String bitToColor = inputString.Substring(colorBit, inputString.Length - colorBit);

                Debug.WriteLine("Bit to color is" + bitToColor);

                int nextBitToColor = bitToColor.Substring(1).IndexOf('\'');

                Debug.WriteLine("Next Color Bit is" + nextBitToColor);

                bitToColor = bitToColor.Substring(0, nextBitToColor + 2);

                Debug.WriteLine("Start of new bit to color is  is " + bitToColor);

                Debug.WriteLine("Coloring : " + AddColorBig(bitToColor, "black", minimumSize));

                Debug.WriteLine("Reurning : " + inputString.Substring(0, colorBit) + AddColorBig(bitToColor, "black", minimumSize + (minimumSize / 10)) + CheckColors(inputString.Substring(colorBit + bitToColor.Length)));

                return inputString.Substring(0, colorBit) + AddColorBig(bitToColor, "black", minimumSize + (minimumSize / 10)) + CheckColors(inputString.Substring(colorBit + bitToColor.Length), minimumSize);



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

        /*

         * Example input:

         {

            "Title":"Starshiplad's JSON Notes",

            "Summary":"Content is changes in Change 1 and 2"

            "Area":{

                "Method":[

                    "Change1",

                    "Change2"

                ]

            }

 

        }

         *

         *

         *

         */

        public static async System.Threading.Tasks.Task APICallAsync(TextBox b, string fileLocation = "Changes.json")

        {

            string doc = "";

            if (fileLocation.Equals("") || fileLocation == null)

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

            JToken entireJson = JToken.Parse(doc); ;

            List<DisplayModule> ReturnValues = FindTokens(entireJson, null);

            String htmlFile = "<!DOCTYPE html><html style='background-color:darkgray;'><head><Title>Oscar Test</Title></head><body>";

            Debug.WriteLine(String.Concat(Enumerable.Repeat("-", 20)));

            htmlFile += String.Concat(Enumerable.Repeat("-", 20));

            htmlFile += "<br>";

            htmlFile += "<br><br>" + AddColorBig("Date : ", "blue") + AddColorBig(DateTime.Now.ToString("dd/MM/yyyy"), "black") + "->";

            foreach (DisplayModule s in ReturnValues)

            {

                htmlFile += s.DrawLine();

            }

            htmlFile += "</body></html>";

            System.IO.File.WriteAllText("yoursite.html", htmlFile);

            System.Diagnostics.Process.Start("yoursite.html");



        }

        private static List<DisplayModule> FindTokens(JToken containerToken, string name)

        {

            List<DisplayModule> matches = new List<DisplayModule>();

            FindTokens(containerToken, name, matches);

            Debug.WriteLine(matches);

            return matches;

        }



        private static void FindTokens(JToken containerToken, string name, List<DisplayModule> matches, int depth = 0)

        {

            if (containerToken.Type == JTokenType.Object)

            {



                foreach (JProperty child in containerToken.Children<JProperty>())

                {



                    if (child.Name.ToLower().Equals("title"))

                    {

                        matches.Add(new DisplayModule(child.Name + ":" + child.First, DisplayTypes.Title, 0));

                    }

                    if (child.Name.ToLower().Equals("summary"))

                    {

                        matches.Add(new DisplayModule(child.Name + ":" + child.First, DisplayTypes.Summary, 0));

                    }

                    else

                    {

                        matches.Add(new DisplayModule(child.Name, DisplayTypes.Header, depth));

                        FindTokens(child.Value, name, matches, depth + 1);

                    }

                }

            }

            else if (containerToken.Type == JTokenType.Array || containerToken.Type == JTokenType.Property)

            {

                foreach (JToken child in containerToken.Children())

                {

                    matches.Add(new DisplayModule(child.ToString(), DisplayTypes.Text, depth));

                }

            }

        }

        private class DisplayModule

        {

            int minimumSize = 150;

            string text;

            DisplayTypes type;

            int depth;

            int size = 100;

            public DisplayModule(string text, DisplayTypes type, int depth)

            {

                this.depth = depth;

                this.type = type;

                this.text = text;

                this.size = ((4 - depth) * 20) + minimumSize;

            }

            public String DrawLine()

            {

                string htmlFile = "";

                if (this.type == DisplayTypes.Header)

                {

                    htmlFile += AddBackgroundColor(String.Concat(Enumerable.Repeat("&emsp;", depth * 2)) + AddColorBig("Area: ", "red", this.size) + AddColorBig(this.text, "black", this.size), depth % 2 == 0 ? "gray" : "#777777");

                }

                else if (this.type == DisplayTypes.Title)

                {

                    htmlFile += AddBackgroundColor(AddColorBig(text, "green", this.size + 50), "#888888");

                }



                else if (this.type == DisplayTypes.Summary)

                {

                    htmlFile += AddBackgroundColor(AddColorBig(text, "white", this.size + 25), "#666666");

                }

                else

                {



                    htmlFile += AddBackgroundColor(String.Concat(Enumerable.Repeat("&emsp;", depth * 2)) + AddColorBig("Change: ", "green", this.size) + AddColorBig(CheckColors(this.text, minimumSize), "black", minimumSize, minimumSize), "lightgray");

                }



                return htmlFile;

            }

        }

        enum DisplayTypes

        {

            Header, Text, Title, Summary

        }



    }

}