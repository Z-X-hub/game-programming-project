using UnityEngine;

namespace FoxDash.Characters
{
	public enum PlayerCharacterRole
	{
		Runner,
		Knight,
		Monkey
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
				case PlayerCharacterRole.Knight:
					return "SOLDIER";
				case PlayerCharacterRole.Monkey:
					return "ADVENTURER";
				default:
					return "PLAYER";
			}
		}

		public static string GetAbilityText ( PlayerCharacterRole role )
		{
			switch ( role )
			{
				case PlayerCharacterRole.Knight:
					return "1X REVIVE";
				case PlayerCharacterRole.Monkey:
					return "2X JUMP";
				default:
					return "RUN FAST";
			}
		}

		private static PlayerCharacterRole LoadSelectedRole ()
		{
			int savedValue = PlayerPrefs.GetInt ( PlayerPrefsKey, ( int )PlayerCharacterRole.Runner );
			if ( savedValue < ( int )PlayerCharacterRole.Runner || savedValue > ( int )PlayerCharacterRole.Monkey )
			{
				savedValue = ( int )PlayerCharacterRole.Runner;
			}

			return ( PlayerCharacterRole )savedValue;
		}
	}
}
