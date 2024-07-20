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
        IReadOnlyReactiveCollection<CharacterStat> Stats { get; }
        IReadOnlyReactiveProperty<bool> CanLevelUp { get; }
        int RequiredExperience { get; }
        ReactiveCommand CanLevelUpCommand { get; }
        void LevelUp();
        void AddExperience(int range);
        void AddStat(CharacterStat stat);
        void RemoveStat(CharacterStat stat);
        CharacterStat GetStat(string name);
        CharacterStat[] GetStats();
    }

}
