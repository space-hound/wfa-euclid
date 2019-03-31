using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Primitives2d.model;
using Primitives2d.maths;


namespace Primitives2d.draws
{
    public class DrawingEngine
    {
        private static DrawingEngine instance = null;

        private static PictureBox pb;
        public PictureBox PB
        {
            get
            {
                return pb;
            }
        }
        private static int width, height;
        public int W
        {
            get
            {
                return width;
            }
        }
        public int H
        {
            get
            {
                return height;
            }
        }

        private static Bitmap bmp;
        public Bitmap BMP
        {
            get
            {
                return bmp;
            }
        }
        private static Graphics grp;
        public Graphics GRP
        {
            get
            {
                return grp;
            }
        }

        private DrawingEngine(PictureBox _pb)
        {
            pb = _pb;
            width = pb.Width; height = pb.Height;

            bmp = new Bitmap(width, height);
            grp = Graphics.FromImage(bmp);
        }
        public static DrawingEngine Load(PictureBox _pb)
        {
            if(instance == null)
            {
                instance = new DrawingEngine(_pb);
            }

            return instance;
        }

        public void R()
        {
            pb.Image = bmp;
        }
        public void C()
        {
            grp.Clear(pb.BackColor);
            pb.Image = bmp;
        }

        public void Point(PointC a)
        {
            grp.FillEllipse(new SolidBrush(Color.Purple), a.X - 5, a.Y - 5, 10, 10);
            grp.DrawEllipse(new Pen(Color.Black), a.X - 5, a.Y - 5, 10, 10);
            this.R();;
        }


        private static int XOffSet = 50, YOffSet = 50;
        public void ChangeOffSet(int x, int y)
        {
            XOffSet = x;
            YOffSet = y;
        }
        public static PointC[] ExtremePoints()
        {
            PointC[] ps = new PointC[4];

            ps[0] = new PointC(XOffSet, YOffSet);
            ps[1] = new PointC(width - XOffSet, YOffSet);
            ps[2] = new PointC(width - XOffSet, height - YOffSet);
            ps[3] = new PointC(XOffSet, height - YOffSet);

            return ps;
        }
        public static LineC[] ExtremeLines()
        {
            LineC[] ls = new LineC[4];
            PointC[] ps = ExtremePoints();

            ls[0] = new LineC(ps[0], ps[1]);
            ls[1] = new LineC(ps[1], ps[2]);
            ls[2] = new LineC(ps[2], ps[3]);
            ls[3] = new LineC(ps[3], ps[0]);

            return ls;
        }

        public static float RNDX()
        {
            return Mathematics.RandomFloat(XOffSet, width - XOffSet);
        }
        public static float RNDY()
        {
            return Mathematics.RandomFloat(YOffSet, height - YOffSet);
        }
        public static VectorC RNDV()
        {
            float x = Mathematics.RandomFloat(XOffSet, width - XOffSet);
            float y = Mathematics.RandomFloat(XOffSet, width - XOffSet);

            return new VectorC(x, y);
        }
        public static PointC RNDP()
        {
            float x = Mathematics.RandomFloat(XOffSet, width - XOffSet);
            float y = Mathematics.RandomFloat(XOffSet, width - XOffSet);

            return new PointC(x, y);
        }

        public static float[] RANGEX()
        {
            return new float[] { XOffSet, width - XOffSet };
        }
        public static float[] RANGEY()
        {
            return new float[] { YOffSet, height - YOffSet };
        }


        public static  PointC[] ImPts()
        {
            PointC[] ps = new PointC[5];

            ps[0] = new PointC(width / 2, height / 2);
            ps[1] = ExtremePoints()[0];
            ps[2] = ExtremePoints()[1];
            ps[3] = ExtremePoints()[2];
            ps[4] = ExtremePoints()[3];


            return ps;
        }

        private static Color stroke = Color.Black;
        private static Color fill = Color.Purple;

        public void DrawPoint(PointC pt, float w = 11, float s = 2.6f)
        {
            PointF p = ce(pt, w);
            grp.FillEllipse(new SolidBrush(fill), p.X, p.Y, w, w);
            grp.DrawEllipse(new Pen(stroke, s), p.X, p.Y, w, w);
            this.R();
        }
        public void DrawPoints(PointC pt1, PointC pt2, float w = 11, float s = 2.6f)
        {
            this.DrawPoint(pt1, w, s);
            this.DrawPoint(pt2, w, s);
        }
        public void DrawPoints(PointC pt1, PointC pt2, PointC pt3, float w = 11, float s = 2.6f)
        {
            this.DrawPoint(pt1, w, s);
            this.DrawPoint(pt2, w, s);
            this.DrawPoint(pt3, w, s);
        }
        public void DrawPoints(PointC[] ps, float w = 11, float s = 2.6f)
        {
            foreach (PointC p in ps)
            {
                this.DrawPoint(p);
            }
        }
        public void DrawPoints(List<PointC> ps, float w = 11, float s = 2.6f)
        {
            foreach (PointC p in ps)
            {
                this.DrawPoint(p);
            }
        }


        /*===================================================================================================*/

        private const float lstroky = 2.3f;
        private const Boolean isDashy = false;
        private const Boolean isPointy = false;

        public void DrawLine(LineC l, float s = lstroky, Boolean isDash = isDashy, Boolean isPoints = isPointy)
        {
            Pen pen = new Pen(stroke, s);
            if (isDash)
            {
                pen.DashPattern = dash;
            }


            PointF p1 = ext(l.P1, l.P2, 4000);
            PointF p2 = ext(l.P2, l.P1, 4000);

            grp.DrawLine(pen, p1, p2);

            if (isPoints)
            {
                this.DrawPoints(l.P1, l.P2);
            }

            this.R();
        }
        public void DrawLines(LineC l1, LineC l2, float s = lstroky, Boolean isDash = isDashy, Boolean isPoints = isPointy)
        {
            this.DrawLine(l1, s, isDash, isPoints);
            this.DrawLine(l2, s, isDash, isPoints);
        }
        public void DrawLines(LineC l1, LineC l2, LineC l3, float s = lstroky, Boolean isDash = isDashy, Boolean isPoints = isPointy)
        {
            this.DrawLine(l1, s, isDash, isPoints);
            this.DrawLine(l2, s, isDash, isPoints);
            this.DrawLine(l3, s, isDash, isPoints);
        }
        public void DrawLines(LineC[] ls, float s = lstroky, Boolean isDash = isDashy, Boolean isPoints = isPointy)
        {
            foreach (LineC l in ls)
            {
                this.DrawLine(l, s, isDash, isPoints);
            }
        }
        public void DrawLines(List<LineC> ls, float s = lstroky, Boolean isDash = isDashy, Boolean isPoints = isPointy)
        {
            foreach (LineC l in ls)
            {
                this.DrawLine(l, s, isDash, isPoints);
            }
        }

        /*===================================================================================================*/

        private const float str = 2.9f;
        private const Boolean sda = true;
        private const Boolean smd = true;
        private const Boolean spt = true;

        public void DrawSegment(SegmentC s, float strk = str, Boolean dashy = sda, Boolean mid = smd, Boolean pts = spt)
        {
            Pen pen = new Pen(stroke, strk);
            if (dashy)
            {
                pen.DashPattern = dash;
            }
            grp.DrawLine(pen, s.P1.P, s.P2.P);

            if (pts)
            {
                this.DrawPoints(s.P1, s.P2);
            }

            if (mid)
            {
                this.DrawPoint(s.Mid);
            }

            this.R();;
        }
        public void DrawSegments(SegmentC s1, SegmentC s2, float strk = str, Boolean dashy = sda, Boolean mid = smd, Boolean pts = spt)
        {
            this.DrawSegment(s1, strk, dashy, mid, pts);
            this.DrawSegment(s2, strk, dashy, mid, pts);

        }
        public void DrawSegments(SegmentC s1, SegmentC s2, SegmentC s3, float strk = str, Boolean dashy = sda, Boolean mid = smd, Boolean pts = spt)
        {
            this.DrawSegment(s1, strk, dashy, mid, pts);
            this.DrawSegment(s2, strk, dashy, mid, pts);
            this.DrawSegment(s3, strk, dashy, mid, pts);

        }
        public void DrawSegments(SegmentC[] ss, float strk = str, Boolean dashy = sda, Boolean mid = smd, Boolean pts = spt)
        {
            foreach (SegmentC s in ss)
            {
                this.DrawSegment(s, strk, dashy, mid, pts);
            }

        }
        public void DrawSegments(List<SegmentC> ss, float strk = str, Boolean dashy = sda, Boolean mid = smd, Boolean pts = spt)
        {
            foreach (SegmentC s in ss)
            {
                this.DrawSegment(s, strk, dashy, mid, pts);
            }

        }

        /*===================================================================================================*/




        private const float estr = 3.3f;
        private const Boolean isD = false;
        private const Boolean isC = true;
        private const Boolean isF = false;

        public void DrawEllipse(EllipseC e, float strk = estr, Boolean isDa = isD, Boolean isCen = isC, Boolean isFl = isF)
        {
            Pen pen = new Pen(stroke, strk);
            if (isDa)
            {
                pen.DashPattern = dash;
            }

            PointF c = ce(e.C, e.W, e.H);
            grp.DrawEllipse(pen, c.X, c.Y, e.W, e.H);

            if (isFl)
            {
                grp.FillEllipse(new SolidBrush(fill), c.X, c.Y, e.W, e.H);
            }

            if (isCen)
            {
                this.DrawPoint(e.C);
            }

            this.R();;
        }
        public void DrawEllipses(EllipseC e1, EllipseC e2, float strk = estr, Boolean isDa = isD, Boolean isCen = isC, Boolean isFl = isF)
        {
            this.DrawEllipse(e1, strk, isDa, isCen, isFl);
            this.DrawEllipse(e2, strk, isDa, isCen, isFl);
        }
        public void DrawEllipses(EllipseC[] es, float strk = estr, Boolean isDa = isD, Boolean isCen = isC, Boolean isFl = isF)
        {
            foreach (EllipseC e in es)
            {
                this.DrawEllipse(e, strk, isDa, isCen, isFl);
            }
        }
        public void DrawEllipses(List<EllipseC> es, float strk = estr, Boolean isDa = isD, Boolean isCen = isC, Boolean isFl = isF)
        {
            foreach (EllipseC e in es)
            {
                this.DrawEllipse(e, strk, isDa, isCen, isFl);
            }
        }


        /*===================================================================================================*/


        private const float tstr = 2.9f;
        private const Boolean tdsh = false;
        private const Boolean tpts = true;

        private static float[] dash = new float[] { 0.01f, 1.2f, 2.8f };

        public void DrawTriangle(TriangleC t, float strk = tstr, Boolean tdsha = tdsh, Boolean tptsn = tpts)
        {
            Pen pen = new Pen(stroke, strk);
            if (tdsha)
            {
                pen.DashPattern = dash;
            }

            this.DrawSegments(t.Sides, strk, tdsha, false, tpts);

            if (tptsn)
            {
                this.DrawPoints(t.A, t.B, t.C);
            }

            this.R();
        }
        public void DrawTriangles(TriangleC t1, TriangleC t2, float strk = tstr, Boolean tdsha = tdsh, Boolean tptsn = tpts)
        {
            this.DrawTriangle(t1, strk, tdsha, tptsn);
            this.DrawTriangle(t2, strk, tdsha, tptsn);
        }
        public void DrawTriangles(TriangleC[] ts, float strk = tstr, Boolean tdsha = tdsh, Boolean tptsn = tpts)
        {
            foreach (TriangleC t in ts)
            {
                this.DrawTriangle(t, strk, tdsha, tptsn);
            }
        }
        public void DrawTriangles(List<TriangleC> ts, float strk = tstr, Boolean tdsha = tdsh, Boolean tptsn = tpts)
        {
            foreach (TriangleC t in ts)
            {
                this.DrawTriangle(t, strk, tdsha, tptsn);
            }
        }

        public void DrawRectangle(RectangleC rect)
        {
            this.DrawSegments(rect.Sides, 2.9f, false, false, false);
        }


        private const int minC = 10;
        private const int maxC = 10;

        /* random Coords X or Y */
        public static float rx(int min = minC, int max = maxC)
        {
            //return Mathematics.RandomFloat(min, width - max);
            return Mathematics.RandomFloat(-width / 2 + min, width / 2 - max);
        }
        public static float ry(int min = minC, int max = maxC)
        {
            //return Mathematics.RandomFloat(min, height - max);
            return Mathematics.RandomFloat(-height / 2 + min, height / 2 - max);
        }
        public static PointF rxy(int min = minC, int max = maxC)
        {
            return new PointF(rx(min, max), ry(min, max));
        }

        public static float[] rangeX()
        {
            //return new float[] { minC, width - maxC};
            return new float[] { -width / 2 + minC, width / 2 - minC };
        }
        public static float[] rangeY()
        {
            //return new float[] { minC, height - maxC };
            return new float[] { -height / 2 + minC, height / 2 - minC };
        }

        /* used when drawing ellipses */
        public static PointF ce(PointF p, float w, float h)
        {
            float newX = p.X - w / 2;
            float newY = p.Y - h / 2;

            return new PointF(newX, newY);
        }
        public static PointF ce(PointF p, float w)
        {
            float newX = p.X - w / 2;
            float newY = p.Y - w / 2;

            return new PointF(newX, newY);
        }
        public static PointF ce(PointC p, float w, float h)
        {
            float newX = p.X - w / 2;
            float newY = p.Y - h / 2;

            return new PointF(newX, newY);
        }
        public static PointF ce(PointC p, float w)
        {
            float newX = p.X - w / 2;
            float newY = p.Y - w / 2;

            return new PointF(newX, newY);
        }

        /* find a point at an orthogonal distance from other point */
        public static PointF ext(PointF a, PointF b, float length = 3500)
        {
            float vx = b.X - a.X;
            float vy = b.Y - a.Y;

            float dist = (float)Math.Sqrt(Math.Pow(vx, 2) + Math.Pow(vy, 2));

            float ux = (vx / dist) * length;
            float uy = (vy / dist) * length;

            return new PointF(a.X + ux, a.Y + uy);
        }
        public static PointF ext(PointC a, PointC b, float length = 3500)
        {
            float vx = b.X - a.X;
            float vy = b.Y - a.Y;

            float dist = (float)Math.Sqrt(Math.Pow(vx, 2) + Math.Pow(vy, 2));

            float ux = (vx / dist) * length;
            float uy = (vy / dist) * length;

            return new PointF(a.X + ux, a.Y + uy);
        }
    }
}