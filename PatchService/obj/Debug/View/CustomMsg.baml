   M S B A M L   `   `   `  ����G  CPatchService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PatchService.ResourcePatchService.Resource  5   	   5clr-namespace:PatchService.ViewPatchService.View  5   	   '    !PatchService.View.CustomMsgWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8controls#clr-namespace:PatchService.Resource     5   	   1localclr-namespace:PatchService.View     5   	   -   5        $	��200��5	   	   $	��300��6     � 
ResizeMode$  NoResize=�5
   	   $	��350��5   
   $	��210��6    � WindowStartupLocation$ CenterScreen=�5   	   
���   �.��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ #eeeeee��6    � Offset$ 0Q�6/   � 5      $ #c3c3c3��6   $ 1Q�6/   � 5      $ #eeeeee��6   $ 0.866Q�6/   5       � RowDefinitions 5   
   �� 5      $��0 �6   �� 5      .+�5   
   � -   5        $��30��5      $	��Top=�6   ���	#FF40A6D1$��1,0,0,0q�6e   $��2��6v    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#   	   ShadowDepth$	 3Q�63   
   	Direction$	
 270Q�6C      Opacity$	 0.2Q�6S    � ColumnDefinitions 5!      �� 5"      $
��Auto �6#   �� 5#      �� 5$      $
��Auto �6#   .+�5(      ,   &PatchService.Resource.HeaderDecoration  $��2��6(   $��
-1,0,132,0q�6<   ]� 5*      $��2��6   $��0��6,   $
��Left=�6<   ��.  .+�5,      �� $��	 Warning!��6   $��Center=�6,   $��Bottom=�6N   
�������$��SemiBold"�5-      $��14(�6.    J� IsManipulationEnabled . $��0,5,5,5q�6Y   $��	7,-1,0,-3q�6k   $��34��6~   $	��112��6�   5.      �� 5/      $��1��6   $��2��6-   $��Center=�6=    �� Orientation$ 
Horizontal=�6X   $��0,0,10,0q�50      .+�52      �� -   5        $��40��52      $��38��6$   $	����60      CloseMinMaxButtonStyle#���   6<   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�54      $��20(�6O   $��Bottom=�55      |�.  6W   56      57      � 58   
   $��0��6   $��35��6   $��Stretch=�6+   $��Stretch=�6I   $��0,0,0,80q�6e   $��1��6w    59      �� 5:      $��* �6#    5<      �� 5=      $
��Auto �6    $��35��6.   5?      � 5@   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��	0,63,0,68q�6Y   $��1��6l    5A      �� 5B      $��* �6#    5D      �� 5E      $
��Auto �6    $��35��6.   .+�5G      �� -   5         ��Name$ btn_Msg��5G      $��Stretch=�6&   $��0,5,5,0q�6D   $	��0 2q�6U     RoundedSettingsButtonStyle#���  5H   %   ���#eeeeee
�������$��0q�5I   O   ��68   Z� .��5J      �� $ 
Horizontal=�6   .+�5K      �� $��25,9,9,6q�6   $��29��6.   $��/Resource/Warning.ico��69   $
��Fill=�6X   $��29��6g   �� 5L      -   5         ��Name$ lbl_Msg��5L      $#��You are missing the anything.��6-   $��0,9,10,6q�6U   $��Center=�6g   $	��Top=�6�   $��Sitka Small)�6�   $��16(�6�   $	��253��6�    J� RenderTransformOrigin$ 
0.15,0.471&�6�   $��27��6�   5M      5N      5O      � 5P   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��
0,134,0,10q�6Y   $��1��6m    5Q      �� 5R      $��* �6#    5T      �� 5U      $
��Auto �6    $��30��6.   .+�5W      �� -   5        $ LoginButton��5W      $��OK��6*     RoundedButtonStyle#���  67   $
��25 5q�5X      $��SemiBold"�6(   
����������	#FF40A6D1$��14(�5Y   0   $��3��6W   $	��100��6d   $ 1.174,0.864&�6p   $��30��6�   $��Center=�6�   ��5X   Q   Z� 5Y   �   5Z      � 5[   
   $��0��6   $��Stretch=�6   $��Stretch=�6=   $��	0,149,0,1q�6Y   $��1��6l    5\      �� 5]      $��* �6#    5_      �� 5`      $
��Auto �6    $��30��6.   5b      5c      5d      