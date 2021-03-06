﻿namespace LogDnaParse.Dtos
{
    /// <summary>
    /// Interface of the ParseTextToJson class for ease of use
    /// </summary>
    public interface IParseTextToJson
    {
        /// <summary>
        /// Interface method to parse text to JSON
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        LogDnaDto ParseText(string input);
    }
}