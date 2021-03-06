﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cartonization.Business
{
    public class Carton
    {
        private List<Product> _productsInCarton;
        private Dictionary<decimal, List<Product>> _layerdProducts;
        private decimal _usedHeight;

        public Carton(int id, decimal height, decimal length, decimal width)
        {
            Id = id;
            Space = new Space(new Dimension(width, length, height));
            _productsInCarton = new List<Product>();
            _layerdProducts = new Dictionary<decimal, List<Product>>();
        }

        public int Id { get; private set; }

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

        public decimal UsedHeight
        {
            get
            {
                return _usedHeight;
            }
        }

        public ReadOnlyCollection<Product> ProductsInCarton 
        { 
            get 
            {
                return _productsInCarton.AsReadOnly();
            }
        }

        public decimal PackedVolume 
        {
            get 
            {
                return ProductsInCarton.Sum(p => p.Space.Volume);
            }
        }

        public decimal WasteVolume 
        {
            get {
                return Space.Volume - PackedVolume;
            }
        }

        public Dictionary<decimal, List<Product>> LayeredProducts
        {
            get
            {
                return _layerdProducts;
            }
        }

        public decimal LevelSurfaceArea( decimal level )
        {
            if (_layerdProducts.ContainsKey(level))
            {
                return _layerdProducts[level].Sum(p => p.Space.SurfaceArea);
            }
            return 0;
        }

        public void Add(decimal level, Product product)
        {
            if (_layerdProducts.ContainsKey(level))
            {
                _layerdProducts[level].Add(product);
                
            }
            else
            {
                List<Product> products = new List<Product>();
                products.Add(product);
                _layerdProducts.Add(level, products);

                int result = 0;
                // only increment height if its a integer level.
                if (int.TryParse(level.ToString(), out result))
                {
                    _usedHeight = _usedHeight + product.Space.Dimension.Height;
                }
            }

            _productsInCarton.Add(product);
        }

        public bool CanAddProductWithoutHeightOverflow(Product product)
        {
            return _usedHeight + product.Height <= Space.Dimension.Height;
        }


        public override string ToString()
        {
            return
                string.Format("CartonID: " + Id)
                + string.Format("<br>Products Count: " + ProductsInCarton.Count)
                + string.Format("<br>Height: Available {0}, Used {1}", Height, UsedHeight)
                + string.Format("<br>Volume: Used: {0}, Wasted {1}", PackedVolume, WasteVolume)
                + string.Format("<br>Levels packed: " + LayeredProducts.Keys.Count)
                + string.Format("<br>SurfaceAreaUsedPerLevel:" + SurfaceAreaUsedPerLevel());
        }


        public string SurfaceAreaUsedPerLevel()
        {
            StringBuilder stringBuilder = new System.Text.StringBuilder();

            decimal surfaceArea = Width * Length;

            foreach(decimal level in LayeredProducts.Keys)
            {
                stringBuilder.AppendFormat("<br>level: {0}, Used: {1} , Wasted: {2}", level, LevelSurfaceArea(level), surfaceArea - LevelSurfaceArea(level));
            }

            return stringBuilder.ToString();
        }
    }
}
