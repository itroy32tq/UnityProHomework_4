using Assets.Code.HomeworksCode;
using Lessons.Architecture.PM;
using System.Collections.Generic;

namespace Assets.Code.HeroPopupManager
{
    public sealed class HeroPopupPresenter : IHeroPopupPresenter
    {
        public IReadOnlyList<IHeroPresenter> HeroPresenters => _presenters;
        private readonly List<IHeroPresenter> _presenters = new();

        public HeroPopupPresenter(HeroesPool heroesPool, HeroPresenterFactory factory)
        {
            HeroInfo[] heroes = heroesPool.Heroes;

            for (int i = 0, count = heroes.Length; i < count; i++)
            {
                HeroInfo hero = heroes[i];
                IHeroPresenter presenter = factory.Create(hero);
                _presenters.Add(presenter);
            }
        }
    }
}
