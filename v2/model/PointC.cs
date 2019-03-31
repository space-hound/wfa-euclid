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
    public class PointC
    {
        private float x;
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

        private float y;
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


        private float getRo()
        {
            float dx = (float)Math.Pow(this.x, 2);
            float dy = (float)Math.Pow(this.y, 2);

            return (float)Math.Sqrt(dx + dy);
        }
        private void setRo(float val)
        {
            float th = this.getTheta();
            this.x = (float)(val * Math.Cos(th * Constants.RAD));
            this.y = (float)(val * Math.Sin(th * Constants.RAD));
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

        private float getTheta()
        {
            return (float)(Math.Atan2(this.y, this.x) * Constants.ANG);
        }
        private void setTheta(float val)
        {
            float ro = this.getRo();
            this.x = (float)(ro * Math.Cos(val * Constants.RAD));
            this.y = (float)(ro * Math.Sin(val * Constants.RAD));
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

        /************************************************************************************************
         * 
         *  CONSTRUCTOR();
         *  
         ************************************************************************************************/

        public PointC()
        {
            this.x = DrawingEngine.RNDX();
            this.y = DrawingEngine.RNDY();
        }
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
        public PointC(PointC p)
        {
            this.x = p.X;
            this.y = p.Y;
        }
        public PointC(VectorC v)
        {
            this.x = v.X;
            this.y = v.Y;
        }

        /************************************************************************************************
         * 
         *  METHODS();
         *  
         ************************************************************************************************/

        public float Dist(PointC p)
        {
            return (float)Math.Sqrt(Math.Pow((p.x - this.x), 2) + Math.Pow((p.y - this.y), 2));
        }
        public Boolean IsInsideMap()
        {
            return
                Mathematics.isIn(this.x, DrawingEngine.RANGEX()[0], DrawingEngine.RANGEX()[1])
                &&
                Mathematics.isIn(this.y, DrawingEngine.RANGEY()[0], DrawingEngine.RANGEY()[1]);
        }

        public Boolean IsBetween(PointC p1, PointC p2)
        {
            if (Mathematics.Equal(p1.x, p2.x))
            {
                if (Mathematics.isIn(this.y, p1.y, p2.y))
                {
                    return true;
                }
            }

            if (Mathematics.Equal(p1.y, p2.y))
            {
                if (Mathematics.isIn(this.x, p1.x, p2.x))
                {
                    return true;
                }
            }

            float l1 = (this.x - p1.x) / (p2.x - p1.x);
            float l2 = (this.y - p1.y) / (p2.y - p1.y);

            if (Mathematics.isIn(l1, 0, 1f) && Mathematics.isIn(l2, 0, 1f))
            {
                return true;
            }

            return false;
        }

        public PointC ReflectRel(PointC p, float times = 2.00f)
        {
            float x = this.x + times * (p.x - this.x);
            float y = this.y + times * (p.y - this.y);

            return new PointC(x, y);
        }
        public static List<PointC> PointsBetweenRel(PointC p1, PointC p2, float step = 0.05f)
        {
            List<PointC> ps = new List<PointC>();
            for (float i = 0; i < 1f; i += step)
            {
                ps.Add(p1.ReflectRel(p2, i));
            }
            return ps;
        }

        public PointC ReflectOrt(PointC p, float times = 50.00f)
        {
            VectorC v = new VectorC(this, p);
            VectorC dv = v.dV.Mul(times);

            return new PointC(this.x + dv.X, this.y + dv.Y);
        }
        public static List<PointC> PointsBetweenOrt(PointC p1, PointC p2, float step = 40)
        {
            List<PointC> ps = new List<PointC>();
            for (float i = step; i < p1.Dist(p2); i += step)
            {
                ps.Add(p1.ReflectOrt(p2, i));
            }
            return ps;
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
        public PointC ReflectRel(LineC l, float times = 2)
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
                ps.Add(p1.ReflectRel(p2, i));
            }

            return ps;
        }
        public static List<PointC> OrderedPointsBetweenOrtho(PointC p1, PointC p2, float step = 40)
        {
            List<PointC> ps = new List<PointC>();
            for (float i = step; i < p1.Dist(p2) - step; i += step)
            {
                ps.Add(p1.ReflectOrt(p2, i));
            }

            return ps;
        }

        /************************************************************************************************
         * 
         *  SCALE - TRANSLATE - ROTATE - SHEAR - REFLECT - ETC.
         *  
         ************************************************************************************************/

        private void update(PointC u)
        {
            this.x = u.x;
            this.y = u.y;
        }

        public PointC SCALE(float Sx, float Sy)
        {
            return new PointC(this.x * Sx, this.y * Sy);
        }
        public PointC SCALE(float S)
        {
            return this.SCALE(S, S);
        }
        public PointC SCALE(PointC u, float Sx, float Sy)
        {
            float newX = this.x * Sx + u.x * (1 - Sx),
                  newY = this.y * Sy + u.y * (1 - Sy);

            return new PointC(newX, newY);
        }
        public PointC SCALE(PointC u, float S)
        {
            return this.SCALE(u, S, S);
        }

        public void scale(float Sx, float Sy)
        {
            PointC temp = this.SCALE(Sx, Sy);
            this.update(temp);
        }
        public void scale(float S)
        {
            this.scale(S, S);
        }
        public void scale(PointC u, float Sx, float Sy)
        {
            PointC temp = this.SCALE(u, Sx, Sy);
            this.update(temp);
        }
        public void scale(PointC u, float S)
        {
            PointC temp = this.SCALE(u, S, S);
            this.update(temp);
        }

        public PointC TRANSLATE(float Tx, float Ty)
        {
            return new PointC(this.x + Tx, this.y + Ty);
        }
        public PointC TRANSLATE(float T)
        {
            return this.TRANSLATE(T, T);
        }

        public void translate(float Tx, float Ty)
        {
            PointC temp = this.TRANSLATE(Tx, Ty);
            this.update(temp);
        }
        public void translate(float T)
        {
            this.translate(T, T);
        }

        public PointC ROTATE(float angle)
        {
            float alfa = angle * Constants.RAD;

            float dx = (float)(this.x * Math.Cos(alfa) - this.y * Math.Sin(alfa));
            float dy = (float)(this.x * Math.Sin(alfa) + this.y * Math.Cos(alfa));

            return new PointC(dx, dy);
        }
        public PointC ROTATE(PointC u, float angle)
        {
            float alfa = angle * Constants.RAD;

            float dx = (float)(Math.Cos(alfa) * (this.x - u.x) + Math.Sin(alfa) * (-this.y + u.y) + u.x);
            float dy = (float)(Math.Cos(alfa) * (this.y - u.y) + Math.Sin(alfa) * (this.x - u.x) + u.y);

            return new PointC(dx, dy);
        }

        public void rotate(float angle)
        {
            PointC temp = this.ROTATE(angle);
            this.update(temp);
        }
        public void rotate(PointC u, float angle)
        {
            PointC temp = this.ROTATE(u, angle);
            this.update(temp);
        }

        public PointC SHEARX(float angle)
        {
            float alfa = angle * Constants.RAD;
            float dx = (float)(this.x + this.y * Math.Tan(alfa));
            float dy = this.y;

            return new PointC(dx, dy);
        }
        public PointC SHEARY(float angle)
        {
            float alfa = angle * Constants.RAD;
            float dx = this.x;
            float dy = (float)(Math.Tan(alfa) * this.x + this.y);

            return new PointC(dx, dy);
        }


        public void shearx(float angle)
        {
            PointC temp = this.SHEARX(angle);
            this.update(temp);
        }
        public void sheary(float angle)
        {
            PointC temp = this.SHEARY(angle);
            this.update(temp);
        }

        public PointC REFLECTX(float axis = 0)
        {
            return new PointC(this.x, (this.y * -1) + (2 * axis));
        }
        public PointC REFLECTY(float axis = 0)
        {
            return new PointC((this.x * -1) + (2 * axis), this.y);
        }

        public void reflectx(float axis = 0)
        {
            PointC temp = this.REFLECTX(axis);
            this.update(temp);
        }
        public void reflecty(float axis = 0)
        {
            PointC temp = this.REFLECTY(axis);
            this.update(temp);
        }


        /************************************************************************************************
         * 
         *  STRINGIFY():
         *  
         ************************************************************************************************/

        public override string ToString()
        {
            return this.P.ToString();
        }
        public string Stringify(int op = 0)
        {
            if (op == 1)
            {
                return "c[ " + this.x + " : " + this.y + " ]";
            }
            else
            {
                return "p[ " + this.RO + " : " + this.THETA + " ]";
            }
        }
    }
}
