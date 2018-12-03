using MonoDevelop.Ide.Gui;

using Gtk;
using MonoDevelop.Components;
using System;
using System.Collections;

namespace MonoDevelopTerminalEmulator
{
    public class TerminalEmulatorPad : PadContent
    {
        Vte.Terminal control;

        public TerminalEmulatorPad()
        {
        }

        public TerminalEmulatorPad(string title, string icon = null) : base(title, icon)
        {
        }

        public override Control Control
        {
            get
            {
                if (control == null)
                    CreateControl();
                return control;
            }
        }
        public string DefaultPlacement => "Bottom";

        public void CreateControl()
        {
            control = new Vte.Terminal
            {
                CursorBlinks = true,
                MouseAutohide = true,
                ScrollOnKeystroke = true,
                DeleteBinding = Vte.TerminalEraseBinding.Auto,
                BackspaceBinding = Vte.TerminalEraseBinding.Auto,
                Encoding = "UTF-8",
            };

            string[] argv = Environment.GetCommandLineArgs();

            string[] envv = new string[Environment.GetEnvironmentVariables().Count];
            int i = 0;
            foreach (DictionaryEntry e in Environment.GetEnvironmentVariables())
            {
                if (e.Key == string.Empty || e.Value == string.Empty)
                {
                    continue;
                }

                string tmp = String.Format("{0}={1}", e.Key, e.Value);
                envv[i] = tmp;
                i++;
            }

            int pid = control.ForkCommand(
                Environment.GetEnvironmentVariable("SHELL"),
                argv,
                envv,
                Environment.CurrentDirectory,
                false,
                true,
                true);

            Console.WriteLine("Child pid: {0}", pid);

            control.Show();
        }
    }
}
