using Booma.Instance.Common;
using Booma.Instance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GladNet.Engine.Common;
using System.Collections;

namespace Booma.Instance.Server
{
	//TODO: Thread safety
	public class ServerPlayerEntityCollection : IServerPlayerEntityCollection
	{
		/// <summary>
		/// Dictionary that is managed internally. We don't expose a dictionary directly so that we can extend the behavior.
		/// </summary>
		private Dictionary<int, GameObject> internallyManagedPlayerGameobjectCollection = new Dictionary<int, GameObject>();

		/// <summary>
		/// Internally managed reference list to the peers.
		/// </summary>
		private List<INetPeer> internalManagedPeerCollection = new List<INetPeer>();

		/// <summary>
		/// Indicates if the collection has been made dirty.
		/// </summary>
		private bool isDirty { get; set; } = false;

		//TODO: Implement enum flag system for determining dirty-ness for future features.

		public GameObject this[int key]
		{
			get
			{
				return internallyManagedPlayerGameobjectCollection[key];
			}

			set
			{
				//Set makes it dirty
				isDirty = true;

				internallyManagedPlayerGameobjectCollection[key] = value;
			}
		}

		public EntityType CollectionType { get; } = EntityType.Player;

		public int Count
		{
			get
			{
				return internallyManagedPlayerGameobjectCollection.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((IDictionary<int, GameObject>)internallyManagedPlayerGameobjectCollection).IsReadOnly;
			}
		}

		public ICollection<int> Keys
		{
			get
			{
				return internallyManagedPlayerGameobjectCollection.Keys;
			}
		}

		public ICollection<GameObject> Values
		{
			get
			{
				return internallyManagedPlayerGameobjectCollection.Values;
			}
		}

		public void Add(KeyValuePair<int, GameObject> item)
		{
			//Add makes it dirty
			isDirty = true;

			((IDictionary<int, GameObject>)internallyManagedPlayerGameobjectCollection).Add(item);
		}

		public void Add(int key, GameObject value)
		{
			//Add makes it dirty
			isDirty = true;

			internallyManagedPlayerGameobjectCollection.Add(key, value);
		}

		public IEnumerable<INetPeer> AllPeers()
		{
			//If we're dirty we must rebuild the peer list
			if (isDirty)
			{
				internalManagedPeerCollection.Clear();

				foreach (GameObject go in this.internallyManagedPlayerGameobjectCollection.Values)
				{
					PlayerNetworkEntity networkTag = go.GetComponent<PlayerNetworkEntity>();

					internalManagedPeerCollection.Add(networkTag.Peer);
				}

				//Once done we should set not dirty
				isDirty = false;
			}

			return internalManagedPeerCollection;
		}

		public void Clear()
		{
			//Clear makes it dirty
			isDirty = true;

			internallyManagedPlayerGameobjectCollection.Clear();
		}

		public bool Contains(KeyValuePair<int, GameObject> item)
		{
			return internallyManagedPlayerGameobjectCollection.Contains(item);
		}

		public bool ContainsKey(int key)
		{
			return internallyManagedPlayerGameobjectCollection.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<int, GameObject>[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<KeyValuePair<int, GameObject>> GetEnumerator()
		{
			return internallyManagedPlayerGameobjectCollection.GetEnumerator();
		}

		public bool Remove(KeyValuePair<int, GameObject> item)
		{
			//Remove makes it dirty
			isDirty = true;

			return ((IDictionary<int, GameObject>)internallyManagedPlayerGameobjectCollection).Remove(item);
		}

		public bool Remove(int key)
		{
			//Remove makes it dirty
			isDirty = true;

			return internallyManagedPlayerGameobjectCollection.Remove(key);
		}

		public bool TryGetValue(int key, out GameObject value)
		{
			//I'm lazy
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return internallyManagedPlayerGameobjectCollection.GetEnumerator();
		}
	}
}
