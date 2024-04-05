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
    private PoolManager _pool = new();
    private DataManager _data = new();
    private UIManager _ui = new();
    private SoundManager _sound = new();

    public static Managers Instance {
        get
        {
            Init();
            return _instance;
        }
    }

    public static bool IsInitialized = false;

    // TODO Input other Manager Properties
    public static GameSceneManager GameSceneManager { get { return Instance._gameScene; } }
    public static GameManager GameManager { get { return Instance._game; } }
    public static ResourceManager ResourceManager { get { return Instance._resource; } }
    public static InputManager InputManager { get { return Instance._input; } }
    public static PoolManager PoolManager { get { return Instance._pool; } }
    public static DataManager DataManager { get { return Instance._data; } }
    public static UIManager UIManager { get { return Instance._ui; } }
    public static SoundManager SoundManager { get { return Instance._sound; } }

    private void Start()
    {
        Init();

        _data.Init();
        _sound.Init();

        IsInitialized = true;
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    public static void Init()
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
