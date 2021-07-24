using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Newtonsoft.Json;

namespace DiseasePredictor
{
    class Loader
    {
        private List<Disease> _Diseases;
        public List<Disease> Diseases{
            get{ return this._Diseases; }
        }
        private string _JsonPath;
        
        public Loader(string filepath= "E:/Documents/Dev/ISPM-projects/TD-IA/C-SHARP-IA-AUTODOC/DiseasePredictor/bin/Debug/net5.0-windows/Data/data-csharp.json")
        {
            this._JsonPath = filepath;
            this.ReadJson();
        }
        private void ReadJson()
        {
            StreamReader fp = new StreamReader(this._JsonPath);
            string line = fp.ReadLine();
            string rawfile = "";
            try
            {
                rawfile += line;
                while (line != null)
                {
                    line = fp.ReadLine();
                    rawfile += line;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                fp.Close();
            }
            this._Diseases = JsonConvert.DeserializeObject<List<Disease>>(rawfile);

        }
    }
}
