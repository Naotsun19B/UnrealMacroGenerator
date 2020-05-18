using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;
using UnrealMacroGenerator.Properties;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace UnrealMacroGenerator
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(CommandPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(OptionPageGrid), "Unreal Macro Generator", "Unreal Macro Generator Settings", 0, 0, true)]
    public sealed class CommandPackage : AsyncPackage
    {
        /// <summary>
        /// GenerateCommandPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "45543a66-76de-4020-a2ba-ebab6371cf2a";

        private static CommandPackage UniqueInstance;
        public static CommandPackage Instance
        {
            get
            {
                if(UniqueInstance == null)
                {
                    UniqueInstance = new CommandPackage();
                }
                return UniqueInstance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandPackage"/> class.
        /// </summary>
        public CommandPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
            XmlFunctionLibrary.LoadDefaultConfig();
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await Command.GenerateCommand.InitializeAsync(this);
            await Command.EditCommand.InitializeAsync(this);
        }

        #endregion
    }

    class CustomCollectionEditor : CollectionEditor
    {
        public CustomCollectionEditor() : base(type: typeof(StringCollection)) { }
        protected override object CreateInstance(Type itemType)
        {
            return string.Empty;
        }
    }

    public class OptionPageGrid : DialogPage
    {
        [Category("UPROPERTY")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for UPROPERTY")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UPROPERTY_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.UPROPERTY_MacroSpecifiers == null)
                {
                    Settings.Default.UPROPERTY_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.UPROPERTY_MacroSpecifiers;
            }
            set
            {
                Settings.Default.UPROPERTY_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UPROPERTY")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for UPROPERTY\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UPROPERTY_AdvancedSettings
        {
            get
            {
                if (Settings.Default.UPROPERTY_AdvancedSettings == null)
                {
                    Settings.Default.UPROPERTY_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.UPROPERTY_AdvancedSettings;
            }
            set
            {
                Settings.Default.UPROPERTY_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("UPROPERTY")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for UPROPERTY\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UPROPERT_YMetaSpecifiers
        {
            get
            {
                if (Settings.Default.UPROPERTY_MetaSpecifiers == null)
                {
                    Settings.Default.UPROPERTY_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.UPROPERTY_MetaSpecifiers;
            }
            set
            {
                Settings.Default.UPROPERTY_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UFUNCTION")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for UFUNCTION")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UFUNCTION_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.UFUNCTION_MacroSpecifiers == null)
                {
                    Settings.Default.UFUNCTION_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.UFUNCTION_MacroSpecifiers;
            }
            set
            {
                Settings.Default.UFUNCTION_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UFUNCTION")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for UFUNCTION\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UFUNCTION_AdvancedSettings
        {
            get
            {
                if (Settings.Default.UFUNCTION_AdvancedSettings == null)
                {
                    Settings.Default.UFUNCTION_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.UFUNCTION_AdvancedSettings;
            }
            set
            {
                Settings.Default.UFUNCTION_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("UFUNCTION")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for UFUNCTION\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UFUNCTION_MetaSpecifiers
        {
            get
            {
                if (Settings.Default.UFUNCTION_MetaSpecifiers == null)
                {
                    Settings.Default.UFUNCTION_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.UFUNCTION_MetaSpecifiers;
            }
            set
            {
                Settings.Default.UFUNCTION_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UCLASS")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for UCLASS")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UCLASS_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.UCLASS_MacroSpecifiers == null)
                {
                    Settings.Default.UCLASS_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.UCLASS_MacroSpecifiers;
            }
            set
            {
                Settings.Default.UCLASS_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UCLASS")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for UCLASS\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UCLASS_AdvancedSettings
        {
            get
            {
                if (Settings.Default.UCLASS_AdvancedSettings == null)
                {
                    Settings.Default.UCLASS_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.UCLASS_AdvancedSettings;
            }
            set
            {
                Settings.Default.UCLASS_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("UCLASS")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for UCLASS\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UCLASS_MetaSpecifiers
        {
            get
            {
                if (Settings.Default.UCLASS_MetaSpecifiers == null)
                {
                    Settings.Default.UCLASS_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.UCLASS_MetaSpecifiers;
            }
            set
            {
                Settings.Default.UCLASS_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("USTRUCT")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for USTRUCT")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_USTRUCT_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.USTRUCT_MacroSpecifiers == null)
                {
                    Settings.Default.USTRUCT_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.USTRUCT_MacroSpecifiers;
            }
            set
            {
                Settings.Default.USTRUCT_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("USTRUCT")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for USTRUCT\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_USTRUCT_AdvancedSettings
        {
            get
            {
                if (Settings.Default.USTRUCT_AdvancedSettings == null)
                {
                    Settings.Default.USTRUCT_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.USTRUCT_AdvancedSettings;
            }
            set
            {
                Settings.Default.USTRUCT_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("USTRUCT")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for USTRUCT\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_USTRUCT_MetaSpecifiers
        {
            get
            {
                if (Settings.Default.USTRUCT_MetaSpecifiers == null)
                {
                    Settings.Default.USTRUCT_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.USTRUCT_MetaSpecifiers;
            }
            set
            {
                Settings.Default.USTRUCT_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UENUM")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for UENUM")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UENUM_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.UENUM_MacroSpecifiers == null)
                {
                    Settings.Default.UENUM_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.UENUM_MacroSpecifiers;
            }
            set
            {
                Settings.Default.UENUM_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UENUM")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for UENUM\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UENUM_AdvancedSettings
        {
            get
            {
                if (Settings.Default.UENUM_AdvancedSettings == null)
                {
                    Settings.Default.UENUM_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.UENUM_AdvancedSettings;
            }
            set
            {
                Settings.Default.UENUM_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("UENUM")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for UENUM\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UENUM_MetaSpecifiers
        {
            get
            {
                if (Settings.Default.UENUM_MetaSpecifiers == null)
                {
                    Settings.Default.UENUM_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.UENUM_MetaSpecifiers;
            }
            set
            {
                Settings.Default.UENUM_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UINTERFACE")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for UINTERFACE")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UINTERFACE_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.UINTERFACE_MacroSpecifiers == null)
                {
                    Settings.Default.UINTERFACE_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.UINTERFACE_MacroSpecifiers;
            }
            set
            {
                Settings.Default.UINTERFACE_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UINTERFACE")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for UINTERFACE\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UINTERFACE_AdvancedSettings
        {
            get
            {
                if (Settings.Default.UINTERFACE_AdvancedSettings == null)
                {
                    Settings.Default.UINTERFACE_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.UINTERFACE_AdvancedSettings;
            }
            set
            {
                Settings.Default.UINTERFACE_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("UINTERFACE")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for UINTERFACE\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UINTERFACE_MetaSpecifiers
        {
            get
            {
                if (Settings.Default.UINTERFACE_MetaSpecifiers == null)
                {
                    Settings.Default.UINTERFACE_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.UINTERFACE_MetaSpecifiers;
            }
            set
            {
                Settings.Default.UINTERFACE_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UMETA")]
        [DisplayName("Specifiers")]
        [Description("List of specifiers that appear for UMETA")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UMETA_MacroSpecifiers
        {
            get
            {
                if (Settings.Default.UMETA_MacroSpecifiers == null)
                {
                    Settings.Default.UMETA_MacroSpecifiers = new StringCollection();
                }
                return Settings.Default.UMETA_MacroSpecifiers;
            }
            set
            {
                Settings.Default.UMETA_MacroSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("UMETA")]
        [DisplayName("Advanced Settings")]
        [Description("List of advanced settings that appear for UMETA\r\nAdvanced settings are specifiers that specify a string and are not included in the meta specifier")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UMETA_AdvancedSettings
        {
            get
            {
                if (Settings.Default.UMETA_AdvancedSettings == null)
                {
                    Settings.Default.UMETA_AdvancedSettings = new StringCollection();
                }
                return Settings.Default.UMETA_AdvancedSettings;
            }
            set
            {
                Settings.Default.UMETA_AdvancedSettings = value;
                Settings.Default.Save();
            }
        }

        [Category("UMETA")]
        [DisplayName("Meta Specifiers")]
        [Description("List of meta specifiers that appear for UMETA\r\nWhen adding an item, follow this format (meta specifier name; input type)\r\nInputType = {NoInput or String or Bool or Int or Float}")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UMETA_MetaSpecifiers
        {
            get
            {
                if (Settings.Default.UMETA_MetaSpecifiers == null)
                {
                    Settings.Default.UMETA_MetaSpecifiers = new StringCollection();
                }
                return Settings.Default.UMETA_MetaSpecifiers;
            }
            set
            {
                Settings.Default.UMETA_MetaSpecifiers = value;
                Settings.Default.Save();
            }
        }

        [Category("Logging")]
        [DisplayName("LogCategory")]
        [Description("List of log category that appear for UE_LOG")]
        [Editor(typeof(CustomCollectionEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection Setting_UE_LOG_LogCategory
        {
            get
            {
                if (Settings.Default.UE_LOG_LogCategory == null)
                {
                    Settings.Default.UE_LOG_LogCategory = new StringCollection();
                }
                return Settings.Default.UE_LOG_LogCategory;
            }
            set
            {
                Settings.Default.UE_LOG_LogCategory = value;
                Settings.Default.Save();
            }
        }

        [Category("UPROPERTY")]
        [DisplayName("Document")]
        [Description("URL of the document about UPROPERTY that can be opened from the editor screen")]
        public string Setting_UPROPERTY_Document
        {
            get { return Settings.Default.UPROPERTY_Document; }
            set { Settings.Default.UPROPERTY_Document = value; Settings.Default.Save(); }
        }

        [Category("UFUNCTION")]
        [DisplayName("Document")]
        [Description("URL of the document about UFUNCTION that can be opened from the editor screen")]
        public string Setting_UFUNCTION_Document
        {
            get { return Settings.Default.UFUNCTION_Document; }
            set { Settings.Default.UFUNCTION_Document = value; Settings.Default.Save(); }
        }

        [Category("UCLASS")]
        [DisplayName("Document")]
        [Description("URL of the document about UCLASS that can be opened from the editor screen")]
        public string Setting_UCLASS_Document
        {
            get { return Settings.Default.UCLASS_Document; }
            set { Settings.Default.UCLASS_Document = value; Settings.Default.Save(); }
        }

        [Category("USTRUCT")]
        [DisplayName("Document")]
        [Description("URL of the document about USTRUCT that can be opened from the editor screen")]
        public string Setting_USTRUCT_Document
        {
            get { return Settings.Default.USTRUCT_Document; }
            set { Settings.Default.USTRUCT_Document = value; Settings.Default.Save(); }
        }

        [Category("UENUM")]
        [DisplayName("Document")]
        [Description("URL of the document about UENUM that can be opened from the editor screen")]
        public string Setting_UENUM_Document
        {
            get { return Settings.Default.UENUM_Document; }
            set { Settings.Default.UENUM_Document = value; Settings.Default.Save(); }
        }

        [Category("UINTERFACE")]
        [DisplayName("Document")]
        [Description("URL of the document about UINTERFACE that can be opened from the editor screen")]
        public string Setting_UINTERFACE_Document
        {
            get { return Settings.Default.UINTERFACE_Document; }
            set { Settings.Default.UINTERFACE_Document = value; Settings.Default.Save(); }
        }

        [Category("UMETA")]
        [DisplayName("Document")]
        [Description("URL of the document about UMETA that can be opened from the editor screen")]
        public string Setting_UMETA_Document
        {
            get { return Settings.Default.UMETA_Document; }
            set { Settings.Default.UMETA_Document = value; Settings.Default.Save(); }
        }

        [Category("Logging")]
        [DisplayName("Document")]
        [Description("URL of the document about Logging that can be opened from the editor screen")]
        public string Setting_Logging_Document
        {
            get { return Settings.Default.Logging_Document; }
            set { Settings.Default.Logging_Document = value; Settings.Default.Save(); }
        }

        [Category("Delegate")]
        [DisplayName("Document")]
        [Description("URL of the document about Delegate that can be opened from the editor screen")]
        public string Setting_Delegate_Document
        {
            get { return Settings.Default.Delegate_Document; }
            set { Settings.Default.Delegate_Document = value; Settings.Default.Save(); }
        }

        [Category("UPROPERTY")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the UPROPERTY macro is generated")]
        public string Setting_UPROPERTY_Template
        {
            get { return Settings.Default.UPROPERTY_Template; }
            set { Settings.Default.UPROPERTY_Template = value; Settings.Default.Save(); }
        }

        [Category("UFUNCTION")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the UFUNCTION macro is generated")]
        public string Setting_UFUNCTION_Template
        {
            get { return Settings.Default.UFUNCTION_Template; }
            set { Settings.Default.UFUNCTION_Template = value; Settings.Default.Save(); }
        }

        [Category("UCLASS")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the UCLASS macro is generated")]
        public string Setting_UCLASS_Template
        {
            get { return Settings.Default.UCLASS_Template; }
            set { Settings.Default.UCLASS_Template = value; Settings.Default.Save(); }
        }

        [Category("USTRUCT")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the USTRUCT macro is generated")]
        public string Setting_USTRUCT_Template
        {
            get { return Settings.Default.USTRUCT_Template; }
            set { Settings.Default.USTRUCT_Template = value; Settings.Default.Save(); }
        }

        [Category("UENUM")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the UENUM macro is generated")]
        public string Setting_UENUM_Template
        {
            get { return Settings.Default.UENUM_Template; }
            set { Settings.Default.UENUM_Template = value; Settings.Default.Save(); }
        }

        [Category("UINTERFACE")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the UINTERFACE macro is generated")]
        public string Setting_UINTERFACE_Template
        {
            get { return Settings.Default.UINTERFACE_Template; }
            set { Settings.Default.UINTERFACE_Template = value; Settings.Default.Save(); }
        }

        [Category("UMETA")]
        [DisplayName("Template")]
        [Description("Template code generated below the macro line when the UMETA macro is generated")]
        public string Setting_UMETA_Template
        {
            get { return Settings.Default.UMETA_Template; }
            set { Settings.Default.UMETA_Template = value; Settings.Default.Save(); }
        }
    }
}
