using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ubik.Messaging;
using Ubik.Samples;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject ButtonPrefab;


    private void Awake()
    {

        

    }

    // Start is called before the first frame update
    void Start()
    {
        var button = NetworkSpawner.SpawnPersistent(this, ButtonPrefab);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
