﻿using System;

namespace MathLibrary
{
    public class Vector2
    {
        private float _x;
        private float _y;

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

        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
        } //Magnitude Property

        public Vector2 Normalized
        {
            get { return Normalize(this); }
        } //Normalized Property

        public Vector2()
        {
            _x = 0;
            _y = 0;
        } //Constructor

        public Vector2(float x, float y)
        {
            _x = x;
            _y = y;
        } //Overload Constructor

        /// <summary>
        /// Returns the normalized version of a the vector passed in.
        /// </summary>
        /// <param name="vector">The vector that will be normalized</param>
        /// <returns></returns>
        public static Vector2 Normalize(Vector2 vector)
        {
            if (vector.Magnitude == 0)
                return new Vector2();
            return vector / vector.Magnitude;
        } //Normalize function

        /// <summary>
        /// Returns the dot product of the two vectors given.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y);
        } //Dot Product function

        public static float FindAngle(Vector2 lhs, Vector2 rhs)
        {
            lhs = lhs.Normalized;
            rhs = rhs.Normalized;

            float dotProd = DotProduct(lhs, rhs);

            if (Math.Abs(dotProd) > 1)
                return 0;

            float angle = (float)Math.Acos(dotProd);

            Vector2 perp = new Vector2(rhs.Y, -rhs.X);

            float perpDot = DotProduct(perp, lhs);

            if (perpDot != 0)
                angle *= perpDot / Math.Abs(perpDot);

            return angle;
        } //Find Angle function

        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X += rhs.X, lhs.Y += rhs.Y);
        } //Addition overload

        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.X - rhs.X, lhs.Y - rhs.Y);
        } //Subtraction overload

        public static Vector2 operator *(Vector2 lhs, float scalar)
        {
            return new Vector2(lhs.X * scalar, lhs.Y * scalar);
        } //Multiplication overload

        public static Vector2 operator *(float scalar, Vector2 lhs)
        {
            return new Vector2(lhs.X * scalar, lhs.Y * scalar);
        } //Multiplication overload

        public static Vector2 operator /(Vector2 lhs, float scalar)
        {
            return new Vector2(lhs.X / scalar, lhs.Y / scalar);
        } //Division overload
    } //Vector 2
} //Math For Games
