using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace WingidonExpansionPack;


// Couldn't get this part working, so I'm reworking her.

[RegisterTypeInIl2Cpp]
public class w_Devout : Role
{
    bool haveActed = false;
    public override ActedInfo GetInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool targetIsOutcast = false;
            if (character.dataRef.type == ECharacterType.Outcast)
            {
                targetIsOutcast = true;
            }
            if (!targetIsOutcast)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        string line = "info";
        if (newList2.Count == 0)
        {
            line = "There are no Outcasts";
        }
        else
        {
            Character random = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            selection.Add(random);
            line = string.Format("#{0} is an Outcast", random.id);
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override ActedInfo GetBluffInfo(Character charRef)
    {
        Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
        System.Collections.Generic.List<Character> newList = new System.Collections.Generic.List<Character>();
        System.Collections.Generic.List<Character> newList2 = new System.Collections.Generic.List<Character>();
        Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
        Characters charInst = Characters.Instance;
        foreach (Character character in characters)
        {
            bool targetIsOutcast = false;
            if (character.GetCharacterType() == ECharacterType.Outcast)
            {
                targetIsOutcast = true;
            }
            if (!targetIsOutcast)
            {
                newList.Add(character);
            }
            else
            {
                newList2.Add(character);
            }
        }
        string line = "info";
        if (newList.Count == 0)
        {
            line = "There are no Outcasts";
        }
        else
        {
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random);
            line = string.Format("#{0} is an Outcast", random.id);
        }

        ActedInfo actedInfo = new ActedInfo(line, selection);
        return actedInfo;
    }
    public override string Description
    {
        get
        {
            return "Start revealed, then learn some stuff.";
        }
    }
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Init)
        {
            haveActed = false;
        }
        if (trigger == ETriggerPhase.None)
        {
            if (haveActed) return;
            haveActed = true;
            ///WaitTask();
            charRef.onReveal.Invoke();
            charRef.Act(ETriggerPhase.Day);
            charRef.ChangeState(ECharacterState.Revealed);
            if (charRef.bluff)
            {
                charRef.Reveal();
            }
            else
            {
                charRef.Reveal();
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetInfo(charRef));
        }
    }
    private async void WaitTask()
    {
        await Task.Delay(1200);
        return;
    }
    public override void BluffAct(ETriggerPhase trigger, Character charRef)
    {
        if (trigger == ETriggerPhase.Init)
        {
            // new wx_SavedScripts().DebugMessage($"Initialised {charRef.dataRef.characterName} at #{charRef.id}");
        }
        if (trigger == ETriggerPhase.Init)
        {
            haveActed = false;
        }
        if (trigger == ETriggerPhase.None)
        {
            if (haveActed) return;
            haveActed = true;
            //WaitTask();
            charRef.onReveal.Invoke();
            charRef.Act(ETriggerPhase.Day);
            charRef.ChangeState(ECharacterState.Revealed);
            if (charRef.bluff)
            {
                charRef.Reveal();
            }
            else
            {
                charRef.Reveal();
            }
        }
        if (trigger == ETriggerPhase.Day)
        {
            OnActed(ETriggerPhase.Day, charRef, GetBluffInfo(charRef));
        }
    }
    public w_Devout() : base(ClassInjector.DerivedConstructorPointer<w_Devout>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Devout(IntPtr ptr) : base(ptr)
    {
    }



    /*
    [HarmonyPatch(typeof(Gameplay), "OnCharacterReveal")]
    public static class CharacterRevealPatch
    {
        public static ECharacterStatus DevoutReveal = (ECharacterStatus)452215212;
        [HarmonyPrefix]
        public static bool CharacterRevealPrefix(Character obj)
        {
            foreach (Character character in Gameplay.CurrentCharacters)
            {
                if (!character.statuses.Contains(DevoutReveal))
                {
                    MelonLogger.Msg($"Doing First Reveal action of #{character.id}");
                    character.statuses.AddStatus(DevoutReveal, character);
                    if (character.bluff)
                    {
                        if (character.bluff.characterId == "Devout_WING")
                        {
                            if (CharacterHelper.CheckLying(character))
                            {
                                character.bluff.role.BluffAct(ETriggerPhase.None, character);
                                MelonLogger.Msg("Activating Devout ability");
                            }
                            else
                            {
                                character.bluff.role.Act(ETriggerPhase.None, character);
                                MelonLogger.Msg("Activating Devout ability");
                            }
                        }
                    }
                    else
                    {
                        if (character.dataRef.characterId == "Devout_WING")
                        {
                            if (CharacterHelper.CheckLying(character))
                            {
                                character.dataRef.role.BluffAct(ETriggerPhase.None, character);
                                MelonLogger.Msg("Activating Devout ability");
                            }
                            else
                            {
                                character.dataRef.role.Act(ETriggerPhase.None, character);
                                MelonLogger.Msg("Activating Devout ability");
                            }
                        }
                    }

                }
            }
            return true;
        }
    }
    */
}


