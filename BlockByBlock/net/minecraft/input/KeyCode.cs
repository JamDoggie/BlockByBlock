using BlockByBlock.net.minecraft.input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.input
{
    // LWJGL keycodes translated to OpenTK keycodes.
    public enum KeyCode
    {
        [KeyMap(Keys.Unknown)]
        NONE = 0,
        [KeyMap(Keys.Escape)]
        ESCAPE = 1,
        [KeyMap(Keys.D1)]
        One = 2,
        [KeyMap(Keys.D2)]
        Two = 3,
        [KeyMap(Keys.D3)]
        Three = 4,
        [KeyMap(Keys.D4)]
        Four = 5,
        [KeyMap(Keys.D5)]
        Five = 6,
        [KeyMap(Keys.D6)]
        Six = 7,
        [KeyMap(Keys.D7)]
        Seven = 8,
        [KeyMap(Keys.D8)]
        Eight = 9,
        [KeyMap(Keys.D9)]
        Nine = 10,
        [KeyMap(Keys.D0)]
        Zero = 11,
        [KeyMap(Keys.Minus)]
        MINUS = 12,
        [KeyMap(Keys.Equal)]
        EQUALS = 13,
        [KeyMap(Keys.Backspace)]
        BACK = 14,
        [KeyMap(Keys.Tab)]
        TAB = 15,
        [KeyMap(Keys.Q)]
        Q = 16,
        [KeyMap(Keys.W)]
        W = 17,
        [KeyMap(Keys.E)]
        E = 18,
        [KeyMap(Keys.R)]
        R = 19,
        [KeyMap(Keys.T)]
        T = 20,
        [KeyMap(Keys.Y)]
        Y = 21,
        [KeyMap(Keys.U)]
        U = 22,
        [KeyMap(Keys.I)]
        I = 23,
        [KeyMap(Keys.O)]
        O = 24,
        [KeyMap(Keys.P)]
        P = 25,
        [KeyMap(Keys.LeftBracket)]
        LBRACKET = 26,
        [KeyMap(Keys.RightBracket)]
        RBRACKET = 27,
        [KeyMap(Keys.Enter)]
        RETURN = 28,
        [KeyMap(Keys.LeftControl)]
        LCONTROL = 29,
        [KeyMap(Keys.A)]
        A = 30,
        [KeyMap(Keys.S)]
        S = 31,
        [KeyMap(Keys.D)]
        D = 32,
        [KeyMap(Keys.F)]
        F = 33,
        [KeyMap(Keys.G)]
        G = 34,
        [KeyMap(Keys.H)]
        H = 35,
        [KeyMap(Keys.J)]
        J = 36,
        [KeyMap(Keys.K)]
        K = 37,
        [KeyMap(Keys.L)]
        L = 38,
        [KeyMap(Keys.Semicolon)]
        SEMICOLON = 39,
        [KeyMap(Keys.Apostrophe)]
        APOSTROPHE = 40,
        [KeyMap(Keys.GraveAccent)]
        GRAVE = 41,
        [KeyMap(Keys.LeftShift)]
        LSHIFT = 42,
        [KeyMap(Keys.Backslash)]
        BACKSLASH = 43,
        [KeyMap(Keys.Z)]
        Z = 44,
        [KeyMap(Keys.X)]
        X = 45,
        [KeyMap(Keys.C)]
        C = 46,
        [KeyMap(Keys.V)]
        V = 47,
        [KeyMap(Keys.B)]
        B = 48,
        [KeyMap(Keys.N)]
        N = 49,
        [KeyMap(Keys.M)]
        M = 50,
        [KeyMap(Keys.Comma)]
        COMMA = 51,
        [KeyMap(Keys.Period)]
        PERIOD = 52,
        [KeyMap(Keys.Slash)]
        SLASH = 53,
        [KeyMap(Keys.RightShift)]
        RSHIFT = 54,
        [KeyMap(Keys.KeyPadMultiply)]
        MULTIPLY = 55,
        [KeyMap(Keys.LeftAlt, Keys.Menu)]
        LMENU = 56,
        [KeyMap(Keys.Space)]
        SPACE = 57,
        [KeyMap(Keys.CapsLock)]
        CAPITAL = 58,
        [KeyMap(Keys.F1)]
        F1 = 59,
        [KeyMap(Keys.F2)]
        F2 = 60,
        [KeyMap(Keys.F3)]
        F3 = 61,
        [KeyMap(Keys.F4)]
        F4 = 62,
        [KeyMap(Keys.F5)]
        F5 = 63,
        [KeyMap(Keys.F6)]
        F6 = 64,
        [KeyMap(Keys.F7)]
        F7 = 65,
        [KeyMap(Keys.F8)]
        F8 = 66,
        [KeyMap(Keys.F9)]
        F9 = 67,
        [KeyMap(Keys.F10)]
        F10 = 68,
        [KeyMap(Keys.NumLock)]
        NUMLOCK = 69,
        [KeyMap(Keys.ScrollLock)]
        SCROLL = 70,
        [KeyMap(Keys.KeyPad7)]
        NUMPAD7 = 71,
        [KeyMap(Keys.KeyPad8)]
        NUMPAD8 = 72,
        [KeyMap(Keys.KeyPad9)]
        NUMPAD9 = 73,
        [KeyMap(Keys.KeyPadSubtract)]
        SUBTRACT = 74,
        [KeyMap(Keys.KeyPad4)]
        NUMPAD4 = 75,
        [KeyMap(Keys.KeyPad5)]
        NUMPAD5 = 76,
        [KeyMap(Keys.KeyPad6)]
        NUMPAD6 = 77,
        [KeyMap(Keys.KeyPadAdd)]
        ADD = 78,
        [KeyMap(Keys.KeyPad1)]
        NUMPAD1 = 79,
        [KeyMap(Keys.KeyPad2)]
        NUMPAD2 = 80,
        [KeyMap(Keys.KeyPad3)]
        NUMPAD3 = 81,
        [KeyMap(Keys.KeyPad0)]
        NUMPAD0 = 82,
        [KeyMap(Keys.KeyPadDecimal)]
        DECIMAL = 83,
        [KeyMap(Keys.F11)]
        F11 = 87,
        [KeyMap(Keys.F12)]
        F12 = 88,
        [KeyMap(Keys.F13)]
        F13 = 100,
        [KeyMap(Keys.F14)]
        F14 = 101,
        [KeyMap(Keys.F15)]
        F15 = 102,
        [KeyMap()] // No equivalent key in OpenTK
        KANA = 112,
        [KeyMap()] // No equivalent key in OpenTK
        CONVERT = 121,
        [KeyMap()] // No equivalent key in OpenTK
        NOCONVERT = 123,
        [KeyMap()] // No equivalent key in OpenTK
        YEN = 125,
        [KeyMap(Keys.KeyPadEqual)]
        NUMPADEQUALS = 141,
        [KeyMap()] // No equivalent key in OpenTK
        CIRCUMFLEX = 144,
        [KeyMap()] // No equivalent key in OpenTK
        AT = 145,
        [KeyMap()]
        COLON = 146,
        [KeyMap()] 
        UNDERLINE = 147,
        [KeyMap()]
        KANJI = 148,
        [KeyMap()]
        STOP = 149,
        [KeyMap()]
        AX = 150,
        [KeyMap()]
        UNLABELED = 151,
        [KeyMap(Keys.KeyPadEnter)]
        NUMPADENTER = 156,
        [KeyMap(Keys.RightControl)]
        RCONTROL = 157,
        [KeyMap(Keys.RightControl)]
        SECTION = 167,
        [KeyMap()]
        NUMPADCOMMA = 179,
        [KeyMap(Keys.KeyPadDivide)]
        DIVIDE = 181,
        [KeyMap(Keys.PrintScreen)]
        SYSRQ = 183,
        [KeyMap(Keys.RightAlt)]
        RMENU = 184,
        [KeyMap()]
        FUNCTION = 196,
        [KeyMap(Keys.Pause)]
        PAUSE = 197,
        [KeyMap(Keys.Home)]
        HOME = 199,
        [KeyMap(Keys.Up)]
        UP = 200,
        [KeyMap(Keys.PageUp)]
        PRIOR = 201,
        [KeyMap(Keys.Left)]
        LEFT = 203,
        [KeyMap(Keys.Right)]
        RIGHT = 205,
        [KeyMap(Keys.End)]
        END = 207,
        [KeyMap(Keys.Down)]
        DOWN = 208,
        [KeyMap(Keys.PageDown)]
        NEXT = 209,
        [KeyMap(Keys.Insert)]
        INSERT = 210,
        [KeyMap(Keys.Delete)]
        DELETE = 211,
        [KeyMap()]
        CLEAR = 218,
        [KeyMap(Keys.LeftSuper)]
        LMETA = 219,
        [KeyMap(Keys.LeftSuper)]
        LWIN = 219,
        [KeyMap(Keys.RightSuper)]
        RMETA = 220,
        [KeyMap(Keys.RightSuper)]
        RWIN = 220,
        [KeyMap()]
        APPS = 221,
        [KeyMap()]
        POWER = 222,
        [KeyMap()]
        SLEEP = 223
    }

    public static class InputHelper
    {
        public static KeyCode FromOpenTKKey(Keys openTKKey)
        {
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                // Get C# attribute using reflection of the enum
                var field = code.GetType().GetField(code.ToString());

                if (field != null &&
                    field.GetCustomAttributes(typeof(KeyMapAttribute), false).FirstOrDefault() is KeyMapAttribute attribute)
                {
                    if (attribute.Keys.Contains(openTKKey))
                    {
                        return code;
                    }
                }
            }

            return KeyCode.NONE;
        }
    }
}
