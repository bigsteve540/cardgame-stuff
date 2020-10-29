using GameEngine.CardEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameEngine.Core
{
    public static class CardEffectHandler
    {
        private static Dictionary<int, CardEffect> library;
        private static bool loaded = false;

         
        public static void Init()
        {
            if (!loaded)
            {
                library = new Dictionary<int, CardEffect>();

                IEnumerable<Type> types = Assembly.GetAssembly(typeof(CardEffect)).GetTypes()
                .Where(type => !type.IsAbstract && type.IsClass && type.IsSubclassOf(typeof(CardEffect)));

                foreach (Type fx in types)
                {
                    CardEffect instance = Activator.CreateInstance(fx) as CardEffect;
                    library.Add(instance.EffectID, instance);
                }
                loaded = true;
            }
        }

        public static CardEffect GetEffect(int _id)
        {
            return Activator.CreateInstance(library[_id].GetType()) as CardEffect;
        }

        public static string GetEffectText(int _id)
        {
            return library[_id].Description;
        }
    }
}
