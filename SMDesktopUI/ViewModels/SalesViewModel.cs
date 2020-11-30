using Caliburn.Micro;
using SMDesktopUI.Library.Api;
using SMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        IProductEndpoint _productEndpoint;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
            
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productList = await _productEndpoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }
        private BindingList<ProductModel> _products;

        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set 
            { 
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }


        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }


        public string SubTotal
        {
            get 
            {
                //TODO-Replace with calculation
                return "0.00$";
            }
            
        }

        public string Total
        {
            get
            {
                //TODO-Replace with calculation
                return "0.00$";
            }

        }

        public string Tax
        {
            get
            {
                //TODO-Replace with calculation
                return "0.00$";
            }

        }

        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                //Make sure something is selected
                //Make sure there is an item quantity

                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock>=ItemQuantity)
                {
                    output = true;
                }


                return output;
            }
        }

        public void AddToCart()
        {
            CartItemModel item = new CartItemModel()
            {
                Product = SelectedProduct,
                QuantityInCart = ItemQuantity
            };
            Cart.Add(item);
        }

        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                return output;
            }
        }
        public void RemoveFromCart() { }

        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                //Make sure something is in the cart
                return output;
            }
        }
        public void CheckOut() { }

    }
}
