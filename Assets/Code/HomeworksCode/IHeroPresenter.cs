using Code;
using UniRx;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public interface IHeroPresenter : IPresenter
    {
        string Name { get; }
        string Description { get; }
        Sprite Icon { get; }
        IReadOnlyReactiveProperty<int> CurrentLevel { get; }
        IReadOnlyReactiveProperty<int> CurrentExperience { get; }
        int RequiredExperience { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        ReactiveCommand CanLevelUpCommand { get; }
        void LevelUp();
        void AddExperience(int range);
        void AddStat(CharacterStat stat);
        void RemoveStat(CharacterStat stat);
        CharacterStat GetStat(string name);
        public CharacterStat[] GetStats();
    }

}
