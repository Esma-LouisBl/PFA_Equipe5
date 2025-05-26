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
            return;
        }
        FrameInstance[] instances = FindObjectsByType<FrameInstance>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var instance in instances)
        {
            if (instance.prefabReference == frameData.PhotoFrame)
            {
                instance.gameObject.SetActive(true);
                return;
            }
        }

    }
}










