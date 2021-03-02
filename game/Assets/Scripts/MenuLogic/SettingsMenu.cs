using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    #region GameSettings
    public void GameSettings_ShowBlood(Text text)
    {
        if (text.text == "ON") text.text = "OFF";
        else if (text.text == "OFF") text.text = "ON";
        GameSettings.ShowBlood = text.text;
    }
    public void GameSettings_PhysicsQualityNextButton(Text text)
    {
        if (text.text == "LOW") text.text = "MEDIUM";
        else if (text.text == "MEDIUM") text.text = "HIGH";
        else if (text.text == "HIGH") text.text = "LOW";
        GameSettings.PhysicsQuality = text.text;
    }
    public void GameSettings_PhysicsQualityPreviousButton(Text text)
    {
        if (text.text == "LOW") text.text = "HIGH";
        else if (text.text == "MEDIUM") text.text = "LOW";
        else if (text.text == "HIGH") text.text = "MEDIUM";
        GameSettings.PhysicsQuality = text.text;
    }
    public void GameSettings_Crosshair(Text text)
    {
        if (text.text == "ON") text.text = "OFF";
        else if (text.text == "OFF") text.text = "ON";
        GameSettings.CrossHair = text.text;
    }
    public void GameSettings_Difficulty(int num)
    {
        GameSettings.Difficulty = num;
    }
    #endregion
    #region GraphicSettings
    public void GraphicSettings_GameQualityPreviousButton(Text text)
    {
        if (text.text == "LOW") text.text = "ULTRA";
        else if (text.text == "MEDIUM") text.text = "LOW";
        else if (text.text == "HIGH") text.text = "MEDIUM";
        else if (text.text == "ULTRA") text.text = "HIGH";
        GraphicSettings.Quality = text.text;
    }
    public void GraphicSettings_GameQualityNextButton(Text text)
    {
        if (text.text == "LOW") text.text = "MEDIUM";
        else if (text.text == "MEDIUM") text.text = "HIGH";
        else if (text.text == "HIGH") text.text = "ULTRA";
        else if (text.text == "ULTRA") text.text = "LOW";
        GraphicSettings.Quality = text.text;
    }
    public void GraphicSettings_AntiAlising(Text text)
    {
        if (text.text == "ON") text.text = "OFF";
        else if (text.text == "OFF") text.text = "ON";
        GraphicSettings.AntiAlising = text.text;
    }
    public void GraphicSettings_VSync(Text text)
    {
        if (text.text == "ON") text.text = "OFF";
        else if (text.text == "OFF") text.text = "ON";
        GraphicSettings.VSync = text.text;
    }
    #endregion
}
public static class GameSettings
{
    public static string Language;
    public static string ShowBlood;
    public static string PhysicsQuality;
    public static string CrossHair;
    public static int Difficulty;
}
public static class GraphicSettings
{
    public static string Quality;
    public static string AntiAlising;
    public static string VSync;
}
