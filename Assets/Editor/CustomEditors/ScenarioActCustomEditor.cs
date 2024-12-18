using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScenarioAct), true)]
public class ScenarioActCustomEditor : Editor
{
    public void OnSceneGUI()
    {
        var selectedAct = target as ScenarioAct;
        foreach (var act in FindObjectsByType<ScenarioAct>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Except(selectedAct.Yield()))
        {
            ActClosure(act);
        }
    }

    private static void ActClosure(ScenarioAct act)
    {
        Handles.color = MyColors.Transparent;
        if (Handles.Button(act.transform.position, Quaternion.identity, 0.22f, 0.22f, RectangleHandleCapWithTooltip))
        {
            Selection.activeObject = act;
        }

        void RectangleHandleCapWithTooltip(int controlID, Vector3 position, Quaternion rotation, float size, EventType eventType)
        {
            Handles.RectangleHandleCap(controlID, position, rotation, size, eventType);
            if (eventType == EventType.Repaint && HandleUtility.nearestControl == controlID)
            {
                Handles.Label(position, act.name, new()
                {
                    normal = new() { textColor = Color.black },
                    alignment = TextAnchor.MiddleCenter
                });
            }
        }
    }
}
