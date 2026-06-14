using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace ExpansionPack
{
    // Token: 0x02000007 RID: 7
    [RegisterTypeInIl2Cpp]
    public class w_Clairvoyant : Role
    {
        // Token: 0x06000014 RID: 20 RVA: 0x00003208 File Offset: 0x00001408
        public override ActedInfo GetInfo(Character charRef)
        {
            Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList2 = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList3 = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
            Characters charInst = Characters.Instance;
            foreach (Character character in characters)
            {
                bool targetIsEvil = false;
                bool flag = character.statuses != null;
                if (flag)
                {
                    targetIsEvil = (character.GetRegisterAlignment() == EAlignment.Evil);
                }
                newList3.Add(character);
                bool flag2 = !targetIsEvil;
                if (flag2)
                {
                    newList.Add(character);
                }
                newList2.Add(character);
            }
            newList2 = Characters.Instance.FilterAlignmentCharacters(newList2, EAlignment.Evil);
            Character random = newList3[UnityEngine.Random.RandomRangeInt(0, newList3.Count)];
            bool flag3 = newList2.Count < 2;
            if (flag3)
            {
                random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            }
            selection.Add(random);
            Character random2 = newList3[UnityEngine.Random.RandomRangeInt(0, newList3.Count)];
            bool flag4 = random.GetRegisterAlignment() == EAlignment.Evil;
            if (flag4)
            {
                newList2.Remove(random);
                random2 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
                selection.Add(random2);
            }
            else
            {
                newList.Remove(random);
                random2 = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
                selection.Add(random2);
            }
            bool flag5 = random.id < random2.id;
            string line;
            if (flag5)
            {
                line = string.Format("#{0} and #{1} are on the same team", random.id, random2.id);
            }
            else
            {
                line = string.Format("#{0} and #{1} are on the same team", random2.id, random.id);
            }
            return new ActedInfo(line, selection);
        }

        // Token: 0x06000015 RID: 21 RVA: 0x000033E4 File Offset: 0x000015E4
        public override ActedInfo GetBluffInfo(Character charRef)
        {
            Il2CppSystem.Collections.Generic.List<Character> characters = Gameplay.CurrentCharacters;
            Il2CppSystem.Collections.Generic.List<Character> newList = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> newList2 = new Il2CppSystem.Collections.Generic.List<Character>();
            Il2CppSystem.Collections.Generic.List<Character> selection = new Il2CppSystem.Collections.Generic.List<Character>();
            Characters charInst = Characters.Instance;
            foreach (Character character in characters)
            {
                bool targetIsEvil = false;
                bool flag = character.statuses != null;
                if (flag)
                {
                    targetIsEvil = (character.GetRegisterAlignment() == EAlignment.Evil);
                }
                bool flag2 = !targetIsEvil;
                if (flag2)
                {
                    newList.Add(character);
                }
                newList2.Add(character);
            }
            newList2 = Characters.Instance.FilterAlignmentCharacters(newList2, EAlignment.Evil);
            Character random = newList[UnityEngine.Random.RandomRangeInt(0, newList.Count)];
            selection.Add(random);
            Character random2 = newList2[UnityEngine.Random.RandomRangeInt(0, newList2.Count)];
            selection.Add(random2);
            bool flag3 = random.id < random2.id;
            string line;
            if (flag3)
            {
                line = string.Format("#{0} and #{1} are on the same team", random.id, random2.id);
            }
            else
            {
                line = string.Format("#{0} and #{1} are on the same team", random2.id, random.id);
            }
            return new ActedInfo(line, selection);
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000016 RID: 22 RVA: 0x0000352C File Offset: 0x0000172C
        public override string Description
        {
            get
            {
                return "2 characters are on the same team.";
            }
        }

        // Token: 0x06000017 RID: 23 RVA: 0x00003544 File Offset: 0x00001744
        public override void Act(ETriggerPhase trigger, Character charRef)
        {
            bool flag = trigger == ETriggerPhase.Day;
            if (flag)
            {
                base.onActed.Invoke(this.GetInfo(charRef));
            }
        }

        // Token: 0x06000018 RID: 24 RVA: 0x00003570 File Offset: 0x00001770
        public override void BluffAct(ETriggerPhase trigger, Character charRef)
        {
            bool flag = trigger == ETriggerPhase.Day;
            if (flag)
            {
                base.onActed.Invoke(this.GetBluffInfo(charRef));
            }
        }

        // Token: 0x06000019 RID: 25 RVA: 0x0000359C File Offset: 0x0000179C
        public w_Clairvoyant() : base(ClassInjector.DerivedConstructorPointer<w_Clairvoyant>())
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        // Token: 0x0600001A RID: 26 RVA: 0x000035B2 File Offset: 0x000017B2
        public w_Clairvoyant(IntPtr ptr) : base(ptr)
        {
        }
    }
}
