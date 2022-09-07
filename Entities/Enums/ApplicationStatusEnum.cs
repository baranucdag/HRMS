using System.ComponentModel;

namespace Entities.Enums
{
    public enum ApplicationStatusEnum
    {
        [Description("CandidateRejectedOffer")]
        CandidateRejectedOffer = -2,
        [Description("Rejected")]
        Rejected = -1,

        [Description("Evaluation")]
        Evaluation = 0,

        [Description("OnlineMeeting")]
        OnlineMeeting = 1,

        [Description("FaceToFaceMeeting")]
        FaceToFaceMeeting = 2,

        [Description("FormCompleted")]
        FormCompleted = 3,

        [Description("Offered")]
        Offered = 4,

        [Description("AcceptOffer")]
        AcceptOffer = 5,

        [Description("StartToWork")]
        StartToWork = 6,

    }
}
