using System.Collections.Generic;
using YG;

public static class UIText
{
    private const string Russian = "ru";
    private const string English = "en";
    private const string Turkish = "tr";

    private static string CurrentLanguage => YG2.lang;

    private static readonly Dictionary<EntityType, Dictionary<string, string>> EntityTypeDict =
    new Dictionary<EntityType, Dictionary<string, string>>
    {
        { EntityType.Enemy, new Dictionary<string, string>
            {
                { Russian, "враг" },
                { English, "enemy" },
                { Turkish, "düşman" }
            }
        },
        { EntityType.Stone, new Dictionary<string, string>
            {
                { Russian, "камень" },
                { English, "stone" },
                { Turkish, "taş" }
            }
        },
        { EntityType.Tree, new Dictionary<string, string>
            {
                { Russian, "дерево" },
                { English, "tree" },
                { Turkish, "ağaç" }
            }
        },
        { EntityType.Boss, new Dictionary<string, string>
            {
                { Russian, "босс" },
                { English, "boss" },
                { Turkish, "baş düşman" }
            }
        },
        { EntityType.Generic, new Dictionary<string, string>
            {
                { Russian, "общий" },
                { English, "generic" },
                { Turkish, "genel" }
            }
        }
    };

    private static readonly Dictionary<string, string> DamageDict = new Dictionary<string, string>
    {
        { Russian, "урон" },
        { English, "damage" },
        { Turkish, "hasar" }
    };

    private static readonly Dictionary<string, string> AttackDelayDict = new Dictionary<string, string>
    {
        { Russian, "задержка атаки" },
        { English, "attack delay" },
        { Turkish, "saldırı gecikmesi" }
    };

    private static readonly Dictionary<string, string> AttackRangeDict = new Dictionary<string, string>
    {
        { Russian, "дальность атаки" },
        { English, "attack range" },
        { Turkish, "saldırı menzili" }
    };

    private static readonly Dictionary<string, string> FlightDistanceDict = new Dictionary<string, string>
    {
        { Russian, "дальность полёта" },
        { English, "flight distance" },
        { Turkish, "uçuş mesafesi" }
    };

    private static readonly Dictionary<string, string> CooldownDict = new Dictionary<string, string>
    {
        { Russian, "перезарядка" },
        { English, "cooldown" },
        { Turkish, "bekleme süresi" }
    };

    private static readonly Dictionary<string, string> HitCountDict = new Dictionary<string, string>
    {
        { Russian, "количество ударов" },
        { English, "hit count" },
        { Turkish, "vuruş sayısı" }
    };

    private static readonly Dictionary<string, string> BouncesCountDict = new Dictionary<string, string>
    {
        { Russian, "количество отскоков" },
        { English, "bounces count" },
        { Turkish, "zıplama sayısı" }
    };

    private static readonly Dictionary<string, string> CooldownPerHitDict = new Dictionary<string, string>
    {
        { Russian, "перезарядка за удар" },
        { English, "cooldown per hit" },
        { Turkish, "vuruş başına bekleme" }
    };

    private static readonly Dictionary<string, string> BounceRangeDict = new Dictionary<string, string>
    {
        { Russian, "радиус отскока" },
        { English, "bounce range" },
        { Turkish, "zıplama menzili" }
    };

    private static readonly Dictionary<string, string> RadiusDict = new Dictionary<string, string>
    {
        { Russian, "радиус" },
        { English, "radius" },
        { Turkish, "yarıçap" }
    };

    private static readonly Dictionary<string, string> RotationSpeedDict = new Dictionary<string, string>
    {
        { Russian, "скорость вращения" },
        { English, "rotation speed" },
        { Turkish, "dönüş hızı" }
    };

    private static readonly Dictionary<string, string> IncreaseValueDict = new Dictionary<string, string>
    {
        { Russian, "значение увеличения" },
        { English, "increase value" },
        { Turkish, "artış değeri" }
    };

    private static readonly Dictionary<string, string> CountDict = new Dictionary<string, string>
    {
        { Russian, "количество" },
        { English, "count" },
        { Turkish, "saymak" }
    };

    private static readonly Dictionary<string, string> MultiplierDict = new Dictionary<string, string>
    {
        { Russian, "Множитель к" },
        { English, "Multiplier to" },
        { Turkish, "Çarpan" }
    };

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

    private static string GetText(Dictionary<string, string> dict)
    {
        if (dict.TryGetValue(CurrentLanguage, out string value))
            return value;
        return dict[English];
    }

    public static string GetEntityTypeText(EntityType type)
    {
        if (EntityTypeDict.TryGetValue(type, out var translations))
        {
            if (string.IsNullOrEmpty(CurrentLanguage) == false && translations.TryGetValue(CurrentLanguage, out var localized))
                return localized;

            if (translations.TryGetValue(English, out var en)) return en;
        }

        return type.ToString();
    }
}
