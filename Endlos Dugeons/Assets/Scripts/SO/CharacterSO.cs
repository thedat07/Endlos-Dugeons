using UnityEngine;

public interface ICharacterSO
{
    float GetLevel();
    float GetDamge();
    float GetHp();
    float GetAtk();
    float GetDef();
    float GetAgi();
    float GetLuk();
    float GetAs();
    float GetCirt();
    float GetEC();
}

[System.Serializable]
public class CharacterInfo
{
    public float Level;
    public float Hp;
    public float Atk;
    public float Def;
    public float Agi;
    public float Luk;
    public float As;
    public float Crit;
    public float EC;

    public CharacterInfo(CharacterSO characterSO)
    {
        Level = characterSO.info.Level;
        Hp = characterSO.info.Hp;
        Atk = characterSO.info.Atk;
        Def = characterSO.info.Def;
        Agi = characterSO.info.Agi;
        Luk = characterSO.info.Luk;
        As = characterSO.info.As;
        Crit = characterSO.info.Crit;
        EC = characterSO.info.EC;
    }

    public CharacterInfo()
    {
        Level = 1;
        Hp = 1;
        Atk = 1;
        Def = 1;
        Agi = 1;
        Luk = 1;
        As = 1;
        Crit = 1;
        EC = 0;
    }
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterSO", order = 1)]
public class CharacterSO : ScriptableObject, ICharacterSO
{
    public CharacterInfo info;
    public Sprite body;
    public ParticleSystem effect;

    public float GetLevel() => info.Level;
    
    public virtual float GetHp() => info.Hp * 64;

    public virtual float GetAtk() => info.Atk * 4;
    
    public virtual float GetDef() => info.Def * 12;

    public virtual float GetAgi() => info.Agi * 4;

    public virtual float GetLuk() => info.Luk * 4;

    public virtual float GetAs() => info.As + (GetAgi() / 10000);

    public virtual float GetCirt() => info.Crit + (GetAgi() / 10000);

    public virtual float GetEC() => info.EC * (GetAgi() / 10000);

    public virtual float GetDamge()
    {
        bool isCrit = (Random.value < GetCirt());
        float damage = GetAtk() * (isCrit ? 1.5f : 1);
        return damage;
    }
}
