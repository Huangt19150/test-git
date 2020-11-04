using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Ubik.Messaging;

namespace Ubik.Samples
{
    public interface ISpawnable
    {
        void OnSpawned(bool local);
    }


    public class NetworkSpawner : MonoBehaviour, INetworkObject, INetworkComponent
    {
        public NetworkId Id { get; } = new NetworkId(3);//todo: get rid of this magic number

        public PrefabCatalogue catalogue;

        private NetworkContext context;

        [Serializable]
        public struct Message // public to avoid warning 0649
        {
            public int catalogueIndex;
            public int networkId;
        }

        void Start()
        {
            context = NetworkScene.Register(this);
        }

        private GameObject Instantiate(int i, int networkId, bool local)
        {
            var go = GameObject.Instantiate(catalogue.prefabs[i], transform);
            go.GetNetworkObjectInChildren().Id.Set(networkId);
            foreach (var item in go.GetComponentsInChildren<MonoBehaviour>())
            {
                if(item is ISpawnable)
                {
                    (item as ISpawnable).OnSpawned(local);
                }
            }
            return go;
        }

        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            var msg = message.FromJson<Message>();
            Instantiate(msg.catalogueIndex, msg.networkId, false);
        }

        public GameObject Spawn(int i)
        {
            var networkId = NetworkScene.GenerateUniqueId();
            context.SendJson(new Message() { catalogueIndex = i, networkId = networkId });
            return Instantiate(i, networkId, true);
        }

        public GameObject Spawn(GameObject gameObject)
        {
            return Spawn(catalogue.IndexOf(gameObject));
        }

        public static GameObject Spawn(MonoBehaviour caller, GameObject prefab)
        {
            var spawner = FindNetworkSpawner(NetworkScene.FindNetworkScene(caller));
            return spawner.Spawn(prefab);
        }

        public static NetworkSpawner FindNetworkSpawner(NetworkScene scene)
        {
            var spawner = scene.GetComponentInChildren<NetworkSpawner>();
            Debug.Assert(spawner != null, $"Cannot find NetworkSpawner Component for {scene}. Ensure a NetworkSpawner Component has been added.");
            return spawner;
        }
    }

}