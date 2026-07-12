using Il2Cpp;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using static Il2CppSystem.Globalization.CultureInfo;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_SwitchDemon : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public Il2CppSystem.Collections.Generic.List<Il2Cpp.CharacterData> scriptCharacters = Gameplay.Instance.GetScriptCharacters();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
    }
    public w_SwitchDemon() : base(ClassInjector.DerivedConstructorPointer<w_SwitchDemon>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_SwitchDemon(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
        bluff = Characters.Instance.GetRandomUniqueBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);
        if (CheckAdjacentVillagers(charRef) == 2)
        {
            charRef.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef);
        }
        return bluff;
    }

    public int CheckAdjacentVillagers(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> adjacentCharacters = new Il2CppSystem.Collections.Generic.List<Character>();
        foreach (Character ch in Gameplay.CurrentCharacters)
            if (charRef == ch)
            {
                adjacentCharacters = Characters.Instance.GetAdjacentCharacters(ch);
                break;
            }

        int villagers = 0;

        foreach (Character ch in adjacentCharacters)
        {
            if (ch.GetCharacterType() == ECharacterType.Villager)
            {
                villagers++;
            }
        }
        return villagers;
    }
}


