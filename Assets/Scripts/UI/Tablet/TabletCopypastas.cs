using System.Collections.Generic;
using UnityEngine;

public class TabletCopypastas : TabletApplication
{
    [SerializeField]
    private TextLocalizer _text;

    private string[] _copypastas;
    private readonly List<string> _unusedCopypastas = new();

    private void Awake()
    {
        _copypastas = new[] { "111", "222", "333", "444", "555" };
        SetRandomCopypasta();
    }

    public void SetRandomCopypasta()
    {
        if (_unusedCopypastas.Count == 0)
        {
            if (_copypastas.Length == 0)
            {
                Logger.Error("No copypastas found");
                return;
            }
            _copypastas.ForEach(x => _unusedCopypastas.Add(x));
        }
        var randomindex = Random.Range(0, _unusedCopypastas.Count);
        _text.SetText(_unusedCopypastas[randomindex]);
        _unusedCopypastas.Remove(_unusedCopypastas[randomindex]);
    }
}
