using UnityEngine;

public class HandGestures : MonoBehaviour
{
    [SerializeField]
    private OVRHand ovrHand;
    private bool isFingerPinching = false; 

    [SerializeField]
    private OVRHand.Hand HandType = OVRHand.Hand.None;

    private void OnEnable()
    {
        if (ovrHand == null)
        {
            Debug.Log("ovrHand was set faultly in the inspector");
        }
        else
        {
            Debug.Log("ovrHand was set correctly in the inspector");
        }
    }
    void Update()
    {
        // Index finger pinch creates an anchor
        if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && !isFingerPinching)
        {
            isFingerPinching = true; 
            Debug.Log("Creating Anchor");

            OVRPlugin.SpatialEntityAnchorCreateInfo createInfo = new OVRPlugin.SpatialEntityAnchorCreateInfo()
            {
                
                Time = OVRPlugin.GetTimeInSeconds(),
                BaseTracking = OVRPlugin.GetTrackingOriginType(),
                PoseInSpace = OVRExtensions.ToOVRPose(ovrHand.transform, false).ToPosef()
            };

            ulong anchorHandle = AnchorSession.kInvalidHandle;
            OVRPlugin.SpatialEntityCreateSpatialAnchor(createInfo, ref anchorHandle);
        }else{
            isFingerPinching = false; 
        }
    }
}
