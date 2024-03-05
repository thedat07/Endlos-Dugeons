using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DesignPatterns;

public interface IGamePlayPresenter
{
    Vector2 JoyStick();
    void PopupAttack(UnityAction action, EnemySO enemySO);
}

public class GamePlayPresenter : MonoBehaviour, IGamePlayPresenter
{
    private ICharacterModel m_PlayerModel;

    [Header("Setting")]
    [SerializeField] float m_Speed;

    [Header("Ref")]
    [SerializeField] FloatingJoystick m_FloatingJoystick;
    [SerializeField] PlayerView m_PlayerView;

    [Header("Info")]
    [SerializeField] bool m_IsAttack;

    public Vector2 JoyStick() => new Vector2(m_FloatingJoystick.Horizontal, m_FloatingJoystick.Vertical);

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        m_PlayerModel = GameManager.Instance.GetPlayerModel();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (m_IsAttack) return;
        m_PlayerView.Move(JoyStick() * m_Speed);
    }

    public void PopupAttack(UnityAction action, EnemySO enemySO)
    {
        m_IsAttack = true;
        action += () => {  m_IsAttack = false; };
        GameManager.Instance.SetEnemyModel(enemySO);
        SceneGameManager.Add(SceneGameManager.PopupAttack, new PopupData(action));
    }
}
