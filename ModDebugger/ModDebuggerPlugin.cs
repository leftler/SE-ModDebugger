using System;
using VRage.Compiler;
using VRage.Plugins;

namespace ModDebugger
{
    public sealed class ModDebuggerPlugin : IPlugin
    {
        public void Dispose()
        {
        }

        public void Init(object gameInstance)
        {
            IlCompiler.Options.IncludeDebugInformation = true;
            if (String.IsNullOrWhiteSpace(IlCompiler.Options.CompilerOptions))
            {
                IlCompiler.Options.CompilerOptions = "/define:MOD_DEBUGGER_ACTIVE";
            }
            else
            {
                IlCompiler.Options.CompilerOptions += " /define:MOD_DEBUGGER_ACTIVE";
            }
            IlChecker.AllowedOperands.Add(typeof(System.Diagnostics.Debugger), null);
        }

        public void Update()
        {
        }
    }
}
