   M S B A M L   `   `   `  ����  EASY COS3    -WpfTutorialSamples.Dialogs.InputDialogSample2   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   V RMaterialDesignThemes.Wpf, Version=3.1.0.1244, Culture=neutral, PublicKeyToken=nullFmaterialdesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$   Options de reinitalisation��5   	    � SizeToContent$ WidthAndHeight=�6,    � WindowStartupLocation$ CenterScreen=�6K   .��5      � $��15q�6    � ColumnDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ 	choix_sup��5      $��1��6&   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$# Choisissez les tables a vider��66   $��1��6u   $	��250��6�   .@�5      �� $��1) Employées��6   64   �� 5      $��2) Prets Remboursables��6   6>   �� 5      $ ��3) Prets Non Remboursables��6   6B   �� 5      $��4) Types de Prets��6   69   �� 5      $��5) Archives��6   63   5      � 5   
   $��2��6   $��2��6!   $��Right=�65   $��0,15,0,0q�6Q   .+�5      �� -   5        $��btnDialogOk��5   &    �� 	IsDefault . $��60��6S   $��0,0,10,0q�6a   .��6s   _Ok6x   �� 5      -   5         �� IsCancel . $��60��5   %   .��6H   
_Annuler6R   5      5      5      