using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;
using UnityEngine.SceneManagement;

public interface IEnemyView : IObserver
{
    // Định nghĩa các phương thức cần thiết cho View
    void UpdateView();
}

public class EnemyView : CharacterView, IEnemyView
{
    [SerializeField] EnemySO m_EnemySO;
    [SerializeField] bool m_Touch;
    IGamePlayPresenter m_IGamePlayPresenter;

    protected override void Start()
    {
        m_IGamePlayPresenter = FindFirstObjectByType<GamePlayPresenter>();
        m_SpriteBody = m_EnemySO.body;
        base.Start();
    }

    public void OnNotify()
    {
        UpdateView();
    }

    public void UpdateView()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && m_Touch == false)
        {
            m_IGamePlayPresenter.PopupAttack(() => { gameObject.SetActive(false); }, m_EnemySO);
            m_Touch = true;
        }
    }
}
