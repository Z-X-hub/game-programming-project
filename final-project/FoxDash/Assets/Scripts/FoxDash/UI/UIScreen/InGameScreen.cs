using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FoxDash.UI
{
    public class InGameScreen : UIScreen
    {
        [SerializeField]
        protected Button PauseButton = null;

        private bool m_ButtonActionReady = false;
        private bool m_CoinStatsReady = false;
        private Transform m_CoinStatsRoot = null;
        private Text m_RunCoinText = null;
        private Text m_TotalCoinText = null;

        private void Start()
        {
            SetupPauseButton();
            SetupCoinStats();
        }

        private void SetupPauseButton()
        {
            if (m_ButtonActionReady)
            {
                return;
            }

            if (PauseButton == null)
            {
                PauseButton = CreateFallbackPauseButton();
            }

            if (PauseButton == null)
            {
                return;
            }

            m_ButtonActionReady = true;
            PauseButton.SetButtonAction(() =>
            {
                var pauseScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.PAUSE_SCREEN);
                UIManager.Singleton.OpenScreen(pauseScreen);
                GameManager.Singleton.StopGame();
            });
        }

        private Button CreateFallbackPauseButton()
        {
            Font font = ResolveFont();
            GameObject buttonObject = new GameObject("Pause Button", typeof(RectTransform), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(transform, false);

            RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.pivot = new Vector2(1f, 1f);
            rectTransform.sizeDelta = new Vector2(48f, 40f);
            rectTransform.anchoredPosition = new Vector2(-16f, -16f);

            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.02f, 0.08f, 0.12f, 0.62f);

            GameObject textObject = new GameObject("Text", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(buttonObject.transform, false);

            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            Text text = textObject.GetComponent<Text>();
            text.font = font;
            text.text = "II";
            text.fontSize = 18;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;

            return buttonObject.GetComponent<Button>();
        }

        private void SetupCoinStats()
        {
            if (m_CoinStatsRoot == null)
            {
                m_CoinStatsRoot = FindGeneratedRoot("Run Coin Stats");
            }

            if (m_CoinStatsRoot == null)
            {
                CreateCoinStatsPanel();
            }
            else
            {
                m_CoinStatsRoot.gameObject.SetActive(IsOpen);
                CacheCoinStatsText();
            }

            if (!m_CoinStatsReady && GameManager.Singleton != null)
            {
                m_CoinStatsReady = true;
                GameManager.Singleton.m_Coin.AddEventAndFire(UpdateCoinStats, this, true);
            }
            else
            {
                UpdateCoinStats(0);
            }
        }

        private void CreateCoinStatsPanel()
        {
            Font font = ResolveFont();
            GameObject panelObject = new GameObject("Run Coin Stats", typeof(RectTransform), typeof(Image));
            panelObject.transform.SetParent(ScreenContentParent, false);
            m_CoinStatsRoot = panelObject.transform;

            RectTransform rectTransform = panelObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.pivot = new Vector2(1f, 1f);
            rectTransform.sizeDelta = new Vector2(188f, 66f);
            rectTransform.anchoredPosition = new Vector2(-72f, -14f);

            Image panel = panelObject.GetComponent<Image>();
            panel.color = new Color(0.02f, 0.08f, 0.12f, 0.56f);
            panel.raycastTarget = false;

            Shadow shadow = panelObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.28f);
            shadow.effectDistance = new Vector2(0f, -3f);

            m_RunCoinText = CreateCoinStatsText(panelObject.transform, "RUN COINS: x 0", new Vector2(0f, -7f), 15, font);
            m_TotalCoinText = CreateCoinStatsText(panelObject.transform, "TOTAL: x 0", new Vector2(0f, -35f), 13, font);
            panelObject.SetActive(IsOpen);
        }

        private Text CreateCoinStatsText(Transform parent, string label, Vector2 offset, int fontSize, Font font)
        {
            GameObject textObject = new GameObject(label, typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);

            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.offsetMin = new Vector2(10f, offset.y - 22f);
            rectTransform.offsetMax = new Vector2(-10f, offset.y);

            Text text = textObject.GetComponent<Text>();
            text.font = font;
            text.text = label;
            text.fontSize = fontSize;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleLeft;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 10;
            text.resizeTextMaxSize = fontSize;
            text.color = Color.white;
            text.raycastTarget = false;

            Shadow shadow = textObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.45f);
            shadow.effectDistance = new Vector2(1f, -1f);

            return text;
        }

        private void CacheCoinStatsText()
        {
            if (m_CoinStatsRoot == null)
            {
                return;
            }

            Text[] texts = m_CoinStatsRoot.GetComponentsInChildren<Text>(true);
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i] == null)
                {
                    continue;
                }

                if (texts[i].name.StartsWith("RUN COINS"))
                {
                    m_RunCoinText = texts[i];
                }
                else if (texts[i].name.StartsWith("TOTAL"))
                {
                    m_TotalCoinText = texts[i];
                }
            }
        }

        private Transform FindGeneratedRoot(string rootName)
        {
            Transform[] children = GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] != null && children[i].name == rootName)
                {
                    return children[i];
                }
            }

            return null;
        }

        private void UpdateCoinStats(int unusedCoinValue)
        {
            GameManager gameManager = GameManager.Singleton;
            if (gameManager == null)
            {
                return;
            }

            if (m_RunCoinText != null)
            {
                m_RunCoinText.text = "RUN COINS: x " + gameManager.currentRoundCoins;
            }

            if (m_TotalCoinText != null)
            {
                m_TotalCoinText.text = "TOTAL: x " + gameManager.totalCoins;
            }
        }

        private Font ResolveFont()
        {
            Text existingText = GetComponentInChildren<Text>(true);
            if (existingText != null && existingText.font != null)
            {
                return existingText.font;
            }

            try
            {
                Font builtinFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                if (builtinFont != null)
                {
                    return builtinFont;
                }
            }
            catch
            {
            }

            return Font.CreateDynamicFontFromOSFont("Arial", 16);
        }

        public override void UpdateScreenStatus(bool open)
        {
            if (open)
            {
                SetupPauseButton();
                SetupCoinStats();
                SetGeneratedChildActive("Run Coin Stats", true);
                UpdateCoinStats(0);
            }
            else
            {
                SetGeneratedChildActive("Run Coin Stats", false);
            }

            base.UpdateScreenStatus(open);
        }

        private void OnDestroy()
        {
            if (GameManager.Singleton != null)
            {
                GameManager.Singleton.m_Coin.RemoveEvent(UpdateCoinStats);
            }
        }
    }

}
