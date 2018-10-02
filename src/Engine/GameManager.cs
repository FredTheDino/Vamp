using System;
using System.Collections.Generic;

namespace Vamp
{
	public class GameManager
	{
		// The list with object id's to remove
		private HashSet<long> toRemove;

		// Attack list
		private List<Attack> attacks;

		private List<Enemy> enemies;

		public GameManager (List<Attack> attacks, List<Enemy> enemies)
		{
			this.toRemove = new HashSet<long>();
			this.attacks = attacks;
			this.enemies = enemies;
		}

		// Mark a GameObject for removal
		public void MarkForRemoval (GameObject gameObject)
		{
			toRemove.Add(gameObject.Id);
		}

		// Removes all marked GameObjects
		public void RemoveObjects ()
		{
			for (int i = attacks.Count - 1; i >= 0; i--)
			{
				if (toRemove.Contains(attacks[i].Id))
				{
					attacks.RemoveAt(i);
				}
			}

			for (int i = enemies.Count - 1; i >= 0; i--)
			{
				if (toRemove.Contains(enemies[i].Id))
				{
					enemies.RemoveAt(i);
				}
			}

			toRemove.Clear();
		}
	}
}
