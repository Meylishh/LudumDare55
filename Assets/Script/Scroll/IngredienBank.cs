using System;
using UnityEngine;

namespace Script.Scroll
{
    public class IngredientBank : MonoBehaviour
    {
        public RectTransform WorkspaceZone;
        public Vector2 spawnPosition;
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
            Instantiate(ingredient, spawnPosition, Quaternion.identity, WorkspaceZone.transform);
        }
    }
}