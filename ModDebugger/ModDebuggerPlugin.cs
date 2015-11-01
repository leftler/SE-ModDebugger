using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using VRage.Compiler;
using VRage.Library.Collections;
using VRage.Plugins;

namespace ModDebugger
{
    public sealed class ModDebuggerPlugin : IPlugin
    {
        private readonly CustomCSharpCodeProvider _customCSharpCodeProvider;
        private readonly List<TempFileCollection> _oldTempFileCollections; 

        public ModDebuggerPlugin()
        {
            _customCSharpCodeProvider = new CustomCSharpCodeProvider();
            _customCSharpCodeProvider.CompilerCreating += OnCompilerCreating;

            _oldTempFileCollections = new CacheList<TempFileCollection>();
        }

        public void Dispose()
        {
            _customCSharpCodeProvider.CompilerCreating -= OnCompilerCreating;

            foreach (string file in _oldTempFileCollections.SelectMany(x => x.Cast<string>()))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    //If we can't delete it just skip it.
                }
            }
        }

        public void Init(object gameInstance)
        {
            bool replaced = ReplaceCompiler();
            Debug.Assert(replaced, "Failed to replace compiler.");

            IlCompiler.Options.IncludeDebugInformation = true;
            
            if (IlCompiler.Options.CompilerOptions == null)
            {
                IlCompiler.Options.CompilerOptions = "";
            }
            IlCompiler.Options.CompilerOptions += " /define:MOD_DEBUGGER_ACTIVE";

            IlChecker.AllowedOperands.Add(typeof(Debugger), null);
        }

        //Whenever we create a new compiler we need to replace the temp files collection because the compiler will try to re-use the same file names.
        private void OnCompilerCreating(object sender, EventArgs e)
        {
            ReplaceTempFileCollection();
        }

        private void ReplaceTempFileCollection()
        {
            var oldTempDir = IlCompiler.Options.TempFiles.TempDir;
            var tempFiles = new TempFileCollection(oldTempDir, true);
            IlCompiler.Options.TempFiles = tempFiles;

            _oldTempFileCollections.Add(tempFiles);
        }

        private bool ReplaceCompiler()
        {
            var fields = typeof (IlCompiler).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var field = fields.FirstOrDefault(x => x.FieldType.IsAssignableFrom(typeof (CustomCSharpCodeProvider)));
            if (field != null)
            {
                field.SetValue(null, _customCSharpCodeProvider);
                return true;
            }

            //Future proofing, check to see if it could be in a property.
            var properties = typeof(IlCompiler).GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            var property = properties.FirstOrDefault(x => x.PropertyType.IsAssignableFrom(typeof (CustomCSharpCodeProvider)));
            if (property != null)
            {
                property.SetValue(null, _customCSharpCodeProvider, null);
                return true;
            }

            return false;
        }

        public void Update()
        {
        }
    }
}
