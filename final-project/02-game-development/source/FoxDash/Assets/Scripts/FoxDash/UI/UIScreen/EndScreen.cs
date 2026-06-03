using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FoxDash.Utilities;

namespace FoxDash.UI
{
    public class EndScreen : UIScreen
    {
        [SerializeField]
        protected Button ResetButton = null;
        [SerializeField]
        protected Button HomeButton = null;
        [SerializeField]
        protected Button ExitButton = null;

        private Transform m_ResultRoot = null;
        private Text m_ResultText = null;
        private Text m_RecordText = null;
        private Font m_Font = null;
        private bool m_ButtonActionsReady = false;

        private void Start()
        {
            SetupButtonActions();
            CreateResultPanel();
            RefreshResultPanel();
        }

        private void SetupButtonActions()
        {
            if (m_ButtonActionsReady)
            {
                return;
            }

            m_ButtonActionsReady = true;

            if (ResetButton != null)
            {
                ResetButton.SetButtonAction(RestartRun);
            }

            if (HomeButton != null)
            {
                HomeButton.SetButtonAction(ReturnHome);
            }

            if (ExitButton != null)
            {
                ExitButton.SetButtonAction(() =>
                {
                    GameManager.Singleton.ExitGame();
                });
            }
        }

        private void RestartRun()
        {
            GameManager.Singleton.Reset();
            var ingameScreen = UIManager.Singleton.GetUIScreen(UIScreenInfo.IN_GAME_SCREEN);
            UIManager.Singleton.OpenScreen(ingameScreen);
            GameManager.Singleton.StartGame();
        }

        private void ReturnHome()
        {
            GameManager.Singleton.ReturnHome();
        }

        private void CreateResultPanel()
        {
            if (m_ResultRoot == null)
            {
                m_ResultRoot = FindGeneratedRoot("Run Result Panel");
            }
            if (m_ResultRoot != null)
            {
                m_ResultRoot.gameObject.SetActive(true);
                return;
            }

            m_Font = ResolveFont();
            HideLegacyEndWidgets();

            GameObject root = new GameObject("Run Result Panel", typeof(RectTransform), typeof(Image));
            root.transform.SetParent(ScreenContentParent, false);
            root.transform.SetAsLastSibling();
            m_ResultRoot = root.transform;

            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.anchorMin = new Vector2(0.5f, 0.5f);
            rootRect.anchorMax = new Vector2(0.5f, 0.5f);
            rootRect.pivot = new Vector2(0.5f, 0.5f);
            rootRect.sizeDelta = new Vector2(430f, 300f);
            rootRect.anchoredPosition = new Vector2(0f, 4f);

            Image panel = root.GetComponent<Image>();
            panel.color = new Color(0.02f, 0.08f, 0.12f, 0.76f);
            panel.raycastTarget = true;

            Shadow panelShadow = root.AddComponent<Shadow>();
            panelShadow.effectColor = new Color(0f, 0f, 0f, 0.38f);
            panelShadow.effectDistance = new Vector2(0f, -6f);

            CreateTitle(root.transform);
            CreateRecordText(root.transform);
            CreateSummaryText(root.transform);
            CreateActionButton(root.transform, "RESTART", new Vector2(-82f, -116f), new Color(1f, 0.36f, 0.32f, 0.96f), RestartRun);
            CreateActionButton(root.transform, "HOME", new Vector2(82f, -116f), new Color(0.3f, 0.66f, 1f, 0.96f), ReturnHome);
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

        private void CreateTitle(Transform parent)
        {
            GameObject titleObject = new GameObject("Result Title", typeof(RectTransform), typeof(Text));
            titleObject.transform.SetParent(parent, false);

            RectTransform rectTransform = titleObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(380f, 42f);
            rectTransform.anchoredPosition = new Vector2(0f, 116f);

            Text title = titleObject.GetComponent<Text>();
            title.font = m_Font;
            title.text = "RUN COMPLETE";
            title.fontSize = 30;
            title.fontStyle = FontStyle.Bold;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(1f, 0.95f, 0.68f, 1f);
            title.raycastTarget = false;

            Shadow shadow = titleObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.45f);
            shadow.effectDistance = new Vector2(2f, -2f);
        }

        private void CreateRecordText(Transform parent)
        {
            GameObject recordObject = new GameObject("Record Text", typeof(RectTransform), typeof(Text));
            recordObject.transform.SetParent(parent, false);

            RectTransform rectTransform = recordObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(360f, 26f);
            rectTransform.anchoredPosition = new Vector2(0f, 82f);

            m_RecordText = recordObject.GetComponent<Text>();
            m_RecordText.font = m_Font;
            m_RecordText.fontSize = 16;
            m_RecordText.fontStyle = FontStyle.Bold;
            m_RecordText.alignment = TextAnchor.MiddleCenter;
            m_RecordText.color = new Color(1f, 0.88f, 0.2f, 1f);
            m_RecordText.raycastTarget = false;
        }

        private void CreateSummaryText(Transform parent)
        {
            GameObject summaryObject = new GameObject("Run Summary Text", typeof(RectTransform), typeof(Text));
            summaryObject.transform.SetParent(parent, false);

            RectTransform rectTransform = summaryObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(370f, 132f);
            rectTransform.anchoredPosition = new Vector2(0f, 10f);

            m_ResultText = summaryObject.GetComponent<Text>();
            m_ResultText.font = m_Font;
            m_ResultText.fontSize = 17;
            m_ResultText.fontStyle = FontStyle.Bold;
            m_ResultText.alignment = TextAnchor.MiddleLeft;
            m_ResultText.horizontalOverflow = HorizontalWrapMode.Wrap;
            m_ResultText.verticalOverflow = VerticalWrapMode.Overflow;
            m_ResultText.resizeTextForBestFit = true;
            m_ResultText.resizeTextMinSize = 12;
            m_ResultText.resizeTextMaxSize = 17;
            m_ResultText.color = Color.white;
            m_ResultText.raycastTarget = false;
        }

        private Button CreateActionButton(Transform parent, string label, Vector2 position, Color color, System.Action action)
        {
            GameObject buttonObject = new GameObject(label + " Button", typeof(RectTransform), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);

            RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(132f, 48f);
            rectTransform.anchoredPosition = position;

            Image image = buttonObject.GetComponent<Image>();
            image.color = color;

            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;
            button.SetButtonAction(action);

            GameObject textObject = new GameObject("Text", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(buttonObject.transform, false);

            RectTransform textRect = textObject.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            Text text = textObject.GetComponent<Text>();
            text.font = m_Font;
            text.text = label;
            text.fontSize = 15;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;

            Shadow shadow = buttonObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.28f);
            shadow.effectDistance = new Vector2(0f, -3f);

            return button;
        }

        private void RefreshResultPanel()
        {
            if (m_ResultText == null || m_RecordText == null || GameManager.Singleton == null)
            {
                return;
            }

            GameManager gameManager = GameManager.Singleton;
            m_RecordText.text = gameManager.lastRunWasHighScore ? "NEW BEST SCORE!" : "BEST: " + gameManager.highScore.ToLength();
            m_ResultText.text =
                "Reason: " + gameManager.GetDeathReasonText() + "\n" +
                "Score: " + gameManager.lastScore.ToLength() + "\n" +
                "High Score: " + gameManager.highScore.ToLength() + "\n" +
                "Coins This Run: x " + gameManager.lastRoundCoins + "\n" +
                "Total Coins: x " + gameManager.totalCoins;
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

        private bool IsChildOf(Transform child, Transform parent)
        {
            if (child == null || parent == null)
            {
                return false;
            }

            Transform current = child;
            while (current != null)
            {
                if (current == parent)
                {
                    return true;
                }

                current = current.parent;
            }

            return false;
        }

        private void HideLegacyEndWidgets()
        {
            Text[] texts = GetComponentsInChildren<Text>(true);
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i] != null && !IsChildOf(texts[i].transform, m_ResultRoot))
                {
                    texts[i].gameObject.SetActive(false);
                }
            }

            Button[] buttons = GetComponentsInChildren<Button>(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != null && !IsChildOf(buttons[i].transform, m_ResultRoot))
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

        public override void UpdateScreenStatus(bool open)
        {
            if (open)
            {
                SetGeneratedChildActive("Run Result Panel", true);
                SetupButtonActions();
                CreateResultPanel();
                RefreshResultPanel();
            }
            else
            {
                SetGeneratedChildActive("Run Result Panel", false);
            }

            base.UpdateScreenStatus(open);
        }
    }

}
