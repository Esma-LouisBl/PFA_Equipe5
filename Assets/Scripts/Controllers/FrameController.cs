using System;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    [SerializeField]
    private Frame frameObject;
    private GameObject _activeFrame;


    public void ShowFrame(Frame frameData)
    {
        if (frameData == null || frameData.PhotoFrame == null)
        {
            Debug.LogWarning("Invalid frame data.");
            return;
        }
        FrameInstance[] instances = FindObjectsOfType<FrameInstance>();

        foreach (var instance in instances)
        {
            if (instance.prefabReference == frameData.PhotoFrame)
            {
                instance.gameObject.SetActive(true);
                Debug.Log("Frame activated: " + instance.name);
                return;
            }
        }

        Debug.LogWarning("No matching scene object found for prefab: " + frameData.PhotoFrame.name);
    }
}










