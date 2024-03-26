using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers _instance;

    // TODO Input other Manager 
    private GameSceneManager _gameScene = new();
    private GameManager _game = new();
    private ResourceManager _resource = new();
    private InputManager _input = new();
    private CameraManager _camera = new();
    private PoolManager _pool = new();
    private DataManager _data = new();
    private UIManager _ui = new();

    static Managers Instance {
        get
        {
            Init();
            return _instance;
        }
    }

    // TODO Input other Manager Properties
    public static GameSceneManager GameSceneManager { get { return Instance._gameScene; } }
    public static GameManager GameManager { get { return Instance._game; } }
    public static ResourceManager ResourceManager { get { return Instance._resource; } }
    public static InputManager InputManager { get { return Instance._input; } }
    public static CameraManager CameraManager { get { return Instance._camera; } }
    public static PoolManager PoolManager { get { return Instance._pool; } }
    public static DataManager DataManager { get { return Instance._data; } }
    public static UIManager UIManager { get { return Instance._ui; } }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject gameObj = GameObject.Find("--Managers");
        if (gameObj == null)
        {
            gameObj = new GameObject { name = "--Managers" };
            gameObj.AddComponent<Managers>();
        }
        DontDestroyOnLoad(gameObj);
        _instance = gameObj.GetComponent<Managers>();
    }
}
