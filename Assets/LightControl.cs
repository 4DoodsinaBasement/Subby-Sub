using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : SubsystemTemplate
{
       public float deltaSteering = 25f; // (degrees / second)
       public GameObject cam;
       public float smooth = 2.0F;
       public float tiltAngle = 30.0F;


        public float speed = 5.0f;


         public float YminAngle = -30f;
         public float YmaxAngle = 30f;
         public float XminAngle = -30f;
         public float XmaxAngle = 30f;
     
 float ClampAngle(float angle, float from, float to) {
   if(angle > 180) angle = angle -360;// - angle;
   angle = Mathf.Clamp(angle, from, to);
   if(angle < 0) angle = 360 + angle;
         
   return angle;
 }


    void UpdateRotateX(float value){
        Vector3 rot = cam.transform.localEulerAngles;
        rot.y += value;
        rot.y =  ClampAngle(rot.y, XminAngle, XmaxAngle);
        cam.transform.localEulerAngles = rot;
    }
    void UpdateRotateY(float value){
        Vector3 rot = cam.transform.localEulerAngles;
        rot.z -= value;
        rot.z = ClampAngle(rot.z, YminAngle, YmaxAngle);
        cam.transform.localEulerAngles = rot;
    }

    public override void LeftStickX(float value) {UpdateRotateX(value);}
    public override void LeftStickY(float value) {UpdateRotateY(value);}
    public override void RightStickX(float value) {}
    public override void RightStickY(float value) {}

    public override void LeftStickClick() {}
    public override void LeftStickClick_Up() {}
    public override void LeftStickClick_Down() {}
    public override void RightStickClick() {}
    public override void RightStickClick_Up() {}
    public override void RightStickClick_Down() {}

    public override void ButtonNorth() {}
    public override void ButtonNorth_Up() {}
    public override void ButtonNorth_Down() {}
    public override void ButtonSouth() {}
    public override void ButtonSouth_Up() {}
    public override void ButtonSouth_Down() {}
    public override void ButtonEast() {}
    public override void ButtonEast_Up() {}
    public override void ButtonEast_Down() {}
    public override void ButtonWest() {}
    public override void ButtonWest_Up() {}
    public override void ButtonWest_Down() {}

    public override void LeftTrigger() {}
    public override void LeftTrigger_Up() {}
    public override void LeftTrigger_Down() {}
    public override void RightTrigger() {}
    public override void RightTrigger_Up() {}
    public override void RightTrigger_Down() {}
    public override void LeftBumper() {}
    public override void LeftBumper_Up() {}
    public override void LeftBumper_Down() {}
    public override void RightBumper() {}
    public override void RightBumper_Up() {}
    public override void RightBumper_Down() {}

    public override void PadNorth() {}
    public override void PadNorth_Up() {}
    public override void PadNorth_Down() {}
    public override void PadSouth() {}
    public override void PadSouth_Up() {}
    public override void PadSouth_Down() {}
    public override void PadEast() {}
    public override void PadEast_Up() {}
    public override void PadEast_Down() {}
    public override void PadWest() {}
    public override void PadWest_Up() {}
    public override void PadWest_Down() {}

    public override void ButtonStart() {}
    public override void ButtonStart_Up() {}
    public override void ButtonStart_Down() {}
    public override void ButtonSelect() {}
    public override void ButtonSelect_Up() {}
    public override void ButtonSelect_Down() {}
}

