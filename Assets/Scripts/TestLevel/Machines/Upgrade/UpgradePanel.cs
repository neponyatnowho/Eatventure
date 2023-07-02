using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UpgradePanel : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void OpenPanel()
    {
        _canvas.enabled = true;
    }
    public void ClosePanel()
    {
        _canvas.enabled = false;
    }
    
}
