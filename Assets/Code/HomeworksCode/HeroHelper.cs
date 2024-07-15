using Assets.Code.HomeworksCode;
using Lessons.Architecture.PM;
using System;
using UnityEngine;
using Zenject;

namespace Code
{
    public sealed class HeroHelper : MonoBehaviour
    {
        [SerializeField] private HeroInfo _heroInfo;
        [SerializeField] private HeroPopup _heroPopup;
        [SerializeField] private HeroesPool _heroesPool;
        [SerializeField] private Transform _container;
        [SerializeField] private int _range;
        [SerializeField] private ShopPopup _shopPopup;

        private HeroPresenterFactory _factory;
        private IHeroPresenter _currentPresenter;

        [SerializeField] private CharacterStat _characterStat;
        [SerializeField] private string _characterStatName;
        [SerializeField] private int _value;

        [Inject]
        private void Construct(HeroPresenterFactory presentorFactory)
        {
            _factory = presentorFactory;
        }

        public void ShowPopup()
        {
            _currentPresenter = _factory.Create(_heroInfo);
            HeroPopup popup = Instantiate(_heroPopup, _container);
            popup.Show(_currentPresenter);
        }

        public void ShowShopPopup()
        {
            //_shopPopup.Show(new ShopPopupPresenter(_productCatalog, _productPresenterFactory));
        }

        public void AddExperience()
        {
            _currentPresenter.AddExperience(_range);
        }

        public void AddStat()
        {
            _currentPresenter.AddStat(_characterStat);
        }

        public void RemoveStat()
        {
            _currentPresenter.RemoveStat(_characterStat);
        }

        public void GetStat()
        {
            _characterStat = _currentPresenter.GetStat(_characterStatName);
        }

        public void ChangeStatValue()
        {
            _characterStat = _currentPresenter.GetStat(_characterStatName);
            _characterStat.SetValue(_value);
        }
    }
}
