using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "TerminalEmulator",
    Namespace = "MonoDevelopTerminalEmulator",
    Version = "1.0"
)]

[assembly: AddinName("TerminalEmulator")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("Open a Terminal inside the IDE")]
[assembly: AddinAuthor("David Tavarez")]

[assembly: ImportAddinAssembly("vte-sharp.dll")]
