using System.Collections.Generic;

namespace ReadyPlayerMe
{
    public static class AvatarConfigMap
    {
        public static readonly Dictionary<Pose, string> Pose = new Dictionary<Pose, string>
        {
            { ReadyPlayerMe.Pose.APose, "A" },
            { ReadyPlayerMe.Pose.TPose, "T" }
        };

        public static readonly Dictionary<TextureAtlas, string> TextureAtlas = new Dictionary<TextureAtlas, string>
        {
            { ReadyPlayerMe.TextureAtlas.None, "none" },
            { ReadyPlayerMe.TextureAtlas.High, "1024" },
            { ReadyPlayerMe.TextureAtlas.Medium, "512" },
            { ReadyPlayerMe.TextureAtlas.Low, "256" }
        };

        public static readonly Dictionary<MorphTarget, string> MorphTargets = new Dictionary<MorphTarget, string>
        {
            { MorphTarget.None, "none" },
            { MorphTarget.Oculus, "Oculus Visemes" },
            { MorphTarget.ARKit, "ARKit" },
            { MorphTarget.Viseme_aa, "viseme_aa" },
            { MorphTarget.Viseme_E, "viseme_E" },
            { MorphTarget.Viseme_I, "viseme_I" },
            { MorphTarget.Viseme_O, "viseme_O" },
            { MorphTarget.Viseme_U, "viseme_U" },
            { MorphTarget.Viseme_CH, "viseme_CH" },
            { MorphTarget.Viseme_DD, "viseme_DD" },
            { MorphTarget.Viseme_FF, "viseme_FF" },
            { MorphTarget.Viseme_kk, "viseme_kk" },
            { MorphTarget.Viseme_nn, "viseme_nn" },
            { MorphTarget.Viseme_PP, "viseme_PP" },
            { MorphTarget.Viseme_RR, "viseme_RR" },
            { MorphTarget.Viseme_sil, "viseme_sil" },
            { MorphTarget.Viseme_SS, "viseme_SS" },
            { MorphTarget.Viseme_TH, "viseme_TH" },
            { MorphTarget.BrowDownLeft, "browDownLeft" },
            { MorphTarget.BrowDownRight, "browDownRight" },
            { MorphTarget.BrowInnerUp, "browInnerUp" },
            { MorphTarget.BrowOuterUpLeft, "browOuterUpLeft" },
            { MorphTarget.BrowOuterUpRight, "browOuterUpRight" },
            { MorphTarget.EyesClosed, "eyesClosed" },
            { MorphTarget.EyeBlinkLeft, "eyeBlinkLeft" },
            { MorphTarget.EyeBlinkRight, "eyeBlinkRight" },
            { MorphTarget.EyeSquintLeft, "eyeSquintLeft" },
            { MorphTarget.EyeSquintRight, "eyeSquintRight" },
            { MorphTarget.EyeWideLeft, "eyeWideLeft" },
            { MorphTarget.EyeWideRight, "eyeWideRight" },
            { MorphTarget.EyesLookUp, "eyesLookUp" },
            { MorphTarget.EyesLookDown, "eyesLookDown" },
            { MorphTarget.EyeLookDownLeft, "eyeLookDownLeft" },
            { MorphTarget.EyeLookDownRight, "eyeLookDownRight" },
            { MorphTarget.EyeLookUpLeft, "eyeLookUpLeft" },
            { MorphTarget.EyeLookUpRight, "eyeLookUpRight" },
            { MorphTarget.EyeLookInLeft, "eyeLookInLeft" },
            { MorphTarget.EyeLookInRight, "eyeLookInRight" },
            { MorphTarget.EyeLookOutLeft, "eyeLookOutLeft" },
            { MorphTarget.EyeLookOutRight, "eyeLookOutRight" },
            { MorphTarget.JawOpen, "jawOpen" },
            { MorphTarget.JawForward, "jawForward" },
            { MorphTarget.JawLeft, "jawLeft" },
            { MorphTarget.JawRight, "jawRight" },
            { MorphTarget.NoseSneerLeft, "noseSneerLeft" },
            { MorphTarget.NoseSneerRight, "noseSneerRight" },
            { MorphTarget.CheekPuff, "cheekPuff" },
            { MorphTarget.CheekSquintLeft, "cheekSquintLeft" },
            { MorphTarget.CheekSquintRight, "cheekSquintRight" },
            { MorphTarget.MouthSmileLeft, "mouthSmileLeft" },
            { MorphTarget.MouthSmileRight, "mouthSmileRight" },
            { MorphTarget.MouthOpen, "mouthOpen" },
            { MorphTarget.MouthSmile, "mouthSmile" },
            { MorphTarget.MouthLeft, "mouthLeft" },
            { MorphTarget.MouthRight, "mouthRight" },
            { MorphTarget.MouthClose, "mouthClose" },
            { MorphTarget.MouthFunnel, "mouthFunnel" },
            { MorphTarget.MouthDimpleLeft, "mouthDimpleLeft" },
            { MorphTarget.MouthDimpleRight, "mouthDimpleRight" },
            { MorphTarget.MouthStretchLeft, "mouthStretchLeft" },
            { MorphTarget.MouthStretchRight, "mouthStretchRight" },
            { MorphTarget.MouthRollLower, "mouthRollLower" },
            { MorphTarget.MouthRollUpper, "mouthRollUpper" },
            { MorphTarget.MouthPressLeft, "mouthPressLeft" },
            { MorphTarget.MouthPressRight, "mouthPressRight" },
            { MorphTarget.MouthUpperUpLeft, "mouthUpperUpLeft" },
            { MorphTarget.MouthUpperUpRight, "mouthUpperUpRight" },
            { MorphTarget.MouthFrownLeft, "mouthFrownLeft" },
            { MorphTarget.MouthFrownRight, "mouthFrownRight" },
            { MorphTarget.MouthPucker, "mouthPucker" },
            { MorphTarget.MouthShrugLower, "mouthShrugLower" },
            { MorphTarget.MouthShrugUpper, "mouthShrugUpper" },
            { MorphTarget.MouthLowerDownLeft, "mouthLowerDownLeft" },
            { MorphTarget.MouthLowerDownRight, "mouthLowerDownRight" },
            { MorphTarget.TongueOut, "tongueOut" }
        };
    }
}
