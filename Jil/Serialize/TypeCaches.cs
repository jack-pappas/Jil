using Sigil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Jil.Serialize
{
    interface ISerializeOptions
    {
        bool PrettyPrint { get; }
        bool ExcludeNulls { get; }
        DateTimeFormat DateFormat { get; }
        bool JSONP { get; }
        bool IncludeInherited { get; }
        UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get; }
        SerializationNameFormat SerializationNameFormat { get; }
    }

    static class TypeCache<TOptions, T>
        where TOptions : struct, ISerializeOptions
    {
        static readonly object ThunkInitLock = new object();
        static volatile bool ThunkBeingBuilt = false;
        public static volatile Action<TextWriter, T, int> Thunk;
        public static Exception ThunkExceptionDuringBuild;

        static readonly object StringThunkInitLock = new object();
        static volatile bool StringThunkBeingBuilt = false;
        public static volatile StringThunkDelegate<T> StringThunk;
        public static Exception StringThunkExceptionDuringBuild;

        public static Action<TextWriter, T, int> Get()
        {
            Load();
            return Thunk;
        }

        public static void Load()
        {
            if (Thunk != null) return;

            lock (ThunkInitLock)
            {
                if (Thunk != null || ThunkBeingBuilt) return;
                ThunkBeingBuilt = true;

                var opts = new TOptions();

                Thunk = InlineSerializerHelper.Build<T>(typeof(TOptions), pretty: opts.PrettyPrint, excludeNulls: opts.ExcludeNulls, dateFormat: opts.DateFormat, jsonp: opts.JSONP, includeInherited: opts.IncludeInherited, dateTimeBehavior: opts.DateTimeKindBehavior, serializationNameFormat: opts.SerializationNameFormat, exceptionDuringBuild: out ThunkExceptionDuringBuild);
            }
        }

        public static StringThunkDelegate<T> GetToString()
        {
            LoadToString();
            return StringThunk;
        }

        public static void LoadToString()
        {
            if (StringThunk != null) return;

            lock (StringThunkInitLock)
            {
                if (StringThunk != null || StringThunkBeingBuilt) return;
                StringThunkBeingBuilt = true;

                var opts = new TOptions();

                StringThunk = InlineSerializerHelper.BuildToString<T>(typeof(TOptions), pretty: opts.PrettyPrint, excludeNulls: opts.ExcludeNulls, dateFormat: opts.DateFormat, jsonp: opts.JSONP, includeInherited: opts.IncludeInherited, dateTimeBehavior: opts.DateTimeKindBehavior, serializationNameFormat: opts.SerializationNameFormat, exceptionDuringBuild: out StringThunkExceptionDuringBuild);
            }
        }
    }

    // Start OptionsGeneration.linq generated content
    struct MicrosoftStyle : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrint : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStyleExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MicrosoftStylePrettyPrintExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MicrosoftStyleMillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601 : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrint : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601JSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601Inherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601Utc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601CamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601JSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601JSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601JSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601InheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601InheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601UtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601JSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601JSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601JSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601InheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601ExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601JSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601ExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct ISO8601PrettyPrintExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.ISO8601; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct Milliseconds : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrint : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct MillisecondsPrettyPrintExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.MillisecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123 : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrint : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123JSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123Inherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123Utc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123CamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123JSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123JSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123JSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123InheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123InheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123UtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123JSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123JSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123JSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123InheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123ExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123JSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123ExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct RFC1123PrettyPrintExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.RFC1123; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct Seconds : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrint : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNulls : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONP : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPInherited : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPInheritedUtc : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.Verbatim; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPInheritedCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsLocal; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return false; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return false; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return false; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return false; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }

    struct SecondsPrettyPrintExcludeNullsJSONPInheritedUtcCamelCase : ISerializeOptions
    {
        public DateTimeFormat DateFormat { get { return DateTimeFormat.SecondsSinceUnixEpoch; } }
        public bool PrettyPrint { get { return true; } }
        public bool ExcludeNulls { get { return true; } }
        public bool JSONP { get { return true; } }
        public bool IncludeInherited { get { return true; } }
        public UnspecifiedDateTimeKindBehavior DateTimeKindBehavior { get { return UnspecifiedDateTimeKindBehavior.IsUTC; } }
        public SerializationNameFormat SerializationNameFormat { get { return SerializationNameFormat.CamelCase; } }
    }
    // End OptionsGeneration.linq generated content
}