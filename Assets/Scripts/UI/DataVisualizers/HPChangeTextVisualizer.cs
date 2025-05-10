using TMPro;
using UnityEngine;

class HPChangeTextVisualizer : TextVisualizer
{
    [SerializeField]
    private float _lowerThreshold;

    private WorldCanvasProvider _canvasProvider;

    protected DestroyableWorldObject WorldObject { get; private set; }

    protected void Start()
    {
        _canvasProvider = this.GetRequiredComponent<WorldCanvasProvider>();
        WorldObject = this.GetRequiredComponent<DestroyableWorldObject>();
        WorldObject.HealthPointsChanged += VisualizeHPChange;
    }

    private void VisualizeHPChange(float prevHP, float curHP, float maxHP)
    {
        if (prevHP == 0)
        {
            return;
        }
        var hpDelta = prevHP - curHP;
        if (hpDelta < 0 && -hpDelta > _lowerThreshold)
        {
            var text = VisualizeText((-hpDelta).ToString());
            text.color = Color.green;
        }
        else if (hpDelta > 0)
        {
            var text = VisualizeText(hpDelta.ToString());
            text.color = Color.red;
        }
    }

    public override TMP_Text VisualizeText(string visualizeString)
    {
        var text = base.VisualizeText(visualizeString);
        text.transform.SetParent(_canvasProvider.CanvasController.HPChangeParent, false);
        return text;
    }
}
