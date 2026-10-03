using System;
using System.IO;
using System.Security;
using Microsoft.Win32;

namespace MT2Config
{
    /// <summary>
    /// Choices about the tool itself (language, dark mode), not game settings. Stored per Windows user under
    /// HKCU\Software\MT2Config, which needs no administrator rights.
    /// </summary>
    internal static class UserSettings
    {
        const string KeyPath = @"Software\MT2Config";

        public static object Get(string name)
        {
            return Read(KeyPath, name);
        }

        public static void Set(string name, object value)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(KeyPath))
                    key.SetValue(name, value);
            }
            catch (Exception e) when (e is SecurityException || e is UnauthorizedAccessException || e is IOException)
            {
            }
        }

        public static object Read(string keyPath, string name)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
                    return key?.GetValue(name);
            }
            catch (Exception e) when (e is SecurityException || e is UnauthorizedAccessException || e is IOException)
            {
                return null;
            }
        }
    }
}
