using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Primitives2d.maths;
using Primitives2d.draws;



namespace Primitives2d.model
{
    public class LineC
    {
        /*===================================================================================================*/
        #region fields

        private PointC p1;
        private PointC p2;

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
                y[0] = DrawingEngine.RNDY();
                y[1] = DrawingEngine.RNDY();
            }
            else if (Mathematics.IsZero(m))
            {
                x[0] = DrawingEngine.RNDX();
                x[1] = DrawingEngine.RNDX();
                y[0] = b;
                y[1] = b;

            }
            else
            {
                x[0] = DrawingEngine.RNDX();
                y[0] = m * x[0] + b;

                x[1] = DrawingEngine.RNDX();
                y[1] = m * x[1] + b;
            }

            LineC l = new LineC(new PointC(x[0], y[0]), new PointC(x[1], y[1]));

            LineC[] BL = DrawingEngine.ExtremeLines();

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
                x[0] = DrawingEngine.RNDX();
                x[1] = DrawingEngine.RNDX();
                y[0] = -c / b;
                y[1] = -c / b;
            }
            else if (Mathematics.IsZero(b))
            {
                x[0] = -c / a;
                x[1] = -c / a;
                y[0] = DrawingEngine.RNDY();
                y[1] = DrawingEngine.RNDY();
            }
            else
            {
                x[0] = DrawingEngine.RNDX();
                y[0] = (-a * x[0] - c) / b;
                x[1] = DrawingEngine.RNDX();
                y[1] = (-a * x[1] - c) / b;
            }

            LineC l = new LineC(new PointC(x[0], y[0]), new PointC(x[1], y[1]));

            LineC[] BL = DrawingEngine.ExtremeLines();

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

        public LineC(float x0, float y0, float x1, float y1)
        {
            this.p1 = new PointC(x0, y0);
            this.p2 = new PointC(x1, y1);
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

                    if (p != null)
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

        private void update(LineC temp)
        {
            this.p1 = temp.p1;
            this.p2 = temp.p2;
        }

        public LineC SCALE(float Sx, float Sy)
        {
            return new LineC(this.p1.SCALE(Sx, Sy), this.p2.SCALE(Sx, Sy));
        }
        public LineC SCALE(float S)
        {
            return this.SCALE(S, S);
        }

        public void scale(float Sx, float Sy)
        {
            LineC temp = this.SCALE(Sx, Sy);
            this.update(temp);
        }
        public void scale(float S)
        {
            this.scale(S, S);
        }


        public LineC TRANSLATE(float Tx, float Ty)
        {
            return new LineC(this.p1.TRANSLATE(Tx, Ty), this.p2.TRANSLATE(Tx, Ty));
        }
        public LineC TRANSLATE(float T)
        {
            return this.TRANSLATE(T, T);
        }

        public void translate(float Tx, float Ty)
        {
            LineC temp = this.TRANSLATE(Tx, Ty);
            this.update(temp);
        }
        public void translate(float T)
        {
            this.translate(T, T);
        }

        public LineC ROTATE(float angle)
        {
            return new LineC(this.p1.ROTATE(angle), this.p2.ROTATE(angle));
        }
        public LineC ROTATE(PointC p, float angle)
        {
            return new LineC(this.p1.ROTATE(p, angle), this.p2.ROTATE(p, angle));
        }

        public void rotate(float angle)
        {
            LineC temp = this.ROTATE(angle);
            this.update(temp);
        }
        public void rotate(PointC p, float angle)
        {
            LineC temp = this.ROTATE(p, angle);
            this.update(temp);
        }
        #endregion
        /*===================================================================================================*/
    }
}
