using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public sealed class ShopPopup : MonoBehaviour
    {
        [SerializeField] private Transform _container; 
        [SerializeField] private ProductView _viewPrefab;
        [SerializeField] private Button _hideButton;
    
        private readonly List<ProductView> _views = new();
    
        public void Show(IPresenter args)
        {
            if (args is not IShopPopupPresenter shopPopupPresenter)
            {
                throw new Exception("Expected IShopPopupPresenter");
            }

            gameObject.SetActive(true);

            for (var index = 0; index < shopPopupPresenter.ProductPresenters.Count; index++)
            {
                IProductPresenter productPresenter = shopPopupPresenter.ProductPresenters[index];
                ProductView view = Instantiate(_viewPrefab, _container);
                view.Initialized(productPresenter);
                _views.Add(view);
            }
            _hideButton.onClick.AddListener(Hide);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            for (var index = 0; index < _views.Count; index++)
            {
                ProductView productView = _views[index];
                Destroy(productView.gameObject);
            }
            _views.Clear();
            _hideButton.onClick.RemoveListener(Hide);
        }
    }
}
