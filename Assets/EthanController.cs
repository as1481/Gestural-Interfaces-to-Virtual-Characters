using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EthanController : MonoBehaviour
{
    public static int ORIGIN = 0;
    public static int NORTHWEST = 1;
    public static int NORTH = 2;
    public static int NORTHEAST = 3;
    public static int EAST = 4;
    public static int SOUTHEAST = 5;
    public static int SOUTH = 6;
    public static int SOUTHWEST = 7;
    public static int WEST = 8;

    private string headPath = "char_ethan_skeleton/char_ethan_Hips/char_ethan_Spine/char_ethan_Spine1/char_ethan_Spine2/char_ethan_Neck/char_ethan_Head";
    private string rightEye = "char_ethan_skeleton/char_ethan_Hips/char_ethan_Spine/char_ethan_Spine1/char_ethan_Spine2/char_ethan_Neck/char_ethan_Head/char_ethan_RightEye";
    private string leftEye = "char_ethan_skeleton/char_ethan_Hips/char_ethan_Spine/char_ethan_Spine1/char_ethan_Spine2/char_ethan_Neck/char_ethan_Head/char_ethan_LeftEye";
    private string rightShoulder = "char_ethan_skeleton/char_ethan_Hips/char_ethan_Spine/char_ethan_Spine1/char_ethan_Spine2/char_ethan_Neck/char_ethan_RightShoulder";
    private string leftShoulder = "char_ethan_skeleton/char_ethan_Hips/char_ethan_Spine/char_ethan_Spine1/char_ethan_Spine2/char_ethan_Neck/char_ethan_LeftShoulder";
    private AnimationClip currentClip;

    private ComponentCurve rightEyeCurve;
    private ComponentCurve leftEyeCurve;
    private ComponentCurve headCurve;
    private ComponentCurve rightShoulderCurve;
    private ComponentCurve leftShoulderCurve;

    // The curve of a body part
    private class ComponentCurve{

        private AnimationCurve xCurve;
        private AnimationCurve yCurve;
        private AnimationCurve zCurve;
        private AnimationCurve wCurve;

        public ComponentCurve()
        {
            xCurve = new AnimationCurve();
            yCurve = new AnimationCurve();
            zCurve = new AnimationCurve();
            wCurve = new AnimationCurve();
        }

        public void AddXKey(float time, float angle){
            xCurve.AddKey(time, angle);
        }
        public void AddYKey(float time, float angle)
        {
            yCurve.AddKey(time, angle);
        }
        public void AddZKey(float time, float angle){
            zCurve.AddKey(time, angle);
        }
        public void AddWKey(float time, float angle){
            wCurve.AddKey(time, angle);
        }

        public AnimationCurve GetXCurve(){ 
            return xCurve;
        }
        public AnimationCurve GetYCurve()
        {
            return yCurve;
        }
        public AnimationCurve GetZCurve()
        {
            return zCurve;
        }
        public AnimationCurve GetWCurve()
        {
            return wCurve;
        }
    }

    void Start()
    {
        Animation animation = GetComponent<Animation>();

        // Initiate Animation Curves
        rightEyeCurve = new ComponentCurve();
        leftEyeCurve = new ComponentCurve();
        headCurve = new ComponentCurve();
        rightShoulderCurve = new ComponentCurve();
        leftShoulderCurve = new ComponentCurve();
        
        // Initiate Animation Clip
        currentClip = new AnimationClip();
        currentClip.name = "animation1";
        currentClip.legacy = true;

        // Add Components To Curve
        NodHead(0, 1, 1);
        ShakeHead(2, 1, 1);
        RotateEye(0, 1, ORIGIN, NORTH);
        RotateEye(1, 1, NORTH, EAST);
        RotateEye(2, 1, EAST, SOUTH);
        Shrug(3, 2);

        // Add Curves to Animation Clip
        AddCurves();
        animation.AddClip(currentClip, currentClip.name);
    }

    // <summary> Constructs a nodding animation clip </summary>
    // StartTime: The start time of the curves
    // Duration: The duration of the curves
    // Intensity: The intensity of the head as a decimal
    private void NodHead(float startTime, float duration, float intensity)
    {
        var nodAngle = Quaternion.Euler(0, 0, 25 * intensity);
        var restAngle = Quaternion.Euler(0, 0, 0);

        headCurve.AddXKey(startTime, restAngle.x);
        headCurve.AddYKey(startTime, restAngle.y);
        headCurve.AddZKey(startTime, restAngle.z);
        headCurve.AddWKey(startTime, restAngle.w);

        headCurve.AddXKey(startTime + (duration * 0.5f), nodAngle.x);
        headCurve.AddYKey(startTime + (duration * 0.5f), nodAngle.y);
        headCurve.AddZKey(startTime + (duration * 0.5f), nodAngle.z);
        headCurve.AddWKey(startTime + (duration * 0.5f), nodAngle.w);

        headCurve.AddXKey(startTime + duration, restAngle.x);
        headCurve.AddYKey(startTime + duration, restAngle.y);
        headCurve.AddZKey(startTime + duration, restAngle.z);
        headCurve.AddWKey(startTime + duration, restAngle.w);
    }

    // <summary> Constructs a head shaking animation clip </summary>
    // StartTime: The start time of the curves
    // Duration: The duration of the curves
    // Intensity: The intensity of the shake as a decimal
    private void ShakeHead(float startTime, float duration, float intensity)
    {
        var leftAngle = Quaternion.Euler(25 * intensity, 0, 0);
        var rightAngle = Quaternion.Euler(-25 * intensity, 0, 0);
        var restAngle = Quaternion.Euler(0, 0, 0);

        headCurve.AddXKey(startTime, restAngle.x);
        headCurve.AddYKey(startTime, restAngle.y);
        headCurve.AddZKey(startTime, restAngle.z);
        headCurve.AddWKey(startTime, restAngle.w);

        headCurve.AddXKey(startTime + (duration * 0.25f), leftAngle.x);
        headCurve.AddYKey(startTime + (duration * 0.25f), leftAngle.y);
        headCurve.AddZKey(startTime + (duration * 0.25f), leftAngle.z);
        headCurve.AddWKey(startTime + (duration * 0.25f), leftAngle.w);

        headCurve.AddXKey(startTime + (duration * 0.5f), restAngle.x);
        headCurve.AddYKey(startTime + (duration * 0.5f), restAngle.y);
        headCurve.AddZKey(startTime + (duration * 0.5f), restAngle.z);
        headCurve.AddWKey(startTime + (duration * 0.5f), restAngle.w);

        headCurve.AddXKey(startTime + (duration * 0.75f), rightAngle.x);
        headCurve.AddYKey(startTime + (duration * 0.75f), rightAngle.y);
        headCurve.AddZKey(startTime + (duration * 0.75f), rightAngle.z);
        headCurve.AddWKey(startTime + (duration * 0.75f), rightAngle.w);

        headCurve.AddXKey(startTime + duration, restAngle.x);
        headCurve.AddYKey(startTime + duration, restAngle.y);
        headCurve.AddZKey(startTime + duration, restAngle.z);
        headCurve.AddWKey(startTime + duration, restAngle.w);
    }

    // <summary> Rotates the eyes depending on the orientation </summary>
    // StartTime: The start time of the curves
    // Duration: The duration of the curves
    private void RotateEye(float startTime, float duration, int startDirection, int endDirection)
    {
        Quaternion startAngle = Quaternion.Euler(0, 270, 270);
        Quaternion endAngle = Quaternion.Euler(0, 270, 270);

        switch (startDirection)
        {
            case 0: startAngle = Quaternion.Euler(0, 270, 270); break;
            case 1: startAngle = Quaternion.Euler(-20, 270, 290); break;
            case 2: startAngle = Quaternion.Euler(0, 270, 290); break;
            case 3: startAngle = Quaternion.Euler(20, 270, 290); break;
            case 4: startAngle = Quaternion.Euler(20, 270, 270); break;
            case 5: startAngle = Quaternion.Euler(20, 270, 260); break;
            case 6: startAngle = Quaternion.Euler(0, 270, 260); break;
            case 7: startAngle = Quaternion.Euler(-20, 270, 260); break;
            case 8: startAngle = Quaternion.Euler(-20, 270, 270); break;
        }
        switch (endDirection)
        {
            case 0: endAngle = Quaternion.Euler(0, 270, 270); break;
            case 1: endAngle = Quaternion.Euler(-20, 270, 290); break;
            case 2: endAngle = Quaternion.Euler(0, 270, 290); break;
            case 3: endAngle = Quaternion.Euler(20, 270, 290); break;
            case 4: endAngle = Quaternion.Euler(20, 270, 270); break;
            case 5: endAngle = Quaternion.Euler(20, 270, 260); break;
            case 6: endAngle = Quaternion.Euler(0, 270, 260); break;
            case 7: endAngle = Quaternion.Euler(-20, 270, 260); break;
            case 8: endAngle = Quaternion.Euler(-20, 270, 270); break;
        }

        rightEyeCurve.AddXKey(startTime, startAngle.x);
        rightEyeCurve.AddYKey(startTime, startAngle.y);
        rightEyeCurve.AddZKey(startTime, startAngle.z);
        rightEyeCurve.AddWKey(startTime, startAngle.w);

        rightEyeCurve.AddXKey(startTime + duration, endAngle.x);
        rightEyeCurve.AddYKey(startTime + duration, endAngle.y);
        rightEyeCurve.AddZKey(startTime + duration, endAngle.z);
        rightEyeCurve.AddWKey(startTime + duration, endAngle.w);

        leftEyeCurve.AddXKey(startTime, startAngle.x);
        leftEyeCurve.AddYKey(startTime, startAngle.y);
        leftEyeCurve.AddZKey(startTime, startAngle.z);
        rightEyeCurve.AddWKey(startTime, startAngle.w);

        leftEyeCurve.AddXKey(startTime + duration, endAngle.x);
        leftEyeCurve.AddYKey(startTime + duration, endAngle.y);
        leftEyeCurve.AddZKey(startTime + duration, endAngle.z);
        leftEyeCurve.AddWKey(startTime + duration, endAngle.w);
    }

    private void TiltHead(float startTime, float duration, int startDirection, int endDirection)
    {
        Quaternion startAngle = Quaternion.Euler(0, 0, 0);
        Quaternion endAngle = Quaternion.Euler(0, 0, 0);

        switch (startDirection)
        {
            case 0: startAngle = Quaternion.Euler(0, 0, 0); break;
            case 1: startAngle = Quaternion.Euler(-30, 0, -15); break;
            case 2: startAngle = Quaternion.Euler(0, 0, -15); break;
            case 3: startAngle = Quaternion.Euler(30, 0, -15); break;
            case 4: startAngle = Quaternion.Euler(30, 0, 0); break;
            case 5: startAngle = Quaternion.Euler(30, 0, 15); break;
            case 6: startAngle = Quaternion.Euler(0, 0, 15); break;
            case 7: startAngle = Quaternion.Euler(-30, 0, 15); break;
            case 8: startAngle = Quaternion.Euler(-30, 0, 0); break;
        }
        switch (endDirection)
        {
            case 0: endAngle = Quaternion.Euler(0, 0, 0); break;
            case 1: endAngle = Quaternion.Euler(-30, 0, -15); break;
            case 2: endAngle = Quaternion.Euler(0, 0, -15); break;
            case 3: endAngle = Quaternion.Euler(30, 0, -15); break;
            case 4: endAngle = Quaternion.Euler(30, 0, 0); break;
            case 5: endAngle = Quaternion.Euler(30, 0, 15); break;
            case 6: endAngle = Quaternion.Euler(0, 0, 15); break;
            case 7: endAngle = Quaternion.Euler(-30, 0, 15); break;
            case 8: endAngle = Quaternion.Euler(-30, 0, 0); break;
        }

        headCurve.AddXKey(startTime, startAngle.x);
        headCurve.AddYKey(startTime, startAngle.y);
        headCurve.AddZKey(startTime, startAngle.z);
        headCurve.AddWKey(startTime, startAngle.w);

        headCurve.AddXKey(startTime + duration, endAngle.x);
        headCurve.AddYKey(startTime + duration, endAngle.y);
        headCurve.AddZKey(startTime + duration, endAngle.z);
        headCurve.AddWKey(startTime + duration, endAngle.w);
    }

    private void FaceAnger(float startTime, float duration)
    {
        // Need to check for current emotions at this keyframe#

        // char_ethan_Jaw
        // x = -0.01
       
        // char_ethan_Lower_Lip
        // z = -0.025

        // char_ethan_LeftBrow
        // x = -0.1

        // char_ethan_RightBrow
        // x - -0.1

        // char_ethan_LeftCorner
        // x = 0.2

        // char_ethan_RightCorner
        // x = 0.2
    }

    private void FaceFear(float startTime, float duration)
    {
        // char_ethan_Jaw
        // x = -0.025

        // Char_ethan_LeftBrow
        // x = -0.14
        // y = -0.03

        // Char_ethan_RightBrow
        // x = -0.14
        // y = -0.03

        // Char_ethan_RightBlink
        // x = -0.114

        // Char_ethan_LeftBlink
        // x = -0.114

        // Char_ethan_RightCorner
        // x = 0.2

        // Char_ethan_LeftCorner
        // x = = 0.2
                
    }

    private void FaceDisgust(float startTime, float duration)
    {
        // char_ethan_UpperLip
        // x = 0.173

        // char_ethan_LeftLowerLip
        // x = 0.055

        // char_ethan_RightLowerLip
        // x = 0.055

        // char_ethan_LowerLip
        // x = 0.075

        // char_ethan_Jaw
        // x = -0.035

        // char_ethan_RightBrow
        // x = -0.1

        // char_ethan_LeftBrow
        // x = -0.1
    }

    private void FaceSad(float startTime, float duration)
    {
        // char_ethan_RightCorner
        // x = 0.2

        // char_ethan_LeftCorner
        // x = 0.195

        // char_ethan_RightBrow
        // x = -0.1
        
        // char_ethan_LeftBrow
        // x = -0.1
    }

    private void FaceSurprise(float startTime, float duration)
    {
        // char_ethan_RightBrow
        // x = -0.15

        // char_ethan_LeftBrow
        // x = -0.15

        // char_Ethan_Jaw
        // x = -0.03

        // char_ethan_LeftLowerLip
        // y = -0.005

        // char_ethan_RightLowerLip
        // y = 0.02

        // char_ethan_RightCorner
        // z = 0.02
        
        // char_ethan_LeftCorner
        // z = -0.02
    }

    private void FaceJoy(float startTime, float duration)
    {
        // char_ethan_Jaw
        // x = -0.03

        // char_ethan_LeftCorner
        // x = 0.18

        // char_ethan_RightCorner
        // x = 0.17

        // char_ethan_UpperLip
        // x = 0.175
        
        // char_ethan_LeftLowerLip
        // z = -0.015

        // char_ethan_RightLowerLip
        // z = -0.015
    }

    private void Shrug(float startTime, float duration)
    {
        Vector3 startRightPos = new Vector3(0,0,0.03f);
        Vector3 startLeftPos = new Vector3(0,0,-0.03f);

        // Start
        rightShoulderCurve.AddXKey(startTime, startRightPos.x);
        rightShoulderCurve.AddYKey(startTime, startRightPos.y);
        rightShoulderCurve.AddZKey(startTime, startRightPos.z);

        leftShoulderCurve.AddXKey(startTime, startLeftPos.x);
        leftShoulderCurve.AddYKey(startTime, startLeftPos.y);
        leftShoulderCurve.AddZKey(startTime, startLeftPos.z);

        // Shrug
        rightShoulderCurve.AddXKey(startTime + duration / 2, -0.05f);
        rightShoulderCurve.AddYKey(startTime + duration / 2, startRightPos.y);
        rightShoulderCurve.AddZKey(startTime + duration / 2, startRightPos.z);

        leftShoulderCurve.AddXKey(startTime + duration / 2, -0.05f);
        leftShoulderCurve.AddYKey(startTime + duration / 2, startLeftPos.y);
        leftShoulderCurve.AddZKey(startTime + duration / 2, startLeftPos.z);

        // End
        rightShoulderCurve.AddXKey(startTime + duration, startRightPos.x);
        rightShoulderCurve.AddYKey(startTime + duration, startRightPos.y);
        rightShoulderCurve.AddZKey(startTime + duration, startRightPos.z);

        leftShoulderCurve.AddXKey(startTime + duration, startLeftPos.x);
        leftShoulderCurve.AddYKey(startTime + duration, startLeftPos.y);
        leftShoulderCurve.AddZKey(startTime + duration, startLeftPos.z);
   }


    public void AddCurves()
    {
        // Right Eye
        currentClip.SetCurve(rightEye, typeof(Transform), "localRotation.x", rightEyeCurve.GetXCurve());
        currentClip.SetCurve(rightEye, typeof(Transform), "localRotation.y", rightEyeCurve.GetYCurve());
        currentClip.SetCurve(rightEye, typeof(Transform), "localRotation.z", rightEyeCurve.GetZCurve());
        currentClip.SetCurve(rightEye, typeof(Transform), "localRotation.w", rightEyeCurve.GetWCurve());

        // Left Eye
        currentClip.SetCurve(leftEye, typeof(Transform), "localRotation.x", leftEyeCurve.GetXCurve());
        currentClip.SetCurve(leftEye, typeof(Transform), "localRotation.y", leftEyeCurve.GetYCurve());
        currentClip.SetCurve(leftEye, typeof(Transform), "localRotation.z", leftEyeCurve.GetZCurve());
        currentClip.SetCurve(leftEye, typeof(Transform), "localRotation.w", leftEyeCurve.GetWCurve());
        
        // Head
        currentClip.SetCurve(headPath, typeof(Transform), "localRotation.x", headCurve.GetXCurve());
        currentClip.SetCurve(headPath, typeof(Transform), "localRotation.y", headCurve.GetYCurve());
        currentClip.SetCurve(headPath, typeof(Transform), "localRotation.z", headCurve.GetZCurve());
        currentClip.SetCurve(headPath, typeof(Transform), "localRotation.w", headCurve.GetWCurve());
    
    
        // Right Shoulder
        currentClip.SetCurve(rightShoulder, typeof(Transform), "localPosition.x", rightShoulderCurve.GetXCurve());
        currentClip.SetCurve(rightShoulder, typeof(Transform), "localPosition.y", rightShoulderCurve.GetYCurve());
        currentClip.SetCurve(rightShoulder, typeof(Transform), "localPosition.z", rightShoulderCurve.GetZCurve());

        // Left Shoulder
        currentClip.SetCurve(leftShoulder, typeof(Transform), "localPosition.x", leftShoulderCurve.GetXCurve());
        currentClip.SetCurve(leftShoulder, typeof(Transform), "localPosition.y", leftShoulderCurve.GetYCurve());
        currentClip.SetCurve(leftShoulder, typeof(Transform), "localPosition.z", leftShoulderCurve.GetZCurve());
    }

}
