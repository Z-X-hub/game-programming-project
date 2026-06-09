using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FoxDash.UI
{
	public class UIScreen : MonoBehaviour
	{
        [SerializeField]
        internal UIScreenInfo ScreenInfo;
		[SerializeField]
		protected Animator m_Animator;
		[SerializeField]
		protected CanvasGroup m_CanvasGroup;

        public bool IsOpen { get; set; }

		protected Transform ScreenContentParent
		{
			get
			{
				return m_CanvasGroup != null ? m_CanvasGroup.transform : transform;
			}
		}

		protected void SetGeneratedChildActive ( string childName, bool active )
		{
			Transform[] children = GetComponentsInChildren<Transform> ( true );
			for ( int i = 0; i < children.Length; i++ )
			{
				Transform child = children [ i ];
				if ( child != null && child.name == childName )
				{
					child.gameObject.SetActive ( active );
				}
			}
		}

        public virtual void UpdateScreenStatus(bool open)
        {
            m_Animator.SetBool("Open", open);
            m_CanvasGroup.interactable = open;
            m_CanvasGroup.blocksRaycasts = open;
            IsOpen = open;
        }
	}

}
