using UnityEngine;

public class TableOpener : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;

    private void Awake()
    {
        ActivateIndicator(false);
    }
    public void ActivateIndicator(bool isActivate)
    {
        _indicator.SetActive(isActivate);
    }
    public void DisableTable()
    {
        this.gameObject.SetActive(false);
    }
}
