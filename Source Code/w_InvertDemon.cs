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
public class w_InvertDemon : Demon
{
    public CharacterData[] allDatas = Il2CppSystem.Array.Empty<CharacterData>();

    Il2CppSystem.Collections.Generic.List<string> GetJinxedIDs()
    {
        Il2CppSystem.Collections.Generic.List<string> jinxedIDs = new Il2CppSystem.Collections.Generic.List<string>();
        jinxedIDs.Add("Alchemist_94446803");
        jinxedIDs.Add("Confessor_18741708");
        jinxedIDs.Add("Knight_47970624");

        jinxedIDs.Add("Chatterbox_WING");
        jinxedIDs.Add("Lycanthrope_16077432");
        jinxedIDs.Add("MadScientist_scm");

        jinxedIDs.Add("Guardian_scm");
        jinxedIDs.Add("Mezepheles_09511163");
        jinxedIDs.Add("Poisoner_64796285");
        jinxedIDs.Add("Saboteur_WING");
        jinxedIDs.Add("Snake Charmer_WING");
        jinxedIDs.Add("Turncoat_WING");

        jinxedIDs.Add("Foggy_POW");
        jinxedIDs.Add("Snowy_POW");
        jinxedIDs.Add("SnowedIn_POW");
        jinxedIDs.Add("Stormy_POW");
        jinxedIDs.Add("Sunny_POW");

        return jinxedIDs;
    }
    wx_SavedScripts sharedScripts = new wx_SavedScripts();
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            sharedScripts.DoJinxes(charRef, "Mendaverte_WING", false);
            sharedScripts.DoJinxes(charRef, "Weather", false);
        }
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            sharedScripts.DoJinxes(charRef, "Mendaverte_WING", true);
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.dataRef.type == ECharacterType.Villager && character.alignment == EAlignment.Good) // Checks if they're a Good Villager
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


