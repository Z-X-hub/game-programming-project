using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FoxDash.UI
{
    public class PauseScreen : UIScreen
    {
        [SerializeField]
        protected Button ResumeButton = null;
        [SerializeField]
        protected Button HomeButton = null;
        [SerializeField]
        protected Button SoundButton = null;
        [SerializeField]
        protected Button ExitButton = null;

        private Transform m_ActionRoot = null;
        private Font m_Font = null;
        private bool m_ButtonActionsReady = false;

        private void Start()
        {
            SetupButtonActions();
            CreatePauseActions();
        }

        private void SetupButtonActions()
        {
            if (m_ButtonActionsReady)
            {
                return;
            }

            m_ButtonActionsReady = true;

            if (ResumeButton != null)
            {
                ResumeButton.SetButtonAction(ResumeGame);
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

        private void ResumeGame()
        {
            var inGameScreen = UIManager.Singleton.UISCREENS.Find(el => el.ScreenInfo == UIScreenInfo.IN_GAME_SCREEN);
            UIManager.Singleton.OpenScreen(inGameScreen);
            GameManager.Singleton.ResumeGame();
        }

        private void RestartRun()
        {
            GameManager.Singleton.Reset();
            var inGameScreen = UIManager.Singleton.GetUIScreen(UIScreenInfo.IN_GAME_SCREEN);
            UIManager.Singleton.OpenScreen(inGameScreen);
            GameManager.Singleton.StartGame();
        }

        private void ReturnHome()
        {
            GameManager.Singleton.ReturnHome();
        }

        private void CreatePauseActions()
        {
            if (m_ActionRoot == null)
            {
                m_ActionRoot = FindGeneratedRoot("Pause Action Panel");
            }
            if (m_ActionRoot != null)
            {
                m_ActionRoot.gameObject.SetActive(true);
                return;
            }

            m_Font = ResolveFont();
            HideLegacyPauseWidgets();

            GameObject root = new GameObject("Pause Action Panel", typeof(RectTransform), typeof(Image));
            root.transform.SetParent(ScreenContentParent, false);
            m_ActionRoot = root.transform;

            RectTransform rootRect = root.GetComponent<RectTransform>();
            rootRect.anchorMin = new Vector2(0.5f, 0.5f);
            rootRect.anchorMax = new Vector2(0.5f, 0.5f);
            rootRect.pivot = new Vector2(0.5f, 0.5f);
            rootRect.sizeDelta = new Vector2(360f, 210f);
            rootRect.anchoredPosition = Vector2.zero;

            Image panel = root.GetComponent<Image>();
            panel.color = new Color(0.02f, 0.08f, 0.12f, 0.72f);
            panel.raycastTarget = true;

            Shadow shadow = root.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.35f);
            shadow.effectDistance = new Vector2(0f, -6f);

            CreatePanelTitle(root.transform, "PAUSED", new Vector2(0f, 76f));
            CreatePanelHint(root.transform, "Resume, restart this run, or go back to the hero menu.", new Vector2(0f, 46f));
            CreateActionButton(root.transform, "RESUME", new Vector2(-112f, -28f), new Color(0.3f, 0.68f, 1f, 0.95f), ResumeGame);
            CreateActionButton(root.transform, "RESTART", new Vector2(0f, -28f), new Color(1f, 0.74f, 0.22f, 0.95f), RestartRun);
            CreateActionButton(root.transform, "HOME", new Vector2(112f, -28f), new Color(1f, 0.34f, 0.3f, 0.95f), ReturnHome);
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

        private void CreatePanelTitle(Transform parent, string value, Vector2 position)
        {
            GameObject titleObject = new GameObject("Title", typeof(RectTransform), typeof(Text));
            titleObject.transform.SetParent(parent, false);

            RectTransform rectTransform = titleObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(320f, 34f);
            rectTransform.anchoredPosition = position;

            Text title = titleObject.GetComponent<Text>();
            title.font = m_Font;
            title.text = value;
            title.fontSize = 28;
            title.fontStyle = FontStyle.Bold;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(1f, 0.95f, 0.68f, 1f);
            title.raycastTarget = false;

            Shadow shadow = titleObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.45f);
            shadow.effectDistance = new Vector2(2f, -2f);
        }

        private void CreatePanelHint(Transform parent, string value, Vector2 position)
        {
            GameObject textObject = new GameObject("Hint", typeof(RectTransform), typeof(Text));
            textObject.transform.SetParent(parent, false);

            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(310f, 28f);
            rectTransform.anchoredPosition = position;

            Text text = textObject.GetComponent<Text>();
            text.font = m_Font;
            text.text = value;
            text.fontSize = 12;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 9;
            text.resizeTextMaxSize = 12;
            text.color = Color.white;
            text.raycastTarget = false;
        }

        private Button CreateActionButton(Transform parent, string label, Vector2 position, Color color, System.Action action)
        {
            GameObject buttonObject = new GameObject(label + " Button", typeof(RectTransform), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);

            RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.sizeDelta = new Vector2(96f, 48f);
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
            text.fontSize = 14;
            text.fontStyle = FontStyle.Bold;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;

            Shadow shadow = buttonObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0f, 0f, 0f, 0.28f);
            shadow.effectDistance = new Vector2(0f, -3f);

            return button;
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

        private void HideLegacyPauseWidgets()
        {
            Text[] texts = GetComponentsInChildren<Text>(true);
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i] != null && !IsChildOf(texts[i].transform, m_ActionRoot))
                {
                    texts[i].gameObject.SetActive(false);
                }
            }

            Button[] buttons = GetComponentsInChildren<Button>(true);
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != null && !IsChildOf(buttons[i].transform, m_ActionRoot))
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
        }

        public override void UpdateScreenStatus(bool open)
        {
            if (open)
            {
                SetGeneratedChildActive("Pause Action Panel", true);
                SetupButtonActions();
                CreatePauseActions();
            }
            else
            {
                SetGeneratedChildActive("Pause Action Panel", false);
            }

            base.UpdateScreenStatus(open);
        }
    }
}
