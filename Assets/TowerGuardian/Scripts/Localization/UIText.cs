using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using YG;

namespace TowerGuardian.Scripts.Localization
{
    public static class UIText
    {
        private const string Russian = "ru";
        private const string English = "en";
        private const string Turkish = "tr";

        private static readonly Dictionary<EntityType, Dictionary<string, string>> EntityTypeDict =
            new Dictionary<EntityType, Dictionary<string, string>>
            {
                {
                    EntityType.Enemy, new Dictionary<string, string>
                    {
                        { Russian, "враг" },
                        { English, "enemy" },
                        { Turkish, "düşman" },
                    }
                },
                {
                    EntityType.Stone, new Dictionary<string, string>
                    {
                        { Russian, "камень" },
                        { English, "stone" },
                        { Turkish, "taş" },
                    }
                },
                {
                    EntityType.Tree, new Dictionary<string, string>
                    {
                        { Russian, "дерево" },
                        { English, "tree" },
                        { Turkish, "ağaç" },
                    }
                },
                {
                    EntityType.Boss, new Dictionary<string, string>
                    {
                        { Russian, "босс" },
                        { English, "boss" },
                        { Turkish, "baş düşman" },
                    }
                },
                {
                    EntityType.Generic, new Dictionary<string, string>
                    {
                        { Russian, "общий" },
                        { English, "generic" },
                        { Turkish, "genel" },
                    }
                },
            };

        private static readonly Dictionary<WindowType, Dictionary<string, string>> WindowTypeDict =
            new Dictionary<WindowType, Dictionary<string, string>>
            {
                {
                    WindowType.None, new Dictionary<string, string>
                    {
                        { Russian, "нет" },
                        { English, "none" },
                        { Turkish, "yok" },
                    }
                },
                {
                    WindowType.QuestViewer, new Dictionary<string, string>
                    {
                        { Russian, "отрисовщик квестов" },
                        { English, "quest viewer" },
                        { Turkish, "görev görüntüleyici" },
                    }
                },
                {
                    WindowType.Shop, new Dictionary<string, string>
                    {
                        { Russian, "магазин" },
                        { English, "shop" },
                        { Turkish, "dükkan" },
                    }
                },
                {
                    WindowType.Sell, new Dictionary<string, string>
                    {
                        { Russian, "продажа" },
                        { English, "sell" },
                        { Turkish, "satış" },
                    }
                },
                {
                    WindowType.WinLevelMenu, new Dictionary<string, string>
                    {
                        { Russian, "меню победы" },
                        { English, "win level menu" },
                        { Turkish, "seviye kazanma menüsü" },
                    }
                },
                {
                    WindowType.StartLevelMenu, new Dictionary<string, string>
                    {
                        { Russian, "меню начала уровня" },
                        { English, "start level menu" },
                        { Turkish, "seviye başlatma menüsü" },
                    }
                },
                {
                    WindowType.LouseLevelMenu, new Dictionary<string, string>
                    {
                        { Russian, "меню поражения" },
                        { English, "lose level menu" },
                        { Turkish, "seviye kaybetme menüsü" },
                    }
                },
                {
                    WindowType.Settings, new Dictionary<string, string>
                    {
                        { Russian, "настройки" },
                        { English, "settings" },
                        { Turkish, "ayarlar" },
                    }
                },
                {
                    WindowType.MainSettings, new Dictionary<string, string>
                    {
                        { Russian, "главные настройки" },
                        { English, "main settings" },
                        { Turkish, "ana ayarlar" },
                    }
                },
                {
                    WindowType.Pause, new Dictionary<string, string>
                    {
                        { Russian, "пауза" },
                        { English, "pause" },
                        { Turkish, "duraklat" },
                    }
                },
                {
                    WindowType.CardMenu, new Dictionary<string, string>
                    {
                        { Russian, "меню карт" },
                        { English, "card menu" },
                        { Turkish, "kart menüsü" },
                    }
                },
                {
                    WindowType.HUD, new Dictionary<string, string>
                    {
                        { Russian, "интерфейс" },
                        { English, "HUD" },
                        { Turkish, "gösterge paneli" },
                    }
                },
                {
                    WindowType.MainMenu, new Dictionary<string, string>
                    {
                        { Russian, "главное меню" },
                        { English, "main menu" },
                        { Turkish, "ana menü" },
                    }
                },
                {
                    WindowType.ShowCardsButton, new Dictionary<string, string>
                    {
                        { Russian, "кнопка показать карты" },
                        { English, "show cards button" },
                        { Turkish, "kartları göster düğmesi" },
                    }
                },
                {
                    WindowType.Background, new Dictionary<string, string>
                    {
                        { Russian, "фон" },
                        { English, "background" },
                        { Turkish, "arka plan" },
                    }
                },
                {
                    WindowType.DamageScreen, new Dictionary<string, string>
                    {
                        { Russian, "экран урона" },
                        { English, "damage screen" },
                        { Turkish, "hasar ekranı" },
                    }
                },
                {
                    WindowType.WaveViewer, new Dictionary<string, string>
                    {
                        { Russian, "отображатель волн" },
                        { English, "wave viewer" },
                        { Turkish, "dalga görüntüleyici" },
                    }
                },
                {
                    WindowType.Joystick, new Dictionary<string, string>
                    {
                        { Russian, "джойстик" },
                        { English, "joystick" },
                        { Turkish, "joystick" },
                    }
                },
                {
                    WindowType.Inventory, new Dictionary<string, string>
                    {
                        { Russian, "Инвентарь" },
                        { English, "Inventory" },
                        { Turkish, "Envanter" },
                    }
                },
                {
                    WindowType.LeaderBoard, new Dictionary<string, string>
                    {
                        { Russian, "Таблица Лидеров" },
                        { English, "Leaderboard" },
                        { Turkish, "Liderlik Tablosu" },
                    }
                },
            };

        private static readonly Dictionary<string, string> DamageDict = new Dictionary<string, string>
        {
            { Russian, "урон" },
            { English, "damage" },
            { Turkish, "hasar" },
        };

        private static readonly Dictionary<string, string> AttackDelayDict = new Dictionary<string, string>
        {
            { Russian, "задержка атаки" },
            { English, "attack delay" },
            { Turkish, "saldırı gecikmesi" },
        };

        private static readonly Dictionary<string, string> AttackRangeDict = new Dictionary<string, string>
        {
            { Russian, "дальность атаки" },
            { English, "attack range" },
            { Turkish, "saldırı menzili" },
        };

        private static readonly Dictionary<string, string> FlightDistanceDict = new Dictionary<string, string>
        {
            { Russian, "дальность полёта" },
            { English, "flight distance" },
            { Turkish, "uçuş mesafesi" },
        };

        private static readonly Dictionary<string, string> CooldownDict = new Dictionary<string, string>
        {
            { Russian, "перезарядка" },
            { English, "cooldown" },
            { Turkish, "bekleme süresi" },
        };

        private static readonly Dictionary<string, string> HitCountDict = new Dictionary<string, string>
        {
            { Russian, "количество ударов" },
            { English, "hit count" },
            { Turkish, "vuruş sayısı" },
        };

        private static readonly Dictionary<string, string> BouncesCountDict = new Dictionary<string, string>
        {
            { Russian, "количество отскоков" },
            { English, "bounces count" },
            { Turkish, "zıplama sayısı" },
        };

        private static readonly Dictionary<string, string> CooldownPerHitDict = new Dictionary<string, string>
        {
            { Russian, "перезарядка за удар" },
            { English, "cooldown per hit" },
            { Turkish, "vuruş başına bekleme" },
        };

        private static readonly Dictionary<string, string> BounceRangeDict = new Dictionary<string, string>
        {
            { Russian, "радиус отскока" },
            { English, "bounce range" },
            { Turkish, "zıplama menzili" },
        };

        private static readonly Dictionary<string, string> RadiusDict = new Dictionary<string, string>
        {
            { Russian, "радиус" },
            { English, "radius" },
            { Turkish, "yarıçap" },
        };

        private static readonly Dictionary<string, string> RotationSpeedDict = new Dictionary<string, string>
        {
            { Russian, "скорость вращения" },
            { English, "rotation speed" },
            { Turkish, "dönüş hızı" },
        };

        private static readonly Dictionary<string, string> IncreaseValueDict = new Dictionary<string, string>
        {
            { Russian, "значение увеличения" },
            { English, "increase value" },
            { Turkish, "artış değeri" },
        };

        private static readonly Dictionary<string, string> CountDict = new Dictionary<string, string>
        {
            { Russian, "количество" },
            { English, "count" },
            { Turkish, "saymak" },
        };

        private static readonly Dictionary<string, string> MultiplierDict = new Dictionary<string, string>
        {
            { Russian, "Множитель к" },
            { English, "Multiplier to" },
            { Turkish, "Çarpan" },
        };

        private static readonly Dictionary<string, string> MaxLevelDict = new Dictionary<string, string>
        {
            { Russian, "Макс. уровень" },
            { English, "Max Level" },
            { Turkish, "Maks Seviye" },
        };

        private static readonly Dictionary<string, string> ShopDict = new Dictionary<string, string>
        {
            { Russian, "Maгазин" },
            { English, "Shop" },
            { Turkish, "Mağaza" },
        };

        private static readonly Dictionary<string, string> LVLDict = new Dictionary<string, string>
        {
            { Russian, "Ур" },
            { English, "LVL" },
            { Turkish, "LVL" },
        };

        private static readonly Dictionary<string, string> YourBestScoreDict = new Dictionary<string, string>
        {
            { Russian, "ВАШ ЛУЧШИЙ СЧЕТ" },
            { English, "YOUR BEST SCORE" },
            { Turkish, "EN İYİ SKORUN" },
        };

        private static readonly Dictionary<string, string> NoBestScoreDict = new Dictionary<string, string>
        {
            { Russian, "НЕТ СЧЕТА" },
            { English, "NO SCORE" },
            { Turkish, "PUAN YOK" },
        };

        public static string YourBestScore => GetText(YourBestScoreDict);

        public static string NoBestScore => GetText(NoBestScoreDict);

        public static string Damage => GetText(DamageDict);

        public static string AttackDelay => GetText(AttackDelayDict);

        public static string AttackRange => GetText(AttackRangeDict);

        public static string FlightDistance => GetText(FlightDistanceDict);

        public static string Cooldown => GetText(CooldownDict);

        public static string HitCount => GetText(HitCountDict);

        public static string BouncesCount => GetText(BouncesCountDict);

        public static string CooldownPerHit => GetText(CooldownPerHitDict);

        public static string BounceRange => GetText(BounceRangeDict);

        public static string Radius => GetText(RadiusDict);

        public static string RotationSpeed => GetText(RotationSpeedDict);

        public static string IncreaseValue => GetText(IncreaseValueDict);

        public static string Count => GetText(CountDict);

        public static string Multiplier => GetText(MultiplierDict);

        public static string Shop => GetText(ShopDict);

        public static string LVL => GetText(LVLDict);

        public static string MaxLevel => GetText(MaxLevelDict);

        private static string CurrentLanguage => YG2.lang;

        public static string GetEntityTypeText(EntityType type)
        {
            if (EntityTypeDict.TryGetValue(type, out var translations))
            {
                if (!string.IsNullOrEmpty(CurrentLanguage) && translations.TryGetValue(CurrentLanguage, out var localized))
                {
                    return localized;
                }

                if (translations.TryGetValue(English, out var en))
                {
                    return en;
                }
            }

            return type.ToString();
        }

        public static string GetWindowTypeText(WindowType type)
        {
            if (WindowTypeDict.TryGetValue(type, out var translations))
            {
                if (!string.IsNullOrEmpty(CurrentLanguage) && translations.TryGetValue(CurrentLanguage, out var localized))
                {
                    return localized;
                }

                if (translations.TryGetValue(English, out var en))
                {
                    return en;
                }
            }

            return type.ToString();
        }

        private static string GetText(Dictionary<string, string> dict)
        {
            if (dict.TryGetValue(CurrentLanguage, out string value))
            {
                return value;
            }

            return dict[English];
        }
    }
}