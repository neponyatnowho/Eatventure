using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class ClearPlayerPrefs : MonoBehaviour
{
    [MenuItem("Custom/Clear PlayerPrefs")]
    private static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared!");
    }
}
#endif

