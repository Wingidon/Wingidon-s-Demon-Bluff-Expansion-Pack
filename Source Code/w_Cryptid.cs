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
public class w_Cryptid : Role
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
            Il2CppSystem.Collections.Generic.List<CharacterData> whitelistMinions = new Il2CppSystem.Collections.Generic.List<CharacterData>();
            Il2CppSystem.Collections.Generic.List<string> whitelistMinionIDs = sharedScripts.GetPossibleCharacterIDsOfRole("Cryptid_WING");



            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (whitelistMinionIDs.Contains(character.dataRef.characterId))
                {
                    while (whitelistMinionIDs.Contains(character.dataRef.characterId))
                    {
                        whitelistMinionIDs.Remove(character.dataRef.characterId);
                    }
                }
            }
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
                if (whitelistMinionIDs.Contains(allDatas[j].characterId))
                {
                    whitelistMinions.Add(allDatas[j]);
                }
            }
            whitelistMinions = Characters.Instance.FilterNotInDeckCharactersUnique(whitelistMinions);
            CharacterData realMinion = whitelistMinions[UnityEngine.Random.RandomRangeInt(0, whitelistMinions.Count)];
            Gameplay.Instance.AddScriptCharacterIfAble(ECharacterType.Minion, realMinion);
            DeckView.AddToObscuredDeckView(realMinion);
            charRef.Init(realMinion);
        }
    }
    public w_Cryptid() : base(ClassInjector.DerivedConstructorPointer<w_Cryptid>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Cryptid(System.IntPtr ptr) : base(ptr)
    {

    }
}


