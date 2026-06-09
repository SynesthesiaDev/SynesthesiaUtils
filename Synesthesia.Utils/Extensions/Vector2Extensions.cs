// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Numerics;

namespace Synesthesia.Utils.Extensions;

public static class Vector2Extensions
{
    extension(Vector2 point)
    {
        public Vector2 RotateAroundOrigin(Vector2 origin, float angle)
        {
            angle = -angle;

            point.X -= origin.X;
            point.Y -= origin.Y;

            var ret = point.Rotate(angle);

            ret.X += origin.X;
            ret.Y += origin.Y;

            return ret;
        }

        public Vector2 Rotate(float angle)
        {
            return new Vector2(
                point.X * MathF.Cos(float.DegreesToRadians(angle)) + point.Y * MathF.Sin(float.DegreesToRadians(angle)),
                point.X * -MathF.Sin(float.DegreesToRadians(angle)) + point.Y * MathF.Cos(float.DegreesToRadians(angle))
            );
        }
    }

    public static Vector2 GetScaledPosition(Vector2 scale, Vector2 origin, Vector2 position, float axisRotation = 0)
    {
        return origin + ((position - origin).Rotate(axisRotation) * scale).Rotate(-axisRotation);
    }

    public static bool IsFinite(this Vector2 toCheck) => float.IsFinite(toCheck.X) && float.IsFinite(toCheck.Y);

}
