﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FastReader {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Language {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Language() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FastReader.Language", typeof(Language).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Get metoda property &apos;{0}&apos; typu &apos;{1}&apos; neni abstraktni..
        /// </summary>
        internal static string GetMethodIsNotAbstract {
            get {
                return ResourceManager.GetString("GetMethodIsNotAbstract", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; nebyla nalezena..
        /// </summary>
        internal static string PropertyWasNotFound {
            get {
                return ResourceManager.GetString("PropertyWasNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Set metoda property &apos;{0}&apos; typu &apos;{1}&apos; neni abstraktni..
        /// </summary>
        internal static string SetMethodIsNotAbstract {
            get {
                return ResourceManager.GetString("SetMethodIsNotAbstract", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Typ &apos;{0}&apos; neobsahuje odpovidajici konstruktor..
        /// </summary>
        internal static string SuitableCtorNotFound {
            get {
                return ResourceManager.GetString("SuitableCtorNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nastal nesoulad typu. Typ property je &apos;{0}&apos;, ocekavany typ je &apos;{1}&apos;..
        /// </summary>
        internal static string TypeMismatch {
            get {
                return ResourceManager.GetString("TypeMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Neocekavany typ hodnoty &apos;{0}&apos;. Byl ocekavan typ &apos;{1}&apos;..
        /// </summary>
        internal static string UnexpectedValueType {
            get {
                return ResourceManager.GetString("UnexpectedValueType", resourceCulture);
            }
        }
    }
}
