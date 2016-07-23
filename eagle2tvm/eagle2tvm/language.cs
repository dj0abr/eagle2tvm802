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
            return sde[idx];
        }
    }
}
