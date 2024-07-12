using Assets.Code.HomeworksCode;
using Code;
using Lessons.Architecture.PM;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public sealed class HeroesHelper : MonoBehaviour
{
    [SerializeField] private HeroInfo _heroInfo;
    [SerializeField] private HeroPopup _heroPopup;
    [SerializeField] private ShopPopup _shopPopup;
    [SerializeField] private HeroesPool _heroesPool;

    [SerializeField] private int _range;

    private HeroPresenterFactory _factory;
    private IHeroPresenter _currentPresenter;

    [Inject]
    private void Construct(HeroPresenterFactory factory)
    {
        _factory = factory;
    }

    public void ShowPopup()
    {
        _currentPresenter = _factory.Create(_heroInfo);
        _heroPopup.Show(_currentPresenter);
    }

    public void ShowShopPopup()
    {
        //_shopPopup.Show(new ShopPopupPresenter(_heroesPool, _factory));
    }

    [Button]
    public void AddExperience()
    {
        _currentPresenter.AddExperience(_range);
    }

}