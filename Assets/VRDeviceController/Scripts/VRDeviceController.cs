// =================================
//
//	VRDeviceController.cs
//	Created by Takuya Himeji
//
// =================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRDeviceController : MonoBehaviour
{
	#region Singleton
	private static VRDeviceController instance;

	public static VRDeviceController Instance
	{
		get
		{
			if (instance == null) 
			{
				instance = (VRDeviceController) FindObjectOfType(typeof(VRDeviceController));

				if (instance == null)
				{
					Debug.Log("VRDeviceController is not found.");
				}
			}
			return instance;
		}
	}
	#endregion // Singleton

	#region Inspector Settings
	#endregion // Inspector Settings


	#region Member Field
	// 定数宣言
	private static readonly string 	VR_DEVICE_NAME = "cardboard";	// Cardboardデバイス名
	private static readonly int 	FOV = 60;						// カメラの視野の規定値
	private bool vrModeEnabled = true;	// VRモードを設定する
	#endregion // Member Field


	#region Properties
	/// <summary>
	/// VRモードの切り替え
	/// </summary>
	public bool VRModeEnabled
	{
		get { return vrModeEnabled; }
		set { SetVR(value); }
	}
	#endregion // Properties


	#region MonoBehaviour Methods
	private void Start ()
	{
        // Cardboardデバイスが存在するかどうか
		bool isValid = false;
        foreach (string device in VRSettings.supportedDevices)
        {
            Debug.Log("supportDevices : " + device);
			if (device == VR_DEVICE_NAME)
			{
				isValid = true;
			}
        }
		if (!isValid)
		{
			throw new Exception("Could not find \"Cardboard\" in Virtual Reality SDKs.");
		}
		
		// VRDevice検知用オブジェクト生成
		GameObject detector = Instantiate(new GameObject ("VRDeviceDetector"));
		GameObject.DontDestroyOnLoad(detector);
		detector.AddComponent<VRDeviceDetector>();

		// VRセットアップ
		if (VRSettings.loadedDeviceName == VR_DEVICE_NAME)
		{
			// VRの設定を引き継ぐ
			SetVR(VRSettings.enabled);
			// VRHead追加
			AddVRHead ();
		}
		else
		{
			// VRデバイスを読み込む
			StartCoroutine(LoadDevice(vrModeEnabled));
		}
	}
	
	private void Update ()
	{
		
	}
	#endregion // MonoBehaviour Methods


	#region Member Methods
	// VRモード切り替え
    private void SetVR(bool enabled)
    {
		vrModeEnabled		= enabled;	// フラグ設定
		VRSettings.enabled	= enabled;	// VRモード切り替え
	}
	
	// VRデバイスの読み込み
    private IEnumerator LoadDevice(bool enabled)
    {
        VRSettings.LoadDeviceByName(VR_DEVICE_NAME); // 指定のデバイスを読み込む
        yield return null;				// 1フレーム待機
		VRSettings.enabled = enabled;	// VRモードを有効

		// VRHead追加
		AddVRHead ();
	}

	// シーン内のカメラにVRHeadコンポーネントを追加する
	private void AddVRHead ()
	{
		Camera cam = Camera.main;
		cam.fieldOfView = FOV; // 視野を規定値に設定
		cam.gameObject.AddComponent<VRHead>();
	}
	#endregion // Member Methods
}