using UnityEngine;

namespace FoxDash.Characters
{
	public enum PlayerCharacterRole
	{
		Player,
		Soldier,
		Adventurer
	}

	public static class PlayerCharacterSelection
	{
		private const string PlayerPrefsKey = "FoxDash.SelectedCharacterRole";
		private static PlayerCharacterRole m_SelectedRole = LoadSelectedRole ();

		public static PlayerCharacterRole SelectedRole
		{
			get
			{
				return m_SelectedRole;
			}
			set
			{
				m_SelectedRole = value;
				PlayerPrefs.SetInt ( PlayerPrefsKey, ( int )m_SelectedRole );
				PlayerPrefs.Save ();
			}
		}

		public static string GetDisplayName ( PlayerCharacterRole role )
		{
			switch ( role )
			{
				case PlayerCharacterRole.Soldier:
					return "SOLDIER";
				case PlayerCharacterRole.Adventurer:
					return "ADVENTURER";
				default:
					return "PLAYER";
			}
		}

		public static string GetAbilityText ( PlayerCharacterRole role )
		{
			switch ( role )
			{
				case PlayerCharacterRole.Soldier:
					return "1X REVIVE";
				case PlayerCharacterRole.Adventurer:
					return "2X JUMP";
				default:
					return "RUN FAST";
			}
		}

		private static PlayerCharacterRole LoadSelectedRole ()
		{
			int savedValue = PlayerPrefs.GetInt ( PlayerPrefsKey, ( int )PlayerCharacterRole.Player );
			if ( savedValue < ( int )PlayerCharacterRole.Player || savedValue > ( int )PlayerCharacterRole.Adventurer )
			{
				savedValue = ( int )PlayerCharacterRole.Player;
			}

			return ( PlayerCharacterRole )savedValue;
		}
	}
}
