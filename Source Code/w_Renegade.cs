using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using MelonLoader;
using System;
using Il2Cpp;

namespace ExpansionPack;

[RegisterTypeInIl2Cpp]
public class w_Renegade : Role
{
    public override string Description
    {
        get
        {
            return "I'm an Evil Outcast";
        }
    }
    public override CharacterData GetBluffIfAble(Character charRef)
    {
        int diceRoll = Calculator.RollDice(10);

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
    public w_Renegade() : base(ClassInjector.DerivedConstructorPointer<w_Renegade>())
    {
        ClassInjector.DerivedConstructorBody((Il2CppObjectBase)this);
    }
    public w_Renegade(IntPtr ptr) : base(ptr)
    {
    }
}


