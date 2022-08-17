using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManger : MonoBehaviourPunCallbacks
{
    public static RoomManger Instance;
    void Awake()
    {
        if(Instance)// checks if this is the only roommanger
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);//i am the only one
        Instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerManger"), Vector3.zero,Quaternion.identity);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
