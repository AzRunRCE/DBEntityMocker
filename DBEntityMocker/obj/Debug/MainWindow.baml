   M S B A M L   `   `   `  ����I  EDBEntityMocker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DBEntityMockerDBEntityMocker  5   	       DBEntityMocker.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DBEntityMocker     5   	     � Title$  DBEntityMock��5   	   $��486.648��6   $��548.1��6/    � 
ResizeMode$ NoResize=�6=    � DataContext 5	      (   "DBEntityMocker.MainWindowViewModel  5
   
   6&   .��5      � $��0,0,0,1q�6    � RowDefinitions 5   
   �� 5      $��54 �6   �� 5       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��295* �6   �� 5      $	��57* �6   .+�5   
   �� $��
10,10,0,10q�6   $
��Left=�6F   $��34��6a   ��6%   �� *LoadAssembly+.��5      �� $��Center=�6   $��Center=�66   .+�5      �� $��Resources/magnifier.png��6   $��26��6:   5      5      �� 5   
   $��8,10,7,0q�6   $	��Top=�6%    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$
 Name��5       $��25��5!   F   $��1��6R   ��5   =   ��  �� Path$ EFTypes� �� SelectedValue 5!      �� $ SelectedEntityName�6R   �� 5#   
   $��	11,6,12,0q�6   $	��Top=�6$   	 J� RenderTransformOrigin$	 0.56,-1.282&�6Y   $��35��6}   $��2��6�   ��6=   �� *	GenMock+.��5$      �� $��Center=�6   $��Center=�66   .+�5%      �� $��Resources/accept.png��6   $��26��67   5&      5'      �� 5(   
   $��10,61,10,10q�6   $��3��6+   $��1��6?   . �5)      /� .T�5*      J� .*�6   .*�5+      �� �6   �� *	OutPutSrc+5,      5-      5.      �� 5/   
   $��37��6   $��	5,14,12,0q�6   $	��Top=�61   $��1��6�   $��1��6�   $��2��6�   ��6I   �� *
RequestSQL+
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 50   
   $��SQL Request��6   $
��Left=�6&   $��	10,20,0,0q�6A   $	��Top=�6T   $	 0.447,-0.367&�6l   $��1��6�   $��26��6�   $��82��6�   52      53      