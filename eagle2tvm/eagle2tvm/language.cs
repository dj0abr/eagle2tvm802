using System;

namespace eagle2tvm
{
    public static class language
    {
        static String[] sen = new String[] {
"incomplete definition of ",    // 0
"Error TOP side",     // 1
"Error BOTTOM side",     // 2
"Folder",  // 3
"Filename",   // 4
"Devices", //5
"mirrored",   // 6
"Assign Stack/Tray values automatically ?", // 7
"Automatic Assignment", // 8
"Stack / Tray Assignments:", // 9
"List", // 10
"side",    // 11
        };
        static String[] spl = new String[] {
"Niekompletna definicja: ",    // 0
"Błąd na stronie TOP",     // 1
"Błąd na stronie BOTTOM",     // 2
"Folder",  // 3
"Nazwa pliku",   // 4
"Elementy", //5
"lustrzane odbicie",   // 6
"Przydzielić wartości torów podajników/tacek automatycznie?", // 7
"Przydział automatyczny", // 8
"Przydział podajników/tacek:", // 9
"Lista", // 10
"strona",    // 11
        };

        // language.str(x)
        static String[] sde = new String[] {
"unvollständige Definition von ",    // 0
"Fehler TOP Seite",     // 1
"Fehler BOTTOM Seite",     // 2
"Verzeichnis",  // 3
"Dateinamen",   // 4
"Bauteile", //5
"ist gespiegelt",   // 6
"Werte aus der Stack/Tray Liste automatisch zuweisen ?", // 7
"Automatische Zuweisung", // 8
"Stack / Tray Bauteilzuweisungen:", // 9
"Liste", // 10
"Seite",    // 11
        };

        public static String str(int idx)
        {
            if (info.lang == 0) return sen[idx];
            if (info.lang == 2) return spl[idx];
            return sde[idx];
        }
    }
}
