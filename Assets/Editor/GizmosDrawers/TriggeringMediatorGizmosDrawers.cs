using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class TriggeringMediatorGizmosDrawers
{
    // todo: draw all rectangles at once
    //[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    public static void WorldObjectsTriggeringMediator(WorldObjectsTriggeringMediator mediator, GizmoType gizmoType)
    {
        var grid = (WorldGrid<WorldObject>)typeof(WorldObjectsTriggeringMediator).BaseType
            .GetField("WorldGrid", BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(mediator);
        var cellSize = grid.CellSize;
        var margin = grid.CellSize / 20;
        var halfCellSize = grid.CellSize / 2;
        foreach (var cell in grid)
        {
            if (grid.GetItems(cell).Any())
            {
                EditorUtils.DrawRectangle(new Vector2[]
                {
                    new(cell.x * cellSize - halfCellSize + margin, cell.y * cellSize - halfCellSize + margin),
                    new(cell.x * cellSize - halfCellSize + margin, cell.y * cellSize + halfCellSize - margin),
                    new(cell.x * cellSize + halfCellSize - margin, cell.y * cellSize + halfCellSize - margin),
                    new(cell.x * cellSize + halfCellSize - margin, cell.y * cellSize - halfCellSize + margin)
                }, Color.magenta);
            }
        }
    }
}
