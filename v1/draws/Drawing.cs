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
using Euclid2d.Euclid.model;
using Euclid2d.Euclid.others;


namespace Euclid2d.Euclid.draws
{
    public class Drawing
    {
        /*===================================================================================================*/
        #region fields

        private static Drawing instance = null;

        private static PictureBox map;
        private static Bitmap bitmap;
        private static Graphics graph;

        private static int mapWidth;
        private static int mapHeight;

        private static Color stroke = ColorC.DimBlack();
        private static Color fill = ColorC.Amber();

        private static float[] dash = new float[] { 0.01f, 1.2f, 2.8f };

        #endregion
        /*===================================================================================================*/
        #region properties

        public PictureBox Map
        {
            get
            {
                return map;
            }
        }
        public static int Width
        {
            get
            {
                return mapWidth;
            }
        }
        public static int Height
        {
            get
            {
                return mapHeight;
            }
        }

        public Graphics Graphics
        {
            get
            {
                return graph;
            }
        }

        public void Fill(string s = "")
        {
            if (s == "")
            {
                fill = ColorC.RandomColor();
            }
            else
            {
                fill = ColorC.HEX(s);
            }
        }
        public void Stroke(string s = "0")
        {
            if (s == "0")
            {
                stroke = ColorC.RandomColor();
            }
            else
            {
                stroke = ColorC.HEX(s);
            }
        }

        #endregion
        /*===================================================================================================*/
        #region constructors

        private Drawing(PictureBox pic, int mode = 1)
        {
            map = pic;
            bitmap = new Bitmap(map.Width, map.Height);
            graph = Graphics.FromImage(bitmap);

            SmoothingMode(mode);

            mapWidth = map.Width;
            mapHeight = map.Height;
            //graph.TranslateTransform(mapWidth / 2, mapHeight / 2);
        }

        private static void SmoothingMode(int mode)
        {
            if (mode == 1)
            {
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            }
            else if (mode == 2)
            {
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            }
            else if (mode == 3)
            {
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            }
            else
            {
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            }
        }

        /* Instantiate the instance or returns it */
        public static Drawing getInstance(PictureBox pic, int mode = 1)
        {
            if (instance == null)
            {
                instance = new Drawing(pic, mode);
            }

            return instance;
        }
        /* Reinstantiate the object */
        public void Reload(PictureBox pic, int mode = 1)
        {
            instance = null;
            instance = new Drawing(pic, mode);
        }
        /* Delete the instance */
        public static void Dispose()
        {
            instance = null;
        }
        
        
        /* Refresh the drawing area*/
        public void Refresh()
        {
            map.Image = bitmap;
        }
        /* Clear the drawing area */
        public void Clear()
        {
            graph.Clear(map.BackColor);
            map.Image = bitmap;

            stroke = ColorC.DimBlack();
            fill = ColorC.Amber();
        }

        #endregion
        /*===================================================================================================*/
        #region points

        public void DrawPoint(PointC pt, float w = 11, float s = 2.6f)
        {
            PointF p = ce(pt, w);
            graph.FillEllipse(new SolidBrush(fill), p.X, p.Y, w, w);
            graph.DrawEllipse(new Pen(stroke, s), p.X, p.Y, w, w);
            this.Refresh();
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

        #endregion
        /*===================================================================================================*/
        #region lines

        private const float lstroky = 2.3f;
        private const Boolean isDashy = true;
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

            graph.DrawLine(pen, p1, p2);

            if (isPoints)
            {
                this.DrawPoints(l.P1, l.P2);
            }

            this.Refresh();
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

        #endregion
        /*===================================================================================================*/
        #region segments

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
            graph.DrawLine(pen, s.P1.P, s.P2.P);

            if (pts)
            {
                this.DrawPoints(s.P1, s.P2);
            }

            if (mid)
            {
                this.DrawPoint(s.Mid);
            }

            this.Refresh();
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
            foreach(SegmentC s in ss)
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


        #endregion
        /*===================================================================================================*/
        #region ellipses

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
            graph.DrawEllipse(pen, c.X, c.Y, e.W, e.H);

            if (isFl)
            {
                graph.FillEllipse(new SolidBrush(fill), c.X, c.Y, e.W, e.H);
            }

            if (isCen)
            {
                this.DrawPoint(e.C);
            }

            this.Refresh();
        }
        public void DrawEllipses(EllipseC e1, EllipseC e2, float strk = estr, Boolean isDa = isD, Boolean isCen = isC, Boolean isFl = isF)
        {
            this.DrawEllipse(e1, strk, isDa, isCen, isFl);
            this.DrawEllipse(e2, strk, isDa, isCen, isFl);
        }
        public void DrawEllipses(EllipseC[] es, float strk = estr, Boolean isDa = isD, Boolean isCen = isC, Boolean isFl = isF)
        {
            foreach(EllipseC e in es)
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

        #endregion
        /*===================================================================================================*/
        #region triangles

        private const float tstr = 2.9f;
        private const Boolean tdsh = false;
        private const Boolean tpts = true;

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

            this.Refresh();
        }
        public void DrawTriangles(TriangleC t1, TriangleC t2, float strk = tstr, Boolean tdsha = tdsh, Boolean tptsn = tpts)
        {
            this.DrawTriangle(t1, strk, tdsha, tptsn);
            this.DrawTriangle(t2, strk, tdsha, tptsn);
        }
        public void DrawTriangles(TriangleC[] ts, float strk = tstr, Boolean tdsha = tdsh, Boolean tptsn = tpts)
        {
            foreach(TriangleC t in ts)
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

        public void drawR(RectangleC r)
        {
            this.DrawSegments(r.getSides());
            this.Refresh();
        }

        #endregion
        /*===================================================================================================*/
        #region grid

        public void grid()
        {
            PointC o = new PointC(0, 0);
            LineC[] bl = LineC.BoundingLines();
            this.Stroke("a1a1a1");
            this.DrawLines(bl, 2.5f, false, false);
            this.DrawLine(new LineC(o.Reflect(bl[0], 1), o.Reflect(bl[1], 1)), 2.5f, false, false);
            this.DrawLine(new LineC(o.Reflect(bl[2], 1), o.Reflect(bl[3], 1)), 2.5f, false, false);
            this.Refresh();

            stroke = ColorC.DimBlack();
            fill = ColorC.Amber();
        }
        
        #endregion
        /*===================================================================================================*/
        #region others

        private const int minC = 10;
        private const int maxC = 10;

        /* random Coords X or Y */
        public static float rx(int min = minC, int max = maxC)
        {
            //return Mathematics.RandomFloat(min, mapWidth - max);
            return Mathematics.RandomFloat(-mapWidth / 2 + min, mapWidth / 2 - max);
        }
        public static float ry(int min = minC, int max = maxC)
        {
            //return Mathematics.RandomFloat(min, mapHeight - max);
            return Mathematics.RandomFloat(-mapHeight / 2 + min, mapHeight / 2 - max);
        }
        public static PointF rxy(int min = minC, int max = maxC)
        {
            return new PointF(rx(min, max), ry(min, max));
        }

        public static float[] rangeX()
        {
            //return new float[] { minC, mapWidth - maxC};
            return new float[] { -mapWidth / 2 + minC, mapWidth / 2 - minC };
        }
        public static float[] rangeY()
        {
            //return new float[] { minC, mapHeight - maxC };
            return new float[] { -mapHeight / 2 + minC, mapHeight / 2 - minC };
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

        #endregion
        /*===================================================================================================*/
    }
}
