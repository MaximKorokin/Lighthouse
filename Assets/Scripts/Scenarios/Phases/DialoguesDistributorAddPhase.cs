using System.Collections.Generic;
using UnityEngine;

public class DialoguesDistributorAddPhase : ActPhase
{
    [SerializeField]
    private DialoguesDistributorKey _key;
    [SerializeField]
    private Dialogue[] _dialogues;

    public override void Invoke()
    {
        _distributors.AddOrModify(_key, () => new(_dialogues), x => x.Add(_dialogues));
    }

    public override string IconName => "DialogueAdd1.png";

    private static readonly Dictionary<DialoguesDistributorKey, Distributor<Dialogue>> _distributors = new();

    public static Dialogue GetNextDialogue(DialoguesDistributorKey key)
    {
         return _distributors.TryGetValue(key, out var distributor) ? distributor.GetNext() : null;
    }
}

public enum DialoguesDistributorKey
{
    Key0 = 0,
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
}
