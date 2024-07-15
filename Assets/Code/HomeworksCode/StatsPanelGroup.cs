using Lessons.Architecture.PM;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets.Code.HomeworksCode
{
    public sealed class StatsPanelGroup : MonoBehaviour
    {
        [SerializeField] private List<CharacterStat> _stats;

        private IHeroPresenter _heroPresenter;
        private readonly CompositeDisposable _disposable = new();

        internal void SetAndSubscribeValue(IHeroPresenter heroPresenter)
        {
            _heroPresenter = heroPresenter;

            _heroPresenter.Stats.ObserveAdd().Subscribe(OnStatsAdd).AddTo(_disposable);
            _heroPresenter.Stats.ObserveRemove().Subscribe(OnStatsRemove).AddTo(_disposable);
            SetStatus(_heroPresenter.Stats);
        }

        private void OnStatsRemove(CollectionRemoveEvent<CharacterStat> removeEvent)
        {
            CharacterStat addItem = removeEvent.Value;
            for (int i = 0; i < _stats.Count; i++)
            {
                CharacterStat item = _stats[i];
                if (item.Name == addItem.Name)
                {
                    item.Label.gameObject.SetActive(false);
                    item.Point.gameObject.SetActive(false);
                    item.ValueLabel.gameObject.SetActive(false);
                }
            }
        }

        private void OnStatsAdd(CollectionAddEvent<CharacterStat> addEvent)
        {
            CharacterStat addItem = addEvent.Value;
            for (int i = 0; i < _stats.Count; i++)
            {
                CharacterStat item = _stats[i];
                if (item.Name == addItem.Name)
                {
                    item.Label.gameObject.SetActive(true);
                    item.Point.gameObject.SetActive(true);
                    item.ValueLabel.gameObject.SetActive(true);
                    item.ValueLabel.text = addItem.Value.ToString();
                }
            }
        }

        private void SetStatus(IReadOnlyReactiveCollection<CharacterStat> list)
        {
            List<CharacterStat> activeStats = list.Join(_stats,
                x => x.Name,
                y => y.Name,
                (x, y) =>
                {
                    y.Label.gameObject.SetActive(true);
                    y.Point.gameObject.SetActive(true);
                    y.ValueLabel.gameObject.SetActive(true);
                    y.ValueLabel.text = x.Value.ToString();
                    return y;

                }).ToList();
        }
    }
}
