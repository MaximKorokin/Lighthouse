using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DevBuildLabel : MonoBehaviour
{
    [SerializeField]
    private string _formatString;

    private void Awake()
    {
        GetComponent<TMP_Text>().text = string.Format(_formatString, Application.version);
    }
}
