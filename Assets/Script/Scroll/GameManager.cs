using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Script.Scroll
{
    public class GameManager : MonoBehaviour
    {
        public int ReferenceWidth;
        public int ReferenceHeight;
        [Header("Delays")]
        public int DelayBetweenChars;
        public int DelayBeforeBoardUpdated = 2000;
        public int DelayAfterSpeech = 3000;
        public int DelayBeforeMoveToTable = 3000;
        
        
        [Header("Zones")]
        public RectTransform Zones;
        public RectTransform WorkspaceZone;
        public RectTransform AssemblyZone;
        public RectTransform TrashBinZone;
        [SerializeField] private GameObject spawnPosition;

        [Header("Objects")]
        public Burger Burger;
        public Pentagram Pentagram;
        public IngredientBoard IngredientBoard;

        [Header("Characters")] 
        public List<Character> Characters; 

        public Character CurrentCharacter { get; set; }
        private int currentCharIndex;
        public static GameManager Instance { get; private set; }

        public GameLoopManager GameLoopManager;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
            
            currentCharIndex = 0;
            CurrentCharacter = Characters[currentCharIndex];
        }

        private void Start()
        {
            
            GameLoopManager = new GameLoopManager();
            StartGame().Forget();
        }

        private async UniTask StartGame()
        {
            await UniTask.Delay(3000);
            GameLoopManager.StartSession();
        }

        public void SwitchCharacter()
        {
            if (currentCharIndex < Characters.Count-1)
            {
                currentCharIndex++;
                CurrentCharacter = Characters[currentCharIndex];
            }
            else
            {
                //todo: something on game end??????
                Debug.Log("All characters served");
            }
        }
        
        public void InstantiateIngredient(GameObject ingredient)
        {
            Instantiate(ingredient, spawnPosition.transform.position, Quaternion.identity, Zones.transform);
        }
    }
}