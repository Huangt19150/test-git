using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        owner = true;
        /*        if (owner)
                {
                    transform.position = transform.position + new Vector3(0, 1, 0);
                    owner = false;
                }*/

        //context = NetworkScene.Register(this);
        transform.position = transform.position + new Vector3(0, 1, 0);

        //owner = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
        
        

        //print(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //context.SendJson(new Message(transform));
        if (owner)
        {
            context.SendJson(new Message(transform));
            //owner = false;

        }

        /*        if (owner)
                {
                    owner = false;

                }*/
    }
    private void Awake()
    {
        //owner = false;
        //context = NetworkScene.Register(this); 
        //Destroy(gameObject);
       
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        owner = false;
        var msg = message.FromJson<Message>();
        transform.localPosition = msg.transform.position;
        transform.localRotation = msg.transform.rotation;

    }

}
