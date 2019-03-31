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
            PointC q1 = this.getMid().ReflectOrtho(this.p1, val / 2);
            PointC q2 = this.getMid().ReflectOrtho(this.p2, val / 2);

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

            if(ip != null)
            {
                if(this.Contains(ip) && s.Contains(ip))
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

        public PointC PointX(float st= 0.5f)
        {
            float x = this.p1.X + st * (this.p2.X - this.p1.X);
            float y = this.p1.Y + st * (this.p2.Y - this.p1.Y);

            return new PointC(x, y);
        }
        public SegmentC Reflect(LineC l, float times = 2f)
        {
            PointC q1 = this.p1.Reflect(l, times);
            PointC q2 = this.p2.Reflect(l, times);

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
