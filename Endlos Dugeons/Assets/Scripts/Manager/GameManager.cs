using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class GameManager : SingletonMaster<GameManager>
{
    public HeroSO heroSO;

    private IManagerModel m_ManagerModel;

    private ICharacterModel m_PlayerModel;
    private ICharacterModel m_EnemyModel;

    public override void Awake()
    {
        base.Awake();
        heroSO.Init();
        // Tạo một instance mới của ScoreModel khi GameManager được tạo
        m_ManagerModel = new ManagerModel();
        m_PlayerModel = new CharacterModel(heroSO);
    }

    public ICharacterModel GetPlayerModel()
    {
        return m_PlayerModel;
    }

    public ICharacterModel GetEnemyModel()
    {
        return m_EnemyModel;
    }

    public void SetEnemyModel(EnemySO enemySO)
    {
        m_EnemyModel = new CharacterModel(enemySO);
    }
    
}
