using HealthCare.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Model
{
    public class Survey : RepositoryItem
    {


        public override object Key
        {
            get => surveyID;
            set => surveyID = (int)value;
        }

        public int surveyID { get; set; }
        public string Description { get; set; }
        public int SelectedRating { get; set; }
        public String AdditionalComment { get; set; }

        public override void Deserialize(string[] values)
        {
            surveyID = int.Parse(values[0]);
            Description = values[1];
            SelectedRating = int.Parse(values[2]);
            AdditionalComment = values[3];
        }

        public override string[] Serialize()
        {
            return new string[] { surveyID.ToString(), Description, SelectedRating.ToString(), AdditionalComment };
        }
    }
}
