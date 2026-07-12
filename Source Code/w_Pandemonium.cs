using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static WingidonExpansionPack.MainMod;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Pandemonium : Demon
{
    public override Il2CppSystem.Collections.Generic.List<SpecialRule> GetRules()
    {
        Il2CppSystem.Collections.Generic.List<SpecialRule> sr = new Il2CppSystem.Collections.Generic.List<SpecialRule>();
        sr.Add(new NightModeRule(4));
        return sr;
    }
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Health health = PlayerController.PlayerInfo.health;
            health.AddMaxHp(Gameplay.CurrentCharacters.Count);
            health.AddMaxHp(-10);
            health.Heal(100);
            Il2CppSystem.Collections.Generic.List<CharacterData> possibleDemons = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<string> possibleDemonIDs = sharedScripts.GetPossibleCharacterIDsOfRole("Pandemonium_WING");
            if (allDatas.Length == 0)
            {
                var loadedCharList = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CharacterData>());
                if (loadedCharList != null)
                {
                    allDatas = new CharacterData[loadedCharList.Length];
                    for (int j = 0; j < loadedCharList.Length; j++)
                    {
                        allDatas[j] = loadedCharList[j]!.Cast<CharacterData>();
                    }
                }
            }

            for (int j = 0; j < allDatas.Length; j++)
            {
                if (possibleDemonIDs.Contains(allDatas[j].characterId) && !Gameplay.Instance.GetScriptCharactersOfType(ECharacterType.Demon).Contains(allDatas[j]))
                {
                    possibleDemons.Add(allDatas[j]);
                }
            }
            CharacterData fakeDemon = possibleDemons[UnityEngine.Random.RandomRangeInt(0, possibleDemons.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, fakeDemon);
            possibleDemons.Remove(fakeDemon);
            CharacterData realDemon = possibleDemons[UnityEngine.Random.RandomRangeInt(0, possibleDemons.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Demon, realDemon);
            charRef.Init(realDemon);
        }
    }
    public w_Pandemonium() : base(ClassInjector.DerivedConstructorPointer<w_Pandemonium>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Pandemonium(System.IntPtr ptr) : base(ptr)
    {

    }
}


