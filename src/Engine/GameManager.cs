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

		public GameManager (List<Attack> attacks)
		{
			this.toRemove = new HashSet<long>();
			this.attacks = attacks;
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
			toRemove.Clear();
		}
	}
}
