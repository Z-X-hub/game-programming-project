using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using UnityStandardAssets.CrossPlatformInput;

using FoxDash.Utilities;
namespace FoxDash.Characters
{

	public class RedCharacter : Character
	{
		#region Fields

		[Header ( "Character Details" )]
		[Space]
		[SerializeField]
		protected float m_MaxRunSpeed = 8f;
		[SerializeField]
		protected float m_RunSmoothTime = 5f;
		[SerializeField]
		protected float m_RunSpeed = 5f;
		[SerializeField]
		protected float m_WalkSpeed = 1.75f;
		[SerializeField]
		protected float m_JumpStrength = 10f;
		[SerializeField]
		protected string[] m_Actions = new string[0];
		[SerializeField]
		protected int m_CurrentActionIndex = 0;

		[Header ( "Character Reference" )]
		[Space]
		[SerializeField]
		protected Rigidbody2D m_Rigidbody2D;
		[SerializeField]
		protected Collider2D m_Collider2D;
		[SerializeField]
		protected Animator m_Animator;
		[SerializeField]
		protected GroundCheck m_GroundCheck;
		[SerializeField]
		protected ParticleSystem m_RunParticleSystem;
		[SerializeField]
		protected ParticleSystem m_JumpParticleSystem;
		[SerializeField]
		protected ParticleSystem m_WaterParticleSystem;
		[SerializeField]
		protected ParticleSystem m_BloodParticleSystem;
		[SerializeField]
		protected Skeleton m_Skeleton;
		[SerializeField]
		protected float m_RollForce = 10f;

		[Header ( "Character Audio" )]
		[Space]
		[SerializeField]
		protected AudioSource m_MainAudioSource;
		[SerializeField]
		protected AudioSource m_FootstepAudioSource;
		[SerializeField]
		protected AudioSource m_JumpAndGroundedAudioSource;

		#endregion

		#region Character Role

		[SerializeField]
		protected float m_RunnerSpeedMultiplier = 1.2f;
		[SerializeField]
		protected float m_KnightSpeedMultiplier = 0.9f;
		[SerializeField]
		protected float m_MonkeySpeedMultiplier = 1f;

		protected PlayerCharacterRole m_CurrentRole = PlayerCharacterRole.Runner;
		protected bool m_KnightReviveAvailable = false;
		[SerializeField]
		protected float m_KnightReviveHeight = 2.6f;
		[SerializeField]
		protected float m_KnightReviveGraceDuration = 0.75f;
		protected int m_JumpsUsed = 0;
		protected float m_BaseRunSpeed = 0f;
		protected float m_BaseMaxRunSpeed = 0f;
		protected SpriteRenderer[] m_SpriteRenderers = new SpriteRenderer[0];
		protected Color[] m_InitialSpriteColors = new Color[0];
		protected Transform m_RoleVisualRoot = null;
		protected GameObject m_KnightShieldAuraObject = null;
		protected KenneyCharacterVisual m_KenneyVisual = null;
		[SerializeField]
		protected float m_RollAnimationDuration = 0.38f;
		protected float m_RollAnimationTimer = 0f;

		private enum RoleVisualShape
		{
			Circle,
			Rectangle,
			RoundedRectangle,
			Capsule,
			Triangle,
			Diamond,
			Ring,
			Bolt,
			Heart,
			Star,
			Sparkle,
			Crescent,
			Shield,
			Smile,
			Crown
		}

		private const int RoleVisualTextureSize = 96;
		private static readonly Vector2[] s_BoltPoints = new Vector2[]
		{
			new Vector2 ( -0.12f, 0.95f ),
			new Vector2 ( -0.52f, 0.08f ),
			new Vector2 ( -0.18f, 0.08f ),
			new Vector2 ( -0.42f, -0.95f ),
			new Vector2 ( 0.5f, -0.18f ),
			new Vector2 ( 0.12f, -0.18f )
		};
		private static readonly Vector2[] s_StarPoints = CreateStarPoints ( 5, 0.92f, 0.42f, -90f );
		private static readonly Vector2[] s_ShieldPoints = new Vector2[]
		{
			new Vector2 ( -0.78f, 0.78f ),
			new Vector2 ( 0.78f, 0.78f ),
			new Vector2 ( 0.62f, -0.2f ),
			new Vector2 ( 0f, -0.96f ),
			new Vector2 ( -0.62f, -0.2f )
		};
		private static readonly Vector2[] s_CrownPoints = new Vector2[]
		{
			new Vector2 ( -0.9f, -0.58f ),
			new Vector2 ( 0.9f, -0.58f ),
			new Vector2 ( 0.78f, 0.26f ),
			new Vector2 ( 0.38f, -0.02f ),
			new Vector2 ( 0f, 0.78f ),
			new Vector2 ( -0.38f, -0.02f ),
			new Vector2 ( -0.78f, 0.26f )
		};
		private static readonly Dictionary<string, Sprite> s_RoleVisualSprites = new Dictionary<string, Sprite> ();

		#endregion

		#region Private Variables

		protected bool m_ClosingEye = false;
		protected bool m_Guard = false;
		protected bool m_Block = false;
		protected Vector2 m_Speed = Vector2.zero;
		protected float m_CurrentRunSpeed = 0f;
		protected float m_CurrentSmoothVelocity = 0f;
		protected int m_CurrentFootstepSoundIndex = 0;
		protected Vector3 m_InitialScale;
		protected Vector3 m_InitialPosition;
		protected Vector3 m_LastSafePosition;
		protected float m_ReviveGraceTimer = 0f;

		#endregion

		#region Properties

		public override float MaxRunSpeed
		{
			get
			{
				return m_MaxRunSpeed;
			}
		}

		public override float RunSmoothTime
		{
			get
			{
				return m_RunSmoothTime;
			}
		}

		public override float RunSpeed
		{
			get
			{
				return m_RunSpeed;
			}
		}

		public override float WalkSpeed
		{
			get
			{
				return m_WalkSpeed;
			}
		}

		public override float JumpStrength
		{
			get
			{
				return m_JumpStrength;
			}
		}

		public override Vector2 Speed
		{
			get
			{
				return m_Speed;
			}
		}

		public override string[] Actions
		{
			get
			{
				return m_Actions;
			}
		}

		public override string CurrentAction
		{
			get
			{
				return m_Actions [ m_CurrentActionIndex ];
			}
		}

		public override int CurrentActionIndex
		{
			get
			{
				return m_CurrentActionIndex;
			}
		}

		public override GroundCheck GroundCheck
		{
			get
			{
				return m_GroundCheck;
			}
		}

		public override Rigidbody2D Rigidbody2D
		{
			get
			{
				return m_Rigidbody2D;
			}
		}

		public override Collider2D Collider2D
		{
			get
			{
				return m_Collider2D;
			}
		}

		public override Animator Animator
		{
			get
			{
				return m_Animator;
			}
		}

		public override ParticleSystem RunParticleSystem
		{
			get
			{
				return m_RunParticleSystem;
			}
		}

		public override ParticleSystem JumpParticleSystem
		{
			get
			{
				return m_JumpParticleSystem;
			}
		}

		public override ParticleSystem WaterParticleSystem
		{
			get
			{
				return m_WaterParticleSystem;
			}
		}

		public override ParticleSystem BloodParticleSystem
		{
			get
			{
				return m_BloodParticleSystem;
			}
		}

		public override Skeleton Skeleton
		{
			get
			{
				return m_Skeleton;
			}
		}

        public override bool ClosingEye
		{
			get
			{
				return m_ClosingEye;
			}
		}

		public override bool Guard
		{
			get
			{
				return m_Guard;
			}
		}

		public override bool Block
		{
			get
			{
				return m_Block;
			}
		}

		public override AudioSource Audio
		{
			get
			{
				return m_MainAudioSource;
			}
		}

		#endregion

		#region MonoBehaviour Messages

		void Awake ()
		{
			CacheReferences ();
			IsDead = new Property<bool>(false);
			m_InitialPosition = transform.position;
			m_LastSafePosition = m_InitialPosition;
			m_InitialScale = transform.localScale;
			m_BaseRunSpeed = m_RunSpeed;
			m_BaseMaxRunSpeed = m_MaxRunSpeed;
			CacheSpriteRenderers ();
			ApplyRole ( PlayerCharacterSelection.SelectedRole );
			if ( m_GroundCheck != null )
			{
				m_GroundCheck.OnGrounded += GroundCheck_OnGrounded;
			}
			if ( m_Skeleton != null )
			{
				m_Skeleton.OnActiveChanged += Skeleton_OnActiveChanged;
			}
			m_ClosingEye = false;
			m_Guard = false;
			m_Block = false;
			m_CurrentFootstepSoundIndex = 0;
			GameManager.OnReset += GameManager_OnReset;
		}

		/// <summary>
		/// 游戏运行时处理掉落死亡、速度计算、移动跳跃和防御动作输入。
		/// </summary>
		void Update ()
		{
			GameManager gameManager = GameManager.Singleton;
			if ( gameManager == null || IsDead == null )
			{
				return;
			}

			if ( !gameManager.gameStarted || !gameManager.gameRunning )
			{
				return;
			}

			if ( m_Rigidbody2D == null )
			{
				CacheReferences ();
				if ( m_Rigidbody2D == null )
				{
					return;
				}
			}

			if ( m_ReviveGraceTimer > 0f )
			{
				m_ReviveGraceTimer = Mathf.Max ( 0f, m_ReviveGraceTimer - Time.deltaTime );
			}

			if ( transform.position.y < 0f && m_ReviveGraceTimer <= 0f )
			{
				Die ();
			}
			else if ( m_GroundCheck != null && m_GroundCheck.IsGrounded && !IsDead.Value )
			{
				m_LastSafePosition = transform.position;
			}

			// Speed
			m_Speed = new Vector2 ( Mathf.Abs ( m_Rigidbody2D.velocity.x ), Mathf.Abs ( m_Rigidbody2D.velocity.y ) );

			// Speed Calculations
			m_CurrentRunSpeed = m_RunSpeed;
			if ( m_Speed.x >= m_RunSpeed )
			{
				m_CurrentRunSpeed = Mathf.SmoothDamp ( m_Speed.x, m_MaxRunSpeed, ref m_CurrentSmoothVelocity, m_RunSmoothTime );
			}

			// Input Processing
			Move ( CrossPlatformInputManager.GetAxis ( "Horizontal" ) );
			if ( CrossPlatformInputManager.GetButtonDown ( "Jump" ) )
			{
				Jump ();
			}
			if ( IsDead.Value && !m_ClosingEye )
			{
				StartCoroutine ( CloseEye () );
			}
			if ( CrossPlatformInputManager.GetButtonDown ( "Guard" ) )
			{
				m_Guard = !m_Guard;
			}
			if ( m_Guard )
			{
				if ( CrossPlatformInputManager.GetButtonDown ( "Fire" ) )
				{
					m_Animator.SetTrigger ( m_Actions [ m_CurrentActionIndex ] );
					if ( m_CurrentActionIndex < m_Actions.Length - 1 )
					{
						m_CurrentActionIndex++;
					}
					else
					{
						m_CurrentActionIndex = 0;
					}
				}
			}

			if ( Input.GetButtonDown ( "Roll" ) )
			{
				m_RollAnimationTimer = m_RollAnimationDuration;
				Vector2 force = new Vector2 ( 0f, 0f );
				if ( transform.localScale.x > 0f )
				{
					force.x = m_RollForce;
				}
				else if ( transform.localScale.x < 0f )
				{
					force.x = -m_RollForce;
				}
				m_Rigidbody2D.AddForce ( force );
			}
		}

		/// <summary>
		/// 在所有输入和物理状态更新后，把角色状态同步给 Animator。
		/// </summary>
		void LateUpdate ()
		{
			if ( IsDead == null || m_Animator == null || m_Rigidbody2D == null || m_GroundCheck == null )
			{
				CacheReferences ();
				if ( IsDead == null || m_Animator == null || m_Rigidbody2D == null || m_GroundCheck == null )
				{
					return;
				}
			}

			m_Animator.SetFloat ( "Speed", m_Speed.x );
			m_Animator.SetFloat ( "VelocityX", Mathf.Abs ( m_Rigidbody2D.velocity.x ) );
			m_Animator.SetFloat ( "VelocityY", m_Rigidbody2D.velocity.y );
			m_Animator.SetBool ( "IsGrounded", m_GroundCheck.IsGrounded );
			m_Animator.SetBool ( "IsDead", IsDead.Value );
			m_Animator.SetBool ( "Block", m_Block );
			m_Animator.SetBool ( "Guard", m_Guard );
			if ( Input.GetButtonDown ( "Roll" ) )
			{
				m_Animator.SetTrigger ( "Roll" );
			}
			UpdateKenneyCharacterVisual ();
		}

		//		void OnCollisionEnter2D ( Collision2D collision2D )
		//		{
		//			bool isGround = collision2D.collider.CompareTag ( GroundCheck.GROUND_TAG );
		//			if ( isGround && !m_IsDead )
		//			{
		//				bool isBottom = false;
		//				for ( int i = 0; i < collision2D.contacts.Length; i++ )
		//				{
		//					if ( !isBottom )
		//					{
		//						isBottom = collision2D.contacts [ i ].normal.y == 1;
		//					}
		//					else
		//					{
		//						break;
		//					}
		//				}
		//				if ( isBottom )
		//				{
		//					m_JumpParticleSystem.Play ();
		//				}
		//			}
		//		}

		#endregion

		#region Private Methods

		/// <summary>
		/// 死亡后逐步收起骨骼眼睛，作为角色失败反馈的一部分。
		/// </summary>
		IEnumerator CloseEye ()
		{
			if ( m_Skeleton == null || m_Skeleton.RightEye == null || m_Skeleton.LeftEye == null )
			{
				yield break;
			}

			m_ClosingEye = true;
			yield return new WaitForSeconds ( 0.6f );
			while ( m_Skeleton.RightEye.localScale.y > 0f )
			{
				if ( m_Skeleton.RightEye.localScale.y > 0f )
				{
					Vector3 scale = m_Skeleton.RightEye.localScale;
					scale.y -= 0.1f;
					m_Skeleton.RightEye.localScale = scale;
				}
				if ( m_Skeleton.LeftEye.localScale.y > 0f )
				{
					Vector3 scale = m_Skeleton.LeftEye.localScale;
					scale.y -= 0.1f;
					m_Skeleton.LeftEye.localScale = scale;
				}
				yield return new WaitForSeconds ( 0.05f );
			}
		}

		void CacheReferences ()
		{
			if ( m_Rigidbody2D == null )
			{
				m_Rigidbody2D = GetComponent<Rigidbody2D> ();
			}
			if ( m_Collider2D == null )
			{
				m_Collider2D = GetComponent<Collider2D> ();
			}
			if ( m_Animator == null )
			{
				m_Animator = GetComponent<Animator> ();
			}
			if ( m_GroundCheck == null )
			{
				m_GroundCheck = GetComponentInChildren<GroundCheck> ( true );
			}
			if ( m_Skeleton == null )
			{
				m_Skeleton = GetComponent<Skeleton> ();
				if ( m_Skeleton == null )
				{
					m_Skeleton = GetComponentInChildren<Skeleton> ( true );
				}
			}
		}

		void CacheSpriteRenderers ()
		{
			SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer> ( true );
			List<SpriteRenderer> characterRenderers = new List<SpriteRenderer> ();
			for ( int i = 0; i < renderers.Length; i++ )
			{
				if ( renderers [ i ] == null )
				{
					continue;
				}
				if ( m_RoleVisualRoot != null && renderers [ i ].transform.IsChildOf ( m_RoleVisualRoot ) )
				{
					continue;
				}

				characterRenderers.Add ( renderers [ i ] );
			}

			m_SpriteRenderers = characterRenderers.ToArray ();
			m_InitialSpriteColors = new Color[m_SpriteRenderers.Length];
			for ( int i = 0; i < m_SpriteRenderers.Length; i++ )
			{
				m_InitialSpriteColors [ i ] = m_SpriteRenderers [ i ].color;
			}
		}

		void ApplyRoleStats ()
		{
			float speedMultiplier = m_RunnerSpeedMultiplier;
			if ( m_CurrentRole == PlayerCharacterRole.Knight )
			{
				speedMultiplier = m_KnightSpeedMultiplier;
			}
			else if ( m_CurrentRole == PlayerCharacterRole.Monkey )
			{
				speedMultiplier = m_MonkeySpeedMultiplier;
			}

			m_RunSpeed = m_BaseRunSpeed * speedMultiplier;
			m_MaxRunSpeed = m_BaseMaxRunSpeed * speedMultiplier;
		}

		void ApplyRoleTint ()
		{
			if ( m_SpriteRenderers == null || m_InitialSpriteColors == null )
			{
				return;
			}

			Color tint = Color.white;
			if ( m_CurrentRole == PlayerCharacterRole.Knight )
			{
				tint = m_KnightReviveAvailable ? new Color ( 0.72f, 0.86f, 1f, 1f ) : new Color ( 0.78f, 0.78f, 0.82f, 1f );
			}
			else if ( m_CurrentRole == PlayerCharacterRole.Monkey )
			{
				tint = new Color ( 0.82f, 0.55f, 0.28f, 1f );
			}

			for ( int i = 0; i < m_SpriteRenderers.Length && i < m_InitialSpriteColors.Length; i++ )
			{
				if ( m_SpriteRenderers [ i ] == null )
				{
					continue;
				}

				bool isEye = m_SpriteRenderers [ i ].name.ToLowerInvariant ().Contains ( "eye" );
				m_SpriteRenderers [ i ].color = isEye ? m_InitialSpriteColors [ i ] : m_InitialSpriteColors [ i ] * tint;
			}

			RefreshRoleVisualState ();
		}

		void BuildRoleVisuals ()
		{
			ClearRoleVisuals ();

			GameObject root = new GameObject ( "Kenney Character Visuals" );
			root.transform.SetParent ( transform, false );
			root.transform.localPosition = Vector3.zero;
			root.transform.localRotation = Quaternion.identity;
			root.transform.localScale = Vector3.one;
			m_RoleVisualRoot = root.transform;
			m_KenneyVisual = root.AddComponent<KenneyCharacterVisual> ();
			bool ready = m_KenneyVisual.Initialize ( m_SpriteRenderers ) && m_KenneyVisual.ApplyRole ( m_CurrentRole );
			SetBaseCharacterVisible ( !ready );
			RefreshRoleVisualState ();
		}

		void BuildRunnerVisuals ()
		{
			CreateSoftCharacterShadow ();
			CreateOutlinedRoleVisualPart ( "Runner Plush Hood", RoleVisualShape.RoundedRectangle, new Vector2 ( 0f, 0.08f ), new Vector2 ( 0.9f, 0.92f ), new Color ( 1f, 0.29f, 0.29f, 0.96f ), new Color ( 0.72f, 0.1f, 0.1f, 0.55f ), 10, 0f, 1.08f );
			CreateOutlinedRoleVisualPart ( "Runner Left Ear", RoleVisualShape.Triangle, new Vector2 ( -0.31f, 0.68f ), new Vector2 ( 0.32f, 0.34f ), new Color ( 1f, 0.34f, 0.32f, 1f ), new Color ( 0.72f, 0.1f, 0.1f, 0.65f ), 16, 15f, 1.12f );
			CreateOutlinedRoleVisualPart ( "Runner Right Ear", RoleVisualShape.Triangle, new Vector2 ( 0.31f, 0.68f ), new Vector2 ( 0.32f, 0.34f ), new Color ( 1f, 0.34f, 0.32f, 1f ), new Color ( 0.72f, 0.1f, 0.1f, 0.65f ), 16, -15f, 1.12f );
			CreateRoleVisualPart ( "Runner Left Inner Ear", RoleVisualShape.Triangle, new Vector2 ( -0.31f, 0.64f ), new Vector2 ( 0.16f, 0.18f ), new Color ( 1f, 0.72f, 0.68f, 1f ), 17, 15f );
			CreateRoleVisualPart ( "Runner Right Inner Ear", RoleVisualShape.Triangle, new Vector2 ( 0.31f, 0.64f ), new Vector2 ( 0.16f, 0.18f ), new Color ( 1f, 0.72f, 0.68f, 1f ), 17, -15f );
			CreateCuteFace ( new Color ( 1f, 0.86f, 0.72f, 1f ), new Color ( 1f, 0.5f, 0.56f, 0.82f ), 19 );
			CreateOutlinedRoleVisualPart ( "Runner Lightning Badge", RoleVisualShape.Bolt, new Vector2 ( 0.03f, -0.31f ), new Vector2 ( 0.24f, 0.34f ), new Color ( 1f, 0.9f, 0.05f, 1f ), new Color ( 0.95f, 0.42f, 0.04f, 0.75f ), 20, -8f, 1.18f );
			CreateRoleVisualPart ( "Runner Scarf Back", RoleVisualShape.Capsule, new Vector2 ( -0.58f, 0.08f ), new Vector2 ( 0.55f, 0.14f ), new Color ( 1f, 0.82f, 0.12f, 0.92f ), -3, 8f );
			CreateRoleVisualPart ( "Runner Scarf Tail", RoleVisualShape.Triangle, new Vector2 ( -0.85f, 0.04f ), new Vector2 ( 0.3f, 0.24f ), new Color ( 1f, 0.82f, 0.12f, 0.9f ), -3, -75f );
			CreateRoleVisualPart ( "Runner Sparkle Left", RoleVisualShape.Sparkle, new Vector2 ( -0.52f, 0.43f ), new Vector2 ( 0.13f, 0.13f ), new Color ( 1f, 0.94f, 0.35f, 0.95f ), 22, 0f );
			CreateRoleVisualPart ( "Runner Sparkle Right", RoleVisualShape.Star, new Vector2 ( 0.5f, -0.12f ), new Vector2 ( 0.12f, 0.12f ), new Color ( 1f, 0.98f, 0.55f, 0.9f ), 22, 14f );
		}

		void BuildKnightVisuals ()
		{
			CreateSoftCharacterShadow ();
			CreateOutlinedRoleVisualPart ( "Knight Soft Armor", RoleVisualShape.RoundedRectangle, new Vector2 ( 0f, -0.02f ), new Vector2 ( 0.86f, 0.85f ), new Color ( 0.8f, 0.9f, 1f, 0.96f ), new Color ( 0.18f, 0.32f, 0.58f, 0.48f ), 10, 0f, 1.08f );
			CreateOutlinedRoleVisualPart ( "Knight Helmet Dome", RoleVisualShape.Circle, new Vector2 ( 0f, 0.47f ), new Vector2 ( 0.78f, 0.46f ), new Color ( 0.88f, 0.94f, 1f, 1f ), new Color ( 0.38f, 0.5f, 0.66f, 0.68f ), 18, 0f, 1.09f );
			CreateRoleVisualPart ( "Knight Helmet Shine", RoleVisualShape.Crescent, new Vector2 ( -0.15f, 0.54f ), new Vector2 ( 0.36f, 0.28f ), new Color ( 1f, 1f, 1f, 0.55f ), 19, -18f );
			CreateRoleVisualPart ( "Knight Visor Soft", RoleVisualShape.Capsule, new Vector2 ( 0.08f, 0.44f ), new Vector2 ( 0.52f, 0.12f ), new Color ( 0.14f, 0.2f, 0.32f, 1f ), 26, 0f );
			CreateOutlinedRoleVisualPart ( "Knight Crown Crest", RoleVisualShape.Crown, new Vector2 ( 0f, 0.72f ), new Vector2 ( 0.34f, 0.27f ), new Color ( 1f, 0.36f, 0.32f, 1f ), new Color ( 0.85f, 0.16f, 0.12f, 0.72f ), 27, 0f, 1.1f );
			CreateCuteFace ( new Color ( 1f, 0.88f, 0.76f, 0.96f ), new Color ( 1f, 0.56f, 0.62f, 0.72f ), 22 );

			CreateOutlinedRoleVisualPart ( "Knight Shield Gem", RoleVisualShape.Shield, new Vector2 ( -0.56f, -0.12f ), new Vector2 ( 0.48f, 0.62f ), new Color ( 0.2f, 0.52f, 1f, 1f ), new Color ( 0.08f, 0.18f, 0.46f, 0.72f ), 24, 0f, 1.12f );
			CreateRoleVisualPart ( "Knight Shield Shine", RoleVisualShape.Crescent, new Vector2 ( -0.63f, 0.04f ), new Vector2 ( 0.22f, 0.26f ), new Color ( 1f, 1f, 1f, 0.58f ), 25, -20f );
			CreateRoleVisualPart ( "Knight Shield Heart", RoleVisualShape.Heart, new Vector2 ( -0.56f, -0.15f ), new Vector2 ( 0.18f, 0.18f ), new Color ( 1f, 0.93f, 0.35f, 1f ), 26, 0f );

			CreateOutlinedRoleVisualPart ( "Knight Sword Blade", RoleVisualShape.Capsule, new Vector2 ( 0.56f, 0.1f ), new Vector2 ( 0.1f, 0.68f ), new Color ( 0.9f, 0.98f, 1f, 1f ), new Color ( 0.38f, 0.53f, 0.68f, 0.72f ), 18, -18f, 1.18f );
			CreateRoleVisualPart ( "Knight Sword Guard", RoleVisualShape.Capsule, new Vector2 ( 0.43f, -0.19f ), new Vector2 ( 0.34f, 0.08f ), new Color ( 1f, 0.78f, 0.2f, 1f ), 21, -18f );
			CreateRoleVisualPart ( "Knight Sword Grip", RoleVisualShape.Capsule, new Vector2 ( 0.38f, -0.31f ), new Vector2 ( 0.1f, 0.24f ), new Color ( 0.24f, 0.16f, 0.12f, 1f ), 21, -18f );

			GameObject auraGroup = new GameObject ( "Knight Shield Aura Group" );
			auraGroup.transform.SetParent ( m_RoleVisualRoot, false );
			auraGroup.transform.localPosition = Vector3.zero;
			auraGroup.transform.localRotation = Quaternion.identity;
			auraGroup.transform.localScale = Vector3.one;
			m_KnightShieldAuraObject = auraGroup;
			ReparentRoleVisualPart ( CreateRoleVisualPart ( "Knight Shield Aura", RoleVisualShape.Ring, new Vector2 ( 0f, 0.05f ), new Vector2 ( 1.55f, 1.55f ), new Color ( 1f, 0.86f, 0.16f, 0.58f ), 30, 0f ), auraGroup.transform );
			ReparentRoleVisualPart ( CreateRoleVisualPart ( "Knight Aura Star Left", RoleVisualShape.Star, new Vector2 ( -0.52f, 0.48f ), new Vector2 ( 0.18f, 0.18f ), new Color ( 1f, 0.98f, 0.52f, 0.95f ), 31, 0f ), auraGroup.transform );
			ReparentRoleVisualPart ( CreateRoleVisualPart ( "Knight Aura Star Right", RoleVisualShape.Sparkle, new Vector2 ( 0.54f, -0.36f ), new Vector2 ( 0.16f, 0.16f ), new Color ( 1f, 0.98f, 0.52f, 0.95f ), 31, 0f ), auraGroup.transform );
		}

		void BuildMonkeyVisuals ()
		{
			CreateSoftCharacterShadow ();
			CreateOutlinedRoleVisualPart ( "Monkey Rounded Fur", RoleVisualShape.RoundedRectangle, new Vector2 ( 0f, 0.02f ), new Vector2 ( 0.86f, 0.9f ), new Color ( 0.66f, 0.38f, 0.17f, 0.98f ), new Color ( 0.32f, 0.15f, 0.06f, 0.62f ), 10, 0f, 1.08f );
			CreateOutlinedRoleVisualPart ( "Monkey Tail Curl Back", RoleVisualShape.Crescent, new Vector2 ( -0.62f, 0.08f ), new Vector2 ( 0.72f, 0.72f ), new Color ( 0.43f, 0.22f, 0.08f, 1f ), new Color ( 0.22f, 0.09f, 0.03f, 0.72f ), -7, -20f, 1.12f );
			CreateOutlinedRoleVisualPart ( "Monkey Tail Tip", RoleVisualShape.Circle, new Vector2 ( -0.77f, 0.35f ), new Vector2 ( 0.24f, 0.24f ), new Color ( 0.43f, 0.22f, 0.08f, 1f ), new Color ( 0.22f, 0.09f, 0.03f, 0.72f ), -5, 0f, 1.1f );

			CreateOutlinedRoleVisualPart ( "Monkey Left Ear", RoleVisualShape.Circle, new Vector2 ( -0.39f, 0.44f ), new Vector2 ( 0.34f, 0.34f ), new Color ( 0.48f, 0.25f, 0.1f, 1f ), new Color ( 0.25f, 0.1f, 0.03f, 0.72f ), 16, 0f, 1.1f );
			CreateOutlinedRoleVisualPart ( "Monkey Right Ear", RoleVisualShape.Circle, new Vector2 ( 0.39f, 0.44f ), new Vector2 ( 0.34f, 0.34f ), new Color ( 0.48f, 0.25f, 0.1f, 1f ), new Color ( 0.25f, 0.1f, 0.03f, 0.72f ), 16, 0f, 1.1f );
			CreateRoleVisualPart ( "Monkey Left Inner Ear", RoleVisualShape.Heart, new Vector2 ( -0.39f, 0.43f ), new Vector2 ( 0.18f, 0.18f ), new Color ( 1f, 0.67f, 0.46f, 1f ), 18, 0f );
			CreateRoleVisualPart ( "Monkey Right Inner Ear", RoleVisualShape.Heart, new Vector2 ( 0.39f, 0.43f ), new Vector2 ( 0.18f, 0.18f ), new Color ( 1f, 0.67f, 0.46f, 1f ), 18, 0f );
			CreateCuteFace ( new Color ( 1f, 0.76f, 0.48f, 1f ), new Color ( 1f, 0.48f, 0.48f, 0.78f ), 19 );
			CreateRoleVisualPart ( "Monkey Belly Patch", RoleVisualShape.Circle, new Vector2 ( 0.02f, -0.3f ), new Vector2 ( 0.5f, 0.38f ), new Color ( 1f, 0.74f, 0.46f, 0.9f ), 14, 0f );
			CreateOutlinedRoleVisualPart ( "Monkey Banana Pin", RoleVisualShape.Crescent, new Vector2 ( 0.24f, 0.71f ), new Vector2 ( 0.25f, 0.18f ), new Color ( 1f, 0.86f, 0.18f, 1f ), new Color ( 0.8f, 0.48f, 0.05f, 0.68f ), 22, -28f, 1.12f );
			CreateRoleVisualPart ( "Monkey Jump Spark Left", RoleVisualShape.Sparkle, new Vector2 ( -0.32f, -0.58f ), new Vector2 ( 0.12f, 0.12f ), new Color ( 0.7f, 0.94f, 1f, 0.9f ), 20, 0f );
			CreateRoleVisualPart ( "Monkey Jump Spark Right", RoleVisualShape.Star, new Vector2 ( 0.38f, -0.54f ), new Vector2 ( 0.1f, 0.1f ), new Color ( 0.7f, 0.94f, 1f, 0.82f ), 20, 8f );
		}

		void CreateSoftCharacterShadow ()
		{
			CreateRoleVisualPart ( "Cute Soft Shadow", RoleVisualShape.Circle, new Vector2 ( 0f, -0.62f ), new Vector2 ( 0.78f, 0.12f ), new Color ( 0f, 0f, 0f, 0.16f ), -12, 0f );
		}

		SpriteRenderer CreateOutlinedRoleVisualPart ( string name, RoleVisualShape shape, Vector2 localPosition, Vector2 localScale, Color color, Color outlineColor, int sortingOffset, float rotationZ, float outlineScale )
		{
			int outlineSortingOffset = sortingOffset - 1;
			CreateRoleVisualPart ( name + " Outline", shape, localPosition, localScale * outlineScale, outlineColor, outlineSortingOffset, rotationZ );
			return CreateRoleVisualPart ( name, shape, localPosition, localScale, color, sortingOffset, rotationZ );
		}

		void CreateCuteFace ( Color faceColor, Color blushColor, int sortingOffset )
		{
			CreateOutlinedRoleVisualPart ( "Cute Face Patch", RoleVisualShape.Circle, new Vector2 ( 0f, 0.19f ), new Vector2 ( 0.62f, 0.48f ), faceColor, new Color ( 0f, 0f, 0f, 0.12f ), sortingOffset, 0f, 1.08f );
			CreateRoleVisualPart ( "Cute Left Eye", RoleVisualShape.Circle, new Vector2 ( -0.18f, 0.26f ), new Vector2 ( 0.09f, 0.12f ), new Color ( 0.12f, 0.08f, 0.08f, 1f ), sortingOffset + 2, 0f );
			CreateRoleVisualPart ( "Cute Right Eye", RoleVisualShape.Circle, new Vector2 ( 0.18f, 0.26f ), new Vector2 ( 0.09f, 0.12f ), new Color ( 0.12f, 0.08f, 0.08f, 1f ), sortingOffset + 2, 0f );
			CreateRoleVisualPart ( "Cute Left Eye Highlight", RoleVisualShape.Circle, new Vector2 ( -0.2f, 0.3f ), new Vector2 ( 0.032f, 0.04f ), new Color ( 1f, 1f, 1f, 0.95f ), sortingOffset + 3, 0f );
			CreateRoleVisualPart ( "Cute Right Eye Highlight", RoleVisualShape.Circle, new Vector2 ( 0.16f, 0.3f ), new Vector2 ( 0.032f, 0.04f ), new Color ( 1f, 1f, 1f, 0.95f ), sortingOffset + 3, 0f );
			CreateRoleVisualPart ( "Cute Smile", RoleVisualShape.Smile, new Vector2 ( 0f, 0.12f ), new Vector2 ( 0.25f, 0.16f ), new Color ( 0.22f, 0.1f, 0.08f, 0.95f ), sortingOffset + 3, 0f );
			CreateRoleVisualPart ( "Cute Left Blush", RoleVisualShape.Circle, new Vector2 ( -0.29f, 0.13f ), new Vector2 ( 0.12f, 0.07f ), blushColor, sortingOffset + 1, 0f );
			CreateRoleVisualPart ( "Cute Right Blush", RoleVisualShape.Circle, new Vector2 ( 0.29f, 0.13f ), new Vector2 ( 0.12f, 0.07f ), blushColor, sortingOffset + 1, 0f );
		}

		void ReparentRoleVisualPart ( SpriteRenderer renderer, Transform parent )
		{
			if ( renderer != null && parent != null )
			{
				renderer.transform.SetParent ( parent, true );
			}
		}

		SpriteRenderer CreateRoleVisualPart ( string name, RoleVisualShape shape, Vector2 localPosition, Vector2 localScale, Color color, int sortingOffset, float rotationZ )
		{
			if ( m_RoleVisualRoot == null )
			{
				return null;
			}

			GameObject part = new GameObject ( name );
			part.transform.SetParent ( m_RoleVisualRoot, false );
			part.transform.localPosition = new Vector3 ( localPosition.x, localPosition.y, 0f );
			part.transform.localRotation = Quaternion.Euler ( 0f, 0f, rotationZ );
			part.transform.localScale = new Vector3 ( localScale.x, localScale.y, 1f );

			SpriteRenderer renderer = part.AddComponent<SpriteRenderer> ();
			renderer.sprite = GetRoleVisualSprite ( shape );
			renderer.color = color;
			ApplyRoleVisualSorting ( renderer, sortingOffset );
			return renderer;
		}

		void ApplyRoleVisualSorting ( SpriteRenderer renderer, int sortingOffset )
		{
			if ( renderer == null )
			{
				return;
			}

			SpriteRenderer reference = GetRoleVisualReferenceRenderer ();
			if ( reference != null )
			{
				renderer.sortingLayerID = reference.sortingLayerID;
			}

			int baseOrder = sortingOffset < 0 ? GetBackSortingOrder () : GetFrontSortingOrder ();
			renderer.sortingOrder = baseOrder + sortingOffset;
		}

		SpriteRenderer GetRoleVisualReferenceRenderer ()
		{
			if ( m_SpriteRenderers == null )
			{
				return null;
			}

			for ( int i = 0; i < m_SpriteRenderers.Length; i++ )
			{
				if ( m_SpriteRenderers [ i ] != null )
				{
					return m_SpriteRenderers [ i ];
				}
			}

			return null;
		}

		int GetFrontSortingOrder ()
		{
			int order = 0;
			bool found = false;
			if ( m_SpriteRenderers != null )
			{
				for ( int i = 0; i < m_SpriteRenderers.Length; i++ )
				{
					if ( m_SpriteRenderers [ i ] == null )
					{
						continue;
					}

					order = found ? Mathf.Max ( order, m_SpriteRenderers [ i ].sortingOrder ) : m_SpriteRenderers [ i ].sortingOrder;
					found = true;
				}
			}

			return order;
		}

		int GetBackSortingOrder ()
		{
			int order = 0;
			bool found = false;
			if ( m_SpriteRenderers != null )
			{
				for ( int i = 0; i < m_SpriteRenderers.Length; i++ )
				{
					if ( m_SpriteRenderers [ i ] == null )
					{
						continue;
					}

					order = found ? Mathf.Min ( order, m_SpriteRenderers [ i ].sortingOrder ) : m_SpriteRenderers [ i ].sortingOrder;
					found = true;
				}
			}

			return order;
		}

		void RefreshRoleVisualState ()
		{
			if ( m_KenneyVisual != null )
			{
				m_KenneyVisual.SetShieldActive ( false );
			}
			if ( m_KnightShieldAuraObject != null )
			{
				m_KnightShieldAuraObject.SetActive ( false );
			}
		}

		void ClearRoleVisuals ()
		{
			m_KnightShieldAuraObject = null;
			m_KenneyVisual = null;
			if ( m_RoleVisualRoot == null )
			{
				return;
			}

			GameObject root = m_RoleVisualRoot.gameObject;
			m_RoleVisualRoot = null;
			if ( Application.isPlaying )
			{
				Destroy ( root );
			}
			else
			{
				DestroyImmediate ( root );
			}
		}

		void SetBaseCharacterVisible ( bool visible )
		{
			if ( m_SpriteRenderers == null )
			{
				return;
			}

			for ( int i = 0; i < m_SpriteRenderers.Length; i++ )
			{
				if ( m_SpriteRenderers [ i ] != null )
				{
					m_SpriteRenderers [ i ].enabled = visible;
				}
			}
		}

		void UpdateKenneyCharacterVisual ()
		{
			if ( m_KenneyVisual == null )
			{
				return;
			}

			if ( m_RollAnimationTimer > 0f )
			{
				m_RollAnimationTimer = Mathf.Max ( 0f, m_RollAnimationTimer - Time.deltaTime );
			}

			float rollNormalized = m_RollAnimationDuration > 0f ? 1f - ( m_RollAnimationTimer / m_RollAnimationDuration ) : 1f;
			bool rolling = m_RollAnimationTimer > 0f;
			bool grounded = m_GroundCheck != null && m_GroundCheck.IsGrounded;
			float speedX = m_Rigidbody2D != null ? Mathf.Abs ( m_Rigidbody2D.velocity.x ) : m_Speed.x;
			float verticalVelocity = m_Rigidbody2D != null ? m_Rigidbody2D.velocity.y : 0f;
			bool dead = IsDead != null && IsDead.Value;
			m_KenneyVisual.UpdateVisual ( speedX, verticalVelocity, grounded, dead, rolling, rollNormalized );
		}

		Sprite GetRoleVisualSprite ( RoleVisualShape shape )
		{
			string key = shape.ToString ();
			Sprite sprite;
			if ( s_RoleVisualSprites.TryGetValue ( key, out sprite ) && sprite != null )
			{
				return sprite;
			}

			Texture2D texture = new Texture2D ( RoleVisualTextureSize, RoleVisualTextureSize, TextureFormat.RGBA32, false );
			texture.name = "Role Visual " + key;
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;

			Color32[] pixels = new Color32[RoleVisualTextureSize * RoleVisualTextureSize];
			const int samples = 4;
			float sampleStep = 1f / samples;

			for ( int y = 0; y < RoleVisualTextureSize; y++ )
			{
				for ( int x = 0; x < RoleVisualTextureSize; x++ )
				{
					int filledSamples = 0;
					for ( int sampleY = 0; sampleY < samples; sampleY++ )
					{
						for ( int sampleX = 0; sampleX < samples; sampleX++ )
						{
							float nx = ( ( float )x + ( sampleX + 0.5f ) * sampleStep ) / RoleVisualTextureSize * 2f - 1f;
							float ny = ( ( float )y + ( sampleY + 0.5f ) * sampleStep ) / RoleVisualTextureSize * 2f - 1f;
							if ( IsRoleVisualPixelFilled ( shape, nx, ny ) )
							{
								filledSamples++;
							}
						}
					}

					byte alpha = ( byte )Mathf.RoundToInt ( 255f * filledSamples / ( samples * samples ) );
					pixels [ y * RoleVisualTextureSize + x ] = new Color32 ( 255, 255, 255, alpha );
				}
			}

			texture.SetPixels32 ( pixels );
			texture.Apply ( false, true );
			sprite = Sprite.Create ( texture, new Rect ( 0f, 0f, RoleVisualTextureSize, RoleVisualTextureSize ), new Vector2 ( 0.5f, 0.5f ), RoleVisualTextureSize );
			sprite.name = "Role Visual " + key;
			s_RoleVisualSprites [ key ] = sprite;
			return sprite;
		}

		bool IsRoleVisualPixelFilled ( RoleVisualShape shape, float nx, float ny )
		{
			float distanceSquared = nx * nx + ny * ny;
			switch ( shape )
			{
				case RoleVisualShape.Circle:
					return distanceSquared <= 0.9f;
				case RoleVisualShape.Rectangle:
					return Mathf.Abs ( nx ) <= 0.94f && Mathf.Abs ( ny ) <= 0.94f;
				case RoleVisualShape.RoundedRectangle:
					return RoundedBoxDistance ( nx, ny, 0.92f, 0.82f, 0.28f ) <= 0f;
				case RoleVisualShape.Capsule:
					return CapsuleDistanceSquared ( nx, ny, 0.38f ) <= 0.55f * 0.55f;
				case RoleVisualShape.Triangle:
					return ny >= -0.95f && ny <= 0.95f && Mathf.Abs ( nx ) <= ( 0.95f - ny ) * 0.5f;
				case RoleVisualShape.Diamond:
					return Mathf.Abs ( nx ) + Mathf.Abs ( ny ) <= 0.95f;
				case RoleVisualShape.Ring:
					return distanceSquared <= 0.95f && distanceSquared >= 0.72f;
				case RoleVisualShape.Bolt:
					return PointInPolygon ( new Vector2 ( nx, ny ), s_BoltPoints );
				case RoleVisualShape.Heart:
					return IsHeartPixelFilled ( nx, ny );
				case RoleVisualShape.Star:
					return PointInPolygon ( new Vector2 ( nx, ny ), s_StarPoints );
				case RoleVisualShape.Sparkle:
					return Mathf.Abs ( nx ) * 0.42f + Mathf.Abs ( ny ) <= 0.78f ||
					       Mathf.Abs ( nx ) + Mathf.Abs ( ny ) * 0.42f <= 0.42f;
				case RoleVisualShape.Crescent:
					return ( nx + 0.1f ) * ( nx + 0.1f ) + ny * ny <= 0.82f * 0.82f &&
					       ( nx - 0.22f ) * ( nx - 0.22f ) + ( ny + 0.03f ) * ( ny + 0.03f ) >= 0.62f * 0.62f;
				case RoleVisualShape.Shield:
					return PointInPolygon ( new Vector2 ( nx, ny ), s_ShieldPoints );
				case RoleVisualShape.Smile:
					return IsSmilePixelFilled ( nx, ny );
				case RoleVisualShape.Crown:
					return PointInPolygon ( new Vector2 ( nx, ny ), s_CrownPoints );
				default:
					return false;
			}
		}

		static Vector2[] CreateStarPoints ( int pointCount, float outerRadius, float innerRadius, float startAngle )
		{
			Vector2[] points = new Vector2[pointCount * 2];
			float angleStep = 360f / points.Length;
			for ( int i = 0; i < points.Length; i++ )
			{
				float radius = i % 2 == 0 ? outerRadius : innerRadius;
				float angle = ( startAngle + angleStep * i ) * Mathf.Deg2Rad;
				points [ i ] = new Vector2 ( Mathf.Cos ( angle ) * radius, Mathf.Sin ( angle ) * radius );
			}

			return points;
		}

		float RoundedBoxDistance ( float x, float y, float halfWidth, float halfHeight, float radius )
		{
			float qx = Mathf.Abs ( x ) - halfWidth + radius;
			float qy = Mathf.Abs ( y ) - halfHeight + radius;
			float outsideX = Mathf.Max ( qx, 0f );
			float outsideY = Mathf.Max ( qy, 0f );
			return Mathf.Sqrt ( outsideX * outsideX + outsideY * outsideY ) + Mathf.Min ( Mathf.Max ( qx, qy ), 0f ) - radius;
		}

		float CapsuleDistanceSquared ( float x, float y, float halfSegmentLength )
		{
			float clampedX = Mathf.Clamp ( x, -halfSegmentLength, halfSegmentLength );
			float dx = x - clampedX;
			return dx * dx + y * y;
		}

		bool IsHeartPixelFilled ( float nx, float ny )
		{
			float x = nx * 1.25f;
			float y = ( ny + 0.12f ) * 1.25f;
			float value = x * x + y * y - 0.55f;
			return value * value * value - x * x * y * y * y <= 0f;
		}

		bool IsSmilePixelFilled ( float nx, float ny )
		{
			float dx = nx;
			float dy = ny - 0.28f;
			float distance = Mathf.Sqrt ( dx * dx + dy * dy );
			return distance >= 0.52f && distance <= 0.68f && ny < 0.2f && Mathf.Abs ( nx ) < 0.7f;
		}

		bool PointInPolygon ( Vector2 point, Vector2[] polygon )
		{
			bool inside = false;
			for ( int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++ )
			{
				bool intersects = ( polygon [ i ].y > point.y ) != ( polygon [ j ].y > point.y ) &&
				                  point.x < ( polygon [ j ].x - polygon [ i ].x ) * ( point.y - polygon [ i ].y ) / ( polygon [ j ].y - polygon [ i ].y ) + polygon [ i ].x;
				if ( intersects )
				{
					inside = !inside;
				}
			}

			return inside;
		}

		bool TryUseKnightRevive ( Vector3 deathPosition )
		{
			if ( m_CurrentRole != PlayerCharacterRole.Knight || !m_KnightReviveAvailable )
			{
				return false;
			}

			m_KnightReviveAvailable = false;
			m_Block = false;
			ReviveFromFall ( deathPosition );
			ApplyRoleTint ();
			return true;
		}

		void ReviveFromFall ( Vector3 deathPosition )
		{
			Vector3 revivePosition = deathPosition;
			float safeFloorY = Mathf.Max ( m_LastSafePosition.y, m_InitialPosition.y );
			revivePosition.y = Mathf.Max ( deathPosition.y + m_KnightReviveHeight, safeFloorY + 0.9f, 1.2f );
			m_LastSafePosition = revivePosition;
			m_ReviveGraceTimer = m_KnightReviveGraceDuration;

			transform.position = revivePosition;
			if ( m_Rigidbody2D != null )
			{
				m_Rigidbody2D.velocity = Vector2.zero;
				m_Rigidbody2D.angularVelocity = 0f;
			}
			if ( m_Skeleton != null )
			{
				m_Skeleton.SetActive ( false, Vector2.zero );
			}
			if ( m_JumpParticleSystem != null )
			{
				m_JumpParticleSystem.Play ();
			}
			if ( CameraController.Singleton != null )
			{
				CameraController.Singleton.fastMove = true;
			}
		}

		void PerformJump ()
		{
			if ( m_Rigidbody2D == null )
			{
				return;
			}

			Vector2 velocity = m_Rigidbody2D.velocity;
			velocity.y = m_JumpStrength;
			m_Rigidbody2D.velocity = velocity;
			if ( m_Animator != null )
			{
				m_Animator.ResetTrigger ( "Jump" );
				m_Animator.SetTrigger ( "Jump" );
			}
			if ( m_JumpParticleSystem != null )
			{
				m_JumpParticleSystem.Play ();
			}
			if ( AudioManager.Singleton != null )
			{
				AudioManager.Singleton.PlayJumpSound ( m_JumpAndGroundedAudioSource );
			}
		}

		#endregion

		#region Public Methods

		public void ApplySelectedRole ()
		{
			ApplyRole ( PlayerCharacterSelection.SelectedRole );
		}

		public void ApplyRole ( PlayerCharacterRole role )
		{
			m_CurrentRole = role;
			m_KnightReviveAvailable = role == PlayerCharacterRole.Knight;
			m_ReviveGraceTimer = 0f;
			m_JumpsUsed = 0;
			m_Block = false;
			ApplyRoleStats ();
			ApplyRoleTint ();
			BuildRoleVisuals ();
		}

		public virtual void PlayFootstepSound ()
		{
			if ( m_GroundCheck != null && m_GroundCheck.IsGrounded && AudioManager.Singleton != null )
			{
				AudioManager.Singleton.PlayFootstepSound ( m_FootstepAudioSource, ref m_CurrentFootstepSoundIndex );
			}
		}

		/// <summary>
		/// 根据水平输入设置刚体速度，并按移动方向翻转角色朝向。
		/// </summary>
		public override void Move ( float horizontalAxis )
		{
			if ( IsDead != null && !IsDead.Value && m_Rigidbody2D != null )
			{
				float speed = m_CurrentRunSpeed;
//				if ( CrossPlatformInputManager.GetButton ( "Walk" ) )
//				{
//					speed = m_WalkSpeed;
				//				}
				Vector2 velocity = m_Rigidbody2D.velocity;
				velocity.x = speed * horizontalAxis;
				m_Rigidbody2D.velocity = velocity;
				if ( horizontalAxis > 0f )
				{
					Vector3 scale = transform.localScale;
					scale.x = Mathf.Sign ( horizontalAxis );
					transform.localScale = scale;
				}
				else if ( horizontalAxis < 0f )
				{
					Vector3 scale = transform.localScale;
					scale.x = Mathf.Sign ( horizontalAxis );
					transform.localScale = scale;
				}
			}
		}

		/// <summary>
		/// 只有站在地面上时允许跳跃，避免空中连续跳。
		/// </summary>
		public override void Jump ()
		{
			if ( IsDead != null && !IsDead.Value )
			{
				bool grounded = m_GroundCheck != null && m_GroundCheck.IsGrounded;
				if ( grounded )
				{
					m_JumpsUsed = 0;
				}

				int maximumJumps = m_CurrentRole == PlayerCharacterRole.Monkey ? 2 : 1;
				if ( !grounded && m_CurrentRole != PlayerCharacterRole.Monkey )
				{
					return;
				}
				if ( m_JumpsUsed >= maximumJumps )
				{
					return;
				}

				PerformJump ();
				m_JumpsUsed++;
			}
		}

		public override void Die ()
		{
			Die ( false );
		}

		/// <summary>
		/// 切换到死亡状态，启用骨骼表现；受到攻击时可额外播放血液粒子。
		/// </summary>
		public override void Die ( bool blood )
		{
			if ( IsDead != null && !IsDead.Value )
			{
				if ( !blood && m_ReviveGraceTimer > 0f )
				{
					return;
				}
				if ( !blood && TryUseKnightRevive ( transform.position ) )
				{
					return;
				}

				if ( GameManager.Singleton != null )
				{
					GameManager.Singleton.SetDeathReason ( blood ? GameDeathReason.Obstacle : GameDeathReason.Fall );
				}

                IsDead.Value = true;
				if ( m_Skeleton != null )
				{
					Vector2 velocity = m_Rigidbody2D != null ? m_Rigidbody2D.velocity : Vector2.zero;
					m_Skeleton.SetActive ( true, velocity );
				}
				if ( blood && m_BloodParticleSystem != null )
				{
					ParticleSystem particle = Instantiate<ParticleSystem> (
						                          m_BloodParticleSystem,
						                          transform.position,
						                          Quaternion.identity );
					Destroy ( particle.gameObject, particle.main.duration );
				}
				if ( CameraController.Singleton != null )
				{
					CameraController.Singleton.fastMove = true;
				}
			}
		}

		public override void EmitRunParticle ()
		{
			if ( IsDead != null && !IsDead.Value && m_RunParticleSystem != null )
			{
				m_RunParticleSystem.Emit ( 1 );
			}
		}

		/// <summary>
		/// 复活或重新开始时恢复角色初始状态、速度和骨骼显示。
		/// </summary>
		public override void Reset ()
		{
			if ( IsDead == null )
			{
				IsDead = new Property<bool>(false);
			}

            IsDead.Value = false;
			m_LastSafePosition = transform.position;
			m_ReviveGraceTimer = 0f;
			m_ClosingEye = false;
			m_Guard = false;
			m_Block = false;
			m_CurrentFootstepSoundIndex = 0;
			ApplyRole ( PlayerCharacterSelection.SelectedRole );
			transform.localScale = m_InitialScale;
			if ( m_Rigidbody2D != null )
			{
				m_Rigidbody2D.velocity = Vector2.zero;
			}
			if ( m_Skeleton != null )
			{
				Vector2 velocity = m_Rigidbody2D != null ? m_Rigidbody2D.velocity : Vector2.zero;
				m_Skeleton.SetActive ( false, velocity );
			}
		}

		#endregion

		#region Events

		void GameManager_OnReset ()
		{
			transform.position = m_InitialPosition;
			Reset ();
		}

		void Skeleton_OnActiveChanged ( bool active )
		{
			if ( m_Animator != null )
			{
				m_Animator.enabled = !active;
			}
			if ( m_Collider2D != null )
			{
				m_Collider2D.enabled = !active;
			}
			if ( m_Rigidbody2D != null )
			{
				m_Rigidbody2D.simulated = !active;
			}
		}

		void GroundCheck_OnGrounded ()
		{
			if ( IsDead != null && !IsDead.Value )
			{
				m_JumpsUsed = 0;
				if ( m_JumpParticleSystem != null )
				{
					m_JumpParticleSystem.Play ();
				}
				if ( AudioManager.Singleton != null )
				{
					AudioManager.Singleton.PlayGroundedSound ( m_JumpAndGroundedAudioSource );
				}
			}
		}

		#endregion

		[System.Serializable]
		public class CharacterDeadEvent : UnityEvent
		{

		}

	}

}
