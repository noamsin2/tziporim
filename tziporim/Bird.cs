using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tziporim
{
    public class Bird
    {
        public string SN { get; }
        public string species { get; set; }
        public string subSpecies { get; set; }
        public string birthDate { get; set; }
        public string gender { get; set; }
        public string cageSN { get; set; }
        public string daddySN { get; set; }
        public string mommySN { get; set; }

        public Bird(string SN, string species, string subSpecies, string birthDate, string gender, string cageSN, string daddySN, string mommySN)
        {
            this.SN = SN;
            this.species = species;
            this.subSpecies = subSpecies;
            this.birthDate = birthDate;
            this.gender = gender;
            this.cageSN = cageSN;
            this.daddySN = daddySN;
            this.mommySN = mommySN;
        }
    }
}
