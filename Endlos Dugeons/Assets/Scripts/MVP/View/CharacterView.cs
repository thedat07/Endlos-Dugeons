using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class CharacterView : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_Body;
    protected Sprite m_SpriteBody;

    protected virtual void Start()
    {
        Init();
    }

    private void Init()
    {
        if (m_SpriteBody != null) m_Body.sprite = m_SpriteBody;
    }
}
