using TMPro;
using UnityEngine;

public class DevBuildLabel : MonoBehaviour
{
    [SerializeField]
    private string _formatString;

    private void Awake()
    {
        this.GetRequiredComponent<TMP_Text>().text = string.Format(_formatString, Application.version);
    }
}
