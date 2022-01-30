// @author: Princy Rasolonjatovo
// @email: princy.m.rasolonjatovo @gmail.com
// @github : princy-rasolonjatovo


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Debug
using System.Diagnostics;

namespace DiseasePredictor
{
    class Predictor
    {
        private Dictionary<string, List<string>> Diseases;
        private Dictionary<string, int> DiseasesID;
        private Dictionary<string, int> _SymptomsID;
        private List<List<int>> matrix;
        public Predictor(){
            this.Diseases = new Dictionary<string, List<string>>();
            this.DiseasesID = new Dictionary<string, int>();
            this._SymptomsID = new Dictionary<string, int>();
            this.matrix = new List<List<int>>();

            this.LoadData();
            this.DiseaseToID();
            this.PopulateMatrix();
        }
        public Dictionary<string, int> Symptoms { 
            get
            {
                return this._SymptomsID;
            }
            private set
            {

            }
        }
        private void LoadData()
        {
            Loader l = new Loader();
            foreach (Disease d in l.Diseases)
            {
                this.Diseases.Add(d.Name, d.Symptoms);
            }
        }
        /// <summary>
        /// Convert Symptoms list to binary vectors
        /// </summary>
        private void DiseaseToID()
        {

            int D_ID = 0, S_ID = 0;  // Disease id and Symptom ID
            foreach(string DiseaseName in this.Diseases.Keys)
            {
                this.DiseasesID[DiseaseName] = D_ID++;
                foreach(string symptom in this.Diseases[DiseaseName])
                {
                    if (!this._SymptomsID.ContainsKey(symptom))
                    {
                        this._SymptomsID[symptom] = S_ID++;
                    }
                }
            }
        }

        private void PopulateMatrix()
        {
            /// Build matrix
            /// 
            int vecDim = this._SymptomsID.Count;
            for (int i=0; i < this.Diseases.Count; i++)
            {
                //this.matrix.Add(new List<int>(vecDim));
                List<int> _v = new List<int>();
                for( int j = 0; j < vecDim; j++)
                {
                    _v.Add(0);
                }
                this.matrix.Add(_v);
            }
            /// Populate Matrix
            foreach(KeyValuePair<string, int> p in this.DiseasesID){
                foreach(string symptom in this.Diseases[p.Key])
                {
                    int S_ID = this._SymptomsID[symptom];
                    this.matrix[p.Value][S_ID] = 1;
                }
            }
        }
        /// <summary>
        /// Compute the XOR on two vector with same dimension
        /// </summary>
        /// <param name="A">First Vector </param>
        /// <param name="B">Second Vector</param>
        private  List<int> XORVector(List<int> A, List<int> B)
        {
            // TODO use reference to vectors instead of passing by value
            List<int> AB = new List<int>();
            if (A.Count != B.Count)
            {
                throw new Exception($"[InvalidVectorsDimensions] Dimension of A({A.Count}) does not match dimension of B({B.Count})");
            }
            for(int i = 0; i < A.Count; i++)
            {
                AB.Add(A[i] ^ B[i]);
            }
            return AB;
        }

        private List<string> GetDiseasesNames(List<int> d)
        {
            List<string> dID = new List<string>();
            Dictionary<int, string> IDToDisease = new Dictionary<int, string>();
            foreach(KeyValuePair<string, int> p in this.DiseasesID)
            {
                IDToDisease[p.Value] = p.Key;
            }
            foreach( int i in d)
            {
                dID.Add(IDToDisease[i]);
            }
            return dID;
        }
        private List<string> GetSymptomsNames(List<int> d)
        {
            List<string> sID = new List<string>();
            Dictionary<int, string> IDToSymptoms = new Dictionary<int, string>();
            foreach (KeyValuePair<string, int> p in this._SymptomsID)
            {
                IDToSymptoms[p.Value] = p.Key;
            }
            foreach (int i in d)
            {
                sID.Add(IDToSymptoms[i]);
            }
            return sID;
        }
        /// <summary>
        /// Compute the Hamming distance between two vectors (Taxicab distance)
        /// </summary>
        /// <param name="A">First Vector</param>
        /// <param name="B">Second Vector</param>
        /// <returns></returns>
        private int Hamming(List<int> A, List<int> B)
        {
            
            List<int> AB = this.XORVector(A, B);
            // Counter Object
            int i = 0;
            foreach( int k in AB)
            {
                if (k == 1) { i++; }
            }
            return i;
        }
        public List<string> Predict(List<string> symptoms)
        {
            /// Clean input
            symptoms.ForEach(delegate(string s)
            {
                s = s.Trim();
            });
            List<int> vec = new List<int>();
            for(int i = 0; i < this._SymptomsID.Count; i++)
            {
                vec.Add(0);
            }

            foreach( string s in symptoms)
            {
                
                if (!this._SymptomsID.ContainsKey(s))
                {
                    throw new Exception($"[UnknownSymptomKeyword] symptom: '{s}'is unrecognized!");
                }
                vec[this._SymptomsID[s]] = 1;
            }

            /// Predict using Hamming distance
            /// when the difference is minimal H(x, y) --> 0
            /// the disease must be one of the minimal
            List<int> h_dists = new List<int>();
            foreach(List<int> v in this.matrix)
            {
                h_dists.Add(this.Hamming(v, vec));
            }
            int min = h_dists.Min();
            List<int> minimals = new List<int>();
            if (min == 0)
            {
                minimals.Add(h_dists.IndexOf(min));
                return this.GetDiseasesNames(minimals);

            }
            /// (minimal < len(self._matrix[0]) - dist_threshold):  # guess
            else if (min < this.matrix[0].Count)
            {
                for ( int i = 0; i < h_dists.Count; i++)
                {
                    if (h_dists[i] <= min)
                    {
                        minimals.Add(i);
                    }
                }
                return this.GetDiseasesNames(minimals);
            }
            else
            {
                return new List<string>();
            }            
        }
    }
}
