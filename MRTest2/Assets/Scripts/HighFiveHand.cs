// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/// <summary>
/// This class implements IInputClickHandler to handle the tap gesture.
/// It increases the scale of the object when tapped.
/// </summary>
/// 
public class HighFiveHand : MonoBehaviour, IInputClickHandler
{
    public bool active = false;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        print("TOUCHED");
        if (!active) { return; }
        // Increase the scale of the object just as a response.
        transform.root.GetComponent<Animator>().SetBool("HighFive", false);
        active = false;
    }
}