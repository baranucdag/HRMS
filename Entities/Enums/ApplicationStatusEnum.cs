using System.ComponentModel;

namespace Entities.Enums
{
    public enum ApplicationStatusEnum
    {
        [Description("Rejected")]
        Rejected = -1,

        [Description("evaluation")]
        Evaluation = 0,

        [Description("OnlineMeeting")]
        OnlineMeeting = 1,

        [Description("FaceToFaceMeeting")]
        FaceToFaceMeeting = 2,

        [Description("Offered")]
        Offered = 3,

        [Description("AcceptOffer")]
        AcceptOffer = 4,

        [Description("StartToWork")]
        StartToWork = 5,

    }
}
