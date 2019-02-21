using System;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    public class Requirement
    {
        public enum RequirementTypes
        {
            ResultSet = 1,
            Effect = 2
        }

        public int RequirementId { get; set; }
        public int CandidateId { get; set; }
        public RequirementTypes Type { get; set; }

        public string ResultQuery { get; set; }
        public bool RequireSort { get; set; }

        public string EffectTable { get; set; }
        public string CheckEffectQuery { get; set; }
        public string TriggerTriggerQuery { get; set; }
    }
}
