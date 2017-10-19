using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogDnaParse.Dtos
{
    /// <summary>
    /// Methods to parse the text to JSON
    /// </summary>
    public class ParseTextToJson : IParseTextToJson
    {
        private LogDnaDto dna;
        private MainOperatorsDto g6;

        public ParseTextToJson()
        {
            dna = new LogDnaDto();
            g6 = new MainOperatorsDto();
            var listy = new List<ComparisonOperatorsDto>();
        }

        /// <summary>
        /// Trims the brackets from the text
        /// </summary>
        /// <param name="text">input text with brackets</param>
        /// <returns>text without the brackets</returns>
        private string TrimBrackets(string text)
        {
            text = text.TrimEnd(')');
            text = text.TrimStart('(');

            return text;// if you want to convert to int but then if not set will always show in the JSON as 0 - Convert.ToInt32(text);
        }

        /// <summary>
        /// return the build object as mapped to dto
        /// </summary>
        /// <param name="position">position to start from</param>
        /// <param name="comparison">comparison operator</param>
        /// <param name="section">section in the split based on ;</param>
        /// <param name="checkDig">check to see if the request is truthy falsy</param>
        /// <returns> truthy falsy object dto</returns>
        private TruthyFalsyOperatorsDto ReturnBuiltTorFObject(int position, string comparison, string section, bool checkDig)
        {
            var truthyFalsyObj = new TruthyFalsyOperatorsDto();
            if (checkDig)
            {
                truthyFalsyObj.Comparison = comparison;
                string booli = section.Substring(position, section.Length - 2).ToUpper();
                truthyFalsyObj.TruthyFalsy = booli.Equals("TRUE") ? true : false;
            }

            return truthyFalsyObj;
        }

        /// <summary>
        /// return the build object as mapped to dto
        /// </summary>
        /// <param name="position">position to start from</param>
        /// <param name="comparison">comparison operator</param>
        /// <param name="section">section in the split based on ;</param>
        /// <param name="property">which property does it represent in the comparisonOperators obj</param>
        /// <returns></returns>
        private ComparisonOperatorsDto ReturnBuiltObject(int position, string comparison, string section, string property)
        {
            var compObj = new ComparisonOperatorsDto();
            switch (property.ToUpper())
            {
                case "DESC":
                    compObj.Desc = section.Substring(position, section.Length);
                    break;
                case "VALUE":
                    compObj.Comparison = comparison;
                    compObj.Value = section.Substring(position, section.Length - 1);
                    break;
                case "LENGTH":
                    compObj.Comparison = comparison;
                    compObj.Length = TrimBrackets(section.Substring(4));
                    break;
                case "QUOTED":
                    compObj.Comparison = comparison;
                    compObj.Quoted = section.Substring(position, section.Length - 2).ToUpper();
                    break;
            }
            return compObj;
        }

        /// <summary>
        /// Takes in an input string and checks for operators to mapp into json objects
        /// for example convert less than sign to $lt and > to $gt etc</to>
        /// </summary>
        /// <param name="input">input from rest URI params</param>
        /// <returns>DNA log object</returns>
        public LogDnaDto ParseText(string input)
        {
            var listy = new List<object>();
            int currentPosition = 0;

            // input convert all to lower for ease of use
            input = input.ToLower();

            // if and exists in string
            if (input.IndexOf("and") != -1)
            {
                var arr = input.Split(';');
                foreach (var section in arr)
                {
                    // if the string has >
                    if (section.IndexOf(">") != -1)
                    {
                        // if the string has len function else just regularly convert to gt
                        if (section.IndexOf("len") != -1)
                        {
                            currentPosition = section.IndexOf("len") + 3;
                            listy.Add(ReturnBuiltObject(currentPosition, "$gt", section, "length"));
                            currentPosition = 0;
                        }
                        else
                        {
                            currentPosition = section.IndexOf(">") + 1;
                            listy.Add(ReturnBuiltObject(currentPosition, "$gt", section, "value"));
                            currentPosition = 0;
                        }
                    }
                    // if string has <
                    else if (section.IndexOf("<") != -1)
                    {
                        // if the string has len function else just regularly convert to lt
                        if (section.IndexOf("len") != -1)
                        {
                            currentPosition = section.IndexOf("len") + 3;
                            listy.Add(ReturnBuiltObject(currentPosition, "$lt", section, "length"));
                            currentPosition = 0;
                        }
                        else
                        {
                            currentPosition = section.IndexOf("<") + 1;
                            listy.Add(ReturnBuiltObject(currentPosition, "$lt", section, "value"));
                            currentPosition = 0;
                        }
                    }
                    // if the string has quotes then t reat the inside of the quotes as equals
                    else if (section.IndexOf('"') != -1)
                    {
                        currentPosition = section.IndexOf('"') + 1;
                        listy.Add(ReturnBuiltObject(currentPosition, "$eq", section, "quoted"));
                        currentPosition = 0;
                    }
                    // if the string length is greater than 3 then its just words and treat them as desc
                    else if (section.Length > 3)
                    {
                        currentPosition = 0;
                        listy.Add(ReturnBuiltObject(currentPosition, null, section, "desc"));
                    }
                    // add to the object in array form
                    g6.And = listy.ToArray();
                }
                // add to the dna Log data
                dna.Primary = g6;
                dna.OriginalContent = input;
            }
            // treat this as the boolean obj
            else if (input.IndexOf("!") != -1)
            {
                var arr = input.Split(';');
                foreach (var section in arr)
                {
                    if (section.IndexOf('!') != -1)
                    {
                        currentPosition = section.IndexOf('!') + 1;
                        listy.Add(ReturnBuiltTorFObject(currentPosition, "$not", section, true));
                        currentPosition = 0;
                    }
                    g6.Not = listy.ToArray();
                }
                dna.Primary = g6;
                dna.OriginalContent = input;
            }
            // else it is or object
            else
            {
                var arr = input.Split(';');
                foreach (var section in arr)
                {
                    if (section.IndexOf(">") != -1)
                    {
                        if (section.IndexOf("len") != -1)
                        {
                            currentPosition = section.IndexOf("len") + 3;
                            listy.Add(ReturnBuiltObject(currentPosition, "$gt", section, "length"));
                            currentPosition = 0;
                        }
                        else
                        {
                            currentPosition = section.IndexOf(">") + 1;
                            listy.Add(ReturnBuiltObject(currentPosition, "$gt", section, "value"));
                            currentPosition = 0;
                        }
                    }
                    else if (section.IndexOf("<") != -1)
                    {
                        if (section.IndexOf("len") != -1)
                        {
                            currentPosition = section.IndexOf("len") + 3;
                            listy.Add(ReturnBuiltObject(currentPosition, "$lt", section, "length"));
                            currentPosition = 0;
                        }
                        else
                        {
                            currentPosition = section.IndexOf("<") + 1;
                            listy.Add(ReturnBuiltObject(currentPosition, "$lt", section, "value"));
                            currentPosition = 0;
                        }
                    }
                    else if (section.IndexOf('"') != -1)
                    {
                        currentPosition = section.IndexOf('"') + 1;
                        listy.Add(ReturnBuiltObject(currentPosition, "$eq", section, "quoted"));
                        currentPosition = 0;
                    }
                    else if (section.Length > 2)
                    {
                        currentPosition = 0;
                        listy.Add(ReturnBuiltObject(currentPosition, null, section, "desc"));
                    }

                    g6.Or = listy.ToArray();
                }
                dna.Primary = g6;
                dna.OriginalContent = input;
            }

            // return the final dna log obj
            return dna;
        }
    }
}