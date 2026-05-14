using UnityEngine;

public static class Damage
{
    public static bool Apply(GameObject target, int damageAmount, int teamId)
    {
        if (target == null)
        {
            return false;
        }

        if (teamId == 0)
        {
            ArcadeEnemy enemy = target.GetComponent<ArcadeEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount);
                return true;
            }

            DestructibleAsteroid asteroid = target.GetComponent<DestructibleAsteroid>();
            if (asteroid != null)
            {
                asteroid.TakeDamage(damageAmount);
                return true;
            }

            return false;
        }

        ArcadePlayerController player = target.GetComponent<ArcadePlayerController>();
        if (player != null)
        {
            player.TakeDamage(damageAmount);
            return true;
        }

        return false;
    }
}
