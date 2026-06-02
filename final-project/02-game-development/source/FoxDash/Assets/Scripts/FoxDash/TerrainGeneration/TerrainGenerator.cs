using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FoxDash.Characters;

namespace FoxDash.TerrainGeneration
{

	public abstract class TerrainGenerator : MonoBehaviour
	{

		private static TerrainGenerator m_Singleton;

		public static TerrainGenerator Singleton
		{
			get
			{
				return m_Singleton;
			}
		}

		protected Dictionary<Vector3, Block> m_Blocks;
		protected Dictionary<Vector3, BackgroundBlock> m_BackgroundBlocks;
		protected BackgroundLayer[] m_BackgroundLayers;
		protected float m_PreviousX;
		protected float m_CurrentX;
		protected float m_FathestBackgroundX;
		[SerializeField]
		protected TerrainGenerationSettings m_Settings;
		protected int m_GeneratedStartBlocksCount;
		protected int m_GeneratedMiddleBlocksCount;
		protected int m_GeneratedEndBlocksCount;
		[SerializeField]
		protected float m_DestroyRange = 100f;
		[SerializeField]
		protected float m_GenerateRange = 100f;
		[SerializeField]
		protected float m_BackgroundGenerateRange = 200f;
		[SerializeField]
		protected Character m_Character;
		protected Block m_LastBlock;
		protected BackgroundBlock m_LastBackgroundBlock;
		protected float m_RemoveTime = 0f;
		protected bool m_Reset = false;

		public float PreviousX
		{
			get
			{
				return m_PreviousX;
			}
		}

		public float CurrentX
		{
			get
			{
				return m_CurrentX;
			}
		}

		public TerrainGenerationSettings Settings
		{
			get
			{
				return m_Settings;
			}
		}

		protected virtual void Awake ()
		{
			if ( m_Singleton != null )
			{
				Destroy ( gameObject );
				return;
			}
			m_Singleton = this;
			m_Blocks = new Dictionary<Vector3, Block> ();
			m_BackgroundBlocks = new Dictionary<Vector3, BackgroundBlock> ();
			m_BackgroundLayers = new BackgroundLayer[m_Settings.BackgroundLayers.Length];
			for ( int i = 0; i < m_Settings.BackgroundLayers.Length; i++ )
			{
				m_BackgroundLayers [ i ] = m_Settings.BackgroundLayers [ i ];
			}
			GameManager.OnReset += Reset;
		}

		/// <summary>
		/// 清空当前生成的地形和背景，把生成游标恢复到关卡起点。
		/// </summary>
		protected virtual void Reset ()
		{
			m_Reset = true;
			RemoveAll ();
			m_CurrentX = 0f;
			m_LastBlock = null;
			m_LastBackgroundBlock = null;
			for ( int i = 0; i < m_BackgroundLayers.Length; i++ )
			{
				m_BackgroundLayers [ i ].Reset ();
			}
			m_FathestBackgroundX = 0f;
			m_Blocks.Clear ();
			m_BackgroundBlocks.Clear ();
			m_GeneratedStartBlocksCount = 0;
			m_GeneratedMiddleBlocksCount = 0;
			m_GeneratedEndBlocksCount = 0;
			m_Reset = false;
		}

		protected virtual void OnDestroy ()
		{
			m_Singleton = null;
		}

		/// <summary>
		/// 每帧按角色位置补齐前方地形，并定时清理身后过远的对象。
		/// </summary>
		protected virtual void Update ()
		{
			if ( m_Reset )
			{
				return;
			}
			if ( m_RemoveTime < Time.time )
			{
				m_RemoveTime = Time.time + 5f;
				Remove ();
			}
			Generate ();
		}

		/// <summary>
		/// 按开始段、中间段、结束段的顺序生成地形块，同时随机补充远景背景块。
		/// </summary>
		public virtual void Generate ()
		{
			if ( m_CurrentX < m_Settings.LevelLength || m_Settings.LevelLength <= 0 )
			{
				bool isEnd = false, isStart = false, isMiddle = false;
				Block block = null;
				Vector3 current = new Vector3 ( m_CurrentX, 0f, 0f );
				float newX = 0f;
				if ( m_GeneratedStartBlocksCount < m_Settings.StartBlocksCount || m_Settings.StartBlocksCount <= 0 )
				{
					isStart = true;
					block = ChooseFrom ( m_Settings.StartBlocks );
				}
				else if ( m_GeneratedMiddleBlocksCount < m_Settings.MiddleBlocksCount || m_Settings.MiddleBlocksCount <= 0 )
				{
					isMiddle = true;
					block = ChooseFrom ( m_Settings.MiddleBlocks );
				}
				else if ( m_GeneratedEndBlocksCount < m_Settings.EndBlocksCount || m_Settings.EndBlocksCount <= 0 )
				{
					isEnd = true;
					block = ChooseFrom ( m_Settings.EndBlocks );
				}
				if ( m_LastBlock != null )
				{
					newX = m_CurrentX + m_LastBlock.Width;
				}
				else
				{
					newX = 0f;
				}
				if ( block != null && ( m_LastBlock == null || newX < m_Character.transform.position.x + m_GenerateRange ) )
				{
					if ( isStart )
					{
						if ( m_Settings.StartBlocksCount > 0 )
						{
							m_GeneratedStartBlocksCount++;
						}
					}
					else if ( isMiddle )
					{
						if ( m_Settings.MiddleBlocksCount > 0 )
						{
							m_GeneratedMiddleBlocksCount++;
						}
					}
					else if ( isEnd )
					{
						if ( m_Settings.EndBlocksCount > 0 )
						{
							m_GeneratedEndBlocksCount++;
						}
					}
					CreateBlock ( block, current );
				}
			}
			for ( int i = 0; i < m_BackgroundLayers.Length; i++ )
			{
				int random = Random.Range ( 0, 2 );
				bool generate = random == 1 ? true : false;
				if ( !generate )
				{
					continue;
				}
				Vector3 current = new Vector3 ( m_BackgroundLayers [ i ].CurrentX, 0f, 0f );
				BackgroundBlock block = ( BackgroundBlock )ChooseFrom ( m_BackgroundLayers [ i ].Blocks );
				float newX = 0f;
				if ( m_BackgroundLayers [ i ].LastBlock != null )
				{
					newX = m_BackgroundLayers [ i ].CurrentX + m_BackgroundLayers [ i ].LastBlock.Width;
				}
				else
				{
					newX = 0f;
				}
				if ( block != null && ( m_BackgroundLayers [ i ].LastBlock == null || newX < m_Character.transform.position.x + m_BackgroundGenerateRange ) )
				{
					CreateBackgroundBlock ( block, current, m_BackgroundLayers [ i ], i );
				}
			}
		}

		/// <summary>
		/// 移除已经远离生成游标的地形和背景，控制场景对象数量。
		/// </summary>
		public virtual void Remove ()
		{
			List<Block> blocksToRemove = new List<Block> ();
			foreach ( KeyValuePair<Vector3, Block> block in m_Blocks )
			{
				if ( block.Value.transform.position.x - m_CurrentX > m_DestroyRange )
				{
					blocksToRemove.Add ( block.Value );
				}
			}
			List<BackgroundBlock> backgroundBlocksToRemove = new List<BackgroundBlock> ();
			foreach ( KeyValuePair<Vector3, BackgroundBlock> block in m_BackgroundBlocks )
			{
				if ( block.Value.transform.position.x - m_FathestBackgroundX > m_DestroyRange )
				{
					backgroundBlocksToRemove.Add ( block.Value );
				}
			}
			for ( int i = 0; i < blocksToRemove.Count; i++ )
			{
				RemoveBlock ( blocksToRemove [ i ] );
			}
			for ( int i = 0; i < backgroundBlocksToRemove.Count; i++ )
			{
				RemoveBackgroundBlock ( backgroundBlocksToRemove [ i ] );
			}
		}

		/// <summary>
		/// 重开一局时移除所有运行时生成对象。
		/// </summary>
		public virtual void RemoveAll ()
		{
			List<Block> blocksToRemove = new List<Block> ();
			foreach ( KeyValuePair<Vector3, Block> block in m_Blocks )
			{
				blocksToRemove.Add ( block.Value );
			}
			List<BackgroundBlock> backgroundBlocksToRemove = new List<BackgroundBlock> ();
			foreach ( KeyValuePair<Vector3, BackgroundBlock> block in m_BackgroundBlocks )
			{
				backgroundBlocksToRemove.Add ( block.Value );
			}
			for ( int i = 0; i < blocksToRemove.Count; i++ )
			{
				RemoveBlock ( blocksToRemove [ i ] );
			}
			for ( int i = 0; i < backgroundBlocksToRemove.Count; i++ )
			{
				RemoveBackgroundBlock ( backgroundBlocksToRemove [ i ] );
			}
		}

		public virtual void RemoveBlockAt ( Vector3 position )
		{
			RemoveBlock ( m_Blocks [ position ] );
		}

		public virtual void RemoveBlock ( Block block )
		{
			block.OnRemove ( this );
			Destroy ( m_Blocks [ block.transform.position ].gameObject );
			m_Blocks.Remove ( block.transform.position );
		}

		public virtual void RemoveBackgroundBlock ( BackgroundBlock block )
		{
			block.OnRemove ( this );
			Destroy ( m_BackgroundBlocks [ block.transform.position ].gameObject );
			m_BackgroundBlocks.Remove ( block.transform.position );
		}

		/// <summary>
		/// 实例化一个地形块，并推进主地形生成游标。
		/// </summary>
		public virtual bool CreateBlock ( Block blockPrefab, Vector3 position )
		{
			if ( blockPrefab == null )
			{
				return false;
			}
			blockPrefab.PreGenerate ( this );
			Block block = Instantiate<Block> ( blockPrefab, position, Quaternion.identity );
			m_PreviousX = m_CurrentX;
			m_CurrentX += block.Width;
			m_Blocks.Add ( position, block );
			blockPrefab.PostGenerate ( this );
			m_LastBlock = block;
			return true;
		}

		/// <summary>
		/// 实例化背景块，并按随机宽度推进对应背景层的生成游标。
		/// </summary>
		public virtual bool CreateBackgroundBlock ( BackgroundBlock blockPrefab, Vector3 position, BackgroundLayer layer, int layerIndex )
		{
			if ( blockPrefab == null )
			{
				return false;
			}
			blockPrefab.PreGenerate ( this );
			position.z = blockPrefab.transform.position.z;
			position.y = blockPrefab.transform.position.y;
			BackgroundBlock block = Instantiate<BackgroundBlock> ( blockPrefab, position, Quaternion.identity );
			float width = Random.Range ( block.MinWidth, block.MaxWidth );
			m_BackgroundLayers [ layerIndex ].PreviousX = m_BackgroundLayers [ layerIndex ].CurrentX;
			m_BackgroundLayers [ layerIndex ].CurrentX += width;
			block.Width = width;
			m_BackgroundLayers [ layerIndex ].LastBlock = block;
			m_BackgroundBlocks.Add ( position, block );
			blockPrefab.PostGenerate ( this );
			if ( m_BackgroundLayers [ layerIndex ].CurrentX > m_FathestBackgroundX )
			{
				m_FathestBackgroundX = m_BackgroundLayers [ layerIndex ].CurrentX;
			}
			return true;
		}

		/// <summary>
		/// 查找角色当前脚下所在的地形块，重生时会用它计算安全位置。
		/// </summary>
		public Block GetCharacterBlock ()
		{
			Block characterBlock = null;
			foreach ( KeyValuePair<Vector3, Block> block in m_Blocks )
			{
				if ( block.Key.x <= m_Character.transform.position.x && block.Key.x + block.Value.Width > m_Character.transform.position.x )
				{
					characterBlock = block.Value;
					break;
				}
			}
			return characterBlock;
		}

		/// <summary>
		/// 按每个地形块的 Probability 权重随机选择一个预制体。
		/// </summary>
		public static Block ChooseFrom ( Block[] blocks )
		{
			if ( blocks.Length <= 0 )
			{
				return null;
			}
			float total = 0;
			for ( int i = 0; i < blocks.Length; i++ )
			{
				total += blocks [ i ].Probability;
			}
			float randomPoint = Random.value * total;
			for ( int i = 0; i < blocks.Length; i++ )
			{
				if ( randomPoint < blocks [ i ].Probability )
				{
					return blocks [ i ];
				}
				else
				{
					randomPoint -= blocks [ i ].Probability;
				}
			}
			return blocks [ blocks.Length - 1 ];
		}

	}

}
