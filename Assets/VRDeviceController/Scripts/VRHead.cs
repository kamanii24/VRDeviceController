// =================================
//
//	VRHead.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;


public class VRHead : MonoBehaviour
{
	#region MonoBehaviour Methods
	private void Start ()
	{
		
	}
	
	private void Update ()
	{
		if (!VRSettings.enabled)
		{
			transform.localPosition = InputTracking.GetLocalPosition(VRNode.CenterEye);
			transform.localRotation = InputTracking.GetLocalRotation(VRNode.CenterEye);
		}
	}
	#endregion // MonoBehaviour Methods
}