using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LibraryGame;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DesignPatterns;

public interface IUIInfoView
{
    void Init(ICharacterSO characterInfo, Sprite body);
    void UpdateView(float hp);
    bool Die();
}

public class UIInfoView : MonoBehaviour, IUIInfoView
{
    [SerializeField] Image m_ImageBody;
    [SerializeField] RectTransform m_ImageHp;
    [SerializeField] RectTransform m_ImageShield;
    [SerializeField] TextMeshProUGUI m_TxtLevel;
    [SerializeField] TextMeshProUGUI m_TxtHp;
    [SerializeField] TextMeshProUGUI m_TxtShield;
    [SerializeField] TextMeshProUGUI m_TxtName;
    [SerializeField] TextMeshProUGUI m_TxtInfo;

    float m_Hp;
    float m_CurHp;
    float m_Shield;
    float m_CurShield;

    public bool Die() => m_CurHp <= 0 ? true : false;

    public void Init(ICharacterSO characterInfo, Sprite body)
    {
        m_ImageBody.sprite = body;
        m_ImageBody.GetComponent<RectTransform>().sizeDelta = new Vector2(m_ImageBody.sprite.texture.width, m_ImageBody.sprite.texture.height);

        m_Hp = characterInfo.GetHp();
        m_CurHp = m_Hp;

        m_Shield = characterInfo.GetDef();
        m_CurShield = m_Shield;

        m_ImageShield.gameObject.SetActive(m_CurShield == 0 ? false : true);
        m_TxtShield.gameObject.SetActive(m_CurShield == 0 ? false : true);

        m_TxtHp.text = string.Format("{0}/{1}", AbbrevationUtility.AbbreviateNumber(m_CurHp), AbbrevationUtility.AbbreviateNumber(m_Hp));
        m_TxtShield.text = string.Format("{0} Shield", AbbrevationUtility.AbbreviateNumber(m_CurShield));
        m_TxtLevel.text = string.Format("Lv. {0}", characterInfo.GetLevel());

        m_TxtInfo.text = string.Format(
            "{0} ATK {1} DEF {2} AGI {3} AS",
            AbbrevationUtility.AbbreviateNumber(characterInfo.GetAtk()),
            AbbrevationUtility.AbbreviateNumber(characterInfo.GetDef()),
            AbbrevationUtility.AbbreviateNumber(characterInfo.GetAgi()),
            AbbrevationUtility.AbbreviateNumber(characterInfo.GetAs())
        );
    }

    public void UpdateView(float hp)
    {
        if (m_CurShield <= 0)
        {
            m_CurHp += hp;
            if (m_CurHp <= 0)
            {
                m_CurHp = 0;
            }
        }
        else
        {
            m_CurShield += hp;
            if (m_CurShield <= 0)
            {
                m_CurHp += m_CurShield;
                m_CurShield = 0;
            }
        }

        m_TxtHp.text = string.Format("{0}/{1}", AbbrevationUtility.AbbreviateNumber(m_CurHp), AbbrevationUtility.AbbreviateNumber(m_Hp));
        m_TxtShield.text = string.Format("{0} Shield", AbbrevationUtility.AbbreviateNumber(m_CurShield));

        m_ImageHp.DOScaleX(m_CurHp / m_Hp, 0.1f).SetLink(gameObject, LinkBehaviour.PauseOnDisable);

        m_ImageShield.DOScaleX(m_CurShield / m_Shield, 0.1f).OnComplete(() =>
        {
            m_ImageShield.gameObject.SetActive(m_CurShield == 0 ? false : true);
            m_TxtShield.gameObject.SetActive(m_CurShield == 0 ? false : true);
        }).SetLink(gameObject, LinkBehaviour.PauseOnDisable);
    }
}
