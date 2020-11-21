using System.Collections;
using System.Collections.Generic;
using Ubik.Messaging;
using Ubik.Samples;
using Ubik.XR;
using UnityEngine;

public class ButtonControl : MonoBehaviour, IUseable, INetworkComponent, INetworkObject
{
    private NetworkContext context;
    private bool owner;

    public NetworkId Id { get; } = new NetworkId();

    [System.Serializable]
    public struct Message
    {
        public TransformMessage transform;
        public Message(Transform transform)
        {
            this.transform = new TransformMessage(transform);
        }
    }

    public void UnUse(Hand controller)
    {
        
    }

    public void Use(Hand controller)
    {
        //owner = true;
        transform.position = transform.position + new Vector3(0, 1, 0);
        //context = NetworkScene.Register(this);
        
        context.SendJson(new Message(transform));

        //owner = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //context = NetworkScene.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
       // if (owner)
        //{
            // Need to send multiple variables in one message

        //}
    }
    private void Awake()
    {
        //owner = false;
        //context = NetworkScene.Register(this); 
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        transform.localPosition = msg.transform.position;

    }
}
