// =================================
//
//	Transition.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
	#region MonoBehaviour Methods
	private void Start ()
	{
		
	}

	private void Update ()
	{
		
	}
	#endregion // MonoBehaviour Methods


	#region Member Methods
	public void OnTransition (string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	#endregion // Member Methods
}