using System;
using UnityEngine;

namespace Script.Scroll
{
    public class IngredientBank : MonoBehaviour
    {
        public RectTransform Zones;
        public RectTransform WorkspaceZone;
        public RectTransform AssemblyZone;
        [SerializeField] private GameObject spawnPosition;

        public Burger Burger;
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

        public void InstantiateIngredient(GameObject ingredient)
        {
            Instantiate(ingredient, spawnPosition.transform.position, Quaternion.identity, Zones.transform);
        }
    }
}