// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Numerics;

namespace Synesthesia.Utils.Extensions;

public static class Vector3Extensions
{
    extension(Vector3 point)
    {
        public Vector3 RotateAroundOrigin(Vector3 axis, Vector3 origin, float angleDegrees)
        {
            var translatedPoint = new Vector3(
                point.X - origin.X,
                point.Y - origin.Y,
                point.Z - origin.Z
            );

            var rotatedPoint = translatedPoint.Rotate(axis, angleDegrees);

            return new Vector3(
                rotatedPoint.X + origin.X,
                rotatedPoint.Y + origin.Y,
                rotatedPoint.Z + origin.Z
            );
        }

        public Vector3 Rotate(Vector3 axis, float angleDegrees)
        {
            var radians = float.DegreesToRadians(angleDegrees);
            var rotation = Quaternion.CreateFromAxisAngle(axis, radians);
            return Vector3.Transform(point, rotation);
        }

        public bool IsFinite() => float.IsFinite(point.X) && float.IsFinite(point.Y) && float.IsFinite(point.Z);
    }
}
