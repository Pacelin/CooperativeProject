using UnityEngine;

public static class TransformExtensions
{
    public static void TeleportRelative(this Transform transform, Transform relativePoint, Transform toPoint)
    {
        var point = relativePoint.InverseTransformPoint(transform.position);
        var newPosition = toPoint.TransformPoint(point);

        transform.position = newPosition;
        transform.Rotate(toPoint.rotation.eulerAngles - relativePoint.rotation.eulerAngles, Space.World);
    }
}