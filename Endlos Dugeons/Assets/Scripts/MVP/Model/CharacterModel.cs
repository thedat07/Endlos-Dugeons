using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public interface ICharacterModel
{
    public ICharacterSO GetInfo();
    public Sprite GetBody();
}

public class CharacterModel : ConcreteSubject, ICharacterModel
{
    CharacterSO m_CharacterSO;
    Sprite m_Body;

    public CharacterModel(CharacterSO characterSO)
    {
        m_CharacterSO = characterSO;
        m_Body = characterSO.body;
    }

    public ICharacterSO GetInfo() => m_CharacterSO;
    public Sprite GetBody() => m_Body;
}