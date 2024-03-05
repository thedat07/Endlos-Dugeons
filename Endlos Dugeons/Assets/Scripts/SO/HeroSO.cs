using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HeroSO", order = 1)]
public class HeroSO : CharacterSO
{
    public void Init()
    {
        var json = PlayerPrefs.GetString(Static.UseData, string.Empty);
        if (!string.IsNullOrEmpty(json))
        {
            info = JsonConvert.DeserializeObject<CharacterInfo>(json);
        }
        else
        {
            info = new CharacterInfo();
        }
    }
}
