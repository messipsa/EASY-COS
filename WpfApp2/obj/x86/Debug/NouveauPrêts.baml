   M S B A M L   `   `   `  ����C  ?EASY COS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp2WpfApp2  5          WpfApp2.NouveauPrêts   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:WpfApp2     5      V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialdesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � 
�������.+�5   
   �� $	��650��6   $��	400,0,0,0q�6"   $	��Top=�65   .+�5      � .+�5      �� -   5          ��Name$
  Note��5       �� Orientation$ 
Horizontal=�6+   $y�Visible=�6D   .+�5      �� $��Note :��6   $��22(�6-   $
��Bold"�6;   $	��Top=�6M   $��Center=�6l   6�   �� 5      $���� La modification de prêt concerne uniquement sa 
 disponibilité . Une fois l'ajout d' un type est 
  confirmé vous ne pourrez plus modifié les autres 
 attributs.��6   $��21(�6�   $	��480��6�   6�   5      �� 5      -   5         ��Name$ Display_Note��5       � ToolTip$ Cacher la Note��60   $��30��6d   $��50��6p   $��550,-60,0,0q�6{   
������ ��6�   Z� .��5      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ Icon_Display_Note��5   .   $��Center=�6I   $��Center=�6f   $��Center=�6�   $��Center=�6�      Kind$ ArrowUpBoldCircleOutline=�6�   
���   �6�   5      5      �� 5      -   5        $  Ajout��5   0   $y�	Collapsed=�6   $	��301��6?   $��0,0,0,0q�6L   $	��Top=�6^   $��Center=�6v   $	��600��6�   .+�5      �� $.��(Veuillez Remplir le formulaire suivant :��6   $��30(�6K   6[   �� 5      -   5         ��Name$ Erreur_formulaire��5      $y�Hidden=�63   $;��5Vous devez fournir toutes les informations demandées��6G   $��18(�6�   
���  ��6�   �� 5      $ 
Horizontal=�6   $��	0,10,0,15q�66   .+�5      �� $��Remboursable : NON��6   $��15(�69   6I   d� 5      -   5         d�Name$ Remboursable��5   #    *  &MaterialDesignSwitchAccentToggleButton#��[  5      $, &MaterialDesignSwitchAccentToggleButton��5       d� 	IsChecked$ FalseY�5      �� 5      $
�� OUI��6   $��15(�6+   6;   5      �� 5       $ 
Horizontal=�6   $��0,0,0,15q�66   .+�5!      �� $��Disponible : NON��6   $��15(�67   6G   d� 5"      -   5        $ 
Disponible��5"   #   #��[  5#      $, &MaterialDesignSwitchAccentToggleButton��5$      $ FalseY�5%      �� 5&      $
�� OUI��6   $��15(�6+   6;   5'      �� 5(      $ 
Horizontal=�6   $��0,0,0,25q�66   .+�5)      �� $��Description :��6   $��15(�64   6D   �� 5*      -   5        	 ��Name$	 Description��5*   ,   $��15(�6   $	��500��6A   6O   5+      �� 5,      $ 
Horizontal=�6   .+�5-      �� $
��left=�6   $��Bottom=�69   $	��100��6T   $��32��6`   
 �� CornerRadius$
 20��6l   ���	#FF1FAB89$��40,0,0,0q�6�   ._�5.      �� -	   5        $ Confirmer_formulaire��5.   !   $��Bottom=�6?   $	��100��6Z   
������ $��	Confirmer��6   
�������$
��Bold"�6�   $��1,0,0,0q�6�   ��6�   Z� 6  5/      �� 50      $
 20��6   $	��100��6/   $��32��6;   $��Bottom=�6G   $
��Left=�6b   ���	#FF1FAB89$��	200,0,0,0q�6�   ._�51      �� -
   5        $ Annuler_formulaire��51   !   $��Bottom=�6=   $	��100��6X   
������ $��Annuler��6}   
�������$
��Bold"�6�   $��0,0,0,0q�6�   ��6�   Z� 6   52      53      �� 54      -   5        $ Ajouter_type_label��54   ,   $y�Hidden=�6   $��0,10,0,0q�6H   $1��+Le nouveau type a été créer avec succés��6Z   $��Center=�6�   ���	#FF272A33
�������$��DemiBold"�6�   $��15(�6�   6  55      �� 56      -   5        $  Modification��56   0   $y�	Collapsed=�6   .+�57      �� $��Modification d'un prêt :��6   $��30(�6;   6K   �� 58      $ 
Horizontal=�6   $��	0,10,0,15q�66   .+�59      �� $��Disponible : NON��6   $��15(�67   6G   d� 5:      -   5        $ Disponible_existant��5:   #   #��[  5;      $, &MaterialDesignSwitchAccentToggleButton��5<      $ FalseY�5=      �� 5>      $
�� OUI��6   $��15(�6+   6;   5?      �� 5@      -   5        $ Mise_a_jour_type��5@   ,   $y�Hidden=�6   $��	0,10,0,15q�6F   $ ��Le type a été mit a jour��6Y   $��Center=�6|   ���	#FF272A33
�������$��DemiBold"�6�   $��15(�6�   6�   �� 5A      $ 
Horizontal=�6   .+�5B      �� $
��left=�6   $��Bottom=�69   $	��100��6T   $��32��6`   $
 20��6l   ���	#FF1FAB89$��40,0,0,0q�6�   ._�5C      �� -   5        $ Confirmer_modification��5C   !   $��Bottom=�6A   $	��100��6\   
������ $��	Confirmer��6�   
�������$
��Bold"�6�   $��1,0,0,0q�6�   ��6�   Z� 6	  5D      �� 5E      $
 20��6   $	��100��6/   $��32��6;   $��Bottom=�6G   $
��Left=�6b   ���	#FF1FAB89$��	200,0,0,0q�6�   ._�5F      �� -   5        $ Annuler_modification��5F   !   $��Bottom=�6?   $	��100��6Z   
������ $��Annuler��6   
�������$
��Bold"�6�   $��0,0,0,0q�6�   ��6�   Z� 6  5G      5H      5I      5J      �� 5K   
   $
��Left=�6   $	��400��60   .+�5M      � $	��550��6   .+�5O      � -   5         �Name$ 	data_grid��5O      .+�5P      �� $��Types De Prêts existants��6   $��Center=�6?   $��20(�6c   $��DemiBold"�6q   6�   &   System.Windows.Controls.DataGrid  5Q      -   5          Name$ Types_Prets��5Q   �     MaterialDesignDataGrid#��[ 6      CanUserResizeColumns .     CanUserResizeRows .     CanUserAddRows .  $   HorizontalScrollBarVisibility$
 Auto=�6�   "   VerticalScrollBarVisibility$
 Auto=�6�      AutoGenerateColumns .  $��0,59,0,0q�6   � 	Resources 5R      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell �� Triggers P� Property P� Value)  #System.Windows.Controls.DataGridRow��%f  '        '  g    '  �    �� 5T      "  6$   .�5U   "   ��  � ��$ 50��6;   6H   �� 5V   "    � ��$ Verdana)�6?   �� 5W   "    � ��
 �����6@   �� 5X   "    � ��$ 0,0,2,1q�6D   �� 5Y   "    � ��$ Center=�6Y   6j   �� 5Z   "    � ��$ 15(�6=   6J   �� 5[   "    � ��
 �����6T   �� 5\   "    � ��
 �����6S   5^      �� 5_      "  6$   .�5`   "   ��  � ��$ Verdana)�6?   �� 5a   "    � ��
 ���� 6@   �� 5b   "    � ��$ 0,0,2,1q�6D   �� 5c   "    � ��$ Center=�6Y   6j   �� 5d   "    � ��$ 15(�6=   6J    5e   "   P� 5f   &    �@ 
IsSelected . . �5g   *   ��  � ��
 ���f�6G   5h   '   5j      �� 5l      "  6$    5m   "   P� 5n   &    �@ 
IsSelected . . �5o   *   ��  � ��
 ���f�6G   5p   '   5r      5s         Columns 5t      4  .System.Windows.Controls.DataGridTemplateColumn  5u      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .  -   5        check_box_modifier��   Header$ 
          ��5u   d      
Visibility$ Visible=�6x      DisplayIndex$ 0��6�      CellTemplate 5v   "   �� 5w   &   .R�5x   *   ��  63   �� *	Include+5y   '   5{      0  *System.Windows.Controls.DataGridTextColumn  5|       . $- '              Description              ��6C       Width5	  /System.Windows.Controls.DataGridLengthConverter$
  auto	 6t   1
  +System.Windows.Controls.DataGridBoundColumn! 
  Binding! 6�   �� *Description+  5}       . $- '              Remboursable             ��6C   $
  auto	 6t   ! 6�   �� *Remboursable+  5~       . $- '              Disponible               ��6C   $
  auto	 6t   ! 6�   �� *
Disponible+5�      5�      5�      �� 5�      -   5        $ Erreur_Modification��5�      $y�Hidden=�61   $��	0,10,0,20q�6E   $;��5Veuillez Sélectionner Un Type De Prêt a Modifié !!��6X   $��Center=�6�   $��15(�6�   
���  ��6�   �� 5�      $ 
Horizontal=�6   .+�5�      �� $
��left=�6   $��Bottom=�65   $	��100��6P   $��32��6\   $
 20��6h   ���	#FF1FAB89$��40,0,0,0q�6�   ._�5�      �� -   5        $ Modifier_type��5�      $��Bottom=�64   $	��100��6O   
������ $��Modifier��6t   
�������$
��Bold"�6�   $��1,0,0,0q�6�   ��6�   Z� 6�   5�      �� 5�      $
 20��6   $	��100��6+   $��32��67   $��Bottom=�6C   $
��Left=�6^   ���	#FF1FAB89$��90,0,0,0q�6�   ._�5�      �� -   5        $ Ajouter_type��5�      $��Bottom=�63   $	��100��6N   
������ $��Ajouter��6s   
�������$
��Bold"�6�   $��0,0,0,0q�6�   ��6�   Z� 6�   5�      5�      5�      5�      5�      