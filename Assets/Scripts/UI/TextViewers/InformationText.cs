using UnityEngine;

[RequireComponent(typeof(TypewriterText))]
public class InformationText : TextViewer
{
    public static InformationText Instance;

    protected void Awake()
    {
        Instance = this;
        Typewriter = GetComponent<TypewriterText>();
    }

    protected override void StartView()
    {
        Instance.Typewriter.Text.enabled = true;
    }

    protected override void EndView()
    {
        Instance.Typewriter.Text.enabled = false;
    }
}
