using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PopupAttackPresenter : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] UIInfoView m_UIEnmey;
    [SerializeField] UIInfoView m_UIHero;

    IUIInfoView m_IEnemy;
    IUIInfoView m_IHero;

    ICharacterModel m_PlayerModel;
    ICharacterModel m_EnemyModel;

    PopupData m_Data;

    Coroutine m_CoroutinePlayer;
    Coroutine m_CoroutineEnemy;

    private void Awake()
    {
        m_IEnemy = m_UIEnmey;
        m_IHero = m_UIHero;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Data = (PopupData)SceneGameManager.dataScene;

        m_PlayerModel = GameManager.Instance.GetPlayerModel();
        m_EnemyModel = GameManager.Instance.GetEnemyModel();

        m_IHero.Init(m_PlayerModel.GetInfo(), m_PlayerModel.GetBody());
        m_IEnemy.Init(m_EnemyModel.GetInfo(), m_EnemyModel.GetBody());

        m_CoroutinePlayer = StartCoroutine(AutoPlayerAttack());
        m_CoroutineEnemy = StartCoroutine(AutoEnemyAttack());
    }

    IEnumerator AutoPlayerAttack()
    {
        while (true)
        {
            m_IEnemy.UpdateView(-m_PlayerModel.GetInfo().GetDamge());
            if (m_IEnemy.Die() == true)
            {
                Debug.Log("Kill Hero");
                StopAutoAttack();
            }
            yield return new WaitForSeconds(m_PlayerModel.GetInfo().GetAs());
        }
    }

    IEnumerator AutoEnemyAttack()
    {
        while (true)
        {
            m_IHero.UpdateView(-m_EnemyModel.GetInfo().GetDamge());
            if (m_IHero.Die() == true)
            {
                Debug.Log("Kill Hero");
                StopAutoAttack();
            }
            yield return new WaitForSeconds(m_EnemyModel.GetInfo().GetAs());
        }
    }

    private void StopAutoAttack()
    {
        StopCoroutine(m_CoroutinePlayer);
        StopCoroutine(m_CoroutineEnemy);
        DOVirtual.DelayedCall(1, () =>
        {
            SceneGameManager.Hide(SceneGameManager.PopupAttack);
        });
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        m_Data.callback?.Invoke();
    }
}
