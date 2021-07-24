using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace DiseasePredictor
{
    //[DataContract]
    class Disease
    {
        //[DataMember]
        [JsonProperty("Name")]
        public string Name { get; set; }
        //[DataMember]
        [JsonProperty("Symptoms")]
        public List<string> Symptoms { get; set; }

        
    }
}
