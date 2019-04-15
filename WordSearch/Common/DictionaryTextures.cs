/*==========================================================================================*
* CHARACTER-INTEGER dictionary for sprites' index positions in sprite atlas textures        *
*===========================================================================================*
* 1. Letters, lines dictionary contains indecies for letters to place on word search grid   *
* 2. Return value from key or key from value for letters, lists dictionaries                *
* ==========================================================================================*/
using System.Collections.Generic;
using System.Linq;

namespace WordSearch
{
    public static class DictionaryTextures
    {
        /*======================*
        * 1. Letters Dictionary *
        *=======================*/
        public static readonly Dictionary<int, char> Letters = new Dictionary<int, char>()
        {
            {0,'a'},
            {1,'b'},
            {2,'c'},
            {3,'d'},
            {4,'e'},
            {5,'f'},
            {6,'g'},
            {7,'h'},
            {8,'i'},
            {9,'j'},
            {10,'k'},
            {11,'l'},
            {12,'m'},
            {13,'n'},
            {14,'o'},
            {15,'p'},
            {16,'q'},
            {17,'r'},
            {18,'s'},
            {19,'t'},
            {20,'u'},
            {21,'v'},
            {22,'w'},
            {23,'x'},
            {24,'y'},
            {25,'z'},
        };
        public static readonly Dictionary<int, char> Lines = new Dictionary<int, char>()
        {
            {0,'-'},
            {1,'|'},
            {2,'\\'},
            {3,'/'},
        };

        /*==========================*
        * 2. Return keys, values    *
        *===========================*/
        public static char ValueLetters(int key)
        {
            Letters.TryGetValue(key, out char value);

            return value;
        }
        public static int KeyLetters(char value)
        {
            int key = 0;
            var keys = Letters.Where(p => p.Value == value).Select(p => p.Key);
               
            foreach (int keyEntry in keys)
            {
                key = keyEntry;
            }

            return key;
        }
        public static char ValueLines(int key)
        {
            Lines.TryGetValue(key, out char value);

            return value;
        }
        public static int KeyLines(char value)
        {

            int key = 0;
            var keys = Lines.Where(p => p.Value == value).Select(p => p.Key);

            foreach (int keyEntry in keys)
            {
                key = keyEntry;
            }

            return key;
        }
    }
}