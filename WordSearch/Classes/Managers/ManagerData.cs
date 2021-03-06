﻿/*==========================================================================================*
*  Manage DataWords.xml word lists.                                                         *
*===========================================================================================*
*  1. ROUTINE: Handle return, process, word list from DataWords.xml                         *
*   a. Return STRING of specified list of Words from DataWords.xml                          *
*   b. Return string with all characters except a-z, removed                                * 
*   c. Return array of seperated STRING Words from given STRING                             *
*   d. Return STRING array of randomly chosen Words in list according to user input size    *
*  2. Return list of all word lists from DataWords.xml                                      *
*===========================================================================================*/
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Collections.Generic;

namespace WordSearch.Common
{
    public static class ManagerData
    {
        /*==========================================*
        *  1. Handle get, modify, return word list  *
        *===========================================*/
        public static string[] HandleListLoad(string nameList, int sizeList)
        {
            string wordsString = LoadListWordsString(nameList);
            string wordsStringSanitised = Helper.RemoveNonLetters(wordsString);
            string[] wordsSeperated = SeperateWords(wordsStringSanitised);
            string[] wordsSelected = SetSizeList(wordsSeperated, sizeList);
            string[] words = Helper.LowerCase(wordsSelected);
            return words;
        }

        private static string LoadListWordsString(string nameListWords)
        {
            XElement dataWords = XElement.Parse(Properties.Resources.dataWords);

            string list = (string)
                (from el in dataWords.Elements(nameListWords)
                 select el).First();

            return list;
        }

        private static string SanitiseWords(string listWords)
        {
            string wordsSanitised = Regex.Replace(listWords, "[^A-Za-z ]", "");

            return wordsSanitised;
        }
        private static string[] SeperateWords(string listWords)
        {
            char[] delimiters = new char[] { ',', ' ', ';' };

            string[] words = listWords.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }
        private static string[] SetSizeList(string[] arrayWords, int sizeList)
        {
            bool wordAdded = false;
            List<string> listWords = new List<string>();

            for (int counter = 0; counter < sizeList; counter++)
            {
                wordAdded = false;
                while (!wordAdded)
                {
                    // Select word from random integer
                    string wordSelected = arrayWords[Helper.Random(0, arrayWords.Length - 1)];
                    // if word not already added to listWords, add
                    if (!listWords.Contains(wordSelected))
                    {
                        listWords.Add(wordSelected);
                        wordAdded = true;
                    }
                }
            }
            return listWords.ToArray();
        }

        /*==========================*
        *  2. Return all word lists *
        *===========================*/
        public static List<string> AllLists()
        {
            XElement dataWords = XElement.Parse(Properties.Resources.dataWords);
            var xElements = dataWords.Elements();
            List<string> listOfLists = new List<string>();

            foreach (var item in xElements)
            {
                listOfLists.Add(item.Name.ToString());
            }

            return listOfLists;
        }
    }
}