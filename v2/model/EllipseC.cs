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
    public class EllipseC
    {
        /*===================================================================================================*/
        #region fields

        private PointC c;

        private float w;
        private float h;

        #endregion
        /*===================================================================================================*/
        #region properties

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

        public float W
        {
            get
            {
                return this.w;
            }
            set
            {
                this.w = value;
            }
        }
        public float H
        {
            get
            {
                return this.h;
            }
            set
            {
                this.h = value;
            }
        }

        public float m
        {
            get
            {
                return this.w / 2;
            }
        }
        public float n
        {
            get
            {
                return this.h / 2;
            }
        }

        public float A
        {
            get
            {
                return this.getArea();
            }
        }
        private float getArea()
        {
            if (this.w == this.h)
            {
                return (float)(Constants.PI * Math.Pow(this.w / 2, 2));
            }
            else
            {
                return 4 * Constants.PI * (this.w / 2) * (this.h / 2);
            }
        }

        #endregion
        /*===================================================================================================*/
        #region constructor

        public EllipseC(PointC p, float r)
        {
            this.c = p;
            this.w = r;
            this.h = r;
        }
        public EllipseC(PointC p, float w, float h)
        {
            this.c = p;
            this.w = w;
            this.h = h;
        }
        public EllipseC()
        {
            this.c = new PointC();
            this.w = Mathematics.RandomInt(150, 450);
            this.h = this.w;
        }

        #endregion
        /*===================================================================================================*/
        #region methods

        public List<PointC> OrderdPointsOnMargin(float step = 5)
        {
            List<PointC> ps = new List<PointC>();
            for (float i = 0; i <= 360; i += step)
            {
                float angle = i * Constants.RAD;
                float dx = this.c.X + this.w * (float)Math.Cos(angle) / 2;
                float dy = this.c.Y + this.h * (float)Math.Sin(angle) / 2;

                ps.Add(new PointC(dx, dy));
            }

            return ps;
        }
        public List<PointC> RandomPointsInside(int no = 360)
        {
            List<PointC> ps = new List<PointC>();
            for (int i = 0; i < no; i++)
            {
                float angle = Mathematics.RandomInt(0, 360) * Constants.RAD;
                float wd = (float)(Math.Sqrt(Mathematics.RandomDouble()) * (this.w / 2));
                float ht = (float)(Math.Sqrt(Mathematics.RandomDouble()) * (this.h / 2));
                float dx = this.c.X + wd * (float)Math.Cos(angle);
                float dy = this.c.Y + ht * (float)Math.Sin(angle);

                ps.Add(new PointC(dx, dy));
            }

            return ps;
        }

        public static List<EllipseC> RandomEllipses(int no = 3)
        {
            List<EllipseC> es = new List<EllipseC>();
            for (int i = 0; i < no; i++)
            {
                es.Add(new EllipseC());
            }

            return es;
        }

        #endregion
        /*===================================================================================================*/
    }
}
