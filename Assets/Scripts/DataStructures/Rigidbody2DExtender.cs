using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rigidbody2DExtender
{
    private readonly Rigidbody2D _rigidbody;
    private readonly Dictionary<string, BoolCounter> _rigidbodyLayersStatus = new();

    public Rigidbody2DExtender(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }

    public void SetExcludeLayers(LayerMask layerMask, bool value)
    {
        var layers = DisassembleMaskIntoLayers(layerMask);
        layers.ForEach(x => _rigidbodyLayersStatus.AddOrModify(x, () => new(value), y => ModifyExclusionBoolCounter(y, value)));

        var newLayerMask = LayerMask.GetMask(_rigidbodyLayersStatus.Where(x => x.Value).Select(x => x.Key).ToArray());
        _rigidbody.excludeLayers = newLayerMask;
    }

    private static string[] DisassembleMaskIntoLayers(LayerMask layerMask)
    {
        return Convert
            .ToString(layerMask.value, 2)
            .Reverse()
            .Select((x, i) => x != '0' ? LayerMask.LayerToName(i) : "")
            .Where(x => x != "")
            .ToArray();
    }

    /// <summary>
    /// BoolCounter can stack exclusion but not vice versa.
    /// </summary>
    private static BoolCounter ModifyExclusionBoolCounter(BoolCounter counter, bool value)
    {
        if (value || counter)
        {
            counter.Set(value);
        }
        else
        {
            counter.Reset(false);
        }

        return counter;
    }
}
