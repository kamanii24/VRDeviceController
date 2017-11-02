// =================================
//
//	VRDeviceHead.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR;
#else
using UnityEngine.VR;
#endif

public class VRDeviceHead : MonoBehaviour
{
    #region MonoBehaviour Methods
    private void Start()
    {

    }

    private void Update()
    {
#if UNITY_2017_2_OR_NEWER
        if (!XRSettings.enabled)
        {
            transform.localPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);
            transform.localRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        }
#else
		if (!VRSettings.enabled)
		{
			transform.localPosition = InputTracking.GetLocalPosition(VRNode.CenterEye);
			transform.localRotation = InputTracking.GetLocalRotation(VRNode.CenterEye);
		}
#endif
    }
    #endregion // MonoBehaviour Methods
}