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
    public class RectangleC
    {
        public PointC p1;
        public PointC p2;
        public PointC p3;
        public PointC p4;

        public float w;
        public float h;

        public SegmentC[] getSides()
        {
            SegmentC[] ss = new SegmentC[4];
            ss[0] = new SegmentC(this.p1, this.p2);
            ss[1] = new SegmentC(this.p2, this.p3);
            ss[2] = new SegmentC(this.p3, this.p4);
            ss[3] = new SegmentC(this.p4, this.p1);

            return ss;
        }

        public SegmentC[] Sieds
        {
            get
            {
                return this.getSides();
            }
        }

        private void cons()
        {
            LineC[] bl = LineC.BoundingLines();
            this.p2 = this.p1.ReflectOrtho(this.p1.Reflect(bl[3], 1), w);
            this.p3 = this.p2.ReflectOrtho(this.p2.Reflect(bl[1], 1), h);
            this.p4 = this.p1.ReflectOrtho(this.p1.Reflect(bl[1], 1), h);

        }

        public RectangleC(PointC p, float w, float h)
        {
            this.p1 = p;
            this.w = w;
            this.h = h;
            this.cons();
        }

        public RectangleC()
        {
            this.p1 = new PointC();
            this.w = Mathematics.RandomInt(150, 500);
            this.h = Mathematics.RandomInt(150, 500);
            this.cons();
        }

        private void update(PointC q1, PointC q2, PointC q3, PointC q4)
        {
            this.p1 = q1;
            this.p2 = q2;
            this.p3 = q3;
            this.p4 = q4;
        }

        public void Rotate(PointC p, float angle)
        {
            PointC q1 = Geometry.Rotation(this.p1, p, angle);
            PointC q2 = Geometry.Rotation(this.p2, p, angle);
            PointC q3 = Geometry.Rotation(this.p3, p, angle);
            PointC q4 = Geometry.Rotation(this.p4, p, angle);
            this.update(q1, q2, q3, q4);
        }

        public PointC getC()
        {
            return new PointC(this.p1.X + 0.5f * this.w, this.p1.Y + 0.5f * this.h);
        }
        public PointC C
        {
            get
            {
                return this.getC();
            }
        }
    }
}
