using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public static class SceneGameManager
{
    public static Object dataScene;
    public readonly static string SceneMenu = "Menu";
    public readonly static string SceneGamePlay = "GamePlay";
    public readonly static string PopupAttack = "PopupAttack";

    public static void Add(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public static void Add(string scene, Object data)
    {
        dataScene = data;
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public static void Hide(string scene)
    {
        SceneManager.UnloadScene(scene);
    }

    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void Load(string scene, Object data)
    {
        dataScene = data;
        SceneManager.LoadScene(scene);
    }
}

public class PopupData
{
    public UnityAction callback;
    public PopupData(UnityAction callback)
    {
        this.callback = callback;
    }
}
