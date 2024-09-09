using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class TriggeringMediatorGizmosDrawers
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    public static void WorldObjectsTriggeringMediator(WorldObjectsTriggeringMediator mediator, GizmoType gizmoType)
    {
        var grid = (WorldGrid<WorldObject>)typeof(WorldObjectsTriggeringMediator).BaseType
            .GetField("WorldGrid", BindingFlags.NonPublic | BindingFlags.Instance)
            .GetValue(mediator);
        var cellSize = grid.CellSize;
        var margin = grid.CellSize / 20;
        var halfCellSize = grid.CellSize / 2;

        var lines = new List<Vector3>();
        foreach (var cell in grid)
        {
            if (grid.GetItems(cell).Any())
            {
                var point1 = new Vector3(cell.x * cellSize - halfCellSize + margin, cell.y * cellSize - halfCellSize + margin);
                var point2 = new Vector3(cell.x * cellSize - halfCellSize + margin, cell.y * cellSize + halfCellSize - margin);
                var point3 = new Vector3(cell.x * cellSize + halfCellSize - margin, cell.y * cellSize + halfCellSize - margin);
                var point4 = new Vector3(cell.x * cellSize + halfCellSize - margin, cell.y * cellSize - halfCellSize + margin);
                lines.AddRange(
                    point1, point2,
                    point2, point3,
                    point3, point4,
                    point4, point1);
            }
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawLineList(lines.ToArray());
    }
}
