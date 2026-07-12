using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace WingidonExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_TwinDemonTwin : Demon
{
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Start)
        {
            //Health health = PlayerController.PlayerInfo.health;
            //health.Damage(2);
            //health.AddMaxHp(-2);
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            Il2CppSystem.Collections.Generic.List<string> blacklistCharacterIDs = new Il2CppSystem.Collections.Generic.List<string>(); // Prevent Vidiyon from misleading a Saint or an Alchemist
            blacklistCharacterIDs.Add("Saint_61372493");
            blacklistCharacterIDs.Add("Alchemist_94446803");
            bool someoneCorrupted = false;
            Il2CppSystem.Collections.Generic.List<Character> goodVillagers = new Il2CppSystem.Collections.Generic.List<Character>();
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (character.alignment == EAlignment.Good && character.dataRef.type == ECharacterType.Villager && !blacklistCharacterIDs.Contains(character.dataRef.characterId))
                {
                    goodVillagers.Add(character);
                    if (character.statuses.Contains(ECharacterStatus.Corrupted) && !sharedScripts.CheckIfAlwaysGood(character))
                    {
                        character.statuses.AddStatus(w_Mezepheles.w_MezephelesMadness.w_mezephelesMadness, charRef);
                        character.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                        character.ChangeAlignment(EAlignment.Evil);
                        someoneCorrupted = true;
                    }
                }
            }
            if (!someoneCorrupted)
            {
                Character backupTarget = goodVillagers[UnityEngine.Random.RandomRangeInt(0, goodVillagers.Count)];
                backupTarget.statuses.AddStatus(ECharacterStatus.Corrupted, charRef);
                backupTarget.statuses.AddStatus(ECharacterStatus.MessedUpByEvil, charRef);
                backupTarget.statuses.AddStatus(w_Mezepheles.w_MezephelesMadness.w_mezephelesMadness, charRef);
                backupTarget.ChangeAlignment(EAlignment.Evil);
            }
        }
    }
    public w_TwinDemonTwin() : base(ClassInjector.DerivedConstructorPointer<w_TwinDemonTwin>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_TwinDemonTwin(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }

    /*
Names for Evil Outcasts:
Bombardier - Mortar
Doppelganger - Mimic
Drunk - Brawler
Plague Doctor - Toxomancer
Wretch - Wretch (does not become Evil)

Chatterbox - Backtalker
Lunatic - Maniac
Marionette - Marionette (does not become Evil)
Mutant - Parasite
Pilgrim - Fanatic
Revolutionary - Rebel
Renegade - Renegade (already Evil)

Bartender - Moonshiner
Tavernkeeper - Bootlegger

Rook - Citadel

Saboteur - Bandit

Magician - Thaumaturge
Pixie - Unseelie

Amnesiac - Derelict
Giant - Ogre
Good Twin - Evil Twin (becomes the Evil Twin as well? Not sure how to handle this one)
Mayor - Mayor (Bribed)
Moonchild - Cambion
Rook - Citadel
Tinker - Mad Scientist
Evil Twin - Evil Twin (already Evil)
Executioner - Executioner (already Evil)

Atheist - Hidebound
    */
}


