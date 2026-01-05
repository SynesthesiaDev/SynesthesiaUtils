using System.Numerics;

namespace SynesthesiaUtil.Extensions;

public static class Vector3Extensions
{
    public static Vector3 RotateAroundOrigin(this Vector3 point, Vector3 axis, Vector3 origin, float angleDegrees)
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

    public static Vector3 Rotate(this Vector3 vector, Vector3 axis, float angleDegrees)
    {
        var radians = float.DegreesToRadians(angleDegrees);
        var rotation = Quaternion.CreateFromAxisAngle(axis, radians);
        return Vector3.Transform(vector, rotation);
    }

    public static bool IsFinite(this Vector3 toCheck) => float.IsFinite(toCheck.X) && float.IsFinite(toCheck.Y) && float.IsFinite(toCheck.Z);
}