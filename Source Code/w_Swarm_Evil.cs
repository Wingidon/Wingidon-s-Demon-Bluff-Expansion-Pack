using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Swarm_Evil : Minion
{
    public override void Act(ETriggerPhase trigger, Character charRef)
    {
    }
    public w_Swarm_Evil() : base(ClassInjector.DerivedConstructorPointer<w_Swarm_Evil>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Swarm_Evil(System.IntPtr ptr) : base(ptr)
    {

    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        int diceRoll = Calculator.RollDice(10);

        //List<CharacterData> notInPlayCh = Gameplay.Instance.GetScriptCharacters();
        //notInPlayCh = Characters.Instance.FilterAlignmentCharacters(notInPlayCh, EAlignment.Good);
        //notInPlayCh = Characters.Instance.FilterBluffableCharacters(notInPlayCh);
        //return notInPlayCh[UnityEngine.Random.Range(0, notInPlayCh.Count - 1)];

        if (diceRoll < 5)
        {
            // 100% Double Claim
            return Characters.Instance.GetRandomDuplicateBluff();
        }
        else
        {
            // Become a new character
            CharacterData bluff = Characters.Instance.GetRandomUniqueBluff();
            Gameplay.Instance.AddScriptCharacterIfAble(bluff.type, bluff);

            return bluff;
        }
    }
}


