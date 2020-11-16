using System;

namespace MathLibrary
{
    public class Vector4
    {
        private float _x;
        private float _y;
        private float _z;
        private float _w;

        public float X
        {
            get { return _x; }
            set { _x = value; }
        } //X property

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        } //Y property

        public float Z
        {
            get { return _z; }
            set { _z = value; }
        } //Z property

        public float W
        {
            get { return _w; }
            set { _w = value; }
        } //W property

        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); }
        } //Manitude property

        public Vector4 Normalized
        {
            get { return Normalize(this); }
        } //Normalized

        public Vector4()
        {
            _x = 0;
            _y = 0;
            _z = 0;
            _w = 0;
        } //Constructor

        public Vector4(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        } //Overload Constructor

        /// <summary>
        /// Returns the normalized version of a the vector passed in.
        /// </summary>
        /// <param name="vector">The vector that will be normalized</param>
        /// <returns></returns>
        public static Vector4 Normalize(Vector4 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector4();
            return vector / vector.Magnitude;
        } //Normalize function

        /// <summary>
        /// Returns the dot product of the two vectors given.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z) + (lhs.W * rhs.W);
        } //Dor Product function

        public static Vector4 CrossProduct(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.Y * rhs.Z - lhs.Z * rhs.Y, //X
                               lhs.Z * rhs.X - lhs.X * rhs.Z, //Y
                               lhs.X * rhs.Y - lhs.Y * rhs.X, //Z
                               0);
        } //Cross Product function

        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4(
                (rhs.X * lhs.m11) + (rhs.Y * lhs.m12) + (rhs.Z * lhs.m13) + (rhs.W * lhs.m14),
                (rhs.X * lhs.m21) + (rhs.Y * lhs.m22) + (rhs.Z * lhs.m23) + (rhs.W * lhs.m24),
                (rhs.X * lhs.m31) + (rhs.Y * lhs.m32) + (rhs.Z * lhs.m33) + (rhs.W * lhs.m34),
                (rhs.X * lhs.m41) + (rhs.Y * lhs.m42) + (rhs.Z * lhs.m43) + (rhs.W * lhs.m44));
        } //Multiplication overload for Matrix4

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.X += rhs.X, lhs.Y += rhs.Y, lhs.Z += rhs.Z, lhs.W += rhs.W);
        } //Addition overload

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(lhs.X -= rhs.X, lhs.Y -= rhs.Y, lhs.Z -= rhs.Z, lhs.W -= rhs.W);
        } //Subtraction overload

        public static Vector4 operator *(Vector4 lhs, float scalar)
        {
            return new Vector4(lhs.X * scalar, lhs.Y * scalar, lhs.Z * scalar, lhs.W * scalar);
        } //Multiplication overload

        public static Vector4 operator *(float scalar, Vector4 lhs)
        {
            return new Vector4(lhs.X * scalar, lhs.Y * scalar, lhs.Z * scalar, lhs.W * scalar);
        } //Multiplication overload

        public static Vector4 operator /(Vector4 lhs, float scalar)
        {
            return new Vector4(lhs.X / scalar, lhs.Y / scalar, lhs.Z / scalar, lhs.W / scalar);
        } //Division overload
    } //Vector 4
} //Math Library