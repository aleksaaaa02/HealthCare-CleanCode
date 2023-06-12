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

        public string DoctorJMBG { get; set; }
        public string TopicName { get; set; }
        public string Description { get; set; }
        private int? selectedRating;

        public int? SelectedRating
        {
            get { return selectedRating; }
            set { selectedRating = value; }
        }

        public String AdditionalComment { get; set; }

        public override void Deserialize(string[] values)
        {
            surveyID = int.Parse(values[0]);
            DoctorJMBG = values[1];
            TopicName = values[2];
            Description = values[3];
            SelectedRating = int.Parse(values[4]);
            AdditionalComment = values[5];
        }

        public override string[] Serialize()
        {
            return new string[] { surveyID.ToString(), DoctorJMBG.ToString(), TopicName, Description, SelectedRating.ToString(), AdditionalComment };
        }
    }
}
