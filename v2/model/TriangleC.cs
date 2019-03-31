﻿using System;
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
    public class TriangleC
    {
        /*===================================================================================================*/
        #region fields

        private PointC a;
        private PointC b;
        private PointC c;

        #endregion
        /*===================================================================================================*/
        #region properties

        public PointC A
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
            }
        }
        public PointC B
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = value;
            }
        }
        public PointC C
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
            }
        }

        private SegmentC[] getSides()
        {
            SegmentC[] sides = new SegmentC[3];
            sides[0] = new SegmentC(this.b, this.c);//a
            sides[1] = new SegmentC(this.c, this.a);//b
            sides[2] = new SegmentC(this.a, this.b);//c

            return sides;
        }
        public SegmentC[] Sides
        {
            get
            {
                return this.getSides();
            }
        }

        private float[] getLengths()
        {
            float[] lengths = new float[3];
            lengths[0] = this.b.Dist(this.c);//a
            lengths[1] = this.c.Dist(this.a);//b
            lengths[2] = this.a.Dist(this.b);//c

            return lengths;
        }
        public float[] Lengths
        {
            get
            {
                return this.getLengths();
            }
        }

        private float[] getSizes()
        {
            float[] s = new float[3];
            float[] l = this.getLengths();
            s[0] = (l[0] + l[1] + l[2]) / 2;
            s[1] = l[0] + l[1] + l[2];
            s[2] = (float)(Math.Sqrt(s[0] * (s[0] - l[0]) * (s[0] - l[1]) * (s[0] - l[2])));

            return s;
        }
        public float[] Sizes
        {
            get
            {
                return this.getSizes();
            }
        }

        private float[] getAngles()
        {
            float[] l = this.getLengths();
            SegmentC[] sd = this.getSides();
            float[] ang = new float[3];
            //ang[0] = (float)Math.Acos((l[1] * l[1] + l[2] * l[2] - l[0] * l[0]) / (2 * l[1] * l[2])) * Constants.ANG;
            //ang[1] = (float)Math.Acos((l[0] * l[0] + l[2] * l[2] - l[1] * l[1]) / (2 * l[1] * l[2])) * Constants.ANG;
            //ang[2] = (float)Math.Acos((l[0] * l[0] + l[1] * l[1] - l[2] * l[2]) / (2 * l[0] * l[1])) * Constants.ANG;
            ang[0] = (float)Math.Acos(-(sd[1].L.nV.Dot(sd[2].L.nV) / (sd[1].L.nV.M * sd[2].L.nV.M))) * Constants.ANG;
            ang[1] = (float)Math.Acos(-(sd[2].L.nV.Dot(sd[0].L.nV) / (sd[2].L.nV.M * sd[0].L.nV.M))) * Constants.ANG;
            ang[2] = (float)Math.Acos(-(sd[0].L.nV.Dot(sd[1].L.nV) / (sd[0].L.nV.M * sd[1].L.nV.M))) * Constants.ANG;

            return ang;
        }
        public float[] Angles
        {
            get
            {
                return this.getAngles();
            }
        }

        private SegmentC[] getAltitudes()
        {
            SegmentC[] alts = new SegmentC[3];
            SegmentC[] s = this.getSides();
            PointC[] hb = new PointC[3];

            hb[0] = this.a.ReflectRel(s[0].L, 1);
            hb[1] = this.b.ReflectRel(s[1].L, 1);
            hb[2] = this.c.ReflectRel(s[2].L, 1);

            LineC[] hl = new LineC[3];
            hl[0] = new LineC(this.a, hb[0]);
            hl[1] = new LineC(this.b, hb[1]);
            hl[2] = new LineC(this.c, hb[2]);

            float x = 0, y = 0;
            PointC ortho;
            int ct = 0;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    PointC p = hl[i].Intersection(hl[j]);

                    if (p != null)
                    {
                        x += p.X;
                        y += p.Y;
                        ct++;
                    }
                }
            }

            ortho = new PointC(x / ct, y / ct);

            if (!hb[0].IsOnSeg(s[0]) && !hb[1].IsOnSeg(s[1]) && !hb[2].IsOnSeg(s[2]))
            {

                alts[0] = new SegmentC(ortho, this.a);
                alts[1] = new SegmentC(ortho, this.b);
                alts[2] = new SegmentC(ortho, this.c);

            }
            else if (!hb[0].IsOnSeg(s[0]) && hb[1].IsOnSeg(s[1]) && hb[2].IsOnSeg(s[2]))
            {
                alts[0] = new SegmentC(ortho, this.a);
                alts[1] = new SegmentC(hb[1], ortho);
                alts[2] = new SegmentC(hb[2], ortho);
            }
            else if (hb[0].IsOnSeg(s[0]) && !hb[1].IsOnSeg(s[1]) && hb[2].IsOnSeg(s[2]))
            {
                alts[1] = new SegmentC(ortho, this.b);
                alts[0] = new SegmentC(hb[0], ortho);
                alts[2] = new SegmentC(hb[2], ortho);
            }
            else if (hb[0].IsOnSeg(s[0]) && hb[1].IsOnSeg(s[1]) && !hb[2].IsOnSeg(s[2]))
            {
                alts[2] = new SegmentC(ortho, this.c);
                alts[0] = new SegmentC(hb[0], ortho);
                alts[1] = new SegmentC(hb[1], ortho);
            }
            else if (!hb[0].IsOnSeg(s[0]) && !hb[1].IsOnSeg(s[1]) && hb[2].IsOnSeg(s[2]))
            {
                alts[0] = new SegmentC(ortho, this.a);
                alts[1] = new SegmentC(ortho, this.b);
                alts[2] = new SegmentC(hb[2], ortho);
            }
            else if (hb[0].IsOnSeg(s[0]) && !hb[1].IsOnSeg(s[1]) && !hb[2].IsOnSeg(s[2]))
            {
                alts[1] = new SegmentC(ortho, this.b);
                alts[2] = new SegmentC(ortho, this.c);
                alts[0] = new SegmentC(hb[0], ortho);
            }
            else if (!hb[0].IsOnSeg(s[0]) && hb[1].IsOnSeg(s[1]) && !hb[2].IsOnSeg(s[2]))
            {
                alts[0] = new SegmentC(ortho, this.a);
                alts[2] = new SegmentC(ortho, this.c);
                alts[1] = new SegmentC(hb[1], ortho);
            }
            else
            {
                alts[0] = new SegmentC(hb[0], this.a);
                alts[1] = new SegmentC(hb[1], this.b);
                alts[2] = new SegmentC(hb[2], this.c);
            }

            return alts;
        }
        private PointC getOrtho()
        {
            SegmentC[] o = this.getAltitudes();

            float x = 0, y = 0, ct = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    PointC ip = o[i].L.Intersection(o[j].L);

                    if (ip != null)
                    {
                        x += ip.X;
                        y += ip.Y;
                        ct++;
                    }
                }
            }

            return new PointC(x / ct, y / ct);
        }
        public SegmentC[] Altitudes
        {
            get
            {
                return this.getAltitudes();
            }
        }
        public PointC Orthocenter
        {
            get
            {
                return this.getOrtho();
            }
        }

        private SegmentC[] getMedians()
        {
            SegmentC[] s = this.getSides();
            SegmentC[] m = new SegmentC[3];
            m[0] = new SegmentC(this.a, s[0].Mid);
            m[1] = new SegmentC(this.b, s[1].Mid);
            m[2] = new SegmentC(this.c, s[2].Mid);

            return m;
        }
        private PointC getCentroid()
        {
            SegmentC[] m = this.getMedians();

            float x = 0, y = 0, ct = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    PointC ip = m[i].L.Intersection(m[j].L);

                    if (ip != null)
                    {
                        x += ip.X;
                        y += ip.Y;
                        ct++;
                    }
                }
            }

            return new PointC(x / ct, y / ct);
        }
        public SegmentC[] Medians
        {
            get
            {
                return this.getMedians();
            }
        }
        public PointC Centroid
        {
            get
            {
                return this.getCentroid();
            }
        }

        private SegmentC[] getMediators()
        {
            SegmentC[] meds = new SegmentC[3];
            SegmentC[] s = this.getSides();

            LineC t = s[0].L.ParallelThrough(this.a);
            meds[0] = new SegmentC(s[0].Mid, s[0].Mid.ReflectRel(t, 1));

            t = s[1].L.ParallelThrough(this.b);
            meds[1] = new SegmentC(s[1].Mid, s[1].Mid.ReflectRel(t, 1));

            t = s[2].L.ParallelThrough(this.c);
            meds[2] = new SegmentC(s[2].Mid, s[2].Mid.ReflectRel(t, 1));

            return meds;
        }
        private EllipseC getOutCircle()
        {
            SegmentC[] me = this.getMediators();

            float x = 0, y = 0, ct = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    PointC ip = me[i].L.Intersection(me[j].L);

                    if (ip != null)
                    {
                        x += ip.X;
                        y += ip.Y;
                        ct++;
                    }
                }
            }

            PointC c = new PointC(x / ct, y / ct);
            float[] sz = this.getSizes();
            float[] le = this.getLengths();

            float D = (le[0] * le[1] * le[2]) / (2 * sz[2]);

            return new EllipseC(c, D);
        }
        public SegmentC[] Mediators
        {
            get
            {
                return this.getMediators();
            }
        }
        public EllipseC OutC
        {
            get
            {
                return this.getOutCircle();
            }
        }

        private EllipseC getInCircle()
        {
            float[] sz = this.getSizes();
            float[] le = this.getLengths();

            float x = (le[0] * this.a.X + le[1] * this.b.X + le[2] * this.c.X) / (2 * sz[0]);
            float y = (le[0] * this.a.Y + le[1] * this.b.Y + le[2] * this.c.Y) / (2 * sz[0]);

            //float D = (le[0] * le[1] * le[2]) / (4 * sz[2]);
            float D = (sz[2] / sz[0]) * 2;
            //float D = new SegmentC(new PointC(x, y), this.getSides()[0].Mid).Length;

            return new EllipseC(new PointC(x, y), D);
        }
        private SegmentC[] getBisectors()
        {
            EllipseC nc = this.getInCircle();
            //SegmentC[] sd = this.getSides();

            SegmentC[] bis = new SegmentC[3];
            bis[0] = new SegmentC(this.a, nc.C);
            bis[1] = new SegmentC(this.b, nc.C);
            bis[2] = new SegmentC(this.c, nc.C);

            return bis;
        }
        public SegmentC[] Bisectors
        {
            get
            {
                return this.getBisectors();
            }
        }
        public EllipseC InC
        {
            get
            {
                return this.getInCircle();
            }
        }

        private SegmentC[] getMids()
        {
            SegmentC[] ms = new SegmentC[3];
            SegmentC[] sd = this.getSides();

            ms[0] = new SegmentC(sd[1].Mid, sd[2].Mid);
            ms[1] = new SegmentC(sd[2].Mid, sd[0].Mid);
            ms[2] = new SegmentC(sd[0].Mid, sd[1].Mid);

            return ms;
        }
        public SegmentC[] Mids
        {
            get
            {
                return this.getMids();
            }
        }


        #endregion
        /*===================================================================================================*/
        #region constructors

        public TriangleC(PointC a, PointC b, PointC c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public TriangleC()
        {
            this.a = new PointC();
            this.b = new PointC();
            this.c = new PointC();
        }

        #endregion
        /*===================================================================================================*/
        #region methods

        public static List<TriangleC> RandomTriangles(int no = 8)
        {
            List<TriangleC> ts = new List<TriangleC>();
            for (int i = 0; i < no; i++)
            {
                ts.Add(new TriangleC());
            }

            return ts;
        }


        #endregion
        /*===================================================================================================*/

        private void update(TriangleC t)
        {
            this.a = t.a;
            this.b = t.b;
            this.c = t.c;
        }

        public TriangleC SCALE(float Sx, float Sy)
        {
            return new TriangleC(this.a.SCALE(Sx, Sy), this.b.SCALE(Sx, Sy), this.c.SCALE(Sx, Sy));
        }
        public TriangleC SCALE(float S)
        {
            return this.SCALE(S, S);
        }
        public TriangleC SCALE(PointC p, float Sx, float Sy)
        {
            return new TriangleC(this.a.SCALE(p, Sx, Sy), this.b.SCALE(p, Sx, Sy), this.c.SCALE(p, Sx, Sy));
        }
        public TriangleC SCALE(PointC p, float S)
        {
            return this.SCALE(p, S, S);
        }

        public void scale(float Sx, float Sy)
        {
            TriangleC temp = this.SCALE(Sx, Sy);
            this.update(temp);
        }
        public void scale(float S)
        {
            this.scale(S, S);
        }
        public void scale(PointC p, float Sx, float Sy)
        {
            TriangleC temp = this.SCALE(p, Sx, Sy);
            this.update(temp);
        }
        public void scale(PointC p, float S)
        {
            this.scale(p, S, S);
        }



        public TriangleC TRANSLATE(float Tx, float Ty)
        {
            return new TriangleC(this.a.TRANSLATE(Tx, Ty), this.b.TRANSLATE(Tx, Ty), this.c.TRANSLATE(Tx, Ty));
        }
        public TriangleC TRANSLATE(float T)
        {
            return this.TRANSLATE(T, T);
        }

        public void translate(float Tx, float Ty)
        {
            TriangleC temp = this.TRANSLATE(Tx, Ty);
            this.update(temp);
        }
        public void translate(float T)
        {
            this.translate(T, T);
        }

        public TriangleC ROTATE(float angle)
        {
            return new TriangleC(this.a.ROTATE(angle), this.b.ROTATE(angle), this.c.ROTATE(angle));
        }
        public TriangleC ROTATE(PointC p, float angle)
        {
            return new TriangleC(this.a.ROTATE(p, angle), this.b.ROTATE(p, angle), this.c.ROTATE(p, angle));
        }

        public void rotate(float angle)
        {
            TriangleC temp = this.ROTATE(angle);
            this.update(temp);
        }
        public void rotate(PointC p, float angle)
        {
            TriangleC temp = this.ROTATE(p, angle);
            this.update(temp);
        }

    }
}
