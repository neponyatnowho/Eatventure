using UnityEngine;

public class IconTable : MonoBehaviour
{
    [SerializeField] private MeshRenderer _iconMeshRander;
    
    public void SetIcon(Material material)
    {
        _iconMeshRander.material = material;
    }
}
