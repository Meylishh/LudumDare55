using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        [SerializeField] private int DelayBeforeEnding = 5000;
        [SerializeField] private int spawnPentagramChangeDelay = 300;
        
        
        [Header("Zones")]
        public RectTransform Zones;
        public RectTransform WorkspaceZone;
        public RectTransform AssemblyZone;
        public RectTransform TrashBinZone;
        [SerializeField] private GameObject spawnPosition;
        [SerializeField] private Sprite inactivePentagram;
        [SerializeField] private Sprite activePentagram;
        [SerializeField] private Image pentagram;

        [Header("Objects")]
        public Burger Burger;
        public Pentagram Pentagram;
        public IngredientBoard IngredientBoard;
        public ChangeScreen ChangeScreen;

        [Header("Characters")] 
        public List<Character> Characters;

        [Header("Endings")] 
        [SerializeField] private GameObject GoodEndingScreen;
        [SerializeField] private GameObject BadEndingScreen;
        public bool BadEnd { get; set; }
        public Character CurrentCharacter { get; set; }
        private int currentCharIndex;
        public static GameManager Instance { get; private set; }

        public static UnityAction<Character> OnOrderFinished;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            currentCharIndex = 0;
            CurrentCharacter = Characters[currentCharIndex];
        }

        private void Start()
        {
            pentagram.sprite = inactivePentagram;
            GoodEndingScreen.SetActive(false);
            BadEndingScreen.SetActive(false);
            
            StartOrder().Forget();
        }

        private async UniTask StartOrder()
        {
            await UniTask.Delay(DelayBetweenChars);
            await CurrentCharacter.CharacterAppearAsync();
        }

        public void SwitchCharacter()
        {
            OnOrderFinished?.Invoke(CurrentCharacter);
            
            if (currentCharIndex < Characters.Count-1)
            {
                currentCharIndex++;
                CurrentCharacter = Characters[currentCharIndex];
                
                Burger.correctIngredients = CurrentCharacter.CharacterOrder;
                StartOrder().Forget();
            }
            else
            {
                Debug.Log("All characters served");
                ShowEnding().Forget();
                
            }
        }
        private async UniTask ShowEnding()
        {
            await UniTask.Delay(DelayBeforeEnding);
            if (BadEnd)
            {
                AudioManager.Instance.PlayMusic("BadEnding");
                BadEndingScreen.SetActive(true);
            }
            else
            {
                AudioManager.Instance.PlayMusic("GoodEnding");
                GoodEndingScreen.SetActive(true);
            }
        }
        private async UniTask LightPentagramAsync()
        {
            pentagram.sprite = activePentagram;
            await UniTask.Delay(spawnPentagramChangeDelay);
            pentagram.sprite = inactivePentagram;
        }
        public async void InstantiateIngredient(GameObject ingredient)
        {
            LightPentagramAsync().Forget();
            var obj = Instantiate(ingredient, spawnPosition.transform.position, Quaternion.identity, Zones.transform);
            await obj.transform.DOScale(1.2f, 0.1f).SetLoops(2, LoopType.Yoyo).ToUniTask();
        }
    }
}