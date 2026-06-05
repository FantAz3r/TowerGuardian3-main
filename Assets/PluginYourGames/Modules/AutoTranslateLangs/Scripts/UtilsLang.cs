using UnityEngine;
#if TMP_YG2
using TMPro;
#endif

namespace YG.LanguageLegacy
{
    public class UtilsLang : MonoBehaviour
    {
        public static bool LangCheckExist(string lang)
        {
            InfoYG.AutoTranslateLangsSettings inf = YG2.infoYG.AutoTranslateLangs;

            if (lang == "ru" && inf.languages.ru)
                return true;
            if (lang == "en" && inf.languages.en)
                return true;
            if (lang == "tr" && inf.languages.tr)
                return true;
            if (lang == "az" && inf.languages.az)
                return true;
            if (lang == "be" && inf.languages.be)
                return true;
            if (lang == "he" && inf.languages.he)
                return true;
            if (lang == "hy" && inf.languages.hy)
                return true;
            if (lang == "ka" && inf.languages.ka)
                return true;
            if (lang == "et" && inf.languages.et)
                return true;
            if (lang == "fr" && inf.languages.fr)
                return true;
            if (lang == "kk" && inf.languages.kk)
                return true;
            if (lang == "ky" && inf.languages.ky)
                return true;
            if (lang == "lt" && inf.languages.lt)
                return true;
            if (lang == "lv" && inf.languages.lv)
                return true;
            if (lang == "ro" && inf.languages.ro)
                return true;
            if (lang == "tg" && inf.languages.tg)
                return true;
            if (lang == "tk" && inf.languages.tk)
                return true;
            if (lang == "uk" && inf.languages.uk)
                return true;
            if (lang == "uz" && inf.languages.uz)
                return true;
            if (lang == "es" && inf.languages.es)
                return true;
            if (lang == "ar" && inf.languages.ar)
                return true;
            if (lang == "id" && inf.languages.id)
                return true;
            if (lang == "ja" && inf.languages.ja)
                return true;
            if (lang == "de" && inf.languages.de)
                return true;
            if (lang == "hi" && inf.languages.hi)
                return true;
            if (lang == "it" && inf.languages.it)
                return true;
            if (lang == "pt" && inf.languages.pt)
                return true;

            return false;
        }

        public static bool[] LangIsActive()
        {
            InfoYG.AutoTranslateLangsSettings inf = YG2.infoYG.AutoTranslateLangs;
            bool[] b = new bool[27];

            b[0] = inf.languages.ru;
            b[1] = inf.languages.en;
            b[2] = inf.languages.tr;
            b[3] = inf.languages.az;
            b[4] = inf.languages.be;
            b[5] = inf.languages.he;
            b[6] = inf.languages.hy;
            b[7] = inf.languages.ka;
            b[8] = inf.languages.et;
            b[9] = inf.languages.fr;
            b[10] = inf.languages.kk;
            b[11] = inf.languages.ky;
            b[12] = inf.languages.lt;
            b[13] = inf.languages.lv;
            b[14] = inf.languages.ro;
            b[15] = inf.languages.tg;
            b[16] = inf.languages.tk;
            b[17] = inf.languages.uk;
            b[18] = inf.languages.uz;
            b[19] = inf.languages.es;
            b[20] = inf.languages.pt;
            b[21] = inf.languages.ar;
            b[22] = inf.languages.id;
            b[23] = inf.languages.ja;
            b[24] = inf.languages.it;
            b[25] = inf.languages.de;
            b[26] = inf.languages.hi;

            return b;
        }

        public static string LangName(int i)
        {
            if (i == 0) return "ru";
            if (i == 1) return "en";
            if (i == 2) return "tr";
            if (i == 3) return "az";
            if (i == 4) return "be";
            if (i == 5) return "he";
            if (i == 6) return "hy";
            if (i == 7) return "ka";
            if (i == 8) return "et";
            if (i == 9) return "fr";
            if (i == 10) return "kk";
            if (i == 11) return "ky";
            if (i == 12) return "lt";
            if (i == 13) return "lv";
            if (i == 14) return "ro";
            if (i == 15) return "tg";
            if (i == 16) return "tk";
            if (i == 17) return "uk";
            if (i == 18) return "uz";
            if (i == 19) return "es";
            if (i == 20) return "pt";
            if (i == 21) return "ar";
            if (i == 22) return "id";
            if (i == 23) return "ja";
            if (i == 24) return "it";
            if (i == 25) return "de";
            if (i == 26) return "hi";
            return null;
        }

        public static Font[] GetFont(int i, InfoYG.AutoTranslateLangsSettings inf)
        {
            if (i == 0) return inf.fonts.ru;
            if (i == 1) return inf.fonts.en;
            if (i == 2) return inf.fonts.tr;
            if (i == 3) return inf.fonts.az;
            if (i == 4) return inf.fonts.be;
            if (i == 5) return inf.fonts.he;
            if (i == 6) return inf.fonts.hy;
            if (i == 7) return inf.fonts.ka;
            if (i == 8) return inf.fonts.et;
            if (i == 9) return inf.fonts.fr;
            if (i == 10) return inf.fonts.kk;
            if (i == 11) return inf.fonts.ky;
            if (i == 12) return inf.fonts.lt;
            if (i == 13) return inf.fonts.lv;
            if (i == 14) return inf.fonts.ro;
            if (i == 15) return inf.fonts.tg;
            if (i == 16) return inf.fonts.tk;
            if (i == 17) return inf.fonts.uk;
            if (i == 18) return inf.fonts.uz;
            if (i == 19) return inf.fonts.es;
            if (i == 20) return inf.fonts.pt;
            if (i == 21) return inf.fonts.ar;
            if (i == 22) return inf.fonts.id;
            if (i == 23) return inf.fonts.ja;
            if (i == 24) return inf.fonts.it;
            if (i == 25) return inf.fonts.de;
            if (i == 26) return inf.fonts.hi;
            return null;
        }

#if TMP_YG2
        public static TMP_FontAsset[] GetFontTMP(int i, InfoYG.AutoTranslateLangsSettings inf)
        {
            if (i == 0) return inf.fontsTMP.ru;
            if (i == 1) return inf.fontsTMP.en;
            if (i == 2) return inf.fontsTMP.tr;
            if (i == 3) return inf.fontsTMP.az;
            if (i == 4) return inf.fontsTMP.be;
            if (i == 5) return inf.fontsTMP.he;
            if (i == 6) return inf.fontsTMP.hy;
            if (i == 7) return inf.fontsTMP.ka;
            if (i == 8) return inf.fontsTMP.et;
            if (i == 9) return inf.fontsTMP.fr;
            if (i == 10) return inf.fontsTMP.kk;
            if (i == 11) return inf.fontsTMP.ky;
            if (i == 12) return inf.fontsTMP.lt;
            if (i == 13) return inf.fontsTMP.lv;
            if (i == 14) return inf.fontsTMP.ro;
            if (i == 15) return inf.fontsTMP.tg;
            if (i == 16) return inf.fontsTMP.tk;
            if (i == 17) return inf.fontsTMP.uk;
            if (i == 18) return inf.fontsTMP.uz;
            if (i == 19) return inf.fontsTMP.es;
            if (i == 20) return inf.fontsTMP.pt;
            if (i == 21) return inf.fontsTMP.ar;
            if (i == 22) return inf.fontsTMP.id;
            if (i == 23) return inf.fontsTMP.ja;
            if (i == 24) return inf.fontsTMP.it;
            if (i == 25) return inf.fontsTMP.de;
            if (i == 26) return inf.fontsTMP.hi;
            return null;
        }
#endif

        public static int[] GetFontSize(int i, InfoYG.AutoTranslateLangsSettings inf)
        {
            if (i == 0) return inf.fontsSizeCorrect.ru;
            if (i == 1) return inf.fontsSizeCorrect.en;
            if (i == 2) return inf.fontsSizeCorrect.tr;
            if (i == 3) return inf.fontsSizeCorrect.az;
            if (i == 4) return inf.fontsSizeCorrect.be;
            if (i == 5) return inf.fontsSizeCorrect.he;
            if (i == 6) return inf.fontsSizeCorrect.hy;
            if (i == 7) return inf.fontsSizeCorrect.ka;
            if (i == 8) return inf.fontsSizeCorrect.et;
            if (i == 9) return inf.fontsSizeCorrect.fr;
            if (i == 10) return inf.fontsSizeCorrect.kk;
            if (i == 11) return inf.fontsSizeCorrect.ky;
            if (i == 12) return inf.fontsSizeCorrect.lt;
            if (i == 13) return inf.fontsSizeCorrect.lv;
            if (i == 14) return inf.fontsSizeCorrect.ro;
            if (i == 15) return inf.fontsSizeCorrect.tg;
            if (i == 16) return inf.fontsSizeCorrect.tk;
            if (i == 17) return inf.fontsSizeCorrect.uk;
            if (i == 18) return inf.fontsSizeCorrect.uz;
            if (i == 19) return inf.fontsSizeCorrect.es;
            if (i == 20) return inf.fontsSizeCorrect.pt;
            if (i == 21) return inf.fontsSizeCorrect.ar;
            if (i == 22) return inf.fontsSizeCorrect.id;
            if (i == 23) return inf.fontsSizeCorrect.ja;
            if (i == 24) return inf.fontsSizeCorrect.it;
            if (i == 25) return inf.fontsSizeCorrect.de;
            if (i == 26) return inf.fontsSizeCorrect.hi;
            return null;
        }
    }
}