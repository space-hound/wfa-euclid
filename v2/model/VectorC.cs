using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Primitives2d.maths;
using Primitives2d.draws;

namespace Primitives2d.model
{
    public class VectorC
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


        private float magnitude()
        {
            return (float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
        }
        public float M
        {
            get
            {
                return this.magnitude();
            }
        }

        private VectorC normalizedVector()
        {
            return new VectorC(this.x / this.magnitude(), this.y / this.magnitude());
        }
        public VectorC dV
        {
            get
            {
                return this.normalizedVector();
            }
        }

        public VectorC()
        {
            this.x = DrawingEngine.RNDX();
            this.y = DrawingEngine.RNDY();
        }
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
        public VectorC(PointC p)
        {
            this.x = p.X;
            this.y = p.Y;
        }
        public VectorC(PointF p)
        {
            this.x = p.X;
            this.y = p.Y;
        }
        public VectorC(PointC p1, PointC p2)
        {
            this.x = p2.X - p1.X;
            this.y = p2.Y - p1.Y;
        }

        /************************************************************************************************
         * 
         *  METHODS();
         *  
         ************************************************************************************************/

        public float Dist(VectorC v)
        {
            return (float)Math.Sqrt(Math.Pow((v.x - this.x), 2) + Math.Pow((v.y - this.y), 2));
        }

        public VectorC Negate()
        {
            return new VectorC(this.x * -1, this.y * -1);
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

        /************************************************************************************************
         * 
         *  TO STRING();
         *  
         ************************************************************************************************/

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

        /************************************************************************************************
         * 
         *  SCALE - TRANSLATE - ROTATE - SHEAR - REFLECT - ETC.
         *  
         ************************************************************************************************/
        
        private void update(VectorC u)
        {
            this.x = u.x;
            this.y = u.y;
        }

        public VectorC SCALE(float Sx, float Sy)
        {
            return new VectorC(this.x * Sx, this.y * Sy);
        }
        public VectorC SCALE(float S)
        {
            return this.SCALE(S, S);
        }
        public VectorC SCALE(VectorC u, float Sx, float Sy)
        {
            float newX = this.x * Sx + u.x * (1 - Sx),
                  newY = this.y * Sy + u.y * (1 - Sy);

            return new VectorC(newX, newY);
        }
        public VectorC SCALE(VectorC u, float S)
        {
            return this.SCALE(u, S, S);
        }

        public void scale(float Sx, float Sy)
        {
            VectorC temp = this.SCALE(Sx, Sy);
            this.update(temp);
        }
        public void scale(float S)
        {
            this.scale(S, S);
        }
        public void scale(VectorC u, float Sx, float Sy)
        {
            VectorC temp = this.SCALE(u, Sx, Sy);
            this.update(temp);
        }
        public void scale(VectorC u, float S)
        {
            VectorC temp = this.SCALE(u, S, S);
            this.update(temp);
        }

        public VectorC TRANSLATE(float Tx, float Ty)
        {
            return new VectorC(this.x + Tx, this.y + Ty);
        }
        public VectorC TRANSLATE(float T)
        {
            return this.TRANSLATE(T, T);
        }

        public void translate(float Tx, float Ty)
        {
            VectorC temp = this.TRANSLATE(Tx, Ty);
            this.update(temp);
        }
        public void translate(float T)
        {
            this.translate(T, T);
        }

        public VectorC ROTATE(float angle)
        {
            float alfa = angle * Constants.RAD;

            float dx = (float)(this.x * Math.Cos(alfa) - this.y * Math.Sin(alfa));
            float dy = (float)(this.x * Math.Sin(alfa) + this.y * Math.Cos(alfa));

            return new VectorC(dx, dy);
        }
        public VectorC ROTATE(VectorC u, float angle)
        {
            float alfa = angle * Constants.RAD;

            float dx = (float)(Math.Cos(this.x - u.x) - Math.Sin(this.y + u.y) + u.x);
            float dy = (float)(Math.Cos(this.y - u.y) + Math.Sin(this.x - u.x) + u.y);

            return new VectorC(dx, dy);
        }

        public void rotate(float angle)
        {
            VectorC temp = this.ROTATE(angle);
            this.update(temp);
        }
        public void rotate(VectorC u, float angle)
        {
            VectorC temp = this.ROTATE(u, angle);
            this.update(temp);
        }

        public VectorC SHEARX(float angle)
        {
            float alfa = angle * Constants.RAD;
            float dx = (float)(this.x + this.y * Math.Tan(alfa));
            float dy = this.y;

            return new VectorC(dx, dy);
        }
        public VectorC SHEARY(float angle)
        {
            float alfa = angle * Constants.RAD;
            float dx = this.x;
            float dy = (float)(Math.Tan(alfa) * this.x + this.y);

            return new VectorC(dx, dy);
        }


        public void shearx(float angle)
        {
            VectorC temp = this.SHEARX(angle);
            this.update(temp);
        }
        public void sheary(float angle)
        {
            VectorC temp = this.SHEARY(angle);
            this.update(temp);
        }

        public VectorC REFLECTX(float axis = 0)
        {
            return new VectorC(this.x, (this.y * -1) + (2 * axis));
        }
        public VectorC REFLECTY(float axis = 0)
        {
            return new VectorC((this.x * -1) + (2 * axis), this.y);
        }

        public void reflectx(float axis = 0)
        {
            VectorC temp = this.REFLECTX(axis);
            this.update(temp);
        }
        public void reflecty(float axis = 0)
        {
            VectorC temp = this.REFLECTY(axis);
            this.update(temp);
        }




    }
}
