using System.ComponentModel;
using UnityEngine;

namespace ReadyPlayerMe
{
    public enum BodyType
    {
        None,
        [Description("fullbody")] FullBody,
        [Description("halfbody")] HalfBody
    }

    public enum OutfitGender
    {
        None,
        [Description("masculine")] Masculine,
        [Description("feminine")] Feminine,
        [Description("neutral")] Neutral
    }

    public enum MeshType
    {
        HeadMesh,
        BeardMesh,
        TeethMesh
    }

    public enum FailureType
    {
        None,
        NoInternetConnection,
        UrlProcessError,
        ShortCodeError,
        DownloadError,
        MetadataDownloadError,
        MetadataParseError,
        ModelDownloadError,
        ModelImportError,
        DirectoryAccessError,
        AvatarProcessError,
        AvatarRenderError
    }

    public enum AvatarRenderScene
    {
        [Description("Upper body render")] Portrait,
        [Description("Upper body render with transparent background")] PortraitTransparent,
        [Description("Posed full body render with transparent background")] FullBodyPostureTransparent
    }

#region Avatar API

    public enum Pose
    {
        APose,
        TPose
    }

    public enum MeshLod
    {
        [InspectorName("High (LOD0)")]
        High,
        [InspectorName("Medium (LOD1)")]
        Medium,
        [InspectorName("Low (LOD2)")]
        Low
    }

    public enum TextureAtlas
    {
        None,
        [InspectorName("High (1024)")]
        High,
        [InspectorName("Medium (512)")]
        Medium,
        [InspectorName("Low (256)")]
        Low
    }

    public enum MorphTarget
    {
        None,
        ARKit,
        Oculus,
        Viseme_aa,
        Viseme_E,
        Viseme_I,
        Viseme_O,
        Viseme_U,
        Viseme_CH,
        Viseme_DD,
        Viseme_FF,
        Viseme_kk,
        Viseme_nn,
        Viseme_PP,
        Viseme_RR,
        Viseme_sil,
        Viseme_SS,
        Viseme_TH,
        BrowDownLeft,
        BrowDownRight,
        BrowInnerUp,
        BrowOuterUpLeft,
        BrowOuterUpRight,
        EyesClosed,
        EyeBlinkLeft,
        EyeBlinkRight,
        EyeSquintLeft,
        EyeSquintRight,
        EyeWideLeft,
        EyeWideRight,
        EyesLookUp,
        EyesLookDown,
        EyeLookDownLeft,
        EyeLookDownRight,
        EyeLookUpLeft,
        EyeLookUpRight,
        EyeLookInLeft,
        EyeLookInRight,
        EyeLookOutLeft,
        EyeLookOutRight,
        JawOpen,
        JawForward,
        JawLeft,
        JawRight,
        NoseSneerLeft,
        NoseSneerRight,
        CheekPuff,
        CheekSquintLeft,
        CheekSquintRight,
        MouthSmileLeft,
        MouthSmileRight,
        MouthOpen,
        MouthSmile,
        MouthLeft,
        MouthRight,
        MouthClose,
        MouthFunnel,
        MouthDimpleLeft,
        MouthDimpleRight,
        MouthStretchLeft,
        MouthStretchRight,
        MouthRollLower,
        MouthRollUpper,
        MouthPressLeft,
        MouthPressRight,
        MouthUpperUpLeft,
        MouthUpperUpRight,
        MouthFrownLeft,
        MouthFrownRight,
        MouthPucker,
        MouthShrugLower,
        MouthShrugUpper,
        MouthLowerDownLeft,
        MouthLowerDownRight,
        TongueOut
    }

#endregion
}
