using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public interface IPlayerView : IObserver
{
    // Định nghĩa các phương thức cần thiết cho View
    void UpdateView();
    void Move(Vector2 velocity);
}

public class PlayerView : CharacterView, IPlayerView
{
    private ICharacterModel m_PlayerModel;

    protected override void Start()
    {
        m_PlayerModel = GameManager.Instance.GetPlayerModel();
        (m_PlayerModel as Subject)?.AddObserver(this);
        UpdateView();
        base.Start();
    }

    public void OnNotify()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        m_SpriteBody = m_PlayerModel.GetBody();
    }

    public void Move(Vector2 velocity)
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }
}
