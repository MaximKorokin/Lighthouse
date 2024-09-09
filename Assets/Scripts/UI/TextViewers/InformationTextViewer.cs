using UnityEngine;

public class InformationTextViewer : AudioTextViewer
{
    public static InformationTextViewer Instance;

    [SerializeField]
    private AudioClip _typingSound;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        SetTypingSound(_typingSound);
    }

    protected override void OnViewStarted()
    {
        Typewriter.Text.enabled = true;
    }

    protected override void OnViewFinished()
    {
        Typewriter.Text.enabled = false;
    }
}
