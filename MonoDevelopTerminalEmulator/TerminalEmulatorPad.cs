using MonoDevelop.Ide.Gui;

using Gtk;
using MonoDevelop.Components;
using System;
using System.Collections;

namespace MonoDevelopTerminalEmulator
{
    public class TerminalEmulatorPad : PadContent
    {
        Vte.Terminal terminal;

        public override Control Control
        {
            get
            {
                if (terminal == null)
                    CreateControl();
                return terminal;
            }
        }

        public string DefaultPlacement => "Bottom";

        void CreateControl()
        {
            Gdk.Color white = new Gdk.Color();
            Gdk.Color.Parse("white", ref white);

            Gdk.Color black = new Gdk.Color();
            Gdk.Color.Parse("black", ref black);

            terminal = new Vte.Terminal
            {
                CursorBlinks = true,
                MouseAutohide = true,
                ScrollOnKeystroke = true,
                DeleteBinding = Vte.TerminalEraseBinding.Auto,
                BackspaceBinding = Vte.TerminalEraseBinding.Auto,
                Encoding = "UTF-8",
                Visible = true,
                IsFocus = true,
                BackgroundTintColor = black,
            };

            string[] envv = new string[Environment.GetEnvironmentVariables().Count];
            int i = 0;
            foreach (DictionaryEntry e in Environment.GetEnvironmentVariables())
            {
                if ((string)e.Key == string.Empty ||
                    (string)e.Value == string.Empty)
                {
                    continue;
                }
                string tmp = String.Format("{0}={1}", e.Key, e.Value);
                envv[i] = tmp;
                i++;
            }

            terminal.ForkCommand(Environment.GetEnvironmentVariable("SHELL"),
                                new string[0],
                                envv,
                                Environment.CurrentDirectory,
                                false,
                                true,
                                true);
        }
    }
}
