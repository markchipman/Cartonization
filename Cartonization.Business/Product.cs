﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Product : IEquatable<Product>
    {
        public Product(int id ,decimal height, decimal length, decimal width)
        {
            decimal[] array = new[] {height, width, length};
            decimal[] result = array.OrderByDescending(d => d).ToArray();
            
            Space = new Space(new Dimension( result[0], result[1], result[2]));
            
            ProductId = id;
        }

        public int ProductId { get; private set; }

        public Space Space { get; private set; }

        public decimal Width
        {
            get
            {
                return Space.Dimension.Width;
            }
        }

        public decimal Height
        {
            get
            {
                return Space.Dimension.Height;
            }
        }

        public decimal Length
        {
            get
            {
                return Space.Dimension.Length;
            }
        }

        public void Rotate2D()
        {
            Dimension newDimension = Space.Dimension.TwoDRotate();
            Space = new Space(newDimension);
        }

        public bool CanRotateTwoDimensional( Space space )
        {
            //3drotate product.
            Dimension newDimension = Space.Dimension.TwoDRotate();
            Space newSpace = new Space(newDimension);
            if (space >= newSpace)
            {
                return true;
            }
            return false;
        }

        public void SetDimension( Dimension d)
        {
            Space = new Space(d);
        }

        public override string ToString() 
        {
            return "ProductId: " + ProductId + ", " + Space.ToString();
        }

        public bool Equals(Product other)
        {
            return this.ProductId == other.ProductId;
        }

        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Product productObj = obj as Product;
            if (productObj == null)
                return false;
            else
                return Equals(productObj);
        }
    }
}
