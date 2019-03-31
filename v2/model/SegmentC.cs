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
    public class SegmentC
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

        public PointC Mid
        {
            get
            {
                return this.getMid();
            }
        }
        private PointC getMid()
        {
            float x = this.p1.X + this.p2.X;
            float y = this.p1.Y + this.p2.Y;

            return new PointC(x / 2, y / 2);
        }

        public float Length
        {
            get
            {
                return this.p1.Dist(this.p2);
            }
            set
            {
                this.setLength(value);
            }
        }
        private void setLength(float val)
        {
            PointC q1 = this.getMid().ReflectOrt(this.p1, val / 2);
            PointC q2 = this.getMid().ReflectOrt(this.p2, val / 2);

            this.p1 = q1;
            this.p2 = q2;
        }

        public VectorC V
        {
            get
            {
                return new VectorC(this.p1, this.p2);
            }
        }
        public VectorC pV
        {
            get
            {
                return new VectorC(this.getMid());
            }
        }
        public LineC L
        {
            get
            {
                return new LineC(this.p1, this.p2);
            }
        }

        #endregion
        /*===================================================================================================*/
        #region constructors

        public SegmentC(PointC p1, PointC p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public SegmentC()
        {
            this.p1 = new PointC();
            this.p2 = new PointC();
        }

        #endregion
        /*===================================================================================================*/
        #region methods

        public Boolean Contains(PointC p)
        {
            return p.IsBetween(this.p1, this.p2);
        }

        public PointC Intersection(SegmentC s)
        {
            PointC ip = this.L.Intersection(s.L);

            if (ip != null)
            {
                if (this.Contains(ip) && s.Contains(ip))
                {
                    return ip;
                }
            }

            return null;
        }
        public PointC Intersection(LineC l)
        {
            PointC ip = this.L.Intersection(l);

            if (ip != null)
            {
                if (this.Contains(ip))
                {
                    return ip;
                }
            }

            return null;
        }

        public List<PointC> Intersection(List<SegmentC> ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ss.Count; i++)
            {
                PointC p = this.Intersection(ss[i]);
                if (p != null)
                {
                    ps.Add(p);
                }
            }

            return ps;
        }
        public List<PointC> Intersection(SegmentC[] ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ss.Length; i++)
            {
                PointC p = this.Intersection(ss[i]);
                if (p != null)
                {
                    ps.Add(p);
                }
            }

            return ps;
        }
        public List<PointC> Intersection(List<LineC> ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ss.Count; i++)
            {
                PointC p = this.Intersection(ss[i]);
                if (p != null)
                {
                    ps.Add(p);
                }
            }

            return ps;
        }
        public List<PointC> Intersection(LineC[] ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ss.Length; i++)
            {
                PointC p = this.Intersection(ss[i]);
                if (p != null)
                {
                    ps.Add(p);
                }
            }

            return ps;
        }

        public PointC PointX(float st = 0.5f)
        {
            float x = this.p1.X + st * (this.p2.X - this.p1.X);
            float y = this.p1.Y + st * (this.p2.Y - this.p1.Y);

            return new PointC(x, y);
        }

        public SegmentC Reflect(LineC l, float times = 2f)
        {
            PointC q1 = this.p1.ReflectRel(l, times);
            PointC q2 = this.p2.ReflectRel(l, times);

            return new SegmentC(q1, q2);
        }

        public static List<SegmentC> RandomSegments(int no = 20)
        {
            List<SegmentC> ss = new List<SegmentC>();

            for (int i = 0; i < no; i++)
            {
                ss.Add(new SegmentC());
            }

            return ss;
        }
        public static List<PointC> Intersections(List<SegmentC> ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ss.Count - 1; i++)
            {
                for (int j = 1; j < ss.Count; j++)
                {
                    PointC p = ss[i].Intersection(ss[j]);

                    if (p != null)
                    {
                        ps.Add(p);
                    }
                }
            }

            return ps;
        }
        public static List<PointC> Intersections(List<LineC> ls, List<SegmentC> ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Count; i++)
            {
                for (int j = 0; j < ss.Count; j++)
                {
                    PointC p = ss[j].Intersection(ls[i]);

                    if (p != null)
                    {
                        ps.Add(p);
                    }
                }
            }

            for (int i = 0; i < ss.Count - 1; i++)
            {
                for (int j = 1; j < ss.Count; j++)
                {
                    PointC p = ss[i].Intersection(ss[j]);

                    if (p != null)
                    {
                        ps.Add(p);
                    }
                }
            }

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
        public static List<PointC> Intersections(List<SegmentC> ls, List<SegmentC> ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Count - 1; i++)
            {
                for (int j = 1; j < ss.Count; j++)
                {
                    PointC p = ss[i].Intersection(ls[j]);

                    if (p != null)
                    {
                        ps.Add(p);
                    }
                }
            }

            return ps;
        }
        public static List<PointC> Intersections(SegmentC[] ls, SegmentC[] ss)
        {
            List<PointC> ps = new List<PointC>();

            for (int i = 0; i < ls.Length - 1; i++)
            {
                for (int j = 1; j < ss.Length; j++)
                {
                    PointC p = ss[i].Intersection(ls[j]);

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

        private void update(SegmentC temp)
        {
            this.p1 = temp.p1;
            this.p2 = temp.p2;
        }

        public SegmentC SCALE(float Sx, float Sy)
        {
            return new SegmentC(this.p1.SCALE(Sx, Sy), this.p2.SCALE(Sx, Sy));
        }
        public SegmentC SCALE(float S)
        {
            return this.SCALE(S, S);
        }

        public void scale(float Sx, float Sy)
        {
            SegmentC temp = this.SCALE(Sx, Sy);
            this.update(temp);
        }
        public void scale(float S)
        {
            this.scale(S, S);
        }


        public SegmentC TRANSLATE(float Tx, float Ty)
        {
            return new SegmentC(this.p1.TRANSLATE(Tx, Ty), this.p2.TRANSLATE(Tx, Ty));
        }
        public SegmentC TRANSLATE(float T)
        {
            return this.TRANSLATE(T, T);
        }

        public void translate(float Tx, float Ty)
        {
            SegmentC temp = this.TRANSLATE(Tx, Ty);
            this.update(temp);
        }
        public void translate(float T)
        {
            this.translate(T, T);
        }

        public SegmentC ROTATE(float angle)
        {
            return new SegmentC(this.p1.ROTATE(angle), this.p2.ROTATE(angle));
        }
        public SegmentC ROTATE(PointC p, float angle)
        {
            return new SegmentC(this.p1.ROTATE(p, angle), this.p2.ROTATE(p, angle));
        }

        public void rotate(float angle)
        {
            SegmentC temp = this.ROTATE(angle);
            this.update(temp);
        }
        public void rotate(PointC p, float angle)
        {
            SegmentC temp = this.ROTATE(p, angle);
            this.update(temp);
        }

        #endregion
        /*===================================================================================================*/
    }
}
