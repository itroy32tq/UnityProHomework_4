using Code;
using Lessons.Architecture.PM;

namespace Assets.Code.HomeworksCode
{
    public sealed class HeroPresenterFactory
    {
        private readonly ProductBuyer _productBuyer;
        private readonly MoneyStorage _moneyStorage;

        
        public IHeroPresenter Create(HeroInfo heroInfo)
        {
            return new HeroPresenter(heroInfo);
        }
    }
}
