using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameEngine.Core
{
    public static class CardLibrary
    {
        public static Dictionary<string, Card> Library;
        private static bool loaded = false;

        public static void InitLib()
        {
            if (!loaded)
            {
                Library = new Dictionary<string, Card>();

                IEnumerable<Type> types = Assembly.GetAssembly(typeof(Card)).GetTypes()
                .Where(type => !type.IsAbstract && type.IsClass && type.IsSubclassOf(typeof(Card)));

                foreach (Type command in types)
                {
                    Card instance = Activator.CreateInstance(command) as Card;
                    Library.Add(instance.CardID.Trim(), instance);
                }
                loaded = true;
            }
        }
    }
}
