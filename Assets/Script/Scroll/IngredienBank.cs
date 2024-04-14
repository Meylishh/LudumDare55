using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Script.Scroll
{
    public class IngredientBank : MonoBehaviour
    {
        public RectTransform Zones;
        public RectTransform WorkspaceZone;
        public RectTransform AssemblyZone;
        public RectTransform TrashBinZone;
        [SerializeField] private GameObject spawnPosition;

        public Burger Burger;
        public Pentagram Pentagram;
        public static IngredientBank Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }

        private async UniTask IngredientAppearAsync(GameObject ingredient)
        {
            await ingredient.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
        public void InstantiateIngredient(GameObject ingredient)
        {
            Instantiate(ingredient, spawnPosition.transform.position, Quaternion.identity, Zones.transform);
            IngredientAppearAsync(ingredient).Forget();
        }
    }
}