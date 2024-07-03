using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code
{
    public sealed class ProductPresenter : IProductPresenter
    {
        private readonly ProductInfo _productInfo;
        private readonly ProductBuyer _productBuyer;
        public string Title { get; }
        public string Description { get; }
        public Sprite Icon { get; }
        public string Price { get; }
        public ReactiveCommand BuyCommand { get; }
        public IReadOnlyReactiveProperty<bool> CanBuy => _canBuy;
        private readonly ReactiveProperty<bool> _canBuy = new BoolReactiveProperty();
        private readonly CompositeDisposable _disposable = new ();

        public ProductPresenter(ProductInfo productInfo, ProductBuyer productBuyer, MoneyStorage moneyStorage)
        {
            _productInfo = productInfo;
            _productBuyer = productBuyer;
            Title = productInfo.Title;
            Description = productInfo.Description;
            Icon = productInfo.Icon;
            Price = productInfo.MoneyPrice.ToString();
            moneyStorage.Money.Subscribe(OnMoneyChanged).AddTo(_disposable);
            BuyCommand = new ReactiveCommand(CanBuy);
            BuyCommand.Subscribe(OnBuyCommand).AddTo(_disposable);
        }

        private void OnMoneyChanged(long _)
        {
            _canBuy.Value = _productBuyer.CanBuy(_productInfo);
        }

        private void OnBuyCommand(Unit obj)
        {
            Buy();
        }

        public bool Buy()
        {
            if (_productBuyer.CanBuy(_productInfo))
            {
                _productBuyer.Buy(_productInfo);
                return true;
            }

            return false;
        }
        
        ~ProductPresenter()
        {
            _disposable.Dispose();
        }
    }
}
