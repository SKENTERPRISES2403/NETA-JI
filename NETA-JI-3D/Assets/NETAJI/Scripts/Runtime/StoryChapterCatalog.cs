using UnityEngine;

namespace NetaJi.Prototype
{
    public static class StoryChapterCatalog
    {
        public const int Count = 24;

        public static readonly string[] Titles =
        {
            "GHAT SE GHAR TAK", "SHAAM KI PAATHSHALA", "SANDHYA KAHAN HAI", "OPERATION UMEED",
            "DAWA KA SACH", "SEVA SE SIYASAT", "WARD KA FAISLA", "PEHLE 100 DIN",
            "VIDHANSABHA KI RAAH", "JANATA KA MANDATE", "JANATA KA MLA", "ZILA SANGATHAN",
            "PRADESH KI DASTAK", "PRADESH KA NETRUTVA", "PRADESH KA JANADESH",
            "CM KE PEHLE 100 DIN", "BADLAV KE PAANCH SAAL", "DESH BHAR KA SAATH",
            "RASHTRIYA CHUNAV", "HAAR KE BAAD HIMMAT", "PM KA JANADESH", "PM KE PEHLE 100 DIN",
            "DESH KA BADLAV", "SEVA SE VISHWA GURU"
        };

        public static string GetTitle(int chapterNumber)
        {
            int index = Mathf.Clamp(chapterNumber, 1, Count) - 1;
            return Titles[index];
        }

        public static string GetSceneName(int chapterNumber)
        {
            int safeChapter = Mathf.Clamp(chapterNumber, 1, Count);
            return safeChapter == 1 ? "Prototype01" : $"Chapter{safeChapter:00}";
        }
    }
}
