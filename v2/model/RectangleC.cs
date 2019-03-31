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
    public class RectangleC
    {
        public PointC[] ps = new PointC[4];
        private float w, h;

        private void constructRectangle()
        {
            LineC[] ls = DrawingEngine.ExtremeLines();

            this.ps[1] = this.ps[0].ReflectOrt(this.ps[0].ReflectRel(ls[1], 1), this.w);
            this.ps[2] = this.ps[1].ReflectOrt(this.ps[1].ReflectRel(ls[2], 1), this.h);
            this.ps[3] = this.ps[0].ReflectOrt(this.ps[0].ReflectRel(ls[2], 1), this.h);
        }
        
        public RectangleC(PointC p, float width, float height)
        {
            this.ps[0] = p;
            this.w = width; this.h = height;
            this.constructRectangle();
        }

        public RectangleC(PointC[] ps, float width, float height)
        {
            this.ps[0] = ps[0];
            this.ps[1] = ps[1];
            this.ps[2] = ps[2];
            this.ps[3] = ps[3];
            this.w = width; this.h = height;
        }

        public RectangleC(PointC p, float width)
        {
            this.ps[0] = p;
            this.w = width; this.h = width;
            this.constructRectangle();
        }

        private SegmentC[] sides()
        {
            SegmentC[] sides = new SegmentC[4];
            for(int i = 0; i < 4; i++)
            {
                int j;

                if(i == 3)
                {
                    j = 0;
                }
                else
                {
                    j = i + 1;
                }

                sides[i] = new SegmentC(this.ps[i], this.ps[j]);
            }

            return sides;
        }

        public SegmentC[] diagonals()
        {
            SegmentC[] ds = new SegmentC[2];
            ds[0] = new SegmentC(this.ps[0], this.ps[2]);
            ds[1] = new SegmentC(this.ps[1], this.ps[3]);

            return ds;
        }

        public PointC Center
        {
            get
            {
                return this.diagonals()[0].Intersection(this.diagonals()[1]);
            }
        }

        public SegmentC[] Diagonals
        {
            get
            {
                return this.diagonals();
            }
        }

        public SegmentC[] Sides
        {
            get
            {
                return this.sides();
            }
        }


        private void update(RectangleC t)
        {
            this.ps[0] = t.ps[0];
            this.ps[1] = t.ps[1];
            this.ps[2] = t.ps[2];
            this.ps[3] = t.ps[3];
        }
        public RectangleC SCALE(float Sx, float Sy)
        {
            PointC[] ts = new PointC[4];

            for (int i = 0; i < 4; i++)
            {
                ts[i] = ps[i].SCALE(Sx, Sy);
            }

            return new RectangleC(ts, this.w, this.h);
        }

        public RectangleC SCALE(float S)
        {
            return this.SCALE(S, S);
        }

        public RectangleC SCALE(PointC p, float Sx, float Sy)
        {
            PointC[] ts = new PointC[4];

            for (int i = 0; i < 4; i++)
            {
                ts[i] = ps[i].SCALE(p, Sx, Sy);
            }

            return new RectangleC(ts, this.w, this.h);
        }
        public RectangleC SCALE(PointC p, float S)
        {
            return this.SCALE(p, S, S);
        }

        public void scale(float Sx, float Sy)
        {
            RectangleC temp = this.SCALE(Sx, Sy);
            this.update(temp);
        }
        public void scale(float S)
        {
            this.scale(S, S);
        }
        public void scale(PointC p, float Sx, float Sy)
        {
            RectangleC temp = this.SCALE(p, Sx, Sy);
            this.update(temp);
        }
        public void scale(PointC p, float S)
        {
            this.scale(p, S, S);
        }

        public RectangleC TRANSLATE(float Tx, float Ty)
        {
            PointC[] ts = new PointC[4];

            for (int i = 0; i < 4; i++)
            {
                ts[i] = ps[i].TRANSLATE(Tx, Ty);
            }

            return new RectangleC(ts, this.w, this.h);
        }
        public RectangleC TRANSLATE(float T)
        {
            return this.TRANSLATE(T, T);
        }

        public void translate(float Tx, float Ty)
        {
            RectangleC temp = this.TRANSLATE(Tx, Ty);
            this.update(temp);
        }
        public void translate(float T)
        {
            this.translate(T, T);
        }

        public RectangleC ROTATE(float angle)
        {
            PointC[] ts = new PointC[4];

            for (int i = 0; i < 4; i++)
            {
                ts[i] = ps[i].ROTATE(angle);
            }

            return new RectangleC(ts, this.w, this.h);
        }
        public RectangleC ROTATE(PointC p, float angle)
        {
            PointC[] ts = new PointC[4];

            for (int i = 0; i < 4; i++)
            {
                ts[i] = ps[i].ROTATE(p, angle);
            }

            return new RectangleC(ts, this.w, this.h);
        }

        public void rotate(float angle)
        {
            RectangleC temp = this.ROTATE(angle);
            this.update(temp);
        }
        public void rotate(PointC p, float angle)
        {
            RectangleC temp = this.ROTATE(p, angle);
            this.update(temp);
        }

    }
}
