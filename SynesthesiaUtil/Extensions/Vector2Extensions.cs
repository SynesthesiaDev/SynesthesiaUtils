using System;
using System.Numerics;

namespace SynesthesiaUtil.Extensions;

public static class Vector2Extensions
{
    public static Vector2 RotateAroundOrigin(this Vector2 point, Vector2 origin, float angle)
    {
        angle = -angle;

        point.X -= origin.X;
        point.Y -= origin.Y;

        var ret = point.Rotate(angle);

        ret.X += origin.X;
        ret.Y += origin.Y;

        return ret;
    }

    public static Vector2 Rotate(this Vector2 vector, float angle)
    {
        return new Vector2(
            vector.X * MathF.Cos(float.DegreesToRadians(angle)) + vector.Y * MathF.Sin(float.DegreesToRadians(angle)),
            vector.X * -MathF.Sin(float.DegreesToRadians(angle)) + vector.Y * MathF.Cos(float.DegreesToRadians(angle))
        );
    }

    public static Vector2 GetScaledPosition(Vector2 scale, Vector2 origin, Vector2 position, float axisRotation = 0)
    {
        return origin + ((position - origin).Rotate(axisRotation) * scale).Rotate(-axisRotation);
    }

    public static bool IsFinite(this Vector2 toCheck) => float.IsFinite(toCheck.X) && float.IsFinite(toCheck.Y);

}