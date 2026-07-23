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
public class w_InvertDemonNew : Demon
{
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
            sharedScripts.DoJinxes(charRef, "Agmeres_WING", false);
            sharedScripts.DoJinxes(charRef, "Weather", false);
        }
        if (trigger == ETriggerPhase.AfterRoundStart)
        {
            wx_SavedScripts sharedScripts = new wx_SavedScripts();
            sharedScripts.DoJinxes(charRef, "Agmeres_WING", true);
            charRef.ChangeAlignment(EAlignment.Evil); // For some reason Mendaverte is randomly making herself Good. This should fix that I hope.
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                EAlignment targetAlignment = EAlignment.None;
                ECharacterType targetType = ECharacterType.None;
                if (character.dataRef.startingAlignment == EAlignment.Good) targetAlignment = EAlignment.Evil;
                if (character.dataRef.startingAlignment == EAlignment.Evil) targetAlignment = EAlignment.Good;
                if (character.dataRef.type == ECharacterType.Villager) targetType = ECharacterType.Outcast;
                if (character.dataRef.type == ECharacterType.Outcast) targetType = ECharacterType.Villager;
                if (character.dataRef.type == ECharacterType.Minion) targetType = ECharacterType.Demon;
                if (character.dataRef.type == ECharacterType.Demon) targetType = ECharacterType.Minion;
                CharacterData registration = character.GetRegisterAs();
                if (targetAlignment != EAlignment.None) registration.startingAlignment = targetAlignment;
                if (targetType != ECharacterType.None) registration.type = targetType;
                registration.characterName = character.dataRef.characterName;
                registration.name = character.dataRef.name;
                character.UpdateRegisterAsRole(registration);
                sharedScripts.DebugMessage($"#{character.id} now Registering as {character.GetRegisterAlignment()} {character.GetRegisterAs().type} {character.GetRegisterAs().characterName}");
            }
            charRef.ChangeAlignment(EAlignment.Evil); // For some reason Mendaverte is randomly making herself Good. This should fix that I hope.
        }
    }
    public w_InvertDemonNew() : base(ClassInjector.DerivedConstructorPointer<w_InvertDemonNew>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_InvertDemonNew(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        CharacterData bluff = Characters.Instance.GetRandomUniqueVillagerBluff();
        Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

        return bluff;
    }
}


