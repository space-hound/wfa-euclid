using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Euclid2d.Euclid.maths;
using Euclid2d.Euclid.draws;
using Euclid2d.Euclid.others;

namespace Euclid2d.Euclid.model
{
    public class LineC
    {
        /*===================================================================================================*/
        #region fields

        private PointC p1;
        private PointC p2;


        public static LineC[] BoundingLines()
        {
            PointC[] BP = PointC.BoundingPoints();
            LineC[] BL = new LineC[4];

            BL[0] = new LineC(BP[0], BP[1]);
            BL[1] = new LineC(BP[2], BP[3]);

            BL[2] = new LineC(BP[0], BP[2]);
            BL[3] = new LineC(BP[1], BP[3]);

            return BL;
        }

        #endregion
        /*===================================================================================================*/
        #region properties

        public PointC P1
        {
            get
            {
                return this.p1;
            }
            set
            {
                this.p1 = value;
            }
        }
        public PointC P2
        {
            get
            {
                return this.p2;
            }
            set
            {
                this.p2 = value;
            }
        }

        public float[] NormalEq
        {
            get
            {
                return this.normalForm();
            }
            set
            {
                this.lineFromNormalEq(value[0], value[1]);
            }
        }
        public float[] GeneralEq
        {
            get
            {
                return this.generalForm();
            }
            set
            {
                this.lineFromGeneralEq(value[0], value[1], value[2]);
            }
        }

        public VectorC V
        {
            get
            {
                return this.dirV();
            }
        }
        public VectorC nV
        {
            get
            {
                return this.normV();
            }
        }


        //--> normalForm()[0] -> Slope
        //--> normalForm()[1] -> Y-Intercept
        private float[] normalForm()
        {
            float[] eq = new float[2];

            if (Mathematics.Equal(this.p1.X, this.p2.X))
            {
                eq[0] = float.NaN;
                eq[1] = this.p1.Y;

                return eq;
            }
            else
            {
                eq[0] = ((this.p2.Y - this.p1.Y) / (this.p2.X - this.p1.X));
                eq[1] = this.p1.Y - this.p1.X * eq[0];

                return eq;
            }

        }

        //--> generalForm()[0] -> a
        //--> generalForm()[1] -> b
        //--> generalForm()[2] -> c
        private float[] generalForm()
        {
            float[] eq = new float[3];

            eq[0] = this.p2.Y - this.p1.Y;
            eq[1] = this.p1.X - this.p2.X;
            eq[2] = -(this.p1.X * this.p2.Y - this.p2.X * this.p1.Y);

            return eq;
        }

        //--> direction vector p1 - p2
        private VectorC dirV()
        {
            return new VectorC(this.p1, this.p2);
        }
        //--> norm vector generalForm()[0] * i + generalForm()[1] * j
        private VectorC normV()
        {
            float[] eq = this.generalForm();

            return new VectorC(eq[0], eq[1]);
        }

        //sets a line from normal equation 
        private void lineFromNormalEq(float m, float b)
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

            LineC[] BL = BoundingLines();

            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < BL.Length; i++)
            {
                PointC p = l.Intersection(BL[i]);

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
                this.p1 = l.p1;
                this.p2 = l.p2;
            }
            else
            {
                this.p1 = ps[0];
                this.p2 = ps[1];
            }
        }
        //sets a line from general form
        public void lineFromGeneralEq(float a, float b, float c)
        {
            if (Mathematics.IsZero(a) && Mathematics.IsZero(b))
            {
                throw new Exception("No' da ce is chestiile astea?");
            }

            float[] x = new float[2];
            float[] y = new float[2];

            if (Mathematics.IsZero(a))
            {
                x[0] = Drawing.rx();
                x[1] = Drawing.rx();
                y[0] = -c / b;
                y[1] = -c / b;
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
                PointC p = l.Intersection(BL[i]);

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
                this.p1 = l.p1;
                this.p2 = l.p2;
            }
            else
            {
                this.p1 = ps[0];
                this.p2 = ps[1];
            }
        }

        #endregion
        /*===================================================================================================*/
        #region constructors

        public LineC()
        {
            this.p1 = new PointC();
            this.p2 = new PointC();
        }

        public LineC(PointC p1, PointC p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
        public LineC(PointF p1, PointF p2)
        {
            this.p1 = new PointC(p1);
            this.p2 = new PointC(p2);
        }

        // construct line from equations 
        public LineC(float m, float b)
        {
            this.lineFromNormalEq(m, b);
        }
        public LineC(float a, float b, float c)
        {
            this.lineFromGeneralEq(a, b, c);
        }

        #endregion
        /*===================================================================================================*/
        #region methods

        public Boolean IsParallel(LineC l)
        {
            return Mathematics.Equal(this.normalForm()[0], l.normalForm()[0]);
        }
        public Boolean IsPerpendicular(LineC l)
        {
            if (this.IsParallel(l))
            {
                return false;
            }

            if (Mathematics.isNan(this.normalForm()[0]) && Mathematics.IsZero(l.normalForm()[0]))
            {
                return true;
            }

            if (Mathematics.isNan(l.normalForm()[0]) && Mathematics.IsZero(this.normalForm()[0]))
            {
                return true;
            }

            return Mathematics.Equal(this.normalForm()[0] * l.normalForm()[0], -1);
        }

        public Boolean Contains(PointC p)
        {
            float[] eq = this.generalForm();
            float test = eq[0] * p.X + eq[1] * p.Y + eq[2];

            return Mathematics.IsZero(test);
        }

        public PointC Intersection(LineC l)
        {
            if (this.IsParallel(l))
            {
                return null;
            }

            float[] eq1 = this.generalForm();
            float[] eq2 = l.generalForm();

            float num = (eq1[0] * eq2[1]) - (eq2[0] * eq1[1]);

            if (Mathematics.IsZero(num))
            {
                return null;
            }

            float numX = (eq2[2] * eq1[1]) - (eq1[2] * eq2[1]);
            float numY = (eq2[0] * eq1[2]) - (eq1[0] * eq2[2]);

            return new PointC(numX / num, numY / num);
        }
        public List<PointC> Intersection(List<LineC> ls)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Count; i++)
            {
                PointC p = this.Intersection(ls[i]);
                if (p != null)
                {
                    ps.Add(p);
                }
            }

            return ps;
        }
        public List<PointC> Intersection(LineC[] ls)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Length; i++)
            {
                PointC p = this.Intersection(ls[i]);
                if (p != null)
                {
                    ps.Add(p);
                }
            }

            return ps;
        }

        public float Dist(PointC pt)
        {
            VectorC n = this.nV;
            float n_n = n.Dot(n);
            VectorC p = pt.V;
            float n_p = n.Dot(p);

            float lambda = -(n_p + this.generalForm()[2]) / (n_n);

            VectorC lambda_n = n.Mul(lambda);

            return lambda_n.M;
        }
        public PointC Reflected(PointC pt, float times = 2)
        {
            VectorC n = this.nV;
            float n_n = n.Dot(n);
            VectorC p = pt.V;
            float n_p = n.Dot(p);

            float lambda = times * (n_p + this.generalForm()[2]) / (n_n);

            VectorC lambda_n = n.Mul(lambda);
            VectorC ptPrim = p.Sub(lambda_n);

            return new PointC(ptPrim.X, ptPrim.Y);
        }

        public LineC NormalThrough(PointC pt)
        {
            float[] eq = this.generalForm();

            float a = -eq[1];
            float b = eq[0];
            float c = eq[1] * pt.X - eq[0] * pt.Y;

            return new LineC(a, b, c);
        }
        public LineC ParallelThrough(PointC pt)
        {
            LineC Npt1 = this.NormalThrough(this.p1);
            LineC Npt2 = this.NormalThrough(this.p2);
            PointC lpt1 = Npt1.Reflected(pt, 1);
            PointC lpt2 = Npt2.Reflected(pt, 1);

            return new LineC(lpt1, lpt2);
        }
        public LineC ParrallelAt(float d = 70)
        {
            float[] eq = this.normalForm();

            float nd;

            if (Mathematics.isNan(eq[0]))
            {
                nd = (float)Math.Sqrt(1);
            }
            else
            {
                nd = (float)Math.Sqrt(1 + eq[0] * eq[0]);
            }

            LineC l = new LineC(eq[0], eq[1] + d * nd);

            return l;
        }

        public static LineC LineBetweenTwoBoints(PointC pt1, PointC pt2, float times = 0.5f)
        {
            float a = pt2.X - pt1.X;
            float b = pt2.Y - pt1.Y;
            float c = (float)(-times * (Math.Pow(pt2.X, 2) - Math.Pow(pt1.X, 2) + Math.Pow(pt2.Y, 2) - Math.Pow(pt1.Y, 2)));

            return new LineC(a, b, c);
        }
        public static List<LineC> RandomLines(int no = 30)
        {
            List<LineC> ls = new List<LineC>();
            for (int i = 0; i < no; i++)
            {
                ls.Add(new LineC());
            }

            return ls;
        }
        public static List<PointC> Intersections(List<LineC> ls)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Count - 1; i++)
            {
                for (int j = 1; j < ls.Count; j++)
                {
                    PointC p = ls[i].Intersection(ls[j]);

                    if(p != null)
                    {
                        ps.Add(p);
                    }
                }
            }

            return ps;
        }

        #endregion
        /*===================================================================================================*/
        #region trsts

        private void update(PointC q1, PointC q2)
        {
            this.p1 = q1;
            this.p2 = q2;
        }

        /* scalation */
        public void Scale(float S)
        {
            PointC q1 = Geometry.Scale(this.p1, S, S);
            PointC q2 = Geometry.Scale(this.p2, S, S);
            this.update(q1, q2);
        }
        public void Scale(float Sx, float Sy)
        {
            PointC q1 = Geometry.Scale(this.p1, Sx, Sy);
            PointC q2 = Geometry.Scale(this.p2, Sx, Sy);
            this.update(q1, q2);
        }
        public void Scale(PointC a, float S)
        {
            PointC q1 = Geometry.Scale(this.p1, a, S, S);
            PointC q2 = Geometry.Scale(this.p2, a, S, S);
            this.update(q1, q2);
        }
        public void Scale(PointC a, float Sx, float Sy)
        {
            PointC q1 = Geometry.Scale(this.p1, a, Sx, Sy);
            PointC q2 = Geometry.Scale(this.p2, a, Sx, Sy);
            this.update(q1, q2);
        }

        public void Translate(float T)
        {
            PointC q1 = Geometry.Translate(this.p1, T, T);
            PointC q2 = Geometry.Translate(this.p2, T, T);
            this.update(q1, q2);
        }
        public void Translate(float Tx, float Ty)
        {
            PointC q1 = Geometry.Translate(this.p1, Tx, Ty);
            PointC q2 = Geometry.Translate(this.p2, Tx, Ty);
            this.update(q1, q2);
        }

        public void Rotate(float angle)
        {
            PointC q1 = Geometry.Rotation(this.p1, angle);
            PointC q2 = Geometry.Rotation(this.p2, angle);
            this.update(q1, q2);
        }
        public void Rotate(PointC a, float angle)
        {
            PointC q1 = Geometry.Rotation(this.p1, a, angle);
            PointC q2 = Geometry.Rotation(this.p2, a, angle);
            this.update(q1, q2);
        }

        #endregion
        /*===================================================================================================*/
    }
}
