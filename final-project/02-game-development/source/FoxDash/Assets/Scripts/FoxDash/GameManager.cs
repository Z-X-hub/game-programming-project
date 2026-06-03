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
        /// 金币数量使用 Property 包装，UI 订阅后可以在数值变化时自动刷新。
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
        /// 响应角色死亡状态变化；死亡时进入结算协程，复活或重置时停止结算流程。
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
        /// 死亡后的延迟结算：锁定本局分数、更新最高分，再切换到结束界面。
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
        /// 场景启动后绑定角色死亡事件，并记录分数计算的起点位置。
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
        /// 重新初始化一局游戏：先停住游戏，再初始化 UI 并显示加载/开始界面。
        /// </summary>
        public void Init()
        {
            EndGame();
            UIManager.Singleton.Init();
            StartCoroutine(Load());
        }

        /// <summary>
        /// 游戏运行时用角色的 X 坐标推进分数，只允许分数向前增长。
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
        /// 给加载界面保留短暂停留时间，然后打开开始界面。
        /// </summary>
        IEnumerator Load()
        {
            var startScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.START_SCREEN);
            yield return new WaitForSecondsRealtime(3f);
            UIManager.Singleton.OpenScreen(startScreen);
        }

        /// <summary>
        /// 退出游戏前保存金币、最后分数和最高分。
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
        /// 同步音频开关到全局音量，并通知 UI 按钮刷新状态。
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
        /// 从开始界面进入游戏状态。
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
        /// 暂停游戏时间流动，供暂停界面和结算流程使用。
        /// </summary>
        public void StopGame()
        {
            m_GameRunning = false;
            Time.timeScale = 0f;
        }

        /// <summary>
        /// 恢复游戏时间流动。
        /// </summary>
        public void ResumeGame()
        {
            m_GameRunning = true;
            Time.timeScale = 1f;
        }

        /// <summary>
        /// 结束当前局，清除已开始状态并停住游戏。
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
        /// 把角色放回当前所在地形块上方，避免复活后掉出平台。
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
        /// 重置本局分数，并广播给角色、地形和 UI 做各自的重置。
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
