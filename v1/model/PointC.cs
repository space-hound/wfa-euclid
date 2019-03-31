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
    public class PointC
    {
        /*===================================================================================================*/
        #region fields

        private float x;
        private float y;


        public static PointC[] BoundingPoints()
        {
            float[] rngX = Drawing.rangeX();
            float[] rngY = Drawing.rangeY();

            PointC[] BP = new PointC[5];

            BP[0] = new PointC(rngX[0], rngY[0]);
            BP[1] = new PointC(rngX[1], rngY[0]);

            BP[2] = new PointC(rngX[0], rngY[1]);
            BP[3] = new PointC(rngX[1], rngY[1]);

            BP[4] = new PointC(rngX[1] / 2, rngY[1] / 2);

            return BP;
        }

        #endregion
        /*===================================================================================================*/
        #region properties

        public float X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public float Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public VectorC V
        {
            get
            {
                return new VectorC(this.x, this.y);
            }
        }
        public PointF P
        {
            get
            {
                return new PointF(this.x, this.y);
            }
        }

        public float D
        {
            get
            {
                return this.getRo();
            }
        }

        private float getRo()
        {
            float dx = (float)Math.Pow(this.x, 2);
            float dy = (float)Math.Pow(this.y, 2);

            return (float)Math.Sqrt(dx + dy);
        }
        private float getTheta()
        {
            return (float)(Math.Atan2(this.y, this.x) * Constants.ANG);
        }
        private void setRo(float val)
        {
            float th = this.getTheta();
            this.x = (float)(val * Math.Cos(th * Constants.RAD));
            this.y = (float)(val * Math.Sin(th * Constants.RAD));
        }
        private void setTheta(float val)
        {
            float ro = this.getRo();
            this.x = (float)(ro * Math.Cos(val * Constants.RAD));
            this.y = (float)(ro * Math.Sin(val * Constants.RAD));
        }

        public float RO
        {
            get
            {
                return this.getRo();
            }
            set
            {
                this.setRo(value);
            }
        }
        public float THETA
        {
            get
            {
                return this.getTheta();
            }
            set
            {
                this.setTheta(value);
            }
        }

        #endregion
        /*===================================================================================================*/
        #region constructors

        public PointC(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public PointC(PointF p)
        {
            this.x = p.X;
            this.y = p.Y;
        }

        public PointC(VectorC v)
        {
            this.x = v.X;
            this.y = v.Y;
        }

        public PointC()
        {
            this.x = Drawing.rx();
            this.y = Drawing.ry();
        }

        #endregion
        /*===================================================================================================*/
        #region methods

        public float Dist(PointC p)
        {
            return (float)Math.Sqrt(Math.Pow((p.x - this.x), 2) + Math.Pow((p.y - this.y), 2));
        }
        public Boolean IsInsideMap()
        {
            float[] rngX = Drawing.rangeX();
            float[] rngY = Drawing.rangeY();

            Boolean c1 = Mathematics.isIn(this.x, rngX[0], rngX[1]);
            Boolean c2 = Mathematics.isIn(this.y, rngY[0], rngY[1]);

            return c1 && c2;
        }
        
        public Boolean IsBetween(PointC p1, PointC p2)
        {
            if(Mathematics.Equal(p1.x, p2.x))
            {
                if(Mathematics.isIn(this.y, p1.y, p2.y))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (Mathematics.Equal(p1.y, p2.y))
            {
                if (Mathematics.isIn(this.x, p1.x, p2.x))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            float l1 = (this.x - p1.x) / (p2.x - p1.x);
            float l2 = (this.y - p1.y) / (p2.y - p1.y);

            if (Mathematics.isIn(l1, 0, 0.999f) && Mathematics.isIn(l2, 0, 0.999f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public PointC Reflect(PointC p, float times = 2.00f)
        {
            float x = this.x + times * (p.x - this.x);
            float y = this.y + times * (p.y - this.y);

            return new PointC(x, y);
        }
        public PointC ReflectOrtho(PointC p, float times = 50.00f)
        {
            VectorC v = new VectorC(this, p);
            VectorC dv = v.dV.Mul(times);

            return new PointC(this.x + dv.X, this.y + dv.Y);
        }

        public Boolean IsOnTheLine(LineC l)
        {
            float[] eq = l.GeneralEq;
            float test = eq[0] * this.x + eq[1] * this.y + eq[2];

            return Mathematics.IsZero(test);
        }
        public Boolean IsOnSeg(SegmentC s)
        {
            return this.IsBetween(s.P1, s.P2);
        }
        public float Dist(LineC l)
        {
            VectorC n = l.nV;
            float n_n = n.Dot(n);
            VectorC p = this.V;
            float n_p = n.Dot(p);

            float lambda = -(n_p + l.GeneralEq[2]) / (n_n);

            VectorC lambda_n = n.Mul(lambda);

            return lambda_n.M;
        }
        public PointC Reflect(LineC l, float times = 2)
        {
            VectorC n = l.nV;
            float n_n = n.Dot(n);
            VectorC p = this.V;
            float n_p = n.Dot(p);

            float lambda = times * (n_p + l.GeneralEq[2]) / (n_n);

            VectorC lambda_n = n.Mul(lambda);
            VectorC ptPrim = p.Sub(lambda_n);

            return new PointC(ptPrim.X, ptPrim.Y);
        }


        public static List<PointC> RandomPoints(int no = 40)
        {
            List<PointC> ps = new List<PointC>();
            for (int i = 0; i < no; i++)
            {
                ps.Add(new PointC());
            }

            return ps;
        }
        public static List<PointC> OrderedPointsBetween(PointC p1, PointC p2, float step = 0.05f)
        {
            List<PointC> ps = new List<PointC>();
            for (float i = 0; i < 1.01; i += step)
            {
                ps.Add(p1.Reflect(p2, i));
            }

            return ps;
        }
        public static List<PointC> OrderedPointsBetweenOrtho(PointC p1, PointC p2, float step = 40)
        {
            List<PointC> ps = new List<PointC>();
            for (float i = step; i < p1.Dist(p2) - step; i += step)
            {
                ps.Add(p1.ReflectOrtho(p2, i));
            }

            return ps;
        }

        #endregion
        /*===================================================================================================*/
        #region trsts

        private void update(PointC point)
        {
            this.x = point.X;
            this.y = point.Y;
        }

        /* scalation */
        public void Scale(float S)
        {
            PointC pt = Geometry.Scale(this, S, S);
            this.update(pt);
        }
        public void Scale(float Sx, float Sy)
        {
            PointC pt = Geometry.Scale(this, Sx, Sy);
            this.update(pt);
        }
        public void Scale(PointC a, float S)
        {
            PointC pt = Geometry.Scale(this, a, S, S);
            this.update(pt);
        }
        public void Scale(PointC a, float Sx, float Sy)
        {
            PointC pt = Geometry.Scale(this, a, Sx, Sy);
            this.update(pt);
        }

        public void Translate(float T)
        {
            PointC pt = Geometry.Translate(this, T, T);
            this.update(pt);
        }
        public void Translate(float Tx, float Ty)
        {
            PointC pt = Geometry.Translate(this, Tx, Ty);
            this.update(pt);
        }

        public void Rotate(float angle)
        {
            PointC pt = Geometry.Rotation(this, angle);
            this.update(pt);
        }
        public void Rotate(PointC a, float angle)
        {
            PointC pt = Geometry.Rotation(this, a, angle);
            this.update(pt);
        }

        public void ShearX(float angle)
        {
            PointC pt = Geometry.ShearX(this, angle);
            this.update(pt);
        }
        public void ShearY(float angle)
        {
            PointC pt = Geometry.ShearY(this, angle);
            this.update(pt);
        }

        public void ReflectX()
        {
            PointC pt = Geometry.ReflectX(this);
            this.update(pt);
        }
        public void ReflectY()
        {
            PointC pt = Geometry.ReflectY(this);
            this.update(pt);
        }
        public void ReflectPX(float axis)
        {
            PointC pt = Geometry.ReflectPX(this, axis);
            this.update(pt);
        }
        public void ReflectPY(float axis)
        {
            PointC pt = Geometry.ReflectPY(this, axis);
            this.update(pt);
        }

        public void TChangeAxis(float Tx, float Ty)
        {
            PointC pt = Geometry.TChangeAxis(this, Tx, Ty);
            this.update(pt);

        }
        public void RChangeAxis(float angle)
        {
            PointC pt = Geometry.RChangeAxis(this, angle);
            this.update(pt);
        }

        #endregion
        /*===================================================================================================*/
        #region stringify

        public override string ToString()
        {
            return this.P.ToString();
        }

        public string Stringify(int op = 0)
        {
            if(op == 1)
            {
                return "c[ " + this.x + " : " + this.y + " ]";
            }
            else
            {
                return "p[ " + this.RO + " : " + this.THETA + " ]";
            }
        }

        #endregion
        /*===================================================================================================*/
    }
}
