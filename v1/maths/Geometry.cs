using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Euclid2d.Euclid.model;
using Euclid2d.Euclid.draws;
using Euclid2d.Euclid.others;

namespace Euclid2d.Euclid.maths
{
    public static class Geometry
    {
        /*===================================================================================================*/
        #region others

        public static Boolean IsNaN(float number)
        {
            return float.IsNaN(number);
        }

        #endregion
        /*===================================================================================================*/
        #region CartesianPolar

        /* RO */
        public static float PolarRO(float x, float y)
        {
            float dx = (float)(Math.Pow(x, 2));
            float dy = (float)(Math.Pow(y, 2));

            return (float)(Math.Sqrt(dx + dy));
        }
        /* THETA */
        public static float PolarTHETA(float x, float y)
        {
            return (float)(Math.Atan2(y, x) * Constants.ANG);
        }
        /* X */
        public static float CartX(float ro, float theta)
        {
            return (float)(ro * Math.Cos(theta * Constants.RAD));
        }
        /* Y */
        public static float CartY(float ro, float theta)
        {
            return (float)(ro * Math.Sin(theta * Constants.RAD));
        }

        #endregion
        /*===================================================================================================*/
        #region vectors

        public static float Magnitude(float x, float y)
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
        public static float Magnitude(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        public static VectorC Normalize(float x, float y)
        {
            float d = Magnitude(x, y);

            return new VectorC(x / d, y / d);
        }
        public static VectorC Negate(float x, float y)
        {
            return new VectorC(-x, -y);
        }
        public static VectorC Add(float x, float y, float alfa)
        {
            return new VectorC(x + alfa, y + alfa);
        }
        public static VectorC Sub(float x, float y, float alfa)
        {
            return new VectorC(x - alfa, y - alfa);
        }
        public static VectorC Mul(float x, float y, float alfa)
        {
            return new VectorC(x * alfa, y * alfa);
        }
        public static VectorC Div(float x, float y, float alfa)
        {
            return new VectorC(x / alfa, y / alfa);
        }
        public static VectorC Add(float x1, float y1, float x2, float y2)
        {
            return new VectorC(x1 + x2, y1 + y2);
        }
        public static VectorC Sub(float x1, float y1, float x2, float y2)
        {
            return new VectorC(x1 - x2, y1 - y2);
        }
        public static VectorC Div(float x1, float y1, float x2, float y2)
        {
            return new VectorC(x1 / x2, y1 / y2);
        }
        public static float Dot(float x1, float y1, float x2, float y2)
        {
            return (x1 * x2 + y1 * y2);
        }
        public static float Cross(float x1, float y1, float x2, float y2)
        {
            return (x1 * y2 + y1 * x2);
        }
        public static float Ang(float x1, float y1, float x2, float y2)
        {
            float cross_product = Cross(x1, y1, x2, y2);
            float magni_product = Magnitude(x1, y1) * Magnitude(x2, y2);

            return (float)(Math.Acos(cross_product / magni_product) * Constants.ANG);
        }

        public static float Magnitude(VectorC v)
        {
            return (float)Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
        }
        public static float Magnitude(VectorC v1, VectorC v2)
        {
            return (float)Math.Sqrt(Math.Pow((v2.X - v1.X), 2) + Math.Pow((v2.Y - v1.Y), 2));
        }
        public static VectorC Normalize(VectorC v)
        {
            float d = Magnitude(v);

            return new VectorC(v.X / d, v.Y / d);
        }
        public static VectorC Negate(VectorC v)
        {
            return new VectorC(-v.X, -v.Y);
        }
        public static VectorC Add(VectorC v, float alfa)
        {
            return new VectorC(v.X + alfa, v.Y + alfa);
        }
        public static VectorC Sub(VectorC v, float alfa)
        {
            return new VectorC(v.X - alfa, v.Y - alfa);
        }
        public static VectorC Mul(VectorC v, float alfa)
        {
            return new VectorC(v.X * alfa, v.Y * alfa);
        }
        public static VectorC Div(VectorC v, float alfa)
        {
            return new VectorC(v.X / alfa, v.Y / alfa);
        }
        public static VectorC Add(VectorC v1, VectorC v2)
        {
            return new VectorC(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static VectorC Sub(VectorC v1, VectorC v2)
        {
            return new VectorC(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static VectorC Div(VectorC v1, VectorC v2)
        {
            return new VectorC(v1.X / v2.X, v1.Y / v2.Y);
        }
        public static float Dot(VectorC v1, VectorC v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y);
        }
        public static float Cross(VectorC v1, VectorC v2)
        {
            return (v1.X * v2.Y + v1.Y * v2.X);
        }
        public static float Ang(VectorC v1, VectorC v2)
        {
            float cross_product = Cross(v1, v2);
            float magni_product = Magnitude(v1) * Magnitude(v2);

            return (float)(Math.Acos(cross_product / magni_product) * Constants.ANG);
        }

        #endregion
        /*===================================================================================================*/
        #region points

        public static Boolean IsInsideMap(float x, float y)
        {
            float[] rngX = Drawing.rangeX();
            float[] rngY = Drawing.rangeY();

            Boolean c1 = Mathematics.isIn(x, rngX[0], rngX[1]);
            Boolean c2 = Mathematics.isIn(y, rngY[0], rngY[1]);

            return c1 && c2;
        }
        public static Boolean IsInsideMap(PointC p)
        {
            float[] rngX = Drawing.rangeX();
            float[] rngY = Drawing.rangeY();

            Boolean c1 = Mathematics.isIn(p.X, rngX[0], rngX[1]);
            Boolean c2 = Mathematics.isIn(p.Y, rngY[0], rngY[1]);

            return c1 && c2;
        }

        public static float Dist(float x, float y)
        {
            return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }
        public static float Dist(PointC p)
        {
            return (float)Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2));
        }

        public static float Dist(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }
        public static float Dist(PointC p1, PointC p2)
        {
            return (float)Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
        }

        public static PointC Reflect(PointC p1, PointC p2, float times = 1.00f)
        {
            float x = p1.X + times * (p2.X - p1.X);
            float y = p1.Y + times * (p2.Y - p1.Y);

            return new PointC(x, y);
        }

        public static PointC ReflectOrtho(PointC p1, PointC p2, float times = 50.00f)
        {
            VectorC v = new VectorC(p1, p2);
            VectorC dv = Mul(Normalize(v), times);

            return new PointC(p1.X + dv.X, p1.Y + dv.Y);
        }

        public static Boolean IsBetween(PointC p, PointC p1, PointC p2)
        {
            float l1 = (p.X - p1.X) / (p2.X - p1.X);
            float l2 = (p.Y - p1.Y) / (p2.Y - p1.Y);

            if (Mathematics.isIn(l1, 0, 0.999f) && Mathematics.isIn(l2, 0, 0.999f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
        /*===================================================================================================*/
        #region lines

        public static float[] NormalEqu(float x1, float y1, float x2, float y2)
        {
            float[] eq = new float[2];

            if (Mathematics.Equal(x1, x2))
            {
                eq[0] = float.NaN;
                eq[1] = y1;

                return eq;
            }
            else
            {
                eq[0] = ((y2 - y1) / (x2 - x1));
                eq[1] = y1 - x1 * eq[0];

                return eq;
            }
        }
        public static float[] NormalEqu(PointC p1, PointC p2)
        {
            float[] eq = new float[2];

            if(Mathematics.Equal(p1.X, p2.X))
            {
                eq[0] = float.NaN;
                eq[1] = p1.Y;

                return eq;
            }
            else
            {
                eq[0] = ((p2.Y - p1.Y) / (p2.X - p1.X));
                eq[1] = p1.Y - p1.X * eq[0];

                return eq;
            }
        }

        public static float[] GeneralEqu(float x1, float y1, float x2, float y2)
        {
            float[] eq = new float[3];

            eq[0] = y2 - y1;
            eq[1] = x1 - x2;
            eq[2] = -(x1 * y2 - x2 * y1);

            return eq;
        }
        public static float[] GeneralEqu(PointC p1, PointC p2)
        {
            float[] eq = new float[3];

            eq[0] = p2.Y - p1.Y;
            eq[1] = p1.X - p2.X;
            eq[2] = -(p1.X * p2.Y - p2.X * p1.Y);

            return eq;
        }

        public static Boolean AreParallel(LineC l1, LineC l2)
        {
            return Mathematics.Equal(l1.NormalEq[0], l2.NormalEq[0]);
        }
        public static Boolean ArePerpendicular(LineC l1, LineC l2)
        {
            if(AreParallel(l1, l2))
            {
                return false;
            }

            if(Mathematics.isNan(l1.NormalEq[0]) && Mathematics.IsZero(l2.NormalEq[0]))
            {
                return true;
            }

            if (Mathematics.isNan(l2.NormalEq[0]) && Mathematics.IsZero(l1.NormalEq[0]))
            {
                return true;
            }

            return Mathematics.Equal(l1.NormalEq[0] * l2.NormalEq[0], -1);
        }

        public static Boolean IsOnTheLine(PointC p, LineC l)
        {
            float[] eq = l.GeneralEq;
            float test = eq[0] * p.X + eq[1] * p.Y + eq[2];

            return Mathematics.IsZero(test);
        }

        public static PointC Intersection(LineC l1, LineC l2)
        {
            if(AreParallel(l1, l2))
            {
                return null;
            }

            float[] eq1 = l1.GeneralEq;
            float[] eq2 = l2.GeneralEq;

            float num = (eq1[0] * eq2[1]) - (eq2[0] * eq1[1]);

            if (Mathematics.IsZero(num))
            {
                return null;
            }

            float numX = (eq2[2] * eq1[1]) - (eq1[2] * eq2[1]);
            float numY = (eq2[0] * eq1[2]) - (eq1[0] * eq2[2]);

            return new PointC(numX / num, numY / num);
        }
        public static List<PointC> Intersections(List<LineC> ls)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Count - 1; i++)
            {
                for (int j = 1; j < ls.Count; j++)
                {
                    PointC p = Intersection(ls[i], ls[j]);

                    if (p != null)
                    {
                        ps.Add(p);
                    }
                }
            }

            return ps;
        }

        public static LineC LineFromNormalForm(float m, float b)
        {
            float[] x = new float[2];
            float[] y = new float[2];

            if (Mathematics.isNan(m))
            {
                x[0] = b;
                x[1] = b;
                y[0] = Drawing.ry();
                y[1] = Drawing.ry();
            }
            else if (Mathematics.IsZero(m))
            {
                x[0] = Drawing.rx();
                x[1] = Drawing.rx();
                y[0] = b;
                y[1] = b;

            }
            else
            {
                x[0] = Drawing.rx();
                y[0] = m * x[0] + b;

                x[1] = Drawing.rx();
                y[1] = m * x[1] + b;
            }
            
            LineC l = new LineC(new PointC(x[0], y[0]), new PointC(x[1], y[1]));

            LineC[] BL = LineC.BoundingLines();

            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < BL.Length; i++)
            {
                PointC p = Intersection(l, BL[i]);

                if(p != null)
                {
                    if (p.IsInsideMap())
                    {
                        ps.Add(p);
                    }
                }
            }

            if(ps.Count < 2)
            {
                return l;
            }

            return new LineC(ps[0], ps[1]);

        }
        public static LineC LineFromGeneralForm(float a, float b, float c)
        {
            if(Mathematics.IsZero(a) && Mathematics.IsZero(b))
            {
                return null;
            }

            float[] x = new float[2];
            float[] y = new float[2];

            if (Mathematics.IsZero(a))
            {
                x[0] = Drawing.rx();
                x[1] = Drawing.rx();
                y[0] = -c/b;
                y[1] = -c/b;
            }
            else if (Mathematics.IsZero(b))
            {
                x[0] = -c / a;
                x[1] = -c / a;
                y[0] = Drawing.ry();
                y[1] = Drawing.ry();
            }
            else
            {
                x[0] = Drawing.rx();
                y[0] = (-a * x[0] - c) / b;
                x[1] = Drawing.rx();
                y[1] = (-a * x[1] - c) / b;
            }

            LineC l = new LineC(new PointC(x[0], y[0]), new PointC(x[1], y[1]));

            LineC[] BL = LineC.BoundingLines();

            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < BL.Length; i++)
            {
                PointC p = Intersection(l, BL[i]);

                if (p != null)
                {
                    if (p.IsInsideMap())
                    {
                        ps.Add(p);
                    }
                }
            }

            if (ps.Count < 2)
            {
                return l;
            }

            return new LineC(ps[0], ps[1]);
        }


        #endregion
        /*===================================================================================================*/
        #region trsts

        public static PointC Scale(PointC p, float Sx, float Sy)
        {
            return new PointC(p.X * Sx, p.Y * Sy);
        }
        public static PointC Scale(PointC scaled, PointC p, float Sx, float Sy)
        {
            float x = scaled.X * Sx + p.X * (1 - Sx);
            float y = scaled.Y * Sy + p.Y * (1 - Sy);

            return new PointC(x, y);
        }
        public static PointC Translate(PointC p, float Tx, float Ty)
        {
            return new PointC(p.X + Tx, p.Y + Ty);
        }
        public static PointC Rotation(PointC p, float angle)
        {
            float alfa = angle * Constants.RAD;
            float dx = (float)(p.X * Math.Cos(alfa) - p.Y * Math.Sin(alfa));
            float dy = (float)(p.X * Math.Sin(alfa) + p.Y * Math.Cos(alfa));

            return new PointC(dx, dy);
        }
        public static PointC Rotation(PointC rot, PointC p, float angle)
        {
            float alfa = angle * Constants.RAD;

            float dx = (float)(p.X * (1 - Math.Cos(alfa)) + p.Y * Math.Sin(alfa));
            float dy = (float)(p.Y * (1 - Math.Cos(alfa)) - p.X * Math.Sin(alfa));

            PointC temp = Rotation(rot, angle);

            return new PointC(temp.X + dx, temp.Y + dy);
        }
        public static PointC ShearX(PointC p, float angle)
        {
            float alfa = angle * Constants.RAD;

            float x = (float)(p.X + p.Y * Math.Tan(alfa));
            float y = p.Y;

            return new PointC(x, y);
        }
        public static PointC ShearY(PointC p, float angle)
        {
            float alfa = angle * Constants.RAD;

            float x = p.X;
            float y = (float)(Math.Tan(alfa) * p.X + p.Y);

            return new PointC(x, y);
        }
        public static PointC ReflectX(PointC p)
        {
            return new PointC(p.X, -p.Y);
        }
        public static PointC ReflectY(PointC p)
        {
            return new PointC(-p.X, p.Y);
        }
        public static PointC ReflectPX(PointC p, float axis)
        {
            return new PointC(p.X, -p.Y + 2 * axis);
        }
        public static PointC ReflectPY(PointC p, float axis)
        {
            return new PointC(-p.X + 2 * axis, p.Y);
        }
        public static PointC TChangeAxis(PointC p, float Tx, float Ty)
        {
            return new PointC(p.X - Tx, p.Y - Ty);
        }
        public static PointC RChangeAxis(PointC p, float angle)
        {
            float alfa = angle * Constants.RAD;
            float dx = (float)(p.X * Math.Cos(alfa) + p.Y * Math.Sin(alfa));
            float dy = (float)(p.X * -Math.Sin(alfa) + p.Y * Math.Cos(alfa));

            return new PointC(dx, dy);
        }

        #endregion
        /*===================================================================================================*/
    }
}
