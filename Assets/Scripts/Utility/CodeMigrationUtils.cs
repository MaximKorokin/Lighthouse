#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

/// <summary>
/// Very useful when need to migrate from old script to a new one when it was used everywhere.
/// Just need to change some hardcode here =)
/// </summary>
public static class CodeMigrationUtils
{
    //[MenuItem("Tools/Add BoxCollider and Save Scene")]
    //private static void Update1()
    //{
    //    var oldPhases = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<ShowHideTilemapsPhase>());

    //    foreach (var phase in oldPhases)
    //    {
    //        if (phase.GetComponent<TilemapsAlphaPhase>() != null) continue;

    //        if (phase._tilemapsToShow?.Length > 0)
    //        {
    //            // Добавляем компонент с поддержкой Undo
    //            var newPhase = Undo.AddComponent<TilemapsAlphaPhase>(phase.gameObject);
    //            // Помечаем объект как изменённый
    //            EditorUtility.SetDirty(phase.gameObject);

    //            newPhase._tilemaps = new Tilemap[phase._tilemapsToShow.Length];
    //            phase._tilemapsToShow.ForEach((x, i) => newPhase._tilemaps[i] = x);
    //            newPhase._time = phase._time;
    //            newPhase._alpha = phase._showAlpha;
    //        }

    //        if (phase._tilemapsToHide?.Length > 0)
    //        {
    //            // Добавляем компонент с поддержкой Undo
    //            var newPhase = Undo.AddComponent<TilemapsAlphaPhase>(phase.gameObject);
    //            // Помечаем объект как изменённый
    //            EditorUtility.SetDirty(phase.gameObject);

    //            newPhase._tilemaps = new Tilemap[phase._tilemapsToHide.Length];
    //            phase._tilemapsToHide.ForEach((x, i) => newPhase._tilemaps[i] = x);
    //            newPhase._time = phase._time;
    //            newPhase._alpha = phase._hideAlpha;
    //        }

    //        Logger.Log("Added new tilemap alpha phase for" + phase.gameObject.name + " | " + phase.gameObject.transform.parent.name);
    //    }

    //    // Помечаем сцену как изменённую
    //    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    //    // Сохраняем сцену
    //    //EditorSceneManager.SaveScene(gameObject.scene);
    //}
}
#endif
