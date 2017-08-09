/**
 *  @file   ZPlayerPrefs.cs
 *  @brief  ZPlayerPrefs.cs
 *  @create 3/17/2015 3:26:21 PM
 *  @author ZETO
 *  @Copyright (c) 2015 Studio ZERO. All rights reserved.
 */

/*==============================================================================
                        EDIT HISTORY FOR MODULE
when        who     what, where, why
DD/MM/YYYY
----------  ---     ------------------------------------------------------------
            ZETO    Initial Create

==============================================================================*/

using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;



public static class ZPlayerPrefs
{
    // Set false if you don't want to use encrypt/decrypt value
    // You could use #if UNITY_EDITOR for check your value
    public static bool useSecure = true;

    const int Iterations = 555;

    // You should Change following password and IV value using Initialize
    static string strPassword = "IamZETOwow!123";
    static string strSalt = "IvmD123A12";
    static bool hasSetPassword = false;


    //level min/max variables for possible settings
    public static int[] minZombieHP = new int[GameMaster._instance_.maxLevel];
    public static int[] maxZombieHP = new int[GameMaster._instance_.maxLevel];
    public static int[] minZombieSpeed = new int[GameMaster._instance_.maxLevel];
    public static int[] maxZombieSpeed = new int[GameMaster._instance_.maxLevel];
    public static int[] minTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
    public static int[] maxTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
    public static int[] minZombieDamage = new int[GameMaster._instance_.maxLevel];
    public static int[] maxZombieDamage = new int[GameMaster._instance_.maxLevel];
    public static int[] minZombieNum = new int[GameMaster._instance_.maxLevel];
    public static int[] maxZombieNum = new int[GameMaster._instance_.maxLevel];
    public static int[] minPlayerHP = new int[GameMaster._instance_.maxLevel];
    public static int[] maxPlayerHP = new int[GameMaster._instance_.maxLevel];
    public static int[] minPlayerDamage = new int[GameMaster._instance_.maxLevel];
    public static int[] maxPlayerDamage = new int[GameMaster._instance_.maxLevel];
    public static int[] minPlayerSpeed = new int[GameMaster._instance_.maxLevel];
    public static int[] maxPlayerSpeed = new int[GameMaster._instance_.maxLevel];


    //Custom settings storage
    public static List<int[]> custom1Settings = new List<int[]>();
    public static List<int[]> custom2Settings = new List<int[]>();
    public static List<int[]> custom3Settings = new List<int[]>();

    //global play time variable
    public static int playTime = 0;
    public static int thisRun = 0;

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0.0f);
    }

    public static float GetFloat(string key, float defaultValue, bool isDecrypt = true)
    {
        float retValue = defaultValue;

        string strValue = GetString(key);

        if (float.TryParse(strValue, out retValue))
        {
            return retValue;
        }
        else
        {
            return defaultValue;
        }
    }

    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static int GetInt(string key, int defaultValue, bool isDecrypt = true)
    {
        int retValue = defaultValue;

        string strValue = GetString(key);

        if (int.TryParse(strValue, out retValue))
        {
            return retValue;
        }
        else
        {
            return defaultValue;
        }
    }



    public static string GetString(string key)
    {
        string strEncryptValue = GetRowString(key);

        return Decrypt(strEncryptValue, strPassword);
    }

    public static string GetRowString(string key)
    {
        CheckPasswordSet();

        string strEncryptKey = Encrypt(key, strPassword);
        string strEncryptValue = PlayerPrefs.GetString(strEncryptKey);

        return strEncryptValue;
    }

    public static string GetString(string key, string defaultValue)
    {
        string strEncryptValue = GetRowString(key, defaultValue);
        return Decrypt(strEncryptValue, strPassword);
    }

    public static string GetRowString(string key, string defaultValue)
    {
        CheckPasswordSet();

        string strEncryptKey = Encrypt(key, strPassword);
        string strEncryptDefaultValue = Encrypt(defaultValue, strPassword);

        string strEncryptValue = PlayerPrefs.GetString(strEncryptKey, strEncryptDefaultValue);

        return strEncryptValue;
    }

    public static bool HasKey(string key)
    {
        CheckPasswordSet();
        return PlayerPrefs.HasKey(Encrypt(key, strPassword));
    }

    public static void Save()
    {
        CheckPasswordSet();
        PlayerPrefs.Save();
    }

    public static void SetFloat(string key, float value)
    {
        string strValue = System.Convert.ToString(value);
        SetString(key, strValue);
    }

    public static void SetInt(string key, int value)
    {
        string strValue = System.Convert.ToString(value);
        SetString(key, strValue);
    }

    public static void SetString(string key, string value)
    {
        CheckPasswordSet();
        PlayerPrefs.SetString(Encrypt(key, strPassword), Encrypt(value, strPassword));
    }

    /////////////////////////////////////////////////////////////////
    // Help Function
    /////////////////////////////////////////////////////////////////
    public static void Initialize(string newPassword, string newSalt)
    {
        strPassword = newPassword;
        strSalt = newSalt;

        hasSetPassword = true;

        if (HasKey("PlayerDoubloons"))
        {
            GameMaster._instance_.playerDoubloons = GetInt("PlayerDoubloons");
        }
        if (HasKey("LevelsUnlocked"))
        {
            for(int i = 1; i < GetInt("LevelsUnlocked"); i++)
            {
                GameMaster._instance_.levelsAvailable.Add(i+1);
            }
        }
        //level 1 keys

        //zombie custom 1 level 1 settings
        if (HasKey("lvl1Custom1ZHp"))
        {
            int[] values = new int[8];
            custom1Settings.Add(values);
            custom1Settings[0][0] = GetInt("lvl1Custom1ZHp");
        }
        if (HasKey("lvl1Custom1ZSpd"))
        {
            custom1Settings[0][1] = GetInt("lvl1Custom1ZSpd");
        }
        if (HasKey("lvl1Custom1ZTBW"))
        {
            custom1Settings[0][2] =GetInt("lvl1Custom1ZTBW");
        }
        if (HasKey("lvl1Custom1ZD"))
        {
            custom1Settings[0][3] = GetInt("lvl1Custom1ZD");
        }
        if (HasKey("lvl1Custom1ZNInW"))
        {
            custom1Settings[0][4] = GetInt("lvl1Custom1ZNInW");
        }
        //player custom 1 level 1 settings
        if (HasKey("lvl1Custom1PHp"))
        {
            custom1Settings[0][5] = GetInt("lvl1Custom1PHp");
        }
        if (HasKey("lvl1Custom1PSpd"))
        {
            custom1Settings[0][6] = GetInt("lvl1Custom1PSpd");
        }
        if (HasKey("lvl1Custom1PD"))
        {
            custom1Settings[0][7] = GetInt("lvl1Custom1PD");
        }

        //zombie custom 2 level 1 settings
        if (HasKey("lvl1Custom2ZHp"))
        {
            int[] values = new int[8];
            custom2Settings.Add(values);
            custom2Settings[0][0] = GetInt("lvl1Custom2ZHp");
        }
        if (HasKey("lvl1Custom2ZSpd"))
        {
            custom2Settings[0][1] = GetInt("lvl1Custom2ZSpd");
        }
        if (HasKey("lvl1Custom2ZTBW"))
        {
            custom2Settings[0][2] = GetInt("lvl1Custom2ZTBW");
        }
        if (HasKey("lvl1Custom2ZD"))
        {
            custom2Settings[0][3] = GetInt("lvl1Custom2ZD");
        }
        if (HasKey("lvl1Custom2ZNInW"))
        {
            custom2Settings[0][4] = GetInt("lvl1Custom2ZNInW");
        }
        //player custom 2 level 1 settings
        if (HasKey("lvl1Custom2PHp"))
        {
            custom2Settings[0][5] = GetInt("lvl1Custom2PHp");
        }
        if (HasKey("lvl1Custom2PSpd"))
        {
            custom2Settings[0][6] = GetInt("lvl1Custom2PSpd");
        }
        if (HasKey("lvl1Custom2PD"))
        {
            custom2Settings[0][7] = GetInt("lvl1Custom2PD");
        }

        //zombie custom 3 level 1 settings
        if (HasKey("lvl1Custom3ZHp"))
        {
            int[] values = new int[8];
            custom3Settings.Add(values);
            custom3Settings[0][0] = GetInt("lvl1Custom3ZHp");
        }
        if (HasKey("lvl1Custom3ZSpd"))
        {
            custom3Settings[0][1] = GetInt("lvl1Custom3ZSpd");
        }
        if (HasKey("lvl1Custom3ZTBW"))
        {
            custom3Settings[0][2] = GetInt("lvl1Custom3ZTBW");
        }
        if (HasKey("lvl1Custom3ZD"))
        {
            custom3Settings[0][3] = GetInt("lvl1Custom3ZD");
        }
        if (HasKey("lvl1Custom3ZNInW"))
        {
            custom3Settings[0][4] = GetInt("lvl1Custom3ZNInW");
        }
        //player custom 3 level 1 settings
        if (HasKey("lvl1Custom3PHp"))
        {
            custom3Settings[0][5] = GetInt("lvl1Custom3PHp");
        }
        if (HasKey("lvl1Custom3PSpd"))
        {
            custom3Settings[0][6] = GetInt("lvl1Custom3PSpd");
        }
        if (HasKey("lvl1Custom3PD"))
        {
            custom3Settings[0][7] = GetInt("lvl1Custom3PD");
        }

        for(int i = 0; i < GameMaster._instance_.maxLevel; i++)//set modifications for custom settings available, based on items bought
        {
            if(HasKey("level" + (i+1) + "minZHP"))
            {
                if (minZombieHP.Length==0)
                {
                    minZombieHP = new int[GameMaster._instance_.maxLevel];
                }
                minZombieHP[i] = GetInt("level" + (i+1) + "minZHP");
            }
            if (HasKey("level" + (i + 1) + "maxZHP"))
            {
                if (maxZombieHP.Length==0)
                {
                    maxZombieHP = new int[GameMaster._instance_.maxLevel];
                }
                maxZombieHP[i] = GetInt("level" + (i+1) + "maxZHP");
            }
            if (HasKey("level" + (i + 1) + "minZSpd"))
            {
                if (minZombieSpeed.Length==0)
                {
                    minZombieSpeed = new int[GameMaster._instance_.maxLevel];
                }
                minZombieSpeed[i] = GetInt("level" + (i+1) + "minZSpd");
            }
            if (HasKey("level" + (i + 1) + "maxZSpd"))
            {
                if (maxZombieSpeed.Length==0)
                {
                    maxZombieSpeed = new int[GameMaster._instance_.maxLevel];
                }
                maxZombieSpeed[i] = GetInt("level" + (i+1) + "maxZSpd");
            }
            if (HasKey("level" + (i + 1) + "minTBWs"))
            {
                if (minTimeBetweenWaves.Length==0)
                {
                    minTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
                }
                minTimeBetweenWaves[i] = GetInt("level" + (i + 1) + "minTBWs");
            }
            if (HasKey("level" + (i + 1) + "maxTBWs"))
            {
                if (maxTimeBetweenWaves.Length==0)
                {
                    maxTimeBetweenWaves = new int[GameMaster._instance_.maxLevel];
                }
                maxTimeBetweenWaves[i] = GetInt("level" + (i + 1) + "maxTBWs");
            }
            if (HasKey("level" + (i + 1) + "minZD"))
            {
                if (minZombieDamage.Length==0)
                {
                    minZombieDamage = new int[GameMaster._instance_.maxLevel];
                }
                minZombieDamage[i] = GetInt("level" + (i + 1)+ "minZD");
            }
            if (HasKey("level" + (i + 1) + "maxZD"))
            {
                if (maxZombieNum.Length==0)
                {
                    maxZombieDamage = new int[GameMaster._instance_.maxLevel];
                }
                maxZombieDamage[i] = GetInt("level" + (i + 1) + "maxZD");
            }
            if (HasKey("level" + (i + 1) + "minZNum"))
            {
                if (minZombieNum.Length==0)
                {
                    minZombieNum = new int[GameMaster._instance_.maxLevel];
                }
                minZombieNum[i] = GetInt("level" + (i + 1) + "minZNum");
            }
            if (HasKey("level" + (i + 1) + "maxZNum"))
            {
                if (maxZombieNum.Length==0)
                {
                    maxZombieNum = new int[GameMaster._instance_.maxLevel];
                }
                maxZombieNum[i] = GetInt("level" + (i + 1) + "maxZNum");
            }
            if (HasKey("level" + (i + 1) + "minPHP"))
            {
                if (minPlayerHP.Length==0)
                {
                    minPlayerHP= new int[GameMaster._instance_.maxLevel];
                }
                minPlayerHP[i] = GetInt("level" + (i + 1) + "minPHP");
            }
            if (HasKey("level" + (i + 1) + "maxPHP"))
            {
                if (maxPlayerHP.Length==0)
                {
                    maxPlayerHP = new int[GameMaster._instance_.maxLevel];
                }
                maxPlayerHP[i] = GetInt("level" + (i + 1) + "maxPHP");
            }
            if (HasKey("level" + (i + 1) + "minPD"))
            {
                if (minPlayerDamage.Length == 0)
                {
                    minPlayerDamage = new int[GameMaster._instance_.maxLevel];
                }
                minPlayerDamage[i] = GetInt("level" + (i + 1) + "minPD");
            }
            if (HasKey("level" + (i + 1) + "maxPD"))
            {
                if (maxPlayerDamage.Length == 0)
                {
                    maxPlayerDamage = new int[GameMaster._instance_.maxLevel];
                }
                maxPlayerDamage[i] = GetInt("level" + (i + 1) + "maxPD");
            }
            if (HasKey("level" + (i + 1) + "minPSpd"))
            {
                if (minPlayerSpeed.Length == 0)
                {
                    minPlayerSpeed = new int[GameMaster._instance_.maxLevel];
                }
                minPlayerSpeed[i] = GetInt("level" + (i + 1) + "minPSpd");
            }
            if (HasKey("level" + (i + 1) + "maxPSpd"))
            {
                if (maxPlayerSpeed.Length == 0)
                {
                    maxPlayerSpeed = new int[GameMaster._instance_.maxLevel];
                }
                maxPlayerSpeed[i] = GetInt("level" + (i + 1) + "maxPSpd");
            }
        }

    }


    static void CheckPasswordSet()
    {
        if (!hasSetPassword)
        {
            Debug.LogWarning("Set Your Own Password & Salt!!!");
        }
    }

    static byte[] GetIV()
    {
        byte[] IV = Encoding.UTF8.GetBytes(strSalt);
        return IV;
    }

    static string Encrypt(string strPlain, string password)
    {
        if (!useSecure)
            return strPlain;

        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, GetIV(), Iterations);

            byte[] key = rfc2898DeriveBytes.GetBytes(8);

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, GetIV()), CryptoStreamMode.Write))
            {
                memoryStream.Write(GetIV(), 0, GetIV().Length);

                byte[] plainTextBytes = Encoding.UTF8.GetBytes(strPlain);

                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Encrypt Exception: " + e);
            return strPlain;
        }
    }

    static string Decrypt(string strEncript, string password)
    {
        if (!useSecure)
            return strEncript;

        try
        {
            byte[] cipherBytes = Convert.FromBase64String(strEncript);

            using (var memoryStream = new MemoryStream(cipherBytes))
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] iv = GetIV();
                memoryStream.Read(iv, 0, iv.Length);

                // use derive bytes to generate key from password and IV
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, iv, Iterations);

                byte[] key = rfc2898DeriveBytes.GetBytes(8);

                using (var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    string strPlain = streamReader.ReadToEnd();
                    return strPlain;
                    //return strSalt;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Decrypt Exception: " + e);
            return strEncript;
        }

    }

}