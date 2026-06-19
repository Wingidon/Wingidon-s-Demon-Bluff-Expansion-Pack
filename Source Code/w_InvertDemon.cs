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

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_InvertDemon : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.GetRegisterAs().type == ECharacterType.Villager && character.alignment == EAlignment.Good) // Checks if they're a Good Villager
                {
                    character.statuses.AddStatus(ECharacterStatus.Corrupted, charRef); // Corrupts them.
                    character.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef); // Lets Witness see them.
                    character.statuses.statuses.Remove(ECharacterStatus.HealthyBluff); // To avoid shenanigans
                    Debug.Log(string.Format("Corrupted #{0}.", character.id));
                }
                if (character.alignment == EAlignment.Evil && character.dataRef.characterId != "Undying_WING") // Specific exception for the Undying, since they're a non-Disguising Minion and I think HealthyBluff is somehow for some reason bugging them out.
                {
                    character.statuses.AddStatus(ECharacterStatus.HealthyBluff, charRef); // Makes them tell the truth.
                    Debug.Log(string.Format("Educated #{0}.", character.id)); // I couldn't think of a better way to word this.
                }
            }
        }
    }
    public w_InvertDemon() : base(ClassInjector.DerivedConstructorPointer<w_InvertDemon>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_InvertDemon(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }
}


