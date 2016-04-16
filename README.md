# eagle2tvm802
an Eagle Layout to TVM802 Pick and Place converter

this program loads eagle mnt and mnb files (which are exported with the standard ULP: mountsmd)
assignes a stack/tray list to the components
and generates a TVM802 compatible pick&place file.

It allows easy editing and automatic processing
of Stacks, Nozzle, Vision... and all other settings.

The software is written in C# with visual stidion 2015,
the compiled and ready-to-use program can be found in the bin/release
folder, name: eagle2tvm.exe
