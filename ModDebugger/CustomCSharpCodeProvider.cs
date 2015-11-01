using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;

namespace ModDebugger
{
    class CustomCSharpCodeProvider : CSharpCodeProvider
    {
#pragma warning disable 618
#pragma warning disable 672
        public override ICodeCompiler CreateCompiler()
        {
            OnCompilerCreating();
            return base.CreateCompiler();
        }
#pragma warning restore 672
#pragma warning restore 618

        public event EventHandler CompilerCreating;

        private void OnCompilerCreating()
        {
            CompilerCreating?.Invoke(this, EventArgs.Empty);
        }

    }
}
