﻿// =================================
//
//	VRDeviceController.cs
//	Created by Takuya Himeji
//
// =================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR;
#else
using UnityEngine.VR;
#endif

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
                instance = (VRDeviceController)FindObjectOfType(typeof(VRDeviceController));
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
    [SerializeField] private bool vrModeEnabled = true; // VRモードを設定する
    #endregion // Inspector Settings


    #region Member Field
    private static readonly string VR_DEVICE_NAME = "cardboard"; // Cardboardデバイス名
    private static readonly int	FOV = 60; // カメラの視野の規定値

    // VR切り替えの完了通知用
    public delegate void OnVRReady(bool vrModeEnabled);
    public OnVRReady onVRReady;
    #endregion // Member Field


    #region Properties
    /// <summary>
    /// VRモードの切り替え
    /// </summary>
    public bool VRModeEnabled
    {
        get { return GetVREnabled(); }
        set { SetVREnabled(value); }
    }
    #endregion // Properties


    #region MonoBehaviour Methods
    private void Start()
    {
		// 60FPSに固定
		Application.targetFrameRate = 60;
		// 対応デバイスを取得
        string[] supportedDevices = XRSettings.supportedDevices;

        // Cardboardデバイスが存在するかどうか
        bool isValid = false;
        foreach (string device in supportedDevices)
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
        GameObject detector = Instantiate(new GameObject("VRDeviceDetector"));
        GameObject.DontDestroyOnLoad(detector);
        detector.AddComponent<VRDeviceDetector>();

        // VRセットアップ
        if (GetLoadedDeviceName() == VR_DEVICE_NAME)
        {
            // VRの設定を引き継ぐ
            vrModeEnabled = GetVREnabled();
            // VRHead追加
            AddVRHead();
        }
        else
        {
            // VRデバイスを読み込む
            StartCoroutine(LoadDevice(vrModeEnabled));
        }
    }

    private void Update()
    {

    }
    #endregion // MonoBehaviour Methods


    #region Member Methods
    // VRデバイスの読み込み
    private IEnumerator LoadDevice(bool enabled)
    {
		// 指定のデバイスを読み込む
        XRSettings.LoadDeviceByName(VR_DEVICE_NAME);
        yield return null;          // 1フレーム待機
        VRModeEnabled = enabled;    // VRモードを有効

        // VRHead追加
        AddVRHead();

        // 準備完了
        if (onVRReady != null)
            onVRReady(vrModeEnabled);
    }

    // シーン内のカメラにVRHeadコンポーネントを追加する
    private void AddVRHead()
    {
        Camera[] cams = (Camera[])GameObject.FindObjectsOfType(typeof(Camera));
        foreach (Camera cam in cams)
        {
            cam.fieldOfView = FOV; // 視野を規定値に設定
            cam.gameObject.AddComponent<VRDeviceHead>();
        }
    }

	// ロード中のデバイス名を取得
	private string GetLoadedDeviceName()
	{
        return XRSettings.loadedDeviceName;
    }

    // VR or XRSettingsの状態を更新
    private void SetVREnabled(bool enabled)
	{
		vrModeEnabled = enabled;
        XRSettings.enabled = enabled;
        // アスペクト比をリセットする
        Camera[] cams = (Camera[])GameObject.FindObjectsOfType(typeof(Camera));
        foreach (Camera cam in cams)
        {
            cam.ResetAspect();
        }
    }

	// VR or XRSettingsの状態を取得
	private bool GetVREnabled()
	{
        return XRSettings.enabled;
    }
    #endregion // Member Methods
}