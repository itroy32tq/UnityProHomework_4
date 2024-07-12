using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class ProductHelper : MonoBehaviour
    {
        [SerializeField] private ProductInfo _productInfo;
        [SerializeField] private ProductPopup _productPopup;
        [SerializeField] private ShopPopup _shopPopup;
        [SerializeField] private ProductCatalog _productCatalog;
        private ProductPresenterFactory _productPresenterFactory;

        [Inject]
        private void Construct(ProductPresenterFactory productPresenterFactory)
        {
            _productPresenterFactory = productPresenterFactory;
        }
        
        public void ShowPopup()
        {
            IProductPresenter productPresenter = _productPresenterFactory.Create(_productInfo);
            ProductPopup popup = Instantiate(_productPopup);
            popup.Show(productPresenter);
        }

        public void ShowShopPopup()
        {
            _shopPopup.Show(new ShopPopupPresenter(_productCatalog, _productPresenterFactory));
        }
    }
}
