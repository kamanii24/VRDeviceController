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
        if (!XRSettings.enabled)
        {
            transform.localPosition = InputTracking.GetLocalPosition(XRNode.CenterEye);
            transform.localRotation = InputTracking.GetLocalRotation(XRNode.CenterEye);
        }
    }
    #endregion // MonoBehaviour Methods
}