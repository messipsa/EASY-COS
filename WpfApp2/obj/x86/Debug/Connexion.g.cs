﻿#pragma checksum "..\..\..\Connexion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5A94CD7154C338803F3A28466240CFE5B151BA5A3345C7E75BBDB626B092009E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp2;


namespace WpfApp2 {
    
    
    /// <summary>
    /// Window1
    /// </summary>
    public partial class Window1 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfApp2.Window1 Connexion_Grid;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid connexion_grid;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Nom_utilisateur;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox mot_de_passe;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox mot_de_passe_text;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon eye;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon eye_off;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Connexion;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement verification;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel error;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp2;component/connexion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Connexion.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Connexion_Grid = ((WpfApp2.Window1)(target));
            return;
            case 2:
            this.connexion_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 15 "..\..\..\Connexion.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Minimizer_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\..\Connexion.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Agrendir_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 21 "..\..\..\Connexion.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Arrét_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Nom_utilisateur = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\Connexion.xaml"
            this.Nom_utilisateur.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.mot_de_passe = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 8:
            this.mot_de_passe_text = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 38 "..\..\..\Connexion.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.hide_pwd_click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.eye = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 11:
            
            #line 41 "..\..\..\Connexion.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.hide_pwd_click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.eye_off = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 13:
            this.Connexion = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Connexion.xaml"
            this.Connexion.Click += new System.Windows.RoutedEventHandler(this.Se_Connecter_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.verification = ((System.Windows.Controls.MediaElement)(target));
            
            #line 48 "..\..\..\Connexion.xaml"
            this.verification.MediaEnded += new System.Windows.RoutedEventHandler(this.checked_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 15:
            this.error = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

