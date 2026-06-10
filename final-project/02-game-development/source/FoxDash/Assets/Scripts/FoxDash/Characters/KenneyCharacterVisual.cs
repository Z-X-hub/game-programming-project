using System.Collections.Generic;
using UnityEngine;

namespace FoxDash.Characters
{
	public class KenneyCharacterVisual : MonoBehaviour
	{
		private const string ResourceRoot = "FoxDash/KenneyCharacters";
		private const float SpritePixelsPerUnit = 100f;
		private const float BaseVisualScale = 2f;
		private const int MaxOptionalRunFrames = 128;
		private const int RunnerTrailCount = 3;
		private static readonly Dictionary<string, Sprite> s_SpriteCache = new Dictionary<string, Sprite> ();
		private static Sprite s_TrailSprite = null;
		private static Sprite s_LimbSprite = null;
		private static Sprite s_HandSprite = null;

		private readonly SpriteSet m_Sprites = new SpriteSet ();
		private readonly List<SpriteRenderer> m_RunnerTrailRenderers = new List<SpriteRenderer> ();
		private SpriteRenderer m_Renderer;
		private SpriteRenderer m_BackArmRenderer;
		private SpriteRenderer m_BackHandRenderer;
		private SpriteRenderer m_FrontArmRenderer;
		private SpriteRenderer m_FrontHandRenderer;
		private Transform m_VisualTransform;
		private Transform m_RunnerTrailRoot;
		private Transform m_ArmSwingRoot;
		private PlayerCharacterRole m_CurrentRole = PlayerCharacterRole.Player;
		private bool m_ShieldActive = false;
		private Sprite m_CurrentSprite;

		public bool Initialize ( SpriteRenderer[] sourceRenderers )
		{
			if ( m_Renderer == null )
			{
				GameObject visualObject = new GameObject ( "Kenney Sprite" );
				visualObject.transform.SetParent ( transform, false );
				visualObject.transform.localPosition = new Vector3 ( 0f, -0.02f, 0f );
				visualObject.transform.localRotation = Quaternion.identity;
				visualObject.transform.localScale = new Vector3 ( BaseVisualScale, BaseVisualScale, 1f );

				m_VisualTransform = visualObject.transform;
				m_Renderer = visualObject.AddComponent<SpriteRenderer> ();
				CreateRunnerSpeedTrails ();
			}

			ApplySorting ( sourceRenderers );
			return m_Renderer != null;
		}

		public bool ApplyRole ( PlayerCharacterRole role )
		{
			m_CurrentRole = role;
			KenneyCharacterInfo info = GetCharacterInfo ( role );
			m_Sprites.Load ( info.folder, info.prefix );

			bool loaded = m_Sprites.idle != null || m_Sprites.stand != null;
			if ( m_Renderer != null )
			{
				m_Renderer.enabled = loaded;
			}

			if ( loaded )
			{
				SetSprite ( m_Sprites.idle != null ? m_Sprites.idle : m_Sprites.stand );
				SetShieldActive ( false );
			}
			UpdateRunnerSpeedTrails ( 0f, true, false, false );

			return loaded;
		}

		public void SetShieldActive ( bool active )
		{
			m_ShieldActive = active;
			if ( m_Renderer == null )
			{
				return;
			}

			if ( m_CurrentRole == PlayerCharacterRole.Soldier && m_ShieldActive )
			{
				m_Renderer.color = new Color ( 1f, 0.92f, 0.35f, 1f );
			}
			else
			{
				m_Renderer.color = Color.white;
			}
		}

		public void UpdateVisual ( float speedX, float verticalVelocity, bool grounded, bool dead, bool rolling, float rollNormalized )
		{
			if ( m_Renderer == null || !m_Renderer.enabled )
			{
				return;
			}

			Sprite nextSprite = ResolveSprite ( speedX, verticalVelocity, grounded, dead, rolling );
			SetSprite ( nextSprite );
			ApplyPoseTransform ( speedX, verticalVelocity, grounded, dead, rolling, rollNormalized );
			UpdateRunnerSpeedTrails ( speedX, grounded, dead, rolling );
			SetShieldActive ( m_ShieldActive );
		}

		private Sprite ResolveSprite ( float speedX, float verticalVelocity, bool grounded, bool dead, bool rolling )
		{
			if ( dead )
			{
				return FirstAvailable ( m_Sprites.hurt, m_Sprites.fall, m_Sprites.idle, m_Sprites.stand );
			}

			if ( rolling )
			{
				int rollFrame = Mathf.FloorToInt ( Time.time * 18f ) % 2;
				return rollFrame == 0 ?
					FirstAvailable ( m_Sprites.slide, m_Sprites.duck, m_Sprites.skid, m_Sprites.idle, m_Sprites.stand ) :
					FirstAvailable ( m_Sprites.duck, m_Sprites.slide, m_Sprites.skid, m_Sprites.idle, m_Sprites.stand );
			}

			if ( !grounded )
			{
				if ( verticalVelocity >= -0.2f )
				{
					return FirstAvailable ( m_Sprites.jump, m_Sprites.fall, m_Sprites.idle, m_Sprites.stand );
				}

				return FirstAvailable ( m_Sprites.fall, m_Sprites.jump, m_Sprites.idle, m_Sprites.stand );
			}

			if ( speedX > 0.35f )
			{
				Sprite runnerRunSprite = GetRunnerRunSprite ( speedX );
				if ( runnerRunSprite != null )
				{
					return runnerRunSprite;
				}

				int walkFrame = Mathf.FloorToInt ( Time.time * GetGroundMoveFrameRate ( speedX ) ) % 2;
				return walkFrame == 0 ?
					FirstAvailable ( m_Sprites.walk1, m_Sprites.walk2, m_Sprites.stand, m_Sprites.idle ) :
					FirstAvailable ( m_Sprites.walk2, m_Sprites.walk1, m_Sprites.stand, m_Sprites.idle );
			}

			return FirstAvailable ( m_Sprites.idle, m_Sprites.stand );
		}

		private void ApplyPoseTransform ( float speedX, float verticalVelocity, bool grounded, bool dead, bool rolling, float rollNormalized )
		{
			if ( m_VisualTransform == null )
			{
				return;
			}

			Vector3 scale = new Vector3 ( BaseVisualScale, BaseVisualScale, 1f );
			Vector3 position = new Vector3 ( 0f, -0.02f, 0f );
			float rotationZ = 0f;

			if ( rolling )
			{
				rotationZ = -360f * rollNormalized;
				scale = new Vector3 ( BaseVisualScale * 0.94f, BaseVisualScale * 0.94f, 1f );
				position.y = -0.08f;
			}
			else if ( dead )
			{
				rotationZ = -12f;
				position.y = -0.06f;
			}
			else if ( !grounded )
			{
				float lift = Mathf.Clamp ( verticalVelocity * 0.015f, -0.04f, 0.05f );
				position.y += lift;
				rotationZ = verticalVelocity >= 0f ? -5f : 5f;
			}
			else if ( speedX > 0.35f )
			{
				ApplyGroundMovePose ( speedX, ref position, ref scale, ref rotationZ );
			}

			m_VisualTransform.localPosition = position;
			m_VisualTransform.localRotation = Quaternion.Euler ( 0f, 0f, rotationZ );
			m_VisualTransform.localScale = scale;
		}

		private void ApplyGroundMovePose ( float speedX, ref Vector3 position, ref Vector3 scale, ref float rotationZ )
		{
			float speedAmount = Mathf.Clamp01 ( speedX / 12f );
			float stridePhase = Time.time * GetGroundMoveFrameRate ( speedX ) * Mathf.PI;
			float stride = Mathf.Sin ( stridePhase );
			float lift = Mathf.Abs ( stride );

			switch ( m_CurrentRole )
			{
				case PlayerCharacterRole.Soldier:
					position.x += 0.01f + stride * 0.022f;
					position.y += lift * 0.02f;
					rotationZ = 0f;
					scale.x = BaseVisualScale * ( 1f + lift * 0.018f );
					scale.y = BaseVisualScale * ( 1f - lift * 0.012f );
					break;
				case PlayerCharacterRole.Adventurer:
					position.x += 0.025f + stride * 0.04f;
					position.y += 0.006f + lift * 0.045f;
					rotationZ = 0f;
					scale.x = BaseVisualScale * ( 1.02f + lift * 0.045f );
					scale.y = BaseVisualScale * ( 0.99f - lift * 0.028f );
					break;
				default:
					if ( m_Sprites.HasRunFrames )
					{
						position.x += 0.055f + speedAmount * 0.03f;
						position.y += 0.006f;
						rotationZ = 0f;
						scale = new Vector3 ( BaseVisualScale, BaseVisualScale, 1f );
						break;
					}

					position.x += 0.07f + speedAmount * 0.04f + stride * 0.055f;
					position.y += 0.012f + lift * 0.05f;
					rotationZ = 0f;
					scale.x = BaseVisualScale * ( 1.05f + lift * 0.055f );
					scale.y = BaseVisualScale * ( 0.97f - lift * 0.03f );
					break;
			}
		}

		private Sprite GetRunnerRunSprite ( float speedX )
		{
			if ( m_CurrentRole != PlayerCharacterRole.Player || !m_Sprites.HasRunFrames )
			{
				return null;
			}

			int frameIndex = Mathf.FloorToInt ( Time.time * GetRunnerRunFrameRate ( speedX ) ) % m_Sprites.runFrames.Length;
			return m_Sprites.runFrames [ frameIndex ];
		}

		private float GetRunnerRunFrameRate ( float speedX )
		{
			float speedAmount = Mathf.Clamp01 ( speedX / 12f );
			if ( m_Sprites.runFrames.Length >= 80 )
			{
				return 24f;
			}

			if ( m_Sprites.runFrames.Length >= 30 )
			{
				return Mathf.Lerp ( 24f, 30f, speedAmount );
			}

			return Mathf.Lerp ( 8.5f, 10.5f, speedAmount );
		}

		private float GetGroundMoveFrameRate ( float speedX )
		{
			float speedAmount = Mathf.Clamp01 ( speedX / 12f );
			switch ( m_CurrentRole )
			{
				case PlayerCharacterRole.Soldier:
					return Mathf.Lerp ( 2.2f, 2.9f, speedAmount );
				case PlayerCharacterRole.Adventurer:
					return Mathf.Lerp ( 2.8f, 3.8f, speedAmount );
				default:
					return Mathf.Lerp ( 3.4f, 4.6f, speedAmount );
			}
		}

		private void CreateArmSwingVisuals ()
		{
			if ( m_ArmSwingRoot != null )
			{
				return;
			}

			GameObject armRoot = new GameObject ( "Arm Swing" );
			armRoot.transform.SetParent ( transform, false );
			armRoot.transform.localPosition = Vector3.zero;
			armRoot.transform.localRotation = Quaternion.identity;
			armRoot.transform.localScale = Vector3.one;
			m_ArmSwingRoot = armRoot.transform;

			m_BackArmRenderer = CreateArmRenderer ( "Back Arm", GetLimbSprite () );
			m_BackHandRenderer = CreateArmRenderer ( "Back Hand", GetHandSprite () );
			m_FrontArmRenderer = CreateArmRenderer ( "Front Arm", GetLimbSprite () );
			m_FrontHandRenderer = CreateArmRenderer ( "Front Hand", GetHandSprite () );

			SetArmSwingActive ( false );
		}

		private SpriteRenderer CreateArmRenderer ( string objectName, Sprite sprite )
		{
			GameObject armObject = new GameObject ( objectName );
			armObject.transform.SetParent ( m_ArmSwingRoot, false );

			SpriteRenderer renderer = armObject.AddComponent<SpriteRenderer> ();
			renderer.sprite = sprite;
			renderer.enabled = false;
			return renderer;
		}

		private void UpdateArmSwing ( float speedX, bool grounded, bool dead, bool rolling )
		{
			bool active = grounded && !dead && !rolling && speedX > 0.35f;
			if ( m_ArmSwingRoot == null )
			{
				return;
			}

			SetArmSwingActive ( active );
			if ( !active )
			{
				return;
			}

			float speedAmount = Mathf.Clamp01 ( speedX / 12f );
			float stridePhase = Time.time * GetGroundMoveFrameRate ( speedX ) * Mathf.PI;
			float stride = Mathf.Sin ( stridePhase );
			float shoulderBob = Mathf.Abs ( stride ) * 0.018f;

			float baseAngle;
			float swingAmount;
			float backSwingRatio;
			float armLength;
			float shoulderY;
			float frontShoulderX;
			float backShoulderX;

			switch ( m_CurrentRole )
			{
				case PlayerCharacterRole.Soldier:
					baseAngle = -66f;
					swingAmount = Mathf.Lerp ( 20f, 28f, speedAmount );
					backSwingRatio = 0.7f;
					armLength = 1.28f;
					shoulderY = 0.06f;
					frontShoulderX = 0.18f;
					backShoulderX = -0.1f;
					break;
				case PlayerCharacterRole.Adventurer:
					baseAngle = -58f;
					swingAmount = Mathf.Lerp ( 30f, 42f, speedAmount );
					backSwingRatio = 0.85f;
					armLength = 1.36f;
					shoulderY = 0.06f;
					frontShoulderX = 0.19f;
					backShoulderX = -0.11f;
					break;
				default:
					baseAngle = -60f;
					swingAmount = Mathf.Lerp ( 38f, 52f, speedAmount );
					backSwingRatio = 0.9f;
					armLength = 1.48f;
					shoulderY = 0.07f;
					frontShoulderX = 0.2f;
					backShoulderX = -0.12f;
					break;
			}

			Vector2 frontShoulder = new Vector2 ( frontShoulderX, shoulderY + shoulderBob );
			Vector2 backShoulder = new Vector2 ( backShoulderX, shoulderY + shoulderBob * 0.6f );
			float frontAngle = baseAngle + stride * swingAmount;
			float backAngle = baseAngle - stride * swingAmount * backSwingRatio;

			ApplyArmPose ( m_BackArmRenderer, m_BackHandRenderer, backShoulder, backAngle, armLength * 0.92f, 0.92f );
			ApplyArmPose ( m_FrontArmRenderer, m_FrontHandRenderer, frontShoulder, frontAngle, armLength, 1f );
		}

		private void ApplyArmPose ( SpriteRenderer armRenderer, SpriteRenderer handRenderer, Vector2 shoulder, float angle, float length, float alphaMultiplier )
		{
			if ( armRenderer == null || handRenderer == null )
			{
				return;
			}

			Transform armTransform = armRenderer.transform;
			armTransform.localPosition = new Vector3 ( shoulder.x, shoulder.y, 0f );
			armTransform.localRotation = Quaternion.Euler ( 0f, 0f, angle );
			armTransform.localScale = new Vector3 ( length, 1f, 1f );

			float radians = angle * Mathf.Deg2Rad;
			float armWorldLength = 0.34f * length;
			Vector2 handPosition = shoulder + new Vector2 ( Mathf.Cos ( radians ), Mathf.Sin ( radians ) ) * armWorldLength;
			Transform handTransform = handRenderer.transform;
			handTransform.localPosition = new Vector3 ( handPosition.x, handPosition.y, 0f );
			handTransform.localRotation = Quaternion.identity;
			handTransform.localScale = new Vector3 ( 0.82f, 0.82f, 1f );

			Color armColor = armRenderer.color;
			armColor.a = Mathf.Min ( armColor.a, alphaMultiplier );
			armRenderer.color = armColor;

			Color handColor = handRenderer.color;
			handColor.a = Mathf.Min ( handColor.a, alphaMultiplier );
			handRenderer.color = handColor;
		}

		private void UpdateArmVisualColors ()
		{
			Color sleeveColor;
			Color handColor;
			switch ( m_CurrentRole )
			{
				case PlayerCharacterRole.Soldier:
					sleeveColor = new Color ( 0.36f, 0.32f, 0.24f, 0.96f );
					handColor = new Color ( 0.42f, 0.27f, 0.15f, 0.98f );
					break;
				case PlayerCharacterRole.Adventurer:
					sleeveColor = new Color ( 0.25f, 0.52f, 0.78f, 0.96f );
					handColor = new Color ( 0.96f, 0.72f, 0.5f, 0.98f );
					break;
				default:
					sleeveColor = new Color ( 0.25f, 0.76f, 0.37f, 0.96f );
					handColor = new Color ( 1f, 0.76f, 0.52f, 0.98f );
					break;
			}

			SetRendererColor ( m_BackArmRenderer, Darken ( sleeveColor, 0.78f ) );
			SetRendererColor ( m_BackHandRenderer, Darken ( handColor, 0.82f ) );
			SetRendererColor ( m_FrontArmRenderer, sleeveColor );
			SetRendererColor ( m_FrontHandRenderer, handColor );
		}

		private void SetArmSwingActive ( bool active )
		{
			SetRendererActive ( m_BackArmRenderer, active );
			SetRendererActive ( m_BackHandRenderer, active );
			SetRendererActive ( m_FrontArmRenderer, active );
			SetRendererActive ( m_FrontHandRenderer, active );
		}

		private void SetRendererActive ( SpriteRenderer renderer, bool active )
		{
			if ( renderer != null )
			{
				renderer.enabled = active;
			}
		}

		private void SetRendererColor ( SpriteRenderer renderer, Color color )
		{
			if ( renderer != null )
			{
				renderer.color = color;
			}
		}

		private Color Darken ( Color color, float multiplier )
		{
			return new Color ( color.r * multiplier, color.g * multiplier, color.b * multiplier, color.a * 0.92f );
		}

		private void CreateRunnerSpeedTrails ()
		{
			if ( m_RunnerTrailRoot != null )
			{
				return;
			}

			GameObject trailRoot = new GameObject ( "Runner Speed Trails" );
			trailRoot.transform.SetParent ( transform, false );
			trailRoot.transform.localPosition = Vector3.zero;
			trailRoot.transform.localRotation = Quaternion.identity;
			trailRoot.transform.localScale = Vector3.one;
			m_RunnerTrailRoot = trailRoot.transform;

			for ( int i = 0; i < RunnerTrailCount; i++ )
			{
				GameObject trailObject = new GameObject ( "Speed Trail " + ( i + 1 ) );
				trailObject.transform.SetParent ( m_RunnerTrailRoot, false );

				SpriteRenderer trailRenderer = trailObject.AddComponent<SpriteRenderer> ();
				trailRenderer.sprite = GetTrailSprite ();
				trailRenderer.color = new Color ( 1f, 0.95f, 0.45f, 0f );
				trailRenderer.enabled = false;
				m_RunnerTrailRenderers.Add ( trailRenderer );
			}
		}

		private void UpdateRunnerSpeedTrails ( float speedX, bool grounded, bool dead, bool rolling )
		{
			bool active = IsFastRunnerMoving ( speedX, grounded, dead, rolling );
			if ( m_RunnerTrailRenderers.Count <= 0 )
			{
				return;
			}

			float speedAmount = Mathf.Clamp01 ( speedX / 12f );
			for ( int i = 0; i < m_RunnerTrailRenderers.Count; i++ )
			{
				SpriteRenderer trailRenderer = m_RunnerTrailRenderers [ i ];
				if ( trailRenderer == null )
				{
					continue;
				}

				trailRenderer.enabled = active;
				if ( !active )
				{
					trailRenderer.color = new Color ( 1f, 0.95f, 0.45f, 0f );
					continue;
				}

				float cycle = Mathf.Repeat ( Time.time * Mathf.Lerp ( 2.4f, 3.8f, speedAmount ) + i * 0.28f, 1f );
				float alpha = ( 1f - cycle ) * Mathf.Lerp ( 0.14f, 0.3f, speedAmount );
				Transform trailTransform = trailRenderer.transform;
				trailTransform.localPosition = new Vector3 (
					-0.44f - cycle * 0.36f - i * 0.08f,
					-0.18f + i * 0.13f + Mathf.Sin ( Time.time * 8f + i ) * 0.012f,
					0f );
				trailTransform.localRotation = Quaternion.Euler ( 0f, 0f, -6f + i * 4f );
				trailTransform.localScale = new Vector3 (
					Mathf.Lerp ( 3.2f, 5.6f, speedAmount ) + cycle * 1.6f,
					0.16f + i * 0.05f,
					1f );
				trailRenderer.color = new Color ( 1f, 0.93f, 0.35f, alpha );
			}
		}

		private bool IsFastRunnerMoving ( float speedX, bool grounded, bool dead, bool rolling )
		{
			return m_CurrentRole == PlayerCharacterRole.Player &&
				grounded &&
				!dead &&
				!rolling &&
				speedX > 0.35f;
		}

		private void SetSprite ( Sprite sprite )
		{
			if ( m_Renderer == null || sprite == null || m_CurrentSprite == sprite )
			{
				return;
			}

			m_CurrentSprite = sprite;
			m_Renderer.sprite = sprite;
		}

		private void ApplySorting ( SpriteRenderer[] sourceRenderers )
		{
			if ( m_Renderer == null )
			{
				return;
			}

			int sortingOrder = 0;
			bool found = false;
			if ( sourceRenderers != null )
			{
				for ( int i = 0; i < sourceRenderers.Length; i++ )
				{
					if ( sourceRenderers [ i ] == null )
					{
						continue;
					}

					if ( !found )
					{
						m_Renderer.sortingLayerID = sourceRenderers [ i ].sortingLayerID;
						sortingOrder = sourceRenderers [ i ].sortingOrder;
						found = true;
					}
					else
					{
						sortingOrder = Mathf.Max ( sortingOrder, sourceRenderers [ i ].sortingOrder );
					}
				}
			}

			m_Renderer.sortingOrder = sortingOrder + 40;
			ApplyRendererSorting ( m_BackArmRenderer, m_Renderer.sortingLayerID, m_Renderer.sortingOrder - 1 );
			ApplyRendererSorting ( m_BackHandRenderer, m_Renderer.sortingLayerID, m_Renderer.sortingOrder );
			ApplyRendererSorting ( m_FrontArmRenderer, m_Renderer.sortingLayerID, m_Renderer.sortingOrder + 1 );
			ApplyRendererSorting ( m_FrontHandRenderer, m_Renderer.sortingLayerID, m_Renderer.sortingOrder + 2 );

			for ( int i = 0; i < m_RunnerTrailRenderers.Count; i++ )
			{
				if ( m_RunnerTrailRenderers [ i ] == null )
				{
					continue;
				}

				m_RunnerTrailRenderers [ i ].sortingLayerID = m_Renderer.sortingLayerID;
				m_RunnerTrailRenderers [ i ].sortingOrder = m_Renderer.sortingOrder - 2 - i;
			}
		}

		private void ApplyRendererSorting ( SpriteRenderer renderer, int sortingLayerID, int sortingOrder )
		{
			if ( renderer == null )
			{
				return;
			}

			renderer.sortingLayerID = sortingLayerID;
			renderer.sortingOrder = sortingOrder;
		}

		private Sprite FirstAvailable ( params Sprite[] sprites )
		{
			for ( int i = 0; i < sprites.Length; i++ )
			{
				if ( sprites [ i ] != null )
				{
					return sprites [ i ];
				}
			}

			return null;
		}

		private static Sprite GetTrailSprite ()
		{
			if ( s_TrailSprite != null )
			{
				return s_TrailSprite;
			}

			Texture2D texture = new Texture2D ( 8, 8, TextureFormat.RGBA32, false );
			texture.hideFlags = HideFlags.HideAndDontSave;
			Color[] pixels = new Color[ 64 ];
			for ( int i = 0; i < pixels.Length; i++ )
			{
				pixels [ i ] = Color.white;
			}

			texture.SetPixels ( pixels );
			texture.Apply ();
			s_TrailSprite = Sprite.Create (
				texture,
				new Rect ( 0f, 0f, texture.width, texture.height ),
				new Vector2 ( 0.5f, 0.5f ),
				SpritePixelsPerUnit );
			s_TrailSprite.name = "Runner Speed Trail";
			return s_TrailSprite;
		}

		private static Sprite GetLimbSprite ()
		{
			if ( s_LimbSprite != null )
			{
				return s_LimbSprite;
			}

			Texture2D texture = CreateCapsuleTexture ( 40, 12 );
			s_LimbSprite = Sprite.Create (
				texture,
				new Rect ( 0f, 0f, texture.width, texture.height ),
				new Vector2 ( 0.1f, 0.5f ),
				SpritePixelsPerUnit );
			s_LimbSprite.name = "Swing Arm Limb";
			return s_LimbSprite;
		}

		private static Sprite GetHandSprite ()
		{
			if ( s_HandSprite != null )
			{
				return s_HandSprite;
			}

			Texture2D texture = CreateCircleTexture ( 14, 14 );
			s_HandSprite = Sprite.Create (
				texture,
				new Rect ( 0f, 0f, texture.width, texture.height ),
				new Vector2 ( 0.5f, 0.5f ),
				SpritePixelsPerUnit );
			s_HandSprite.name = "Swing Arm Hand";
			return s_HandSprite;
		}

		private static Texture2D CreateCapsuleTexture ( int width, int height )
		{
			Texture2D texture = new Texture2D ( width, height, TextureFormat.RGBA32, false );
			texture.hideFlags = HideFlags.HideAndDontSave;

			Color[] pixels = new Color[ width * height ];
			float radius = height * 0.5f - 0.5f;
			Vector2 leftCenter = new Vector2 ( radius, radius );
			Vector2 rightCenter = new Vector2 ( width - radius - 1f, radius );
			for ( int y = 0; y < height; y++ )
			{
				for ( int x = 0; x < width; x++ )
				{
					bool insideMiddle = x >= radius && x <= width - radius - 1f;
					float leftDistance = Vector2.Distance ( new Vector2 ( x, y ), leftCenter );
					float rightDistance = Vector2.Distance ( new Vector2 ( x, y ), rightCenter );
					bool inside = insideMiddle || leftDistance <= radius || rightDistance <= radius;
					pixels [ y * width + x ] = inside ? Color.white : Color.clear;
				}
			}

			texture.SetPixels ( pixels );
			texture.Apply ();
			return texture;
		}

		private static Texture2D CreateCircleTexture ( int width, int height )
		{
			Texture2D texture = new Texture2D ( width, height, TextureFormat.RGBA32, false );
			texture.hideFlags = HideFlags.HideAndDontSave;

			Color[] pixels = new Color[ width * height ];
			Vector2 center = new Vector2 ( ( width - 1f ) * 0.5f, ( height - 1f ) * 0.5f );
			float radius = Mathf.Min ( width, height ) * 0.5f - 0.5f;
			for ( int y = 0; y < height; y++ )
			{
				for ( int x = 0; x < width; x++ )
				{
					float distance = Vector2.Distance ( new Vector2 ( x, y ), center );
					pixels [ y * width + x ] = distance <= radius ? Color.white : Color.clear;
				}
			}

			texture.SetPixels ( pixels );
			texture.Apply ();
			return texture;
		}

		private static KenneyCharacterInfo GetCharacterInfo ( PlayerCharacterRole role )
		{
			switch ( role )
			{
				case PlayerCharacterRole.Soldier:
					return new KenneyCharacterInfo ( "Soldier", "soldier" );
				case PlayerCharacterRole.Adventurer:
					return new KenneyCharacterInfo ( "Adventurer", "adventurer" );
				default:
					return new KenneyCharacterInfo ( "Player", "player" );
			}
		}

		private static Sprite LoadSprite ( string folder, string prefix, string pose )
		{
			string path = ResourceRoot + "/" + folder + "/" + prefix + "_" + pose;
			return LoadSpriteAtPath ( path, prefix + "_" + pose, true );
		}

		private static Sprite LoadOptionalSprite ( string folder, string prefix, string subfolder, string pose )
		{
			string path = ResourceRoot + "/" + folder + "/" + subfolder + "/" + prefix + "_" + pose;
			return LoadSpriteAtPath ( path, prefix + "_" + pose, false );
		}

		private static Sprite LoadSpriteAtPath ( string path, string spriteName, bool warnIfMissing )
		{
			Sprite cachedSprite;
			if ( s_SpriteCache.TryGetValue ( path, out cachedSprite ) )
			{
				return cachedSprite;
			}

			Texture2D texture = Resources.Load<Texture2D> ( path );
			if ( texture == null )
			{
				if ( warnIfMissing )
				{
					Debug.LogWarning ( "Missing Kenney character sprite: " + path );
				}
				s_SpriteCache [ path ] = null;
				return null;
			}

			Sprite sprite = Sprite.Create (
				texture,
				new Rect ( 0f, 0f, texture.width, texture.height ),
				new Vector2 ( 0.5f, 0.5f ),
				SpritePixelsPerUnit );
			sprite.name = spriteName;
			s_SpriteCache [ path ] = sprite;
			return sprite;
		}

		private static Sprite[] LoadRunFrames ( string folder, string prefix )
		{
			List<Sprite> frames = new List<Sprite> ();
			for ( int i = 1; i <= MaxOptionalRunFrames; i++ )
			{
				Sprite sprite = LoadOptionalSprite ( folder, prefix, "Run", "run_" + i.ToString ( "00" ) );
				if ( sprite == null )
				{
					if ( frames.Count > 0 )
					{
						break;
					}

					continue;
				}

				frames.Add ( sprite );
			}

			return frames.ToArray ();
		}

		private struct KenneyCharacterInfo
		{
			public readonly string folder;
			public readonly string prefix;

			public KenneyCharacterInfo ( string folder, string prefix )
			{
				this.folder = folder;
				this.prefix = prefix;
			}
		}

		private class SpriteSet
		{
			public Sprite idle;
			public Sprite stand;
			public Sprite walk1;
			public Sprite walk2;
			public Sprite jump;
			public Sprite fall;
			public Sprite slide;
			public Sprite duck;
			public Sprite hurt;
			public Sprite skid;
			public Sprite[] runFrames = new Sprite[ 0 ];

			public bool HasRunFrames
			{
				get { return runFrames != null && runFrames.Length > 0; }
			}

			public void Load ( string folder, string prefix )
			{
				idle = LoadSprite ( folder, prefix, "idle" );
				stand = LoadSprite ( folder, prefix, "stand" );
				walk1 = LoadSprite ( folder, prefix, "walk1" );
				walk2 = LoadSprite ( folder, prefix, "walk2" );
				jump = LoadSprite ( folder, prefix, "jump" );
				fall = LoadSprite ( folder, prefix, "fall" );
				slide = LoadSprite ( folder, prefix, "slide" );
				duck = LoadSprite ( folder, prefix, "duck" );
				hurt = LoadSprite ( folder, prefix, "hurt" );
				skid = LoadSprite ( folder, prefix, "skid" );
				runFrames = LoadRunFrames ( folder, prefix );
			}
		}
	}
}
