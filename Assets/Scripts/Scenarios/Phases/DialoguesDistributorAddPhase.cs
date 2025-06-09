using System.Collections.Generic;
using UnityEngine;

public class DialoguesDistributorAddPhase : ActPhase
{
    [SerializeField]
    private DialoguesDistributorKey _key;
    [SerializeField]
    private Dialogue[] _dialogues;

    private static readonly Dictionary<DialoguesDistributorKey, Distributor<Dialogue>> _distributors = new();

    static DialoguesDistributorAddPhase() => GameManager.SceneChanging += () => _distributors.Clear();

    public override void Invoke()
    {
        _distributors.AddOrModify(_key, () => new(_dialogues), x => x.Add(_dialogues));
    }

    public static Dialogue GetNextDialogue(DialoguesDistributorKey key)
    {
        return _distributors.TryGetValue(key, out var distributor) ? distributor.GetNext() : null;
    }

    public override string IconName => "DialogueAdd2.png";
}

public enum DialoguesDistributorKey
{
    Key0 = 0,
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
}
