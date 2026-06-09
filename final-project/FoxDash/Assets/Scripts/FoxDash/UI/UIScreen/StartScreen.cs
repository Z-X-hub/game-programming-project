using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using FoxDash.Characters;

namespace FoxDash.UI
{
    public class StartScreen : UIScreen
    {
        [SerializeField]
        protected Button PlayButton = null;
        [SerializeField]
        protected Button HelpButton = null;
        [SerializeField]
        protected Button InfoButton = null;
        [SerializeField]
        protected Button ExitButton = null;

		private readonly List<Button> m_CharacterButtons = new List<Button> ();
		private static readonly Dictionary<string, Sprite> s_PortraitSpriteCache = new Dictionary<string, Sprite> ();
		private static Sprite s_CoverSprite = null;
		private const string GameTitle = "FOX DASH";
		private const string CoverResourcePath = "FoxDash/HomeCover/fox_dash_cover";
		private Font m_SelectorFont;
		private Transform m_SelectorRoot;
		private Transform m_TutorialRoot;
		private Transform m_HomePolishRoot;
		private bool m_ButtonActionsReady = false;

        private void Start()
        {
			SetupButtonActions ();
			HideLegacyHomeObjects ();
			CreateHomePolish ();
			CreateCharacterSelector ();
			CreateTutorialPanel ();
			LayoutHomeButtons ();
			ApplySelectedRoleToCharacter ();
        }

		private void SetupButtonActions ()
		{
			if ( m_ButtonActionsReady )
			{
				return;
			}

			m_ButtonActionsReady = true;
			if ( PlayButton != null )
			{
				PlayButton.SetButtonAction ( () =>
				{
					ApplySelectedRoleToCharacter ();
					var uiManager = UIManager.Singleton;
					if ( uiManager == null )
					{
						return;
					}

					var inGameScreen = uiManager.UISCREENS.Find ( el => el.ScreenInfo == UIScreenInfo.IN_GAME_SCREEN );
					if ( inGameScreen != null )
					{
						uiManager.OpenScreen ( inGameScreen );
						GameManager.Singleton.StartGame ();
					}
				} );
			}

			if ( ExitButton != null )
			{
				ExitButton.SetButtonAction ( () =>
				{
					GameManager.Singleton.ExitGame ();
				} );
			}
		}

		private void Update ()
		{
			if ( !IsOpen )
			{
				return;
			}

			if ( Input.GetKeyDown ( KeyCode.Alpha1 ) || Input.GetKeyDown ( KeyCode.Keypad1 ) )
			{
				SelectRole ( PlayerCharacterRole.Runner );
			}
			else if ( Input.GetKeyDown ( KeyCode.Alpha2 ) || Input.GetKeyDown ( KeyCode.Keypad2 ) )
			{
				SelectRole ( PlayerCharacterRole.Knight );
			}
			else if ( Input.GetKeyDown ( KeyCode.Alpha3 ) || Input.GetKeyDown ( KeyCode.Keypad3 ) )
			{
				SelectRole ( PlayerCharacterRole.Monkey );
			}
		}

		private void CreateCharacterSelector ()
		{
			if ( m_SelectorRoot == null )
			{
				m_SelectorRoot = FindGeneratedRoot ( "Character Selector" );
			}
			if ( m_SelectorRoot != null )
			{
				m_SelectorRoot.gameObject.SetActive ( true );
				return;
			}

			m_SelectorFont = ResolveSelectorFont ();
			GameObject root = new GameObject ( "Character Selector", typeof ( RectTransform ), typeof ( Image ) );
			root.transform.SetParent ( ScreenContentParent, false );
			m_SelectorRoot = root.transform;

			RectTransform rootRect = root.GetComponent<RectTransform> ();
			rootRect.anchorMin = new Vector2 ( 0f, 0.5f );
			rootRect.anchorMax = new Vector2 ( 0f, 0.5f );
			rootRect.pivot = new Vector2 ( 0f, 0.5f );
			rootRect.sizeDelta = new Vector2 ( 206f, 198f );
			rootRect.anchoredPosition = new Vector2 ( 22f, -70f );

			Image panel = root.GetComponent<Image> ();
			panel.color = new Color ( 0.03f, 0.08f, 0.1f, 0.04f );
			panel.raycastTarget = false;

			Shadow panelShadow = root.AddComponent<Shadow> ();
			panelShadow.effectColor = new Color ( 0f, 0f, 0f, 0.18f );
			panelShadow.effectDistance = new Vector2 ( 0f, -5f );

			CreateLabel ( root.transform );
			CreateRoleButton ( root.transform, PlayerCharacterRole.Runner, 0f, 43f );
			CreateRoleButton ( root.transform, PlayerCharacterRole.Knight, 0f, -12f );
			CreateRoleButton ( root.transform, PlayerCharacterRole.Monkey, 0f, -67f );
			RefreshCharacterButtons ();
		}

		private void CreateTutorialPanel ()
		{
			if ( m_TutorialRoot == null )
			{
				m_TutorialRoot = FindGeneratedRoot ( "Quick Tutorial Panel" );
			}
			if ( m_TutorialRoot != null )
			{
				m_TutorialRoot.gameObject.SetActive ( true );
				return;
			}

			if ( m_SelectorFont == null )
			{
				m_SelectorFont = ResolveSelectorFont ();
			}

			GameObject root = new GameObject ( "Quick Tutorial Panel", typeof ( RectTransform ), typeof ( Image ) );
			root.transform.SetParent ( ScreenContentParent, false );
			m_TutorialRoot = root.transform;

			RectTransform rootRect = root.GetComponent<RectTransform> ();
			rootRect.anchorMin = new Vector2 ( 0f, 0f );
			rootRect.anchorMax = new Vector2 ( 0f, 0f );
			rootRect.pivot = new Vector2 ( 0f, 0f );
			rootRect.sizeDelta = new Vector2 ( 420f, 104f );
			rootRect.anchoredPosition = new Vector2 ( 22f, 18f );

			Image panel = root.GetComponent<Image> ();
			panel.color = new Color ( 0.02f, 0.08f, 0.1f, 0.58f );
			panel.raycastTarget = false;

			Shadow panelShadow = root.AddComponent<Shadow> ();
			panelShadow.effectColor = new Color ( 0f, 0f, 0f, 0.28f );
			panelShadow.effectDistance = new Vector2 ( 0f, -4f );

			CreateTutorialTitle ( root.transform );
			CreateTutorialText ( root.transform );
		}

		private void CreateTutorialTitle ( Transform parent )
		{
			GameObject titleObject = new GameObject ( "Tutorial Title", typeof ( RectTransform ), typeof ( Text ) );
			titleObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = titleObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0f, 1f );
			rectTransform.anchorMax = new Vector2 ( 1f, 1f );
			rectTransform.pivot = new Vector2 ( 0.5f, 1f );
			rectTransform.offsetMin = new Vector2 ( 14f, -26f );
			rectTransform.offsetMax = new Vector2 ( -14f, -6f );

			Text title = titleObject.GetComponent<Text> ();
			title.font = m_SelectorFont;
			title.text = "QUICK GUIDE";
			title.fontSize = 15;
			title.fontStyle = FontStyle.Bold;
			title.alignment = TextAnchor.MiddleLeft;
			title.color = new Color ( 1f, 0.95f, 0.62f, 1f );
			title.raycastTarget = false;

			Shadow shadow = titleObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.45f );
			shadow.effectDistance = new Vector2 ( 1f, -1f );
		}

		private void CreateTutorialText ( Transform parent )
		{
			GameObject textObject = new GameObject ( "Tutorial Text", typeof ( RectTransform ), typeof ( Text ) );
			textObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = textObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.offsetMin = new Vector2 ( 14f, 10f );
			rectTransform.offsetMax = new Vector2 ( -14f, -30f );

			Text text = textObject.GetComponent<Text> ();
			text.font = m_SelectorFont;
			text.text = "Run forward, jump gaps, collect coins, avoid traps.\nPLAYER: faster run  |  SOLDIER: 1 revive  |  ADVENTURER: double jump\nMove A/D or arrows  |  Jump Space/W/up  |  Roll Shift/S  |  Pause Esc";
			text.fontSize = 12;
			text.fontStyle = FontStyle.Bold;
			text.alignment = TextAnchor.UpperLeft;
			text.horizontalOverflow = HorizontalWrapMode.Wrap;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 9;
			text.resizeTextMaxSize = 12;
			text.color = Color.white;
			text.raycastTarget = false;

			Shadow shadow = textObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.35f );
			shadow.effectDistance = new Vector2 ( 1f, -1f );
		}

		private void LayoutHomeButtons ()
		{
			LayoutHomeButton ( PlayButton, new Vector2 ( -28f, 24f ), new Vector2 ( 78f, 78f ) );
			LayoutHomeButton ( InfoButton, new Vector2 ( -116f, 28f ), new Vector2 ( 48f, 48f ) );
			LayoutHomeButton ( HelpButton, new Vector2 ( -174f, 28f ), new Vector2 ( 48f, 48f ) );
			LayoutHomeButton ( ExitButton, new Vector2 ( -232f, 28f ), new Vector2 ( 48f, 48f ) );
		}

		private void LayoutHomeButton ( Button button, Vector2 anchoredPosition, Vector2 sizeDelta )
		{
			if ( button == null )
			{
				return;
			}

			RectTransform rectTransform = button.GetComponent<RectTransform> ();
			if ( rectTransform == null )
			{
				return;
			}

			rectTransform.anchorMin = new Vector2 ( 1f, 0f );
			rectTransform.anchorMax = new Vector2 ( 1f, 0f );
			rectTransform.pivot = new Vector2 ( 1f, 0f );
			rectTransform.anchoredPosition = anchoredPosition;
			rectTransform.sizeDelta = sizeDelta;
			rectTransform.localScale = Vector3.one;
			button.transform.SetAsLastSibling ();
		}

		private Font ResolveSelectorFont ()
		{
			Text existingText = GetComponentInChildren<Text> ( true );
			if ( existingText != null && existingText.font != null )
			{
				return existingText.font;
			}

			try
			{
				Font builtinFont = Resources.GetBuiltinResource<Font> ( "LegacyRuntime.ttf" );
				if ( builtinFont != null )
				{
					return builtinFont;
				}
			}
			catch
			{
			}

			return Font.CreateDynamicFontFromOSFont ( "Arial", 16 );
		}

		private void CreateLabel ( Transform parent )
		{
			GameObject labelObject = new GameObject ( "Character Label", typeof ( RectTransform ), typeof ( Text ) );
			labelObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = labelObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0.5f, 0.5f );
			rectTransform.anchorMax = new Vector2 ( 0.5f, 0.5f );
			rectTransform.pivot = new Vector2 ( 0.5f, 0.5f );
			rectTransform.sizeDelta = new Vector2 ( 178f, 22f );
			rectTransform.anchoredPosition = new Vector2 ( 103f, 83f );

			Text label = labelObject.GetComponent<Text> ();
			label.font = m_SelectorFont;
			label.text = "HERO";
			label.fontSize = 14;
			label.fontStyle = FontStyle.Bold;
			label.alignment = TextAnchor.MiddleCenter;
			label.color = new Color ( 1f, 0.98f, 0.9f, 1f );
			label.raycastTarget = false;

			Shadow shadow = labelObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.45f );
			shadow.effectDistance = new Vector2 ( 2f, -2f );
		}

		private void CreateRoleButton ( Transform parent, PlayerCharacterRole role, float x, float y )
		{
			GameObject buttonObject = new GameObject ( PlayerCharacterSelection.GetDisplayName ( role ) + " Button", typeof ( RectTransform ), typeof ( Image ), typeof ( Button ) );
			buttonObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = buttonObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0.5f, 0.5f );
			rectTransform.anchorMax = new Vector2 ( 0.5f, 0.5f );
			rectTransform.pivot = new Vector2 ( 0.5f, 0.5f );
			rectTransform.sizeDelta = new Vector2 ( 174f, 48f );
			rectTransform.anchoredPosition = new Vector2 ( 103f + x, y );

			Image image = buttonObject.GetComponent<Image> ();
			image.color = GetRoleColor ( role, false );

			Button button = buttonObject.GetComponent<Button> ();
			button.targetGraphic = image;
			button.transition = Selectable.Transition.None;
			ColorBlock colors = button.colors;
			colors.normalColor = Color.white;
			colors.highlightedColor = new Color ( 1f, 1f, 1f, 0.92f );
			colors.pressedColor = new Color ( 0.82f, 0.82f, 0.82f, 1f );
			colors.selectedColor = Color.white;
			button.colors = colors;
			button.onClick.AddListener ( () => SelectRole ( role ) );
			m_CharacterButtons.Add ( button );

			Shadow shadow = buttonObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.32f );
			shadow.effectDistance = new Vector2 ( 0f, -3f );

			CreateRolePortrait ( buttonObject.transform, role );
			CreateButtonText ( buttonObject.transform, role );
		}

		private void CreateRolePortrait ( Transform parent, PlayerCharacterRole role )
		{
			GameObject portraitObject = new GameObject ( "Portrait", typeof ( RectTransform ), typeof ( Image ) );
			portraitObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = portraitObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0f, 0.5f );
			rectTransform.anchorMax = new Vector2 ( 0f, 0.5f );
			rectTransform.pivot = new Vector2 ( 0.5f, 0.5f );
			rectTransform.sizeDelta = new Vector2 ( 36f, 46f );
			rectTransform.anchoredPosition = new Vector2 ( 28f, 1f );

			Image image = portraitObject.GetComponent<Image> ();
			image.sprite = GetRolePortraitSprite ( role );
			image.preserveAspect = true;
			image.raycastTarget = false;
			image.color = Color.white;
		}

		private void CreateButtonText ( Transform parent, PlayerCharacterRole role )
		{
			GameObject textObject = new GameObject ( "Text", typeof ( RectTransform ), typeof ( Text ) );
			textObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = textObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.offsetMin = new Vector2 ( 52f, 5f );
			rectTransform.offsetMax = new Vector2 ( -7f, -5f );

			Text text = textObject.GetComponent<Text> ();
			text.font = m_SelectorFont;
			text.text = PlayerCharacterSelection.GetDisplayName ( role ) + "\n" + PlayerCharacterSelection.GetAbilityText ( role );
			text.fontSize = 12;
			text.fontStyle = FontStyle.Bold;
			text.alignment = TextAnchor.MiddleLeft;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 8;
			text.resizeTextMaxSize = 13;
			text.color = Color.white;
			text.raycastTarget = false;

			Shadow shadow = textObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.28f );
			shadow.effectDistance = new Vector2 ( 1f, -1f );
		}

		private void SelectRole ( PlayerCharacterRole role )
		{
			PlayerCharacterSelection.SelectedRole = role;
			if ( AudioManager.Singleton != null )
			{
				AudioManager.Singleton.PlayClickSound ();
			}
			RefreshCharacterButtons ();
			ApplySelectedRoleToCharacter ();
		}

		private void RefreshCharacterButtons ()
		{
			for ( int i = 0; i < m_CharacterButtons.Count; i++ )
			{
				Button button = m_CharacterButtons [ i ];
				if ( button == null )
				{
					continue;
				}

				PlayerCharacterRole role = ( PlayerCharacterRole )i;
				bool selected = role == PlayerCharacterSelection.SelectedRole;
				Image image = button.GetComponent<Image> ();
				if ( image != null )
				{
					image.color = GetRoleColor ( role, selected );
				}

				Text[] texts = button.GetComponentsInChildren<Text> ( true );
				for ( int textIndex = 0; textIndex < texts.Length; textIndex++ )
				{
					texts [ textIndex ].color = selected ? new Color ( 0.08f, 0.08f, 0.1f, 1f ) : Color.white;
				}
			}
		}

		private Color GetRoleColor ( PlayerCharacterRole role, bool selected )
		{
			Color color;
			switch ( role )
			{
				case PlayerCharacterRole.Knight:
					color = new Color ( 0.33f, 0.6f, 1f, 0.9f );
					break;
				case PlayerCharacterRole.Monkey:
					color = new Color ( 0.92f, 0.58f, 0.24f, 0.9f );
					break;
				default:
					color = new Color ( 1f, 0.25f, 0.25f, 0.9f );
					break;
			}

			if ( selected )
			{
				color = Color.Lerp ( color, new Color ( 1f, 0.95f, 0.58f, 1f ), 0.52f );
				color.a = 1f;
			}

			return color;
		}

		private void CreateHomePolish ()
		{
			if ( m_HomePolishRoot == null )
			{
				m_HomePolishRoot = FindGeneratedRoot ( "Fox Dash Home Polish" );
			}
			if ( m_HomePolishRoot != null )
			{
				m_HomePolishRoot.gameObject.SetActive ( true );
				return;
			}

			if ( m_SelectorFont == null )
			{
				m_SelectorFont = ResolveSelectorFont ();
			}

			GameObject root = new GameObject ( "Fox Dash Home Polish", typeof ( RectTransform ) );
			root.transform.SetParent ( ScreenContentParent, false );
			m_HomePolishRoot = root.transform;

			RectTransform rectTransform = root.GetComponent<RectTransform> ();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;
			root.transform.SetAsFirstSibling ();

			CreateCoverBackground ( root.transform );
			CreateTopRibbon ( root.transform );
			CreateGameTitle ( root.transform );
			CreateBottomFade ( root.transform );
		}

		private void CreateCoverBackground ( Transform parent )
		{
			Sprite coverSprite = GetCoverSprite ();
			if ( coverSprite == null )
			{
				return;
			}

			GameObject coverObject = new GameObject ( "Fox Dash Cover Art", typeof ( RectTransform ), typeof ( Image ), typeof ( AspectRatioFitter ) );
			coverObject.transform.SetParent ( parent, false );
			coverObject.transform.SetAsFirstSibling ();

			RectTransform rectTransform = coverObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.pivot = new Vector2 ( 0.5f, 0.5f );
			rectTransform.offsetMin = Vector2.zero;
			rectTransform.offsetMax = Vector2.zero;

			Image image = coverObject.GetComponent<Image> ();
			image.sprite = coverSprite;
			image.color = Color.white;
			image.raycastTarget = false;

			AspectRatioFitter fitter = coverObject.GetComponent<AspectRatioFitter> ();
			fitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
			fitter.aspectRatio = coverSprite.rect.width / coverSprite.rect.height;
		}

		private void CreateTopRibbon ( Transform parent )
		{
			GameObject ribbonObject = new GameObject ( "Title Ribbon", typeof ( RectTransform ), typeof ( Image ) );
			ribbonObject.transform.SetParent ( parent, false );
			ribbonObject.transform.SetAsFirstSibling ();

			RectTransform rectTransform = ribbonObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0.5f, 1f );
			rectTransform.anchorMax = new Vector2 ( 0.5f, 1f );
			rectTransform.pivot = new Vector2 ( 0.5f, 1f );
			rectTransform.sizeDelta = new Vector2 ( 700f, 104f );
			rectTransform.anchoredPosition = new Vector2 ( 0f, -18f );

			Image image = ribbonObject.GetComponent<Image> ();
			image.color = new Color ( 0.02f, 0.08f, 0.12f, 0.04f );
			image.raycastTarget = false;

			Shadow shadow = ribbonObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.16f );
			shadow.effectDistance = new Vector2 ( 0f, -6f );
		}

		private void CreateGameTitle ( Transform parent )
		{
			GameObject titleObject = new GameObject ( "Fox Dash Title", typeof ( RectTransform ), typeof ( Text ) );
			titleObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = titleObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0.5f, 1f );
			rectTransform.anchorMax = new Vector2 ( 0.5f, 1f );
			rectTransform.pivot = new Vector2 ( 0.5f, 1f );
			rectTransform.sizeDelta = new Vector2 ( 760f, 104f );
			rectTransform.anchoredPosition = new Vector2 ( 0f, -30f );

			Text title = titleObject.GetComponent<Text> ();
			title.font = m_SelectorFont;
			title.text = GameTitle;
			title.fontSize = 82;
			title.fontStyle = FontStyle.Bold;
			title.alignment = TextAnchor.MiddleCenter;
			title.color = new Color ( 1f, 0.95f, 0.7f, 1f );
			title.raycastTarget = false;

			Outline outline = titleObject.AddComponent<Outline> ();
			outline.effectColor = new Color ( 0.95f, 0.21f, 0.18f, 0.95f );
			outline.effectDistance = new Vector2 ( 5f, -5f );

			Shadow shadow = titleObject.AddComponent<Shadow> ();
			shadow.effectColor = new Color ( 0f, 0f, 0f, 0.35f );
			shadow.effectDistance = new Vector2 ( 0f, -8f );

			CreateTitleAccent ( parent, -134f, new Color ( 1f, 0.31f, 0.27f, 0.95f ) );
			CreateTitleAccent ( parent, 134f, new Color ( 1f, 0.83f, 0.23f, 0.95f ) );
		}

		private void CreateTitleAccent ( Transform parent, float x, Color color )
		{
			GameObject accentObject = new GameObject ( "Title Accent", typeof ( RectTransform ), typeof ( Image ) );
			accentObject.transform.SetParent ( parent, false );

			RectTransform rectTransform = accentObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0.5f, 1f );
			rectTransform.anchorMax = new Vector2 ( 0.5f, 1f );
			rectTransform.pivot = new Vector2 ( 0.5f, 0.5f );
			rectTransform.sizeDelta = new Vector2 ( 88f, 8f );
			rectTransform.anchoredPosition = new Vector2 ( x, -126f );

			Image image = accentObject.GetComponent<Image> ();
			image.color = color;
			image.raycastTarget = false;
		}

		private void CreateBottomFade ( Transform parent )
		{
			GameObject fadeObject = new GameObject ( "Bottom Calm Band", typeof ( RectTransform ), typeof ( Image ) );
			fadeObject.transform.SetParent ( parent, false );
			fadeObject.transform.SetAsFirstSibling ();

			RectTransform rectTransform = fadeObject.GetComponent<RectTransform> ();
			rectTransform.anchorMin = new Vector2 ( 0f, 0f );
			rectTransform.anchorMax = new Vector2 ( 1f, 0f );
			rectTransform.pivot = new Vector2 ( 0.5f, 0f );
			rectTransform.sizeDelta = new Vector2 ( 0f, 108f );
			rectTransform.anchoredPosition = Vector2.zero;

			Image image = fadeObject.GetComponent<Image> ();
			image.color = new Color ( 0.02f, 0.08f, 0.06f, 0.08f );
			image.raycastTarget = false;
		}

		private Sprite GetCoverSprite ()
		{
			if ( s_CoverSprite != null )
			{
				return s_CoverSprite;
			}

			Texture2D texture = Resources.Load<Texture2D> ( CoverResourcePath );
			if ( texture == null )
			{
				return null;
			}

			s_CoverSprite = Sprite.Create ( texture, new Rect ( 0f, 0f, texture.width, texture.height ), new Vector2 ( 0.5f, 0.5f ), 100f );
			s_CoverSprite.name = "Fox Dash Cover Art";
			return s_CoverSprite;
		}

		private Sprite GetRolePortraitSprite ( PlayerCharacterRole role )
		{
			string resourcePath;
			switch ( role )
			{
				case PlayerCharacterRole.Knight:
					resourcePath = "FoxDash/KenneyCharacters/Soldier/soldier_idle";
					break;
				case PlayerCharacterRole.Monkey:
					resourcePath = "FoxDash/KenneyCharacters/Adventurer/adventurer_idle";
					break;
				default:
					resourcePath = "FoxDash/KenneyCharacters/Player/player_idle";
					break;
			}

			Sprite cachedSprite;
			if ( s_PortraitSpriteCache.TryGetValue ( resourcePath, out cachedSprite ) )
			{
				return cachedSprite;
			}

			Texture2D texture = Resources.Load<Texture2D> ( resourcePath );
			if ( texture == null )
			{
				s_PortraitSpriteCache [ resourcePath ] = null;
				return null;
			}

			Sprite sprite = Sprite.Create ( texture, new Rect ( 0f, 0f, texture.width, texture.height ), new Vector2 ( 0.5f, 0.5f ), 100f );
			sprite.name = resourcePath;
			s_PortraitSpriteCache [ resourcePath ] = sprite;
			return sprite;
		}

		private void HideLegacyHomeObjects ()
		{
			Transform[] children = GetComponentsInChildren<Transform> ( true );
			for ( int i = 0; i < children.Length; i++ )
			{
				Transform child = children [ i ];
				if ( child == null || child == transform )
				{
					continue;
				}

				if ( ShouldHideHomeObject ( child ) )
				{
					child.gameObject.SetActive ( false );
				}
			}

			UIShareButtons[] shareButtons = GetComponentsInChildren<UIShareButtons> ( true );
			for ( int i = 0; i < shareButtons.Length; i++ )
			{
				if ( shareButtons [ i ] != null )
				{
					shareButtons [ i ].gameObject.SetActive ( false );
				}
			}
		}

		private bool ShouldHideHomeObject ( Transform child )
		{
			string lowerName = child.name.ToLowerInvariant ();
			if ( lowerName.Contains ( "twitter" ) ||
			     lowerName.Contains ( "facebook" ) ||
			     lowerName.Contains ( "google" ) ||
			     lowerName.Contains ( "share" ) )
			{
				return true;
			}

			if ( lowerName == "logo image" )
			{
				return true;
			}

			Image image = child.GetComponent<Image> ();
			if ( image != null && image.sprite != null )
			{
				string spriteName = image.sprite.name.ToLowerInvariant ();
				if ( spriteName.Contains ( "main logo" ) ||
				     spriteName.Contains ( "splash logo" ) ||
				     spriteName.Contains ( "red runner" ) )
				{
					return true;
				}
			}

			return false;
		}

		private Transform FindGeneratedRoot ( string rootName )
		{
			Transform[] children = GetComponentsInChildren<Transform> ( true );
			for ( int i = 0; i < children.Length; i++ )
			{
				if ( children [ i ] != null && children [ i ].name == rootName )
				{
					return children [ i ];
				}
			}

			return null;
		}

		private void ApplySelectedRoleToCharacter ()
		{
			RedCharacter character = FindObjectOfType<RedCharacter> ( true );
			if ( character != null )
			{
				character.ApplySelectedRole ();
			}
		}

        public override void UpdateScreenStatus(bool open)
        {
			if ( open )
			{
				SetGeneratedChildActive ( "Fox Dash Home Polish", true );
				SetGeneratedChildActive ( "Character Selector", true );
				SetGeneratedChildActive ( "Quick Tutorial Panel", true );
				SetupButtonActions ();
				HideLegacyHomeObjects ();
				CreateHomePolish ();
				CreateCharacterSelector ();
				CreateTutorialPanel ();
				LayoutHomeButtons ();
				RefreshCharacterButtons ();
				ApplySelectedRoleToCharacter ();
			}
			else
			{
				SetGeneratedChildActive ( "Fox Dash Home Polish", false );
				SetGeneratedChildActive ( "Character Selector", false );
				SetGeneratedChildActive ( "Quick Tutorial Panel", false );
			}

            base.UpdateScreenStatus(open);
        }
    }
}
