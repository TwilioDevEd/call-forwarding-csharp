﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallForwarding.Web.Properties {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CallForwarding.Web.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;Zipcode&quot;,&quot;ZipCodeType&quot;,&quot;City&quot;,&quot;State&quot;,&quot;LocationType&quot;,&quot;Lat&quot;,&quot;Long&quot;,&quot;Location&quot;,&quot;Decommisioned&quot;,&quot;TaxReturnsFiled&quot;,&quot;EstimatedPopulation&quot;,&quot;TotalWages&quot;
        ///&quot;00705&quot;,&quot;STANDARD&quot;,&quot;AIBONITO&quot;,&quot;PR&quot;,&quot;PRIMARY&quot;,18.14,-66.26,&quot;NA-US-PR-AIBONITO&quot;,&quot;false&quot;,,,
        ///&quot;00610&quot;,&quot;STANDARD&quot;,&quot;ANASCO&quot;,&quot;PR&quot;,&quot;PRIMARY&quot;,18.28,-67.14,&quot;NA-US-PR-ANASCO&quot;,&quot;false&quot;,,,
        ///&quot;00611&quot;,&quot;PO BOX&quot;,&quot;ANGELES&quot;,&quot;PR&quot;,&quot;PRIMARY&quot;,18.28,-66.79,&quot;NA-US-PR-ANGELES&quot;,&quot;false&quot;,,,
        ///&quot;00612&quot;,&quot;STANDARD&quot;,&quot;ARECIBO&quot;,&quot;PR&quot;,&quot;PRIMARY&quot;,18.45,-66.73,&quot;NA-US-PR-ARECIBO&quot;,&quot;false&quot;,,,
        ///&quot;00601&quot;,&quot;STAND [rest of string was truncated]&quot;;.
        /// </summary>
        public static string free_zipcode_database {
            get {
                return ResourceManager.GetString("free_zipcode_database", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] senators {
            get {
                object obj = ResourceManager.GetObject("senators", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
