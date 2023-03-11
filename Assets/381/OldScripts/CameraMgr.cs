using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class for controlling the camera
public class CameraMgr : MonoBehaviour
{
    public static CameraMgr inst;
    private void Awake()
    {
        inst = this;
    }

    public GameObject rtsCameraRig;

    public bool isRTSMode = true;
    public Vector3 rtsCameraDefaultYaw = Vector3.zero;

    public GameObject yawNode;
    public GameObject pitchNode;
    public GameObject rollNode;

    public float cameraMoveSpeed = 25;
    public float cameraTurnRate = 75;
    public Vector3 currentYawEulerAngles = Vector3.zero;
    public Vector3 currentPitchEulerAngles = Vector3.zero;
    public Vector3 currentRollEulerAngles = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rtsCameraDefaultYaw = yawNode.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        currentYawEulerAngles = yawNode.transform.localEulerAngles;

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentYawEulerAngles.y -= cameraTurnRate * Time.deltaTime;
            }
            else
            {
                yawNode.transform.Translate(Vector3.left * Time.deltaTime * cameraMoveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentYawEulerAngles.y += cameraTurnRate * Time.deltaTime;
            }
            else
            {
                yawNode.transform.Translate(Vector3.right * Time.deltaTime * cameraMoveSpeed);
            }
        }


        yawNode.transform.localEulerAngles = currentYawEulerAngles;
        currentPitchEulerAngles = pitchNode.transform.localEulerAngles;

        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentPitchEulerAngles.x -= cameraTurnRate * Time.deltaTime;
            }
            else
            {
                yawNode.transform.Translate(Vector3.forward * Time.deltaTime * cameraMoveSpeed);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentPitchEulerAngles.x += cameraTurnRate * Time.deltaTime;
            }
            else
            {
                yawNode.transform.Translate(Vector3.back * Time.deltaTime * cameraMoveSpeed);
            }
        }

        pitchNode.transform.localEulerAngles = currentPitchEulerAngles;

        if (Input.GetKey(KeyCode.R))
        {
            yawNode.transform.Translate(Vector3.up * Time.deltaTime * cameraMoveSpeed);

        }
        if (Input.GetKey(KeyCode.F))
        {
            yawNode.transform.Translate(Vector3.down * Time.deltaTime * cameraMoveSpeed);

        }


        //This was to attatch the camera to the moving entity, it can be added back in, just took it out for now

        //if (Input.GetKeyUp(KeyCode.C))
        //{
        //    if (isRTSMode)
        //    {
        //        yawNode.transform.SetParent(SelectionMgr.inst.selectedEntity.cameraRig.transform);
        //        yawNode.transform.localPosition = Vector3.zero;
        //        yawNode.transform.localEulerAngles = Vector3.zero;
        //        pitchNode.transform.localPosition = Vector3.zero;
        //        pitchNode.transform.localEulerAngles = Vector3.zero;
        //    }
        //    else
        //    {
        //        yawNode.transform.SetParent(rtsCameraRig.transform);
        //        yawNode.transform.localPosition = Vector3.zero;
        //        yawNode.transform.localEulerAngles = rtsCameraDefaultYaw;
        //        pitchNode.transform.localPosition = Vector3.zero;
        //        pitchNode.transform.localEulerAngles = Vector3.zero;
        //    }
        //    isRTSMode = !isRTSMode;
        //}
    }
}
