// =================================
//
//	VRDeviceDetector.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

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
			VRSettings.enabled = false;
			VRSettings.LoadDeviceByName("None");
		}
		Destroy(gameObject);
	}
	#endregion // Member Methods
}