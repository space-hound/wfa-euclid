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
    public class VectorC
    {
        /*===================================================================================================*/
        #region fields

        private float x;
        private float y;

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

        public float M
        {
            get
            {
                return this.magnitude();
            }
        }
        public VectorC dV
        {
            get
            {
                return this.normalizedVector();
            }
        }

        #endregion
        /*===================================================================================================*/
        #region methods

        private float magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
        }
        private VectorC normalizedVector()
        {
            return new VectorC(this.x / this.magnitude(), this.y / this.magnitude());
        }

        #endregion
        /*===================================================================================================*/
        #region constructors

        public VectorC(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public VectorC(float x1, float y1, float x2, float y2)
        {
            this.x = x2 - x1;
            this.y = y2 - y1;
        }

        public VectorC(PointF p)
        {
            this.x = p.X;
            this.y = p.Y;
        }
        public VectorC(PointF p1, PointF p2)
        {
            this.x = p2.X - p1.X;
            this.y = p2.X - p1.Y;
        }

        public VectorC(PointC p)
        {
            this.x = p.X;
            this.y = p.Y;
        }
        public VectorC(PointC p1, PointC p2)
        {
            this.x = p2.X - p1.X;
            this.y = p2.Y - p1.Y;
        }

        public VectorC()
        {
            this.x = Drawing.rx();
            this.y = Drawing.ry();
        }

        #endregion
        /*===================================================================================================*/
        #region operations



        #endregion
        /*===================================================================================================*/
        #region stringify

        public string Stringify()
        {
            return this.sign()[0] + this.x.ToString() + "i " + this.sign()[1] + this.y.ToString() + "j";
        }

        private string[] sign()
        {
            string[] sign = new string[2];
            sign[0] = ((this.x < 0) ? "-" : "");
            sign[1] = ((this.y) < 0) ? "" : "+";

            return sign;
        }

        #endregion
        /*===================================================================================================*/
        #region operations

        public float Magnitude(VectorC v)
        {
            return (float)Math.Sqrt(Math.Pow((v.x - this.x), 2) + Math.Pow((v.y - this.y), 2));
        }
        public VectorC Negate()
        {
            return new VectorC(-this.x, -this.y);
        }
        public VectorC Add(float alfa)
        {
            return new VectorC(this.x + alfa, this.y + alfa);
        }
        public VectorC Sub(float alfa)
        {
            return new VectorC(this.x - alfa, this.y - alfa);
        }
        public VectorC Mul(float alfa)
        {
            return new VectorC(this.x * alfa, this.y * alfa);
        }
        public VectorC Div(float alfa)
        {
            return new VectorC(this.x / alfa, this.y / alfa);
        }
        public VectorC Add(VectorC v)
        {
            return new VectorC(this.x + v.x, this.y + v.y);
        }
        public VectorC Sub(VectorC v)
        {
            return new VectorC(this.x - v.x, this.y - v.y);
        }
        public VectorC Div(VectorC v)
        {
            return new VectorC(this.x / v.x, this.y / v.y);
        }
        public float Dot(VectorC v)
        {
            return (this.x * v.x + this.y * v.y);
        }
        public float Cross(VectorC v)
        {
            return (this.x * v.y + this.y * v.x);
        }
        public float Ang(VectorC v)
        {
            float cross_product = this.Dot(v);
            float magni_product = this.magnitude() * v.magnitude();

            return (float)(Math.Acos(cross_product / magni_product) * Constants.ANG);
        }

        #endregion
        /*===================================================================================================*/
    }
}
