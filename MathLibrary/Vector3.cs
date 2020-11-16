using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public class Vector3
    {
        private float _x;
        private float _y;
        private float _z;

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

        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
        } //Manitude property

        public Vector3 Normalized
        {
            get { return Normalize(this); }
        } //Normalized

        public Vector3()
        {
            _x = 0;
            _y = 0;
            _z = 0;
        } //Constructor

        public Vector3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        } //Overload Constructor

        /// <summary>
        /// Returns the normalized version of a the vector passed in.
        /// </summary>
        /// <param name="vector">The vector that will be normalized</param>
        /// <returns></returns>
        public static Vector3 Normalize(Vector3 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector3();
            return vector / vector.Magnitude;
        } //Normalize function

        /// <summary>
        /// Returns the dot product of the two vectors given.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        } //Dot Product function

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.Y * rhs.Z - lhs.Z * rhs.Y, //X
                               lhs.Z * rhs.X - lhs.X * rhs.Z, //Y
                               lhs.X * rhs.Y - lhs.Y * rhs.X);//Z
        } //Cross Product function

        public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
        {
            return new Vector3(
                rhs.X * lhs.m11 + rhs.Y * lhs.m12 + rhs.Z * lhs.m13,
                rhs.X * lhs.m21 + rhs.Y * lhs.m22 + rhs.Z * lhs.m23,
                rhs.X * lhs.m31 + rhs.Y * lhs.m32 + rhs.Z * lhs.m33);
        } //Multiplication overload for Matrix3

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.X += rhs.X, lhs.Y += rhs.Y, lhs.Z += rhs.Z);
        } //Addition overload

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        } //Subtraction overload

        public static Vector3 operator *(Vector3 lhs, float scalar)
        {
            return new Vector3(lhs.X * scalar, lhs.Y * scalar, lhs.Z * scalar);
        } //Multiplication overload

        public static Vector3 operator *(float scalar, Vector3 lhs)
        {
            return new Vector3(lhs.X * scalar, lhs.Y * scalar, lhs.Z * scalar);
        } //Multiplication overload

        public static Vector3 operator /(Vector3 lhs, float scalar)
        {
            return new Vector3(lhs.X / scalar, lhs.Y / scalar, lhs.Z / scalar);
        } //Division overload
    } //Vector3
} //Math Library