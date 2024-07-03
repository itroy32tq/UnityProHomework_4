using System.Collections.Generic;

namespace Code
{
    public sealed class ShopPopupPresenter : IShopPopupPresenter
    {
        public IReadOnlyList<IProductPresenter> ProductPresenters => _presenters;
        private readonly List<IProductPresenter> _presenters = new();

        public ShopPopupPresenter(ProductCatalog productCatalog, ProductPresenterFactory productPresenterFactory)
        {
            ProductInfo[] products = productCatalog.Products;
            for (int i = 0, count = products.Length; i < count; i++)
            {
                ProductInfo product = products[i];
                IProductPresenter presenter = productPresenterFactory.Create(product); 
                _presenters.Add(presenter);
            }
        }
    }
}
