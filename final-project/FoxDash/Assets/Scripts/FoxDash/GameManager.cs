using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;

using FoxDash.Characters;
using FoxDash.Collectables;
using FoxDash.TerrainGeneration;

namespace FoxDash
{
    public enum GameDeathReason
    {
        Unknown,
        Fall,
        Water,
        Spike,
        Saw,
        Mace,
        Obstacle
    }

    public sealed class GameManager : MonoBehaviour
    {
        public delegate void AudioEnabledHandler(bool active);

        public delegate void ScoreHandler(float newScore, float highScore, float lastScore);

        public delegate void ResetHandler();

        public static event ResetHandler OnReset;
        public static event ScoreHandler OnScoreChanged;
        public static event AudioEnabledHandler OnAudioEnabled;

        private static GameManager m_Singleton;
        private const string CoinPrefsKey = "FoxDash.Coin";
        private const string AudioEnabledPrefsKey = "FoxDash.AudioEnabled";
        private const string LastScorePrefsKey = "FoxDash.LastScore";
        private const string HighScorePrefsKey = "FoxDash.HighScore";

        public static GameManager Singleton
        {
            get
            {
                if (m_Singleton == null)
                {
                    m_Singleton = FindObjectOfType<GameManager>(true);
                }

                return m_Singleton;
            }
        }

        [SerializeField]
        private Character m_MainCharacter;
        [SerializeField]
        [TextArea(3, 30)]
        private string m_ShareText;
        [SerializeField]
        private string m_ShareUrl;
        private float m_StartScoreX = 0f;
        private float m_HighScore = 0f;
        private float m_LastScore = 0f;
        private float m_Score = 0f;
        private int m_RoundStartCoinCount = 0;
        private int m_LastRoundCoinCount = 0;
        private bool m_LastRunWasHighScore = false;
        private GameDeathReason m_DeathReason = GameDeathReason.Unknown;

        private bool m_GameStarted = false;
        private bool m_GameRunning = false;
        private bool m_AudioEnabled = true;

        /// <summary>
        /// Coin totals use Property wrappers so subscribed UI can refresh when values change.
        /// </summary>
        public Property<int> m_Coin = new Property<int>(0);


        #region Getters
        public bool gameStarted
        {
            get
            {
                return m_GameStarted;
            }
        }

        public bool gameRunning
        {
            get
            {
                return m_GameRunning;
            }
        }

        public bool audioEnabled
        {
            get
            {
                return m_AudioEnabled;
            }
        }

        public float currentScore
        {
            get
            {
                return m_Score;
            }
        }

        public float lastScore
        {
            get
            {
                return m_LastScore;
            }
        }

        public float highScore
        {
            get
            {
                return m_HighScore;
            }
        }

        public int totalCoins
        {
            get
            {
                return m_Coin.Value;
            }
        }

        public int currentRoundCoins
        {
            get
            {
                return Mathf.Max(0, m_Coin.Value - m_RoundStartCoinCount);
            }
        }

        public int lastRoundCoins
        {
            get
            {
                return m_LastRoundCoinCount;
            }
        }

        public bool lastRunWasHighScore
        {
            get
            {
                return m_LastRunWasHighScore;
            }
        }

        public GameDeathReason deathReason
        {
            get
            {
                return m_DeathReason;
            }
        }
        #endregion

        void Awake()
        {
            if (m_Singleton != null && m_Singleton != this)
            {
                Destroy(gameObject);
                return;
            }
            m_Singleton = this;
            m_Score = 0f;

            m_Coin.Value = PlayerPrefs.GetInt(CoinPrefsKey, 0);
            m_RoundStartCoinCount = m_Coin.Value;
            SetAudioEnabled(PlayerPrefs.GetInt(AudioEnabledPrefsKey, 1) == 1);
            m_LastScore = PlayerPrefs.GetFloat(LastScorePrefsKey, 0f);
            m_HighScore = PlayerPrefs.GetFloat(HighScorePrefsKey, 0f);

        }

        /// <summary>
        /// Responds to character death-state changes and starts or stops the result flow.
        /// </summary>
        void UpdateDeathEvent(bool isDead)
        {
            if (isDead)
            {
                StartCoroutine(DeathCrt());
            }
            else
            {
                StopCoroutine("DeathCrt");
            }
        }

        /// <summary>
        /// Delays result handling after death, records the run score, updates best score, and opens the end screen.
        /// </summary>
        IEnumerator DeathCrt()
        {
            m_LastScore = m_Score;
            m_LastRoundCoinCount = currentRoundCoins;
            m_LastRunWasHighScore = m_Score > m_HighScore;
            if (m_Score > m_HighScore)
            {
                m_HighScore = m_Score;
            }
            SaveProgress();
            if (OnScoreChanged != null)
            {
                OnScoreChanged(m_Score, m_HighScore, m_LastScore);
            }

            yield return new WaitForSecondsRealtime(1.5f);

            EndGame();
            var endScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.END_SCREEN);
            UIManager.Singleton.OpenScreen(endScreen);
        }

        /// <summary>
        /// Binds character death events after scene startup and records the score baseline position.
        /// </summary>
        private void Start()
        {
            EnsureMainCharacter();
            if (m_MainCharacter != null)
            {
                if (m_MainCharacter.IsDead == null)
                {
                    m_MainCharacter.IsDead = new Property<bool>(false);
                }

                m_MainCharacter.IsDead.AddEventAndFire(UpdateDeathEvent, this);
                m_StartScoreX = m_MainCharacter.transform.position.x;
            }
            else
            {
                Debug.LogError("GameManager could not find a main character in the scene.");
            }

            if (UIManager.Singleton != null)
            {
                Init();
            }
            else
            {
                Debug.LogError("GameManager could not find a UIManager in the scene.");
            }
        }

        private void EnsureMainCharacter()
        {
            if (m_MainCharacter == null)
            {
                m_MainCharacter = FindObjectOfType<Character>(true);
            }
        }

        /// <summary>
        /// Reinitialises a run by pausing gameplay, resetting UI, and showing the loading/start flow.
        /// </summary>
        public void Init()
        {
            EndGame();
            UIManager.Singleton.Init();
            StartCoroutine(Load());
        }

        /// <summary>
        /// Advances score from the character X position during gameplay and only allows score to increase.
        /// </summary>
        void Update()
        {
            EnsureMainCharacter();
            if (m_MainCharacter == null)
            {
                return;
            }

            if (m_GameRunning)
            {
                if (m_MainCharacter.transform.position.x > m_StartScoreX && m_MainCharacter.transform.position.x > m_Score)
                {
                    m_Score = m_MainCharacter.transform.position.x;
                    if (OnScoreChanged != null)
                    {
                        OnScoreChanged(m_Score, m_HighScore, m_LastScore);
                    }
                }
            }
        }

        /// <summary>
        /// Keeps the loading screen visible briefly before opening the start screen.
        /// </summary>
        IEnumerator Load()
        {
            var startScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.START_SCREEN);
            yield return new WaitForSecondsRealtime(3f);
            UIManager.Singleton.OpenScreen(startScreen);
        }

        /// <summary>
        /// Saves coins, last score, and best score before the game quits.
        /// </summary>
        void OnApplicationQuit()
        {
            if (m_Score > m_HighScore)
            {
                m_HighScore = m_Score;
            }
            if (m_GameStarted && m_Score > 0f)
            {
                m_LastScore = m_Score;
            }
            SaveProgress();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void ToggleAudioEnabled()
        {
            SetAudioEnabled(!m_AudioEnabled);
        }

        /// <summary>
        /// Syncs the audio toggle to global volume and notifies UI buttons to refresh.
        /// </summary>
        public void SetAudioEnabled(bool active)
        {
            m_AudioEnabled = active;
            AudioListener.volume = active ? 1f : 0f;
            if (OnAudioEnabled != null)
            {
                OnAudioEnabled(active);
            }
        }

        /// <summary>
        /// Enters the active gameplay state from the start screen.
        /// </summary>
        public void StartGame()
        {
            if (!m_GameStarted)
            {
                m_Score = 0f;
                m_RoundStartCoinCount = m_Coin.Value;
                m_LastRoundCoinCount = 0;
                m_LastRunWasHighScore = false;
                m_DeathReason = GameDeathReason.Unknown;
            }
            m_GameStarted = true;
            ResumeGame();
        }

        /// <summary>
        /// Pauses game time for pause screens and result handling.
        /// </summary>
        public void StopGame()
        {
            m_GameRunning = false;
            Time.timeScale = 0f;
        }

        /// <summary>
        /// Resumes game time.
        /// </summary>
        public void ResumeGame()
        {
            m_GameRunning = true;
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Ends the current run, clears the started flag, and pauses gameplay.
        /// </summary>
        public void EndGame()
        {
            m_GameStarted = false;
            StopGame();
        }

        public void ReturnHome()
        {
            Reset();
            Init();
        }

        public void RespawnMainCharacter()
        {
            RespawnCharacter(m_MainCharacter);
        }

        /// <summary>
        /// Places the character above the current terrain block to avoid respawning off-platform.
        /// </summary>
        public void RespawnCharacter(Character character)
        {
            Block block = TerrainGenerator.Singleton.GetCharacterBlock();
            if (block != null)
            {
                Vector3 position = block.transform.position;
                position.y += 2.56f;
                position.x += 1.28f;
                character.transform.position = position;
                character.Reset();
            }
        }

        /// <summary>
        /// Resets the current run score and broadcasts reset events to character, terrain, and UI systems.
        /// </summary>
        public void Reset()
        {
            m_Score = 0f;
            m_RoundStartCoinCount = m_Coin.Value;
            m_LastRoundCoinCount = 0;
            m_LastRunWasHighScore = false;
            m_DeathReason = GameDeathReason.Unknown;
            if (OnReset != null)
            {
                OnReset();
            }
        }

        public void SetDeathReason(GameDeathReason reason)
        {
            if (reason == GameDeathReason.Unknown)
            {
                return;
            }

            m_DeathReason = reason;
        }

        public string GetDeathReasonText()
        {
            switch (m_DeathReason)
            {
                case GameDeathReason.Fall:
                    return "You fell off the platforms.";
                case GameDeathReason.Water:
                    return "You landed in the water.";
                case GameDeathReason.Spike:
                    return "You hit the spikes.";
                case GameDeathReason.Saw:
                    return "You touched a saw blade.";
                case GameDeathReason.Mace:
                    return "You were crushed by a mace.";
                case GameDeathReason.Obstacle:
                    return "You hit an obstacle.";
                default:
                    return "The run ended.";
            }
        }

        private void SaveProgress()
        {
            PlayerPrefs.SetInt(CoinPrefsKey, m_Coin.Value);
            PlayerPrefs.SetInt(AudioEnabledPrefsKey, m_AudioEnabled ? 1 : 0);
            PlayerPrefs.SetFloat(LastScorePrefsKey, m_LastScore);
            PlayerPrefs.SetFloat(HighScorePrefsKey, m_HighScore);
            PlayerPrefs.Save();
        }

        public void ShareOnTwitter()
        {
            Share("https://twitter.com/intent/tweet?text={0}&url={1}");
        }

        public void ShareOnGooglePlus()
        {
            Share("https://plus.google.com/share?text={0}&href={1}");
        }

        public void ShareOnFacebook()
        {
            Share("https://www.facebook.com/sharer/sharer.php?u={1}");
        }

        public void Share(string url)
        {
            Application.OpenURL(string.Format(url, m_ShareText, m_ShareUrl));
        }

        [System.Serializable]
        public class LoadEvent : UnityEvent
        {

        }

    }

}
