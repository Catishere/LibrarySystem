using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.MVVM.Model;

namespace LibrarySystem.Utils
{
    public class SuggestionFileManager
    {
        private const string filename = "logindata.dat";
        public List<UserLoginSuggestion> GetSuggestions()
        {
            if (!File.Exists(filename))
                File.Create(filename).Close();

            var data = Cryptography.Decrypt(File.ReadAllText(filename), "yolo123");
            var lines = data.Split('\n');
            List<UserLoginSuggestion> suggestions = new List<UserLoginSuggestion>();
            foreach (var line in lines)
            {
                var index = line.IndexOf('+');
                if (index < 0) continue;
                var uls = new UserLoginSuggestion(line.Substring(0, index).Trim(), line.Substring(index + 1).Trim());
                suggestions.Add(uls);
            }

            return suggestions;
        }

        public void SetSuggestions(List<UserLoginSuggestion> suggestions)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var suggestion in suggestions)
            {
                sb.Append(suggestion.Username);
                sb.Append("+");
                sb.Append(suggestion.Password);
                sb.Append("\n");
            }
            File.WriteAllText(filename, Cryptography.Encrypt(sb.ToString(), "yolo123"));
        }

        public void AddSuggestion(UserLoginSuggestion suggestion)
        {
            var list = GetSuggestions() ?? new List<UserLoginSuggestion>();
            if (!list.Contains(suggestion))
                list.Add(suggestion);
            SetSuggestions(list);
        }

        public SuggestionFileManager()
        {
        }
    }
}
