// =================================
//
//	VRDeviceDetector.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR;
#else
using UnityEngine.VR;
#endif

public class VRDeviceDetector : MonoBehaviour
{
	#region MonoBehaviour Methods
	private void Awake ()
	{
		SceneManager.activeSceneChanged += OnActiveSceneChange;
	}

	private void Start ()
	{

	}

	private void Update ()
	{
		
	}
	#endregion // MonoBehaviour Methods


	#region Member Methods
	// シーン遷移を受け取る
	private void OnActiveSceneChange (Scene prevScene, Scene nextScene)
	{
		SceneManager.activeSceneChanged -= OnActiveSceneChange;	// コールバック解除
		if (VRDeviceController.Instance == null)
		{
            // VRモードを無効にして通常画面にする
#if UNITY_2017_2_OR_NEWER
            XRSettings.enabled = false;
            XRSettings.LoadDeviceByName("None");
#else
            VRSettings.enabled = false;
			VRSettings.LoadDeviceByName("None");
#endif
        }
		Destroy(gameObject);
	}
	#endregion // Member Methods
}